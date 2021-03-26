// File:    Termin.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:47:56 PM
// Purpose: Definition of Class Termin

using System;

namespace Model
{
   public class Termin
   {
         private DateTime pocetnoVreme;
         private TimeSpan vremeTrajanja;
         private TipTermina vrstaTermina;
      
         public Lekar lekar;
         public Pacijent pacijent;
         public Prostorija prostorija;

   }
}