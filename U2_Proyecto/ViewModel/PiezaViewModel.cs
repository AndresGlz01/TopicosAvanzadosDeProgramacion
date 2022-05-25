using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using U2_Proyecto.Model;

namespace U2_Proyecto.ViewModel
{
    internal class PiezaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Pieza? pieza;

        public Pieza? Pieza
        {
            get { return pieza; }
            set 
            { 
                pieza = value;
                NotificarCambios();
            }
        }

        private Artista? artista;

        public Artista? Artista
        {
            get { return artista; }
            set 
            { 
                artista = value;
                NotificarCambios();
            }
        }

        public ObservableCollection<Pieza> Piezas { get; set; } = new ();
        public ObservableCollection<Artista> Artistas { get; set; } = new ();

        #region Comandos
        public ICommand AgregarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand ModificarCommand { get; set; }
        public ICommand CambiarViewCommand { get; set; }
        #endregion

        public string? View { get; set; } = string.Empty;
        public PiezaViewModel()
        {
            Deserializar();
            AgregarCommand = new RelayCommand(Agregar);
            CancelarCommand = new RelayCommand(Cancelar);
            EliminarCommand = new RelayCommand(Eliminar);
            ModificarCommand = new RelayCommand(Modificar);
            CambiarViewCommand = new RelayCommand<string>(CambiarView);
        }

        private void Modificar()
        {
            if (Pieza != null)
                {
                    Piezas[PosicionModificar] = Pieza;
                }
            Serializar();
            CambiarView("Ver");
        }

        private void Eliminar()
        {
            if (Pieza != null)
            {

                    Piezas.Remove(Pieza);
            }
            Serializar();
            NotificarCambios();
            CambiarView("Ver");
        }

        private void Cancelar()
        {
            CambiarView("Ver");
            Artista = null;
            Pieza = null;
            NotificarCambios();
        }

        private void Agregar()
        {
            if (View == "AgregarPieza")
            {
                if (Pieza != null) Piezas.Add(Pieza); 
            }
            else if(View == "AgregarArtista")
            {
                if (Artista != null) Artistas.Add(Artista); 
            }
            Serializar();
            CambiarView("Ver");
            NotificarCambios();
        }

        int PosicionModificar;
        void CambiarView(string View)
        {
            this.View = View;
            if (View == "AgregarPieza")
            {
                Pieza = new ();
            }

            if (View == "AgregarArtista")
            {
                Artista = new ();
            }
            
            if (View == "ModificarPieza")
            {
                if (Pieza != null)
                {
                    Pieza clon = new Pieza()
                    {
                        Titulo = Pieza.Titulo,
                        Artista = Pieza.Artista,
                        Tipo = Pieza.Tipo,
                        FechaCreacion = Pieza.FechaCreacion,
                        Descripcion = Pieza.Descripcion,                        
                        Imagen = Pieza.Imagen
                    };
                    PosicionModificar = Piezas.IndexOf(Pieza);
                    Pieza = clon;
                }
            }
            NotificarCambios();
        }

        void NotificarCambios()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        void Serializar()
        {
            var json = JsonConvert.SerializeObject(Piezas);
            File.WriteAllText("Piezas.json", json);
            json = JsonConvert.SerializeObject(Artistas);
            File.WriteAllText("Artistas.json", json);
        }

        void Deserializar()
        {
            if (File.Exists("Piezas.json"))
            {
                var json = File.ReadAllText("Piezas.json");
                var datos = JsonConvert.DeserializeObject<ObservableCollection<Pieza>>(json);

                if (datos == null) Piezas = new ObservableCollection<Pieza>();
                else Piezas = datos;
            }
            if (File.Exists("Artistas.json"))
            {
                var json = File.ReadAllText("Artistas.json");
                var datos = JsonConvert.DeserializeObject<ObservableCollection<Artista>>(json);

                if (datos == null) Artistas = new ObservableCollection<Artista>();
                else Artistas = datos;
            }
        }
    }
}