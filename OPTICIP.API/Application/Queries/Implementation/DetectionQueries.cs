using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public class DetectionQueries : IDetectionQueries
    {
        private readonly IDbConnection _dbConnection;
        private readonly IRepositoryFactory _repositoryFactory;

        public DetectionQueries(IDbConnection dbConnection , IRepositoryFactory repositoryFactory)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task LancerDetectionComptes(string userID)
        {
            await Task.Run(() => {

                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_CIP1";

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_Errmsg";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Output;

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "return_value";
                param2.Direction = ParameterDirection.ReturnValue;
                param2.DbType = DbType.String;

                dbCommand.Parameters.Add(param1);
                dbCommand.Parameters.Add(param2);

                dbCommand.ExecuteNonQuery();

                _dbConnection.Close();

                // Logging
                Guid guid = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guid, "Détection des comptes", "", userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });
        }

        public async Task LancerDetectionPersonnesPhysiques(string userID)
        {
            await Task.Run(() => {
                _dbConnection.Open();
                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_CIP2";

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_Errmsg";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Output;

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "return_value";
                param2.Direction = ParameterDirection.ReturnValue;
                param2.DbType = DbType.String;

                dbCommand.Parameters.Add(param1);
                dbCommand.Parameters.Add(param2);

                dbCommand.ExecuteNonQuery();

                _dbConnection.Close();

                // Logging
                Guid guid = Guid.NewGuid();
                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guid, "Détection des personnes physiques", "", userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });
        }

        public async Task LancerDetectionPersonnesMorales(string userID)
        {
            await Task.Run(() => {
                _dbConnection.Open();
                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_CIP3";

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_Errmsg";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Output;

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "return_value";
                param2.Direction = ParameterDirection.ReturnValue;
                param2.DbType = DbType.String;

                dbCommand.Parameters.Add(param1);
                dbCommand.Parameters.Add(param2);

                dbCommand.ExecuteNonQuery();

                _dbConnection.Close();

                // Logging
                Guid guid = Guid.NewGuid();
                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guid, "Détection des personnes morales", "", userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });
        }

        public async Task LancerDetectionCartes(string userID)
        {
            await Task.Run(() => {
            });
        }
       
        public async Task LancerDetectionCheques(string userID)
        {
            await Task.Run(() => {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.CommandText = "PK_CIP.F_CIP5";
                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_Errmsg";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Output;

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "return_value";
                param2.Direction = ParameterDirection.ReturnValue;
                param2.DbType = DbType.String;

                dbCommand.Parameters.Add(param1);
                dbCommand.Parameters.Add(param2);

                dbCommand.ExecuteNonQuery();

                _dbConnection.Close();

                // Logging
                Guid guid = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guid, "Détection des incidents chèques", "", userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });

        }

        public async Task LancerDetectionChequesIrreguliers(string userID)
        {
            await Task.Run(() => {
                _dbConnection.Open();
                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.CommandText = "PK_CIP.F_CIP6";

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_Errmsg";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Output;

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "return_value";
                param2.Direction = ParameterDirection.ReturnValue;
                param2.DbType = DbType.String;

                dbCommand.Parameters.Add(param1);
                dbCommand.Parameters.Add(param2);

                dbCommand.ExecuteNonQuery();

                _dbConnection.Close();

                // Logging
                Guid guid = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guid, "Détection des chèques irréguliers", "", userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });
        }

        public async Task LancerDetectionEffets(string userID)
        {
            await Task.Run(() => {
                _dbConnection.Open();
                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.CommandText = "PK_CIP.F_CIP7";
                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_Errmsg";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Output;

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "return_value";
                param2.Direction = ParameterDirection.ReturnValue;
                param2.DbType = DbType.String;

                dbCommand.Parameters.Add(param1);
                dbCommand.Parameters.Add(param2);

                dbCommand.ExecuteNonQuery();
                _dbConnection.Close();

                // Logging
                Guid guid = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guid, "Détection des incidents effets", "", userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });
        }

        public async Task LancerDetectionEffetsTFJ()
        {
            await Task.Run(() => {
                _dbConnection.Open();
                DateTime debut, fin;
                int resultat;

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;

                // Régularisation automatique des effets
                dbCommand.CommandText = "PK_INCIDAUTO.PB_INCIDEFF_AUTO";
                debut = DateTime.Now;
                resultat =  dbCommand.ExecuteNonQuery();
                fin = DateTime.Now;
                this.HistoriqueLancementProcedure("PK_INCIDAUTO.PB_INCIDEFF_AUTO", debut, fin, resultat);

                _dbConnection.Close();

                // Logging
                Guid guid = Guid.NewGuid();
                TLog log = new TLog(guid, "Détection des incidents effets au traitement comptable", "", guid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();


            });
        }

        public async Task LancerDetectionChequesTFJ()
        {
            await Task.Run(() => {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                DateTime debut, fin;
                int resultat;

                // Régularisation automatique des chèques
                dbCommand.CommandText = "PK_INCIDAUTO.PB_INCIDREGAUTO";
                debut = DateTime.Now;
                resultat = dbCommand.ExecuteNonQuery();
                fin = DateTime.Now;
                this.HistoriqueLancementProcedure("PK_INCIDAUTO.PB_INCIDREGAUTO", debut, fin, resultat);

                // Logging
                Guid guid1 = Guid.NewGuid();
                TLog log1 = new TLog(guid1, "Régularisation automatique des chèques au traitement comptable", "", guid1, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log1);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                // Passage des chèques de l'avertissement à l'injonction
                dbCommand.CommandText = "PK_INCIDAUTO.PB_INCIDUPDINJ";
                debut = DateTime.Now;
                resultat = dbCommand.ExecuteNonQuery();
                fin = DateTime.Now;
                this.HistoriqueLancementProcedure("PK_INCIDAUTO.PB_INCIDUPDINJ", debut, fin, resultat);

                // Logging
                Guid guid2 = Guid.NewGuid();
                TLog log2 = new TLog(guid2, "Passage de l'avertissement à l'injonction au traitement comptable", "", guid2, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log2);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                // Détection des chèques en incident (avertissement, incident simple, interdit bancaire et judiciaire)
                dbCommand.CommandText = "PK_INCIDAUTO.PB_INCIDCHQ_AUTO";
                debut = DateTime.Now;
                resultat = dbCommand.ExecuteNonQuery();
                fin = DateTime.Now;
                this.HistoriqueLancementProcedure("PK_INCIDAUTO.PB_INCIDCHQ_AUTO", debut, fin, resultat);

                // Logging
                Guid guid3 = Guid.NewGuid();
                TLog log3 = new TLog(guid3, "Détection des incidents chèques au traitement comptable", "", guid3, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log3);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                _dbConnection.Close();
            });
        }

        public async Task LancerTraitementCheques()
        {
            await Task.Run(() => {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.CommandText = "PK_INCIDAUTO.PB_INCIDLOAD_CHQEB";
                dbCommand.ExecuteNonQuery();

                _dbConnection.Close();

                // Logging
                Guid guid = Guid.NewGuid();
                TLog log = new TLog(guid, "Chargement des chèques", "", guid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });

        }

        public void HistoriqueLancementProcedure(string p_Nom, DateTime p_DateDebut, DateTime p_DateFin, int p_Resultat)
        {

            THistorique_Lanc_Proc historique_Lanc_Proc = new THistorique_Lanc_Proc { 
                Id = Guid.NewGuid(),
                Nom = p_Nom,
                DateDebut = p_DateDebut,
                DateFin = p_DateFin,
                Resultat = p_Resultat
            };

            _repositoryFactory.Historique_Lanc_ProcRepository.Add(historique_Lanc_Proc);
            _repositoryFactory.Historique_Lanc_ProcRepository.UnitOfWork.SaveEntitiesAsync();

        }

        public async Task<IEnumerable<IncidChqEbViewModel>> GetIncidChqEbAsync()
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<IncidChqEbViewModel> result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(@"select * from v_cip_incidchqeb");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<IncidChqEbViewModel>> GetIncidChqEbAsync(string compte, string cheque, DateTime dateOperation)
        {
            try
            {
                IEnumerable<IncidChqEbViewModel> result;
                _dbConnection.Open();
                string sql, dateOperationString;

                if (!String.IsNullOrWhiteSpace(compte))
                {
                    if (!String.IsNullOrWhiteSpace(cheque))
                    {
                        if (dateOperation != DateTime.MinValue)
                        {
                            dateOperationString = dateOperation.ToString("dd/MM/yy");
                            sql = @"select * from v_cip_incidchqeb where compte = '" + compte + "' and chqref = '" + cheque + "' and datoper = '" + dateOperationString + "'";
                            result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(sql);
                        }
                        else
                        {
                            sql = @"select * from v_cip_incidchqeb where compte = '" + compte + "' and chqref = '" + cheque + "'";
                            result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(sql);
                        }
                    }
                    else
                    {
                        if (dateOperation != DateTime.MinValue)
                        {
                            dateOperationString = dateOperation.ToString("dd/MM/yy");
                            sql = @"select * from v_cip_incidchqeb where compte = '" + compte + "' and datoper = '" + dateOperationString + "'";
                            result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(sql);
                        }
                        else
                        {
                            sql = @"select * from v_cip_incidchqeb where compte = '" + compte + "'";
                            result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(sql);
                        }
                    }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(cheque))
                    {
                        if (dateOperation != DateTime.MinValue)
                        {
                            dateOperationString = dateOperation.ToString("dd/MM/yy");
                            sql = @"select * from v_cip_incidchqeb where chqref = '" + cheque + "' and datoper = '" + dateOperationString + "'";
                            result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(sql);
                        }
                        else
                        {
                            sql = @"select * from v_cip_incidchqeb where chqref = '" + cheque + "'";
                            result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(sql);
                        }
                    }
                    else
                    {
                        if (dateOperation != DateTime.MinValue)
                        {
                            dateOperationString = dateOperation.ToString("dd/MM/yy");
                            sql = @"select * from v_cip_incidchqeb where datoper = '" + dateOperationString + "'";
                            result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(sql);
                        }
                        else
                        {
                            sql = @"select * from v_cip_incidchqeb where compte = '' and chqref = ''";
                            result = await _dbConnection.QueryAsync<IncidChqEbViewModel>(sql);
                        }
                    }
                }


                _dbConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateIncidChqEbAsync(string compte, string cheque, DateTime dateOperation, string benef, DateTime datemi)
        {
            int result = 0;

            try
            {
                await Task.Run(() => {

                    _dbConnection.Open();

                    IDbCommand dbCommand = _dbConnection.CreateCommand();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "PK_INCIDAUTO.F_Upd_Incidchqeb";

                    IDbDataParameter param6 = dbCommand.CreateParameter();
                    param6.ParameterName = "return_value";
                    param6.Direction = ParameterDirection.ReturnValue;
                    param6.DbType = DbType.String;
                    param6.Size = 1;
                    dbCommand.Parameters.Add(param6);

                    IDbDataParameter param1 = dbCommand.CreateParameter();
                    param1.ParameterName = "P_Benef";
                    param1.DbType = DbType.String;
                    param1.Direction = ParameterDirection.Input;
                    param1.Value = benef;
                    dbCommand.Parameters.Add(param1);

                    IDbDataParameter param2 = dbCommand.CreateParameter();
                    param2.ParameterName = "P_Datemis";
                    param2.DbType = DbType.Date;
                    param2.Direction = ParameterDirection.Input;
                    param2.Value = datemi;
                    dbCommand.Parameters.Add(param2);

                    IDbDataParameter param3 = dbCommand.CreateParameter();
                    param3.ParameterName = "P_Cheque";
                    param3.DbType = DbType.String;
                    param3.Direction = ParameterDirection.Input;
                    param3.Value = cheque;
                    dbCommand.Parameters.Add(param3);

                    IDbDataParameter param4 = dbCommand.CreateParameter();
                    param4.ParameterName = "P_Compte";
                    param4.DbType = DbType.String;
                    param4.Direction = ParameterDirection.Input;
                    param4.Value = compte;
                    dbCommand.Parameters.Add(param4);

                    IDbDataParameter param5 = dbCommand.CreateParameter();
                    param5.ParameterName = "P_Datoper";
                    param5.DbType = DbType.Date;
                    param5.Direction = ParameterDirection.Input;
                    param5.Value = dateOperation;
                    dbCommand.Parameters.Add(param5);

                    result = dbCommand.ExecuteNonQuery();
                    _dbConnection.Close();

                    // Logging
                    Guid guid = Guid.NewGuid();
                    string messageLog = "P_Benef:" + benef + ", P_Datemis:" + datemi + " ,P_Cheque:" + cheque + " ,P_Compte:" + compte + " ,P_Datoper:" + dateOperation;
                    TLog log = new TLog(guid, "Mise à jour de chèque", messageLog, guid, DateTime.Now);
                    _repositoryFactory.LogRepository.Add(log);
                    _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return result;
        }

        public async Task<IEnumerable<IncidChqViewModel>> GetIncidChqAsync()
        {
            try
            {
                _dbConnection.Open();
                IEnumerable<IncidChqViewModel> result = await _dbConnection.QueryAsync<IncidChqViewModel>(@"select * from v_cip_incidchq where datreg is null and djustif is null");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<IncidChqViewModel>> GetIncidChqAsync(string compte, string cheque)
        {
            try
            {
                IEnumerable<IncidChqViewModel> result;
                _dbConnection.Open();
                string sql;

                if (!String.IsNullOrWhiteSpace(compte))
                {
                    if (!String.IsNullOrWhiteSpace(cheque))
                    {
                        sql = @"select * from v_cip_incidchq where datreg is null and djustif is null and compte = '" + compte + "' and chqref = '" + cheque + "'";
                        result = await _dbConnection.QueryAsync<IncidChqViewModel>(sql);
                    }
                    else
                    {
                        sql = @"select * from v_cip_incidchq where datreg is null and djustif is null and compte = '" + compte + "'";
                        result = await _dbConnection.QueryAsync<IncidChqViewModel>(sql);
                    }
                }
                else
                {
                    sql = @"select * from v_cip_incidchq where datreg is null and djustif is null and chqref = '" + cheque + "'";
                    result = await _dbConnection.QueryAsync<IncidChqViewModel>(sql);
                }

               _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateIncidChqAsync(string compte, string cheque, DateTime datreg, DateTime djustif, string mreg, string numpen, decimal mtpen)
        {
            int result = 0;

            try
            {
                await Task.Run(() => {

                    _dbConnection.Open();

                    IDbCommand dbCommand = _dbConnection.CreateCommand();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "PK_INCIDAUTO.F_Regul_Incidchq";

                    IDbDataParameter param8 = dbCommand.CreateParameter();
                    param8.ParameterName = "return_value";
                    param8.Direction = ParameterDirection.ReturnValue;
                    param8.DbType = DbType.String;
                    param8.Size = 1;
                    dbCommand.Parameters.Add(param8);

                    IDbDataParameter param1 = dbCommand.CreateParameter();
                    param1.ParameterName = "P_Mreg";
                    param1.DbType = DbType.String;
                    param1.Direction = ParameterDirection.Input;
                    param1.Value = mreg;
                    dbCommand.Parameters.Add(param1);

                    IDbDataParameter param2 = dbCommand.CreateParameter();
                    param2.ParameterName = "P_NumPen";
                    param2.DbType = DbType.String;
                    param2.Direction = ParameterDirection.Input;
                    param2.Value = numpen;
                    dbCommand.Parameters.Add(param2);

                    IDbDataParameter param3 = dbCommand.CreateParameter();
                    param3.ParameterName = "P_MntPen";
                    param3.DbType = DbType.Int32;
                    param3.Direction = ParameterDirection.Input;
                    param3.Value = mtpen;
                    dbCommand.Parameters.Add(param3);

                    IDbDataParameter param4 = dbCommand.CreateParameter();
                    param4.ParameterName = "P_Datreg";
                    param4.DbType = DbType.Date;
                    param4.Direction = ParameterDirection.Input;
                    param4.Value = datreg;
                    dbCommand.Parameters.Add(param4);

                    IDbDataParameter param5 = dbCommand.CreateParameter();
                    param5.ParameterName = "P_Djustif";
                    param5.DbType = DbType.Date;
                    param5.Direction = ParameterDirection.Input;
                    param5.Value = djustif;
                    dbCommand.Parameters.Add(param5);

                    IDbDataParameter param6 = dbCommand.CreateParameter();
                    param6.ParameterName = "P_Cheque";
                    param6.DbType = DbType.String;
                    param6.Direction = ParameterDirection.Input;
                    param6.Value = cheque;
                    dbCommand.Parameters.Add(param6);

                    IDbDataParameter param7 = dbCommand.CreateParameter();
                    param7.ParameterName = "P_Compte";
                    param7.DbType = DbType.String;
                    param7.Direction = ParameterDirection.Input;
                    param7.Value = compte;
                    dbCommand.Parameters.Add(param7);

                    result = dbCommand.ExecuteNonQuery();

                    _dbConnection.Close();

                    if (param8.Value.ToString() == "1")
                    {
                        Guid guid = Guid.NewGuid();
                        string messageLog = "P_Mreg:" + mreg + ", P_NumPen:" + numpen + ", P_MntPen:" + mtpen.ToString() + " ,P_Datreg:" + datreg.ToString() + " ,P_Djustif:" + djustif.ToString() + " ,P_Cheque:" + cheque + " ,P_Compte:" + compte;
                        TLog log = new TLog(guid, "Régularisation de chèque", messageLog, guid, DateTime.Now);
                        _repositoryFactory.LogRepository.Add(log);
                        _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
                    }
                    
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
         
            return result;

        }

        public async Task<IEnumerable<ChqRejViewModel>> ListChequesDetectesTFJ()
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<ChqRejViewModel> result = await _dbConnection.QueryAsync<ChqRejViewModel>(@"select * from v_cip_dchqrej order by datinc desc");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<EffRejViewModel>> ListEffetsDetectesTFJ()
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<EffRejViewModel> result = await _dbConnection.QueryAsync<EffRejViewModel>(@"select * from v_cip_deffrej");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
