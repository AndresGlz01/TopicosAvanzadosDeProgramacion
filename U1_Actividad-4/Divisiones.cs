using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace U1_Actividad_4
{
    public class Divisiones : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public double[] Operando1 { get; set; } = { 0, 0 };
        public double[] Operando2 { get; set; } = { 0, 0 };
        public double?[] Resultado { get; set; } = new double?[] { null, null };
        public ICommand DividirCommand { get; set; }
        public ICommand SumarCommand { get; set; }
        public ICommand RestarCommand { get; set; }
        public ICommand MultiplicarCommand { get; set; }
        public Divisiones()
        {
            DividirCommand = new RelayCommand(Dividir);
            SumarCommand = new RelayCommand(Sumar);
            RestarCommand = new RelayCommand(Restar);
            MultiplicarCommand = new RelayCommand(Multiplicar);
        }
        public void Sumar()
        {
            if (Operando1[1] == Operando2[1])
            {
                Resultado[0] = Operando1[0] + Operando2[0];
                Resultado[1] = Operando1[1];
            }
            else
            {
                Resultado[0] = (Operando1[0] * Operando2[1]) + (Operando1[1] * Operando2[0]);
                Resultado[1] = Operando1[1] * Operando2[1];
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public void Restar()
        {
            if (Operando1[1] == Operando2[1])
            {
                Resultado[0] = Operando1[0] - Operando2[0];
                Resultado[1] = Operando1[1];
            }
            else
            {
                Resultado[0] = (Operando1[0] * Operando2[1]) - (Operando1[1] * Operando2[0]);
                Resultado[1] = Operando1[1] * Operando2[1];
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public void Multiplicar()
        {
            Resultado[0] = Operando1[0] * Operando2[0];
            Resultado[1] = Operando1[1] * Operando2[1];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public void Dividir()
        {
            Resultado[0] = Operando1[0] * Operando2[1];
            Resultado[1] = Operando1[1] * Operando2[0];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
