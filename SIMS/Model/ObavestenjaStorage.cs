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

        public List<Obavestenje> ReadByUser(String key)
        {
            List<Obavestenje> retVal = this.ReadList();

            foreach (Obavestenje o in this.ReadList())
            {
                if (o.Target != "All" || o.Target != (key))
                    retVal.Remove(o);
            }

            return retVal;

        }
    }
}
