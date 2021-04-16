using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikWindow.xaml
    /// </summary>
    public partial class UpravnikWindow : Window
    {

        private static UpravnikWindow _instance = new UpravnikWindow();
        public static UpravnikWindow Instance
        {
            get
            {
                return _instance;
            }
        }

        public void SetLabel(string label)
        {
            MainLabel.Content = label;
        }
        public void SetContent(Page page)
        {
            Sadrzaj.Content = page;
        }

        private UpravnikWindow()
        {
            InitializeComponent();
            SetContent(new UpravnikWelcomePage());
            SetLabel("Početna");
        }

        private void Zaposleni_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Inventar_Click(object sender, RoutedEventArgs e)
        {
            SetContent(new UpravnikOpremaPage());
            SetLabel("Inventar");
        }

        private void Prostorije_Click(object sender, RoutedEventArgs e)
        {
            SetContent(new UpravnikProstorijePage());
            SetLabel("Prostorije");
        }

        private void Usluge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nalog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tutorijal_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
