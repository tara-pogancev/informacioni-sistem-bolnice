using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.Model
{
    class DoctorSurveyRepository : GenericFileRepository<string, DoctorSurvey, DoctorSurveyRepository>
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
