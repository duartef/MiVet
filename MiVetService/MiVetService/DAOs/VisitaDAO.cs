using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MiVetService
{
    public class VisitaDAO : DAOBase
    {
        internal static List<Visita> GetVisitasDeLaVeterinaria(int vetId)
        {
            try
            {
                List<Visita> visitas = new List<Visita>();
                DataTable dt = DAOBase.GetDataTable(new Visita(), string.Empty);
                if (dt.Rows.Count > 0)
                {
                    visitas = LlenarVisitas(new Visita(), dt);
                }

                return visitas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<Visita> LlenarVisitas(Visita vendedor, DataTable dt)
        {
            try
            {
                List<Visita> dtos = new List<Visita>();

                foreach (DataRow dr in dt.Rows)
                {
                    Visita aux = new Visita();
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