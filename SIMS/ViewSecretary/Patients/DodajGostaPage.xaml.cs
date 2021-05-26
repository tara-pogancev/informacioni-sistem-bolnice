using SIMS.Repositories.SecretaryRepo;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;

namespace SIMS.ViewSecretary
{
    public partial class DodajGostaPage : Page
    {
        public DodajGostaPage()
        {
            InitializeComponent();
        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text);
            PatientFileRepository.Instance.Save(patient);
            SekretarPacijentiPage.GetInstance().RefreshView();

            NavigationService.GoBack();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
