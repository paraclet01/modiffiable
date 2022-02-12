using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CIP2ViewModel
    {
        /*
            numseq    VARCHAR2(21),
            compte    VARCHAR2(11),
            codbnq    VARCHAR2(5),
            codgch    VARCHAR2(5),
            clerib    VARCHAR2(2),
            nomnais   VARCHAR2(32),
            prenom    VARCHAR2(32),
            commnais  VARCHAR2(32),
            datnais   DATE,
            nommari   VARCHAR2(32),
            nommere   VARCHAR2(32),
            iso       VARCHAR2(2),
            sexe      VARCHAR2(1),
            numid     VARCHAR2(16),
            resp      VARCHAR2(1),
            adr       VARCHAR2(50),
            ville     VARCHAR2(32),
            payadr    VARCHAR2(2),
            explmaj   VARCHAR2(3),
            datmaj    DATE,
            type      VARCHAR2(1),
            state     VARCHAR2(1),
            valide    VARCHAR2(1),
            residumoa VARCHAR2(1),
            numlig    NUMBER(5),
            mand      VARCHAR2(1),
            datenv    DATE
            */
        public string numseq { get; set; }
        public string compte { get; set; }
        public string codbnq { get; set; }
        public string codgch { get; set; }
        public string clerib { get; set; }
        public string nomnais { get; set; }
        public string prenom { get; set; }
        public string commnais { get; set; }
        public DateTime datnais { get; set; }
        public string nommari { get; set; }
        public string nommere { get; set; }
        public string iso { get; set; }
        public string sexe { get; set; }
        public string numid { get; set; }
        public string resp { get; set; }
        public string adr { get; set; }
        public string ville { get; set; }
        public string payadr { get; set; }
        public string explmaj { get; set; }
        public DateTime datmaj { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string valide { get; set; }
        public string residumoa { get; set; }
        public Int32 numlig { get; set; }
        public string mand { get; set; }
        public DateTime datenv { get; set; }
        //==> CIP V2
        public string EmailTitu { get; set; }
        public string NomContact { get; set; }
        public string PnomContact { get; set; }
        public string AdrContact { get; set; }
        public string EmailContact { get; set; }

    }
}
