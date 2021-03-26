// File:    Grad.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 8:19:13 PM
// Purpose: Definition of Class Grad

using System;

namespace Model
{
   public class Grad
   {
      private String naziv;
      private int postanskiBroj;
      
      public Drzava drzava;
      
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
   
   }
}