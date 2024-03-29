﻿using SIMS.Repositories.SecretaryRepo;
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
using SIMS.Controller;
using SIMS.Enumerations;
using SIMS.Model;
using SIMS.Repositories.NotificationRepo;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for Obavjestenja.xaml
    /// </summary>
    /// 

    public class ObavjestenjeWpf
    {
        Notification obavestenje;
        string displayedImage;



        public ObavjestenjeWpf()
        {

        }
        public ObavjestenjeWpf(Notification obv,String src)
        {
            obavestenje = obv;
            displayedImage = src;
        }
        public string DisplayedImage
        {

            get
            {
                
                if (obavestenje.Author.Equals("Sekretarijat"))
                {
                    return "/src/sekretar.png";
                }
                else if (obavestenje.NotificationType==NotificationType.Receipt)
                {
                    return "/src/reports.png";
                }else if (obavestenje.NotificationType == NotificationType.NoteReminder)
                {
                    return "/src/PhotoReminder.png";
                }else if (obavestenje.NotificationType == NotificationType.AppointmentAllert)
                {
                    return "/src/doctor.png";
                }
                else
                {
                    return "/src/sekretar.png";
                }
            }


        }

        public Notification Obavestenje { get => obavestenje; set => obavestenje = value; }
    }
    public partial class Obavjestenja : UserControl
    {
        private List<Notification> obavjestenja;
        private Patient pacijent;


        public Obavjestenja()
        {
            InitializeComponent();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            obavjestenja = new List<Notification>();
            NotificationController obavestenjaController = new NotificationController();
            obavjestenja = obavestenjaController.ReadPastNotificationsByUser(pacijent.Jmbg);
            
            obavjestenja.Reverse();
            //obavestenjeView = new ObservableCollection<Obavestenje>(listaObavestenja);
            List<ObavjestenjeWpf> obavjestenjaWpfs = new List<ObavjestenjeWpf>();
            foreach(Notification obavestenje in obavjestenja)
            {
                obavjestenjaWpfs.Add(new ObavjestenjeWpf(obavestenje,""));
            }
            viewerObavestenja.ItemsSource = obavjestenjaWpfs;

            this.DataContext = obavjestenjaWpfs;
        }

        
    }
}
