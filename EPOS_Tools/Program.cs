using EPOS_Tools;
using EPOS_Tools.Commands;
using System.Collections.Specialized;
using System.Configuration;

internal class Program
{

    private static void Main(string[] args)
    {
        Start();
    }

    static void Start()
    {
        Console.WriteLine("Starting EPOS TOOLS v0.1");

        CommandFramework.Register("exit", new CH_Exit());
        CommandFramework.Register("help", new CH_Help());
        CommandFramework.Register("reload", new CH_Reload());
        CommandFramework.Register("get", new CH_Ge());
        CommandFramework.Register("report", new CH_EPOS_REPORT());

        LoadConfigs();
        Run();
    }

    static void LoadConfigs()
    {
        string sAttr;
        NameValueCollection sAll;
        sAll = ConfigurationManager.AppSettings;

        Logger.Log(sAll.Get(0));
    }

    static void Run()
    {
        do
        {
            Console.Write("> ", ConsoleColor.Green);
            string input = Console.ReadLine();
            string[] args = input.Split(' ');
            CommandFramework.ExecuteCommand(args[0], args);
        } while (true);
    }
}