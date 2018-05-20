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

            AddClassCommand(app);
            AddDefaultCommand(app);

            Console.WriteLine("This is C++ Class Generator");
            return app.Execute(args);
        }

        static void AddClassCommand(CommandLineApplication app)
        {
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
                
                var headerOnlyOption = command
                    .Option("-h|--header-only", "Only generate header file.", CommandOptionType.NoValue);

                command.OnExecute(() =>
                {
                    var classMetaInfo = new ClassMetaInfo(
                        locationOption.Value(),
                        classNameOption.Value(),
                        namespaceOption.Value()
                    );

                    var filesystem = new Filesystem();
                    var classGenerator = new ClassGenerator(filesystem);

                    var success = classGenerator.GenerateHeader(classMetaInfo);
                    if(success && !headerOnlyOption.HasValue())
                        classGenerator.GenerateSource(classMetaInfo);

                    return 0;
                });
            });
        }

        static void AddDefaultCommand(CommandLineApplication app)
        {
            app.OnExecute(() =>
            {
                Console.WriteLine("Specify a command");
                app.ShowHelp();
                return 1;
            });
        }
    }
}
