using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ReceptStorage : Storage<string, Recept, ReceptStorage>
    {
        protected override string getKey(Recept entity)
        {
            return entity.ReceptKey;
        }

        protected override void RemoveReferences(string key)
        {
            //throw new NotImplementedException();
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\recepti.json";
        }

        public List<Recept> ReadByPatient(Pacijent p)
        {
            List<Recept> retVal = new List<Recept>();

            foreach (Recept r in this.ReadList())
            {
                if (r.PacijentKey == p.Jmbg)
                    retVal.Add(r);
            }

            return retVal;
        }

    }
}
