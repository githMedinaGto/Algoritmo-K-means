//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PerspectivaCliente.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Edad { get; set; }
        public string Genero { get; set; }
        public Nullable<int> UbicacionID { get; set; }
        public Nullable<int> PosibilidadesEconomicasID { get; set; }
        public Nullable<int> PreferenciasID { get; set; }
    
        public virtual PosibilidadesEconomicas PosibilidadesEconomicas { get; set; }
        public virtual Preferencias Preferencias { get; set; }
        public virtual Region Region { get; set; }
    }
}