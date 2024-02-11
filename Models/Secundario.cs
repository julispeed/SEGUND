using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersonaCompleta.Models
{
    public class Secundario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idCantdoc { get; set; }

        public int PersonaId { get; set; }

        [ForeignKey("PersonaID")]
        public Persona documento { get; set; }

        public string religion { get; set; }

        public string barrio {get; set;}

        public string region { get; set; }



    }
}
