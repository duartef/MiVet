using System;
using System.Collections.Generic;
using System.Web;

namespace MiVetService
{
    [Serializable]
    public class Visita
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdAnimal { get; set; }
        public int IdVeterinaria { get; set; }
    }
}