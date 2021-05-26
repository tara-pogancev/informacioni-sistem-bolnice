using SIMS.Repositories.SecretaryRepo;
using SIMS.PacijentGUI;
using SIMS.Repositories.DoctorRepo;
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
using SIMS.Model;
using SIMS.Controller;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LastLoginController loginController = new LastLoginController();

        public MainWindow()
        {
            InitializeComponent();

            if (loginController.CheckForLastLogged())
                Login();

        }

        private void Login()
        {
            String user = username.Text;
            String pass = password.Password;

            if (loginController.CheckForLastLogged())
            {
                SetupLastLoggedUser(out user, out pass);
            }

            //impelemntacija za pacijenta
            Patient pacijent = PatientFileRepository.Instance.ReadUser(user);
            if (pacijent != null && pass.Equals(pacijent.Password))
            {
                if (pacijent.IsBanned)
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
            Manager upravnik = ManagerFileRepository.Instance.ReadUser(user);
            if (upravnik != null && pass.Equals(upravnik.Password))
            {
                ManagerLogin();
                return;
            }

            //impelementacija za doktora
            Doctor lekar = DoctorFileRepository.Instance.ReadUser(user);
            if (lekar != null && pass.Equals(lekar.Password))
            {
                DoctorLogin(lekar);
                return;
            }

            //implementacija za sekretara
            Secretary sekretar = SecretaryFileRepository.Instance.ReadUser(user);
            if (sekretar != null && pass.Equals(sekretar.Password))
            {
                SecretaryLogin(sekretar);
                return;
            }

            MessageBox.Show("Pogrešno korisničko ime ili pogrešna lozinka!");

        }

        private void SetupLastLoggedUser(out string user, out string pass)
        {
            LoggedUser lastLogged = loginController.GetLastLogged();
            user = lastLogged.Username;
            pass = lastLogged.Password;
        }

        private void ManagerLogin()
        {
            UpravnikWindow.Instance.Show();
            this.Close();
        }

        private void DoctorLogin(Doctor lekar)
        {
            DoctorUI lekarUI = DoctorUI.GetInstance(lekar);
            lekarUI.Show();
            this.Close();
        }

        private void SecretaryLogin(Secretary sekretar)
        {
            SekretarUI sekretarUI = SekretarUI.GetInstance(sekretar);
            sekretarUI.Show();
            this.Close();
        }

        private void ButtonLogin(object sender, RoutedEventArgs e)
        {
            Login();
        }

        public void UsernameGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= UsernameGotFocus;

        }

        private void PassGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            pb.Password = string.Empty;
            pb.GotFocus -= PassGotFocus;
        }

        private void UsernameLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == string.Empty)
            {
                tb.Text = "Username";
            }
            tb.GotFocus -= UsernameGotFocus;
        }

        private void PassLostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            if (pb.Password == string.Empty)
            {
                pb.Password = "jenova";
            }
            pb.GotFocus -= PassGotFocus;
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
