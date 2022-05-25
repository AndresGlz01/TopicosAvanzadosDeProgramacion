using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace U1_Actividad_5.viewmodels
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

        /// <summary>
        /// Incrementa 1 al contador
        /// </summary>
        public void Incrementar()
        {
            if (conteo == 999)
            {
                return;
            }
            conteo++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        /// <summary>
        /// Resta 1 al contador
        /// </summary>
        public void Decrementar()
        {
            if (conteo == 0)
            {
                return;
            }
            conteo--;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        /// <summary>
        /// Reinicia el contador a cero
        /// </summary>
        public void Reiniciar()
        {
            conteo = 0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
