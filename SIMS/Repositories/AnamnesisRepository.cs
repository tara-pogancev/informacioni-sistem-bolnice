using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class AnamnesisRepository : GenericFileRepository<string, Anamnesis, AnamnesisRepository>
    {
        protected override string getKey(Anamnesis entity)
        {
            return entity.IdAnamneze;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\anamneze.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        public List<Anamnesis> ReadByPatient(Patient p)
        {
            List<Anamnesis> retVal = new List<Anamnesis>();

            foreach (Anamnesis a in this.ReadEntities())
            {
                a.InitData();

                if (a.Termin.Pacijent.Jmbg == p.Jmbg)
                    retVal.Add(a);
            }

            return retVal;
        }

    }
}
