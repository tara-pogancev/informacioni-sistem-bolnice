// File:    PacijentStorage.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:17:45 PM
// Purpose: Definition of Class PacijentStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
   public class PacijentStorage
   {
      public bool Create()
      {
            List<Pacijent> pacijenti = new List<Pacijent>();
            Drzava Bih = new Drzava("Bosna i Hercegovina");
            Grad Foca = new Grad("Foca", 73300, Bih);
            Adresa adresaDj = new Adresa("Barakovac", 1, Foca);
            UlogovanKorisnik Djordje = new UlogovanKorisnik("Djordje", "Krsmanovic", "1", "pacijent", "pacijent", "djordje1499@gmail.com", "0645568131", adresaDj);
            //public Pacijent(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa,String lbo,Boolean gost)
            Pacijent p = new Pacijent(Djordje.Ime, Djordje.Prezime, Djordje.Jmbg, Djordje.KorisnickoIme, Djordje.Lozinka, Djordje.Email, Djordje.Telefon, Djordje.Adresa, "123", false);
            Drzava SrbijaT = new Drzava("Srbija");
            Grad BP = new Grad("Backa Palanka", 15000, SrbijaT);
            Adresa adresaT = new Adresa("Vojvode Putnika", 1, BP);
            UlogovanKorisnik TaraP = new UlogovanKorisnik("Nikola", "Markovic", "2", "pacijent1", "pacijent1", "nikola123@gmail.com", "0645568131", adresaT);
            Pacijent p2 = new Pacijent(TaraP.Ime, TaraP.Prezime, TaraP.Jmbg, TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, "124",false);
            UlogovanKorisnik Tara = new UlogovanKorisnik("Nikolina", "Milanovic", "3", "pacijent2", "pacijent2", "nikolina123@gmail.com", "0645568131", adresaT);
            Pacijent p3 = new Pacijent(Tara.Ime, Tara.Prezime, Tara.Jmbg, Tara.KorisnickoIme, Tara.Lozinka, Tara.Email, Tara.Telefon, Tara.Adresa, "125", false);
            pacijenti.Add(p);
            pacijenti.Add(p2);
            pacijenti.Add(p3);
            var jsonToWrite = JsonConvert.SerializeObject(pacijenti, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter("../../../pacijenti.json"))
            {
                writer.Write(jsonToWrite);
            }


            return true;
        }
      
      public Pacijent Read()
      {
         throw new NotImplementedException();
      }

      public List<Pacijent> ReadAll()
      {
            String json = File.ReadAllText("../../../pacijenti.json");
            List<Pacijent> pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(json);
            return pacijenti;
        }
      
      public bool Update()
      {
         throw new NotImplementedException();
      }
      
      public bool Delete()
      {
         throw new NotImplementedException();
      }
   
   }
}