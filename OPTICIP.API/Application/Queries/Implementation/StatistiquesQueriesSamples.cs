using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Implementation
{

    public class StatistiquesQueriesSamples : IStatistiquesQueries
    {
        private string _connectionString = string.Empty;
        private readonly IDbConnection _coreDBConnection;
        private readonly IDbConnection _appDBConnection;

        public StatistiquesQueriesSamples(string constr, IDbConnection coreDBConnection, SqlConnection appDBConnection)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
            _coreDBConnection = coreDBConnection ?? throw new ArgumentNullException(nameof(coreDBConnection));
            _appDBConnection = appDBConnection ?? throw new ArgumentNullException(nameof(appDBConnection));
        }

        public async Task<IEnumerable<CompteBancaireViewModel>> GetComptesBancairesDeclares(String dateDebut, String dateFin)
        {
            List<CompteBancaireViewModel> results = new List<CompteBancaireViewModel>();
            CompteBancaireViewModel result = new CompteBancaireViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new CompteBancaireViewModel() { datenv = DateTime.UtcNow, datfrm = DateTime.UtcNow,
                datmaj=DateTime.UtcNow, datouv=DateTime.UtcNow, nom_compte="Test", rib="rib01", type_declaration="01"};

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<CompteMandataireViewModel>> GetComptesMandataires(String dateDebut, String dateFin)
        {
            List<CompteMandataireViewModel> results = new List<CompteMandataireViewModel>();
            CompteMandataireViewModel result = new CompteMandataireViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new CompteMandataireViewModel()
                {
                    datenv = DateTime.UtcNow,
                    datfrm = DateTime.UtcNow,
                    datmaj = DateTime.UtcNow,
                    datouv = DateTime.UtcNow,
                    nom_compte= "compte",
                    rib = "rb",
                    type_declaration = "type"
                };

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<IncidentDePaiementChequeViewModel>> GetIncidentsDePaiementCheques(String dateDebut, String dateFin)
        {

            List<IncidentDePaiementChequeViewModel> results = new List<IncidentDePaiementChequeViewModel>();
            IncidentDePaiementChequeViewModel result = new IncidentDePaiementChequeViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new IncidentDePaiementChequeViewModel()
                {
                    beneficiaire = "benef",
                    datenv = DateTime.UtcNow,
                    date_emis = DateTime.UtcNow,
                    type_declaration = "type declaration",
                    rib = "rib",
                    datmaj = DateTime.UtcNow,
                    date_presentation = DateTime.UtcNow,
                    datinc = DateTime.UtcNow,
                    montant = "100",
                    nocheque = "0001",
                    nom = "nom",
                    type_incident = "type incident"
                };

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<IncidentDePaiementEffetViewModel>> GetIncidentsDePaiementEffets(String dateDebut, String dateFin)
        {
            List<IncidentDePaiementEffetViewModel> results = new List<IncidentDePaiementEffetViewModel>();
            IncidentDePaiementEffetViewModel result = new IncidentDePaiementEffetViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new IncidentDePaiementEffetViewModel() { rib="rib", type_declaration="type declaration",
                nom="nom", motif_rejet="motif", mnt="100", datmaj=DateTime.UtcNow, datech=DateTime.UtcNow,
                datenv=DateTime.UtcNow};

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<DeclarationSusmentionneeViewModel>> GetDeclarationsSusmentionnees(String dateDebut, String dateFin)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IncidentDePaiementRegulariseViewModel>> GetIncidentsDePaiementRegularises(String dateDebut, String dateFin)
        {
            List<IncidentDePaiementRegulariseViewModel> results = new List<IncidentDePaiementRegulariseViewModel>();
            IncidentDePaiementRegulariseViewModel result = new IncidentDePaiementRegulariseViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new IncidentDePaiementRegulariseViewModel()
                {
                    beneficiaire = "benef",
                    date_emis = DateTime.UtcNow,
                    type_declaration = "type declaration",
                    rib = "rib",
                    datreg = DateTime.UtcNow,
                    date_presentation = DateTime.UtcNow,
                    datinc = DateTime.UtcNow,
                    montant = "100",
                    nocheque = "0001",
                    nom = "nom",
                    type_incident = "type incident"
                };

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<CompteEnInterditBancaireViewModel>> GetComptesEnInterditBancaire(String dateDebut, String dateFin)
        {
            List<CompteEnInterditBancaireViewModel> results = new List<CompteEnInterditBancaireViewModel>();
            CompteEnInterditBancaireViewModel result = new CompteEnInterditBancaireViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new CompteEnInterditBancaireViewModel() { beneficiaire="benef",
                datenv = DateTime.UtcNow, date_emis= DateTime.UtcNow, type_declaration="type declaration",
                rib="rib", datmaj=DateTime.UtcNow, date_presentation=DateTime.UtcNow, datinc=DateTime.UtcNow,
                montant="100", nocheque="0001", nom="nom",type_incident="type incident"};

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<CompteEnInterditionLeveeViewModel>> GetComptesEnInterditionLevee(String dateDebut, String dateFin)
        {
            List<CompteEnInterditionLeveeViewModel> results = new List<CompteEnInterditionLeveeViewModel>();
            CompteEnInterditionLeveeViewModel result = new CompteEnInterditionLeveeViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new CompteEnInterditionLeveeViewModel()
                {
                    beneficiaire = "benef",
                    datenv = DateTime.UtcNow,
                    date_emis = DateTime.UtcNow,
                    type_declaration = "type declaration",
                    rib = "rib",
                    datmaj = DateTime.UtcNow,
                    date_presentation = DateTime.UtcNow,
                    datinc = DateTime.UtcNow,
                    montant = "100",
                    nocheque = "0001",
                    nom = "nom",
                    type_incident = "type incident"
                };

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<PersPhysiqueDeclareeViewModel>> GetPersPhysiquesDeclarees(String dateDebut, String dateFin)
        {
            List<PersPhysiqueDeclareeViewModel> results = new List<PersPhysiqueDeclareeViewModel>();
            PersPhysiqueDeclareeViewModel result = new PersPhysiqueDeclareeViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new PersPhysiqueDeclareeViewModel() { datenv=DateTime.UtcNow, datmaj=DateTime.UtcNow,
                datnais=DateTime.UtcNow, nommari="nom mari", nomnais="nom naiss", nom_compte="compte",
                prenom="prenom", rib="rib", type_declaration="type"};

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<PersMoraleDeclareeViewModel>> GetPersMoralesDeclarees(String dateDebut, String dateFin)
        {
            List<PersMoraleDeclareeViewModel> results = new List<PersMoraleDeclareeViewModel>();
            PersMoraleDeclareeViewModel result = new PersMoraleDeclareeViewModel();

            for (int i = 0; i < 50; i++)
            {
                result = new PersMoraleDeclareeViewModel() { type_declaration="type", rib="rib", rcsno="rcsno",
                nom_compte="nom", datenv=DateTime.UtcNow, datmaj=DateTime.UtcNow, forme="forme"};

                result.numseq = "55554";

                results.Add(result);
            }

            return results;
        }
    }
}
