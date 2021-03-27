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
        public bool Create(Termin termin)
        {
            //Upis termina u json

            List<Termin> termini = new List<Termin>();
            termini.Add(termin);
            termini.Add(termin);


            String jsonString = System.Text.Json.JsonSerializer.Serialize(termini);

            File.WriteAllText("termini.json", jsonString);

            /*using (StreamWriter outputFile = new StreamWriter("termini.json", true))
            {
                outputFile.WriteLine(jsonString);
            } */


            return true;
        }

        public List<Termin> Read()
        {
            String json = File.ReadAllText("termini.json");
            List<Termin> termini_all = JsonConvert.DeserializeObject<List<Termin>>(json);
            return termini_all;

        }

        public bool Update(Termin termin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Termin termin)
        {
            throw new NotImplementedException();
        }

        public List<Termin> ReadByLekar(Lekar lekar)
        {
            String json = File.ReadAllText("termini.json");
            List<Termin> termini_all = JsonConvert.DeserializeObject<List<Termin>>(json);

            List<Termin> termini = new List<Termin>();

            foreach (Termin t in termini_all)
            {
                if (t.Lekar.Equals(lekar))
                    termini.Add(t);
            }

            return termini;
        }

        public Termin ReadByPacijent(Pacijent pacijent)
        {
            throw new NotImplementedException();

        }

    }
}