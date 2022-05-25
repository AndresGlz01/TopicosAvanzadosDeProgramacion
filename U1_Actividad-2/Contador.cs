using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace U1_Actividad_2
{
    public class Contador : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand IncrementarCommand { get; }
        public ICommand DecrementarCommand { get; }
        public ICommand ReiniciarCommand { get; }

        private ushort conteo;

        public ushort Conteo
        {
            get { return conteo; }
        }
        public Contador()
        {
            conteo = 000;
            IncrementarCommand = new RelayCommand(Incrementar);
            DecrementarCommand = new RelayCommand(Decrementar);
            ReiniciarCommand = new RelayCommand(Reiniciar);
        }
        public void Incrementar()
        {
            if (conteo == 999)
            {
                return;
            }
            conteo++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public void Decrementar()
        {
            if (conteo == 0)
            {
                return;
            }
            conteo--;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public void Reiniciar()
        {
            conteo = 0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
