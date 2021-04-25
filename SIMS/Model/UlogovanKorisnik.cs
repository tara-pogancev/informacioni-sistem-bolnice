// File:    UlogovanKorisnik.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:37:39 PM
// Purpose: Definition of Class UlogovanKorisnik

using Newtonsoft.Json;
using System;

namespace Model
{
   public class UlogovanKorisnik
   {
      protected String ime;
      protected String prezime;
      protected String jmbg;
      protected String korisnickoIme;
      protected String lozinka;
      protected String email;
      protected String telefon;
      protected bool serijalizuj;

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
            this.serijalizuj = true;
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


        [JsonIgnore]
        public String ImePrezime { get => (ime + " " + prezime); }

        [JsonIgnore]
        public String fullAddress
        {
            get
            {
                return this.Adresa.Ulica + " " + this.Adresa.Broj + ", " + 
                    this.Adresa.grad.Naziv + " " + this.Adresa.grad.PostanskiBroj + ", " + this.Adresa.grad.Drzava.Naziv;
            }
        }

        [JsonIgnore]
        public bool Serijalizuj { get => serijalizuj; set => serijalizuj = value; }

        public bool ShouldSerializePrezime()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeKorisnickoIme()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeLozinka()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeEmail()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeTelefon()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeAdresa()
        {
            return serijalizuj;
        }

        public bool ShouldSerializeIme()
        {
            return serijalizuj;
        }


    }
}