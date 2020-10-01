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
    public partial class CotizacionAire : ContentPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        public CotizacionAire()
        {
            InitializeComponent();
            //string estado = await SecureStorage.GetAsync("estado");
            //string ciudad = await SecureStorage.GetAsync("ciudad");
            //string direccion = await SecureStorage.GetAsync("direccion");

            Estado.SelectedItem = "Lara";
            Ciudad.Title = "Barquisimeto";
            Direccion.Placeholder = "Esta es mi direccion";
        }

        private async void Solicitar_Clicked(object sender, EventArgs e)
        {
            servicio servicio = new servicio();
            servicio.id_servicio = 4;
            servicio.descripcion = "Filtro de aire";
            servicio.status = "Cotizacion";
            try
            {

                Repositorio repositorio = new Repositorio();
                servicio serv = repositorio.postServicio(servicio).Result;

                try
                {

                    // Guarda los datos de la cotizacion en el securestorage para mostrarlos mas adelante y/o enviarlos a la BD
                    await SecureStorage.SetAsync("direccion_cotizacion", Direccion.Text);
                    await SecureStorage.SetAsync("estado_cotizacion", Estado.SelectedItem.ToString());
                    await SecureStorage.SetAsync("ciudad_cotizacion", Ciudad.SelectedItem.ToString());

                    Dialogs.ShowLoading("Tu cotización se ha agregado correctamente,");
                    await Task.Delay(2000);

                    // Obtiene los datos del usuario para enviarlo a la pagina Menu
                    var username = SecureStorage.GetAsync("username".ToString());
                    var lastname = SecureStorage.GetAsync("userlastname").ToString();

                    Menu myHomePage = new Menu(username + " " + lastname);
                    NavigationPage.SetHasNavigationBar(myHomePage, false);
                    await Navigation.PushModalAsync(myHomePage);

                    Dialogs.HideLoading();
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

    }
}