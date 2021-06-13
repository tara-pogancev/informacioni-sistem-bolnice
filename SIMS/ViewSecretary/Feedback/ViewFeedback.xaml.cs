using SIMS.Controller;
using SIMS.ViewSecretary.Home;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;

namespace SIMS.ViewSecretary.Feedback
{
    public partial class ViewFeedback : Page
    {
        private ApplicationFeedbackController feedbackController = new ApplicationFeedbackController();
        private Secretary secretary;
        private ApplicationFeedback feedback;

        public ViewFeedback(Secretary s)
        {
            InitializeComponent();

            secretary = s;
            feedback = new ApplicationFeedbackController().FindById(secretary.Jmbg);
            if (feedback != null)
            {
                feedbackTextBox.Text = feedback.Comment;
                ratingBar.Value = feedback.Grade;
            }
        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
            NavigationService.Navigate(ViewHome.GetInstance());
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            //TODO: ISPRAVITI!!!
            //ApplicationFeedback applicationFeedback = new ApplicationFeedback();
            //feedbackController.CreateOrUpdate(feedbackTextBox.Text);
            feedback = new ApplicationFeedback(feedbackTextBox.Text, secretary, ratingBar.Value);
            new ApplicationFeedbackController().CreateOrUpdate(feedback);
            SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
            NavigationService.Navigate(ViewHome.GetInstance());

        }
    }
}
