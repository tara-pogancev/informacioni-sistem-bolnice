using Model;
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

        private void Login()
        {
            Boolean flag = true;
            Boolean nasaoKorisnika = false;
            if (flag)
            {
                String user = username.Text;
                String pass = password.Password;
                List<Pacijent> pacijenti = new List<Pacijent>();
                PacijentStorage pc = new PacijentStorage();
                pacijenti = pc.ReadAll();

                foreach (Pacijent pac in pacijenti)
                {
                    if (pac.KorisnickoIme.Equals(user) && pac.Lozinka.Equals(pass))
                    {
                        PacijentUI pacijent = new PacijentUI(pac);
                        this.Close();
                        pacijent.Show();
                        nasaoKorisnika = true;
                        flag = false;
                        break;
                        

                    }
                }
            }

            if (flag) { 
                //impelemntacija za upravnika
            }

            if (flag)
            {
                //impelementacija za doktora
            }
             if (flag)
            {
                //implementacija za sekretara
            }
            if (nasaoKorisnika==false)
            {
                MessageBox.Show("Pogrešno korisničko ime ili pogrešna lozinka!");
            }

           /* if (password.Password.SequenceEqual("pacijent") && username.Text.SequenceEqual("pacijent"))
            {
                PacijentUI pacijent = new PacijentUI(pac);
                this.Close();
                
                pacijent.Show();
            }
            else if (password.Password.SequenceEqual("lekar") && username.Text.SequenceEqual("lekar") || username.Text.StartsWith("l"))
            {
                //unijeti kod za otvaranje nove stranice 
                LekarUI lekarUI = new LekarUI();
                this.Close();
                lekarUI.Show();
            }
            else if (password.Password.SequenceEqual("upravnik") && username.Text.SequenceEqual("upravnik"))
            {
                //unijeti kod za otvaranje nove stranice
            }
            else if (password.Password.SequenceEqual("sekretar") && username.Text.SequenceEqual("sekretar"))
            {
                //uniejti kod za otaranje nove stranice
            }
            else
            {
                
            }
*/

        }
    }
}
