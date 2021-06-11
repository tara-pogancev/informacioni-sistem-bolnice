using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SIMS.Controller;
using SIMS.Model;

namespace SIMS.ViewSecretary.Notifications
{
    public partial class ViewNotifications : Page
    {
        private Secretary _secretary;
        public ObservableCollection<Notification> _notifications { get; }
        private List<Patient> _patients;
        private List<Doctor> _doctors;
        private List<Secretary> _secretaries;
        private List<Manager> _directors;
        private ObservableCollection<NotificationRole> _rolesOfUsers;

        private NotificationController notificationController = new NotificationController();

        private const int NumberOfRoleGroups = 5;
        public ViewNotifications(Secretary secretary)
        {
            InitializeComponent();
            _secretary = secretary;

            PatientController patientController = new PatientController();
            DoctorController doctorController = new DoctorController();
            SecretaryController secretaryController = new SecretaryController();
            ManagerController managerController = new ManagerController();

            List<Notification> notificationsForReversing = notificationController.ReadByUser(secretary.Jmbg);

            notificationsForReversing.Reverse();

            _notifications = new ObservableCollection<Notification>(notificationsForReversing);
            _patients = patientController.GetAllPatients();
            _doctors = doctorController.GetAllDoctors();
            _secretaries = secretaryController.GetAllSecretaries();
            _directors = managerController.GetAllManagers();

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
                CustomMessageBox.Show(TranslationSource.Instance["SelectRoleMessage"]);
                return;
            }
            List<string> targets = CreateNotificationTargetsFromUserInput();


            Notification notification = new Notification("Sekretarijat", DateTime.Now, notificationTextBox.Text.Trim(), targets);
            notificationController.SaveNotification(notification);

            _notifications.Insert(0, notification);
            notificationViewer.ItemsSource = _notifications;

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
            notificationController.DeleteNotification(toDelete.ID);
            notificationViewer.ItemsSource = _notifications;
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
            this.NavigationService.Navigate(new UpdateNotification(_notifications, toUpdate, _secretary));
        }
    }
}
