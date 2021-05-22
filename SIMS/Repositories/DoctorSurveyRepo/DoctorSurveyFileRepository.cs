using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.PatientRepo;
using SIMS.Model;


namespace SIMS.Repositories.DoctorSurveyRepo
{
    class DoctorSurveyFileRepository : GenericFileRepository<string, DoctorSurvey, DoctorSurveyFileRepository>,IDoctorSurveyRepository
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
