﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ServicesAPI.Persistence.Settings;
using System.Data;

namespace ServicesAPI.Persistence
{
    public class ServicesContext
    {
        private readonly ContextSettings _settings;
        public ServicesContext(IOptions<ContextSettings> settings)
        {
            _settings = settings.Value;
        }

        public virtual IDbConnection CreateConnection() => new SqlConnection(_settings.RegularConnectionString);
        public virtual IDbConnection CreateMasterConnection() => new SqlConnection(_settings.MasterConnectionString);
    }
}
