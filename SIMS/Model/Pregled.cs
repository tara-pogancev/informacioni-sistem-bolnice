// File:    Pregled.cs
// Author:  paracelsus
// Created: 22 March 2021 18:47:56
// Purpose: Definition of Class Pregled

using System;

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

        public DateTime PocetnoVreme
        {
            get{return pocetnoVreme;}
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

        public String Ime
        {
            get { return lekar.Ime; }
        }

    }
}