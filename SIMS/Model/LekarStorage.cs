// File:    LekarStorage.cs
// Author:  paracelsus
// Created: Friday, March 26, 2021 4:38:57 PM
// Purpose: Definition of Class LekarStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
   public class LekarStorage
   {
      public bool Create(List<Lekar> lekari)
      {
            var jsonToWrite = JsonConvert.SerializeObject(lekari, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("../../../Data/lekari.json"))
            {
                writer.Write(jsonToWrite);
            }

            return true;
        }
      
      public List<Lekar> Read()
      {
            String json = File.ReadAllText("../../../Data/lekari.json");
            List<Lekar> lekar_all = JsonConvert.DeserializeObject<List<Lekar>>(json);
            return lekar_all;
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