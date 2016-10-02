using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.ViewModels
{
    sealed class UserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string Location
        {
            get
            {
                return (string)this["Location"];
            }
            set
            {
                this["Location"] = value;
            }
        }
    }
}
