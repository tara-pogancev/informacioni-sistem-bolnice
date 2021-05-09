using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Model
{
    public class TerminStorage : Storage<string, Termin, TerminStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\termini.json";
        }

        public List<Termin> ReadByPatient(Pacijent pacijent)
        {
            List<Termin> termini = new List<Termin>();

            foreach (Termin t in this.ReadList())
            {
                if (istiJmbg(t.Pacijent,pacijent))
                    termini.Add(t);
            }

            return termini;
        }

        private bool istiJmbg(UlogovanKorisnik korisnik1,UlogovanKorisnik korisnik2)
        {
            return korisnik1.Jmbg == korisnik2.Jmbg;
        }

        public List<Termin> ReadByDoctor(Lekar lekar)
        {
            List<Termin> termini = new List<Termin>();

            foreach(Termin t in this.ReadList())
            {
                if (istiJmbg(t.Lekar,lekar))
                    termini.Add(t);
            }

            return termini;
        }

        protected override string getKey(Termin entity)
        {
            return entity.TerminKey;
        }

        protected override void RemoveReferences(string key)
        {
            //Metoda jos uvek nije neophodna za klasu TerminStorage
            return;
        }

       

        public int getAppointmentsCountByDate(DateTime date, TipTermina tip, Lekar l)
        {
            List<Termin> retVal = new List<Termin>();

            foreach (Termin t in TerminStorage.Instance.ReadByDoctor(l))
            {
                DateTime day = t.PocetnoVreme.Date;
                if (t.VrstaTermina == tip && day == date)
                    retVal.Add(t);
            }

            return retVal.Count;
        }

        public List<int> GetAppointmentsCountForCurrentWeek(TipTermina tip, Lekar l)
        {
            List<int> retVal = new List<int>();
            DateTime startOfWeek = GetStartOfWeek();

            for (int i = 0; i < 7; i++)
            {
                DateTime dayOfWeek = startOfWeek.AddDays(i);
                retVal.Add(getAppointmentsCountByDate(dayOfWeek, tip, l));
            }

            return retVal;
        }

        private static DateTime GetStartOfWeek()
        {
            return DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek) + 1);
        }
    }

}      