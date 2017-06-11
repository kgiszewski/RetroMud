using System.Configuration;

namespace RetroMud.Core.Config
{
    public class InstanceConfiguration : IInstanceConfiguration
    {
        public string Name => ConfigurationManager.AppSettings[ConfigConstants.InstanceName];
    }
}
