using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS.Controller;

namespace SIMS.ViewDoctor.Dialogues.Izmena_naloga
{
    /// <summary>
    /// Interaction logic for RateApplicationDialog.xaml
    /// </summary>
    public partial class RateApplicationDialog : Window
    {
        private DoctorAppRatingController doctorAppRatingController = new DoctorAppRatingController();

        public RateApplicationDialog()
        {
            InitializeComponent();
        }

        private void CancelMessage(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AcceptMessage(object sender, RoutedEventArgs e)
        {
            Doctor doctor = DoctorUI.GetInstance().GetUser();
            int rating = BasicRatingBar.Value;
            String text = TextBox.Text;

            if (text.Equals("") || text.Equals("Unesite poruku ovde..."))
                MessageBox.Show("Molimo unesite povratnu poruku!");
            else
                SaveNewAppRating(doctor, rating, text);

        }

        private void SaveNewAppRating(Doctor doctor, int rating, string text)
        {
            DoctorAppRating appRating = new DoctorAppRating(doctor, text, rating);
            doctorAppRatingController.SaveRating(appRating);
            Close();
        }
    }
}
