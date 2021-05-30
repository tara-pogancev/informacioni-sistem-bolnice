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
    /// Interaction logic for TerapijaPrikaz.xaml
    /// </summary>
    public partial class TherapyView : Window
    {
        public TherapyView()
        {
            InitializeComponent();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
