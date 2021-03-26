// File:    Adresa.cs
// Author:  paracelsus
// Created: 22 March 2021 20:04:56
// Purpose: Definition of Class Adresa

using System;

namespace P1
{
   public class Adresa
   {
      private String ulica;
      private int broj;
      
      private Grad grad;

        /// <summary>
        /// Property for Grad
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Grad Grad
      {
         get
         {
            return grad;
         }
         set
         {
            this.grad = value;
         }
      }

        public Adresa() { }

        public Adresa(String ulica,int broj,Grad grad)
        {
            this.ulica = ulica;
            this.broj = broj;
            this.grad = grad;
        }

        public String Ulica {
            get { return ulica; }
            set { ulica = value; }
        }

        public int Broj 
        {
            get { return broj; }
            set { broj = value; }
        }
   
   }
}