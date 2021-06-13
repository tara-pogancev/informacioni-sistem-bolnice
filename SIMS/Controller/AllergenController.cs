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

        public void CreateOrUpdate(Component allergen)
        {
            allergenService.CreateOrUpdate(allergen);
        }

        public Component GetAllergen(string ID)
        {
            return allergenService.FindById(ID);
        }

        public void Delete(string ID)
        {
            allergenService.Delete(ID);
        }

        public List<Component> GetAll()
        {
            return allergenService.GetAll();
        }
    }
}
