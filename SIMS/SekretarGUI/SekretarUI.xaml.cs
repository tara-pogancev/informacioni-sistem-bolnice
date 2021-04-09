using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;
using SIMS.SekretarGUI;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace SIMS
{

    public partial class SekretarUI : Window
    {
        private static SekretarUI instance = null;
        private Sekretar sekretar;

        public static SekretarUI GetInstance()
        {
            return instance;
        }

        public static SekretarUI GetInstance(Sekretar s)
        {
            if (instance == null)
                instance = new SekretarUI(s);
            return instance;
        }

        private SekretarUI(Sekretar s)
        {
            InitializeComponent();

            sekretar = s;
            UsernameLabel.Content = sekretar.ImePrezime;

            /*dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd/MM/yyyy");
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd/MM/yyyy");
            }, Dispatcher);*/

            MainFrame.Content = SekretarHomePage.GetInstance();
        }


        private void Button_Menu(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void Button_Notification(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Theme(object sender, RoutedEventArgs e)
        {
            ButtonTheme.Content = ButtonTheme.Content == FindResource("MoonImage") ? FindResource("SunImage") : FindResource("MoonImage");
        }

        private void Button_Language(object sender, RoutedEventArgs e)
        {
            ButtonLanguage.Content = (ButtonLanguage.Content.Equals("SR")) ? "EN" : "SR";
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Button_Log_Out(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            instance = null;

            if (SekretarHomePage.GetInstance() != null)
                SekretarHomePage.GetInstance().RemoveInstance();
            if (SekretarPacijentiPage.GetInstance() != null)
                SekretarPacijentiPage.GetInstance().RemoveInstance();

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
                default:
                    break;
            }
        }
    }
}
