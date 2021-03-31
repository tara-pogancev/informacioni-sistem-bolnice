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
      private TimeSpan vremeTrajanja;
      private TipTermina vrstaTermina;

      private String lekarKey;         //JMBG lekara
      private String pacijentKey;      //JMBG pacijenta
      private String prostorijaKey;    //Naziv prostorije
        
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
        public String LekarKey { get => lekarKey; set { lekarKey = value; OnPropertyChanged("LekarKey"); } }
        public String PacijentKey { get => pacijentKey; set { pacijentKey = value; OnPropertyChanged("PacijentKey"); } }
        public String Prostorija { get => prostorijaKey; set { prostorijaKey = value; OnPropertyChanged("Prostorija"); } }

        public Termin(DateTime pocetnoVreme, TimeSpan vremeTrajanja, TipTermina vrstaTermina, String lekar, String pacijent, String prostorija)
        {
            this.pocetnoVreme = pocetnoVreme;
            this.vremeTrajanja = vremeTrajanja;
            this.vrstaTermina = vrstaTermina;
            this.lekarKey = lekar;
            this.pacijentKey = pacijent;
            this.prostorijaKey = prostorija;
        }

        public Termin()
        {
            this.vrstaTermina = TipTermina.pregled;
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
            get 
            {
                PacijentStorage storageS = new PacijentStorage();            
                return (storageS.Read(pacijentKey).ImePrezime); 
            }
        }

        public String ImeLekara
        {
            get
            {
                LekarStorage storageL = new LekarStorage();
                return (storageL.Read(lekarKey).ImePrezime);
            }
            get { return (this.lekar.ImePrezime); }
        }

        public String NazivProstorije
        {
            get { return this.prostorija.Broj; }
        }
    }
}