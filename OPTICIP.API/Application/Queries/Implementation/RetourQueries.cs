using Dapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

    public class Error
    {
        public string NumErr { get; set; }
        public string CodeErr { get; set; }
        public string ChampErr { get; set; }
        public string LibErr { get; set; }
        public Error(string numErr, string codeErr, string libErr, string champErr)
        {
            NumErr = numErr;
            CodeErr = codeErr;
            LibErr = libErr;
            ChampErr = champErr;
        }

    }


    public class RetourQueries : IRetourQueries
    {

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly String _connectionString;
        private readonly String _rootRetourFilesDirectory;
        private ParametresQuerie _parametresQueries;
        private readonly string _prefixErreur = ";motif:";
        private readonly string _prefixNumLigne = "+++ligne";
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
            typesDeDonnees.Add("M1", new TypeDeDonnee { Table = "Compte", NombreDeChamps = 6 });
            if (Parametres != null && Parametres.Libelle == "2")
            {
                typesDeDonnees.Add("D2", new TypeDeDonnee { Table = "Pers_Physique", NombreDeChamps = 25 });
                typesDeDonnees.Add("D3", new TypeDeDonnee { Table = "Pers_Morale", NombreDeChamps = 17 });
                typesDeDonnees.Add("D5", new TypeDeDonnee { Table = "IncidentCheque", NombreDeChamps = 19 });
                typesDeDonnees.Add("D6", new TypeDeDonnee { Table = "ChequeIrregulier", NombreDeChamps = 8 });
                typesDeDonnees.Add("D7", new TypeDeDonnee { Table = "IncidentEffet", NombreDeChamps = 12 });

                typesDeDonnees.Add("M2", new TypeDeDonnee { Table = "Pers_Physique", NombreDeChamps = 25 });
                typesDeDonnees.Add("M3", new TypeDeDonnee { Table = "Pers_Morale", NombreDeChamps = 17 });
                typesDeDonnees.Add("M5", new TypeDeDonnee { Table = "IncidentCheque", NombreDeChamps = 19 });
                typesDeDonnees.Add("M6", new TypeDeDonnee { Table = "ChequeIrregulier", NombreDeChamps = 8 });
                typesDeDonnees.Add("M7", new TypeDeDonnee { Table = "IncidentEffet", NombreDeChamps = 12 });
            }
            else
            {
                typesDeDonnees.Add("D2", new TypeDeDonnee { Table = "Pers_Physique", NombreDeChamps = 20 });
                typesDeDonnees.Add("D3", new TypeDeDonnee { Table = "Pers_Morale", NombreDeChamps = 16 });
                typesDeDonnees.Add("D5", new TypeDeDonnee { Table = "IncidentCheque", NombreDeChamps = 14 });
                typesDeDonnees.Add("D6", new TypeDeDonnee { Table = "ChequeIrregulier", NombreDeChamps = 8 });
                typesDeDonnees.Add("D7", new TypeDeDonnee { Table = "IncidentEffet", NombreDeChamps = 11 });

                typesDeDonnees.Add("M2", new TypeDeDonnee { Table = "Pers_Physique", NombreDeChamps = 20 });
                typesDeDonnees.Add("M3", new TypeDeDonnee { Table = "Pers_Morale", NombreDeChamps = 16 });
                typesDeDonnees.Add("M5", new TypeDeDonnee { Table = "IncidentCheque", NombreDeChamps = 14 });
                typesDeDonnees.Add("M6", new TypeDeDonnee { Table = "ChequeIrregulier", NombreDeChamps = 8 });
                typesDeDonnees.Add("M7", new TypeDeDonnee { Table = "IncidentEffet", NombreDeChamps = 11 });

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
                        if (Parametres.Libelle == "2")
                            this.TraiterLigneErreurV2(ligne, idFichierAller, typesDeDonnees[code].NombreDeChamps, typesDeDonnees[code].Table);
                        else
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

            String identifiant = "";
            String rib = "";
            String numLigneErreur = "";           
            TErreurRetour erreurRetour;
            Guid erreurRetourID;

            if (champs.Count < nbChamps)
                throw new Exception("Format du fichier retour incorrect !");

            identifiant = champs[1];
            rib = champs[2];
            numLigneErreur = champs[nbChamps];

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

        private void TraiterLigneErreurV2(string line, string idFichierAller, Int32 nbChamps, String tableSource)
        {
            
            List<String> champs = line.Split(';').ToList();
            int iIndexPartieErreur = line.IndexOf(_prefixErreur);
            //            Dictionary<String, String> erreurs = new Dictionary<String, String>();
            List<Error> erreurs = new List<Error>();

            TErreurRetour erreurRetour;
            Guid erreurRetourID;

            if (champs.Count < nbChamps)
                throw new Exception("Format du fichier retour incorrect !");

            string identifiant = champs[1];
            string rib = champs[2];

            int idxNum = line.IndexOf(_prefixNumLigne);
            int idxNum2 = line.IndexOf("+++", idxNum+1);
            string numLigneErreur = line.Substring(idxNum + 9, idxNum2 - idxNum - 9);


            try
            {
                //if (!String.IsNullOrWhiteSpace(numLigneErreur))
                //{
                //    for (int i = nbChamps + 1; i < champs.Count - 1; i = i + 2)
                //    {
                //        if (!String.IsNullOrWhiteSpace(champs[i]))
                //            erreurs.Add(champs[i], champs[i + 1]);
                //    }
                //}
                if (iIndexPartieErreur >= 0)
                {
                    string[] errList = line.Substring(iIndexPartieErreur+7).Split(";");
                    for (int i = 0; i < errList.Length; i++)
                    {
                        string errPart = errList[i];
                        string[] partList = errPart.Split("-");
                        //String sLibErr = "";
                        //if (partList != null && partList.Length >= 3)
                        //    sLibErr = $@"{partList[1]}:{partList[2]}";
                        if (partList.Length >= 3)
                            erreurs.Add(new Error(numLigneErreur, partList[0], partList[2], partList[1]));
                    }
                }

                HistoriqueDeclarationsViewModel historiqueDeclaration;

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
                        erreurRetour.Numero_Ligne_Erreur = erreur.NumErr;
                        erreurRetour.Code_Erreur = erreur.CodeErr;
                        erreurRetour.EnregistrementTable = tableSource;
                        erreurRetour.Libelle_Erreur = erreur.LibErr;

                        if (historiqueDeclaration != null)
                        {
                            erreurRetour.EnregistrementID = historiqueDeclaration.RecordID;
                        }

                        erreurRetour.Champ_Erreur = erreur.ChampErr;

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
