using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class DoctorSurvey : Survey
    {
        private Appointment termin;
        private int ocjena;
        public DoctorSurvey():base()
        {
        }

        public DoctorSurvey(Appointment termin,int ocjena,String komentar,String idVlasnika):base(komentar,idVlasnika)
        {
            this.termin = termin;
        }

        public Appointment Termin { get => termin; set => termin = value; }
        public int Ocjena { get => ocjena; set => ocjena = value; }
    }
}
