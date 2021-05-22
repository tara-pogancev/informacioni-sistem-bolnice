// File:    Lekar.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:32:13 PM
// Purpose: Definition of Class Lekar

using Newtonsoft.Json;
using SIMS.Repositories.DoctorRepo.DoctorSurveyRepo;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Doctor : LoggedUser
    {
        public int DaniGodisnjegOdmora { get; set; }
        public double Grade { get; set; }

        public Specialization SpecijalizacijaLekara { get; set; }

        public Doctor()
        {
            Grade = 0.0;
        }

        public Doctor(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Address adresa, Specialization specijalizacija, int daniGodisnjegOdmora) : base(ime, prezime, jmbg, korisnickoIme, lozinka, email, telefon, adresa)
        {
            DaniGodisnjegOdmora = daniGodisnjegOdmora;
            SpecijalizacijaLekara = specijalizacija;
            Grade = 0.0;
        }
             
        public List<Appointment> getZauzetiTermini()
        {
            List<Appointment> retVal = new List<Appointment>();

            retVal = AppointmentRepository.Instance.ReadByDoctor(this);

            return retVal;

        }

        // Salje informacije o novom terminu
        public Boolean IsFree(Appointment terminNew)
        {
            foreach (Appointment t in AppointmentRepository.Instance.ReadByDoctor(this))
            {
                if (terminNew.KrajnjeVreme > t.PocetnoVreme && terminNew.KrajnjeVreme <= t.KrajnjeVreme)
                    return false;

                if (terminNew.PocetnoVreme >= t.PocetnoVreme && terminNew.PocetnoVreme < t.KrajnjeVreme)
                    return false;
            }

            return true;
        }

        public void RecalulateGrade()
        {
            IDoctorSurveyRepository doctorSurveyRepository = new DoctorSurveyFileRepository();
            double Grades = 0;
            int counter = 0;
            
            foreach (var survey in doctorSurveyRepository.GetAll())
            {
                if (survey.DoctorId == this.Jmbg){
                    Grades += survey.Ocjena;
                    counter++;
                }
            }
            if (counter == 0)
            {
                this.Grade = 0;
            }
            else
            {
                this.Grade = Grades / counter;
            }
            
   



        }

        // Salje izmenjen termin ali njega ignorise prilikom provere
        public Boolean IsFreeUpdate(Appointment terminNew)
        {
            foreach (Appointment t in AppointmentRepository.Instance.ReadByDoctor(this))
            {
                if (t.TerminKey != terminNew.TerminKey)
                {
                    if (terminNew.KrajnjeVreme > t.PocetnoVreme && terminNew.KrajnjeVreme <= t.KrajnjeVreme)
                        return false;

                    if (terminNew.PocetnoVreme >= t.PocetnoVreme && terminNew.PocetnoVreme < t.KrajnjeVreme)
                        return false;
                }
            }

            return true;
        }

        public bool ShouldSerializeDaniGodisnjegOdmora()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeSpecijalizacijaLekara()
        {
            return serijalizuj;
        }

        [JsonIgnore]
        public String NameAndSpecialization { get { return ImePrezime + ", " + Specialization; } }

        [JsonIgnore]
        public String Specialization 
        { 
            get 
            {
                if (SpecijalizacijaLekara == Model.Specialization.OpstaPraksa)
                    return "Lekar opšte prakse";
                else if (SpecijalizacijaLekara == Model.Specialization.Hirurg)
                    return "Hirurg";
                else if (SpecijalizacijaLekara == Model.Specialization.Internista)
                    return "Internista";
                else if (SpecijalizacijaLekara == Model.Specialization.Dermatolog)
                    return "Dermatolog";
                else if (SpecijalizacijaLekara == Model.Specialization.Kardiolog)
                    return "Kardiolog";
                else if (SpecijalizacijaLekara == Model.Specialization.Otorinolaringolog)
                    return "Otorinolaringolog";
                else if (SpecijalizacijaLekara == Model.Specialization.Stomatolog)
                    return "Stomatolog";
                else if (SpecijalizacijaLekara == Model.Specialization.Urolog)
                    return "Urolog";
                else if (SpecijalizacijaLekara == Model.Specialization.Ginekolog)
                    return "Ginekolog";
                else
                    return "Neurolog";
            } 

        }
        

    }
}