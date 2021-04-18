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
using Model;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for AnamnezaTrening.xaml
    /// </summary>
    public partial class AnamnezaCreate : Window
    {
        private Termin termin;

        public AnamnezaCreate(Termin t)
        {
            InitializeComponent();

            termin = t;

            LabelDoktor.Content = "Doktor: " + termin.ImeLekara;
            if (termin.VrstaTermina == TipTermina.pregled)
                LabelDatum.Content = "Datum pregleda: " + termin.Datum;
            else LabelDatum.Content = "Datum operacije: " + termin.Datum;

            LabelPacijent.Content = "Pacijent: " + termin.ImePacijenta;
            LabelDatumRodjenja.Content = "Datum rođenja: " + PacijentStorage.Instance.Read(termin.PacijentKey).Datum_Rodjenja.ToString("dd.MM.yyyy.");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt1.Text.Equals("") || txt2.Text.Equals("") || txt3.Text.Equals(""))
                MessageBox.Show("Molimo popunite sva obavezna polja!");
            else
            {
                Anamneza a = new Anamneza(termin.TerminKey, txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text, txt6.Text,
                    txt7.Text, txt8.Text, txt9.Text, txt10.Text, txt11.Text, txt12.Text);
                AnamnezaStorage.Instance.Create(a);
                this.Close();
                //LekarIstorijaPage.GetInstance().refreshView();
                LekarUI.GetInstance().ChangeTab(3);

            }
        }
    }
}
