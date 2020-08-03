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
    public partial class Registro : CarouselPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        public Registro()
        {
            InitializeComponent();
            lablClicked();
        }
        public void Siguiente_Clicked(object sender, EventArgs e)
        {
            CurrentPage = paso2;

        }
        private async void Registro_Clicked(object sender, EventArgs e)
        {
            if (email.Text != null && contrasena.Text == conficontrasena.Text)
            {
                Usuario usuario = new Usuario();
                usuario.nombre = nombre.Text;
                usuario.apellido = "" + estado.Text + " " + ciudad.Text + " " + direccion.Text + "";
                usuario.correo = email.Text;
                usuario.telefono = telefono.Text;
                usuario.nombre_usuario = email.Text;
                usuario.contrasena = contrasena.Text;
                //Usuario usuario = new Usuario();
                //usuario.nombre = "Juan";
                //usuario.apellido = "El Trigal Valencia Venezuela";
                //usuario.correo = "juan@gmail.com";
                //usuario.telefono = "123456789";
                //usuario.nombre_usuario = "juan@gmail.com";
                //usuario.contrasena = "123456";
                try
                {

                    Repositorio repositorio = new Repositorio();
                    Usuario usuarior = repositorio.postUsuario(usuario).Result;

                   // Usuario user = repositorio.getUsuario(usuarior.guid.ToString());

                    Dialogs.ShowLoading("Hola " + nombre.Text + " Bienvenido a Servi");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    try
                    {
                        await SecureStorage.SetAsync("id", usuarior.id_usuario.ToString());
                        await SecureStorage.SetAsync("guid", usuarior.guid.ToString());
                        Menu myHomePage = new Menu(usuarior.nombre + " " + usuarior.apellido);
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
                Dialogs.ShowLoading("Revise los datos");
                await Task.Delay(2000);
                Dialogs.HideLoading();
            }
        }

        void lablClicked()
        {
            lblClick.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("Task", "Now it's working ", "OK");
                })
            });
            lablClick.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("Registro exito", "Bienvenido/a a Servi ", "OK");
                })
            });
        }
    }
}