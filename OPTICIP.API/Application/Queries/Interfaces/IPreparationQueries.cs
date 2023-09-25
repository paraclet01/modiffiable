using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IPreparationQueries
    {
        Task<int> LancerPreparationComptes(string userID);
        Task<int[]> LancerPreparationComptes_V2(string userID);
        Task<int> LancerPreparationPersonnesPhysiques(string userID);
        Task<int> LancerPreparationPersonnesMorales(string userID);
        Task<int> LancerPreparationCartes(string userID);
        Task<int> LancerPreparationCheques(string userID);
        Task<int> LancerPreparationChequesIrreguliers(string userID);
//        Task<IEnumerable<TIncidentEffet>> LancerPreparationEffets(string userID);
        Task<int> LancerPreparationEffets(string userID);
        Task<IEnumerable<CIP1ViewModel>> LancerPreparationInitialeComptes();
        Task<IEnumerable<CIP2ViewModel>> LancerPreparationInitialePersonnesPhysiques();
        Task<IEnumerable<CIP3ViewModel>> LancerPreparationInitialePersonnesMorales();
        Task<IEnumerable<CIP5ViewModel>> LancerPreparationInitialeCheques();
        Task<IEnumerable<CIP6ViewModel>> LancerPreparationInitialeChequesIrreguliers();
        Task<IEnumerable<CIP7ViewModel>> LancerPreparationInitialeEffets();

        //Task<IEnumerable<TDonnees_A_Declarer>> LancerPreparationDonnees(string agence);
        Task<IEnumerable<TDonnees_A_Declarer>> LancerPreparationDonnees(string agence, bool bInitialisation = false);

        Task<IEnumerable<CIP1ViewModel>> GetCIP1Async(string state);
        Task<IEnumerable<CIP2ViewModel>> GetCIP2Async(string state);
        Task<IEnumerable<CIP3ViewModel>> GetCIP3Async(string state);
        Task<IEnumerable<CIP4ViewModel>> GetCIP4Async(string state);
        Task<IEnumerable<CIP5ViewModel>> GetCIP5Async(string state);
        Task<IEnumerable<CIP6ViewModel>> GetCIP6Async(string state);
        Task<IEnumerable<CIP7ViewModel>> GetCIP7Async(string state);

        Task<int> CreerLigneDecComptePourModification();
        void InitCompteur();
        Task<int> ControlerCompteADeclarer();
        Task<int> ControlerPersonnePhysiqueADeclarer();
        Task<int> ControlerPersonneMoraleADeclarer();
    }
}
