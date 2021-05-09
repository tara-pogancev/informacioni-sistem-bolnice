using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMS.SekretarGUI
{
    public partial class IzmeniObavestenjePage : Page
    {
        private Sekretar _secretary;
        private ObservableCollection<Obavestenje> _notifications;
        private Obavestenje _notification;
        private List<Pacijent> _patients;
        private List<Lekar> _doctors;
        private List<Sekretar> _secretaries;
        private List<Upravnik> _directors;

        private const int NumberOfRoleGroups = 5;
        public IzmeniObavestenjePage(ObservableCollection<Obavestenje> notifications, Obavestenje notification, Sekretar secretary)
        {
            InitializeComponent();

            _secretary = secretary;
            _notifications = notifications;
            _notification = notification;

            _patients = PacijentStorage.Instance.ReadList();
            _doctors = LekarStorage.Instance.ReadList();
            _secretaries = SekretarStorage.Instance.ReadList();
            _directors = UpravnikStorage.Instance.ReadList();

            SetRolesForNotificationTargets();

            notificationViewer.ItemsSource = _notifications;
            obavestenjeTextBox.Text = _notification.Tekst;
        }

        private void SetRolesForNotificationTargets()
        {
            ObservableCollection<ObavestenjeUloga> rolesOfUsers = new ObservableCollection<ObavestenjeUloga>
            {
                new ObavestenjeUloga("Svi", 0),
                new ObavestenjeUloga("Svi pacijenti", 1),
                new ObavestenjeUloga("Svi lekari", 2),
                new ObavestenjeUloga("Svi sekretari", 3),
                new ObavestenjeUloga("Svi upravnici", 4)
            };
            SetRoleForEachUser(rolesOfUsers);

            UpdateRolesForEachUser(rolesOfUsers);
        }

        private void UpdateRolesForEachUser(ObservableCollection<ObavestenjeUloga> rolesOfUsers)
        {
            rolesComboBox.ItemsSource = rolesOfUsers;
            rolesComboBox.DisplayMemberPath = "Uloga";
            rolesComboBox.SelectedMemberPath = "Indeks";
            foreach (string key in _notification.Target)
            {
                if (key.Equals("All"))
                {
                    rolesComboBox.SelectedItems.Add(rolesOfUsers[0]);
                    break;
                }
                foreach (ObavestenjeUloga role in rolesOfUsers)
                {
                    if (role.Indeks >= NumberOfRoleGroups && role.Key.Equals(key))
                    {
                        rolesComboBox.SelectedItems.Add(role);
                    }
                }
            }
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (rolesComboBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Oznacite bar jednu ulogu.", "Nema uloge");
                return;
            }
            List<string> targets = UpdateNotificationTargets();
            
            _notifications.Remove(_notification);
            ObavestenjaStorage.Instance.Delete(_notification.ID);

            _notification = new Obavestenje("Sekretarijat", DateTime.Now, obavestenjeTextBox.Text.Trim(), targets);
            ObavestenjaStorage.Instance.Create(_notification);

            NavigationService.Navigate(new SekretarObavestenjaPage(_secretary));
        }

        private List<string> UpdateNotificationTargets()
        {
            List<string> targets = new List<string>();
            foreach (Sekretar s in _secretaries)
                targets.Add(s.Jmbg);

            foreach (ObavestenjeUloga notificationRole in rolesComboBox.SelectedItems)
            {
                if (notificationRole.Indeks == 0)
                    targets.Add("All");
                else if (notificationRole.Indeks == 1)
                    foreach (Pacijent p in _patients)
                        targets.Add(p.Jmbg);
                else if (notificationRole.Indeks == 2)
                    foreach (Lekar l in _doctors)
                        targets.Add(l.Jmbg);
                else if (notificationRole.Indeks == 4)
                    foreach (Upravnik u in _directors)
                        targets.Add(u.Jmbg);
                else
                    targets.Add(notificationRole.Key);
            }
            return targets;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SekretarObavestenjaPage(_secretary));
        }
    }
}
