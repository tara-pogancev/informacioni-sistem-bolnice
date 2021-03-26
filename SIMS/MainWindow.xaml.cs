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
            if (password.Password.SequenceEqual("pacijent") && username.Text.SequenceEqual("pacijent"))
            {
                Pacijent pacijent = new Pacijent();
                this.Close();
                pacijent.Show();
            }
            else if (password.Password.SequenceEqual("lekar") && username.Text.SequenceEqual("lekar"))
            {
                //unijeti kod za otvaranje nove stranice 
                LekarUI lekarUI = new LekarUI();
                this.Close();
                lekarUI.Show();
            }
            else if(password.Password.SequenceEqual("upravnik") && username.Text.SequenceEqual("upravnik"))
            {
                //unijeti kod za otvaranje nove stranice
            }
            else if (password.Password.SequenceEqual("sekretar") && username.Text.SequenceEqual("sekretar"))
            {
                //uniejti kod za otaranje nove stranice
            }
            else
            {
                MessageBox.Show("Pogresna lozinka ili korisnicko ime");
            }



        }
    }
}
