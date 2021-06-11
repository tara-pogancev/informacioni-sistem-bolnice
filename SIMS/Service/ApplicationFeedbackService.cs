using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Repositories.FeedbackRepo;

namespace SIMS.Service
{
    class ApplicationFeedbackService
    {
        private IApplicationFeedbackRepository applicationFeedbackRepository = new ApplicationFeedbackFileRepository();

        public void CreateOrUpdate(ApplicationFeedback applicationFeedback)
        {
            applicationFeedbackRepository.CreateOrUpdate(applicationFeedback);
        }

        public ApplicationFeedback FindById(string ID)
        {
            return applicationFeedbackRepository.FindById(ID);
        }

        public void Delete(string ID)
        {
            applicationFeedbackRepository.Delete(ID);
        }

        public List<ApplicationFeedback> GetAll()
        {
            return applicationFeedbackRepository.GetAll();
        }
    }
}
