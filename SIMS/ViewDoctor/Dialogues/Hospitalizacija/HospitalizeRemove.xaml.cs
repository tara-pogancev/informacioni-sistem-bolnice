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
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.LekarGUI.Dialogues.Hospitalizacija
{
    /// <summary>
    /// Interaction logic for HospitalizeRemove.xaml
    /// </summary>
    public partial class HospitalizeRemove : Window
    {
        private Hospitalization hospitalization;
        private Patient patient;
        private HospitalizationController hospitalizationController = new HospitalizationController();
        private NotificationController notificationController = new NotificationController();

        public HospitalizeRemove(Hospitalization hospitalizationPar)
        {
            InitializeComponent();
            hospitalization = hospitalizationPar;
            hospitalization.InitData();
            patient = hospitalization.Patient;

            LabelDoctor.Content = "Doktor: " + hospitalization.LeadDoctor.FullName;
            LabelPatient.Content = "Pacijent: " + patient.FullName;
            LabelDate.Content = DateTime.Today.ToString("dd.MM.yyyy.");

        }

        private void CancelMessage(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AcceptMessage(object sender, RoutedEventArgs e)
        {
            DoRemoveHospitalization();
        }

        private void DoRemoveHospitalization()
        {
            if (NotificationTextBox.Text != "")
            {
                WriteNotification();
                hospitalizationController.DeleteHospitalization(hospitalization);
                DoctorUI.GetInstance().SellectedTab.Content = new PatientDocumentationView(patient);
                this.Close();
                MessageBox.Show("Otpisni list uspešno napisan! Pacijent je otpušten iz bolnice.");
            }
            else
                MessageBox.Show("Molimo popunite poruku otpisnog lista.");
        }

        private void WriteNotification()
        {
            List<String> target = new List<string>();
            target.Add(patient.Jmbg);
            String author = DoctorUI.GetInstance().GetUser().FullName;
            String content = "Otpisni list hospitalizacije: " + NotificationTextBox.Text;
            Notification notification = new Notification(author, DateTime.Now, content, target);
            notificationController.SaveNotification(notification);
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Return)
                DoRemoveHospitalization();
        }
    }
}
