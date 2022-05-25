using GalaSoft.MvvmLight.Command;
using ListaDeCompras.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListaDeCompras.ViewModels
{
    public class ProductosViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();

        public string Vista { get; set; } = "Ver";
        public string? Mensaje { get; set; }
        public Producto? MiProducto { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand ModificarCommand { get; set; }

        public ProductosViewModel()
        {
            Abrir();
            AgregarCommand = new RelayCommand(Agregar);
            CancelarCommand = new RelayCommand(Cancelar);
            EliminarCommand = new RelayCommand(Eliminar);
            ModificarCommand = new RelayCommand(Modificar);
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
        }

        int posicionorigial;
        private void CambiarVista(string Vista)
        {
            this.Vista = Vista;

            if (this.Vista == "Agregar") MiProducto = new Producto();

            else if (this.Vista == "Modificar")
            {
                var clon = new Producto();
                if(MiProducto != null)
                {
                    posicionorigial = Productos.IndexOf(MiProducto);
                    clon.Cantidad = MiProducto.Cantidad;
                    clon.Descripcion = MiProducto.Descripcion;
                    MiProducto = clon;
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vista)));
        }


        private void Cancelar()
        {
            Vista = "Ver";
            MiProducto = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vista)));
        }

        void Agregar()
        {
            if (MiProducto != null)
            {
                Mensaje = null;

                if (string.IsNullOrWhiteSpace(MiProducto.Descripcion))
                {
                    Mensaje = "Debe ingresar una descripción correcta";
                }
                if (string.IsNullOrWhiteSpace(MiProducto.Cantidad))
                {
                    Mensaje = "Debe ingresar una cantidad correcta";
                }

                if (string.IsNullOrWhiteSpace(Mensaje))
                {
                    Productos.Add(MiProducto);                    
                    CambiarVista("Ver");
                }

                Guardar();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        void Eliminar()
        {
            if (MiProducto != null)
            {
                Productos.Remove(MiProducto);
                Guardar();
                CambiarVista("Ver");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        void Modificar()
        {
            if (MiProducto != null)
            {
                Productos[posicionorigial] = MiProducto;
                Guardar();
                CambiarVista("Ver");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        void Abrir()
        {
            if (System.IO.File.Exists("productos.json"))
            {
                var json = System.IO.File.ReadAllText("productos.json");
                var datos = JsonConvert
                    .DeserializeObject<ObservableCollection<Producto>>(json);

                if (datos == null) Productos = new ObservableCollection<Producto>();

                else Productos = datos;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        void Guardar()
        {
            var json = JsonConvert.SerializeObject(Productos);
            System.IO.File.WriteAllText("productos.json", json);
        }
    }
}
