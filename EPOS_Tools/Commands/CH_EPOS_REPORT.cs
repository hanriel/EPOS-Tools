using EPOS_Tools.EPOS_REPORT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPOS_Tools.Commands
{
    internal class CH_EPOS_REPORT : ICommandHandler
    {

        EposClient eposClinet = new EposClient();

        public void CommandWorker(string[] args)
        {
            switch (args[1])
            {
                case "test":
                    eposClinet.report("stat", DateTime.Now.ToString("yyyy-MM-dd"));
                    break;
                case "themes":
                    eposClinet.report("themes", DateTime.Now.ToString("yyyy-MM-dd"));
                    break;
                case "homework":
                    eposClinet.report("homework", DateTime.Now.ToString("yyyy-MM-dd"));
                    break;
                case "activity":
                    eposClinet.report("activity", DateTime.Now.ToString("yyyy-MM-dd"));
                    break;
                case "all":
                    eposClinet.report("themes", DateTime.Now.ToString("yyyy-MM-dd"));
                    eposClinet.report("homework", DateTime.Now.ToString("yyyy-MM-dd"));
                    eposClinet.report("activity", DateTime.Now.ToString("yyyy-MM-dd"));
                    break;
                default:
                    Logger.Waring("Invalid argument " + args[1]);
                    break;
            };
            



            
        }
    }
}
