using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Guantera : ContentPage
    {
        string sobrenombre;
        string odometrohoy, cambioaceite, cambiofiltroaceite, cambiofiltroaire;

        IUserDialogs Dialogs = UserDialogs.Instance;
        public Guantera()
        {
            InitializeComponent();

            string marca = Application.Current.Properties["marca"].ToString();
            string modelo = Application.Current.Properties["modelo"].ToString();
            //string anho = Application.Current.Properties["anho"].ToString();


            if (Application.Current.Properties.ContainsKey("sobrenombre"))
            {
                sobrenombre = Application.Current.Properties["sobrenombre"].ToString();
            }
            if (Application.Current.Properties.ContainsKey("odometrohoy"))
            {
                odometrohoy = Application.Current.Properties["odometrohoy"].ToString();
            }
            if (Application.Current.Properties.ContainsKey("cambioaceite"))
            {
                cambioaceite = Application.Current.Properties["cambioaceite"].ToString();
            }
            if (Application.Current.Properties.ContainsKey("cambiofiltroaceite"))
            {
                cambiofiltroaceite = Application.Current.Properties["cambiofiltroaceite"].ToString();
            }
            if (Application.Current.Properties.ContainsKey("cambiofiltroaire"))
            {
                cambiofiltroaire = Application.Current.Properties["cambiofiltroaire"].ToString();
            }
            Marca.Text = marca;
            Modelo.Text = modelo;
            //Año.Text = anho;
            Sobrenombre.Text = sobrenombre;
            Odometrohoy.Text = odometrohoy;
            Cambioaceite.Text = cambioaceite;
            Cambiofiltroaceite.Text = cambiofiltroaceite;
            Cambiofiltroaire.Text = cambiofiltroaire;

            lablClicked();
        }
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

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Sobrenombre.Text != null)
                {
                    await SecureStorage.SetAsync("sobrenombre" + "1", Sobrenombre.Text);
                }
                if (Odometrohoy.Text != null)
                {
                    await SecureStorage.SetAsync("odometrohoy" + "1", Odometrohoy.Text);
                }
                if (Cambioaceite.Text != null)
                {
                    await SecureStorage.SetAsync("cambioaceite" + "1", Cambioaceite.Text);
                }
                if (Cambiofiltroaceite.Text != null)
                {
                    await SecureStorage.SetAsync("cambiofiltroaceite", Cambiofiltroaceite.Text);
                }
                if (Cambiofiltroaire.Text != null)
                {
                    await SecureStorage.SetAsync("cambiofiltroaire", Cambiofiltroaire.Text);
                }
            }
            catch (Exception ex)
            {
                Dialogs.ShowLoading("Error " + ex + "");
                await Task.Delay(2000);
                Dialogs.HideLoading();
            }
        }
    }
}