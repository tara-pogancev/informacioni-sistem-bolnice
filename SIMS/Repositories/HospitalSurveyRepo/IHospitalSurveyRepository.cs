using SIMS.Repositories.PatientRepo;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.HospitalSurveyRepo
{
    interface IHospitalSurveyRepository:IGenericRepository<HospitalSurvey,String>
    {
        List<HospitalSurvey> GetPatientSurveys(Patient pacijent);
    }
}
