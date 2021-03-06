//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIDEVIC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Indicador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Indicador()
        {
            this.Archivo = new HashSet<Archivo>();
            this.Evaluacion = new HashSet<Evaluacion>();
            this.Indicador_area = new HashSet<Indicador_area>();
            this.Requerimientos = new HashSet<Requerimientos>();
        }
    
        public int id_indicador { get; set; }
        public string nombre { get; set; }
        public int id_tipo_indicador { get; set; }
        public int id_tema { get; set; }
        public string descripcion { get; set; }
        public int id_frecuencia { get; set; }
        public Nullable<System.DateTime> fecha_inicio_año { get; set; }
        public Nullable<bool> diagnostico { get; set; }
        public Nullable<bool> pmd { get; set; }
        public Nullable<int> id_subprograma { get; set; }
        public string formula { get; set; }
        public string dimension { get; set; }
        public string medio_verificacion { get; set; }
        public string meta { get; set; }
        public string r_inaceptable { get; set; }
        public string r_bajo_aceptable { get; set; }
        public string r_aceptable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Archivo> Archivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
        public virtual Frecuencia Frecuencia { get; set; }
        public virtual Subprograma Subprograma { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Indicador_area> Indicador_area { get; set; }
        public virtual Tipo_indicador Tipo_indicador { get; set; }
        public virtual Tema Tema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requerimientos> Requerimientos { get; set; }
    }
}
