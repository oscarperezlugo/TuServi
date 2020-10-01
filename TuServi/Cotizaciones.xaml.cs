using Acr.UserDialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Cotizaciones : ContentPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;

        int producto = 0;
        //string venta;
        //string fecha;
        int mult = 0; // Resultado de la cantidad x precio

        servicio servicio_actual = new servicio();

        private ObservableCollection<LineCar> _linea;
        public ObservableCollection<LineCar> LineasCar
        {
            get
            {
                return _linea ?? (_linea = new ObservableCollection<LineCar>());
            }
        }



        public Cotizaciones()
        {
            InitializeComponent();
            CheckServices();
            string marca = Application.Current.Properties["marca"].ToString();
            string modelo = Application.Current.Properties["modelo"].ToString();
            string anho = Application.Current.Properties["anho"].ToString();
            Marca.Text = marca;
            Modelo.Text = modelo;
            Año.Text = anho;
        }

        public void CheckServices()
        {

            Console.WriteLine("METODO ");
            Repositorio repo = new Repositorio();
            servicio[] servicios = repo.getServicios().Result;

            int counter = 0;

            for (var i = 0; i < servicios.Length; i++)
            {
                
                if (servicios[i].status == "Cotizacion")
                {
                    // Guarda los datos del servicio actual para utilizarlos en el metodo put
                    servicio_actual.id_servicio = servicios[i].id_servicio;
                    servicio_actual.id_usuario = servicios[i].id_usuario;
                    servicio_actual.guid = servicios[i].guid;
                    servicio_actual.tiempo = servicios[i].tiempo;
                    servicio_actual.descripcion = servicios[i].descripcion;

                    counter++;

                    Console.WriteLine("SERVICIOS " + servicios[i].id_servicio.ToString() + " " + servicios[i].status.ToString());
                    
                    if (servicios[i].descripcion == "Aceite")
                    {
                        Aceite.IsVisible = true;
                        Botones.IsVisible = true;
                        TotalC.IsVisible = true;
                        Console.WriteLine(" ========== COTIZACION DE ACEITE " + servicios[i].id_servicio + servicios[i].status);

                    }
                    else if (servicios[i].descripcion == "Rotacion de cauchos")
                    {
                        Cauchos.IsVisible = true;
                        Botones.IsVisible = true;
                        TotalC.IsVisible = true;
                        Console.WriteLine(" ========== COTIZACION DE CAUCHOS " + servicios[i].id_servicio + servicios[i].status);

                    }
                    else if (servicios[i].descripcion == "Filtro de gasolina")
                    {
                        Gasolina.IsVisible = true;
                        Botones.IsVisible = true;
                        TotalC.IsVisible = true;
                        Console.WriteLine(" ========== COTIZACION DE ACEITE " + servicios[i].id_servicio + servicios[i].status);

                    }
                    else if (servicios[i].descripcion == "Filtro de aire")
                    {
                        Aire.IsVisible = true;
                        Botones.IsVisible = true;
                        TotalC.IsVisible = true;
                        Console.WriteLine(" ========== COTIZACION DE AIRE " + servicios[i].id_servicio + servicios[i].status);

                    }
                } 
            }
            if (counter == 0)
            {
                    image.IsVisible = true;
                    Aceite.IsVisible = false;
                    Aire.IsVisible = false;
                    Cauchos.IsVisible = false;
                    Gasolina.IsVisible = false;
                    Botones.IsVisible = false;
                    Cantidad.IsVisible = false;
                    CantidadAire.IsVisible = false;
                    CantidadCauchos.IsVisible = false;
                    CantidadGasolina.IsVisible = false;
                    TotalC.IsVisible = false;
                    Console.WriteLine("NO HAY COTIZACIONES ");
            }


        }

    // Calcula el precio total de compra de acuerdo a la cantidad que coloque el usuario

        private void CantidadChanged(Object sender, TextChangedEventArgs e)
        {
            if ( Aceite.IsVisible == true)
            {
                if(Cantidad.Text == "")
                {
                    TotalCotizacion.Text = "$" + 0;
                }
                else
                {
                    var cantidad = Convert.ToInt32(Cantidad.Text);
                    mult = cantidad * 5;
                    TotalCotizacion.Text = "$" + mult;
                }
            }
            else if ( Aire.IsVisible == true)
            {
                var cantidad = Convert.ToInt32(CantidadAire.Text);
                mult = cantidad * 5;
                TotalCotizacion.Text = "$" + mult;
            }
            else if ( Cauchos.IsVisible == true)
            {
                var cantidad = Convert.ToInt32(CantidadCauchos.Text);
                mult = cantidad * 5;
                TotalCotizacion.Text = "$" + mult;
            }
            else
            {
                var cantidad = Convert.ToInt32(CantidadGasolina.Text);
                mult = cantidad * 5;
                TotalCotizacion.Text = "$" + mult;
            }
        }

    // Borra la cotizacion de la BD
        private async void Rechazar_Clicked(Object sender, EventArgs e)
        {

            try
            {

                Repositorio repositorio = new Repositorio();
                await repositorio.deleteServicio();

                Dialogs.ShowLoading("Tu cotización fue rechazada con exito");
                await Task.Delay(2000);
                CheckServices();
                Dialogs.HideLoading();
                
            } 
            catch (Exception ex)
            {
                Console.WriteLine("######error ");
                Dialogs.ShowLoading("Error " + ex.Message + "");
                await Task.Delay(2000);
                Dialogs.HideLoading();
            }
        }

    // Agrega el producto al carrito
        private async void Agregar_Clicked(Object sender, EventArgs e)
        {
            Console.WriteLine("===== AGREGAR CLICKED =======");
            var grado = await SecureStorage.GetAsync("grado_sae_cotizacion");
            var tipoaceite = await SecureStorage.GetAsync("tipo_aceite_cotizacion");
            producto = producto + 1;

            /* Verificando que sea el primer producto del carrito para hacer la cabecera */

            Repositorio repositorio = new Repositorio();
            /*if ( producto == 1)
            {
                CabeceraFacturas cabeceraf = new CabeceraFacturas();
                cabeceraf.venta = DateTime.Now.Ticks.ToString();
                venta = cabeceraf.venta;
                cabeceraf.fecha = DateTime.Now;
                fecha = DateTime.Now.ToString();
                try
                {

                    CabeceraFacturas cabecera = repositorio.postCabecera(cabeceraf).Result;
                }
                catch
                {

                }
            }*/

            if (Aceite.IsVisible == true)
            {
                Console.WriteLine("===== AGREGAR CLICKED ACEITE =======");

                var linea = new LineCar()
                {
                    descripcion = tipoaceite + " " + grado + " " + MarcaAceite.SelectedItem + " (" + CapacidadAceite.SelectedItem + ")",
                    cantidad = Cantidad.Text,
                    precio = Convert.ToString(mult)
                };

                LineasCar.Add(linea);

                Console.WriteLine("===== COTIZACION AGREGADA SERVICE " + servicio_actual.descripcion);

                //Actualiza el estado del servicio de cotizacion a agregado para mostrarlo en el historial de servicios
                try
                {
                    servicio serv =  repositorio.putServicio(servicio_actual).Result;

                    try
                    {

                        Dialogs.ShowLoading("¡Tu producto se ha agregado al carrito!");
                        await Task.Delay(2000);

                        Carrito myHomePage = new Carrito(LineasCar);
                        await Navigation.PushModalAsync(new NavigationPage(myHomePage));

                        Dialogs.HideLoading();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("###### ERROR AL ACTUALIZAR LA COTIZACION ");
                        Dialogs.ShowLoading("Error " + ex.Message + "");
                        await Task.Delay(3000);
                        Dialogs.HideLoading();
                    }
                } catch (Exception ex)
                    {
                        Console.WriteLine("###### ERROR AL ACTUALIZAR LA COTIZACION ");
                        Dialogs.ShowLoading("Error " + ex.Message + "");
                        await Task.Delay(3000);
                        Dialogs.HideLoading();
                    }
                


                /*LineasFacturas lineas = new LineasFacturas();
                lineas.producto = "Aceite " + sae + " " + MarcaAceite.SelectedItem + " " + CapacidadAceite.SelectedItem;
                lineas.venta = venta;
                lineas.precio = mult;
                lineas.cantidad = Cantidad.Text;
                try
                {
                    LineasFacturas line = repositorio.postLinea(lineas).Result;
                }
                catch
                {

                }*/
            } else if (Aire.IsVisible == true)
            {
                var linea = new LineCar()
                {
                    descripcion = "Filtro de aire " + MarcaFiltroAire.SelectedItem,
                    cantidad = CantidadAire.Text,
                    precio = Convert.ToString(mult)
                };

                LineasCar.Add(linea);

                Dialogs.ShowLoading("¡Tu producto se ha agregado al carrito!");
                await Task.Delay(2000);

                Carrito myHomePage = new Carrito(LineasCar);
                await Navigation.PushModalAsync(new NavigationPage(myHomePage));

                Dialogs.HideLoading();

                /*LineasFacturas lineas = new LineasFacturas();
                lineas.producto = "Filtro de aire " + MarcaFiltroAire.SelectedItem;
                lineas.venta = venta;
                lineas.precio = mult;
                lineas.cantidad = CantidadAire.Text;

                try
                {
                    LineasFacturas line = repositorio.postLinea(lineas).Result;
                }
                catch
                {

                }*/
            } else if (Cauchos.IsVisible == true )
            {
                var linea = new LineCar()
                {
                    descripcion = "Rotacion de cauchos " + MarcaCauchos.SelectedItem,
                    cantidad = CantidadCauchos.Text,
                    precio = Convert.ToString(mult)
                };

                LineasCar.Add(linea);

                Console.WriteLine("======= COUNT LINEAS ==== " + LineasCar.Count);

                Dialogs.ShowLoading("¡Tu producto se ha agregado al carrito!");
                await Task.Delay(2000);

                Carrito myHomePage = new Carrito(LineasCar);
                await Navigation.PushModalAsync(new NavigationPage(myHomePage));

                Dialogs.HideLoading();

                /*LineasFacturas lineas = new LineasFacturas();
                lineas.producto = "Rotacion de cauchos " + MarcaCauchos.SelectedItem;
                lineas.venta = venta;
                lineas.precio = mult;
                try
                {
                    LineasFacturas line = repositorio.postLinea(lineas).Result;
                }
                catch
                {

                }*/
            } else
            {
                var linea = new LineCar()
                {
                    descripcion = "Filtro de gasolina " + MarcaFiltroGasolina.SelectedItem,
                    cantidad = CantidadGasolina.Text,
                    precio = Convert.ToString(mult)
                };

                LineasCar.Add(linea);


                Console.WriteLine("======= COUNT LINEAS ==== " + LineasCar.Count);

                Dialogs.ShowLoading("¡Tu producto se ha agregado al carrito!");
                await Task.Delay(2000);

                Carrito myHomePage = new Carrito(LineasCar);
                await Navigation.PushModalAsync(new NavigationPage(myHomePage));

                Dialogs.HideLoading();


                /*LineasFacturas lineas = new LineasFacturas();
                lineas.producto = "Filtro de gasolina " + MarcaFiltroGasolina.SelectedItem;
                lineas.venta = venta;
                lineas.precio = mult;
                try
                {
                    LineasFacturas line = repositorio.postLinea(lineas).Result;
                }
                catch
                {

                }*/
            }
        }

    }
}