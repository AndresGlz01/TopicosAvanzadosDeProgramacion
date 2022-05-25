using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace U1_Actividad_5.viewmodels
{
    public class NumerosBinarios : INotifyPropertyChanged
    {
        int numero;
        int puntos = 0;
        byte dificultad = 0;
        string mensaje = "";
        bool jugar;

        /// <summary>
        /// Retorna la dificultad actual
        /// </summary>
        public string Dificultad
        {
            get
            {
                if (dificultad == 0) return "DIFICULTAD: FACIL";

                else if (dificultad == 1) return "DIFICULTAD: NORMAL";

                else return "DIFICULTAD: DIFICIL";
            }
        }

        public ICommand VerificarCommand { get; set; }  // Comando para verificar
        public ICommand GenerarCommand { get; set; }    // Comando para generar

        public string Binario => Convert.ToString(numero, 2);
        public int Puntos => puntos;
        public string Mensaje => mensaje;
        public bool Jugar => jugar;

        /// <summary>
        /// Evento para reflejar cambios en la vista con Binding
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// El constructor instancia ambos comandos
        /// </summary>
        public NumerosBinarios()
        {
            GenerarCommand = new RelayCommand(GenerarNuevo);
            VerificarCommand = new RelayCommand<int>(Verificar);
        }

        /// <summary>
        /// Genera un nuevo número y lo muestra como binario según la dificultad actual
        /// </summary>
        public void GenerarNuevo()
        {
            if (dificultad == 0) numero = new Random().Next(0, 256);

            else if (dificultad == 1) numero = new Random().Next(0, 12232);

            else numero = new Random().Next(0, 343433);

            jugar = true;
            mensaje = "";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        /// <summary>
        /// Verifica que el valor ingresado coinsida con el número binario
        /// </summary>
        /// <param name="dato">Representa el valor ingresado</param>
        public void Verificar(int dato)
        {
            if (jugar == true)
            {
                if (dato == numero)
                {
                    puntos++;

                    if (puntos >= 0 && puntos < 5) dificultad = 0;

                    else if (puntos >= 5 && puntos < 10) dificultad = 1;

                    else dificultad = 2;

                    jugar = false;
                    numero = 0;
                    MostrarMensaje("¡ACERTASTE!");
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                }
                else MostrarMensaje("¡FALLASTE!");
            }
        }

        /// <summary>
        /// Muestra un mensaje personalizado por 1300ms y después lo cambia
        /// </summary>
        /// <param name="mensaje">Valor a mostrar</param>
        public void MostrarMensaje(string mensaje)
        {
            Timer aTimer;
            aTimer = new();
            aTimer.Interval = 1300;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;

            this.mensaje = mensaje;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mensaje"));
            void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
            {
                this.mensaje = "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mensaje"));
            }
        }
    }
}
