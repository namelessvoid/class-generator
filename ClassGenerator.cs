using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ClassGenerator
{
	class ClassGenerator
	{
		static ClassGenerator()
		{
	        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
	        UriBuilder uri = new UriBuilder(codeBase);
	        string path = Uri.UnescapeDataString(uri.Path);

	        ResourceDirectory = Path.GetDirectoryName(path);
		}

		public static bool GenerateClass(ClassMetaInfo classMetaInfo)
		{
			if(!GenerateHeader(classMetaInfo))
			{
				return false;
			}

			return GenerateSource(classMetaInfo);
		}

		private static bool GenerateHeader(ClassMetaInfo classMetaInfo)
		{
			Console.WriteLine("Generating header file.");

			var headerTemplate = GetEmbeddedResource("HeaderTemplate.hpp");
			var header = FillTemplateWithClassMetaInfo(headerTemplate, classMetaInfo);
			return CreateFile(header, $"include/{classMetaInfo.Location}", $"{classMetaInfo.ClassName.ToLower()}.hpp");
		}

		private static bool GenerateSource(ClassMetaInfo classMetaInfo)
		{
			Console.WriteLine("Generating source file.");

			var sourceTemplate = GetEmbeddedResource("SourceTemplate.cpp");
			var source = FillTemplateWithClassMetaInfo(sourceTemplate, classMetaInfo);
			return CreateFile(source, $"src/{classMetaInfo.Location}", $"{classMetaInfo.ClassName.ToLower()}.cpp");
		}

		private static string FillTemplateWithClassMetaInfo(string template, ClassMetaInfo classMetaInfo)
		{
			return template.Replace("CLASSNAMECAPS", classMetaInfo.ClassName.ToUpper())
			               .Replace("NAMESPACECAPS", classMetaInfo.Namespace.ToUpper())
						   .Replace("CLASSNAME", classMetaInfo.ClassName)
			               .Replace("NAMESPACE", classMetaInfo.Namespace)
			               .Replace("LOCATION", classMetaInfo.Location);
		}

		private static string GetEmbeddedResource(string resourceName)
		{
			var path = Path.Combine(ResourceDirectory, resourceName);
			Console.WriteLine(path);
			var resource = File.ReadAllText(path);
			return resource;

			var assembly = Assembly.GetEntryAssembly();
			var resourceFQDN = $"{assembly.GetName().Name}.{resourceName}";
			var resourceStream = assembly.GetManifestResourceStream(resourceFQDN);

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
		}

		private static bool CreateFile(string content, string directory, string fileName)
		{
			var fullPath = $"{directory}/{fileName}";

			if(File.Exists(fullPath))
			{
				Console.WriteLine($"Cannot create file \"{fullPath}\" because it already exists.");
				return false;
			}

			if(!Directory.Exists(directory))
			{
				Console.WriteLine($"Creating directory \"{directory}\"");
				Directory.CreateDirectory(directory);
			}

			Console.WriteLine($"Creating file \"{fullPath}\"");
			File.WriteAllText(fullPath, content);

			return true;
		}

		private static string ResourceDirectory
		{
			get; set;
		}
	}
}
