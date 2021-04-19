using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class AnamnezaStorage : Storage<string, Anamneza, AnamnezaStorage>
    {
        protected override string getKey(Anamneza entity)
        {
            return entity.AnamnezaKey;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\anamneze.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        public List<Anamneza> ReadByPatient(Pacijent p)
        {
            List<Anamneza> retVal = new List<Anamneza>();

            foreach (Anamneza a in this.ReadList())
            {
                if (a.getTermin().PacijentKey == p.Jmbg)
                    retVal.Add(a);
            }

            return retVal;
        }

    }
}
