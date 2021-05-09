using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
    public partial class SekretarObavestenjaPage : Page
    {
        private Sekretar _secretary;
        private ObservableCollection<Obavestenje> _notifications;
        private List<Pacijent> _patients;
        private List<Lekar> _doctors;
        private List<Sekretar> _secretaries;
        private List<Upravnik> _directors;
        ObservableCollection<ObavestenjeUloga> _rolesOfUsers;

        private const int NumberOfRoleGroups = 5;
        public SekretarObavestenjaPage(Sekretar secretary)
        {
            InitializeComponent();
            _secretary = secretary;

            List<Obavestenje> notificationsForReversing = ObavestenjaStorage.Instance.ReadByUser(secretary.Jmbg);
            notificationsForReversing.Reverse();

            _notifications = new ObservableCollection<Obavestenje>(notificationsForReversing);
            _patients = PacijentStorage.Instance.ReadList();
            _doctors = LekarStorage.Instance.ReadList();
            _secretaries = SekretarStorage.Instance.ReadList();
            _directors = UpravnikStorage.Instance.ReadList();

            notificationViewer.ItemsSource = _notifications;

            SetRolesForNotificationTargets();
        }

        private void SetRolesForNotificationTargets()
        {
            _rolesOfUsers = new ObservableCollection<ObavestenjeUloga>
            {
                new ObavestenjeUloga("Svi", 0),
                new ObavestenjeUloga("Svi pacijenti", 1),
                new ObavestenjeUloga("Svi lekari", 2),
                new ObavestenjeUloga("Svi sekretari", 3),
                new ObavestenjeUloga("Svi upravnici", 4)
            };
            SetRoleForEachUser(_rolesOfUsers);

            rolesComboBox.ItemsSource = _rolesOfUsers;
            rolesComboBox.DisplayMemberPath = "Uloga";
            rolesComboBox.SelectedMemberPath = "Indeks";
            rolesComboBox.SelectedItems.Add(_rolesOfUsers[0]);
        }

        private void SetRoleForEachUser(ObservableCollection<ObavestenjeUloga> rolesOfUsers)
        {
            int userNumber = NumberOfRoleGroups;
            foreach (Pacijent p in _patients)
            {
                rolesOfUsers.Add(new ObavestenjeUloga(p.ImePrezime, userNumber, p.Jmbg));
                userNumber++;
            }
            foreach (Lekar d in _doctors)
            {
                rolesOfUsers.Add(new ObavestenjeUloga(d.ImePrezime, userNumber, d.Jmbg));
                userNumber++;
            }
            foreach (Sekretar s in _secretaries)
            {
                rolesOfUsers.Add(new ObavestenjeUloga(s.ImePrezime, userNumber, s.Jmbg));
                userNumber++;
            }
            foreach (Upravnik d in _directors)
            {
                rolesOfUsers.Add(new ObavestenjeUloga(d.ImePrezime, userNumber, d.Jmbg));
                userNumber++;
            }
        }

        private void AddNotification_Click(object sender, RoutedEventArgs e)
        {
            if (rolesComboBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Oznacite bar jednu ulogu.", "Nema uloge");
                return;
            }
            List<string> targets = CreateNotificationTargetsFromUserInput();


            Obavestenje obavestenje = new Obavestenje("Sekretarijat", DateTime.Now, notificationTextBox.Text.Trim(), targets);
            ObavestenjaStorage.Instance.Create(obavestenje);

            _notifications.Insert(0, obavestenje);

            notificationTextBox.Text = "Ovde mozete uneti vase obavestenje.";
            rolesComboBox.SelectedItems.Clear();
            rolesComboBox.SelectedItems.Add(_rolesOfUsers[0]);
        }

        private List<string> CreateNotificationTargetsFromUserInput()
        {
            List<string> targets = new List<string>();

            foreach (Sekretar s in _secretaries)
                targets.Add(s.Jmbg);
            foreach (ObavestenjeUloga ou in rolesComboBox.SelectedItems)
            {
                if (ou.Indeks == 0)
                    targets.Add("All");
                else if (ou.Indeks == 1)
                    foreach (Pacijent p in _patients)
                        targets.Add(p.Jmbg);
                else if (ou.Indeks == 2)
                    foreach (Lekar l in _doctors)
                        targets.Add(l.Jmbg);
                else if (ou.Indeks == 4)
                    foreach (Upravnik u in _directors)
                        targets.Add(u.Jmbg);
                else
                    targets.Add(ou.Key);
            }
            return targets;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Obavestenje toDelete = null;
            foreach (Obavestenje notification in _notifications)
            {
                if (notification.ID.Equals(button.CommandParameter))
                {
                    toDelete = notification;
                }
            }
            _notifications.Remove(toDelete);
            ObavestenjaStorage.Instance.Delete(toDelete.ID);

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Obavestenje toUpdate = null;
            foreach (Obavestenje notification in _notifications)
            {
                if (notification.ID.Equals(button.CommandParameter))
                {
                    toUpdate = notification;
                }
            }
            this.NavigationService.Navigate(new IzmeniObavestenjePage(_notifications, toUpdate, _secretary));
        }
    }
}
