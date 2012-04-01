using Duality;

namespace DualityEditor
{
	public interface IFileImporter
	{
		bool CanImportFile(string srcFile);
		void ImportFile(string srcFile, string targetName, string targetDir);
		string[] GetOutputFiles(string srcFile, string targetName, string targetDir);

		bool IsUsingSrcFile(Resource r, string srcFile);
		void ReimportFile(Resource r, string srcFile);
	}
}
