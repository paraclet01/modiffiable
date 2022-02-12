using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IStatistiquesQueries
    {
        Task<IEnumerable<CompteBancaireViewModel>> GetComptesBancairesDeclares(String dateDebut, String dateFin);
        Task<IEnumerable<CompteMandataireViewModel>> GetComptesMandataires(String dateDebut, String dateFin);
        Task<IEnumerable<IncidentDePaiementChequeViewModel>> GetIncidentsDePaiementCheques(String dateDebut, String dateFin);
        Task<IEnumerable<IncidentDePaiementEffetViewModel>> GetIncidentsDePaiementEffets(String dateDebut, String dateFin);

        //Task<IEnumerable<DeclarationSusmentionneeViewModel>> GetDeclarationsSusmentionnees(String dateDebut, String dateFin);
        Task<IEnumerable<IncidentDePaiementRegulariseViewModel>> GetIncidentsDePaiementRegularises(String dateDebut, String dateFin);
        Task<IEnumerable<CompteEnInterditBancaireViewModel>> GetComptesEnInterditBancaire(String dateDebut, String dateFin);
        Task<IEnumerable<CompteEnInterditionLeveeViewModel>> GetComptesEnInterditionLevee(String dateDebut, String dateFin);
        Task<IEnumerable<PersPhysiqueDeclareeViewModel>> GetPersPhysiquesDeclarees(String dateDebut, String dateFin);
        Task<IEnumerable<PersMoraleDeclareeViewModel>> GetPersMoralesDeclarees(String dateDebut, String dateFin);
    }
}
