using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
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
using SIMS.Service;
using SIMS.Model;
using System.Threading.Tasks;
using SIMS.Enumerations;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for zakazivanje.xaml
    /// </summary>
    public partial class Zakazivanje : UserControl
    {
        Patient pacijent;
        private List<Doctor> lekari;
        private  ObservableCollection<String> dostupniTermini;
        private Appointment termin;
        Boolean doktorSelektovan;
        List<Room> slobodneProstorije;
        AppointmentService appointmentService;
        public Zakazivanje(Patient p)
        {
            InitializeComponent();
            DoctorFileRepository lk = new DoctorFileRepository();
            slobodneProstorije= new RoomFileRepository().UcitajProstorijeZaPreglede();
            lekari = new List<Doctor>();
            lekari = lk.GetAll();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            dostupniTermini = new ObservableCollection<string> (new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" });
            termin = new Appointment();
            this.DataContext = this;
            doktorSelektovan = false;
            appointmentService = new AppointmentService();
            
        }

        public List<Doctor> Lekari { get => lekari; set => lekari = value; }
        public ObservableCollection<string> DostupniTermini { get => dostupniTermini; set => dostupniTermini = value; }
        public Patient Pacijent { get => pacijent; set => pacijent = value; }
        public Appointment Termin { get => termin; set => termin = value; }

        private bool validiraj()
        {
            if (ListaDoktora.SelectedItem == null || OdabirDatuma.SelectedDate == null || terminiLista.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return false;
            }
           
            return true;
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (!validiraj())
            {
                return;
            }
            
            termin.Doctor = lekari[ListaDoktora.SelectedIndex];
            String vrijemeIDatum = OdabirDatuma.Text + " " + terminiLista.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum); 
            if (appointmentService.ScheduleAppointment(lekari[ListaDoktora.SelectedIndex], vremenskaOdrednica, pacijent) == false)
            {
                MessageBox.Show("Ne postoji slobodna prostorija");
                dostupniTermini.Remove(terminiLista.Text);
            }
            else
            {
                MessageBox.Show("Termin je uspjesno zakazan");
            }
            List<String> targets = new List<string>();
            targets.Add(pacijent.Jmbg);
            Notification notification = new Notification("Zakazan termin", termin.StartTime.AddDays(-1),"Imate zakazan termin",targets, false, NotificationType.AppointmentAllert);
            
             ZakazivanjeTermina.getInstance().Zakazivanje1.Children.Clear();
             ZakazivanjeTermina.getInstance().Zakazivanje1.Children.Add(new Zakazivanje(pacijent));
             
        }

        private void ListaDoktora_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doktorSelektovan = true;

        }

        private void OdabirDatuma_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
               
                Doctor chosenDoctor = lekari[ListaDoktora.SelectedIndex];
                String chosenDate = OdabirDatuma.SelectedDate.Value.ToString("dd.MM.yyyy.");
                dostupniTermini = new ObservableCollection<string>(appointmentService.GetAvailableTimeOfAppointment(chosenDoctor,chosenDate,pacijent));
                terminiLista.ItemsSource = dostupniTermini;
                
            }
        }

        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            slobodneProstorije = new RoomFileRepository().UcitajProstorijeZaPreglede();
            if (OdabirDatuma.SelectedDate == null)
            {
                MessageBox.Show("Potrebno je da prvo izaberete datum");
                terminiLista.SelectedIndex = -1;
                return;
            }

            
            
        }

        internal void ZakaziTerminDemo()
        {
            

            
            Task.Delay(1000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     ListaDoktora.SelectedIndex = 1;
                     ListaDoktora.Focus();

                     Task.Delay(2000).ContinueWith(_ =>
                     {
                         Application.Current.Dispatcher.Invoke(
                        System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(
                          delegate ()
                          {
                              OdabirDatuma.IsDropDownOpen = true;
                              Task.Delay(1000).ContinueWith(_ =>
                              {
                                  Application.Current.Dispatcher.Invoke(
                                 System.Windows.Threading.DispatcherPriority.Normal,
                                 new Action(
                                   delegate ()
                                   {

                                       OdabirDatuma.SelectedDate = DateTime.Now.AddDays(3);
                                       Task.Delay(1000).ContinueWith(_ =>
                                       {
                                           Application.Current.Dispatcher.Invoke(
                                          System.Windows.Threading.DispatcherPriority.Normal,
                                          new Action(
                                            delegate ()
                                            {

                                                OdabirDatuma.IsDropDownOpen = false;
                                                Task.Delay(1500).ContinueWith(_ =>
                                                {
                                                    Application.Current.Dispatcher.Invoke(
                                                   System.Windows.Threading.DispatcherPriority.Normal,
                                                   new Action(
                                                     delegate ()
                                                     {
                                                         terminiLista.IsDropDownOpen = true;
                                                         terminiLista.Focus();
                                                         Task.Delay(1500).ContinueWith(_ =>
                                                         {
                                                             Application.Current.Dispatcher.Invoke(
                                                            System.Windows.Threading.DispatcherPriority.Normal,
                                                            new Action(
                                                              delegate ()
                                                              {
                                                                  terminiLista.SelectedIndex = 1;
                                                                  terminiLista.IsDropDownOpen = false;
                                                                  Task.Delay(1500).ContinueWith(_ =>
                                                                  {
                                                                      Application.Current.Dispatcher.Invoke(
                                                                     System.Windows.Threading.DispatcherPriority.Normal,
                                                                     new Action(
                                                                       delegate ()
                                                                       {
                                                                           ObavjestenjeOTerminu obavjestenje = new ObavjestenjeOTerminu();
                                                                           obavjestenje.TekstObavjestenja.Text = "Termin je uspjesno zakazan";
                                                                           obavjestenje.Show();
                                                                           Task.Delay(1000).ContinueWith(_ =>
                                                                           {
                                                                               Application.Current.Dispatcher.Invoke(
                                                                              System.Windows.Threading.DispatcherPriority.Normal,
                                                                              new Action(
                                                                                delegate ()
                                                                                {

                                                                                    obavjestenje.Close();
                                                                                    Task.Delay(1000).ContinueWith(_ =>
                                                                                    {
                                                                                        Application.Current.Dispatcher.Invoke(
                                                                                       System.Windows.Threading.DispatcherPriority.Normal,
                                                                                       new Action(
                                                                                         delegate ()
                                                                                         {
                                                                                             ZakazivanjeTermina.getInstance().Zakazivanje1.Children.Clear();
                                                                                             ZakazivanjeTermina.getInstance().Zakazivanje1.Children.Add(new Zakazivanje(pacijent));
                                                                                             PocetnaStranica.getInstance().frame.Content = new PocetniEkran(PocetnaStranica.getInstance().Pacijent);

                                                                                         }
                                                                                        ));
                                                                                    });
                                                                                   

                                                                                }
                                                                               ));
                                                                           });
                                                                           

                                                                       }
                                                                      ));
                                                                  });

                                                              }
                                                             ));
                                                         });



                                                     }
                                                    ));
                                                });
                                            }
                                           ));
                                       });
                                   }
                                  ));
                              });


                          }
                         ));
                     });
                 }
                ));
            });

            
         

            
           
        }
        
    }
}
