using Model;
using SIMS.Model;
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
        private Termin operation;

        public OperacijaIzvestajCreate(Termin operationPar)
        {
            InitializeComponent();
            operation = operationPar;

            LabelDoctor.Content = "Doktor: " + operation.ImeLekara;
            LabelDate.Content = "Datum operacije: " + operation.Datum;

            LabelPacijent.Content = "Pacijent: " + operation.ImePacijenta;
            LabelBirthDate.Content = "Datum rođenja: " + operation.Pacijent.Datum_Rodjenja;

            LabelRoom.Content = "Prostorija: " + operation.NazivProstorije;

        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            if (!OperationName.Text.Equals("") && !OperationDescription.Text.Equals(""))
            {
                OperacijaIzvestaj o = new OperacijaIzvestaj(operation, OperationName.Text, OperationDescription.Text);
                o.Operacija.Serijalizuj = false;
                OperacijaIzvestajStorage.Instance.Create(o);

                this.Close();
                LekarUI.GetInstance().ChangeTab(3);
            } else
            {
                MessageBox.Show("Molimo popunite sva polja!");
            }       

        }
    }
}
