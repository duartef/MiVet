using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MiVetService
{
    public class DAOBase
    {
        internal const string connStr = @"Data Source=sql5024.mywindowshosting.com;Initial Catalog=DB_9BA2A5_MiVet;User Id=DB_9BA2A5_MiVet_admin;Password=q1w2e3r4;";

        public static bool DeleteEntity(object dto)
        {
            try
            {
                string query = string.Format("DELETE FROM {0} WHERE Id = {1}", dto.GetType().Name, dto.GetType().GetProperty("Id").GetValue(dto, null));

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        int result = (int)cmd.ExecuteNonQuery();

                        if (result > 0)
                            return true;

                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool CreateEntity(object dto)
        {
            if (dto == null)
                return false;

            try
            {

                //string query = string.Format("INSERT INTO {0})", dto.GetType().Name);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;

                        con.Open();

                        var props = dto.GetType().GetProperties();
                        foreach (PropertyInfo prop in props)
                        {
                            if (prop.Name != "CantFotos")
                            {
                                SqlParameter p = cmd.CreateParameter();

                                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    p.SqlDbType = ConvertToDBType(prop.PropertyType.GetGenericArguments()[0]);
                                    if (prop.GetValue(dto, null) == null)
                                        p.SqlValue = Convert.DBNull;
                                    else
                                        p.SqlValue = prop.GetValue(dto, null);
                                }
                                else
                                {
                                    p.SqlDbType = ConvertToDBType(prop);
                                    p.SqlValue = prop.GetValue(dto, null);
                                    if (p.SqlDbType == SqlDbType.NVarChar && p.SqlValue == null)
                                    {
                                        p.SqlValue = "";
                                    }
                                }

                                p.ParameterName = "@" + prop.Name;

                                cmd.Parameters.Add(p);
                            }
                        }
                        //cmd.Parameters.A

                        CrearInsertQuery(cmd, dto.GetType().Name);


                        int result = (int)cmd.ExecuteNonQuery();

                        if (result > 0)
                            return true;

                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void CrearInsertQuery(SqlCommand Comando, string Tabla)
        {

            string sQuery = "INSERT INTO [" + Tabla + "]" + "\r\n" + "(";

            foreach (SqlParameter unParametro in Comando.Parameters)
            {
                if (unParametro.Direction == ParameterDirection.Input)
                {
                    sQuery = sQuery + unParametro.ParameterName.Substring(1) + ",";
                }
            }

            sQuery = sQuery.Remove(sQuery.Length - 1, 1);
            sQuery = sQuery + " )" + "\r\n" + "VALUES ( ";

            foreach (SqlParameter unParametro in Comando.Parameters)
            {
                if (unParametro.Direction == ParameterDirection.Input)
                {
                    sQuery = sQuery + unParametro.ParameterName + ",";
                }
            }

            sQuery = sQuery.Remove(sQuery.Length - 1, 1);
            sQuery = sQuery + " )";

            Comando.CommandType = CommandType.Text;
            Comando.CommandText = sQuery;
        }


        private static SqlDbType ConvertToDBType(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(bool))
                return SqlDbType.Bit;
            if (prop.PropertyType == typeof(string))
                return SqlDbType.NVarChar;
            if (prop.PropertyType == typeof(int))
                return SqlDbType.Int;
            if (prop.PropertyType == typeof(long))
                return SqlDbType.BigInt;
            if (prop.PropertyType == typeof(DateTime))
                return SqlDbType.DateTime;
            if (prop.PropertyType == typeof(DateTime?))
                return SqlDbType.DateTime;
            if (prop.PropertyType == typeof(TimeSpan))
                return SqlDbType.Time;
            if (prop.PropertyType == typeof(float))
                return SqlDbType.Float;
            if (prop.PropertyType == typeof(decimal))
                return SqlDbType.Decimal;
            if (prop.PropertyType == typeof(double))
                return SqlDbType.Real;
            if (prop.PropertyType == typeof(byte[]))
                return SqlDbType.VarBinary;
            if (prop.PropertyType == null)
                return SqlDbType.VarBinary;
            if (prop.PropertyType == typeof(Nullable<>))
                return SqlDbType.Int;

            throw new Exception("Err: Tipo de Valor Desconocido!");
        }

        private static SqlDbType ConvertToDBType(Type type)
        {
            if (type == typeof(bool))
                return SqlDbType.Bit;
            if (type == typeof(string))
                return SqlDbType.NVarChar;
            if (type == typeof(int))
                return SqlDbType.Int;
            if (type == typeof(long))
                return SqlDbType.BigInt;
            if (type == typeof(DateTime))
                return SqlDbType.DateTime;
            if (type == typeof(DateTime?))
                return SqlDbType.DateTime;
            if (type == typeof(TimeSpan))
                return SqlDbType.Time;
            if (type == typeof(float))
                return SqlDbType.Float;
            if (type == typeof(decimal))
                return SqlDbType.Decimal;
            if (type == typeof(double))
                return SqlDbType.Real;
            if (type == typeof(byte[]))
                return SqlDbType.VarBinary;
            if (type == null)
                return SqlDbType.VarBinary;
            if (type == typeof(Nullable<>))
                return SqlDbType.Int;

            throw new Exception("Err: Tipo de Valor Desconocido!");
        }

        public static int GetNextId(object dto)
        {
            if (dto == null)
                return 0;

            try
            {
                string query = string.Format("SELECT MAX(Id) FROM {0}", dto.GetType().Name);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        object aux = cmd.ExecuteScalar();

                        //Si el resultado es un numero, le sumo 1 y lo devuelvo.
                        //Sino, devuelvo 1.
                        int nextId;
                        if ((bool)int.TryParse(aux.ToString(), out nextId))
                            return (nextId + 1);
                        else
                            return 1;
                    }
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetDataTable(object dto, string filtro)
        {
            if (dto == null)
                return null;

            DataTable dt = new DataTable(dto.GetType().Name);

            try
            {
                //Condición boba para poder agregarle los OR
                string query = string.Format("SELECT * FROM {0} WHERE 1=1 AND ", dto.GetType().Name);

                if (filtro != string.Empty)
                {
                    var props = dto.GetType().GetProperties().Where(x => x.Name != "Id");

                    foreach (PropertyInfo prop in props)
                    {
                        object aux = prop.GetValue(dto, null);
                        if (aux != null)
                        {
                            query += string.Format("{0} LIKE '%{1}%' OR ",
                                prop.Name,
                                filtro
                                );
                        }
                    }
                }

                //Le saco el AND o el OR que quedó en el final de la query.
                if (query.Trim().EndsWith("AND") || query.Trim().EndsWith("OR"))
                {
                    query = query.Remove(query.Length - 4);
                }


                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        //Preparo el DataAdapter y DataTable para retornar los datos.
                        SqlDataAdapter da = new SqlDataAdapter();

                        da.SelectCommand = cmd;

                        da.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetDataTablePaginado(object dto, int num, string filtro)
        {
            if (dto == null)
                return null;

            DataTable dt = new DataTable(dto.GetType().Name);

            try
            {
                //Condición boba para poder agregarle los OR
                string query = string.Format("SELECT * FROM {0} WHERE Id BETWEEN {1} AND {2} AND ", dto.GetType().Name, num, num + 9);

                if (filtro != string.Empty)
                {
                    query += filtro;

                }

                //Le saco el AND o el OR que quedó en el final de la query.
                if (query.Trim().EndsWith("AND") || query.Trim().EndsWith("OR"))
                    query = query.Remove(query.Length - 4);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        //Preparo el DataAdapter y DataTable para retornar los datos.
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ExecuteQueryWrite(string query, List<SqlParameter> param)
        {
            int filas;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    foreach (SqlParameter p in param)
                        cmd.Parameters.Add(p);

                    filas = cmd.ExecuteNonQuery();
                }
            }
            return filas;
        }

        public static DataTable ExcecuteQuery(string query)
        {
            DataTable dt = new DataTable("Listado");

            if (query == string.Empty)
                return dt;
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        //Preparo el DataAdapter y DataTable para retornar los datos.
                        SqlDataAdapter da = new SqlDataAdapter();

                        da.SelectCommand = cmd;

                        da.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetDataTableWhere(object dto, string filtro)
        {
            if (dto == null)
                return null;

            DataTable dt = new DataTable(dto.GetType().Name);

            try
            {
                //Condición boba para poder agregarle los OR
                string query = string.Format("SELECT * FROM {0} WHERE 1=1 AND ", dto.GetType().Name);

                if (filtro != string.Empty)
                {
                    query += filtro;
                    //var props = dto.GetType().GetProperties().Where(x => x.Name != "Id");

                    //foreach (System.Reflection.PropertyInfo prop in props)
                    //{
                    //    object aux = prop.GetValue(dto, null);
                    //    if (aux != null)
                    //    {
                    //        query += string.Format("{0} LIKE '%{1}%' OR ",
                    //            prop.Name,
                    //            filtro
                    //            );
                    //    }
                    //}
                }

                //Le saco el AND o el OR que quedó en el final de la query.
                if (query.Trim().EndsWith("AND") || query.Trim().EndsWith("OR"))
                {
                    query = query.Remove(query.Length - 4);
                }


                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        //Preparo el DataAdapter y DataTable para retornar los datos.
                        SqlDataAdapter da = new SqlDataAdapter();

                        da.SelectCommand = cmd;

                        da.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static void PoblarObjetoDesdeDataRow(object obj, System.Data.DataRow dr)
        {
            //Obtengo el Tipo/Clase del objeto.
            int i;
            Type Objeto = obj.GetType();
            PropertyInfo[] myPropertyInfo = Objeto.GetProperties((BindingFlags.Public | BindingFlags.Instance));

            for (i = 0; i < myPropertyInfo.Length; i++)
            {
                PropertyInfo myPropInfo = ((PropertyInfo)myPropertyInfo[i]);

                //Solo voy a tener en cuenta las propiedades que no sean ReadOnly.
                //Así ignoro la 'TABLA'.
                if (myPropInfo.CanWrite && dr.Table.Columns.Contains(myPropInfo.Name))
                {
                    if (dr[myPropInfo.Name] is Single && (myPropInfo.PropertyType == typeof(decimal) || myPropInfo.PropertyType == typeof(decimal?)))
                    {
                        //Seteo el valor de la propiedad SOLO si no es nulo.
                        if (dr[myPropInfo.Name] != System.DBNull.Value)
                            myPropInfo.SetValue(obj, Convert.ToDecimal(dr[myPropInfo.Name]), null);
                    }
                    else if (dr[myPropInfo.Name] is TimeSpan && (myPropInfo.PropertyType == typeof(DateTime) || myPropInfo.PropertyType == typeof(DateTime?)))
                    {
                        if (dr[myPropInfo.Name] != System.DBNull.Value)
                            myPropInfo.SetValue(obj, (new DateTime().Add((TimeSpan)dr[myPropInfo.Name])), null);
                    }
                    else
                    {
                        //Seteo el valor de la propiedad SOLO si no es nulo.
                        if (dr[myPropInfo.Name] != System.DBNull.Value)
                            myPropInfo.SetValue(obj, dr[myPropInfo.Name], null);
                    }
                }
            }
        }

        public static bool UpdateEntity(object dto)
        {
            List<SqlParameter> lParameters = new List<SqlParameter>();
            string query = string.Empty;
            int filas;

            try
            {
                query = @"UPDATE " + dto.GetType().Name + " SET ";

                if (dto == null)
                    return false;

                string columns = string.Empty;
                string values = string.Empty;

                var props = dto.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo prop in props.Where(x => x.Name != "Id"))
                {
                    if (prop.GetValue(dto, null) != null)
                    {
                        query += prop.Name + " = @" + prop.Name.ToLowerInvariant() + ", ";
                        lParameters.Add(new SqlParameter("@" + prop.Name.ToLowerInvariant(), prop.GetValue(dto, null)));
                        //values += prop.Name + " = " + "'" + prop.GetValue(dto, null) + "'" + ", ";
                    }
                }

                if (query.EndsWith(", "))
                {
                    query = query.Remove(query.Length - 2);
                }

                PropertyInfo propId = props.Where(x => x.Name == "Id").First();
                query += " WHERE Id = @Id";
                lParameters.Add(new SqlParameter("@Id", propId.GetValue(dto, null)));

                filas = ExecuteQueryWrite(query, lParameters);

                if (filas > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}