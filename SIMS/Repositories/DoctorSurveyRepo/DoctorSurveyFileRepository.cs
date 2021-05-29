using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;


namespace SIMS.Repositories.DoctorSurveyRepo
{
    class DoctorSurveyFileRepository : GenericFileRepository<string, DoctorSurvey, DoctorSurveyFileRepository>,IDoctorSurveyRepository
    {
        public List<DoctorSurvey> GetSurveysForDoctor(Doctor doctor)
        {
            List<DoctorSurvey> surveys = GetAll();
            for (int i = 0; i < surveys.Count; i++)
            {
                if (surveys[i].DoctorId != doctor.Jmbg)
                {
                    surveys.RemoveAt(i);
                    i--;
                }
            }
            return surveys;
        }

        protected override string getKey(DoctorSurvey entity)
        {
            return entity.SurveyID;
        }

        protected override string getPath()
        {
             return @".\..\..\..\Data\anketaLekara.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(DoctorSurvey entity)
        {
            //ne treba logika za serijalizaciju
        }
    }
}
