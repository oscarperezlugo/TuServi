using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carrito : ContentPage
    {
        float totalCompra;


        public Carrito(ObservableCollection<LineCar> LineasCar)
        {
            InitializeComponent();
            Console.WriteLine("==================== CARRITO ========= ");
            ListaProductos.ItemsSource = LineasCar;

            foreach (var item in LineasCar)
            {
                totalCompra = Convert.ToInt64(item.precio);
            }
        }

        public class ListProduct
        {
            public String descripcion
            {
                set;
                get;
            }
            public ImageSource cantidad
            {
                get;
                set;
            }

            public int precio
            {
                get;
                set;
            }


        }


        private async void Totalizar_Clicked(object sender, EventArgs e)
        {
            /*Existe la posibilidad de que desde aqui guarde lineas y cabecera en la bd*/

            await SecureStorage.SetAsync("total_compra", totalCompra.ToString());

            Totalizar myHomePage = new Totalizar();
            await Navigation.PushModalAsync(new NavigationPage(myHomePage));
        }
    }
}