using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPOS_Tools.Commands
{
    internal class CH_Help : ICommandHandler
    {
        public void CommandWorker(string[] args)
        {
            Logger.Log("Справка по приложению:");
            Logger.Log("exit - завершить работу приложения");
            Logger.Log("get <url> - Получить веб страницу ГЕЙ запросом");
            Logger.Log("help - вывести эту справку");
        }
    }
}