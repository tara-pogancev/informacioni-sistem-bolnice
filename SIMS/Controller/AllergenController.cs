using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class AllergenController
    {
        AllergenService allergenService = new AllergenService();

        public void CreateOrUpdate(Allergen allergen)
        {
            allergenService.CreateOrUpdate(allergen);
        }

        public Allergen FindById(string ID)
        {
            return allergenService.FindById(ID);
        }

        public void Delete(string ID)
        {
            allergenService.Delete(ID);
        }

        public List<Allergen> GetAll()
        {
            return allergenService.GetAll();
        }
    }
}
