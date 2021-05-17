using Model;
using SIMS.PacijentGUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class HospitalSurveyRepository : Repository<string, HospitalSurvey, HospitalSurveyRepository>
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

        public List<HospitalSurvey> getAnketeByPatient(Patient pacijent)
        {
            List<HospitalSurvey> anketeBolnice = ReadList();
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
