using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class DoctorSurvey : Survey
    {
        private Appointment appointment;
        private int grade;
        private String doctorId;

        public DoctorSurvey():base()
        {
        }

        public DoctorSurvey(Appointment termin,int ocjena,String komentar,String idVlasnika):base(komentar,idVlasnika)
        {
            this.appointment = termin;
            doctorId = termin.Doctor.Jmbg;
        }

        public Appointment Appointment { get => appointment; set => appointment = value; }
        public int Grade { get => grade; set => grade = value; }
        public string DoctorId { get => doctorId; set =>doctorId = value; }
    }
}
