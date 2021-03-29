// File:    SekretarStorage.cs
// Author:  paracelsus
// Created: Friday, March 26, 2021 4:38:56 PM
// Purpose: Definition of Class SekretarStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
   public class SekretarStorage
   {
        public bool Create(List<Sekretar> sekretari)
        {
            var jsonToWrite = JsonConvert.SerializeObject(sekretari, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("../../../Data/sekretari.json"))
            {
                writer.Write(jsonToWrite);
            }
            return true;
        }

        public List<Sekretar> Read()
        {
            List<Sekretar> sekretari;
            try
            {
                String json = File.ReadAllText("../../../Data/sekretari.json");
                sekretari = JsonConvert.DeserializeObject<List<Sekretar>>(json);
            }
            catch (Exception e)
            {
                sekretari = new List<Sekretar>();
            }

            return sekretari;
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