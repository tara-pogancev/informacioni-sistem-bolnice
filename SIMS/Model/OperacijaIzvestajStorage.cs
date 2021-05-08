using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class OperacijaIzvestajStorage : Storage<string, OperacijaIzvestaj, OperacijaIzvestajStorage>
    {
        protected override string getKey(OperacijaIzvestaj entity)
        {
            return entity.OperacijaKey;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\operacijaIzvestaji.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        public List<OperacijaIzvestaj> ReadByPatient(Pacijent p)
        {
            List<OperacijaIzvestaj> retVal = new List<OperacijaIzvestaj>();

            foreach (OperacijaIzvestaj a in this.ReadList())
            {
                a.InitData();

                if (a.Operacija.Pacijent.Jmbg == p.Jmbg)
                    retVal.Add(a);
            }

            return retVal;
        }
    }
}
