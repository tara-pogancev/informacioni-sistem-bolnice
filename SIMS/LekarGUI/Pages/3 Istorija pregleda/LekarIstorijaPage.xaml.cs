using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIMS.LekarGUI.Dialogues.Izvestaji;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarIstorijaPage.xaml
    /// </summary>
    public partial class LekarIstorijaPage : Page
    {
        private static Lekar lekarUser;

        private ObservableCollection<Termin> evidentiraniView;
        public ObservableCollection<Termin> EvidentiraniView { get => evidentiraniView; set => evidentiraniView = value; }

        private ObservableCollection<Termin> prazniView;
        public ObservableCollection<Termin> PrazniView { get => prazniView; set => prazniView = value; }

        public LekarIstorijaPage(Lekar l)
        {
            InitializeComponent();

            lekarUser = l;

            //Evidentirani - pregledi sa anamnezom
            //Prazni - pregledi bez anamneze

            this.DataContext = this;
            evidentiraniView = new ObservableCollection<Termin>(TerminStorage.Instance.ReadList());
            prazniView = new ObservableCollection<Termin>(TerminStorage.Instance.ReadList());
            dobaviPodatkeOPacijenuILekaru();
            refreshView();
        }

        public void refreshView()
        {
            prazniView.Clear();
            evidentiraniView.Clear();

            List<Termin> temp = new List<Termin>(TerminStorage.Instance.ReadByDoctor(lekarUser));
            popuniInformacijeODoktoruIPacijentu(temp);
            foreach (Termin t in temp)
            {
                if (t.Evidentiran == true)
                    evidentiraniView.Add(t);

                else if (t.IsPast == true)
                {
                    prazniView.Add(t);
                }

            }

        }

        private void popuniInformacijeODoktoruIPacijentu(List<Termin> temp)
        {
            foreach (Termin termin in temp)
            {
                termin.Pacijent = new PacijentStorage().Read(termin.Pacijent.Jmbg);
                termin.Lekar = new LekarStorage().Read(termin.Lekar.Jmbg);
            }
        }

        private void Button_Anamneza(object sender, RoutedEventArgs e)
        {
            if (dataGridEvidentirani.SelectedItem != null)
            {
                Termin t = (Termin)dataGridEvidentirani.SelectedItem;

                if (t.VrstaTermina == TipTermina.pregled)
                {
                    Anamneza anamneza = AnamnezaStorage.Instance.Read(t.TerminKey);
                    if (anamneza != null)
                    {
                        AnamnezaView a = new AnamnezaView(anamneza);
                        a.Show();
                    }
                        
                }
                else
                {
                    //TODO Pregled operacije
                }
            }
        }

        private void Button_Evidencija(object sender, RoutedEventArgs e)
        {
            if (dataGridPrazni.SelectedItem != null)
            {
                Termin t = (Termin)dataGridPrazni.SelectedItem;

                if (t.VrstaTermina == TipTermina.pregled)
                {
                    AnamnezaCreate a = new AnamnezaCreate((Termin)dataGridPrazni.SelectedItem);
                    a.Show();
                    refreshView();
                }
                else
                {
                    //TODO pisanje izvestaja o operaciji
                }
            }
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void dobaviPodatkeOPacijenuILekaru()
        {
            foreach (Termin termin in EvidentiraniView)
            {
                termin.Pacijent = new PacijentStorage().Read(termin.Pacijent.Jmbg);
                termin.Lekar = new LekarStorage().Read(termin.Lekar.Jmbg);
            }
            
            
            foreach (Termin termin in prazniView)
            {
               termin.Pacijent = new PacijentStorage().Read(termin.Pacijent.Jmbg);
               termin.Lekar = new LekarStorage().Read(termin.Lekar.Jmbg);
            }
            
        }

    }
}
