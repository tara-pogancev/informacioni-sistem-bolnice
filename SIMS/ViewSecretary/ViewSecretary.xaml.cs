using System.Windows;
using System.Windows.Controls;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using SIMS.Model;
using SIMS.ViewSecretary.Appointments;
using SIMS.ViewSecretary.Home;
using SIMS.ViewSecretary.Notifications;
using SIMS.ViewSecretary.Patients;

namespace SIMS.ViewSecretary
{
    public partial class ViewSecretary : Window
    {
        private static ViewSecretary _instance = null;
        private Secretary _secretary;

        public static ViewSecretary GetInstance(Secretary secretary)
        {
            if (_instance == null)
                _instance = new ViewSecretary(secretary);
            return _instance;
        }

        private ViewSecretary(Secretary secretary)
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
            ButtonTheme.Content = ButtonTheme.Content == FindResource("MoonImage") ? FindResource("SunImage") : FindResource("MoonImage");
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
    }
}
