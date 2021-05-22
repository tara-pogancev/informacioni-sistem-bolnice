using SIMS.Repositories.PatientRepo;
using SIMS.PacijentGUI;
using SIMS.Repositories.HospitalSurveyRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class HospitalSurveyFileRepository : GenericFileRepository<string, HospitalSurvey, HospitalSurveyFileRepository>,IHospitalSurveyRepository
    {
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

        public List<HospitalSurvey> GetPatientSurveys(Patient pacijent)
        {
            List<HospitalSurvey> anketeBolnice = GetAll();
            for(int i = 0; i < anketeBolnice.Count; i++)
            {
                if (anketeBolnice[i].IdVlasnika != pacijent.Jmbg)
                {
                    anketeBolnice.RemoveAt(i);
                    i--;
                }
            }
            return anketeBolnice;
        } 


    }
}
