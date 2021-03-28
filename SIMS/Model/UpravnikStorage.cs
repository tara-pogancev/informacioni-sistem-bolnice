// File:    UpravnikStorage.cs
// Author:  paracelsus
// Created: Thursday, March 25, 2021 4:23:30 PM
// Purpose: Definition of Class UpravnikStorage

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Model
{
   public class UpravnikStorage
   {
        private const string path = @".\..\..\..\Data\upravnici.json";

        private static Dictionary<String, Upravnik> readFile()
        {
            String json = File.ReadAllText(path);
            if (json.Equals(""))
            {
                return new Dictionary<String, Upravnik>();
            }
            else
            {
                return JsonSerializer.Deserialize<Dictionary<String, Upravnik>>(json);
            }
        }

        private static void writeFile(Dictionary<String, Upravnik> prostorije)
        {
            string json = JsonSerializer.Serialize(prostorije);
            File.WriteAllText(path, json);
        }


        public static bool Create(Upravnik Upravnik)
        {
            Dictionary<String, Upravnik> prostorije = readFile();

            if (prostorije.ContainsKey(Upravnik.KorisnickoIme.ToString()))
            {
                return false;
            }

            prostorije[Upravnik.KorisnickoIme] = Upravnik;

            writeFile(prostorije);

            return true;
        }

        public static Upravnik Read(string korisnickoIme)
        {
            Dictionary<String, Upravnik> prostorije = readFile();
            Upravnik retVal;

            if (!prostorije.TryGetValue(korisnickoIme, out retVal))
            {
                return null;
            }

            return retVal;
        }


        public static bool Update(Upravnik Upravnik)
        {
            Dictionary<String, Upravnik> prostorije = readFile();

            if (!prostorije.ContainsKey(Upravnik.KorisnickoIme))
            {
                return false;
            }

            prostorije[Upravnik.KorisnickoIme] = Upravnik;

            writeFile(prostorije);

            return true;
        }


        public static bool Delete(string korisnickoIme)
        {
            Dictionary<String, Upravnik> prostorije = readFile();

            bool retVal = prostorije.Remove(korisnickoIme);

            writeFile(prostorije);

            return retVal;
        }


    }
}