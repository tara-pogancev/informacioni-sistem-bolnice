using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.HospitalizationRepo
{
    class HospitalizationFileRepository : GenericFileRepository<String, Hospitalization, HospitalizationFileRepository>, IHospitalizationRepository
    {
        protected override string getKey(Hospitalization entity)
        {
            return entity.HospitalizationID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\hospitalization.json";
        }

        protected override void RemoveReferences(string key)
        {
            //throw new NotImplementedException();
        }

        protected override void ShouldSerialize(Hospitalization entity)
        {
            entity.Patient.Serialize = false;
            entity.LeadDoctor.Serialize = false;
            entity.Room.Serialize = false;
        }
    }
}
