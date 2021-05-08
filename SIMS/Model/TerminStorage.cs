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

       

         


    }

}      