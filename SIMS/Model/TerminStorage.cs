// File:    TerminStorage.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:57 PM
// Purpose: Definition of Class TerminStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Model
{
    public class TerminStorage
    {
        private List<Termin> terminStorage = new List<Termin>();

        public TerminStorage()
        {
            terminStorage = this.Read();
        }

        public bool Create(List<Termin> termini)
        {
            var jsonToWrite = JsonConvert.SerializeObject(termini, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("termini.json"))
            {
                writer.Write(jsonToWrite);
            }

            return true;
        }

        public Termin Read(Termin termin)
        {
            foreach(Termin t in this.terminStorage)
            {
                if (t.Lekar.Equals(termin.Lekar) &&
                    t.Pacijent.Equals(termin.Lekar) &&
                    t.PocetnoVreme.Equals(termin.PocetnoVreme))

                    return t;
            }

            return null;
        }

        public List<Termin> Read()
        {
            //Metoda koja ucitava sve podatke iz fajla u listu

            String json = File.ReadAllText("termini.json");
            List<Termin> termini_all = JsonConvert.DeserializeObject<List<Termin>>(json);

            this.terminStorage.Clear();

            foreach (Termin t in termini_all)
            {
                this.terminStorage.Add(t);
            }

            return termini_all;
        }

        public bool Update(Termin termin, Termin terminNew)
        {
            return true;
        }

        public bool Delete(Termin termin)
        {
            foreach (Termin t in this.terminStorage)
            {
                if (t.Lekar.Jmbg == termin.Lekar.Jmbg &&
                    t.Pacijent.Equals(termin.Pacijent) &&
                    t.PocetnoVreme.Equals(termin.PocetnoVreme))

                    this.terminStorage.Remove(t);
                    return true;
            }

            return false;
        }

        public List<Termin> ReadByLekar(Lekar lekar)
        {
            List<Termin> termini = new List<Termin>();

            foreach (Termin t in terminStorage)
            {
                if (t.Lekar.Equals(lekar))
                    termini.Add(t);
            }

            return termini;
        }

        public List<Termin> ReadByPacijent(Pacijent pacijent)
        {
            List<Termin> termini = new List<Termin>();

            foreach (Termin t in terminStorage)
            {
                if (t.Lekar.Equals(pacijent))
                    termini.Add(t);
            }

            return termini;
        }

        public List<Termin> getTerminStorage()
        {
            return this.terminStorage;
        }

        public void AddTermin(Termin termin)
        {
            this.terminStorage.Add(termin);
        }


    }
}