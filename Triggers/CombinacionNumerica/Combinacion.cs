using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CombinacionNumerica
{
    public class Combinacion : INotifyPropertyChanged
    {
        string[] combinacion = new string[4];

        public bool JuegoIniciado { get; set; } = false;

        private int correctos;
        private int oportunidades;

        public int Oportunidades
        {
            get { return oportunidades; }
            set { oportunidades = value; }
        }

        public int Correctos
        {
            get { return correctos; }
            set { correctos = value; }
        }

        public ICommand GenerarCommand { get; set; }
        public ICommand VerificarCommand { get; set; }

        public Combinacion()
        {
            GenerarCommand = new RelayCommand(Generar);
            VerificarCommand = new RelayCommand<string[]>(Verificar);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Generar()
        {
            Random r = new Random();
            for (int i = 0; i < combinacion.Length; i++)
            {
                combinacion[i] = r.Next(1, 10).ToString();
            }
            Correctos = 0;
            Oportunidades = 10;
            JuegoIniciado = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public void Verificar(string[] datos)
        {
            Correctos = 0;
            for (int i = 0; i < combinacion.Length; i++)
            {
                if (combinacion[i] == datos[i])
                {
                    Correctos++;
                }
            }

            if (correctos == 4)
            {
                JuegoIniciado = false;
            }
            else
            {
                oportunidades--;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
