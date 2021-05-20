using Model;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
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
            PatientRepository.Instance.CreateEntity(patient);
            SekretarPacijentiPage.GetInstance().RefreshView();

            NavigationService.GoBack();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
