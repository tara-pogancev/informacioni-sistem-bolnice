// File:    Termin.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:56 PM
// Purpose: Definition of Class Termin

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

        private String lekarKey;         //JMBG lekara
        private String pacijentKey;      //JMBG pacijenta
        private String prostorijaKey;    //Naziv prostorije
        private String terminKey;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public DateTime PocetnoVreme { get => pocetnoVreme; set { pocetnoVreme = value; OnPropertyChanged("PocetnoVreme"); OnPropertyChanged("Vrijeme"); OnPropertyChanged("Datum"); } }
        public int VremeTrajanja { get => vremeTrajanja; set => vremeTrajanja = value; }
        public TipTermina VrstaTermina { get => vrstaTermina; set => vrstaTermina = value; }
        public String LekarKey { get => lekarKey; set { lekarKey = value; OnPropertyChanged("LekarKey"); } }
        public String PacijentKey { get => pacijentKey; set { pacijentKey = value; OnPropertyChanged("PacijentKey"); } }
        public String Prostorija { get => prostorijaKey; set { prostorijaKey = value; OnPropertyChanged("Prostorija"); } }

        public Termin(DateTime pocetnoVreme, int vremeTrajanja, TipTermina vrstaTermina, String lekar, String pacijent, String prostorija)
        {
            this.pocetnoVreme = pocetnoVreme;
            this.vremeTrajanja = vremeTrajanja;
            this.vrstaTermina = vrstaTermina;
            this.lekarKey = lekar;
            this.pacijentKey = pacijent;
            this.prostorijaKey = prostorija;
            this.terminKey = DateTime.Now.ToString("yyMMddhhmmss");
        }

        public Termin()
        {
            this.vrstaTermina = TipTermina.pregled;
            this.terminKey = DateTime.Now.ToString("yyMMddhhmmss");
        }        

        public String Datum { get => PocetnoVreme.ToString("dd.MM.yyyy."); }

        public String Vrijeme { get => PocetnoVreme.ToString("HH:mm"); }

        public String TerminKey { get => terminKey; set => terminKey = value; }

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

        public String ImePacijenta
        {
            get 
            {
                Pacijent p = PacijentStorage.Instance.Read(pacijentKey);
                return (p.ImePrezime); 
            }
        }

        public String ImeLekara
        {
            get
            {
                Lekar l = LekarStorage.Instance.Read(lekarKey);
                return (l.ImePrezime);
            }
        }

        public String NazivProstorije
        {
            get { return this.prostorijaKey; }
        }

        public bool Evidentiran
        {
            get
            {
                if (AnamnezaStorage.Instance.Read(this.TerminKey) == null)
                    return false;
                else return true;
            }
        }

        public DateTime KrajnjeVreme
        {
            get
            {
                DateTime krajnjeVreme = PocetnoVreme.AddMinutes(VremeTrajanja);
                return krajnjeVreme;
            }
        }

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