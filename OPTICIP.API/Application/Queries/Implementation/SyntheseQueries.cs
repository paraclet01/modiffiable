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
    public class SyntheseQueries : ISyntheseQueries
    {
        private string _connectionString = string.Empty;
        private readonly IDbConnection _coreDBConnection;
        private readonly IDbConnection _appDBConnection;
        private readonly IRepositoryFactory _repositoryFactory;

        public SyntheseQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<CompteViewModel>> GetComptesDeclaresAsync(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql = "";

                    sql = "select * from Compte where etat in ('D', 'R') and Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'";

                    return await connection.QueryAsync<CompteViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<PersPhysiqueViewModel>> GetPersPhysiquesDeclaresAsync(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql = "";

                    sql = "select * from Pers_Physique where etat in ('D', 'R') and Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'";

                    return await connection.QueryAsync<PersPhysiqueViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<PersMoraleViewModel>> GetPersMoralesDeclaresAsync(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql = "";

                    sql = "select * from Pers_Morale where etat in ('D', 'R') and Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'";

                    return await connection.QueryAsync<PersMoraleViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        public async Task<IEnumerable<IncidentsChequesViewModel>> GetIncidentsChequesDeclaresAsync(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql = "";

                    sql = "select * from IncidentCheque where etat in ('D', 'R') and Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'";

                    return await connection.QueryAsync<IncidentsChequesViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<IncidentsEffetsViewModel>> GetIncidentsEffetsDeclaresAsync(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql = "";

                    sql = "select * from IncidentEffet where etat in ('D', 'R') and Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'";

                    return await connection.QueryAsync<IncidentsEffetsViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ChequesIrreguliersViewModel>> GetChequesIrreguliersDeclaresAsync(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql = "";

                    sql = "select * from ChequeIrregulier where etat in ('D', 'R') and Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'";

                    return await connection.QueryAsync<ChequesIrreguliersViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<SyntheseViewModel>> GetSyntheseDeclarationAsync(String dateDebut, String dateFin)
        {
            List<SyntheseViewModel> results = new List<SyntheseViewModel>();
            SyntheseViewModel result;

            try
            {
                // Comptes
                var comptesDeclares = this.GetComptesDeclaresAsync(dateDebut, dateFin);
                result = new SyntheseViewModel();
                result.Type = "Comptes";
                result.Declares = comptesDeclares.Result.Where(c => c.Code == "D1").Count();
                result.DeclaresAcceptes = comptesDeclares.Result.Where(c => c.Code == "D1" && c.Etat == "D").Count();
                result.DeclaresRejetes = comptesDeclares.Result.Where(c => c.Code == "D1" && c.Etat == "R").Count();
                result.Modifies = comptesDeclares.Result.Where(c => c.Code == "M1").Count();
                result.ModifiesAcceptes = comptesDeclares.Result.Where(c => c.Code == "M1" && c.Etat == "D").Count();
                result.ModifiesRejetes = comptesDeclares.Result.Where(c => c.Code == "M1" && c.Etat == "R").Count();
                results.Add(result);

                // Pesonnes physiques
                var persPhysiquesDeclares = this.GetPersPhysiquesDeclaresAsync(dateDebut, dateFin);
                result = new SyntheseViewModel();
                result.Type = "Personnes Physiques";
                result.Declares = persPhysiquesDeclares.Result.Where(c => c.Code == "D2").Count();
                result.DeclaresAcceptes = persPhysiquesDeclares.Result.Where(c => c.Code == "D2" && c.Etat == "D").Count();
                result.DeclaresRejetes = persPhysiquesDeclares.Result.Where(c => c.Code == "D2" && c.Etat == "R").Count();
                result.Modifies = persPhysiquesDeclares.Result.Where(c => c.Code == "M2").Count();
                result.ModifiesAcceptes = persPhysiquesDeclares.Result.Where(c => c.Code == "M2" && c.Etat == "D").Count();
                result.ModifiesRejetes = persPhysiquesDeclares.Result.Where(c => c.Code == "M2" && c.Etat == "R").Count();
                results.Add(result);

                // Pesonnes morales
                var persMoralesDeclares = this.GetPersMoralesDeclaresAsync(dateDebut, dateFin);
                result = new SyntheseViewModel();
                result.Type = "Personnes Morales";
                result.Declares = persMoralesDeclares.Result.Where(c => c.Code == "D3").Count();
                result.DeclaresAcceptes = persMoralesDeclares.Result.Where(c => c.Code == "D3" && c.Etat == "D").Count();
                result.DeclaresRejetes = persMoralesDeclares.Result.Where(c => c.Code == "D3" && c.Etat == "R").Count();
                result.Modifies = persMoralesDeclares.Result.Where(c => c.Code == "M3").Count();
                result.ModifiesAcceptes = persMoralesDeclares.Result.Where(c => c.Code == "M3" && c.Etat == "D").Count();
                result.ModifiesRejetes = persMoralesDeclares.Result.Where(c => c.Code == "M3" && c.Etat == "R").Count();
                results.Add(result);


                // Incidents chèques
                var incidentsChequesDeclares = this.GetIncidentsChequesDeclaresAsync(dateDebut, dateFin);
                result = new SyntheseViewModel();
                result.Type = "Incidents Chèque";
                result.Declares = incidentsChequesDeclares.Result.Where(c => c.Code == "D5").Count();
                result.DeclaresAcceptes = incidentsChequesDeclares.Result.Where(c => c.Code == "D5" && c.Etat == "D").Count();
                result.DeclaresRejetes = incidentsChequesDeclares.Result.Where(c => c.Code == "D5" && c.Etat == "R").Count();
                result.Modifies = incidentsChequesDeclares.Result.Where(c => c.Code == "M5").Count();
                result.ModifiesAcceptes = incidentsChequesDeclares.Result.Where(c => c.Code == "M5" && c.Etat == "D").Count();
                result.ModifiesRejetes = incidentsChequesDeclares.Result.Where(c => c.Code == "M5" && c.Etat == "R").Count();
                results.Add(result);

                // Chèques irréguliers
                var chequesIrreguliersDeclares = this.GetChequesIrreguliersDeclaresAsync(dateDebut, dateFin);
                result = new SyntheseViewModel();
                result.Type = "Chèques Irréguliers";
                result.Declares = chequesIrreguliersDeclares.Result.Where(c => c.Code == "D6").Count();
                result.DeclaresAcceptes = chequesIrreguliersDeclares.Result.Where(c => c.Code == "D6" && c.Etat == "D").Count();
                result.DeclaresRejetes = chequesIrreguliersDeclares.Result.Where(c => c.Code == "D6" && c.Etat == "R").Count();
                result.Modifies = chequesIrreguliersDeclares.Result.Where(c => c.Code == "M6").Count();
                result.ModifiesAcceptes = chequesIrreguliersDeclares.Result.Where(c => c.Code == "M6" && c.Etat == "D").Count();
                result.ModifiesRejetes = chequesIrreguliersDeclares.Result.Where(c => c.Code == "M6" && c.Etat == "R").Count();
                results.Add(result);

                // Chèques irréguliers
                var incidentsEffetsDeclares = this.GetIncidentsEffetsDeclaresAsync(dateDebut, dateFin);
                result = new SyntheseViewModel();
                result.Type = "Incidents Effet";
                result.Declares = incidentsEffetsDeclares.Result.Where(c => c.Code == "D7").Count();
                result.DeclaresAcceptes = incidentsEffetsDeclares.Result.Where(c => c.Code == "D7" && c.Etat == "D").Count();
                result.DeclaresRejetes = incidentsEffetsDeclares.Result.Where(c => c.Code == "D7" && c.Etat == "R").Count();
                result.Modifies = incidentsEffetsDeclares.Result.Where(c => c.Code == "M7").Count();
                result.ModifiesAcceptes = incidentsEffetsDeclares.Result.Where(c => c.Code == "M7" && c.Etat == "D").Count();
                result.ModifiesRejetes = incidentsEffetsDeclares.Result.Where(c => c.Code == "M7" && c.Etat == "R").Count();
                results.Add(result);

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    
}
