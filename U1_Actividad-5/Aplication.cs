using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace U1_Actividad_5
{
    public class Aplication : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Vista { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public Aplication()
        {
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
        }
        public void CambiarVista(string Vista)
        {
            this.Vista = Vista;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
