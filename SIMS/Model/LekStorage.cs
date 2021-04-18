using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class LekStorage : Storage<string, Lek, LekStorage>
    {
        protected override string getKey(Lek entity)
        {
            return entity.ID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\lekovi.json";
        }

        protected override void RemoveReferences(string key)
        {
        
        }
    }
}
