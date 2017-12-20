using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;

namespace NuGetReferenceSwitcher.Configuration
{
    public class ConfigManager
    {
        private const string configName = "config.NuGetReferenceSwitcher";

        public ConfigManager(DTE application)
        {
            this.Application = application;
        }

        /// <summary>Gets or sets the Visual Studio application object. </summary>
        public DTE Application { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Config SwitchConfig { get; private set; }

        /// <summary>Load configuration file if it exists</summary>
        public ConfigManager Load(n)
        {
            var solutionPath = Application.roo

            //Config config = Config.LoadFromPath()
            return this;
        }
    }
}
