using OPTICIP.API.Application.Queries.ViewModels;
using System;
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
        Task<List<String>> GenererLotCertNonPaiements();

        Task<IEnumerable<AttNonPaiementEffetViewModel>> GetAttNonPaiementEffet();
        Task GenererAttNonPaiementEffet();
        Task<List<String>> GenererLotAttNonPaiementEffet();

        Task<IEnumerable<InfraViewModel>> GetChequesEnInfraction();
        Task GenererLettresEnInfraction();
        Task GenererLotLettresEnInfraction();

        Task<IEnumerable<LettreViewModel>> GetLettres(string TypeLettre);
        Task<IEnumerable<LettreLotViewModel>> GetLettresLot(string TypeLettre);

        bool VerifierLettreExiste(string typeLettre, string compte, string cheque);

        Task<IEnumerable<AttPaiementChequesViewModel>> GetAttPaiementCheques();
        Task GenererAttPaiementCheques();
        Task<List<String>> GenererLotAttPaiementCheques();

        Task RecupererDonneesIncidentsFromSIB();

        Task<List<String>> GenererLettreAvertissementFromXcip(String pCompte = null, String pNumChq = null);
        Task<List<String>> GenererLotLettreAvertissementFromXcip();

        Task<List<String>> GenererLettreInjonctionFromXcip(String pCompte = null, String pNumChq = null);
        Task<List<String>> GenererLotLettreInjonctionFromXcip();

        Task<List<String>> GenererLettreInfMandatairesInjFromXcip(String pCompte = null, String pNumChq = null);
        Task<List<String>> GenererLettreInfMandatairesInfFromXcip(String pCompte = null, String pNumChq = null);
        Task<List<String>> GenererLotLettreInfMandatairesInfFromXcip();
        Task<List<String>> GenererLotLettreInfMandatairesInjFromXcip();
        Task<List<String>> GenererLettresEnInfractionFromXcip(String pCompte = null, String pNumChq = null);
        Task<List<String>> GenererLotLettresEnInfractionFromXcip();
    }
}
