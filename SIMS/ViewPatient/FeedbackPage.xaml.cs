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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Controller;
using SIMS.Model;

namespace SIMS.ViewPatient
{
    /// <summary>
    /// Interaction logic for FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page
    {
        public ApplicationFeedback  Feedback{
            get;
            set;
        }

        private Patient patient;
        public FeedbackPage(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            Feedback = new ApplicationFeedbackController().FindById(patient.Jmbg);
            if (Feedback != null)
            {
                Komentar.Text = Feedback.Comment;
                BasicRatingBar.Value = Feedback.Grade;
            }

        }

        private void Posalji_OnClick(object sender, RoutedEventArgs e)
        {
            Feedback = new ApplicationFeedback(Komentar.Text, patient, BasicRatingBar.Value);
            new ApplicationFeedbackController().CreateOrUpdate(Feedback);
            NavigationService.GoBack();
        }

        private void Otkazi_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
