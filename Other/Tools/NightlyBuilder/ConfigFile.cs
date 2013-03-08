﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace NightlyBuilder
{
	public sealed class ConfigFile
	{
		public string SolutionPath { get; set; }
		public string BuildResultDir { get; set; }
		public string DocSolutionPath { get; set; }
		public string DocBuildResultDir { get; set; }
		public string DocBuildResultFile { get; set; }
		public string AdditionalFileDir { get; set; }
		public string TargetDir { get; set; }
		public string PackageDir { get; set; }
		public string PackageName { get; set; }
		public bool NoDocs { get; set; }
		public List<string> FileCopyBlackList { get; set; }

		public void Save(string filePath)
		{
			using (FileStream stream = File.Open(filePath, FileMode.Create))
			{
				this.Save(stream);
			}
		}
		public void Save(Stream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
			serializer.Serialize(stream, this);
		}

		public static ConfigFile Load(string filePath)
		{
			ConfigFile result;
			using (FileStream stream = File.OpenRead(filePath))
			{
				result = Load(stream);
			}
			return result;
		}
		public static ConfigFile Load(Stream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
			return serializer.Deserialize(stream) as ConfigFile;
		}
	}
}
