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
      
      public System.Collections.Generic.List<Adresa> adresa;
      
      
      /// Property for collection of Adresa
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.Generic.List<Adresa> Adresa
      {
         get
         {
            if (adresa == null)
               adresa = new System.Collections.Generic.List<Adresa>();
            return adresa;
         }
         set
         {
            RemoveAllAdresa();
            if (value != null)
            {
               foreach (Adresa oAdresa in value)
                  AddAdresa(oAdresa);
            }
         }
      }
      
      /// <summary>
      /// Add a new Adresa in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddAdresa(Adresa newAdresa)
      {
         if (newAdresa == null)
            return;
         if (this.adresa == null)
            this.adresa = new System.Collections.Generic.List<Adresa>();
         if (!this.adresa.Contains(newAdresa))
            this.adresa.Add(newAdresa);
      }
      
      /// <summary>
      /// Remove an existing Adresa from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveAdresa(Adresa oldAdresa)
      {
         if (oldAdresa == null)
            return;
         if (this.adresa != null)
            if (this.adresa.Contains(oldAdresa))
               this.adresa.Remove(oldAdresa);
      }
      
      /// <summary>
      /// Remove all instances of Adresa from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllAdresa()
      {
         if (adresa != null)
            adresa.Clear();
      }
   
   }
}