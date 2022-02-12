using OPTICIP.API.Application.Queries.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IReportingQueries
    {
        Task<IEnumerable<AvertViewModel>> GetChequesEnAvertissement();
        Task GenererLettreAvertissement();
        Task GenererLotLettreAvertissement();

        Task<IEnumerable<InjViewModel>> GetChequesEnInjonction();
        Task GenererLettreInjonction();
        Task GenererLotLettreInjonction();

        Task<IEnumerable<InfMandViewModel>> GetMandatairesDesChequesEnInjonction();
        Task<IEnumerable<InfMandViewModel>> GetMandatairesDesChequesEnInfraction();
        Task GenererLettreInfMandatairesInj();
        Task GenererLettreInfMandatairesInf();
        Task GenererLotLettreInfMandatairesInf();
        Task GenererLotLettreInfMandatairesInj();

        Task<IEnumerable<CertNonPaiementViewModel>> GetDonneesCertNonPaiement();
        Task GenererCertNonPaiements();
        Task GenererLotCertNonPaiements();

        Task<IEnumerable<AttNonPaiementEffetViewModel>> GetAttNonPaiementEffet();
        Task GenererAttNonPaiementEffet();
        Task GenererLotAttNonPaiementEffet();

        Task<IEnumerable<InfraViewModel>> GetChequesEnInfraction();
        Task GenererLettresEnInfraction();
        Task GenererLotLettresEnInfraction();

        Task<IEnumerable<LettreViewModel>> GetLettres(string TypeLettre);
        Task<IEnumerable<LettreLotViewModel>> GetLettresLot(string TypeLettre);

        Task<IEnumerable<AttPaiementChequesViewModel>> GetAttPaiementCheques();
        Task GenererAttPaiementCheques();
        Task GenererLotAttPaiementCheques();



    }
}
