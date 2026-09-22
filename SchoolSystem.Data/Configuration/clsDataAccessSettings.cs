using SchoolSystem.Enums;
using System;

using System.Configuration;

namespace SchoolSystem.Data.Configuration
{
    static class clsDataAccessSettings
    {
        public static string ConnectionString
        {
            get
            {
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["SchoolSystemDatabase"];

                if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
                {
                    throw new ConfigurationErrorsException(
                        "The 'SchoolSystemDatabase' connection string is missing from App.config.");
                }

                return settings.ConnectionString;
            }
        }


    }
}
