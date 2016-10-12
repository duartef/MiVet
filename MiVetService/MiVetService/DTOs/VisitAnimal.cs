using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiVetService
{
    [Serializable]
    public class VisitAnimal
    {
        public Visita Visita { get; set; }
        public Animal Animal { get; set; }
    }
}