﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SIMS.PacijentGUI.ViewModel
{

    public class ViewModelPatient : INotifyPropertyChanged
        {
            
            [field: NonSerialized]
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
       }
    
}
