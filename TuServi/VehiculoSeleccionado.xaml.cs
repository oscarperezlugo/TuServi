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
    public partial class VehiculoSeleccionado : TabbedPage
    {
        public VehiculoSeleccionado()
        {
            InitializeComponent();
        }


       /* private async void Shoppingcar_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Carrito()));
        }*/
    }
}