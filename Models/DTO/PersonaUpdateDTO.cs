using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace PersonaCompleta.Models.DTO
{
    public class PersonaUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string nombre { get; set; }
        public string segundonombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string apellido { get; set; }
        public char sexo { get; set; }
        public int edad { get; set; }
        public int documento { get; set; }
        public string nacionalidad { get; set; }

        public DateTime fecha { get; set; }
    }
}
