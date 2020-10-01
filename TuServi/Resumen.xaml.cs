using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuServi.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Resumen : ContentPage
    {
        public Resumen()
        {
            InitializeComponent();
            GetProgressContent();
            string marca = Application.Current.Properties["marca"].ToString();
            string modelo = Application.Current.Properties["modelo"].ToString();
            string km_ultimo_servicio = Application.Current.Properties["km_ultimo_servicio"].ToString();
            Marca.Text = marca;
            Modelo.Text = modelo;
            Km_ultimo.Text = km_ultimo_servicio + " Km";
            ServiciosList();
        }


        public void ServiciosList()
        {

            List<ListaServicios> serv = new List<ListaServicios>
            {
                new ListaServicios{ Servicio="Aceite", icon="oil.png", progress = GetProgressContent()},
                new ListaServicios{ Servicio="Rotación de cauchos", icon="tire.png", progress = GetProgressContent()},
                new ListaServicios{ Servicio="Filtro de gasolina", icon="fuel.png", progress = GetProgressContent()},
                new ListaServicios{ Servicio="Filtro de aire", icon="wind.png", progress = GetProgressContent()},
            };
            Lista.ItemsSource = serv;
        }

        private void ListServ_ItemSelected(Object sender, SelectedItemChangedEventArgs e)
        {
            var servi = e.SelectedItem as ListaServicios;

            // send service name to current properties for use it in next pages
            Application.Current.Properties["service_name"] = servi.Servicio;
            Console.WriteLine("####### SERVICENAME " + Application.Current.Properties["service_name"].ToString());
            ServicioSeleccionado myHomePage = new ServicioSeleccionado();
            myHomePage.BindingContext = servi;
            Navigation.PushModalAsync(new NavigationPage(myHomePage));
        }

        public class ListaServicios
        {
            public String Servicio
            {
                set;
                get;
            }
            public ImageSource icon
            {
                get;
                set;
            }

            public Page Page
            {
                get;
                set;
            }

            public double progress
            {
                get;
                set;
            }
        }


        public double GetProgressContent() // Calculate total days from ultimate service to current date
        {

            string fecha_ultimo_servicio = Application.Current.Properties["fecha_ultimo_servicio"].ToString();
            DateTime date;
            bool isSuccess = DateTime.TryParse(fecha_ultimo_servicio, out date);

            Application.Current.Properties["fecha_proximo_servicio"] = date.AddDays(60).ToShortDateString();
            
            DateTime currentday;
            bool Success = DateTime.TryParse(DateTime.Now.ToString(), out currentday);

            var result = (currentday - date).TotalDays;
            var months = result / 30;
            Application.Current.Properties["total_months"] = months.ToString("F1");
            var progre =  result / 60;
            Application.Current.Properties["progress"] = progre;
            return progre;
        }


    }


}