using Model;
using SIMS.PacijentGUI;
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
            Patient pacijent = PatientRepository.Instance.ReadUser(user);
            if (pacijent != null && pass.Equals(pacijent.Lozinka))
            {
                if (pacijent.BanovanKorisnik)
                {
                    ObavjestenjeOTerminu o = new ObavjestenjeOTerminu();
                    o.TekstObavjestenja.Text= "Poštovani Vaš nalog je blokiran.Za više detalja obratite se sekretaru bolnice";
                    o.Show();
                    return;
                }
                PocetnaStranica pocetnaStranica=PocetnaStranica.getInstance();
                pocetnaStranica.Pacijent = pacijent;
                pocetnaStranica.kreirajAnketu();
                pocetnaStranica.Show();
                this.Close();
                pocetnaStranica.pokreniNit();
                return;
            }


            //impelemntacija za upravnika
            Manager upravnik = ManagerRepository.Instance.ReadUser(user);
            if (upravnik != null && pass.Equals(upravnik.Lozinka))
            {
                UpravnikWindow.Instance.Show();
                this.Close();
                return;
            }

            //impelementacija za doktora
            Doctor lekar = DoctorRepository.Instance.ReadUser(user);
            if (lekar != null && pass.Equals(lekar.Lozinka))
            {
                LekarUI lekarUI = LekarUI.GetInstance(lekar);
                lekarUI.Show();
                this.Close();
                return;
            }

            //implementacija za sekretara
            Secretary sekretar = SecretaryRepository.Instance.ReadUser(user);
            if (sekretar != null && pass.Equals(sekretar.Lozinka))
            {
                    SekretarUI sekretarUI = SekretarUI.GetInstance(sekretar);
                    sekretarUI.Show();
                    this.Close();
                    return;
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
