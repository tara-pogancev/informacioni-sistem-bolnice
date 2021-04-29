// File:    Termin.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:56 PM
// Purpose: Definition of Class Termin

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
   public class Termin : INotifyPropertyChanged
   {
        private DateTime pocetnoVreme;
        private int vremeTrajanja;    //U minutima
        private TipTermina vrstaTermina;
        private DateTime inicijalnoVrijeme;
        private String terminKey;
        private Lekar lekar;
        private Pacijent pacijent;
        private Prostorija prostorija;
        private bool serijalizuj;
        

     /*   private String lekarKey;         //JMBG lekara
    private String pacijentKey;      //JMBG pacijenta
        private String prostorijaKey;    //Naziv prostorije*/
       
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public Termin(DateTime pocetnoVreme, int vremeTrajanja, TipTermina vrstaTermina, Lekar lekar, Pacijent pacijent, Prostorija prostorija)
        {
            this.inicijalnoVrijeme = pocetnoVreme;
            this.pocetnoVreme = pocetnoVreme;
            this.vremeTrajanja = vremeTrajanja;
            this.vrstaTermina = vrstaTermina;
            this.lekar = lekar;
            this.pacijent = pacijent;
            this.prostorija = prostorija;
            this.terminKey = DateTime.Now.ToString("yyMMddhhmmss");
            serijalizuj = true;
        }

        public Termin()
        {
            this.vrstaTermina = TipTermina.pregled;
            this.terminKey = DateTime.Now.ToString("yyMMddhhmmss");
            serijalizuj = true;
        }

        public DateTime PocetnoVreme { get => pocetnoVreme; set { pocetnoVreme = value; OnPropertyChanged("PocetnoVreme"); OnPropertyChanged("Vrijeme"); OnPropertyChanged("Datum"); } }
        public int VremeTrajanja { get => vremeTrajanja; set => vremeTrajanja = value; }
        public TipTermina VrstaTermina { get => vrstaTermina; set => vrstaTermina = value; }
        public Lekar Lekar { get => lekar; set { lekar = value; OnPropertyChanged("Lekar"); } }
        public Pacijent Pacijent { get => pacijent; set { pacijent = value; OnPropertyChanged("PacijentKey"); } }
        public Prostorija Prostorija { get => prostorija; set { prostorija = value; OnPropertyChanged("Prostorija"); } }
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
                if (vrstaTermina == TipTermina.pregled)
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
                //lekar = LekarStorage.Instance.Read(doktor) 
                return (lekar.ImePrezime);
            }
        }

        [JsonIgnore]
        public String NazivProstorije
        {
            get { return prostorija.Broj; }
        }

        [JsonIgnore]
        public bool Evidentiran
        {
            get
            {
                if (AnamnezaStorage.Instance.Read(this.TerminKey) == null)
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

    }
}