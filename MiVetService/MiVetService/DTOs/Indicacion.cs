using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiVetService
{
    [Serializable]
    public class Indicacion
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public DateTime FechaInicio { get; set; }
        public int HoraInicio { get; set; }
        public int Frecuencia { get; set; }
        public int Duracion { get; set; }
        public string Descripcion { get; set; }
    }
}