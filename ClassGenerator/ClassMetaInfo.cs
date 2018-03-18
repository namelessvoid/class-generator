using System;
using System.Text.RegularExpressions;

namespace ClassGenerator
{
    public class ClassMetaInfo
    {
        // The path at which the class is placed.
        // NOT prepended with 'include/' or 'src/'.
        public string Location { get; private set; }

        public string Namespace { get; private set; }
        public string ClassName { get; private set; }

        // Hide constructor
        public ClassMetaInfo(string location, string className, string @namespace)
        {
            Location = location;
            ClassName = className;
            Namespace = @namespace;
        }
    }
}
