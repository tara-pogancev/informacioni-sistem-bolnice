using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;

namespace SIMS.SekretarGUI
{
    public partial class SekretarObavestenjaPage : Page
    {
        private Secretary _secretary;
        private ObservableCollection<Notification> _notifications;
        private List<Patient> _patients;
        private List<Doctor> _doctors;
        private List<Secretary> _secretaries;
        private List<Manager> _directors;
        ObservableCollection<NotificationRole> _rolesOfUsers;

        private const int NumberOfRoleGroups = 5;
        public SekretarObavestenjaPage(Secretary secretary)
        {
            InitializeComponent();
            _secretary = secretary;

            List<Notification> notificationsForReversing = NotificationFileRepository.Instance.ReadByUser(secretary.Jmbg);
            notificationsForReversing.Reverse();

            _notifications = new ObservableCollection<Notification>(notificationsForReversing);
            _patients = PatientFileRepository.Instance.GetAll();
            _doctors = DoctorFileRepository.Instance.GetAll();
            _secretaries = SecretaryFileRepository.Instance.GetAll();
            _directors = ManagerFileRepository.Instance.GetAll();

            notificationViewer.ItemsSource = _notifications;

            SetRolesForNotificationTargets();
        }

        private void SetRolesForNotificationTargets()
        {
            _rolesOfUsers = new ObservableCollection<NotificationRole>
            {
                new NotificationRole("Svi", 0),
                new NotificationRole("Svi pacijenti", 1),
                new NotificationRole("Svi lekari", 2),
                new NotificationRole("Svi sekretari", 3),
                new NotificationRole("Svi upravnici", 4)
            };
            SetRoleForEachUser(_rolesOfUsers);

            rolesComboBox.ItemsSource = _rolesOfUsers;
            rolesComboBox.DisplayMemberPath = "Uloga";
            rolesComboBox.SelectedMemberPath = "Indeks";
            rolesComboBox.SelectedItems.Add(_rolesOfUsers[0]);
        }

        private void SetRoleForEachUser(ObservableCollection<NotificationRole> rolesOfUsers)
        {
            int userNumber = NumberOfRoleGroups;
            foreach (Patient p in _patients)
            {
                rolesOfUsers.Add(new NotificationRole(p.FullName, userNumber, p.Jmbg));
                userNumber++;
            }
            foreach (Doctor d in _doctors)
            {
                rolesOfUsers.Add(new NotificationRole(d.FullName, userNumber, d.Jmbg));
                userNumber++;
            }
            foreach (Secretary s in _secretaries)
            {
                rolesOfUsers.Add(new NotificationRole(s.FullName, userNumber, s.Jmbg));
                userNumber++;
            }
            foreach (Manager d in _directors)
            {
                rolesOfUsers.Add(new NotificationRole(d.FullName, userNumber, d.Jmbg));
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


            Notification obavestenje = new Notification("Sekretarijat", DateTime.Now, notificationTextBox.Text.Trim(), targets);
            NotificationFileRepository.Instance.Save(obavestenje);

            _notifications.Insert(0, obavestenje);

            notificationTextBox.Text = "Ovde mozete uneti vase obavestenje.";
            rolesComboBox.SelectedItems.Clear();
            rolesComboBox.SelectedItems.Add(_rolesOfUsers[0]);
        }

        private List<string> CreateNotificationTargetsFromUserInput()
        {
            List<string> targets = new List<string>();

            foreach (Secretary s in _secretaries)
                targets.Add(s.Jmbg);
            foreach (NotificationRole ou in rolesComboBox.SelectedItems)
            {
                if (ou.Indeks == 0)
                    targets.Add("All");
                else if (ou.Indeks == 1)
                    foreach (Patient p in _patients)
                        targets.Add(p.Jmbg);
                else if (ou.Indeks == 2)
                    foreach (Doctor l in _doctors)
                        targets.Add(l.Jmbg);
                else if (ou.Indeks == 4)
                    foreach (Manager u in _directors)
                        targets.Add(u.Jmbg);
                else
                    targets.Add(ou.Key);
            }
            return targets;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Notification toDelete = null;
            foreach (Notification notification in _notifications)
            {
                if (notification.ID.Equals(button.CommandParameter))
                {
                    toDelete = notification;
                }
            }
            _notifications.Remove(toDelete);
            NotificationFileRepository.Instance.Delete(toDelete.ID);

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Notification toUpdate = null;
            foreach (Notification notification in _notifications)
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
