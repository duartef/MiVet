using System;
using System.Collections.Generic;
using System.Web;

namespace MiVetService
{
    [Serializable]
    public class Mascota
    {
        public int Id { get; set; }
        public int IdAnimal { get; set; }
        public int IdPersona { get; set; }
    }
}