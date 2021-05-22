using Model;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.HospitalSurveyRepo
{
    class HospitalSurveyFileRepository : GenericFileRepository<string, HospitalSurvey, HospitalSurveyRepository>,IHospitalSurveyRepository
    {
        

        public List<HospitalSurvey> getPatientSurveys(Patient pacijent)
        {
            List<HospitalSurvey> hospitalSurveys = base.GetAll();
            for (int i = 0; i < hospitalSurveys.Count; i++)
            {
                if (hospitalSurveys[i].IdVlasnika != pacijent.Jmbg)
                {
                    hospitalSurveys.RemoveAt(i);
                    i--;
                }
            }
            return hospitalSurveys;
        }

        protected override string getKey(HospitalSurvey entity)
        {
            return entity.IdAnkete;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\anketaBolnice.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

    
    }
}
