using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    public class PatientController
    {
        private PatientService patientService;

        public PatientController()
        {
            patientService = new PatientService();
        }


    }
}
