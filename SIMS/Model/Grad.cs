// File:    Grad.cs
// Author:  paracelsus
// Created: 22 March 2021 20:19:13
// Purpose: Definition of Class Grad

using System;

namespace P1
{
   public class Grad
   {
      private String naziv;
      private int postanskiBroj;
      
      private Drzava drzava;
      
      /// <summary>
      /// Property for Drzava
      /// </summary>
      /// <pdGenerated>Default opposite class property</pdGenerated>
      public Drzava Drzava
      {
         get
         {
            return drzava;
         }
         set
         {
            if (this.drzava == null || !this.drzava.Equals(value))
            {
               if (this.drzava != null)
               {
                  Drzava oldDrzava = this.drzava;
                  this.drzava = null;
                  oldDrzava.RemoveGrad(this);
               }
               if (value != null)
               {
                  this.drzava = value;
                  this.drzava.AddGrad(this);
               }
            }
         }
      }

        public Grad(string naziv, int postanskiBroj, Drzava drzava)
        {
            this.naziv = naziv;
            this.postanskiBroj = postanskiBroj;
            this.drzava = drzava;
        }

        public String Naziv 
        {
            get { return naziv; }
            set { naziv = value; }
        }
        public int PostanskiBroj
        {
            get { return postanskiBroj; }
            set { postanskiBroj = value; }
        }
    }
}