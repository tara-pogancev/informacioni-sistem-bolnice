// File:    Drzava.cs
// Author:  paracelsus
// Created: 22 March 2021 20:19:14
// Purpose: Definition of Class Drzava

using System;

namespace P1
{
   public class Drzava
   {
      private String naziv;
      
      public System.Collections.Generic.List<Grad> grad;
      
      /// <summary>
      /// Property for collection of Grad
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
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
      
      /// <summary>
      /// Add a new Grad in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
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
      
      /// <summary>
      /// Remove an existing Grad from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
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
      
      /// <summary>
      /// Remove all instances of Grad from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
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

      public Drzava() { }

      public Drzava(String naziv)
        {
            this.naziv = naziv;
            this.grad = new System.Collections.Generic.List<Grad>();
        }

      public String Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }
   
   }
}