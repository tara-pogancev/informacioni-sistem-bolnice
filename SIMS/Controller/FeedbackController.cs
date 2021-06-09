using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class FeedbackController
    {
        FeedbackService feedbackService = new FeedbackService();

        public void Send(string message) => feedbackService.Send(message);
    }
}
