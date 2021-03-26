// File:    KorisnikStorage.cs
// Author:  paracelsus
// Created: 22 March 2021 19:17:45
// Purpose: Definition of Class KorisnikStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace P1
{
   public class KorisnikStorage
   {
      public bool Create()
      {
            Drzava Bih = new Drzava("Bosna i Hercegovina");
            Grad Foca = new Grad("Foca", 73300, Bih);
            Adresa adresaDj = new Adresa("Barakovac", 1, Foca);
 
            UlogovanKorisnik Djordje = new UlogovanKorisnik("Djordje", "Krsmanovic", "1234567891021", "pacijent", "pacijent", "djordje1499@gmail.com", "0645568131", adresaDj);

            List<UlogovanKorisnik> korisnici = new List<UlogovanKorisnik>();
            korisnici.Add(Djordje);
            Drzava SrbijaT = new Drzava("Srbija");
            Grad BP = new Grad("Backa Palanka", 15000, SrbijaT);
            Adresa adresaT = new Adresa("Vojvode Putnika", 1, BP);

            UlogovanKorisnik TaraP = new UlogovanKorisnik("Tara", "Pogancev", "1234567891021", "doktor", "doktor", "tara123@gmail.com", "0645568131", adresaDj);
            korisnici.Add(TaraP);

            
            Grad Sombor = new Grad("Sombor", 25000, SrbijaT);
            Adresa adresaN = new Adresa("Sombroska", 1, Sombor);

            UlogovanKorisnik Nikola= new UlogovanKorisnik("Nikola", "Jovisic", "1234567891021", "upravnik", "upravnik", "nikola123@gmail.com", "0645568131", adresaN);
            
            korisnici.Add(Nikola);

            Grad Becej = new Grad("Novi Becej", 22000, SrbijaT);
            Adresa adresaM = new Adresa("Becejska", 1, Becej);

            UlogovanKorisnik Milos= new UlogovanKorisnik("Milos", "Zivic", "1234567891021", "sekretar", "sekretar", "milos123@gmail.com", "0645568131", adresaM);

           

            korisnici.Add(Milos);

            List<UlogovanKorisnik> korisnici1 = new List<UlogovanKorisnik>();
             var jsonToWrite = JsonConvert.SerializeObject(korisnici, Formatting.Indented);
             using (StreamWriter writer = new StreamWriter(@"C:\Users\Djordje\Desktop\SIMS proba\ProbaProjekat\users.json"))
             {
                 writer.Write(jsonToWrite);
             }
            
            string json = JsonConvert.SerializeObject(korisnici, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            File.WriteAllText(@"C:\Users\Djordje\Desktop\SIMS proba\ProbaProjekat\user1.json", json);
            string adresa = JsonConvert.SerializeObject(adresaDj, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            File.WriteAllText(@"C:\Users\Djordje\Desktop\SIMS proba\ProbaProjekat\adresadj.json", adresa);
            string procitano = File.ReadAllText(@"C:\Users\Djordje\Desktop\SIMS proba\ProbaProjekat\user1.json");
            
            List < UlogovanKorisnik > deserializedPeople = JsonConvert.DeserializeObject<List<UlogovanKorisnik>>(procitano,new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            string jsonFile = File.ReadAllText(@"C:\Users\Djordje\Desktop\SIMS proba\ProbaProjekat\users.json");
            var playerList = JsonConvert.DeserializeObject<List<UlogovanKorisnik>>(jsonFile);
            bool equal = Object.ReferenceEquals(deserializedPeople[0].adresa,deserializedPeople[1].adresa);
            bool equal1 = Object.ReferenceEquals(playerList[0].adresa, playerList[1].adresa);
            return true;
      }
      
      public UlogovanKorisnik Read()
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

        public KorisnikStorage() { }
   
   }
}