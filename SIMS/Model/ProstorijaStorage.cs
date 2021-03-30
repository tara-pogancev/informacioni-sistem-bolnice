
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
            return entity.Broj.ToString();
        }

        protected override void removeReferences(string key)
        {
            
        }

    }
}