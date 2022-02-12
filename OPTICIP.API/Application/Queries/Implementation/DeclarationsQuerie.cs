using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public class DeclarationsQuerie : IDeclarationsQuerie
    {
        private string _connectionString = string.Empty;

        public DeclarationsQuerie(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        
        public async Task<IEnumerable<ChequesIrreguliersViewModel>> GetChequesIrregulierAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<ChequesIrreguliersViewModel>(@"select * from V_ListeChequesIrreguliers");
            }
        }


        public async Task<IEnumerable<CompteViewModel>> GetComptesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<CompteViewModel>(@"select * from V_ListeComptes");
            }
        }


        public async Task<IEnumerable<IncidentChequeViewModel>> GetIncidentsChequesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<IncidentChequeViewModel>(@"select * from V_ListeIncidentsCheques");
            }
        }

        public async Task<IEnumerable<IncidentEffetViewModel>> GetIncidentsEffetsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<IncidentEffetViewModel>(@"select * from V_ListeIncidentsEffets");
            }
        }

        public async Task<IEnumerable<Pers_MoraleViewModel>> GetPersonnesMoralesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<Pers_MoraleViewModel>(@"select * from V_ListePersMorales");
            }
        }

        public async Task<IEnumerable<Pers_PhysiqueViewModel>> GetPersonnesPhysiquesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<Pers_PhysiqueViewModel>(@"select * from V_ListePersPhysiques");
            }
        }

        public async Task<IEnumerable<CartesViewModel>> GetCartesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<CartesViewModel>(@"select * from V_ListeCartes ");
            }
        }

        public async Task<IEnumerable<DeclarationsViewModel>> GetDeclarationsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<DeclarationsViewModel>(@"select * from V_Donnees_A_Declarer ");
            }
        }

        public  IEnumerable<DeclarationsViewModel> GetDeclarationsSync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return  connection.Query<DeclarationsViewModel>(@"select * from V_Donnees_A_Declarer ");
            }
        }

        public async Task<FileDeclarationInfoViewModel> GetFileDeclarationInfoAsync()
        {
            FileDeclarationInfoViewModel file = new FileDeclarationInfoViewModel();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<FileDeclarationInfoViewModel>(@"SP_GES_FILENAME", new { DateJour = DateTime.UtcNow.Date }, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
        
        private UsersViewModel MapUsersItems(dynamic result)
        {
            var user = new UsersViewModel();

            return user;
        }
    }
}
