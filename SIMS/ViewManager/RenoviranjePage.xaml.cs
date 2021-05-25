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
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for RenoviranjePage.xaml
    /// </summary>
    public partial class RenoviranjePage : Page
    {
        private Room room;
        private RoomController roomController = new RoomController();
        public RenoviranjePage(string BrojProstorije)
        {
            InitializeComponent();
            room = RoomFileRepository.Instance.FindById(BrojProstorije);
            if (room.RenovationStart != null && room.RenovationEnd != null)
            {
                Pocetak.SelectedDate = room.RenovationStart;
                Kraj.SelectedDate = room.RenovationEnd;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Pocetak.SelectedDate != null && Kraj.SelectedDate != null)
            {
                room.RenovationStart = Pocetak.SelectedDate;
                room.RenovationEnd = Kraj.SelectedDate;
                roomController.Update(room);
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
            room.RenovationStart = null;
            room.RenovationEnd = null;
            roomController.Update(room);
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }
    }
}
