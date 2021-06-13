using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SIMS.Model;

namespace SIMS.ViewPatient.ViewModel
{
    

    class TherapyReportViewModel
    {
        public Patient Patient { get; set; }
        public String FirstDate { get; set; }
        public String FirstDay { get; set; }
        public String SecondDate { get; set; }
        public String SecondDay { get; set; }
        public String ThirdDate { get; set; }
        public String ThirdDay { get; set; }
        public String FourthDate { get; set; }
        public String FourthDay { get; set; }
        public String FifthDate { get; set; }
        public String FifthDay { get; set; }
        public String SixthDay { get; set; }
        public String SixthDate { get; set; }
        public String SeventhDate { get; set; }
        public String SeventhDay { get; set; }

        public TherapyReportViewModel()
        {
            Patient = HomePageViewModel.patient;
            FirstDate = DateTime.Now.ToString("dd.MM.yyyy.");
            FirstDay = DateToDay(DateTime.Now);
            SecondDate = DateTime.Now.AddDays(1).ToString("dd.MM.yyyy.");
            SecondDay = DateToDay(DateTime.Now.AddDays(1));
            ThirdDate = DateTime.Now.AddDays(2).ToString("dd.MM.yyyy.");
            ThirdDay = DateToDay(DateTime.Now.AddDays(2));
            FourthDate = DateTime.Now.AddDays(3).ToString("dd.MM.yyyy.");
            FourthDay = DateToDay(DateTime.Now.AddDays(3));
            FifthDate = DateTime.Now.AddDays(4).ToString("dd.MM.yyyy.");
            FifthDay = DateToDay(DateTime.Now.AddDays(4));
            SixthDate = DateTime.Now.AddDays(5).ToString("dd.MM.yyyy.");
            SixthDay = DateToDay(DateTime.Now.AddDays(5));
            SeventhDate = DateTime.Now.AddDays(6).ToString("dd.MM.yyyy.");
            SeventhDay = DateToDay(DateTime.Now.AddDays(6));
        }

        private string DateToDay(DateTime now)
        {
            return now.ToString("dddd", new CultureInfo("sr-Latn-RS"));
        }
    }
}
