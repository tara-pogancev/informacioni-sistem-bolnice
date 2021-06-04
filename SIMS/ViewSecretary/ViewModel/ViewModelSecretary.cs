using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMS.ViewSecretary.ViewModel
{
    class ViewModelSecretary : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
