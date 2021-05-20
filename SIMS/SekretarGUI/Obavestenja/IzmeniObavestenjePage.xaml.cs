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
        private Secretary _secretary;
        private ObservableCollection<Notification> _notifications;
        private Notification _notification;
        private List<Patient> _patients;
        private List<Doctor> _doctors;
        private List<Secretary> _secretaries;
        private List<Manager> _directors;

        private const int NumberOfRoleGroups = 5;
        public IzmeniObavestenjePage(ObservableCollection<Notification> notifications, Notification notification, Secretary secretary)
        {
            InitializeComponent();

            _secretary = secretary;
            _notifications = notifications;
            _notification = notification;

            _patients = PatientRepository.Instance.ReadEntities();
            _doctors = DoctorRepository.Instance.ReadEntities();
            _secretaries = SecretaryRepository.Instance.ReadEntities();
            _directors = ManagerRepository.Instance.ReadEntities();

            SetRolesForNotificationTargets();

            notificationViewer.ItemsSource = _notifications;
            obavestenjeTextBox.Text = _notification.Tekst;
        }

        private void SetRolesForNotificationTargets()
        {
            ObservableCollection<NotificationRole> rolesOfUsers = new ObservableCollection<NotificationRole>
            {
                new NotificationRole("Svi", 0),
                new NotificationRole("Svi pacijenti", 1),
                new NotificationRole("Svi lekari", 2),
                new NotificationRole("Svi sekretari", 3),
                new NotificationRole("Svi upravnici", 4)
            };
            SetRoleForEachUser(rolesOfUsers);

            UpdateRolesForEachUser(rolesOfUsers);
        }

        private void UpdateRolesForEachUser(ObservableCollection<NotificationRole> rolesOfUsers)
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
                foreach (NotificationRole role in rolesOfUsers)
                {
                    if (role.Indeks >= NumberOfRoleGroups && role.Key.Equals(key))
                    {
                        rolesComboBox.SelectedItems.Add(role);
                    }
                }
            }
        }

        private void SetRoleForEachUser(ObservableCollection<NotificationRole> rolesOfUsers)
        {
            int userNumber = NumberOfRoleGroups;
            foreach (Patient p in _patients)
            {
                rolesOfUsers.Add(new NotificationRole(p.ImePrezime, userNumber, p.Jmbg));
                userNumber++;
            }
            foreach (Doctor d in _doctors)
            {
                rolesOfUsers.Add(new NotificationRole(d.ImePrezime, userNumber, d.Jmbg));
                userNumber++;
            }
            foreach (Secretary s in _secretaries)
            {
                rolesOfUsers.Add(new NotificationRole(s.ImePrezime, userNumber, s.Jmbg));
                userNumber++;
            }
            foreach (Manager d in _directors)
            {
                rolesOfUsers.Add(new NotificationRole(d.ImePrezime, userNumber, d.Jmbg));
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
            NotificationRepository.Instance.DeleteEntity(_notification.ID);

            _notification = new Notification("Sekretarijat", DateTime.Now, obavestenjeTextBox.Text.Trim(), targets);
            NotificationRepository.Instance.CreateEntity(_notification);

            NavigationService.Navigate(new SekretarObavestenjaPage(_secretary));
        }

        private List<string> UpdateNotificationTargets()
        {
            List<string> targets = new List<string>();
            foreach (Secretary s in _secretaries)
                targets.Add(s.Jmbg);

            foreach (NotificationRole notificationRole in rolesComboBox.SelectedItems)
            {
                if (notificationRole.Indeks == 0)
                    targets.Add("All");
                else if (notificationRole.Indeks == 1)
                    foreach (Patient p in _patients)
                        targets.Add(p.Jmbg);
                else if (notificationRole.Indeks == 2)
                    foreach (Doctor l in _doctors)
                        targets.Add(l.Jmbg);
                else if (notificationRole.Indeks == 4)
                    foreach (Manager u in _directors)
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
