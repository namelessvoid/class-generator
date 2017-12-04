using System;
using System.IO;

namespace ClassGenerator
{
	interface IFilesystem
	{
		string ReadFile(string path);

		void WriteFile(string path, string content);

		bool FileExists(string path);

		bool DirectoryExists(string path);

		void CreateDirectory(string path);
	}

	class Filesystem : IFilesystem
	{
		public string ReadFile(string path)
		{
			return File.ReadAllText(path);
		}

		public void WriteFile(string path, string content)
		{
			File.WriteAllText(path, content);
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public void CreateDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}
	}
}
