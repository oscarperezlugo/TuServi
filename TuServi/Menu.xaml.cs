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
    public partial class Menu : MasterDetailPage
    {
        public Menu(string nombreUsuario)
        {
            InitializeComponent();
            username.Text = nombreUsuario;
            MyMenu();
            lablClicked();
            Navigation.PopAsync(false);
        }

        public void MyMenu()
        {
            Detail = new NavigationPage(new Garage());
            List<MenuPrincipal> menu = new List<MenuPrincipal>
            {
                new MenuPrincipal{ Page = new Garage(), MenuTitle="Garage", icon="garage.png"},
                new MenuPrincipal{ Page = new Perfil(), MenuTitle="Opciones", icon="options.png"},
                new MenuPrincipal{ Page = new Garage(), MenuTitle="Contáctanos", icon="contact.png"},
                new MenuPrincipal{ Page = new Perfil(), MenuTitle="Carrito", icon="shopping.png"},
            };
            ListMenu.ItemsSource = menu;
        }

        private void ListMenu_ItemSelected(Object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as MenuPrincipal;
            if(menu != null)
            {
                IsPresented = false;
                Detail = new NavigationPage(menu.Page);
            }
        }

        public class MenuPrincipal
        {
            public String MenuTitle
            {
                set;
                get;
            }
            public String MenuDetail
            {
                get;
                set;
            }
            public ImageSource icon
            {
                get;
                set;
            }
            public Page Page
            {
                get;
                set;
            }
        }

        void lablClicked()
        {
            labelClicked.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    MainPage myHomePage = new MainPage();
                    NavigationPage.SetHasNavigationBar(myHomePage, false);
                    Navigation.PushModalAsync(myHomePage);
                })
            });
        }
    }
}