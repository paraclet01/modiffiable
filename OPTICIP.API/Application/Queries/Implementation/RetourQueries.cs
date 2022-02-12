using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public class TypeDeDonnee
    {
        public String Table { get; set; }
        public Int32 NombreDeChamps { get; set; }
    }

    public class RetourQueries : IRetourQueries
    {

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly String _connectionString;
        private readonly String _rootRetourFilesDirectory;
        private ParametresQuerie _parametresQueries;
        public string RootRetourFilesDirectory
        {
            get {

                if (!Directory.Exists(_rootRetourFilesDirectory))
                    Directory.CreateDirectory(_rootRetourFilesDirectory);

                return _rootRetourFilesDirectory;
            }
        }
       
        public RetourQueries(IRepositoryFactory repositoryFactory, String connectionString, String rootRetourFilesDirectory)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _rootRetourFilesDirectory = rootRetourFilesDirectory ?? throw new ArgumentNullException(nameof(rootRetourFilesDirectory));

            _parametresQueries = new ParametresQuerie(_connectionString);
        }

        public void IntegrerFichierRetour(string nomFichierRetour, string idFichierAller)
        {
            string ligne, code;
            Dictionary<String, TypeDeDonnee> typesDeDonnees = new Dictionary<String, TypeDeDonnee>();

            var Parametres = _parametresQueries.GetParametreByCodeAsync("VERSION_CIP");

            typesDeDonnees.Add("D1", new TypeDeDonnee { Table = "Compte", NombreDeChamps = 6 });
            if (Parametres != null && Parametres.Libelle == "2")
            {
                typesDeDonnees.Add("D2", new TypeDeDonnee { Table = "Pers_Physique", NombreDeChamps = 25 });
                typesDeDonnees.Add("D3", new TypeDeDonnee { Table = "Pers_Morale", NombreDeChamps = 17 });
                typesDeDonnees.Add("D5", new TypeDeDonnee { Table = "IncidentCheque", NombreDeChamps = 19 });
                typesDeDonnees.Add("D6", new TypeDeDonnee { Table = "ChequeIrregulier", NombreDeChamps = 8 });
                typesDeDonnees.Add("D7", new TypeDeDonnee { Table = "IncidentEffet", NombreDeChamps = 12 });
            }
            else
            {
                typesDeDonnees.Add("D2", new TypeDeDonnee { Table = "Pers_Physique", NombreDeChamps = 20 });
                typesDeDonnees.Add("D3", new TypeDeDonnee { Table = "Pers_Morale", NombreDeChamps = 16 });
                typesDeDonnees.Add("D5", new TypeDeDonnee { Table = "IncidentCheque", NombreDeChamps = 14 });
                typesDeDonnees.Add("D6", new TypeDeDonnee { Table = "ChequeIrregulier", NombreDeChamps = 8 });
                typesDeDonnees.Add("D7", new TypeDeDonnee { Table = "IncidentEffet", NombreDeChamps = 11 });

            }
            StreamReader file = null;
            try
            {
                file = new StreamReader(_rootRetourFilesDirectory + nomFichierRetour);

                while ((ligne = file.ReadLine()) != null)
                {
                    code = ligne.Substring(0, 2);

                    if (typesDeDonnees.ContainsKey(code))
                    {
                        this.TraiterLigneErreur(ligne, idFichierAller, typesDeDonnees[code].NombreDeChamps, typesDeDonnees[code].Table);
                    }                
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = @"update DeclarationFichier set Traitement_Retour = '1' where Id = '" + idFichierAller + "'";
                    connection.Execute(sql);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            finally
            {
                if (file != null)
                    file.Dispose();
            }
        }

        private void TraiterLigneErreur(string line, string idFichierAller, Int32 nbChamps, String tableSource)
        {
            List<String> champs = line.Split(';').ToList();
            Dictionary<String, String> erreurs = new Dictionary<String, String>();

            String identifiant = champs[1];
            String rib = champs[2];
            String numLigneErreur = champs[nbChamps];           
            TErreurRetour erreurRetour;
            Guid erreurRetourID;

            try
            {
                if (!String.IsNullOrWhiteSpace(numLigneErreur))
                {
                    for (int i = nbChamps + 1; i < champs.Count - 1; i = i + 2)
                    {
                        if (!String.IsNullOrWhiteSpace(champs[i])) 
                            erreurs.Add(champs[i], champs[i + 1]);
                    }
                }

                HistoriqueDeclarationsViewModel historiqueDeclaration ;

                if (erreurs.Count != 0)
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        try
                        {
                            connection.Open();

                            String sql = @"select * from Historique_Declarations where Id_Declaration = '" + idFichierAller + "' "
                                       + @"and TableSource = '" + tableSource + "' and Text like '%" + identifiant + "%' and Text like '%" + rib + "%'";

                            historiqueDeclaration = connection.QueryAsync<HistoriqueDeclarationsViewModel>(sql).Result.FirstOrDefault();

                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                    // Enregistrement des erreurs dans la table erreurretour
                    foreach (var erreur in erreurs)
                    {
                        erreurRetour = new TErreurRetour();
                        erreurRetourID = Guid.NewGuid();

                        erreurRetour.Id = erreurRetourID;
                        erreurRetour.Numero_Ligne_Erreur = numLigneErreur;
                        erreurRetour.Code_Erreur = erreur.Value;
                        erreurRetour.EnregistrementTable = tableSource;

                        if (historiqueDeclaration != null)
                        {
                            erreurRetour.EnregistrementID = historiqueDeclaration.RecordID;
                        }

                        erreurRetour.Champ_Erreur = erreur.Key;

                        _repositoryFactory.ErreurRetourRepository.Add(erreurRetour);
                    }

                    //Mise à jour du statut de l'enregistrement dans la table source
                    if (historiqueDeclaration != null)
                    {
                        int result;

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            try
                            {
                                connection.Open();

                                string sql = @"update " + tableSource + " set Etat = 'R', Num_Ligne_Erreur = '" + numLigneErreur + "' where Id = '" + historiqueDeclaration.RecordID + "'";
                                result = connection.Execute(sql);

                                connection.Close();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    _repositoryFactory.ErreurRetourRepository.UnitOfWork.SaveEntitiesAsync();
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }
    }
}
