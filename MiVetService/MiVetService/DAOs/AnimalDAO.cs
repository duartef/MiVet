using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MiVetService
{
    public class AnimalDAO : DAOBase
    {
        internal static List<Animal> GetAnimalesDeLaVeterinaria(int vetId)
        {
            try
            {
                List<Animal> animales = new List<Animal>();
                DataTable dt = DAOBase.GetDataTableWhere(new Animal(), "IdVeterinaria = " + vetId);
                if (dt.Rows.Count > 0)
                {
                    animales = LlenarAnimales(new Animal(), dt);
                }

                return animales;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<Animal> LlenarAnimales(Animal vendedor, DataTable dt)
        {
            try
            {
                List<Animal> dtos = new List<Animal>();

                foreach (DataRow dr in dt.Rows)
                {
                    Animal aux = new Animal();
                    PoblarObjetoDesdeDataRow(aux, dr);

                    dtos.Add(aux);
                }

                return dtos;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}