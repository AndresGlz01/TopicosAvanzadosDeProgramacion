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
    public class Fracciones : INotifyPropertyChanged
    {
        /// <summary>
        /// Evento para reflejar los cambiós en la vista que tengan Binding
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Representa a la fracción 1
        /// </summary>
        public double[] Operando1 { get; set; } = { 0, 0 };

        /// <summary>
        /// Representa a la fracción 2
        /// </summary>
        public double[] Operando2 { get; set; } = { 0, 0 };

        /// <summary>
        /// Representa el resultado de operar ambos operandos
        /// </summary>
        public double?[] Resultado { get; set; } = new double?[] { null, null };

        public ICommand DividirCommand { get; set; }        // Comando para dividir
        public ICommand SumarCommand { get; set; }          // Comando para sumar
        public ICommand RestarCommand { get; set; }         // Comando para restar
        public ICommand MultiplicarCommand { get; set; }    // Comando para múltiplicar

        public Fracciones()
        {
            DividirCommand = new RelayCommand(Dividir); 
            SumarCommand = new RelayCommand(Sumar);
            RestarCommand = new RelayCommand(Restar);
            MultiplicarCommand = new RelayCommand(Multiplicar);
        }

        /// <summary>
        /// Suma ambos operandos y los valores los asigna al resultado
        /// </summary>
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

        /// <summary>
        /// Resta ambos operandos y los valores los asigna al resultado
        /// </summary>
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

        /// <summary>
        /// Múltiplica ambos operandos y los valores los asigna al resultado 
        /// </summary>
        public void Multiplicar()
        {
            Resultado[0] = Operando1[0] * Operando2[0];
            Resultado[1] = Operando1[1] * Operando2[1];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        /// <summary>
        /// Divide ambos operandos y los valores los asigna al resultado
        /// </summary>
        public void Dividir()
        {
            Resultado[0] = Operando1[0] * Operando2[1];
            Resultado[1] = Operando1[1] * Operando2[0];
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
