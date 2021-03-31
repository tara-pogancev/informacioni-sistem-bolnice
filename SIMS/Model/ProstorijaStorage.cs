
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Model
{
    public class ProstorijaStorage : Storage<string, Prostorija, ProstorijaStorage>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\prostorije.json";
        } 
        protected override string getKey(Prostorija entity)
        {
            return entity.Broj;
        }

        protected override void RemoveReferences(string key)
        {
            
        }

        public bool UpdateKolicineDinamickeOpreme(string keyProstorije, string keyOpreme, int kolicina)
        {
            DinamickaOprema dinamickaOprema = DinamickaOpremaStorage.Instance.Read(keyOpreme);
            Prostorija prostorija = Read(keyProstorije);

            if (dinamickaOprema == null || prostorija == null)
            {
                return false;
            }

            prostorija.SetKolicinaDinamickeOpreme(keyOpreme, kolicina);
            Update(prostorija);

            return true;
        }

        public bool UpdateKolicineStatickeOpreme(string keyProstorije, string keyOpreme, int kolicina)
        {
            StatickaOprema statickaOprema = StatickaOpremaStorage.Instance.Read(keyOpreme);
            Prostorija prostorija = Read(keyProstorije);

            if (statickaOprema == null || prostorija == null)
            {
                return false;
            }

            prostorija.SetKolicinaStatickeOpreme(keyOpreme, kolicina);
            Update(prostorija);

            return true;
        }

    }
}