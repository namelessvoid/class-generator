using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ClassGenerator
{
    class ClassGenerator
    {
        public ClassGenerator()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);

            ResourceDirectory = Path.Combine(Path.GetDirectoryName(path), "templates");
        }

        public bool GenerateClass(ClassMetaInfo classMetaInfo)
        {
            if(!GenerateHeader(classMetaInfo))
            {
                return false;
            }

            return GenerateSource(classMetaInfo);
        }

        private bool GenerateHeader(ClassMetaInfo classMetaInfo)
        {
            Console.WriteLine("Generating header file.");

            var headerTemplate = GetTemplate("HeaderTemplate.hpp");
            var header = FillTemplateWithClassMetaInfo(headerTemplate, classMetaInfo);
            return CreateFile(header, $"include/{classMetaInfo.Location}", $"{classMetaInfo.ClassName.ToLower()}.hpp");
        }

        private bool GenerateSource(ClassMetaInfo classMetaInfo)
        {
            Console.WriteLine("Generating source file.");

            var sourceTemplate = GetTemplate("SourceTemplate.cpp");
            var source = FillTemplateWithClassMetaInfo(sourceTemplate, classMetaInfo);
            return CreateFile(source, $"src/{classMetaInfo.Location}", $"{classMetaInfo.ClassName.ToLower()}.cpp");
        }

        private string FillTemplateWithClassMetaInfo(string template, ClassMetaInfo classMetaInfo)
        {
            return template.Replace("CLASSNAMECAPS", classMetaInfo.ClassName.ToUpper())
                           .Replace("CLASSNAMELOWER", classMetaInfo.ClassName.ToLower())
                           .Replace("NAMESPACECAPS", classMetaInfo.Namespace.ToUpper())
                           .Replace("CLASSNAME", classMetaInfo.ClassName)
                           .Replace("NAMESPACE", classMetaInfo.Namespace)
                           .Replace("LOCATION", classMetaInfo.Location);
        }

        private string GetTemplate(string templateFileName)
        {
            var path = Path.Combine(ResourceDirectory, templateFileName);
            var resource = File.ReadAllText(path);
            return resource;
        }

        private bool CreateFile(string content, string directory, string fileName)
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

        private string ResourceDirectory
        {
            get; set;
        }
    }
}
