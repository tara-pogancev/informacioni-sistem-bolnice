﻿
using Model;
using SIMS.LekarGUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace SIMS
{
    /// <summary>
    /// Glavni prozor za lekara
    /// </summary>
    public partial class LekarUI : Window
    {
        public static LekarUI instance;

        private static Lekar lekarUser;

        private WindowBar bar = new WindowBar();

        public static LekarUI GetInstance(Lekar l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarUI();
            }
            return instance;
        }

        public static LekarUI GetInstance()
        {
            return instance;
        }

        public LekarUI()
        {
            InitializeComponent();

            //Tred za prikazivanje sata i datuma

            this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd.MM.yyyy.");

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd.MM.yyyy.");
            }, this.Dispatcher);

            SellectedTab.Content = LekarDashboard.GetInstance(lekarUser);

            this.UsernameLabel.Content = lekarUser.ImePrezime;

            WindowBarFrame.Content = bar;

        }

        private void Button_Termini(object sender, RoutedEventArgs e)
        {
            //Button: Termini
            ChangeTab(1);
        }

        private void Button_Pacijenti(object sender, RoutedEventArgs e)
        {
            //Button: Pacijenti
            ChangeTab(2);
        }

        private void Button_Istorija(object sender, RoutedEventArgs e)
        {
            //Button: Istorija pregleda
            ChangeTab(3);
        }

        private void Button_Evidencija(object sender, RoutedEventArgs e)
        {
            //Button: Evidentiranje materijala
            ChangeTab(4);
        }

        private void Button_Nalog(object sender, RoutedEventArgs e)
        {
            //Button: Nalog, DEBUG po potrebi
            ChangeTab(5);

        }

        private void Button_Dashboard(object sender, MouseButtonEventArgs e)
        {
            //Dashboard
            ChangeTab(0);

        }

        public Lekar GetUser()
        {
            return lekarUser;
        }

        public void ChangeTab(int tabNum)
        {

            SolidColorBrush sellectedTab = new SolidColorBrush(Color.FromRgb(38, 46, 62));

            //Funkcija pomocu enumeracije menja tab
            switch (tabNum)
            {
                              

                case 0:
                    {
                        SellectedTab.Content = LekarDashboard.GetInstance(lekarUser);
                        ResetActiveButtons();
                        break;
                    }
                case 1:
                    {
                        SellectedTab.Content = LekarTerminiPage.GetInstance(lekarUser);
                        ResetActiveButtons();
                         B1.Fill = sellectedTab;
                        break;
                    }
                case 2:
                    {
                        SellectedTab.Content = LekarPacijentiPage.GetInstance(lekarUser);
                        ResetActiveButtons();
                        B2.Fill = sellectedTab;
                        break;
                    }
                case 3:
                    {
                        SellectedTab.Content = LekarIstorijaPage.GetInstance(lekarUser);
                        ResetActiveButtons();
                        B3.Fill = sellectedTab;
                        break;
                    }
                case 4:
                    {
                        SellectedTab.Content = LekarEvidencijaPage.GetInstance(lekarUser);
                        ResetActiveButtons();
                        B4.Fill = sellectedTab;
                        break;
                    }
                case 5:
                    {
                        SellectedTab.Content = LekarNalogPage.GetInstance(lekarUser);
                        ResetActiveButtons();
                        B5.Fill = sellectedTab;
                        break;
                    }

            }
        }

        public void ChangeWindowMinimize()
        {
            this.WindowState = WindowState.Minimized;
        }

        public void ChangeWindowClose()
        {
            this.Close();
        }

        public void ChangeWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        public WindowState GetWindowState()
        {
            return this.WindowState;
        }

        private void Button_LogOut(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                "Odjava", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new MainWindow().Show();
                this.Close();

                if (LekarEvidencijaPage.GetInstance() != null)
                    LekarEvidencijaPage.GetInstance().RemoveInstance();

                if (LekarPacijentiPage.GetInstance() != null)
                    LekarPacijentiPage.GetInstance().RemoveInstance();

                if (LekarIstorijaPage.GetInstance() != null)
                    LekarIstorijaPage.GetInstance().RemoveInstance();

                if (LekarNalogPage.GetInstance() != null)
                    LekarNalogPage.GetInstance().RemoveInstance();

                if (LekarTerminiPage.GetInstance() != null)
                    LekarTerminiPage.GetInstance().RemoveInstance();

                instance = null;

            }
        }

        private void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Normal;

            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Width = 1050;
            Application.Current.MainWindow.Height = 625;

            double height = SystemParameters.WorkArea.Height;
            double width = SystemParameters.WorkArea.Width;
            this.Top = (height - this.Height) / 2;
            this.Left = (width - this.Width) / 2;

            bar.ChangeMinimizeButton();
        }

        private void ResetActiveButtons()
        {
            //MenuGrid.Row[0].Cells[1].Style.BackColor = new SolidColorBrush(Colors.Green);

            B1.Fill = new SolidColorBrush(Colors.Transparent);
            B2.Fill = new SolidColorBrush(Colors.Transparent);
            B3.Fill = new SolidColorBrush(Colors.Transparent);
            B4.Fill = new SolidColorBrush(Colors.Transparent);
            B5.Fill = new SolidColorBrush(Colors.Transparent);


        }
    }
}
