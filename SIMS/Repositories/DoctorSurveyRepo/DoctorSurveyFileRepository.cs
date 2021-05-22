using Model;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.DoctorRepo.DoctorSurveyRepo
{
    class DoctorSurveyFileRepository : GenericFileRepository<string, DoctorSurvey, DoctorSurveyRepository>,IDoctorSurveyRepository
    {
        

        protected override string getKey(DoctorSurvey entity)
        {
            return entity.IdAnkete;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\anketaLekara.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }
    }
}

