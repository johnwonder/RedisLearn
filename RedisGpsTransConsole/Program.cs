using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using System.Reflection;
using System.IO;
using System.Threading;

namespace RedisGpsTransConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            string configFilePath = assemblyDirPath + "\\log4net.config";
            XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));

            TransportLoc trans = new TransportLoc();
            //trans.QueryRedis("苏D32969");
            //trans.listBoxMsgAdd = new TransportLoc.EventHandler2(listBoxMsgAdd);
            Thread thread = new Thread(new ThreadStart(trans.StartParall));
            thread.Start();

            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            
        }

        static void listBoxMsgAdd(string msg)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t\t" + msg);
        }
    }
}
