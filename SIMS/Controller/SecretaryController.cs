using SIMS.Model;
using SIMS.Service;
using System.Collections.Generic;

namespace SIMS.Controller
{
    class SecretaryController
    {
        private SecretaryService secretaryService  = new SecretaryService();

        public SecretaryController()
        {

        }

        public List<Secretary> GetAllSecretaries()
        {
            return secretaryService.GetAllSecretaries();
        }

        public void UpdateSecretary(Secretary secretary)
        {
            secretaryService.UpdateSecretary(secretary);
        }
    }
}
