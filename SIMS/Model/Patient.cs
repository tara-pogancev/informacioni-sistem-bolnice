// File:    Pacijent.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:35:53 PM
// Purpose: Definition of Class Pacijent

using Newtonsoft.Json;
using SIMS.Model;
using System;
using System.Collections.Generic;
using SIMS.Repositories.AllergenRepo;
using SIMS.Repositories.DoctorRepo;

namespace SIMS.Model
{
    public class Patient : LoggedUser
    {
        public SexType PatientGender { get; set; }
        public BloodType BloodType { get; set; }
        public bool IsBanned { get; set; }
        public string Lbo { get; set; }
        public bool Guest { get; set; }
        public List<Allergen> Allergens { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> HronicalDiseases { get; set; }
        public Doctor ChosenDoctor {get;set;}


        public Patient(string name, string lastName, string jmbg, string username, string password, string email, string phone, Address address, String lbo, Boolean guest, List<Allergen> allergens) : base(name, lastName, jmbg, username, password, email, phone, address)
        {
            Lbo = lbo;
            Guest = guest;
            Allergens = allergens;
            IsBanned = false;
            Serialize = true;

            foreach (var allergen in allergens)
            {
                allergen.Name = AllergenFileRepository.Instance.FindById(allergen.ID).Name;
            }
            ChosenDoctor = new DoctorFileRepository().GetAll()[0];
        }

        public Patient(string name, string lastName, string jmbg, string username, string password, string email, string phone, Address address, String lbo, Boolean guest, List<Allergen> allergens, DateTime dateOfBirth, BloodType bloodType, SexType gender, List<string> hronicalDiseases) : base(name, lastName, jmbg, username, password, email, phone, address)
        {
            Lbo = lbo;
            Guest = guest;
            Allergens = allergens;
            DateOfBirth = dateOfBirth;
            BloodType = bloodType;
            PatientGender = gender;
            HronicalDiseases = hronicalDiseases;
            IsBanned = false;
            Serialize = true;

            foreach (var allergen in Allergens)
            {
                allergen.Name = AllergenFileRepository.Instance.FindById(allergen.ID).Name;
            }
            ChosenDoctor = new DoctorFileRepository().GetAll()[0];
        }

        public Patient(Patient patient)
        {
            Name = patient.Name;
            LastName = patient.LastName;
            Jmbg = patient.Jmbg;
            Username = patient.Username;
            Password = patient.Password;
            Email = patient.Email;
            Phone = patient.Phone;
            Address = patient.Address;
            Serialize = patient.Serialize;
            Lbo = patient.Lbo;
            Guest = patient.Guest;
            Allergens = patient.Allergens;
            DateOfBirth = patient.DateOfBirth;
            BloodType = patient.BloodType;
            PatientGender = patient.PatientGender;
            HronicalDiseases = patient.HronicalDiseases;
            IsBanned = patient.IsBanned;
        }

        public Patient() : base()
        {
            IsBanned = false;
            Serialize = true;
            ChosenDoctor = new DoctorFileRepository().GetAll()[0];
        }

        public Patient(string name, string lastName, string jmbg) : base(name, lastName, jmbg, "", "", "", "", new Address("", "", new City("", 0, new Country(""))))
        {
            Lbo = "";
            Guest = true;
            Allergens = new List<Allergen>();
            DateOfBirth = DateTime.MinValue;
            BloodType = BloodType.Op;
            PatientGender = SexType.Male;
            HronicalDiseases = new List<string>();
            ChosenDoctor = new DoctorFileRepository().GetAll()[0];
        }

        public String GetIfGuestString()
        {
                if (Guest)
                    return "Da";
                else
                    return "Ne";
        }

        public void SetAttributes(Patient p)
        {
            Name = p.Name;
            LastName = p.LastName;
            Jmbg = p.Jmbg;
            Username = p.Username;
            Password = p.Password;
            Email = p.Email;
            Phone = p.Phone;
            Address = p.Address;
            Lbo = p.Lbo;
            Guest = p.Guest;
        }

        public string GetAllergenListString()
        {
                string allergensString = "";
                if (Allergens.Count == 0)
                    return "Nema";

                AllergenFileRepository allergens = new AllergenFileRepository();

                foreach (Allergen a in Allergens)
                    allergensString += a.Name + ", ";
                return allergensString.Remove(allergensString.Length - 2); 
        }

        public bool Unavailable(Appointment appointment)
        {
            return appointment.Patient.Jmbg == this.Jmbg;
        }

        public String GetDateOfBirthString() 
        { 
            return DateOfBirth.ToString("dd.MM.yyyy."); 
        }

        public String GetGenderString()
        {
                if (PatientGender == SexType.Male)
                    return "Muško";
                else
                    return "Žensko";
        }

        public String GetBloodTypeString()
        {
                if (BloodType == BloodType.ABn)
                    return "AB-";
                else if (BloodType == BloodType.ABp)
                    return "AB+";
                else if (BloodType == BloodType.Ap)
                    return "A+";
                else if (BloodType == BloodType.An)
                    return "A-";
                else if (BloodType == BloodType.Bp)
                    return "B+";
                else if (BloodType == BloodType.Bn)
                    return "B-";
                else if (BloodType == BloodType.Op)
                    return "O+";
                else if (BloodType == BloodType.On)
                    return "O-";

                return null;
        }

        public bool IsAlergic(Medication lek)
        {
            foreach (var a in this.Allergens)
            {
                if (lek.Components.Contains(a))
                    return true;
            }
            return false;
        }

        public string GetHronicalDiseases()
        {
                string hronBolestiString = "";
                if (HronicalDiseases.Count == 0 || HronicalDiseases.Contains(""))
                    return "Nema";

                foreach (string a in HronicalDiseases)
                    hronBolestiString += a + ", ";
                return hronBolestiString.Remove(hronBolestiString.Length - 2);
        }

       
        public bool ShouldSerializeBloodType()
        {
            return Serialize;
        }

        public bool ShouldSerializePatientGender()
        {
            return Serialize;
        }

        public bool ShouldSerializeGetHronicalDiseases()
        {
            return Serialize;
        }

        public bool ShouldSerializeDateOfBirth()
        {
            return Serialize;
        }

        public bool ShouldSerializeAllergens()
        {
            return Serialize;
        }

        public bool ShouldSerializeGuest()
        {
            return Serialize;
        }

        public bool ShouldSerializeLbo()
        {
            return Serialize;
        }

        public bool ShouldSerializeIsBanned()
        {
            return Serialize;
        }

        public bool ShouldSerializeHronicalDiseases()
        {
            return Serialize;
        }
        public bool ShouldSerializeChosenDoctor()
        {
            return Serialize;
        }
                
    }
}