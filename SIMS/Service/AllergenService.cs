using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.AllergenRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class AllergenService
    {
        private IAllergenRepository allergenRepostitory = new AllergenFileRepository();
        public void CreateOrUpdate(Allergen allergen)
        {
            allergenRepostitory.CreateOrUpdate(allergen);
        }

        public Allergen FindById(string ID)
        {
            return allergenRepostitory.FindById(ID);
        }

        public void Delete(string ID)
        {
            allergenRepostitory.Delete(ID);
        }

        public List<Allergen> GetAll()
        {
            return allergenRepostitory.GetAll();
        }
    }
}
