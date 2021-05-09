using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
    public partial class SekretarPacijentiPage : Page
    {
        private ObservableCollection<Pacijent> _patients;
        private static SekretarPacijentiPage _instance = null;

        public static SekretarPacijentiPage GetInstance()
        {
            if (_instance == null)
                _instance = new SekretarPacijentiPage();
            return _instance;
        }
        private SekretarPacijentiPage()
        {
            InitializeComponent();

            _patients = new ObservableCollection<Pacijent>(PacijentStorage.Instance.ReadList());
            patientsView.ItemsSource = _patients;
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DodajPacijentaPage());
        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DodajGostaPage());
        }

        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (patientsView.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati pacijenta za izmenu.", "Pacijent nije izabran");
            }
            else
            {
                NavigationService.Navigate(new IzmeniPacijentaPage((Pacijent)patientsView.SelectedItem));
            }
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (patientsView.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati pacijenta za brisanje.", "Pacijent nije izabran");
            }
            else
            {
                Pacijent toDelete = (Pacijent)patientsView.SelectedItem;
                PacijentStorage.Instance.Delete(toDelete.Jmbg);
                RefreshView();
            }
        }

        public void RefreshView()
        {
            _patients.Clear();
            List<Pacijent> pacijentiAll = PacijentStorage.Instance.ReadList();
            foreach (Pacijent p in pacijentiAll)
                _patients.Add(p);

        }

        public void RemoveInstance()
        {
            _instance = null;
        }
    }
}
