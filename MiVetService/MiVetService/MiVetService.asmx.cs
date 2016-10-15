using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Services;

namespace MiVetService
{
    /// <summary>
    /// Summary description for MiVetService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class MiVetService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void Test(Animal a, Mascota m, Persona p, Veterinaria v, Visita vis, Indicacion i)
        {

        }

        [WebMethod]
        public object LogIn(string usuario, string password)
        {
            if (usuario.StartsWith("vet"))
            {
                return VeterinariaDAO.Login(usuario, password);
            }
            else
            {
                return PersonaDAO.Login(usuario, password);
            }

        }

        [WebMethod]
        public Visita UpsertVisita(Visita visita)
        {
            try
            {
                if (visita.Id == 0)
                {
                    visita.Id = DAOBase.GetNextId(visita);
                    if (DAOBase.CreateEntity(visita))
                    {
                        return visita;
                    }
                    else
                    {
                        throw new Exception("No se pudo insertar la visita");
                    }
                }
                else
                {
                    if (DAOBase.UpdateEntity(visita))
                    {
                        return visita;
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar la visita");
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public Veterinaria UpsertVeterinaria(Veterinaria veterinaria)
        {
            try
            {
                if (veterinaria.Id == 0)
                {
                    veterinaria.Id = DAOBase.GetNextId(veterinaria);
                    if (DAOBase.CreateEntity(veterinaria))
                    {
                        return veterinaria;
                    }
                    else
                    {
                        throw new Exception("No se pudo insertar el veterinaria");
                    }
                }
                else
                {
                    if (DAOBase.UpdateEntity(veterinaria))
                    {
                        return veterinaria;
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el veterinaria");
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public Persona UpsertPersona(Persona persona)
        {
            try
            {
                if (persona.Id == 0)
                {
                    persona.Id = DAOBase.GetNextId(persona);
                    if (DAOBase.CreateEntity(persona))
                    {
                        return persona;
                    }
                    else
                    {
                        throw new Exception("No se pudo insertar el persona");
                    }
                }
                else
                {
                    if (DAOBase.UpdateEntity(persona))
                    {
                        return persona;
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el persona");
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public Mascota UpsertMascota(Mascota mascota)
        {
            try
            {
                if (mascota.Id == 0)
                {
                    mascota.Id = DAOBase.GetNextId(mascota);
                    if (DAOBase.CreateEntity(mascota))
                    {
                        return mascota;
                    }
                    else
                    {
                        throw new Exception("No se pudo insertar el mascota");
                    }
                }
                else
                {
                    if (DAOBase.UpdateEntity(mascota))
                    {
                        return mascota;
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el mascota");
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public Indicacion UpsertIndicacion(Indicacion indicacion)
        {
            try
            {
                if (indicacion.Id == 0)
                {
                    indicacion.Id = DAOBase.GetNextId(indicacion);
                    if (DAOBase.CreateEntity(indicacion))
                    {
                        return indicacion;
                    }
                    else
                    {
                        throw new Exception("No se pudo insertar la indicacion");
                    }
                }
                else
                {
                    if (DAOBase.UpdateEntity(indicacion))
                    {
                        return indicacion;
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar la indicacion");
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public Animal UpsertAnimal(Animal animal)
        {
            try
            {
                if (animal.Id == 0)
                {
                    animal.Id = DAOBase.GetNextId(animal);
                    if (DAOBase.CreateEntity(animal))
                    {
                        return animal;
                    }
                    else
                    {
                        throw new Exception("No se pudo insertar el animal");
                    }
                }
                else
                {
                    if (DAOBase.UpdateEntity(animal))
                    {
                        return animal;
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar el animal");
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public Visita[] GetVisitasDeLaVeterinaria(int vetId)
        {
            try
            {
                return VisitaDAO.GetVisitasDeLaVeterinaria(vetId).ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public Animal[] GetAnimalesDeLaVeterinaria(int vetId)
        {
            try
            {
                return AnimalDAO.GetAnimalesDeLaVeterinaria(vetId).ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public Visita[] GetProximasVisitasDelAnimal(int vetId, int animalId)
        {
            try
            {
                List<Visita> visitas = new List<Visita>();
                string query = string.Format("SELECT * FROM Vista WHERE IdAnimal = {0} AND IdVeterinaria = {1} AND Fecha >= {2}", vetId, animalId, DateTime.Now.ToString("yyyy/MM/dd"));
                DataTable dt = DAOBase.ExcecuteQuery(query);
                foreach (DataRow dr in dt.Rows)
                {
                    Visita aux = new Visita();
                    DAOBase.PoblarObjetoDesdeDataRow(aux, dr);

                    visitas.Add(aux);
                }

                return visitas.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public VisitAnimal[] GetVisitasDelDiaPorVeterinaria(int vetId, DateTime fecha)
        {
            try
            {
                List<VisitAnimal> visitAnimals = new List<VisitAnimal>();

                string query = "SELECT * FROM Visita JOIN Animal ON Visita.IdAnimal = Animal.Id WHERE Visita.Fecha = '" + fecha.ToString("yyyy/MM/dd") + "'";
                DataTable dt = DAOBase.ExcecuteQuery(query);

                foreach (DataRow dr in dt.Rows)
                {
                    VisitAnimal visitAnimal = new VisitAnimal();
                    Animal animal = new Animal();
                    Visita visita = new Visita();

                    animal.Id = (int)dr["IdAnimal"];
                    animal.Documento = (string)dr["Documento"];
                    animal.Especie = (string)dr["Especie"];
                    animal.FechaNacimiento = (DateTime)dr["FechaNacimiento"];
                    animal.Foto = (byte[])dr["Foto"];
                    animal.IdVeterinaria = (int)dr["IdVeterinaria"];
                    animal.Nombre = (string)dr["Nombre"];
                    animal.Raza = (string)dr["Raza"];
                    animal.Sexo = (string)dr["Sexo"];

                    visita.Actividad = (string)dr["Actividad"];
                    visita.ComentariosInternos = (string)dr["ComentariosInternos"];
                    visita.Fecha = (DateTime)dr["Fecha"];
                    visita.Id = (int)dr["Id"];
                    visita.IdAnimal = (int)dr["IdAnimal"];
                    visita.IdVeterinaria = (int)dr["IdVeterinaria"];

                    visitAnimal.Animal = animal;
                    visitAnimal.Visita = visita;
                    visitAnimals.Add(visitAnimal);
                }

                return visitAnimals.ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public void TestGetVisitasDelDia()
        {
            try
            {
                DateTime dt = new DateTime(2016, 9, 18);
                int vetId = 1;
                GetVisitasDelDiaPorVeterinaria(vetId, dt);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [WebMethod]
        public Animal[] GetAnimalesPorDueño(int vetId, string dni)
        {
            try
            {
                return AnimalDAO.GetAnimalesPorDueño(vetId, dni).ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public void TestInsertVisita()
        {
            try
            {
                Visita visita = new Visita();
                visita.Actividad = "Baño";
                visita.ComentariosInternos = "Es dificil de bañarla";
                visita.Fecha = new DateTime(2016, 9, 18);
                visita.IdAnimal = 1;
                visita.IdVeterinaria = 1;

                UpsertVisita(visita);

                visita.Fecha = visita.Fecha.AddDays(2);
                visita.Id = 0;
                UpsertVisita(visita);

                visita.Fecha = visita.Fecha.AddDays(5);
                visita.Id = 0;
                UpsertVisita(visita);

                visita.Fecha = visita.Fecha.AddDays(10);
                visita.Id = 0;
                UpsertVisita(visita);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
