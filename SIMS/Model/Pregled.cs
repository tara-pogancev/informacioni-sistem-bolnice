// File:    Pregled.cs
// Author:  paracelsus
// Created: 22 March 2021 18:47:56
// Purpose: Definition of Class Pregled

using System;
using System.Globalization;

namespace P1
{
    public class Pregled
    {
        private DateTime pocetnoVreme;
        private DateTime vrijemeZavrsetka;

        public Lekar lekar;
        public Pacijent pacijent;
        public Prostorija prostorija;

        public Pregled(DateTime pocetnoVreme, DateTime vrijemeZavrsetka, Lekar lekar, Pacijent pacijent, Prostorija prostorija)
        {
            this.pocetnoVreme = pocetnoVreme;
            this.vrijemeZavrsetka = vrijemeZavrsetka;
            this.lekar = lekar;
            this.pacijent = pacijent;
            this.prostorija = prostorija;
        }

        public Pregled(String pocetnoVreme, String vrijemeZavrsetka)
        {
            //TODO: Test konstruktor koji bi trebalo da uzima kljuceve za lekara, pacijenta a kasnije i prostoriju
            this.pocetnoVreme = Convert.ToDateTime(pocetnoVreme);
            this.vrijemeZavrsetka = Convert.ToDateTime(vrijemeZavrsetka);

            this.lekar = null;
            this.pacijent = null;
            this.prostorija = null;
        }

        public DateTime PocetnoVreme
        {
            get {return pocetnoVreme;}
            set { pocetnoVreme = value; }
            
        }

        public DateTime VrijemeZavrsetka
        {
            get { return vrijemeZavrsetka; }
            set { vrijemeZavrsetka = value; }

        }

        public Lekar Lekar
        {
            get { return lekar; }
            set { lekar = value; }
        }

        public Pacijent Pacijent
        {
            get { return pacijent; }
            set { pacijent = value; }
        }

        public Prostorija Prostorija
        {
            get { return prostorija; }
            set { prostorija = value; }
        }

        public String ImePacijenta
        {
            get { return (pacijent.Ime + " " + pacijent.Prezime); }
        }

        public String KrajnjeVremeString
        {
            get { return this.vrijemeZavrsetka.ToString("HH:mm"); }
        }

        public String PocetnoVremeString
        {
            get { return this.pocetnoVreme.ToString("HH:mm"); }
        }

        public String DatumString
        {
            get { return this.pocetnoVreme.ToString("dd/MM/yyyy");  }
        }

        public String TipTermina
        {
            get { return "Pregled";  }
        }

        public String ImeProstorije
        {
            get { return this.prostorija.Naziv; }
        }



    }
}