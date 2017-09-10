using System.Configuration;
using System.IO;

namespace GW2LogWatcher.Tests
{
    static class TestUtils
    {
        private static string _testFiles;
        private static TestSettings _settings;

        public static string TestFiles
        {
            get
            {
                if (_testFiles == null)
                {
                    var currentDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());
                    _testFiles = Path.Combine(currentDirectory, "..", "..", "..", "Test Files");
                }
                
                return _testFiles;
            }
        }

        public static TestSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    var configMap = new ExeConfigurationFileMap
                    {
                        ExeConfigFilename = Path.Combine(TestFiles, "test.config")
                    };

                    var configuration = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

                    _settings = new TestSettings
                    {
                        Configuration = configuration
                    };
                }

                return _settings;
            }
        }
    }

    class TestSettings
    {
        public Configuration Configuration { get; set; }

        public string RaidarUsername => Configuration.AppSettings.Settings["RaidarUsername"]?.Value;
        public string RaidarPassword => Configuration.AppSettings.Settings["RaidarPassword"]?.Value;
    }
}
