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

        public ViewFeedback()
        {
            InitializeComponent();
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
            SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
            NavigationService.Navigate(ViewHome.GetInstance());

        }
    }
}
