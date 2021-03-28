// File:    TerminStorage.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:57 PM
// Purpose: Definition of Class TerminStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
   public class TerminStorage
   {
      public bool Create(List<Termin> termini)
      {
            var jsonToWrite = JsonConvert.SerializeObject(termini, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("../../../termini.json"))
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Model
{
    public class TerminStorage
    {
        public bool Create(List<Termin> termini)
        {
            var jsonToWrite = JsonConvert.SerializeObject(termini, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("termini.json"))
            {
                writer.Write(jsonToWrite);
            }


            return true;
        }
      
      public List<Termin> Read(Pacijent p)
      {
            String json = File.ReadAllText("../../../termini.json");
            List<Termin> termini_all = JsonConvert.DeserializeObject<List<Termin>>(json);
            for (int i = 0; i < termini_all.Count; i++)
            {
                if (!termini_all[i].Pacijent.Jmbg.Equals(p.Jmbg))
                {
                    termini_all.RemoveAt(i);
                    i--;
                }
            }
            return termini_all;
      }
      
      public bool Update(List<Termin> noviTermini,Pacijent p)
      {
            String json = File.ReadAllText("../../../termini.json");
            List<Termin> termini_all = JsonConvert.DeserializeObject<List<Termin>>(json);
            if (noviTermini.Count == 0)
            {
                for (int i = 0; i < termini_all.Count; i++)
                {
                    if (termini_all[i].Pacijent.Jmbg.Equals(p.Jmbg))
                    {
                        termini_all.RemoveAt(i);
                        i--;
                    }
                }

            }
            else
            {
                
                for (int i = 0; i < termini_all.Count; i++)
                {
                    if (termini_all[i].Pacijent.Jmbg.Equals(noviTermini[0].Pacijent.Jmbg))
                    {
                        termini_all.RemoveAt(i);
                        i--;
                    }
                }
                termini_all.AddRange(noviTermini);
               
            }
            Create(termini_all);
            return true;
      }
      
      public bool Delete()
      {
         throw new NotImplementedException();
      }
   
   }
            return true;
        }

        public List<Termin> Read()
        {
            //Metoda koja ucitava sve podatke iz fajla u listu

            String json = File.ReadAllText("termini.json");
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