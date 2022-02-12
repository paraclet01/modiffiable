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
        Task<IEnumerable<TCompte>> LancerPreparationComptes(string userID);
        Task<IEnumerable<TPersPhysique>> LancerPreparationPersonnesPhysiques(string userID);
        Task<IEnumerable<TPersMorale>> LancerPreparationPersonnesMorales(string userID);
        Task<IEnumerable<TCarte>> LancerPreparationCartes(string userID);
        Task<IEnumerable<TIncidentCheque>> LancerPreparationCheques(string userID);
        Task<IEnumerable<TChequeIrregulier>> LancerPreparationChequesIrreguliers(string userID);
        Task<IEnumerable<TIncidentEffet>> LancerPreparationEffets(string userID);
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
    }
}
