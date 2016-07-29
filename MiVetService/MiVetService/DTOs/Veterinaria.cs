using System;
using System.Collections.Generic;
using System.Web;

namespace MiVetService
{
    [Serializable]
    public class Veterinaria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public bool Urgencias { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }
}