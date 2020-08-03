using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuServi.Conexiones;
using TuServi.Datos;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TuServi
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        public MainPage()
        {
            InitializeComponent();
            lablClicked();

            Navigation.PopAsync(true);

        }
        private async void Registrarse_Clicked(object sender, EventArgs e)
        {
            Registro myHomePage = new Registro();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            if (nombredeusuario.Text != null && contrasena.Text != null)
            {
                Usuario usuariologin = new Usuario();
                usuariologin.correo = nombredeusuario.Text;
                usuariologin.contrasena = contrasena.Text;

                
                try
                {

                    Repositorio repositorio = new Repositorio();
                    Usuario userlogin = repositorio.postLogin(usuariologin).Result;

                    Usuario user = repositorio.getUsuario().Result;

                    Dialogs.ShowLoading("Bienvenido "+nombredeusuario.Text+""); ;
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    try
                    {                        
                        await SecureStorage.SetAsync("id", userlogin.iDu.ToString());
                        await SecureStorage.SetAsync("guid", userlogin.iD.ToString());                        
                        Menu myHomePage = new Menu(user.nombre + " " + user.apellido);
                        NavigationPage.SetHasNavigationBar(myHomePage, false);
                        await Navigation.PushModalAsync(myHomePage);
                    }
                    catch (Exception ex)
                    {
                        Dialogs.ShowLoading("Error "+ex+""); 
                        await Task.Delay(2000);
                        Dialogs.HideLoading();
                    }                    
                }

                catch (Exception ex)
                {
                    await DisplayAlert("Login Error", ex.Message, "Intente de nuevo mas tarde");

                }
            }
            
        }

        void lablClicked()
        {
            labelClick.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Garage myHomePage = new Garage();
                    NavigationPage.SetHasNavigationBar(myHomePage, false);
                    Navigation.PushModalAsync(myHomePage);
                })
            });
        }

            private async void Password_Clicked(object sender, EventArgs e)
        {
            //Aqui va el push hacia la pagina de recuperar contraseña
            Garage myHomePage = new Garage();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }

    }
}
