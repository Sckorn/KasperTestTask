using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using KasperTestTaskServer.Db;

namespace KasperTestTaskServer
{
    public static class Globals
    {
        private const int DEFAULT_CONNECTION_POOL_SIZE = 32;

        private static Object loggerLockObject = new Object();

        private static String logFilePath;

        private static FileServiceDataModel data_model = null;

        public static String LogFilePath
        {
            get
            {
                return logFilePath;
            }

            set
            {
                logFilePath = value;
            }
        }

        public static FileServiceDataModel DbModel
        {
            get
            {
                return data_model;
            }
        }

        public static void InitDataModel(
            String connection_string, 
            String provider_name, 
            int pool_size = DEFAULT_CONNECTION_POOL_SIZE
            )
        {
            Globals.data_model = new FileServiceDataModel(connection_string, provider_name, pool_size);
        }

        public static void CreateFileDir(String FileName)
        {
            var DirName = (new FileInfo(FileName)).DirectoryName;
            if (!Directory.Exists(DirName))
            {
                Directory.CreateDirectory(DirName);
            }
        }

        public static void Trace(String message)
        {
            lock (Globals.loggerLockObject)
            {
                if (!File.Exists(Globals.LogFilePath)) Globals.CreateFileDir(Globals.LogFilePath);

                using (StreamWriter w = File.AppendText(LogFilePath))
                {
                    w.WriteLine("{0} [{1}]: {2}", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ffff"), Thread.CurrentThread.ManagedThreadId, message);
                }
            }
        }
    }
}
