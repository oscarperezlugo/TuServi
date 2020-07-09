using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TuServi.Datos
{
    public class Datos
    {
        
    }
    public class Vehiculo
    {
        public int id_vehiculo { get; set; }
        [JsonProperty("marca")]
        public string marca { get; set; }
        [JsonProperty("modelo")]
        public string modelo { get; set; }
        public int anho { get; set; }
        public string tipo_aceite { get; set; }
        public Nullable<int> km_ultimo_servicio { get; set; }
        public Nullable<System.DateTime> fecha_servicio { get; set; }
        public int id_usuario { get; set; }
        public Nullable<System.Guid> guid { get; set; }

        
    }
    public class Usuario
    {

        public string User { get; set; }
        public string Pass { get; set; }
        public string token { get; set; }
        public string iD { get; set; }
        public string iDu { get; set; }
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string nombre_usuario { get; set; }
        public string contrasena { get; set; }
        public System.Guid guid { get; set; }
        
    }
    public class tiempo
    {
        public int id_tiempo { get; set; }
        public string servicio { get; set; }
        public System.DateTime fecha_inicio { get; set; }
        public Nullable<System.DateTime> fecha_limite { get; set; }
        public int id_usuario { get; set; }
        public Nullable<System.Guid> guid { get; set; }

        
    }
    public class servicio
    {
        public int id_servicio { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> tiempo { get; set; }
        public int id_usuario { get; set; }
        public Nullable<System.Guid> guid { get; set; }

        
    }
    public class producto
    {
        public int id_producto { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public Nullable<System.Guid> guid { get; set; }

       
    }
    public class empresa
    {
        public int id_empresa { get; set; }
        public string nombre { get; set; }
        public string rif { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public int id_usuario { get; set; }
        public Nullable<System.Guid> guid { get; set; }

        
    }
}
