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
        public async Task<Vehiculo> postVehiculo(Vehiculo vehiculo)
        {
            var id = await SecureStorage.GetAsync("id");
            var guid = await SecureStorage.GetAsync("guid");
            Vehiculo vehiculor = new Vehiculo();
            vehiculor.anho = vehiculo.anho;
            vehiculor.fecha_servicio = vehiculo.fecha_servicio;
            vehiculor.km_ultimo_servicio = vehiculo.km_ultimo_servicio;
            vehiculor.marca = vehiculo.marca;
            vehiculor.modelo = vehiculo.modelo;
            vehiculor.tipo_aceite = vehiculo.tipo_aceite;
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
                Vehiculo result = JsonConvert.DeserializeObject<Vehiculo>(dataResult);
                return result;
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
    }
}
