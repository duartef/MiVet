using System;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using VeterinariadeBolsillo.MiVetService;

namespace VeterinariadeBolsillo
{
	public class DataManager
	{
		// specifies the database name
		string DatabaseName = "MiVet";
		SQLiteConnection conn;

		//String for Query handling
		string sqldb_query;
		//String for Message handling
		string sqldb_message;
		//Bool to check for database availability
		bool sqldb_available;

		public DataManager ()
		{
			try
			{
				sqldb_message = "";
				sqldb_available = false;
				CreateDatabase (DatabaseName);
			} catch (SQLiteException ex)
			{
				sqldb_message = ex.Message;
			}
		}

		//Creates a new database which name is given by the parameter
		public void CreateDatabase (string sqldb_name)
		{
			try
			{
				sqldb_message = "";
				//string sqldb_location = Libs.GetAppPath();
				//string sqldb_path = Path.Combine (sqldb_location, sqldb_name);

				string sqldb_location = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
                string sqldb_path = Path.Combine (sqldb_location, sqldb_name); 
				bool sqldb_exists = File.Exists (sqldb_path);

				conn = new SQLiteConnection (sqldb_path);

				if (!sqldb_exists)
				{
					//conn.CreateTable (typeof(Vendedor), CreateFlags.None);
					//conn.CreateTable (typeof(Sucursal), CreateFlags.None);
					//conn.CreateTable (typeof(VendedorSucursal), CreateFlags.None);
				} else
				{
					//sqldb = SQLiteDatabase.OpenDatabase(sqldb_path, null, DatabaseOpenFlags.OpenReadwrite);
					sqldb_message = "Database: " + sqldb_name + " opened";
				}
				sqldb_available = true;
			} catch (SQLiteException ex)
			{
				sqldb_message = ex.Message;
			}
		}

