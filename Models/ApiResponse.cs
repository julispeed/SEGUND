using System.Net;
namespace PersonaCompleta.Models
{
    public class ApiResponse
    {
        public HttpStatusCode statuscode { get; set; }
        public bool IsSucceful { get; set; }

        public List<string> ErrorMessages { get; set; }

        public object Resultado { get; set; }

    }
}
