// File:    Operacija.cs
// Author:  paracelsus
// Created: 22 March 2021 18:47:57
// Purpose: Definition of Class Operacija

using System;
using System.Globalization;

namespace P1
{
   public class Operacija
   {
        private DateTime pocetnoVreme;
        private DateTime krajnjeVreme;
      
        public Specijalista specijalista;
        public Sala sala;
        public Pacijent pacijent;

        public Operacija (DateTime pocetnoVreme, DateTime krajnjeVreme, Specijalista specijalista, Sala sala, Pacijent pacijent)
        {
            this.pocetnoVreme = pocetnoVreme;
            this.krajnjeVreme = krajnjeVreme;
            this.specijalista = specijalista;
            this.sala = sala;
            this.pacijent = pacijent;
        }

        public Operacija(String pocetnoVreme, String krajnjeVreme)
        {
            //TODO

            this.pocetnoVreme = Convert.ToDateTime(pocetnoVreme);
            this.krajnjeVreme = Convert.ToDateTime(krajnjeVreme);

            this.specijalista = null;
            this.sala = null;
            this.pacijent = null;
        }


        public DateTime PocetnoVreme
        {
            get { return pocetnoVreme; }
            set { pocetnoVreme = value; }

        }

        public DateTime KrajnjeVreme
        {
            get { return krajnjeVreme; }
            set { krajnjeVreme = value; }

        }

        public Specijalista Specijalista
        {
            get { return specijalista; }
            set { specijalista = value; }
        }

        public Pacijent Pacijent
        {
            get { return pacijent; }
            set { pacijent = value; }
        }

        public Sala Sala
        {
            get { return sala; }
            set { sala = value; }
        }

        public String ImePacijenta
        {
            get { return (pacijent.Ime + " " + pacijent.Prezime); }
        }

        public String KrajnjeVremeString
        {
            get { return this.krajnjeVreme.ToString("HH:mm"); }
        }

        public String PocetnoVremeString
        {
            get { return this.pocetnoVreme.ToString("HH:mm"); }
        }

        public String DatumString
        {
            get { return this.pocetnoVreme.ToString("dd/MM/yyyy"); }
        }

        public String TipTermina
        {
            get { return "Operacija"; }
        }

        public String ImeSale
        {
            get { return this.sala.Naziv; }
        }

    }
}