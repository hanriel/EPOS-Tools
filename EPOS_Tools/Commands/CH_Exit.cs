using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPOS_Tools.Commands
{
    internal class CH_Exit : ICommandHandler
    {
        public void CommandWorker(string[] args)
        {
            Logger.Success("Exit...GoodBye...");
            System.Environment.Exit(1);
        }
    }
}
