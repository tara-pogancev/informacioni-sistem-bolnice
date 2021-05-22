using SIMS.Repositories.SecretaryRepo;
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

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for RenoviranjePage.xaml
    /// </summary>
    public partial class RenoviranjePage : Page
    {
        Room prostorija;
        public RenoviranjePage(string BrojProstorije)
        {
            InitializeComponent();
            prostorija = RoomFileRepository.Instance.FindById(BrojProstorije);
            if (prostorija.RenovationStart != null && prostorija.RenovationEnd != null)
            {
                Pocetak.SelectedDate = prostorija.RenovationStart;
                Kraj.SelectedDate = prostorija.RenovationEnd;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Pocetak.SelectedDate != null && Kraj.SelectedDate != null)
            {
                prostorija.RenovationStart = Pocetak.SelectedDate;
                prostorija.RenovationEnd = Kraj.SelectedDate;
                RoomFileRepository.Instance.Update(prostorija);
            }
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }

        private void Otkaz_Click(object sender, RoutedEventArgs e)
        {
            prostorija.RenovationStart = null;
            prostorija.RenovationEnd = null;
            RoomFileRepository.Instance.Update(prostorija);
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }
    }
}
