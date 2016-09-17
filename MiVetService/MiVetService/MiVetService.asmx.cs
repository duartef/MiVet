using System;
using System.Collections.Generic;
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
        public void Test(Animal a, Mascota m, Persona p, Veterinaria v, Visita vis)
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
