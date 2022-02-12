using System;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class ErreurEnregistrementViewModel
    {
        public Guid Id { get; set; }
        public Guid EnregistrementID { get; set; }
        public string EnregistrementTable { get; set; }
        public string Texte { get; set; }
    }
}
