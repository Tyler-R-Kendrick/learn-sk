using Core.Utilities.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Config
{
    public static class AISettingsProvider
    {
        public static AISettings GetSettings()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            return config.GetSection("ApplicationSettings").Get<AISettings>();
        }

    }
}
