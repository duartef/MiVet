using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MiVetService
{
    public class PersonaDAO : DAOBase
    {
        internal static object Login(string usuario, string password)
        {
            try
            {
                List<Persona> personas = new List<Persona>();
                DataTable dt = DAOBase.GetDataTableWhere(new Persona(), string.Format("Nombre = '{0}' AND Password = '{1}'", usuario, password));

                if (dt.Rows.Count > 0)
                {
                    Persona persona = new Persona();
                    PoblarObjetoDesdeDataRow(persona, dt.Rows[0]);

                    return persona;
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