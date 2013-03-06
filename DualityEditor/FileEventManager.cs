﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Text.RegularExpressions;

using Duality;
using Duality.Resources;
using Duality.ObjectManagers;

using DualityEditor.Forms;
using DualityEditor.CorePluginInterface;

using OpenTK;

using Ionic.Zip;
using WeifenLuo.WinFormsUI.Docking;

namespace DualityEditor
{
	public static class FileEventManager
	{
		private	static DateTime						lastEventProc			= DateTime.Now;
		private static FileSystemWatcher			pluginWatcher			= null;
		private static FileSystemWatcher			dataDirWatcher			= null;
		private static FileSystemWatcher			sourceDirWatcher		= null;
		private	static HashSet<string>				reimportSchedule		= new HashSet<string>();
		private	static List<string>					editorJustSavedRes		= new List<string>();
		private	static List<FileSystemEventArgs>	dataDirEventBuffer		= new List<FileSystemEventArgs>();
		private	static List<FileSystemEventArgs>	sourceDirEventBuffer	= new List<FileSystemEventArgs>();


		public	static	event	EventHandler<ResourceEventArgs>				ResourceCreated		= null;
		public	static	event	EventHandler<ResourceEventArgs>				ResourceDeleted		= null;
		public	static	event	EventHandler<ResourceEventArgs>				ResourceModified	= null;
		public	static	event	EventHandler<ResourceRenamedEventArgs>		ResourceRenamed		= null;
		public	static	event	EventHandler<FileSystemEventArgs>			SrcFileDeleted		= null;
		public	static	event	EventHandler<FileSystemEventArgs>			SrcFileModified		= null;
		public	static	event	EventHandler<FileSystemEventArgs>			SrcFileRenamed		= null;
		public	static	event	EventHandler<FileSystemEventArgs>			PluginChanged		= null;
		public	static	event	EventHandler<BeginGlobalRenameEventArgs>	BeginGlobalRename	= null;
		
		
		internal static void Init()
		{
			// Set up different file system watchers
			pluginWatcher = new FileSystemWatcher();
			pluginWatcher.SynchronizingObject = DualityEditorApp.MainForm;
			pluginWatcher.EnableRaisingEvents = false;
			pluginWatcher.Filter = "*.dll";
			pluginWatcher.IncludeSubdirectories = true;
			pluginWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
			pluginWatcher.Path = EditorHelper.PluginDirectory;
			pluginWatcher.Changed += corePluginWatcher_Changed;
			pluginWatcher.Created += corePluginWatcher_Changed;
			pluginWatcher.EnableRaisingEvents = true;
			
			dataDirWatcher = new FileSystemWatcher();
			dataDirWatcher.SynchronizingObject = DualityEditorApp.MainForm;
			dataDirWatcher.EnableRaisingEvents = false;
			dataDirWatcher.IncludeSubdirectories = true;
			dataDirWatcher.Path = EditorHelper.DataDirectory;
			dataDirWatcher.Created += fileSystemWatcher_ForwardData;
			dataDirWatcher.Changed += fileSystemWatcher_ForwardData;
			dataDirWatcher.Deleted += fileSystemWatcher_ForwardData;
			dataDirWatcher.Renamed += fileSystemWatcher_ForwardData;
			dataDirWatcher.EnableRaisingEvents = true;
			
			sourceDirWatcher = new FileSystemWatcher();
			sourceDirWatcher.SynchronizingObject = DualityEditorApp.MainForm;
			sourceDirWatcher.EnableRaisingEvents = false;
			sourceDirWatcher.IncludeSubdirectories = true;
			sourceDirWatcher.Path = EditorHelper.SourceDirectory;
			sourceDirWatcher.Created += fileSystemWatcher_ForwardSource;
			sourceDirWatcher.Changed += fileSystemWatcher_ForwardSource;
			sourceDirWatcher.Deleted += fileSystemWatcher_ForwardSource;
			sourceDirWatcher.Renamed += fileSystemWatcher_ForwardSource;
			sourceDirWatcher.EnableRaisingEvents = true;

			// Register events
			DualityEditorApp.MainForm.Activated += mainForm_Activated;
			DualityEditorApp.Idling += DualityEditorApp_Idle;
			Resource.ResourceSaved += Resource_ResourceSaved;
		}
		internal static void Terminate()
		{
			// Unregister events
			DualityEditorApp.MainForm.Activated -= mainForm_Activated;
			DualityEditorApp.Idling -= DualityEditorApp_Idle;
			Resource.ResourceSaved -= Resource_ResourceSaved;

			// Destroy file system watchers
			pluginWatcher.EnableRaisingEvents = false;
			pluginWatcher.Changed -= corePluginWatcher_Changed;
			pluginWatcher.Created -= corePluginWatcher_Changed;
			pluginWatcher.SynchronizingObject = null;
			pluginWatcher.Dispose();
			pluginWatcher = null;

			dataDirWatcher.EnableRaisingEvents = false;
			dataDirWatcher.Created -= fileSystemWatcher_ForwardData;
			dataDirWatcher.Changed -= fileSystemWatcher_ForwardData;
			dataDirWatcher.Deleted -= fileSystemWatcher_ForwardData;
			dataDirWatcher.Renamed -= fileSystemWatcher_ForwardData;
			dataDirWatcher.SynchronizingObject = null;
			dataDirWatcher.Dispose();
			dataDirWatcher = null;

			sourceDirWatcher.EnableRaisingEvents = false;
			sourceDirWatcher.Created -= fileSystemWatcher_ForwardSource;
			sourceDirWatcher.Changed -= fileSystemWatcher_ForwardSource;
			sourceDirWatcher.Deleted -= fileSystemWatcher_ForwardSource;
			sourceDirWatcher.Renamed -= fileSystemWatcher_ForwardSource;
			sourceDirWatcher.SynchronizingObject = null;
			sourceDirWatcher.Dispose();
			sourceDirWatcher = null;
		}


