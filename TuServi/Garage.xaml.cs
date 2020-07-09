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
    public partial class Garage : ContentPage
    {
        Vehiculo[] Vehiculo;
        
        public Garage()
        {
            InitializeComponent();
            Repositorio repo = new Repositorio();
            Vehiculo[] listavehiculo = repo.getVehiculo().Result;
            ListaVehiculos.ItemsSource = listavehiculo;
            Vehiculo = listavehiculo;
        }
        private async void Agregar_Clicked(object sender, EventArgs e)
        {
            AgregarVehiculos myHomePage = new AgregarVehiculos();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
    }
}