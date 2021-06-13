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
using SIMS.Repositories.RoomRepo;
using SIMS.Exceptions;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for RenoviranjePage.xaml
    /// </summary>
    public partial class RenoviranjePage : Page
    {
        private Room room;
        private RoomAvailibilityController roomAvailibilityController = new RoomAvailibilityController();
        private RoomController roomController = new RoomController();
        public RenoviranjePage(string BrojProstorije)
        {
            InitializeComponent();
            room = RoomFileRepository.Instance.FindById(BrojProstorije);
            RoomNumberLabel.Visibility = Visibility.Hidden;
            RoomNumberTextBox.Visibility = Visibility.Hidden;
            if (room.RenovationStart != null && room.RenovationEnd != null)
            {
                Pocetak.SelectedDate = room.RenovationStart;
                Kraj.SelectedDate = room.RenovationEnd;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ScheduleRenovation();
            }
            catch (RenovationAppointmentOverlapException)
            {
                MessageBox.Show("Postoje zakazani termini u datom intervalu.");
                return;
            }
            MergeRoomsSelection();
        }

        private void ScheduleRenovation()
        {
            if (Pocetak.SelectedDate != null && Kraj.SelectedDate != null)
            {
                room.RenovationStart = Pocetak.SelectedDate;
                room.RenovationEnd = Kraj.SelectedDate;
                roomAvailibilityController.Renovate(room);
            }
        }

        private void MergeRoomsSelection()
        {
            if ((bool)NotMerge.IsChecked)
            {
                UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
                UpravnikWindow.Instance.SetLabel("Prostorije");
            }

            else if ((bool)NewRoom.IsChecked)
            {
                UpravnikWindow.Instance.SetContent(new UpravnikProstorijaDetailPage(Pocetak.SelectedDate, Kraj.SelectedDate));
                UpravnikWindow.Instance.SetLabel("Nova prostorija nastala renoviranjem prostorije " + room.Number);
            }

            else if ((bool)MergeInto.IsChecked)
            {
                roomController.MergeRooms(RoomNumberTextBox.Text, room.Number);
                UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
                UpravnikWindow.Instance.SetLabel("Prostorije");
            }
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

        private void NotMerge_Click(object sender, RoutedEventArgs e)
        {
            RoomNumberLabel.Visibility = Visibility.Hidden;
            RoomNumberTextBox.Visibility = Visibility.Hidden;
        }

        private void MergeInto_Click(object sender, RoutedEventArgs e)
        {
            RoomNumberLabel.Visibility = Visibility.Visible;
            RoomNumberTextBox.Visibility = Visibility.Visible;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            RoomNumberLabel.Visibility = Visibility.Hidden;
            RoomNumberTextBox.Visibility = Visibility.Hidden;
        }
    }
}
