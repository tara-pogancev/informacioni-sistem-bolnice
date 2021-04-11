using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ObavestenjaStorage : Storage<string, Obavestenje, ObavestenjaStorage>
    {
        protected override string getKey(Obavestenje entity)
        {
            return entity.ID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\obavestenja.json";
        }

        protected override void RemoveReferences(string key)
        {
        
        }
    }
}
