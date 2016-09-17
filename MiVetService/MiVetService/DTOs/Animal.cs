using System;
using System.Collections.Generic;
using System.Web;

namespace MiVetService
{
    [Serializable]
    public class Animal
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Sexo{ get; set; }
        public DateTime FechaNacimiento { get; set; }
        public byte[] Foto { get; set; }
        public int IdVeterinaria { get; set; }
        public string Raza { get; set; }
    }
}