using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SIMS.Model
{
    public class OperacijaIzvestaj
    {
        public DateTime DatumIzvestaja { get; set; }
        public String OperacijaKey { get; set; }
        public String NazivOperacije { get; set; }
        public String NapomeneOperacije { get; set; }

        public OperacijaIzvestaj()
        {
            DatumIzvestaja = DateTime.Today;
        }

        public OperacijaIzvestaj(Termin termin, String nazivOperacije, String napomeneOperacije)
        {
            Operacija = termin;

            DatumIzvestaja = DateTime.Today;
            NazivOperacije = nazivOperacije;
            NapomeneOperacije = napomeneOperacije;
            OperacijaKey = termin.TerminKey;

        }

        public void InitData()
        {
            Operacija = new TerminStorage().Read(OperacijaKey);
            Operacija.Pacijent = new PacijentStorage().Read(Operacija.Pacijent.Jmbg);
            Operacija.Lekar = new LekarStorage().Read(Operacija.Lekar.Jmbg);

        }

        [JsonIgnore]
        public String ImeLekara
        {
            get { return Operacija.ImeLekara; }
        }

        [JsonIgnore]
        public String ImePacijenta
        {
            get { return Operacija.ImePacijenta; }
        }

        [JsonIgnore]
        public String DatumRodjenjaPacijenta
        {
            get { return Operacija.Pacijent.DatumString; }
        }

        [JsonIgnore]
        public String DatumOperacijeIspis
        {
            get { return Operacija.Datum; }
        }

        [JsonIgnore]
        public String SobaOperacije
        {
            get { return Operacija.NazivProstorije; }
        }

        [JsonIgnore]
        public Termin Operacija { get; set; }

    }
}
