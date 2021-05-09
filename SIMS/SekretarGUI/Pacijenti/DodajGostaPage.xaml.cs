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
            Pacijent patient = new Pacijent(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text);
            PacijentStorage.Instance.Create(patient);
            SekretarPacijentiPage.GetInstance().RefreshView();

            NavigationService.GoBack();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
