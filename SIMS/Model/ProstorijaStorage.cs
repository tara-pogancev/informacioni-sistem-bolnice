
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Model
{
    public class ProstorijaStorage
    {
        private const string path = @"C:\Users\paracelsus\Desktop\SIMSPR\informacioni-sistem-bolnice\SIMS\Data\prostorije.json";

        private static Dictionary<String, Prostorija> readFile()
        {
            String json = File.ReadAllText(path);
            if (json.Equals(""))
            {
                return new Dictionary<String, Prostorija>();
            }
            else
            {
                return JsonSerializer.Deserialize<Dictionary<String, Prostorija>>(json);
            }
        }

        private static void writeFile(Dictionary<String, Prostorija> prostorije)
        {
            string json = JsonSerializer.Serialize(prostorije);
            File.WriteAllText(path, json);
        }


        public static bool Create(Prostorija prostorija)
        {
            Dictionary<String, Prostorija> prostorije = readFile();

            prostorije.Add(prostorija.Broj.ToString(), prostorija);

            writeFile(prostorije);

            return true;
        }

        public static Prostorija Read(int broj)
        {
            Dictionary<String, Prostorija> prostorije = readFile();
            Prostorija retVal;

            if (!prostorije.TryGetValue(broj.ToString(), out retVal))
            {
                return null;
            }

            return retVal;
        }


        public static bool Update(Prostorija p)
        {
            Dictionary<String, Prostorija> prostorije = readFile();

            prostorije[p.Broj.ToString()] = p;

            writeFile(prostorije);

            return true;
        }


        public static bool Delete(int broj)
        {
            Dictionary<String, Prostorija> prostorije = readFile();

            prostorije.Remove(broj.ToString());

            writeFile(prostorije);

            return true;
        }

    }
}