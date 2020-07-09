using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TuServi
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginEmpresa : ContentPage
    {
        public LoginEmpresa()
        {
            InitializeComponent();
        }
        private async void Registrarse_Clicked(object sender, EventArgs e)
        {
            RegistroEmpresa myHomePage = new RegistroEmpresa();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            AgregarVehiculos myHomePage = new AgregarVehiculos();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
    }
}
