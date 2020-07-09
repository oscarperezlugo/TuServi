using Acr.UserDialogs;
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
    public partial class AgregarVehiculos : ContentPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        public AgregarVehiculos()
        {
            InitializeComponent();
        }
        private async void Registro_Clicked(object sender, EventArgs e)
        {
            if (Marca.Text != null && Modelo.Text != null && Año.Text != null && Kilometros.Text != null)
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculo.anho = Int16.Parse(Año.Text);
                vehiculo.fecha_servicio = Fecha.Date;
                vehiculo.km_ultimo_servicio = Int32.Parse(Kilometraje.Text);
                vehiculo.marca = Marca.Text;
                vehiculo.modelo = Modelo.Text;
                vehiculo.tipo_aceite = Aceite.SelectedItem.ToString();
                try
                {

                    Repositorio repositorio = new Repositorio();
                    Vehiculo vehiculor = repositorio.postVehiculo(vehiculo).Result;
                    Dialogs.ShowLoading("Tu " + Modelo.Text + " ya fue agregada a tu garage");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    try
                    {
                        Garage myHomePage = new Garage();
                        NavigationPage.SetHasNavigationBar(myHomePage, false);
                        await Navigation.PushModalAsync(myHomePage);
                    }
                    catch (Exception ex)
                    {
                        Dialogs.ShowLoading("Error " + ex.Message + "");
                        await Task.Delay(2000);
                        Dialogs.HideLoading();
                    }
                }

                catch (Exception ex)
                {
                    await DisplayAlert("Registro Error", ex.Message, "Intente de nuevo mas tarde");

                }

            }
            else
            {
                Dialogs.ShowLoading("Complete los datos");
                await Task.Delay(2000);
                Dialogs.HideLoading();
            }
        }
    }
}