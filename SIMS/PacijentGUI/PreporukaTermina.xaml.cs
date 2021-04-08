using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for PreporukaTermina.xaml
    /// </summary>
    public partial class PreporukaTermina : UserControl
    {
        Pacijent pacijent;
        public PreporukaTermina(Pacijent p)
        {
            pacijent = p;
            InitializeComponent();
        }

        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }

        private void Trazi_Click(object sender, RoutedEventArgs e)
        {
            PreporuceniTermini preporuceniTermini = new PreporuceniTermini(pacijent);
            preporuceniTermini.Show();
        }
    }
}
