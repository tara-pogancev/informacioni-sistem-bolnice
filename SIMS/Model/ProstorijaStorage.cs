
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
            return entity.Naziv;
        }

        protected override void removeReferences(string key)
        {
            TerminStorage storageT = new TerminStorage();
            foreach (Termin t in storageT.ReadAll())
            {
                if (t.Prostorija == key)
                {
                    //storageT.Delete(t);
                }
            }

        }

    }
}