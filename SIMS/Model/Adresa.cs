
using System;

namespace Model
{
   public class Adresa
   {
      private String ulica;
      private string broj;
      public Grad grad;

        public Adresa(string ulica, string broj, Grad grad)
        {
            this.ulica = ulica;
            this.broj = broj;
            this.grad = grad;
        }

        public Adresa()
        {
        }

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

        public string Ulica { get => ulica; set => ulica = value; }
        public string Broj { get => broj; set => broj = value; }

        public override string ToString()
        {
            return ulica + " " + broj;
        }
    }
}