using System;
using System.Collections.Generic;
using System.Web;

namespace MiVetService
{
    [Serializable]
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
    }
}