// File:    PacijentStorage.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:17:45 PM
// Purpose: Definition of Class PacijentStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
   public class PacijentStorage
   {
      public bool Create(List<Pacijent> pacijenti)
      {
            var jsonToWrite = JsonConvert.SerializeObject(pacijenti, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("../../../Data/pacijenti.json"))
            {
                writer.Write(jsonToWrite);
            }
            return true;
        }
      
      public List<Pacijent> Read()
      {
            List<Pacijent> pacijenti;
            try 
            { 
                String json = File.ReadAllText("../../../Data/pacijenti.json");
                pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            }
            catch (Exception e)
            {
                pacijenti = new List<Pacijent>();
            }
            
            return pacijenti;
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