// File:    UlogovanKorisnik.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:37:39 PM
// Purpose: Definition of Class UlogovanKorisnik

using System;

namespace Model
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

        private Adresa adresa;

        public UlogovanKorisnik(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa)
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

        public UlogovanKorisnik()
        {
        }

        public String Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public string Prezime { get => prezime; set => prezime = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Email { get => email; set => email = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public Adresa Adresa { get => adresa; set => adresa = value; }

        public String ImePrezime { get => (ime + " " + prezime); }



    }
}