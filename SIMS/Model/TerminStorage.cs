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
        public bool Create(List<Termin> termini)
        {
            var jsonToWrite = JsonConvert.SerializeObject(termini, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("../../../data/termini.json"))
            {
                writer.Write(jsonToWrite);
            }

            return true;
        }

        public List<Termin> Read()
        {
            //Metoda koja ucitava sve podatke iz fajla u listu

            String json = File.ReadAllText("../../../data/termini.json");
            List<Termin> termini_all = JsonConvert.DeserializeObject<List<Termin>>(json);

            return termini_all;
        }

        public bool Update(Termin termin, Termin terminNew)
        {
            //TODO
            return true;
        }

        public bool Delete(Termin termin)
        {
            //TODO
            return true;
        }

    }
}