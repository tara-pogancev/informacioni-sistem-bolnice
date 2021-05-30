using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.ViewPatient.ViewModel
{
    public class HomePageViewModel
    {
        public static Patient patient;

        public HomePageViewModel(Patient loggedPatient)
        {
            patient = loggedPatient;
        }
    }
}
