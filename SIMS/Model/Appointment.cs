// File:    Termin.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:56 PM
// Purpose: Definition of Class Termin

using Newtonsoft.Json;
using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIMS.Repositories.PatientRepo
{
   public class Appointment : INotifyPropertyChanged
   {
        private DateTime pocetnoVreme;
        private int vremeTrajanja;    //U minutima
        private AppointmentType vrstaTermina;
        private DateTime inicijalnoVrijeme;
        private String terminKey;
        private Doctor lekar;
        private Patient pacijent;
        private Room prostorija;
        private bool serijalizuj;
             
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public Appointment(DateTime pocetnoVreme, int vremeTrajanja, AppointmentType vrstaTermina, Doctor lekar, Patient pacijent, Room prostorija)
        {
            this.inicijalnoVrijeme = pocetnoVreme;
            this.pocetnoVreme = pocetnoVreme;
            this.vremeTrajanja = vremeTrajanja;
            this.vrstaTermina = vrstaTermina;
            this.lekar = lekar;
            this.pacijent = pacijent;
            this.prostorija = prostorija;
            this.terminKey = DateTime.Now.ToString("yyMMddhhmmssffffff");
            serijalizuj = true;
        }

        public Appointment()
        {
            this.vrstaTermina = AppointmentType.pregled;
            this.terminKey = DateTime.Now.ToString("yyMMddhhmmssffffff");
            serijalizuj = true;
        }

        public DateTime PocetnoVreme { get => pocetnoVreme; set { pocetnoVreme = value; OnPropertyChanged("PocetnoVreme"); OnPropertyChanged("Vrijeme"); OnPropertyChanged("Datum"); } }
        public int VremeTrajanja { get => vremeTrajanja; set => vremeTrajanja = value; }
        public AppointmentType VrstaTermina { get => vrstaTermina; set => vrstaTermina = value; }
        public Doctor Lekar { get => lekar; set { lekar = value; OnPropertyChanged("Lekar"); } }
        public Patient Pacijent { get => pacijent; set { pacijent = value; OnPropertyChanged("PacijentKey"); } }
        public Room Prostorija { get => prostorija; set { prostorija = value; OnPropertyChanged("Prostorija"); } }
        public DateTime InicijalnoVrijeme { get => inicijalnoVrijeme; set => inicijalnoVrijeme = value; }
        public String TerminKey { get => terminKey; set => terminKey = value; }

        [JsonIgnore]
        public bool Serijalizuj { get => serijalizuj; set => serijalizuj = value; }

        public bool ShouldSerializePocetnoVreme()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeVremeTrajanja()
        {
            return serijalizuj;
        }
        
        public bool ShouldSerializeVrstaTermina()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeLekar()
        {
            return serijalizuj;
        }

        public bool ShouldSerializePacijent()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeProstorija()
        {
            return serijalizuj;
        }
        public bool ShouldSerializeInicijalnoVrijeme()
        {
            return serijalizuj;
        }

        [JsonIgnore]
        public String Datum { get => PocetnoVreme.ToString("dd.MM.yyyy."); }

        [JsonIgnore]
        public String Vrijeme { get => PocetnoVreme.ToString("HH:mm"); }
                
        [JsonIgnore]
        public String GetVrsta
        {
            get {
                String ret = "";
                if (vrstaTermina == AppointmentType.pregled)
                {
                    ret = "Pregled";
                }
                else
                {
                    ret = "Operacija";
                }

                return ret;
            }
            
        }

        [JsonIgnore]
        public String ImePacijenta
        {
            get
            { 
                return (pacijent.ImePrezime); 
            }
        }

        [JsonIgnore]
        public String ImeLekara
        {
            get
            {
                //lekar = LekarStorage.Instance.ReadEntity(doktor) 
                return (lekar.ImePrezime);
            }
        }

        [JsonIgnore]
        public String NazivProstorije
        {
            get { return prostorija.Number; }
        }

        [JsonIgnore]
        public bool Evidentiran
        {
            get
            {
                if (AnamnesisFileRepository.Instance.FindById(this.TerminKey) == null && SurgeryReportRepository.Instance.FindById(this.TerminKey) == null)
                    return false;
                else return true;
            }
        }

        [JsonIgnore]
        public DateTime KrajnjeVreme
        {
            get
            {
                DateTime krajnjeVreme = PocetnoVreme.AddMinutes(VremeTrajanja);
                return krajnjeVreme;
            }
        }

        [JsonIgnore]
        public bool IsPast
        {
            get
            {
                // Vraca termine koji su se u potpunosti zavrsili
                if (this.KrajnjeVreme <= DateTime.Now)
                    return true;
                else return false;
            }
        }

        [JsonIgnore]
        public bool IsCurrent
        {
            get
            {
                //Vraca termine koji trenutno traju
                if (this.PocetnoVreme <= DateTime.Now && this.KrajnjeVreme >= DateTime.Now)
                    return true;

                return false;
            }
        }

        [JsonIgnore]
        public String AppointmentFullInfo
        {
            get
            {
                return ImeLekara + ", " + Vrijeme + " " + Datum;
            }
        }

        public void InitData()
        {
            Pacijent = new PatientFileRepository().FindById(Pacijent.Jmbg);
            Prostorija = new RoomRepository().FindById(Prostorija.Number);
            Lekar = new DoctorFileRepository().FindById(Lekar.Jmbg);
        }

    }
}