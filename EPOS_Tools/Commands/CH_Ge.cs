using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EPOS_Tools.Commands
{
    internal class CH_Ge : ICommandHandler
    {
        public readonly static CookieContainer _cookieContainer;

        public void CommandWorker(string[] args)
        {
            if (args.Length > 1)
            {
                try
                {
                    Logger.Log(EPOS_Tools.HTTP_Worker.Get(args[1], _cookieContainer));
                } catch (Exception e)
                {
                    Logger.Error(e.StackTrace);
                } 
            }
            
        }
    }
}
