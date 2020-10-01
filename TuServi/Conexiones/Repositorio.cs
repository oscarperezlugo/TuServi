using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TuServi.Datos;
using Xamarin.Essentials;

namespace TuServi.Conexiones
{


    class Repositorio
    {



        public async Task<Usuario> postLogin(Usuario usuario)
        {
            Usuario usuarior = new Usuario();
            usuarior.Pass = usuario.contrasena;
            usuarior.User = usuario.correo;
            var jsonObj = JsonConvert.SerializeObject(usuarior);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://servi.somee.com/api/login"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Usuario result = JsonConvert.DeserializeObject<Usuario>(dataResult);
                return result;
            }
        }

        public async Task<Vehiculo[]> getVehiculo()
        {
            var guids = await SecureStorage.GetAsync("guid");
            string guid = guids.ToString();

            try
            {

                Vehiculo[] vehiculo;
                var URLWebAPI = "https://servi.somee.com/api/vehiculos?guid=" + guid + "";
                using (var Client = new System.Net.Http.HttpClient())
                {
                    var JSON = Client.GetStringAsync(URLWebAPI);
                    vehiculo = Newtonsoft.Json.JsonConvert.DeserializeObject<Vehiculo[]>(JSON.Result);
                }

                return vehiculo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Datos.Vehiculo> postVehiculo(Datos.Vehiculo vehiculo)
        {
            var id = await SecureStorage.GetAsync("id");
            var guid = await SecureStorage.GetAsync("guid");
            Datos.Vehiculo vehiculor = new Datos.Vehiculo();
            vehiculor.anho = vehiculo.anho;
            vehiculor.fecha_servicio = vehiculo.fecha_servicio;
            vehiculor.km_ultimo_servicio = vehiculo.km_ultimo_servicio;
            vehiculor.marca = vehiculo.marca;
            vehiculor.modelo = vehiculo.modelo;
            vehiculor.tipo_aceite = vehiculo.tipo_aceite;
            //vehiculor.niv = vehiculo.niv;
            //vehiculor.sae = vehiculo.sae;
            //vehiculor.km_actual = vehiculo.km_actual;
            vehiculor.id_usuario = Int16.Parse(id);
            vehiculor.guid = new Guid(guid);
            var jsonObj = JsonConvert.SerializeObject(vehiculor);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://servi.somee.com/api/vehiculos"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Datos.Vehiculo result = JsonConvert.DeserializeObject<Datos.Vehiculo>(dataResult);
                return result;
            }
        }

        public async Task<servicio> deleteServicio()
        {
            var guid = await SecureStorage.GetAsync("guid");

            var jsonObj = JsonConvert.SerializeObject(guid);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://servi.somee.com/api/servicios?guid=" + guid + ""),
                    Method = HttpMethod.Delete
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                servicio result = JsonConvert.DeserializeObject<servicio>(dataResult);
                return result;
            }
        }

        // Actualiza el estado del servicio de cotizacion a agregado cuando el usuario agrega la cotizacion al carrito
        public async Task<servicio> putServicio(servicio service)
        {
            var guid = await SecureStorage.GetAsync("guid"); // guid del usuario
            servicio servic = new servicio();
            
            servic.id_servicio = service.id_servicio;
            servic.descripcion = service.descripcion;
            servic.tiempo = 1;
            // servic.tiempo = DateTime.Now.ToString(); para cuando tiempo de servicio sea string o date
            servic.id_usuario = service.id_usuario;
            servic.guid = service.guid;
            servic.status = "Agregado";

            var jsonObj = JsonConvert.SerializeObject(servic);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://servi.somee.com/api/servicios?guid="+guid+""),
                    Method = HttpMethod.Put,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                servicio result = JsonConvert.DeserializeObject<servicio>(dataResult);


                return result;
            }
        }

        public async Task<servicio> postServicio(servicio service)
        {
            var id = await SecureStorage.GetAsync("id");
            var guid = await SecureStorage.GetAsync("guid");

            servicio serv = new servicio();
            serv.id_servicio = service.id_servicio;
            serv.descripcion = service.descripcion;
            serv.status = service.status;
            serv.id_usuario = Int16.Parse(id);
            serv.guid = new Guid(guid);

            var jsonObj = JsonConvert.SerializeObject(serv);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://servi.somee.com/api/servicios"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                servicio result = JsonConvert.DeserializeObject<servicio>(dataResult);
                return result;
            }
        }

        public async Task<servicio[]> getServicios()
        {
            var guids = await SecureStorage.GetAsync("guid");
            string guid = guids.ToString();

            try
            {

                servicio[] serv;
                var URLWebAPI = "https://servi.somee.com/api/servicios?guid=" + guid + "";
                using (var Client = new System.Net.Http.HttpClient())
                {
                    var JSON = Client.GetStringAsync(URLWebAPI);
                    serv = Newtonsoft.Json.JsonConvert.DeserializeObject<servicio[]>(JSON.Result);
                }

                return serv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> postUsuario(Usuario usuario)
        {
            Usuario usuarior = new Usuario();
            usuarior.nombre = usuario.nombre;
            usuarior.apellido = usuario.apellido;
            usuarior.contrasena = usuario.contrasena;
            usuarior.correo = usuario.correo;
            usuarior.telefono = usuario.telefono;
            usuarior.nombre_usuario = usuario.nombre_usuario;
            usuarior.guid = Guid.NewGuid();
            var jsonObj = JsonConvert.SerializeObject(usuarior);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://servi.somee.com/api/usuarios"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Usuario result = JsonConvert.DeserializeObject<Usuario>(dataResult);
                return result;
            }
        }

        public Usuario getUsuario(string iD)
        {
            //var guids = await SecureStorage.GetAsync("guid");
            //string guid = guids.ToString();

            try
            {
                Usuario usuario;
                var url = "https://servi.somee.com/api/usuarios?guid=" + iD.Trim();
                using (var Client = new HttpClient())
                {
                    var JSON = Client.GetStringAsync(url);
                    usuario = JsonConvert.DeserializeObject<Usuario>(JSON.Result);
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<LineasFacturas> postLinea(LineasFacturas linea)
        {
            LineasFacturas lineafact = new LineasFacturas();
            lineafact.venta = linea.venta;
            lineafact.producto = linea.producto;
            lineafact.precio = linea.precio;
            lineafact.cantidad = linea.cantidad;

            var jsonObj = JsonConvert.SerializeObject(lineafact);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://servi.somee.com/api/LineasFacturas"),
                    Method = HttpMethod.Post,
                    Content = content
                };

                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                LineasFacturas result = JsonConvert.DeserializeObject<LineasFacturas>(dataResult);
                return result;
            }
        }


        public async Task<CabeceraFacturas> postCabecera(CabeceraFacturas cabecera)
        {
            CabeceraFacturas cabecerafact = new CabeceraFacturas();
            cabecerafact.cliente = cabecera.cliente;
            cabecerafact.direccion = cabecera.direccion;
            cabecerafact.venta = cabecera.venta;
            cabecerafact.fecha = cabecera.fecha;
            cabecerafact.total = cabecera.total;

            var jsonObj = JsonConvert.SerializeObject(cabecerafact);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://servi.somee.com/api/CabeceraFacturas"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                CabeceraFacturas result = JsonConvert.DeserializeObject<CabeceraFacturas>(dataResult);
                return result;
            }
        }
    }
}
