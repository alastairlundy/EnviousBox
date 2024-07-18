// See https://aka.ms/new-console-template for more information

using Spectre.Console.Cli;

CommandApp app = new CommandApp();

app.Configure(config =>
{
    
});

return app.Run(args);