using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public class ParametresQuerie : IParametresQuerie
    {
        private string _connectionString = string.Empty;

        public ParametresQuerie(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<ParametresViewModel>> GetAllParametresAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<ParametresViewModel> (@"select * from V_ListeParametres");
            }
        }

        public ParametresViewModel GetParametreByCodeAsync(String pCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return  connection.Query<ParametresViewModel>(@"select * from V_ListeParametres where Code = @pCode ", new { pCode }).FirstOrDefault();
            }
        }

        public async Task<ParametresViewModel> GetParametresByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<ParametresViewModel>(@"select * from V_ListeParametres WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<AgencesViewModel>> GetAgencesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //==> Exclure les agences supprimées : Statut = 1
                //return await connection.QueryAsync<AgencesViewModel>(@"select * from V_ListeAgences ");
                return await connection.QueryAsync<AgencesViewModel>(@"select * from V_ListeAgences WHERE Statut = 0 OR Statut IS NULL ORDER BY CodeAgencce");
            }
        }

        public async Task<AgencesViewModel> GetAgenceAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<AgencesViewModel>(@"select * from V_ListeAgences WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }
    }
}
