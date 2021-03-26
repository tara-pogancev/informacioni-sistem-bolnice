// File:    PacijentStorage.cs
// Author:  paracelsus
// Created: 22 March 2021 18:56:08
// Purpose: Definition of Class PacijentStorage

using System;

namespace P1
{
   public class PacijentStorage
   {
      public bool Create()
      {
         throw new NotImplementedException();

            Drzava Bih = new Drzava("Bosna i Hercegovina");
            Grad Foca = new Grad("Foca", 73300, Bih);
            Adresa adresaDj = new Adresa("Barakovac", 1, Foca);
            UlogovanKorisnik Djordje = new UlogovanKorisnik("Djordje", "Krsmanovic", "1234567891021", "pacijent", "pacijent", "djordje1499@gmail.com", "0645568131", adresaDj);
            Pacijent p = new Pacijent(Djordje.Ime, Djordje.Prezime, Djordje.JMBG, Djordje.KorisnickoIme, Djordje.Lozinka, Djordje.Email, Djordje.Telefon, Djordje.Adresa, "123", false, 1); 

        }
      
      public Pacijent Read()
      {
         throw new NotImplementedException();
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