using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;

namespace NuGetReferenceSwitcher.Configuration
{
    public class ConfigManager
    {
        /// <summary>Default config file name</summary>
        private const string configName = "config.NuGetReferenceSwitcher";

        public ConfigManager(DTE application)
        {
            this.Application = application;
        }

        /// <summary>Gets or sets the Visual Studio application object. </summary>
        public DTE Application { get; set; }

        /// <summary></summary>
        public string NugetFilter
        {
            get
            {
                if (switchConfig != null)
                {
                    return switchConfig.nugetFilter;
                }
                return "";
            }
        }

        /// <summary>Config object</summary>
        public Config switchConfig { get; private set; }

        /// <summary>Returns root path of current solution</summary>
        public string solutionPath
        {
            get
            {
                var solutionFile = Application.Solution.FullName;
                var solutionPath = Path.GetDirectoryName(solutionFile);
                return solutionPath;
            }
        }

        /// <summary>Load configuration</summary>
        public ConfigManager Load()
        {
            switchConfig = LoadFromFile(
                    Path.Combine(solutionPath, configName)
                );
            return this;
        }

        /// <summary>Load configuration file from file</summary>
        private Config LoadFromFile(string path)
        {
            Config config = new Config();
            if (File.Exists(path))
            {
                using (var file = File.OpenText(path))
                using (var reader = new JsonTextReader(file))
                {
                    var serializer = JsonSerializer.Create();
                    config = (Config)serializer.Deserialize(reader, typeof(Config));
                }
            }
            return config;
        }

        public ConfigManager Save()
        {
            SaveToFile(
                Path.Combine(solutionPath, configName),
                switchConfig
                );
            return this;
        }

        private void SaveToFile(string path, Config config)
        {
            using (var file = File.CreateText(path))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
                serializer.Serialize(file, config);
            }
        }
    }
}
