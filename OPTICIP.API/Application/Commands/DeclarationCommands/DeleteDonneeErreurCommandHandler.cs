using Dapper;
using MediatR;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class DeleteDonneeErreurCommandHandler : IRequestHandler<DeleteDonneeErreurCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IDbConnection _coreDBConnection;
        private readonly IDbConnection _appDBConnection;
        private readonly IRepositoryFactory _repositoryFactory;

        public DeleteDonneeErreurCommandHandler(IMediator mediator, IDbConnection coreDBConnection, SqlConnection appDBConnection, IRepositoryFactory repositoryFactory)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _coreDBConnection = coreDBConnection ?? throw new ArgumentNullException(nameof(coreDBConnection));
            _appDBConnection = appDBConnection ?? throw new ArgumentNullException(nameof(appDBConnection));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task<bool> Handle(DeleteDonneeErreurCommand message, CancellationToken cancellationToken)
        {
            bool result = true;
            string originalID  ;
            string enregistrementTable;
            Guid enregistrementID;

            try
            {
                enregistrementID = message.EnregistrementID;
                enregistrementTable = message.EnregistrementTable;

                if (enregistrementTable == "Pers_Morale")
                {
                    PersMoraleViewModel persMorale;

                    _appDBConnection.Open();
                    persMorale = _appDBConnection.QueryAsync<PersMoraleViewModel>(@"select * from V_ListePersMorales where ID=@enregistrementID ", new { enregistrementID }).Result.FirstOrDefault();
                    originalID = persMorale.Num_Enr; 
                    _appDBConnection.Close();

                    _coreDBConnection.Open();

                    IDbCommand dbCommand = _coreDBConnection.CreateCommand();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "PK_CIP.F_Delete_CIP3";

                    IDbDataParameter param2 = dbCommand.CreateParameter();
                    param2.ParameterName = "return_value";
                    param2.Direction = ParameterDirection.ReturnValue;
                    param2.DbType = DbType.String;
                    dbCommand.Parameters.Add(param2);

                    IDbDataParameter param1 = dbCommand.CreateParameter();
                    param1.ParameterName = "P_Numseq";
                    param1.DbType = DbType.String;
                    param1.Direction = ParameterDirection.Input;
                    param1.Value = originalID;
                    dbCommand.Parameters.Add(param1);

                    dbCommand.ExecuteNonQuery();

                    _coreDBConnection.Close();

                    _appDBConnection.Close();
                    string sql = @"delete " + enregistrementTable + " where Num_Enr = '" + originalID + "' ";
                    _appDBConnection.Execute(sql);
                    _appDBConnection.Close();

                    _appDBConnection.Close();
                    sql = @"delete ErreurEnregistrement where EnregistrementID in (select Id from " + enregistrementTable + " where Num_Enr = '" + originalID  + "') and EnregistrementTable = '" + enregistrementTable + "' ";
                    _appDBConnection.Execute(sql);
                    _appDBConnection.Close();
                }

                else if(enregistrementTable == "Pers_Physique")
                {
                    PersPhysiqueViewModel persPhysique;

                    _appDBConnection.Open();
                    persPhysique = _appDBConnection.QueryAsync<PersPhysiqueViewModel>(@"select * from V_ListePersPhysiques where ID=@enregistrementID ", new { enregistrementID }).Result.FirstOrDefault();
                    originalID = persPhysique.Num_Enr;
                    _appDBConnection.Close();

                    _coreDBConnection.Open();

                    IDbCommand dbCommand = _coreDBConnection.CreateCommand();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "PK_CIP.F_Delete_CIP2";

                    IDbDataParameter param2 = dbCommand.CreateParameter();
                    param2.ParameterName = "return_value";
                    param2.Direction = ParameterDirection.ReturnValue;
                    param2.DbType = DbType.String;
                    dbCommand.Parameters.Add(param2);

                    IDbDataParameter param1 = dbCommand.CreateParameter();
                    param1.ParameterName = "P_Numseq";
                    param1.DbType = DbType.String;
                    param1.Direction = ParameterDirection.Input;
                    param1.Value = originalID;
                    dbCommand.Parameters.Add(param1);

                    dbCommand.ExecuteNonQuery();

                    _coreDBConnection.Close();

                    _appDBConnection.Close();
                    string sql = @"delete " + enregistrementTable + " where Num_Enr = '" + originalID + "' ";
                    _appDBConnection.Execute(sql);
                    _appDBConnection.Close();

                    _appDBConnection.Close();
                    sql = @"delete ErreurEnregistrement where EnregistrementID in (select Id from " + enregistrementTable + " where Num_Enr = '" + originalID + "') and EnregistrementTable = '" + enregistrementTable + "' ";
                    _appDBConnection.Execute(sql);
                    _appDBConnection.Close();
                }

                // Logging
                Guid guid = Guid.NewGuid();
                var messageLog = "EnregistrementTable : " + message.EnregistrementTable + ", EnregistrementID : " + message.EnregistrementID;
                TLog log = new TLog(guid, "Suppression d'une donnée en erreur", messageLog, guid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                await _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
