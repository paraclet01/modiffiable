using System;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class AgencesViewModel
    {
        public Guid Id { get; set; }
        public string CodeAgencce { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
    }
}