		public void CleanDatabases ()
		{
			try
			{
				//conn.DeleteAll<Vendedor> ();
				//conn.DeleteAll<Sucursal> ();
				//conn.DeleteAll<VendedorSucursal> ();
			} catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Gets the table.
		/// </summary>
		/// <returns>The table.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public TableQuery<T> GetTable<T> () where T : new()
		{
			try
			{
				return conn.Table<T> ();
			} catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Get the specified predicate.
		/// </summary>
		/// <param name="predicate">Predicate.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Get<T> (System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : new()
		{
			try
			{
				return conn.Get<T> (predicate);
			} catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Query the specified query and args.
		/// </summary>
		/// <param name="query">Query.</param>
		/// <param name="args">Arguments.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public List<T> Query<T> (string query, params object[] args) where T : new()
		{
			try
			{
				return conn.Query<T> (query, args);
			} catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Find the specified predicate.
		/// </summary>
		/// <param name="predicate">Predicate.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Find<T> (System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : new()
		{
			try
			{
				return conn.Find<T> (predicate);
			} catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Execute the specified query and args.
		/// </summary>
		/// <param name="query">Query.</param>
		/// <param name="args">Arguments.</param>
		public void Execute (string query, params object[] args)
		{
			try
			{
				//Execute("UPDATE 'tableName' SET set1=@param1, set2=@param2 WHERE set3=@param3", param1, param2, param3);
				conn.Execute (query, args);
			} catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <returns>The scalar.</returns>
		/// <param name="query">Query.</param>
		/// <param name="args">Arguments.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T ExecuteScalar<T> (string query, params object[] args) where T : new()
		{
			try
			{
				return conn.ExecuteScalar<T> (query, args);
			} catch (Exception ex)
			{
				throw ex;
			}
		}
                
		public void InsertlAnimal (Animal animal)
		{
			try
			{
                lAnimal a = lAnimalFromAnimal(animal);
				conn.Insert (a);
			} catch (Exception ex)
			{
				throw ex;
			}
		}

        private lAnimal lAnimalFromAnimal(Animal animal)
        {
            try
            {
                lAnimal a = new lAnimal();
                a.Documento = animal.Documento;
                a.Especie = animal.Especie;
                a.FechaNacimiento = animal.FechaNacimiento;
                a.Foto = animal.Foto;
                a.Id = animal.Id;
                a.IdVeterinaria = animal.IdVeterinaria;
                a.Nombre = animal.Nombre;
                a.Sexo = animal.Sexo;
                a.Raza = animal.Raza;

                return a;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertlMascota (Mascota mascota)
        {
            try
            {
                lMascota m = lMascotaFromMascota(mascota);
                conn.Insert(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private lMascota lMascotaFromMascota(Mascota mascota)
        {
            try
            {
                lMascota m = new lMascota();
                m.Id = mascota.Id;
                m.IdPersona = mascota.IdPersona;
                m.IdAnimal = mascota.IdAnimal;
                
                return m;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertlPersona (Persona persona)
        {
            try
            {
                lPersona p = lPersonaFromPersona(persona);
                conn.Insert(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private lPersona lPersonaFromPersona(Persona persona)
        {
            try
            {
                lPersona p = new lPersona();
                p.Id = persona.Id;
                p.Nombre = persona.Nombre;
                p.Telefono = persona.Telefono;
                p.Documento = persona.Documento;

                return p;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertlVeterinaria(Veterinaria veterinaria)
        {
            try
            {
                lVeterinaria v = lVeterinariaFromVeterinaria(veterinaria);
                conn.Insert(v);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private lVeterinaria lVeterinariaFromVeterinaria(Veterinaria veterinaria)
        {
            try
            {
                lVeterinaria v = new lVeterinaria();
                v.Id = veterinaria.Id;
                v.Nombre = veterinaria.Nombre;
                v.Urgencias = veterinaria.Urgencias;
                v.Ubicacion = veterinaria.Ubicacion;
                v.Latitud = veterinaria.Latitud;
                v.Longitud = veterinaria.Longitud;

                return v;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertlVisita(Visita visita)
        {
            try
            {
                lVisita x = lVisitaFromVisita(visita);
                conn.Insert(x);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private lVisita lVisitaFromVisita(Visita visita)
        {
            try
            {
                lVisita x = new lVisita();
                x.Id = visita.Id;
                x.Fecha = visita.Fecha;
                x.IdAnimal = visita.IdAnimal;
                x.IdVeterinaria = visita.IdVeterinaria;
                x.Actividad = visita.Actividad;

                return x;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Generic update for tables with PK
        /// </summary>
        /// <param name="record">Table.</param>
        public void UpdateTable (object record)
		{
			try
			{
				conn.Update (record);
			} catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Generic Delete tables
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void DeleteAllTable<T> ()
		{
			try
			{
				conn.DeleteAll<T> ();
			} catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Delete the registry specified from table
		/// </summary>
		/// <returns>The table.</returns>
		/// <param name="primaryKey">Primary key.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public int DeleteRecord<T> (object primaryKey)
		{
			try
			{
				return conn.Delete<T> (primaryKey);
			} catch (Exception ex)
			{
				throw ex;
			}
		}
	}

	[Table ("lAnimal")]
	public class lAnimal
    {
		[PrimaryKey, Column ("Id")]
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public byte[] Foto { get; set; }
        public int IdVeterinaria { get; set; }
        public string Raza { get; set; }
    }

    [Table ("lMascota")]
	public class lMascota
    {
		[PrimaryKey, Column ("Id")]
        public int Id { get; set; }
        public int IdAnimal { get; set; }
        public int IdPersona { get; set; }
    }

	[Table ("lPersona")]
	public class lPersona
    {
		[PrimaryKey, Column ("Id")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
    }

	[Table ("lVeterinaria")]
	public class lVeterinaria
    {
		[PrimaryKey, Column ("Id")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public bool Urgencias { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }

	[Table ("lVisita")]
	public class lVisita
    {
		[PrimaryKey, Column ("Id")]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdAnimal { get; set; }
        public int IdVeterinaria { get; set; }
        public string Actividad { get; set; }
    }
}