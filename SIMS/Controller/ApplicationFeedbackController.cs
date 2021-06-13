using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    class ApplicationFeedbackController
    {
        private ApplicationFeedbackService applicationFeedbackService = new ApplicationFeedbackService();

        public void CreateOrUpdate(ApplicationFeedback applicationFeedback)
        {
            applicationFeedbackService.CreateOrUpdate(applicationFeedback);
        }

        public ApplicationFeedback FindById(string ID)
        {
            return applicationFeedbackService.FindById(ID);
        }

        public void Delete(string ID)
        {
            applicationFeedbackService.Delete(ID);
        }

        public List<ApplicationFeedback> GetAll()
        {
            return applicationFeedbackService.GetAll();
        }
    }
}
