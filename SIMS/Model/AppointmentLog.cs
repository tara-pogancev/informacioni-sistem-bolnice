using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class AppointmentLog
    {
        private String terminLogKey;
        private String terminKey;
        private String pacijentKey;
        private DateTime datumPromjene;
        private SurgeryType tipOperacije;
        private bool istekao;

        public AppointmentLog()
        {
            this.istekao = false;
        }
        public AppointmentLog(String terminLogKey,String terminKey,String pacijentKey,DateTime datumPromjene,SurgeryType tipOperacije)
        {
            this.terminLogKey = terminLogKey;
            this.terminKey = terminKey;
            this.pacijentKey = pacijentKey;
            this.datumPromjene = datumPromjene;
            this.tipOperacije = tipOperacije;
            this.istekao = false;
        }

        public string TerminLogKey { get => terminLogKey; set => terminLogKey = value; }
        public string TerminKey { get => terminKey; set => terminKey = value; }
        public string PacijentKey { get => pacijentKey; set => pacijentKey = value; }
        public DateTime DatumPromjene { get => datumPromjene; set => datumPromjene = value; }
        public SurgeryType TipOperacije { get => tipOperacije; set => tipOperacije = value; }
        public bool Istekao { get => istekao; set => istekao = value; }
    }
}
