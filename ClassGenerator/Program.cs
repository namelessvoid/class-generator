using System;
using System.IO;

namespace ClassGenerator
{
    class Program
    {
        static int Main(string[] args)
        {
            if(!CheckArgs(args))
            {
            	return 1;
            }

            var classMetaInfo = ClassMetaInfo.FromDefinition(args[0]);

            if(classMetaInfo == null)
            {
            	return 1;
            }

            var classGenerator = new ClassGenerator();
            classGenerator.GenerateClass(classMetaInfo);

            return 0;
        }

        static bool CheckArgs(string[] args)
        {
        	if(args.Length != 1 || args[0].Equals("--help") || args[0].Equals("-h"))
        	{
        		Console.WriteLine(HelpMessage);
        		return false;
        	}

        	return true;
        }

        private static string HelpMessage =
        	"C++ header generate tool\n" +
        	"------------------------\n\n" +
        	"Usage:\n" +
        	$" {System.AppDomain.CurrentDomain.FriendlyName} <location:namespace::classname>\n\n" +
        	"Example:\n" +
        	$" {System.AppDomain.CurrentDomain.FriendlyName} \"game/states:qrw::SkirmishState\""
        	;
	}
}
