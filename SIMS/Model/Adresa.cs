using System;

namespace Model
{
   public class Adresa
   {
      private String ulica;
      private int broj;
      
      public Grad grad;
      
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
   
   }
}