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

            for (int i=0;i<retVal.Count;i++)
            {
                if (!(retVal[i].Target[0].Equals("All") || retVal[i].ContainsTarget(key)))
                {
                    retVal.RemoveAt(i);
                    i--;
                }
                    
            }

            return retVal;

        }
    }
}
