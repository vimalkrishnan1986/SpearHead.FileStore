﻿using System;
using System.ComponentModel;
using System.Configuration;
namespace SpearHead.FileStore.Common.Helpers
{
    public static class ConfigHelper
    {
        public static T GetConfigValue<T>(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                throw new ArgumentNullException(key);
            }

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(ConfigurationManager.AppSettings[key]);
        }
    }
}
