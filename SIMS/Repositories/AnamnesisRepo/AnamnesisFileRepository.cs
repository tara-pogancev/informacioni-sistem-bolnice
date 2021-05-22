using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AnamnesisRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AnamnesisRepository
{
    public class AnamnesisFileRepository : GenericFileRepository<string, Anamnesis, AnamnesisFileRepository>,IAnamnesisRepository
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

        public List<Anamnesis> ReadByPatient(Patient patient)
        {
            List<Anamnesis> retVal = new List<Anamnesis>();

            foreach (Anamnesis a in this.GetAll())
            {
                a.InitData();

                if (a.Termin.Pacijent.Jmbg == patient.Jmbg)
                    retVal.Add(a);
            }

            return retVal;
        }

    }
}
