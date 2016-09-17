using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MiVetService
{
    public class VeterinariaDAO : DAOBase
    {
        internal static object Login(string usuario, string password)
        {
            try
            {
                List<Veterinaria> veterinarias = new List<Veterinaria>();
                DataTable dt = DAOBase.GetDataTableWhere(new Veterinaria(), string.Format("Nombre = '{0}' AND Password = '{1}'", usuario, password));

                if (dt.Rows.Count > 0)
                {
                    Veterinaria veterinaria = new Veterinaria();
                    PoblarObjetoDesdeDataRow(veterinaria, dt.Rows[0]);

                    return veterinaria;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}