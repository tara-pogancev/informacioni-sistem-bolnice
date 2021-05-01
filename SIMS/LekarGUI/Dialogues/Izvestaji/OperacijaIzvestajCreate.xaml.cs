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

namespace SIMS.LekarGUI.Dialogues.Izvestaji
{
    /// <summary>
    /// Interaction logic for OperacijaCreate.xaml
    /// </summary>
    public partial class OperacijaIzvestajCreate : Window
    {
        public OperacijaIzvestajCreate()
        {
            InitializeComponent();
        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