		private static bool IsResPathIgnored(string filePath)
		{
			return IsPathIgnored(filePath);
		}
		private static bool IsSourcePathIgnored(string filePath)
		{
			return IsPathIgnored(filePath);
		}
		private static bool IsPathIgnored(string filePath)
		{
			if (!File.Exists(filePath) && !Directory.Exists(filePath)) return false;
			if (!PathHelper.IsPathVisible(filePath)) return true;
			if (filePath.Contains(@"/.svn/") || filePath.Contains(@"\.svn\")) return true;
			return false;
		}
		
		private static FileSystemEventArgs FetchFileSystemEvent(List<FileSystemEventArgs> dirEventList, string basePath)
		{
			if (dirEventList.Count == 0) return null;

			FileSystemEventArgs	current	= dirEventList[0];
			dirEventList.RemoveAt(0);

			// Discard or pack rename-rename
			if (current.ChangeType == WatcherChangeTypes.Renamed)
			{
				RenamedEventArgs rename = current as RenamedEventArgs;

				while (rename != null)
				{
					RenamedEventArgs renameB = dirEventList.OfType<RenamedEventArgs>().FirstOrDefault(e => 
						Path.GetFileName(e.OldFullPath) == Path.GetFileName(rename.FullPath));
					if (renameB != null)
					{
						dirEventList.Remove(renameB);
						rename = new RenamedEventArgs(WatcherChangeTypes.Renamed, basePath, renameB.Name, rename.OldName);
						current = rename;
					}
					else break;
				}

				// Discard useless renames
				if (rename.OldFullPath == rename.FullPath) return null;
			}

			// Pack del-rename to change
			if (current.ChangeType == WatcherChangeTypes.Deleted)
			{
				FileSystemEventArgs del		= current;
				RenamedEventArgs	rename	= null;
				
				rename = dirEventList.OfType<RenamedEventArgs>().FirstOrDefault(e => e.FullPath == del.FullPath);
				dirEventList.Remove(rename);

				if (del != null && rename != null) return new FileSystemEventArgs(WatcherChangeTypes.Changed, basePath, del.Name);
			}

			// Pack del-create to rename
			if (current.ChangeType == WatcherChangeTypes.Deleted)
			{
				FileSystemEventArgs del		= current;
				FileSystemEventArgs create	= null;

				create = dirEventList.FirstOrDefault(e => 
					e.ChangeType == WatcherChangeTypes.Created && 
					Path.GetFileName(e.FullPath) == Path.GetFileName(del.FullPath));
				dirEventList.Remove(create);

				if (del != null && create != null) return new RenamedEventArgs(WatcherChangeTypes.Renamed, basePath, create.Name, del.Name);
			}
			
			return current;
		}
		private static void PushDataDirEvent(FileSystemEventArgs e)
		{
			if (IsResPathIgnored(e.FullPath)) return;
			dataDirEventBuffer.RemoveAll(f => f.FullPath == e.FullPath && f.ChangeType == e.ChangeType);
			dataDirEventBuffer.Add(e);
		}
		private static void ProcessDataDirEvents()
		{
			List<ResourceRenamedEventArgs> renameEventBuffer = null;

			// Process events
			while (dataDirEventBuffer.Count > 0)
			{
				FileSystemEventArgs e = FetchFileSystemEvent(dataDirEventBuffer, dataDirWatcher.Path);
				if (e == null) continue;

				if (e.ChangeType == WatcherChangeTypes.Changed)
				{
					ResourceEventArgs args = new ResourceEventArgs(e.FullPath);
					bool justSaved = editorJustSavedRes.Contains(Path.GetFullPath(e.FullPath));
					// Ignore stuff saved by the editor itself
					if (!justSaved && (Resource.IsResourceFile(e.FullPath) || args.IsDirectory))
					{
						// Unregister outdated resources, if modified outside the editor
						if (!args.IsDirectory && ContentProvider.IsContentRegistered(args.Path))
						{
							bool isCurrentScene = args.Content.Is<Scene>() && Scene.Current == args.Content.Res;
							if (isCurrentScene || DualityEditorApp.IsResourceUnsaved(e.FullPath))
							{
								DialogResult result = MessageBox.Show(
									String.Format(EditorRes.GeneralRes.Msg_ConfirmReloadResource_Text, e.FullPath), 
									EditorRes.GeneralRes.Msg_ConfirmReloadResource_Caption, 
									MessageBoxButtons.YesNo,
									MessageBoxIcon.Exclamation);
								if (result == DialogResult.Yes)
								{
									string curScenePath = Scene.CurrentPath;
									ContentProvider.UnregisterContent(args.Path);
									if (isCurrentScene) Scene.Current = ContentProvider.RequestContent<Scene>(curScenePath).Res;
								}
							}
							else
								ContentProvider.UnregisterContent(args.Path);
						}

						if (ResourceModified != null) ResourceModified(null, args);
					}
				}
				else if (e.ChangeType == WatcherChangeTypes.Created)
				{
					if (File.Exists(e.FullPath))
					{
						// Register newly detected ressource file
						if (Resource.IsResourceFile(e.FullPath))
						{
							if (ResourceCreated != null)
								ResourceCreated(null, new ResourceEventArgs(e.FullPath));
						}
						// Import non-ressource file
						else
						{
							bool abort = false;

							if (FileImportProvider.IsImportFileExisting(e.FullPath))
							{
								DialogResult result = MessageBox.Show(
									String.Format(EditorRes.GeneralRes.Msg_ImportConfirmOverwrite_Text, e.FullPath), 
									EditorRes.GeneralRes.Msg_ImportConfirmOverwrite_Caption, 
									MessageBoxButtons.YesNo, 
									MessageBoxIcon.Warning);
								abort = result == DialogResult.No;
							}

							if (!abort)
							{
								bool importedSuccessfully = FileImportProvider.ImportFile(e.FullPath);
								if (!importedSuccessfully)
								{
									MessageBox.Show(
										String.Format(EditorRes.GeneralRes.Msg_CantImport_Text, e.FullPath), 
										EditorRes.GeneralRes.Msg_CantImport_Caption, 
										MessageBoxButtons.OK, 
										MessageBoxIcon.Error);
								}
								abort = !importedSuccessfully;
							}
						}
					}
					else if (Directory.Exists(e.FullPath))
					{
						// Register newly detected ressource directory
						if (ResourceCreated != null)
							ResourceCreated(null, new ResourceEventArgs(e.FullPath));
					}
				}
				else if (e.ChangeType == WatcherChangeTypes.Deleted)
				{
					// Is it a Resource file or just something else?
					ResourceEventArgs args = new ResourceEventArgs(e.FullPath);
					if (Resource.IsResourceFile(e.FullPath) || args.IsDirectory)
					{

						// Unregister no-more existing resources
						if (args.IsDirectory)	ContentProvider.UnregisterContentTree(args.Path);
						else					ContentProvider.UnregisterContent(args.Path);

						if (ResourceDeleted != null)
							ResourceDeleted(null, args);
					}
				}
				else if (e.ChangeType == WatcherChangeTypes.Renamed)
				{
					// Is it a Resource file or just something else?
					RenamedEventArgs re = e as RenamedEventArgs;
					ResourceRenamedEventArgs args = new ResourceRenamedEventArgs(re.FullPath, re.OldFullPath);
					if (Resource.IsResourceFile(e.FullPath) || args.IsDirectory)
					{
						// Rename content registerations
						if (args.IsDirectory)	ContentProvider.RenameContentTree(args.OldPath, args.Path);
						else					ContentProvider.RenameContent(args.OldPath, args.Path);

						// Query skipped paths
						bool isSkippedPath = false;
						if (BeginGlobalRename != null)
						{
							BeginGlobalRenameEventArgs beginGlobalRenameArgs = new BeginGlobalRenameEventArgs(args.Path, args.OldPath);
							BeginGlobalRename(null, beginGlobalRenameArgs);
							isSkippedPath = beginGlobalRenameArgs.Cancel;
						}

						if (!isSkippedPath)
						{
							// Buffer rename event to perform the global rename for all at once.
							if (renameEventBuffer == null) renameEventBuffer = new List<ResourceRenamedEventArgs>();
							renameEventBuffer.Add(args);
						}

						if (ResourceRenamed != null) ResourceRenamed(null, args);
					}
				}
			}

			// If required, perform a global rename operation in all existing content
			if (renameEventBuffer != null)
			{
				// Don't do it now - schedule it for the main form event loop so we don't block here.
				DualityEditorApp.MainForm.BeginInvoke((Action)delegate() {
					ProcessingBigTaskDialog taskDialog = new ProcessingBigTaskDialog( 
						EditorRes.GeneralRes.TaskRenameContentRefs_Caption, 
						EditorRes.GeneralRes.TaskRenameContentRefs_Desc, 
						async_RenameContentRefs, renameEventBuffer);
					taskDialog.ShowDialog(DualityEditorApp.MainForm);
				});
			}
		}
		private static void PushSourceDirEvent(FileSystemEventArgs e)
		{
			if (IsSourcePathIgnored(e.FullPath)) return;
			sourceDirEventBuffer.RemoveAll(f => f.FullPath == e.FullPath && f.ChangeType == e.ChangeType);
			sourceDirEventBuffer.Add(e);
		}
		private static void ProcessSourceDirEvents()
		{
			// Process events
			while (sourceDirEventBuffer.Count > 0)
			{
				FileSystemEventArgs e = FetchFileSystemEvent(sourceDirEventBuffer, sourceDirWatcher.Path);
				if (e == null) continue;

				if (e.ChangeType == WatcherChangeTypes.Changed)
				{
					if (File.Exists(e.FullPath) && PathHelper.IsPathLocatedIn(e.FullPath, EditorHelper.SourceMediaDirectory)) 
						reimportSchedule.Add(e.FullPath);
					if (SrcFileModified != null) SrcFileModified(null, e);
				}
				else if (e.ChangeType == WatcherChangeTypes.Created)
				{
				}
				else if (e.ChangeType == WatcherChangeTypes.Deleted)
				{
					if (SrcFileDeleted != null) SrcFileDeleted(null, e);
				}
				else if (e.ChangeType == WatcherChangeTypes.Renamed)
				{
					RenamedEventArgs rename = e as RenamedEventArgs;
					FileImportProvider.NotifyFileRenamed(rename.OldFullPath, rename.FullPath);
					if (SrcFileRenamed != null) SrcFileRenamed(null, e);
				}
			}
		}

		private static void DualityEditorApp_Idle(object sender, EventArgs e)
		{
			// Process file / source events regularily, if no modal dialog is open.
			if ((DateTime.Now - lastEventProc).TotalMilliseconds > 100.0d)
			{
				ProcessSourceDirEvents();
				ProcessDataDirEvents();
				editorJustSavedRes.Clear();
				lastEventProc = DateTime.Now;
			}
		}
		private static void Resource_ResourceSaved(object sender, Duality.ResourceEventArgs e)
		{
			if (string.IsNullOrEmpty(e.Path)) return;	// Ignore Resources without a path.
			if (e.IsDefaultContent) return;				// Ignore default content
			editorJustSavedRes.Add(Path.GetFullPath(e.Path));
		}
		
		private static void mainForm_Activated(object sender, EventArgs e)
		{
			// Perform scheduled source file reimports
			if (reimportSchedule.Count > 0)
			{
				// Hacky: Wait a little for the files to be accessable again (Might be used by another process)
				System.Threading.Thread.Sleep(50);

				foreach (string file in reimportSchedule)
				{
					if (!File.Exists(file)) continue;
					FileImportProvider.ReimportFile(file);
				}
				reimportSchedule.Clear();
			}
		}
		private static void fileSystemWatcher_ForwardSource(object sender, FileSystemEventArgs e)
		{
			PushSourceDirEvent(e);
		}
		private static void fileSystemWatcher_ForwardData(object sender, FileSystemEventArgs e)
		{
			PushDataDirEvent(e);
		}
		private static void corePluginWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			if (PluginChanged != null)
				PluginChanged(sender, e);
		}

		private static System.Collections.IEnumerable async_RenameContentRefs(ProcessingBigTaskDialog.WorkerInterface state)
		{
			var renameData = state.Data as List<ResourceRenamedEventArgs>;
			int totalCounter = 0;
			int fileCounter = 0;
			
			// Rename in static application data
			state.StateDesc = "DualityApp Data"; yield return null;
			DualityApp.LoadAppData();
			DualityApp.LoadUserData();
			DualityApp.LoadMetaData();
			state.Progress += 0.04f; yield return null;

			totalCounter += async_RenameContentRefs_Perform(DualityApp.AppData, renameData);
			totalCounter += async_RenameContentRefs_Perform(DualityApp.UserData, renameData);
			totalCounter += async_RenameContentRefs_Perform(DualityApp.MetaData, renameData);
			state.Progress += 0.02f; yield return null;

			DualityApp.SaveAppData();
			DualityApp.SaveUserData();
			DualityApp.SaveMetaData();
			state.Progress += 0.04f; yield return null;

			// Special case: Current Scene in sandbox mode
			if (Sandbox.IsActive)
			{
				// Because changes we'll do will be discarded when leaving the sandbox we'll need to
				// do it the hard way - manually load an save the file.
				state.StateDesc = "Current Scene"; yield return null;
				Scene curScene = Resource.LoadResource<Scene>(Scene.CurrentPath);
				fileCounter = async_RenameContentRefs_Perform(curScene, renameData);
				totalCounter += fileCounter;
				if (fileCounter > 0) curScene.Save(Scene.CurrentPath);
			}
			// Special case: Current Scene NOT in sandbox mode, but still unsaved
			else if (Scene.Current.IsRuntimeResource)
			{
				state.StateDesc = "Current Scene"; yield return null;
				fileCounter = async_RenameContentRefs_Perform(Scene.Current, renameData);
				if (fileCounter > 0)
					DualityEditorApp.NotifyObjPropChanged(null, new ObjectSelection(Scene.Current.AllObjects));
				totalCounter += fileCounter;
			}

			// Rename in actual content
			var availContent = ContentProvider.GetAvailContent<Resource>();
			var reloadContent = new List<IContentRef>();
			List<string> resFiles = Resource.GetResourceFiles();
			foreach (string file in resFiles)
			{
				state.StateDesc = file; yield return null;

				// Loaded for the first time? Schedule for later reload.
				bool reload = !availContent.Any(r => r.Path == file);
				// Keep in mind that this operation is performed while Duality content was
				// in an inconsistent state. Loading Resources now may lead to wrong data.
				// Because the ContentRefs might be wrong right now.

				// Load content
				var cr = ContentProvider.RequestContent(file);
				state.Progress += 0.35f / resFiles.Count; yield return null;

				// Perform rename and flag unsaved
				fileCounter = async_RenameContentRefs_Perform(cr.Res, renameData);
				totalCounter += fileCounter;
				if (fileCounter > 0)
				{
					if (!reload)
						DualityEditorApp.FlagResourceUnsaved(cr.Res);
					else
						reloadContent.Add(cr);
				}
				state.Progress += 0.35f / resFiles.Count; yield return null;
			}

			// Perform Resource unload where scheduled
			state.StateDesc = "Saving Resources.."; yield return null;
			foreach (IContentRef cr in reloadContent)
			{
				cr.Res.Save();
				ContentProvider.UnregisterContent(cr.Path);
				state.Progress += 0.2f / reloadContent.Count; yield return null;
			}
		}
		private static int async_RenameContentRefs_Perform(object obj, List<ResourceRenamedEventArgs> args)
		{
			int counter = 0;
			ReflectionHelper.VisitObjectsDeep<IContentRef>(obj, r => 
			{
				if (r.IsDefaultContent) return r;
				if (r.IsExplicitNull) return r;
				if (string.IsNullOrEmpty(r.Path)) return r;

				foreach (ResourceRenamedEventArgs e in args)
				{
					if (e.IsResource && r.Path == e.OldPath)
					{
						r.Path = e.Path;
						counter++;
						break;
					}
					else if (e.IsDirectory && PathHelper.IsPathLocatedIn(r.Path, e.OldPath))
					{
						r.Path = r.Path.Replace(e.OldPath, e.Path);
						counter++;
						break;
					}
				}
				return r; 
			});
			return counter;
		}
	}
}
