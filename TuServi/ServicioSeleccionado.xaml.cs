using System;
using System.Collections.Generic;
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
    public partial class ServicioSeleccionado : ContentPage
    {

       // string progresskm;
        string nameService;
        public ServicioSeleccionado()
        {
            InitializeComponent();
            CheckService();
            List();
            FechaProximo.Text = Application.Current.Properties["fecha_proximo_servicio"].ToString();
            nameService = Application.Current.Properties["service_name"].ToString();
            ConvertirKm();
        
        }

        public String ConvertirKm()
        {
            var km = Convert.ToDouble(Application.Current.Properties["km_ultimo_servicio"]);
            double pkm = km / 3000;
            String progresskm = Convert.ToString(pkm);
            return progresskm;
        }

        public void List()
        {
            List<ListaTiempo> trans = new List<ListaTiempo>
            {
                new ListaTiempo{ ItemTittle="Tiempo transcurrido", tiempo = Application.Current.Properties["total_months"] + " meses", progrest = Application.Current.Properties["progress"].ToString()},
                new ListaTiempo{ ItemTittle="Km recorrido", progrest = ConvertirKm(), tiempo = Application.Current.Properties["km_ultimo_servicio"] + " km "},
            }; Lista.ItemsSource = trans;
        }

        private async void SolicitarCot_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine(" ================== SOLICITAR CLICKED " + nameService);
            if (nameService == "Aceite")
            { 
                SolicitarCotizacion myHomePage = new SolicitarCotizacion();
                await Navigation.PushModalAsync(new NavigationPage(myHomePage));

            } else if ( nameService == "Filtro de aire")
            {
                CotizacionAire myHomePage = new CotizacionAire();
                await Navigation.PushModalAsync(new NavigationPage(myHomePage));

            } else if (nameService == "Filtro de gasolina")
            {
                CotizacionGasolina myHomePage = new CotizacionGasolina();
                await Navigation.PushModalAsync(new NavigationPage(myHomePage));

            } else
            {
                CotizacionCauchos myHomePage = new CotizacionCauchos();
                await Navigation.PushModalAsync(new NavigationPage(myHomePage));
            }
        }

        // Revisa si hay cotizaciones
        public void CheckService()
        {
            Repositorio repo = new Repositorio();
            servicio[] servicios = repo.getServicios().Result;

            if (servicios.Length == 0)
            {
                Console.WriteLine(" ================== NO HAY COTIZACIONES ============ ");
                SolicitarCot.IsVisible = true;
                CotizacionActiva.IsVisible = false;
            }
            else
            {
                for (var i = 0; i < servicios.Length; i++)
                {
                    if (servicios[i].status == "Cotizacion")
                    {
                        SolicitarCot.IsVisible = false;
                        CotizacionActiva.IsVisible = true;

                    }
                    else
                    {
                        SolicitarCot.IsVisible = true;
                        CotizacionActiva.IsVisible = false;
                    }
                }
            }
        }
        private async void VerCotizacion_Clicked(Object sender, EventArgs e)
        {
            Cotizaciones myHomePage = new Cotizaciones();
            await Navigation.PushModalAsync(new NavigationPage(myHomePage));
        }

        public class ListaTiempo
        {
            public String ItemTittle
            {
                set;
                get;
            }
            public String tiempo
            {
                get;
                set;
            }
            public String progrest
            {
               get;
               set;
            }
            public Page Page
            {
                get;
                set;
            }
        }
    }
}