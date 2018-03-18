using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace ClassGenerator
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "classgen";
            app.HelpOption("-?|-h|--help");

            app.Command("class", (command) =>
            {
                command.Description = "Generate a full class, i.e. header and source file.";
                command.HelpOption("-?|-h|--help");

                var locationOption = command
                    .Option("-l|--location", "The folder in which the class is put, NOT prepended with 'include/' or 'src/'.", CommandOptionType.SingleValue)
                    .IsRequired();

                var namespaceOption = command
                    .Option("-n|--namespace", "The namespace of the class.", CommandOptionType.SingleValue)
                    .IsRequired();

                var classNameOption = command
                    .Option("-c|--classname", "The name of the class.", CommandOptionType.SingleValue)
                    .IsRequired();

                command.OnExecute(() =>
                {
                    var classMetaInfo = new ClassMetaInfo(
                        locationOption.Value(),
                        classNameOption.Value(),
                        namespaceOption.Value()
                    );

                    var filesystem = new Filesystem();
                    var classGenerator = new ClassGenerator(filesystem);
                    classGenerator.GenerateClass(classMetaInfo);

                    return 0;
                });
            });

            app.OnExecute(() =>
            {
                Console.WriteLine("Specify a command");
                app.ShowHelp();
                return 1;
            });

            Console.WriteLine("This is C++ Class Generator");
            return app.Execute(args);
        }
    }
}
