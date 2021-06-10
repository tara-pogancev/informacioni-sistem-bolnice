using SIMS.Controller;
using SIMS.ViewSecretary.Home;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.ViewSecretary.Feedback
{
    public partial class ViewFeedback : Page
    {
        private FeedbackController feedbackController = new FeedbackController();

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
            feedbackController.Send(feedbackTextBox.Text);
            SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
            NavigationService.Navigate(ViewHome.GetInstance());

        }
    }
}
