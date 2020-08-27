using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuServi.Conexiones;
using TuServi.Datos;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Garage : ContentPage
    {
        Vehiculo[] Vehiculo;

        public Garage()
        {
            InitializeComponent();
            Repositorio repo = new Repositorio();
            Vehiculo[] listavehiculo = repo.getVehiculo().Result;
            System.Diagnostics.Debug.WriteLine(listavehiculo.Length);
            if (listavehiculo.Length == 0)
            {
                ListaVehiculos.IsVisible = false;
                image.Source = "addcar.jpg";
            }
            else
            {
                ListaVehiculos.ItemsSource = listavehiculo;
                Vehiculo = listavehiculo;
                image.IsVisible = false;
            }
            Navigation.PopAsync(true);
        }
        private async void Agregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AgregarVehiculos())); //Para que se muestre la barra de navegacion en la pagina
        }

        private async void Configurar_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Perfil()));
        }

        private void Vehiculo_Selected(object sender, ItemTappedEventArgs e)
        {
            Vehiculo selected = (Vehiculo)((ListView)sender).SelectedItem;
            Application.Current.Properties["fecha_ultimo_servicio"] = selected.fecha_servicio;
            Application.Current.Properties["marca"] = selected.marca;
            Application.Current.Properties["modelo"] = selected.modelo;
            Application.Current.Properties["km_ultimo_servicio"] = selected.km_ultimo_servicio.ToString();
            Application.Current.SavePropertiesAsync();

            Navigation.PushModalAsync(new VehiculoSeleccionado());
        }

    }

}