using Model;
using SIMS.UpravnikGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Login()
        {
            String user = username.Text;
            String pass = password.Password;

            //impelemntacija za pacijenta
            List<Pacijent> pacijenti = new List<Pacijent>();
            PacijentStorage pc = new PacijentStorage();
            pacijenti = pc.ReadAll();

            foreach (Pacijent pac in pacijenti)
            {
                if (pac.KorisnickoIme.Equals(user) && pac.Lozinka.Equals(pass))
                {
                    PacijentUI pacijent = new PacijentUI(pac);
                    pacijent.Show();
                    this.Close();
                    return;
                }
            }


            //impelemntacija za upravnika
            Upravnik upravnik = UpravnikStorage.Instance.Read(user);
            if (upravnik != null && pass.Equals(upravnik.Lozinka))
            {
                UpravnikUI upravnikUI = new UpravnikUI();
                upravnikUI.Show();
                this.Close();
                return;
            }

            //impelementacija za doktora
            Lekar lekar = LekarStorage.Read(user);
            if (lekar != null && pass.Equals(lekar.Lozinka) || username.Text == "l")
            {

                //Precica
                if (username.Text == "l")
                    lekar = LekarStorage.Read("doktor");

                LekarUI lekarUI = LekarUI.getInstance(lekar);
                lekarUI.Show();
                this.Close();
                return;
            }

            //implementacija za sekretara
            List<Model.Sekretar> sekretari = new List<Model.Sekretar>();
            SekretarStorage ss = new SekretarStorage();
            sekretari = ss.Read();

            foreach (Model.Sekretar s in sekretari)
            {
                if (s.KorisnickoIme.Equals(user) && s.Lozinka.Equals(pass))
                {
                    Sekretar sekretar = Sekretar.GetInstance();
                    sekretar.Show();
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Pogrešno korisničko ime ili pogrešna lozinka!");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        public void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Username_GotFocus;

        }

        private void Pass_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            pb.Password = string.Empty;
            pb.GotFocus -= Pass_GotFocus;
        }

        private void Username_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == string.Empty)
            {
                tb.Text = "Username";
            }
            tb.GotFocus -= Username_GotFocus;
        }

        private void Pass_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            if (pb.Password == string.Empty)
            {
                pb.Password = "jenova";
            }
            pb.GotFocus -= Pass_GotFocus;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Login();
            }
        }

    }
}
