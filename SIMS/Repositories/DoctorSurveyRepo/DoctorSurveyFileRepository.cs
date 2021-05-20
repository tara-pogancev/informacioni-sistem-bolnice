using Model;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.DoctorRepo.DoctorSurveyRepo
{
    class DoctorSurveyFileRepository : GenericFileRepository<string, DoctorSurvey, DoctorSurveyRepository>,IDoctorSurveyRepository
    {
        public void Delete(string key)
        {
            this.DeleteEntity(key);
        }

        public DoctorSurvey FindById(string key)
        {
            return this.ReadEntity(key);
        }

        public IEnumerable<DoctorSurvey> GetAll()
        {
            return this.ReadEntities();
        }


        public void Save(DoctorSurvey entity)
        {
            this.CreateEntity(entity);
        }

        public void Update(DoctorSurvey entity)
        {
            this.UpdateEntity(entity);
        }

        protected override string getKey(DoctorSurvey entity)
        {
            return entity.IdAnkete;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\anketaLekara.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }
    }
}

