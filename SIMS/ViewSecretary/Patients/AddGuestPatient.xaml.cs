using SIMS.Repositories.SecretaryRepo;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;
using SIMS.Repositories.PatientRepo;

namespace SIMS.ViewSecretary.Patients
{
    public partial class AddGuestPatient : Page
    {
        public AddGuestPatient()
        {
            InitializeComponent();
        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text);
            PatientFileRepository.Instance.Save(patient);
            ViewPatients.GetInstance().RefreshView();

            NavigationService.GoBack();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
