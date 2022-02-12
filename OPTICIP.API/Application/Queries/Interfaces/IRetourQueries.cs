using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IRetourQueries
    {
        void IntegrerFichierRetour(string nomFichierRetour, string idFichierAller);
         string RootRetourFilesDirectory {  get; }
    }
}
