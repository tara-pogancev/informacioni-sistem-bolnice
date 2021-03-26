// File:    UlogovanKorisnik.cs
// Author:  paracelsus
// Created: 22 March 2021 18:37:39
// Purpose: Definition of Class UlogovanKorisnik

using System;

namespace P1
{
    public class UlogovanKorisnik
   {
      private String ime;
      private String prezime;
      private String jmbg;
      private String korisnickoIme;
      private String lozinka;
      private String email;
      private String telefon;
      
      public Adresa adresa;

      public UlogovanKorisnik() { }

      public UlogovanKorisnik(String ime,String prezime,String jmbg,String korisnickoIme,String lozinka,String email,String telefon,Adresa adresa)
      {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.email = email;
            this.telefon = telefon;
            this.adresa = adresa;
      }

        public String Ime 
        {
            get { return ime; }
            set { ime = value; } 
        }

        public String Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }
        public String JMBG
        {
            get { return jmbg; }
            set { jmbg = value; }
        }
        public String KorisnickoIme
        {
             get { return korisnickoIme; }
             set { korisnickoIme = value; }
         }
        

        public String Lozinka
        {
            get { return lozinka; }
            set { lozinka = value; }
        }
        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        public String Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }
        public Adresa Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }






    }
}