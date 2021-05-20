using Microsoft.VisualStudio.PlatformUI;
using SIMS.Commands;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SIMS.PacijentGUI.ViewModel


{
    public class Meeting
    {
        public string EventName { get; set; }
        public String Consumption { get; set; }
        public String Use { get; set; }
        public string Organizer { get; set; }
        public string ContactID { get; set; }
        public int Capacity { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Color color { get; set; }
        public Brush Color { get; internal set; }
        public bool AllDay { get; set; }
    }

    public class TerapijaPacijentaViewModel:ViewModel
    {
        public ObservableCollection<Meeting> Meetings { get; set; }
        List<string> eventNameCollection;
        List<Brush> colorCollection;
        List<string> use;


        #region commands
        public RelayCommand GenerisiIzvjestajCommand { get; set; }

        


        #endregion

        public TerapijaPacijentaViewModel()
        {
            
            Meetings = new ObservableCollection<Meeting>();
            CreateEventNameCollection();
            CreateColorCollection();
            CreateQuantityCollection();
            CreateAppointments();
            
            GenerisiIzvjestajCommand = new RelayCommand(Execute_GenerisiIzvjestajCommand);

        }


        

        #region actions
        public void Execute_GenerisiIzvjestajCommand(object obj)
        {
            
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintVisual(new IzvjestajPage(), "izvjestaj");
        }

        public bool CanExecute_GenerisiIzvjestajCommand(object obj)
        {
            return true;
        }
        #endregion

        private void CreateAppointments()
        {
            Random randomTime = new Random();
            List<Point> randomTimeCollection = GettingTimeRanges();
            DateTime date;
            DateTime DateFrom = DateTime.Now.AddDays(-10);
            DateTime DateTo = DateTime.Now.AddDays(10);
            DateTime dataRangeStart = DateTime.Now.AddDays(-3);
            DateTime dataRangeEnd = DateTime.Now.AddDays(3);


            for (date = DateFrom; date < DateTo; date = date.AddDays(1))
            {
                if ((DateTime.Compare(date, dataRangeStart) > 0) && (DateTime.Compare(date, dataRangeEnd) < 0))
                {
                    for (int AdditionalAppointmentIndex = 0; AdditionalAppointmentIndex < 3; AdditionalAppointmentIndex++)
                    {
                        Meeting meeting = new Meeting();
                        int hour = (randomTime.Next((int)randomTimeCollection[AdditionalAppointmentIndex].X, (int)randomTimeCollection[AdditionalAppointmentIndex].Y));
                        meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                        meeting.To = (meeting.From.AddHours(1));
                        int k = randomTime.Next(9);
                        meeting.EventName = eventNameCollection[k];
                        meeting.Consumption = use[k];
                        meeting.Color = colorCollection[randomTime.Next(9)];
                        
                        Meetings.Add(meeting);
                    }
                }
                else
                {
                    Meeting meeting = new Meeting();
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(9, 11), 0, 0);
                    meeting.To = (meeting.From.AddHours(1));

                    int k = randomTime.Next(9);
                    meeting.EventName = eventNameCollection[k];
                    meeting.Consumption = use[k];
                    meeting.Color = colorCollection[randomTime.Next(9)];
                    Meetings.Add(meeting);
                }
            }
        }

        /// <summary>  
        /// Creates event names collection.  
        /// </summary>  
        private void CreateEventNameCollection()
        {
            eventNameCollection = new List<string>();
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Lek za krvne sudove");
            eventNameCollection.Add("Brufen");
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Hladna obloga");
            eventNameCollection.Add("Panadol");
            eventNameCollection.Add("Krema");
            eventNameCollection.Add("Vitamin C");
            eventNameCollection.Add("Lek za pritisak");
            eventNameCollection.Add("Lek za pritisak");
        }

        private void CreateQuantityCollection()
        {
            use = new List<string>();
            use.Add("Jednu tabletu popiti ujutro i uveče");
            use.Add("Ispod jezika staviti 1 tabletu ujutro i uveće");
            use.Add("Piti po potrebi prilikom jakih bolova");
            use.Add("Jednu tabletu popiti ujutro i uveče");
            use.Add("Oblog natopiti hladnom vodom.Stavljati ga na otečeno mjesto 3 puta dnevno.");
            use.Add("Jednu tabletu popiti ujutro i uveče");
            use.Add("Malu kolicinu kreme staviti na ranu.Ranu ne treba zamotavati gazom.");
            use.Add("Vitamin C piti jedan put dnevno.");
            use.Add("Jednu tabletu popiti ujutro i uveče");
            use.Add("Jednu tabletu popiti ujutro i uveče");
        }

        /// <summary>  
        /// Creates color collection.  
        /// </summary>  
        private void CreateColorCollection()
        {
            colorCollection = new List<Brush>();
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF339933")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00ABA9")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE671B8")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1BA1E2")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD80073")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA2C139")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFA2C139")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD80073")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF339933")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE671B8")));
            colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00ABA9")));
        }

        /// <summary>
        /// Gets the time ranges.
        /// </summary>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));
            return randomTimeCollection;
        }




    }
}
