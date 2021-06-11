using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.DoctorAppRatingRepo
{
    class DoctorAppRatingFileRepository : GenericFileRepository<String, DoctorAppRating, DoctorAppRatingFileRepository>, IDoctorAppRatingRepository
    {
        protected override string getKey(DoctorAppRating entity)
        {
            return entity.ID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\doctorAppRating.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(DoctorAppRating entity)
        {
            entity.Doctor.Serialize = false;
        }
    }
}
