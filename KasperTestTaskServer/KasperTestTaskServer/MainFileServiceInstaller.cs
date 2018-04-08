using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceModel;
using System.ServiceProcess;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace KasperTestTaskServer
{
    [RunInstaller(true)]
    public partial class MainFileServiceInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public MainFileServiceInstaller()
        {
            InitializeComponent();
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetAssembly(typeof(MainFileServiceInstaller)).Location);

            service.ServiceName = config.AppSettings.Settings["windowsServiceName"].Value;//System.Configuration.ConfigurationManager.AppSettings["windowsServiceName"].Trim();//"hubCalcService";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
