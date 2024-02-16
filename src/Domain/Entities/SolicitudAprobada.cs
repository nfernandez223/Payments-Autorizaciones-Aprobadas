using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SolicitudesAprobadas")]
    public class SolicitudAprobada
    {
        [Key]
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public string Cliente { get; set; }
        public double Monto { get; set; }
        public DateTime? Fecha { get; set; }
    }
}