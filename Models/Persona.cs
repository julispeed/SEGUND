using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonaCompleta.Models
{
    public class Persona
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string nombre { get; set; }
        public string segundonombre { get; set; }
        public string apellido { get; set; }
        public char sexo { get; set; }
        public int edad { get; set; }
        public int documento { get; set; }
        public string nacionalidad { get; set; }

        public string imagen { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime fecha { get; set;}
        //Constructor
    }
}
