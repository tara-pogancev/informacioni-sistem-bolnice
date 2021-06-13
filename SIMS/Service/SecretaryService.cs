using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;

namespace SIMS.Service
{
    class SecretaryService
    {
        private ISecretaryRepository secretaryRepostitory = new SecretaryFileRepository();

        public SecretaryService()
        {

        }

        public List<Secretary> GetAllSecretaries()
        {
            return secretaryRepostitory.GetAll();
        }

        public void UpdateSecretary(Secretary secretary)
        {
            secretaryRepostitory.Update(secretary); 
        }
    }
}
