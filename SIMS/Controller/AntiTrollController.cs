using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class AntiTrollController
    {
        private AntiTrollService antiTrollService;

        public AntiTrollController()
        {
            antiTrollService = new AntiTrollService();
        }

        public bool CanChangeAppointment(Patient patient) => antiTrollService.CanChangeAppointment(patient);

        public void BanUser(Patient patient) => antiTrollService.BanUser(patient);


    }
}
