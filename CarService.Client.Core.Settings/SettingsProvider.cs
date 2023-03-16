using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarService.Client.Core.Settings
{
    public class SettingsProvider<TUserSettings> : ISettingsProvider where TUserSettings : class, new()
    {
        private readonly string settingsFilename;
        private XmlSerializer Serializer => new XmlSerializer(typeof(TUserSettings));
        private TUserSettings? settings;
        private object lockObj = new object();

        public SettingsProvider(string settingsFilename)
        {
            this.settingsFilename = settingsFilename;
        }

        public TUserSettings GetSettings()
        {
            lock (lockObj)
            {
                if (settings != null) return settings;

                if (!File.Exists(GetSettingsFilename()))
                {
                    return settings = new TUserSettings();
                }
                else
                {
                    using (FileStream fs = new FileStream(GetSettingsFilename(), FileMode.Open))
                    {
                        settings = Serializer.Deserialize(fs) as TUserSettings;
                        if (settings == null) throw new InvalidOperationException("Could not read settings file.");

                        return settings;
                    }
                }
            }
        }

        public void SaveSettings()
        {
            using (TextWriter writer = new StreamWriter(GetSettingsFilename()))
            {
                Serializer.Serialize(writer, settings);
                writer.Close();
            }
        }

        private string GetSettingsFilename()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            string settingsFilePath = Path.Combine(directory, settingsFilename + ".xml");
            return settingsFilePath;
        }
    }

    public interface ISettingsProvider
    {

    }
}
