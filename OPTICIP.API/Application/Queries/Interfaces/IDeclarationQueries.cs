using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IDeclarationQueries
    {
        Task DeleteAllErrorsDatasAsync(string table, string agence);
        Task<IEnumerable<CompteViewModel>> GetComptesAsync(string agence, string etat = "");
        Task<IEnumerable<PersPhysiqueViewModel>> GetPersPhysiquesAsync(string agence, string etat = "");  
        Task<IEnumerable<PersMoraleViewModel>> GetPersMoralesAsync(string agence, string etat = "");     
        Task<IEnumerable<CartesViewModel>> GetCartesAsync(string agence, string etat = "");
        Task<IEnumerable<IncidentsChequesViewModel>> GetIncidentsChequesAsync(string agence, string etat = "");
        Task<IEnumerable<ChequesIrreguliersViewModel>> GetChequesIrreguliersAsync(string agence, string etat = "");
        Task<IEnumerable<IncidentsEffetsViewModel>> GetIncidentsEffetsAsync(string agence, string etat = "");

        Task<IEnumerable<PersPhysiqueErreurViewModel>> GetPersPhysiquesErreursAsync(string agence);
        Task<IEnumerable<PersMoraleErreurViewModel>> GetPersMoralesErreursAsync(string agence);

        Task<IEnumerable<DeclarationsViewModel>> GetDeclarationsAsync(String Agence, String NbreCompte, bool decInitiale = false);
        IEnumerable<DeclarationsViewModel> GetDeclarationsSync(String Agence, String NbreCompte, bool decInitiale = false);
        Task<FileDeclarationInfoViewModel> GetFileDeclarationInfoAsync();
        Task<FileDeclarationInfoViewModel> PostHistorisationDeclarationInfoAsync(String FileName,String Nombre_Compte_CIP, Guid DeclarePar, String Agences, String ZipFolderName = "");
        Task<IEnumerable<HistorisationDeclarationsViewModel>> GetListHistorisationDeclarationInfoSync();
        Task<IEnumerable<ErreurEnregistrementViewModel>> GetListErreurs(string enregistrementID, string enregistrementTable);
        IEnumerable<HistorisationDeclarationsViewModel> GetListHistorisationDeclarationInfo(String Id);
        IEnumerable<DeclarationsViewModel> GetDetailListHistorisationDeclarationInfo(String Id);
       
        Task<IEnumerable<CompteErreurRetourViewModel>> GetComptesDeclaresAsync(string idFichierAller, string agence, string etat);
        Task<IEnumerable<PersPhysiqueErreurRetourViewModel>> GetPersPhysiquesDeclaresAsync(string idFichierAller, string agence, string etat);
        Task<IEnumerable<PersMoraleErreurRetourViewModel>> GetPersMoralesDeclaresAsync(string idFichierAller, string agence, string etat);
        Task<IEnumerable<IncidentsChequesErreurRetourViewModel>> GetIncidentsChequesDeclaresAsync(string idFichierAller, string agence, string etat);
        Task<IEnumerable<ChequesIrreguliersErreurRetourViewModel>> GetChequesIrreguliersDeclaresAsync(string idFichierAller, string agence, string etat);
        Task<IEnumerable<IncidentsEffetsErreurRetourViewModel>> GetIncidentsEffetsDeclaresAsync(string idFichierAller, string agence, string etat);

        Task<TraitementAllerViewModel> GetResumeTraitementAllerAsync(string idFichierAller);

        Task<IEnumerable<ErreurEnregistrementDeclareViewModel>> GetErreursEnregistrementsDeclaresAsync(string enregistrementID, string enregistrementTable);

        //int GetNbreCompteETC(String Agence);
        int GetNbreCompteETC(String Agence, int bInitialisation);
        Task<int> GetNbreCompteFromSIB();
        Task<IEnumerable<DeclarationsViewModel>> GetDeclarationsInitialesAsync(String NbreCompte, bool decInitiale, int iNumeroFichier);
        Task<int> GetNombreDeLignesADeclarerAsync();
        Task<FileDeclarationInfoViewModel> PostHistorisationDeclarationInitialeInfoAsync(String FileName, String Nbre_Compte_CIP, Guid DeclarePar, String ZipFolderName, int numFichier);

        Task<string> InitialisationDeclaration();
    }
}
