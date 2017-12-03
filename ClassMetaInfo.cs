using System;
using System.Text.RegularExpressions;

namespace ClassGenerator
{
    internal class ClassMetaInfo
    {
        public static ClassMetaInfo FromDefinition(string definition)
        {
            Console.WriteLine($"Constructing class from definition: \"{definition}\".");

            var matches = Regex.Matches(definition, @"^(?<location>[\w/]+?):(?<namespace>[\w]+?)::(?<class>[\w]+?)$");

            if(matches.Count != 1)
            {
                Console.WriteLine("Malformed definition. See --help for further information.");
                return null;
            }

            var components = matches[0];
            var location = components.Groups["location"].Value;
            var namespaceName = components.Groups["namespace"].Value;
            var className = components.Groups["class"].Value;

            return new ClassMetaInfo
            {
                Location = location,
                ClassName = className,
                Namespace = namespaceName
            };
        }

        // The path at which the class is placed.
        // NOT prepended with 'include/' or 'src/'.
        public string Location { get; private set; }

        public string ClassName { get; private set; }

        public string Namespace { get; private set; }

        // Hide constructor
        private ClassMetaInfo()
        {}
    }
}
