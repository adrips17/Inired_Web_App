//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplicationInired.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comentarios
    {
        public string comentario { get; set; }
        public string id_user { get; set; }
        public int id_marker { get; set; }
    
        public virtual Marker Marker { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
