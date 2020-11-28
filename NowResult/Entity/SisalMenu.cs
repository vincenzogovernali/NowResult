using System;
using System.Collections.Generic;

namespace NowResult.Sisal
{

    public class SisalMenu
    {
        public int codiceDisciplina { get; set; }
        public int codiceManifestazione { get; set; }

        public string descrizioneDisciplina { get; set; }
        public int numeroAvvenimentiLiveByDisciplina { get; set; }

        public List<Object> scommessaList { get; set; }
    }

}
