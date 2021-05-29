using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.DoctorSurveyRepo
{
    interface IDoctorSurveyRepository:IGenericRepository<DoctorSurvey,String>
    {
        List<DoctorSurvey> GetSurveysForDoctor(Doctor doctor);
    }
}
