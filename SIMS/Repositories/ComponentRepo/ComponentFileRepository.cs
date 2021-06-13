using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.AllergenRepo
{
    class ComponentFileRepository : GenericFileRepository<string, Component, ComponentFileRepository>,IComponentRepository
    {
        protected override string getKey(Component entity)
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

        protected override void ShouldSerialize(Component entity)
        {
            entity.Serialize = true;
        }
    }
}
