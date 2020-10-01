using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuServi.Conexiones;
using TuServi.Datos;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarVehiculos : ContentPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        public AgregarVehiculos()
        {
            InitializeComponent();
            lablClicked();

            ListPicker();
            listAnno();

        }
        private async void Registro_Clicked(object sender, EventArgs e)
        {
            if (Marca.SelectedItem.ToString() != null && Modelo.SelectedItem.ToString() != null && KilometrajeUlt.Text != null && Año.SelectedItem.ToString() != null)
            {
                Datos.Vehiculo vehiculo = new Datos.Vehiculo();
                vehiculo.anho = Int16.Parse(Año.SelectedItem.ToString());
                vehiculo.fecha_servicio = Fecha.Date;
                vehiculo.km_ultimo_servicio = Int32.Parse(KilometrajeUlt.Text);
                vehiculo.marca = Marca.SelectedItem.ToString();
                vehiculo.modelo = Modelo.SelectedItem.ToString();
                vehiculo.tipo_aceite = Aceite.SelectedItem.ToString() + "*" + SAE.SelectedItem.ToString(); ;
                // Int32.Parse(NIV.Text);
                // vehiculo.km_actual = Int32.Parse(Kilometraje.Text);
                try
                {

                    Repositorio repositorio = new Repositorio();
                    Datos.Vehiculo vehiculor = repositorio.postVehiculo(vehiculo).Result;
                    try
                    {

                        Dialogs.ShowLoading("Tu " + Modelo.SelectedItem.ToString() + " ya fue agregado a tu garage");
                        await Task.Delay(2000);

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
            else
            {
                Dialogs.ShowLoading("Complete los datos");
                await Task.Delay(2000);
                Dialogs.HideLoading();
            }
        }

        //Items de marca y modelo
        void ListPicker()
        {
            List<string> marcaList = new List<string>();
            marcaList.Add("Ford");
            marcaList.Add("Toyota");
            marcaList.Add("Fiat");
            marcaList.Add("Kia");
            marcaList.Add("Mazda");
            marcaList.Add("Renault");
            marcaList.Add("Chevrolet");

            Marca.ItemsSource = marcaList;

            List<string> modeloList = new List<string>();
            modeloList.Add("Mustang GT");
            modeloList.Add("Palio");
            modeloList.Add("Mazda 3");
            modeloList.Add("Corolla");
            modeloList.Add("Chevrolet");
            modeloList.Add("Corsa");

            Modelo.ItemsSource = modeloList;
        }

        //Para llenar el picker de año
        void listAnno()
        {
            var minYear = System.DateTime.Now.Year - 40;
            var maxYear = System.DateTime.Now.Year + 1;

            List<int> annoList = new List<int>();

            for(int i = minYear; i< maxYear; i++)
            {
                minYear = minYear + 1;
                annoList.Add(minYear);
            }

            Año.ItemsSource = annoList;
        }

        //Popup NIV
        void lablClicked()
        {
            lblClick.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    var page = new NivPopup();
                    PopupNavigation.Instance.PushAsync(page);

                })
            });


        }

    }
}