using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AnamnesisRepository
{
    class AnamnesisFileRepository : GenericFileRepository<string, Anamnesis, AnamnesisFileRepository>, IAnamnesisRepository
    {
        public void Delete(string key)
        {
            this.DeleteEntity(key);
        }

        public Anamnesis FindById(string key)
        {
            return this.ReadEntity(key);
        }

        public IEnumerable<Anamnesis> GetAll()
        {
            return this.ReadEntities();
        }


        public void Save(Anamnesis entity)
        {
            this.CreateEntity(entity);
        }

        public void Update(Anamnesis entity)
        {
            this.UpdateEntity(entity);
        }

        protected override string getKey(Anamnesis entity)
        {
            return entity.IdAnamneze;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\anamneze.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }
    }
}
