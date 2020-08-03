using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaSeleccion : ContentPage
    {
        public PaginaSeleccion()
        {
            InitializeComponent();
            Navigation.PopAsync(false);

            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                MainPage myHomePage = new MainPage();
                NavigationPage.SetHasNavigationBar(myHomePage, false);
                Navigation.PushModalAsync(myHomePage);

                return false; // True = Repeat again, False = Stop the timer
            });
        }
        public void EmpresaClicked(object sender, EventArgs e)
        {            
            LoginEmpresa myHomePage = new LoginEmpresa();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            Navigation.PushModalAsync(myHomePage);
        }
        public void UsuarioClicked(object sender, EventArgs e)
        {
            

        }
    }
}