using System.Windows;
using System.Windows.Controls;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using SIMS.Model;
using SIMS.ViewSecretary.Appointments;
using SIMS.ViewSecretary.Home;
using SIMS.ViewSecretary.Notifications;
using SIMS.ViewSecretary.Patients;
using SIMS.ViewSecretary.Doctors;
using System.Windows.Media.Animation;
using SIMS.Filters;
using System;

namespace SIMS.ViewSecretary
{
    public partial class SecretaryUI : Window
    {
        private static SecretaryUI _instance = null;
        private Secretary _secretary;

        public static SecretaryUI GetInstance(Secretary secretary)
        {
            if (_instance == null)
                _instance = new SecretaryUI(secretary);
            return _instance;
        }

        private SecretaryUI(Secretary secretary)
        {
            InitializeComponent();

            _secretary = secretary;
            UsernameLabel.Content = _secretary.FullName;

            MainFrame.Content = ViewHome.GetInstance(_secretary);
        }


        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Width == 0)
            {
                Storyboard closeSearchStoryboard = Resources["OpenSearch"] as Storyboard;
                closeSearchStoryboard.Begin();
                SearchTextBox.Focus();
            }
            else if (SearchTextBox.Width == 150)
            {
                Storyboard closeSearchStoryboard = Resources["CloseSearch"] as Storyboard;
                closeSearchStoryboard.Begin();
            }
        }

        private void SearchTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (typeof(ViewPatients).IsInstanceOfType(MainFrame.Content))
            {
                ViewPatients viewPatients = (ViewPatients)MainFrame.Content;
                viewPatients.patientsView.ItemsSource = PatientsFilter.Instance.ApplyFilters(viewPatients._patients, SearchTextBox.Text, true);
            }
            else if (typeof(ViewAppointments).IsInstanceOfType(MainFrame.Content))
            {
                ViewAppointments viewAppointments = (ViewAppointments)MainFrame.Content;
                viewAppointments.appointmentsView.ItemsSource = AppointmentsFilter.Instance.ApplyFilters(viewAppointments._appointmentsForView, SearchTextBox.Text, true);
            }
            else if (typeof(ViewNotifications).IsInstanceOfType(MainFrame.Content))
            {
                ViewNotifications viewNotifications = (ViewNotifications)MainFrame.Content;
                viewNotifications.notificationViewer.ItemsSource = NotificationsFilter.Instance.ApplyFilters(viewNotifications._notifications, SearchTextBox.Text, true);
            }
            else if (typeof(UpdateNotification).IsInstanceOfType(MainFrame.Content))
            {
                UpdateNotification updateNotification = (UpdateNotification)MainFrame.Content;
                updateNotification.notificationViewer.ItemsSource = NotificationsFilter.Instance.ApplyFilters(updateNotification._notifications, SearchTextBox.Text, true);
            }

        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            if (GridMenu.Width == 320)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(ButtonCloseMenu);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;

                invokeProv.Invoke();
            }

            MainFrame.Content = new ViewNotifications(_secretary);
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;

            if (ButtonTheme.Content == FindResource("MoonImage"))
            {
                app.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
                ButtonTheme.Content = FindResource("SunImage");
            }
            else
            {
                app.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
                ButtonTheme.Content = FindResource("MoonImage");
            }
        }

        private void Language_Click(object sender, RoutedEventArgs e)
        {
            ButtonLanguage.Content = (ButtonLanguage.Content.Equals("SR")) ? "EN" : "SR";
        }

        private void CloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            _instance = null;

            if (ViewHome.GetInstance() != null)
                ViewHome.GetInstance().RemoveInstance();
            if (ViewPatients.GetInstance() != null)
                ViewPatients.GetInstance().RemoveInstance();
            if (ViewAppointments.GetInstance() != null)
                ViewAppointments.GetInstance().RemoveInstance();

            this.Close();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((ListViewItem)((ListView)sender).SelectedItem is null)
                return;

            ButtonAutomationPeer peer = new ButtonAutomationPeer(ButtonCloseMenu);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    invokeProv.Invoke();
                    MainFrame.Content = ViewHome.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemPatients":
                    invokeProv.Invoke();
                    MainFrame.Content = ViewPatients.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemDoctors":
                    invokeProv.Invoke();
                    MainFrame.Content = new DoctorVacations();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemAppointments":
                    invokeProv.Invoke();
                    MainFrame.Content = ViewAppointments.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemReport":
                    invokeProv.Invoke();
                    MainFrame.Content = ViewHome.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemAccount":
                    invokeProv.Invoke();
                    MainFrame.Content = ViewHome.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
            }
        }

        private void MainFrame_PageChanged(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (SearchTextBox.Width == 150)
            {
                Storyboard closeSearchStoryboard = Resources["CloseSearch"] as Storyboard;
                closeSearchStoryboard.Begin();
            }
            if (typeof(ViewPatients).IsInstanceOfType(MainFrame.Content))
            {
                ButtonSearch.Visibility = Visibility.Visible;
                SearchTextBox.Text = "";
                ViewPatients viewPatients = (ViewPatients)MainFrame.Content;
                viewPatients.patientsView.ItemsSource = PatientsFilter.Instance.ApplyFilters(viewPatients._patients, SearchTextBox.Text, true);
            }
            else if (typeof(ViewAppointments).IsInstanceOfType(MainFrame.Content))
            {
                ButtonSearch.Visibility = Visibility.Visible;
                SearchTextBox.Text = "";
                ViewAppointments viewAppointments = (ViewAppointments)MainFrame.Content;
                viewAppointments.appointmentsView.ItemsSource = AppointmentsFilter.Instance.ApplyFilters(viewAppointments._appointmentsForView, SearchTextBox.Text, true);
            }
            else if (typeof(ViewNotifications).IsInstanceOfType(MainFrame.Content))
            {
                ButtonSearch.Visibility = Visibility.Visible;
                SearchTextBox.Text = "";
                ViewNotifications viewNotifications = (ViewNotifications)MainFrame.Content;
                viewNotifications.notificationViewer.ItemsSource = NotificationsFilter.Instance.ApplyFilters(viewNotifications._notifications, SearchTextBox.Text, true);
            }
            else if (typeof(UpdateNotification).IsInstanceOfType(MainFrame.Content))
            {
                ButtonSearch.Visibility = Visibility.Visible;
                SearchTextBox.Text = "";
                UpdateNotification updateNotification = (UpdateNotification)MainFrame.Content;
                updateNotification.notificationViewer.ItemsSource = NotificationsFilter.Instance.ApplyFilters(updateNotification._notifications, SearchTextBox.Text, true);
            }
            else
            {
                ButtonSearch.Visibility = Visibility.Hidden;
            }
        }
    }
}
