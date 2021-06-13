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
        private IComponentRepository allergenRepostitory = new ComponentFileRepository();

        public void CreateOrUpdate(Component allergen)
        {
            allergenRepostitory.CreateOrUpdate(allergen);
        }

        public Component FindById(string ID)
        {
            return allergenRepostitory.FindById(ID);
        }

        public void Delete(string ID)
        {
            allergenRepostitory.Delete(ID);
        }

        public List<Component> GetAll()
        {
            return allergenRepostitory.GetAll();
        }
    }
}
