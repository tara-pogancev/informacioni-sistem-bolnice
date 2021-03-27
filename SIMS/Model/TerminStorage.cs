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
            using (StreamWriter writer = new StreamWriter("termini.json"))
            {
                writer.Write(jsonToWrite);
            }


            return true;
        }
      
      public List<Termin> Read()
      {
            String json = File.ReadAllText("termini.json");
            List<Termin> termini_all = JsonConvert.DeserializeObject<List<Termin>>(json);
            return termini_all;
      }
      
      public bool Update()
      {
         throw new NotImplementedException();
      }
      
      public bool Delete()
      {
         throw new NotImplementedException();
      }
   
   }
}