using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AnamnesisRepository
{
    class AnamnesisFileRepository : GenericFileRepository<string, Anamnesis, AnamnesisFileRepository>, IAnamnesisRepository
    {
        

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
