using Dapper;
using OPTICIP.API;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.BusinessLogicLayer.Utilitaire;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using Logger;
using System.DirectoryServices.AccountManagement;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public class UsersQuerie : IUsersQuerie
    {
        private string _connectionString = string.Empty;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly string _ldapAdminLogin;
        private readonly string _ldapAdminPassword;
        private readonly string _ldapAdminPath;

        public UsersQuerie(string constr, IRepositoryFactory repositoryFactory, string ldapAdminLogin, string ldapAdminPassword, string ldapAdminPath)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            //_ldapAdminLogin = !string.IsNullOrWhiteSpace(ldapAdminLogin) ? ldapAdminLogin : throw new ArgumentNullException(nameof(ldapAdminLogin));
            //_ldapAdminPassword = !string.IsNullOrWhiteSpace(ldapAdminPassword) ? ldapAdminPassword : throw new ArgumentNullException(nameof(ldapAdminPassword));
            _ldapAdminLogin = ldapAdminLogin; // !string.IsNullOrWhiteSpace(ldapAdminLogin) ? ldapAdminLogin : throw new ArgumentNullException(nameof(ldapAdminLogin));
            _ldapAdminPassword = ldapAdminPassword; // !string.IsNullOrWhiteSpace(ldapAdminPassword) ? ldapAdminPassword : throw new ArgumentNullException(nameof(ldapAdminPassword));
            _ldapAdminPath = !string.IsNullOrWhiteSpace(ldapAdminPath) ? ldapAdminPath : throw new ArgumentNullException(nameof(ldapAdminPath));
        }

        public async Task<UsersViewModel> GetUserAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<UsersViewModel>(@"select * from V_ListeUsers WHERE Id=@id", new { id });
                return result.FirstOrDefault();
            }
        }

        //public async Task<UsersViewModel> GetUserAsync(String Login, String MotPasse)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        var PassWord = MotPasse.Crypter();

        //        var result = await connection.QueryAsync<UsersViewModel>(@"select * from V_ListeUsers WHERE login=@Login and motpasse=@MotPasse", new { Login, PassWord });
        //        return result.FirstOrDefault();
        //    }
        //}

        public UsersViewModel GetUser_Old(String Login, String MotPasse)
        {
            UsersViewModel user = new UsersViewModel()
            {
                Login = Login,
                Statut = 99
            };

            //using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "YOURDOMAIN"))
            //{
            //    // validate the credentials
            //    bool isValid = pc.ValidateCredentials("myuser", "mypassword");
            //}

            try
            {
                ApplicationLogger.LogInformation($"Appel de la fonction GetUser: Login = {Login}");
                if (Login.ToLower().Trim() != "admin")
                {

                    DirectoryEntry adminDirectoryEntry = new DirectoryEntry(_ldapAdminPath, _ldapAdminLogin, _ldapAdminPassword);
                    DirectorySearcher adminSearcher = new DirectorySearcher(adminDirectoryEntry);

                    adminSearcher.SearchScope = SearchScope.Subtree;
                    adminSearcher.Filter = "(sAMAccountName=" + Login + ")";

                    ApplicationLogger.LogInformation($"Appel de la fonction GetUser: Login = {Login} - Avant adminSearcher.FindAll()");
                    SearchResultCollection adminSearcherResults = adminSearcher.FindAll();

                    if (!(adminSearcherResults == null))
                    {
                        SearchResult adminSearcherResult = adminSearcherResults[0];

                        String userLDAPPath = adminSearcherResult.Path;
                        DirectoryEntry userDirectoryEntry = new DirectoryEntry(userLDAPPath, Login, MotPasse);
                        DirectorySearcher userSearcher = new DirectorySearcher(userDirectoryEntry);

                        userSearcher.SearchScope = SearchScope.Subtree;
                        userSearcher.Filter = "(sAMAccountName=" + Login + ")";

                        ApplicationLogger.LogInformation($"Appel de la fonction GetUser: Login = {Login} - userSearcher.FindAll()");
                        SearchResultCollection userSearcherResults = userSearcher.FindAll();

                        if (userSearcherResults != null)
                        {
                            using (var connection = new SqlConnection(_connectionString))
                            {
                                user = connection.Query<UsersViewModel>(@"select * from V_ListeUsers WHERE login=@Login", new { Login }).FirstOrDefault();

                                // Logging
                                Guid guid = Guid.NewGuid();
                                TLog log = new TLog(guid, "Connexion", Login, user.Id, DateTime.Now);
                                _repositoryFactory.LogRepository.Add(log);
                                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                                //==> YFS le 10/11/2021: S'assurer que l'utilisateur est créé en BD
                                if (user != null)
                                    return user;
                                else
                                {
                                    UsersViewModel user2 = new UsersViewModel()
                                    {
                                        Login = Login,
                                        Statut = 99,
                                        StatutLibelle = "Utilisateur non habilité dans l'application !"
                                    };
                                    return user2;
                                }

                            }
                        }
                    }
                }
                else
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        user = connection.Query<UsersViewModel>(@"select * from V_ListeUsers WHERE login=@Login", new { Login }).FirstOrDefault();

                        if (user != null && user.MotPasse.Decrypter() == MotPasse)
                        {
                            // Logging
                            Guid guid = Guid.NewGuid();
                            TLog log = new TLog(guid, "Connexion", Login, user.Id, DateTime.Now);
                            _repositoryFactory.LogRepository.Add(log);
                            _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                            return user;
                        }
                        else
                        {
                            UsersViewModel user2 = new UsersViewModel()
                            {
                                Login = Login,
                                Statut = 99,
                                StatutLibelle = "Utilisateur ADMIN inexistant ou mot de passe eronné !"
                            };
                            return user2;

                        }

                    }

                }

                //return null;
                return user;
            }
            catch (Exception ex)
            {
                ApplicationLogger.LogError(ex);

                UsersViewModel user2 = new UsersViewModel()
                {
                    Login = Login,
                    Statut = 99,
                    StatutLibelle = ex.Message
                };
                return user2;
                //throw ex;
            }

        }

        public UsersViewModel GetUser(String Login, String MotPasse)
        {
            UsersViewModel user = new UsersViewModel()
            {
                Login = Login,
                Statut = 99
            };

            try
            {
                ApplicationLogger.LogInformation($"Appel de la fonction GetUser: Login = {Login}");
                if (Login.ToLower().Trim() != "admin")
                {
                    using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, _ldapAdminPath))
                    {
                        // validate the credentials
                        bool isValid = pc.ValidateCredentials(Login, MotPasse);
                        if (isValid)
                        {
                            using (var connection = new SqlConnection(_connectionString))
                            {
                                user = connection.Query<UsersViewModel>(@"select * from V_ListeUsers WHERE login=@Login", new { Login }).FirstOrDefault();

                                // Logging
                                if (user != null)
                                {
                                    Guid guid = Guid.NewGuid();
                                    TLog log = new TLog(guid, "Connexion", Login, user.Id, DateTime.Now);
                                    _repositoryFactory.LogRepository.Add(log);
                                    _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                                    //==> YFS le 10/11/2021: S'assurer que l'utilisateur est créé en BD
                                    return user;
                                }
                                else
                                {
                                    UsersViewModel user2 = new UsersViewModel()
                                    {
                                        Login = Login,
                                        Statut = 99,
                                        StatutLibelle = "Utilisateur non habilité dans l'application !"
                                    };
                                    return user2;
                                }

                            }
                        }
                        else
                        {
                            UsersViewModel user2 = new UsersViewModel()
                            {
                                Login = Login,
                                Statut = 99,
                                StatutLibelle = "Authentification Windows: Login ou mot de passe incorect !"
                            };
                            return user2;
                        }

                    }
                }
                else
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        user = connection.Query<UsersViewModel>(@"select * from V_ListeUsers WHERE login=@Login", new { Login }).FirstOrDefault();

                        if (user != null && user.MotPasse.Decrypter() == MotPasse)
                        {
                            // Logging
                            Guid guid = Guid.NewGuid();
                            TLog log = new TLog(guid, "Connexion", Login, user.Id, DateTime.Now);
                            _repositoryFactory.LogRepository.Add(log);
                            _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                            return user;
                        }
                        else
                        {
                            UsersViewModel user2 = new UsersViewModel()
                            {
                                Login = Login,
                                Statut = 99,
                                StatutLibelle = "Utilisateur ADMIN inexistant ou mot de passe eronné !"
                            };
                            return user2;

                        }

                    }

                }

                //return null;
                return user;
            }
            catch (Exception ex)
            {
                ApplicationLogger.LogError(ex);

                UsersViewModel user2 = new UsersViewModel()
                {
                    Login = Login,
                    Statut = 99,
                    StatutLibelle = ex.Message
                };
                return user2;
                //throw ex;
            }

        }

        //public  UsersViewModel GetUser(String Login, String MotPasse)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        var result = connection.Query<UsersViewModel>(@"select * from V_ListeUsers WHERE login=@Login", new { Login }).FirstOrDefault();

        //        if (result != null && result.MotPasse.Decrypter() == MotPasse)
        //        {
        //            // Logging
        //            Guid guid = Guid.NewGuid();

        //            TLog log = new TLog(guid, "Connexion", Login, guid, DateTime.Now);
        //            _repositoryFactory.LogRepository.Add(log);
        //            _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

        //            return result;
        //        }

        //        return null ;
        //    }
        //}

        public async Task<IEnumerable<UsersViewModel>> GetUsersAsync(int Statut)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<UsersViewModel>(@"select * from V_ListeUsers where Statut=@Statut ", new { Statut });
            }
        }

        private UsersViewModel MapUsersItems(dynamic result)
        {
            var user = new UsersViewModel();

            return user;
        }
    }
}
