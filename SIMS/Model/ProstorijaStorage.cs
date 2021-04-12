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
            TerminStorage storageT = new TerminStorage();
            foreach (Termin t in storageT.ReadList())
            {
                if (t.Prostorija == key)
                {
                    storageT.Delete(t.TerminKey);
                }
            }

        }

        public bool UpdateKolicinaOpreme(string keyProstorije, string keyOpreme, int kolicina)
        {
            Oprema dinamickaOprema = OpremaStorage.Instance.Read(keyOpreme);
            Prostorija prostorija = Read(keyProstorije);

            if (dinamickaOprema == null || prostorija == null)
            {
                return false;
            }

            prostorija.SetKolicinaOpreme(keyOpreme, kolicina);
            Update(prostorija);

            return true;
        }

    }
}