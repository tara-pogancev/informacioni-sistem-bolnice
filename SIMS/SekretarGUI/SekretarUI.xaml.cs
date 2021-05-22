using SIMS.Repositories.SecretaryRepo;
using System.Windows;
using System.Windows.Controls;
using SIMS.SekretarGUI;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace SIMS
{
    public partial class SekretarUI : Window
    {
        private static SekretarUI _instance = null;
        private Secretary _secretary;

        public static SekretarUI GetInstance(Secretary secretary)
        {
            if (_instance == null)
                _instance = new SekretarUI(secretary);
            return _instance;
        }

        private SekretarUI(Secretary secretary)
        {
            InitializeComponent();

            _secretary = secretary;
            UsernameLabel.Content = _secretary.ImePrezime;

            MainFrame.Content = SekretarHomePage.GetInstance(_secretary);
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

            MainFrame.Content = new SekretarObavestenjaPage(_secretary);
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

            if (SekretarHomePage.GetInstance() != null)
                SekretarHomePage.GetInstance().RemoveInstance();
            if (SekretarPacijentiPage.GetInstance() != null)
                SekretarPacijentiPage.GetInstance().RemoveInstance();
            if (SekretarTerminiPage.GetInstance() != null)
                SekretarTerminiPage.GetInstance().RemoveInstance();

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
                    MainFrame.Content = SekretarHomePage.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemPatients":
                    invokeProv.Invoke();
                    MainFrame.Content = SekretarPacijentiPage.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemAppointments":
                    invokeProv.Invoke();
                    MainFrame.Content = SekretarTerminiPage.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemReport":
                    invokeProv.Invoke();
                    MainFrame.Content = SekretarHomePage.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
                case "ItemAccount":
                    invokeProv.Invoke();
                    MainFrame.Content = SekretarHomePage.GetInstance();
                    ListViewMenu.SelectedItem = null;
                    break;
            }
        }
    }
}
