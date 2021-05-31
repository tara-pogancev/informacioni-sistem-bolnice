using System.Windows;
using System.Windows.Controls;
using SIMS.Controller;
using SIMS.Model;

namespace SIMS.ViewSecretary.Patients
{
    public partial class AddGuestPatient : Page
    {
        private PatientController patientController = new PatientController();

        public AddGuestPatient()
        {
            InitializeComponent();
        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text);
            patientController.SavePatient(patient);
            ViewPatients.GetInstance().RefreshView();

            NavigationService.GoBack();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
