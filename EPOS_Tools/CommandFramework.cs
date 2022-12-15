//основной поток приложения
using EPOS_Tools;
using EPOS_Tools.Commands;
using System.Security.Cryptography.X509Certificates;

internal static class CommandFramework
{
    static private Dictionary<string, ICommandHandler> commandList = new Dictionary<string, ICommandHandler>();

    public static void ExecuteCommand(string command, string[] args)
    {
        if (commandList.ContainsKey(command))
        {
            commandList[command].CommandWorker(args);
        }
        else
        {
            Logger.Error("Command not found. Type \"help\" for addtitiona information");
        }

    }

    internal static void Register(string command, ICommandHandler commandHandler)
    {
        commandList.Add(command, commandHandler);
    }
}