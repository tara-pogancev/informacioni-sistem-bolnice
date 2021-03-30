using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;

namespace SIMS.UpravnikGUI
{
    public partial class ProstorijaCreate : Window
    {
        public ProstorijaCreate()
        {
            InitializeComponent();
        }
        private void Kreiraj_Click(object sender, RoutedEventArgs e)
        {
            Prostorija prostorija = new Prostorija(
                new Grad(GradTextBox.Text, 0, null),
                UlicaTextBox.Text,
                int.Parse(BrojUUliciTextBox.Text),
                int.Parse(SpratTextBox.Text),
                int.Parse(BrojProstorijeTextBox.Text),
                DostupnostComboBox.SelectedIndex == 0,
                (TipProstorije)TipComboBox.SelectedIndex
                );

            ProstorijaStorage.Instance.Create(prostorija);
        }
    }
}
