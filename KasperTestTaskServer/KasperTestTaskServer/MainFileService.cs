using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceModel;

namespace KasperTestTaskServer
{
    public partial class MainFileService : ServiceBase
    {
        private ServiceHost svcHost = null;
        public MainFileService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (svcHost != null)
                {
                    svcHost.Close();
                }

                Globals.LogFilePath = System.Configuration.ConfigurationManager.AppSettings["logFile"];

                svcHost = new ServiceHost(typeof(FileDataService));
                svcHost.Open();

                var serviceName = System.Configuration.ConfigurationManager.AppSettings["serviceName"];

                Globals.InitDataModel(
                    System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString,
                    System.Configuration.ConfigurationManager.ConnectionStrings[0].ProviderName
                    );

                Globals.Trace("Service is up!!!");
            }
            catch (Exception ex)
            {
                Globals.Trace("Error: " + ex.Message + ex.StackTrace);
                return;
            }
        }

        protected override void OnStop()
        {
            if (svcHost != null)
            {
                svcHost.Close();
                svcHost = null;
            }
        }
    }
}
