

using System;

namespace Model
{
   public class Drzava
   {
      private String naziv;
      
      public System.Collections.Generic.List<Grad> grad;

        public Drzava(string naziv)
        {
            this.naziv = naziv;
        }

        public System.Collections.Generic.List<Grad> Grad
      {
         get
         {
            if (grad == null)
               grad = new System.Collections.Generic.List<Grad>();
            return grad;
         }
         set
         {
            RemoveAllGrad();
            if (value != null)
            {
               foreach (Grad oGrad in value)
                  AddGrad(oGrad);
            }
         }
      }

        public string Naziv { get => naziv; set => naziv = value; }

        public void AddGrad(Grad newGrad)
      {
         if (newGrad == null)
            return;
         if (this.grad == null)
            this.grad = new System.Collections.Generic.List<Grad>();
         if (!this.grad.Contains(newGrad))
         {
            this.grad.Add(newGrad);
            newGrad.Drzava = this;
         }
      }
      
      
      public void RemoveGrad(Grad oldGrad)
      {
         if (oldGrad == null)
            return;
         if (this.grad != null)
            if (this.grad.Contains(oldGrad))
            {
               this.grad.Remove(oldGrad);
               oldGrad.Drzava = null;
            }
      }
      
      
      public void RemoveAllGrad()
      {
         if (grad != null)
         {
            System.Collections.ArrayList tmpGrad = new System.Collections.ArrayList();
            foreach (Grad oldGrad in grad)
               tmpGrad.Add(oldGrad);
            grad.Clear();
            foreach (Grad oldGrad in tmpGrad)
               oldGrad.Drzava = null;
            tmpGrad.Clear();
         }
      }
   
   }
}