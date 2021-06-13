using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Controller;
using SIMS.Model;
using SIMS.UpravnikGUI;

namespace SIMS.ViewManager
{
    /// <summary>
    /// Interaction logic for FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page
    {

        private ApplicationFeedbackController appRatingController = new ApplicationFeedbackController();
        public FeedbackPage()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AcceptRatingAction();
        }

        private void AcceptRatingAction()
        {
            Manager manager = UpravnikWindow.Instance.user;
            int rating = BasicRatingBar.Value;
            String text = TextBox.Text;

            if (ValidateForm(text))
                System.Windows.MessageBox.Show("Molimo unesite povratnu poruku!");
            else
                SaveNewAppRating(manager, rating, text);
        }

        private static bool ValidateForm(string text)
        {
            return text.Equals("") || text.Equals("Unesite poruku ovde...");
        }

        private void SaveNewAppRating(Manager manager, int rating, string text)
        {
            ApplicationFeedback appRating = new ApplicationFeedback(text, manager, rating);
            appRatingController.CreateOrUpdate(appRating);
            System.Windows.MessageBox.Show("Hvala vam na ostavljenoj recenziji aplikacije!");
        }

    }
}
