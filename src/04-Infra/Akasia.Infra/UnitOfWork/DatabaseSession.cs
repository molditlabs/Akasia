using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Akasia.Infra.UnitOfWork
{
    public sealed class DatabaseSession : IDisposable
    {
        public IDbConnection Connection = null;
        public IDbTransaction Transaction = null;

        public DatabaseSession(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
