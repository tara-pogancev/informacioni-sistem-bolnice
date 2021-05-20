using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AllergenRepo
{
    class AllergenFileRepository:GenericFileRepository<string, Allergen, AllergenRepository>,IAllergenRepository
    {
        public void Delete(string key)
        {
            this.DeleteEntity(key);
        }

        public Allergen FindById(string key)
        {
            return this.ReadEntity(key);
        }

        public IEnumerable<Allergen> GetAll()
        {
            return this.ReadEntities();
        }


        public void Save(Allergen entity)
        {
            this.CreateEntity(entity);
        }

        public void Update(Allergen entity)
        {
            this.UpdateEntity(entity);
        }

        protected override string getKey(Allergen entity)
        {
            return entity.ID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\alergeni.json";
        }

        protected override void RemoveReferences(string key)
        {
        }
    }
    
}
