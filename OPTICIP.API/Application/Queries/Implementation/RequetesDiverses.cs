using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace OPTICIP.API.Application.Queries.Implementation
{
    public class RequetesDiverses:IRequetesDiverses
    {
        private string _connectionString = string.Empty;

        public RequetesDiverses(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<bool> TruncateTable(String pTableName)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        connection.Execute(@$"TRUNCATE TABLE {pTableName}");
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public T GetScalarDataByQuery<T>(String pQuery)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var ret = connection.ExecuteScalar<T>(pQuery);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
