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

namespace SIMS.LekarGUI.Dialogues.Recepti_i_terapije
{
    /// <summary>
    /// Interaction logic for PisanjeTerapije.xaml
    /// </summary>
    public partial class PisanjeTerapije : Window
    {
        public PisanjeTerapije()
        {
            InitializeComponent();
        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBox.Show("Terapija uspešno kreirana!");
        }
    }
}
