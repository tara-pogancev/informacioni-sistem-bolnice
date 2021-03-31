// File:    Termin.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:56 PM
// Purpose: Definition of Class Termin

using System;
using System.ComponentModel;

namespace Model
{
   public class Termin : INotifyPropertyChanged
   {
      private DateTime pocetnoVreme;
      private TimeSpan vremeTrajanja;
      private TipTermina vrstaTermina;

      private Lekar lekar;
      private Pacijent pacijent;
      private Prostorija prostorija;
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public DateTime PocetnoVreme { get => pocetnoVreme; set { pocetnoVreme = value; OnPropertyChanged("PocetnoVreme"); OnPropertyChanged("Vrijeme"); OnPropertyChanged("Datum"); } }
        public TimeSpan VremeTrajanja { get => vremeTrajanja; set => vremeTrajanja = value; }
        public TipTermina VrstaTermina { get => vrstaTermina; set => vrstaTermina = value; }
        public Lekar Lekar { get => lekar; set { lekar = value; OnPropertyChanged("Lekar"); } }
        public Pacijent Pacijent { get => pacijent; set { pacijent = value; OnPropertyChanged("Pacijent"); } }
        public Prostorija Prostorija { get => prostorija; set { prostorija = value; OnPropertyChanged("Prostorija"); } }

        public Termin(DateTime pocetnoVreme, TimeSpan vremeTrajanja, TipTermina vrstaTermina, Lekar lekar, Pacijent pacijent, Prostorija prostorija)
        {
            this.pocetnoVreme = pocetnoVreme;
            this.vremeTrajanja = vremeTrajanja;
            this.vrstaTermina = vrstaTermina;
            this.lekar = lekar;
            this.pacijent = pacijent;
            this.prostorija = prostorija;
        }

        public Termin()
        {
        }

        

        public String Datum { get => pocetnoVreme.ToString("dd/MM/yyyy"); }
        public String Vrijeme { get => PocetnoVreme.ToString("HH:mm"); }

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
            get { return (this.pacijent.Ime + " " + this.pacijent.Prezime); }
        }

        public String ImeLekara
        {
            get { return (this.lekar.ImePrezime); }
        }

        public String NazivProstorije
        {
            get { return this.prostorija.Broj; }
        }
    }
}