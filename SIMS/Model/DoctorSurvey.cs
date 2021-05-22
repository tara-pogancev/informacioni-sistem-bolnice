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
        private String doctorId;
        public DoctorSurvey():base()
        {
        }

        public DoctorSurvey(Appointment termin,int ocjena,String komentar,String idVlasnika):base(komentar,idVlasnika)
        {
            this.termin = termin;
            doctorId = termin.Lekar.Jmbg;
        }

        public Appointment Termin { get => termin; set => termin = value; }
        public int Ocjena { get => ocjena; set => ocjena = value; }
        public string DoctorId { get => doctorId; set =>doctorId = value; }
    }
}
