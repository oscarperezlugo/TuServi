using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuServi.Conexiones;
using TuServi.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Historial : ContentPage
    {


        private ObservableCollection<servicio> _serv;
        public ObservableCollection<servicio> servicioshist
        {
            get
            {
                return _serv ?? (_serv = new ObservableCollection<servicio>());
            }
        }

        public Historial()
        {
            InitializeComponent();
            CheckServicios();
        }


        public void CheckServicios()
        {
            Console.WriteLine("METODO CHECK SERVICIOS");

            Repositorio repo = new Repositorio();
            servicio[] servicios = repo.getServicios().Result;

            if (servicios.Length == 0)
            {
                ListaServicios.IsVisible = false;
                image.Source = "historialvacio.png";
            } else
            {
                image.IsVisible = false;

                for (var i = 0; i < servicios.Length; i++)
                {

                    if (servicios[i].status == "Agregado")
                    {
                        servicioshist.Add(servicios[i]);
                    } else
                    {
                        ListaServicios.IsVisible = false;
                        image.Source = "historialvacio.png";
                    }

                }

                ListaServicios.ItemsSource = servicioshist;
            }
        }
    }
}