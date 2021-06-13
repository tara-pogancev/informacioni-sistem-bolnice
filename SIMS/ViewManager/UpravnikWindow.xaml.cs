using SIMS.Model;
using SIMS.ViewManager;
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

        public Manager user { get; private set; }
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

        public void AddUser(Manager user)
        {
            this.user = user;
            UserLabel.Content = this.user.FullName;
        }

        private UpravnikWindow()
        {
            InitializeComponent();
            SetContent(new UpravnikWelcomePage());
            SetLabel("Početna");
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

        private void Izvestaj_Click(object sender, RoutedEventArgs e)
        {
            SetContent(new IzvestajPage());
            SetLabel("Izveštaj o zauzetosti prostorija");
        }

        private void Nalog_Click(object sender, RoutedEventArgs e)
        {
            SetContent(new NalogPage());
            SetLabel("Podaci o nalogu");
        }

        private void Tutorijal_Click(object sender, RoutedEventArgs e)
        {
            SetContent(new TutorialPage());
            SetLabel("Tutorijal za promenu količine inventara");
        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            SetContent(new LekoviPage());
            SetLabel("Lekovi");
        }

        private void Alergeni_Click(object sender, RoutedEventArgs e)
        {
            SetContent(new AlergeniPage());
            SetLabel("Alergeni");
        }

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                "Odjava", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new MainWindow().Show();
                this.Hide();
            }
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            SetContent(new FeedbackPage());
            SetLabel("Prijava grešaka i iskustava u vezi sa aplikacijom");
        }
    }

}
