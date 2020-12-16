using System.ComponentModel.DataAnnotations;

namespace IVSoftware.Web.Models
{
    public class TipoContrato
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}