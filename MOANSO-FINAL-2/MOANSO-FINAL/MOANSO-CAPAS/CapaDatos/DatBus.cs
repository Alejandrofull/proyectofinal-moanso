﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Windows.Forms;

namespace CapaDatos
{
    public class DatBus
    {
        #region sigleton
        //Patron Singleton
        // Variable estática para la instancia
        private static readonly DatBus _instancia = new DatBus();
        //privado para evitar la instanciación directa
        public static DatBus Instancia
        {
            get
            {
                return DatBus._instancia;
            }
        }
        #endregion singleton

        
        #region metodos
        public List<EntBus> ListarBus()
        {
            SqlCommand cmd = null;
            List<EntBus> lista = new List<EntBus>();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar(); //singleton
                cmd = new SqlCommand("spListaBus", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EntBus bus = new EntBus();
                    bus.BusB = dr["BusB"].ToString();
                    bus.Marca = dr["Marca"].ToString();
                    bus.Modelo = dr["Modelo"].ToString();
                    bus.PisoBus = dr["PisoBus"].ToString();
                    bus.NPlaca = dr["NPlaca"].ToString();
                    bus.NChasis = dr["NChasis"].ToString();
                    bus.NMotor = dr["NMotor"].ToString();
                    bus.Capacidad = Convert.ToInt32(dr["Capacidad"]);
                    bus.TipoMotor = dr["TipoMotor"].ToString();
                    bus.Combustible = dr["Combustible"].ToString();
                    bus.FechaAdquisicion = Convert.ToDateTime(dr["FechaAdquisicion"]);
                    bus.Kilometraje = Convert.ToInt32(dr["Kilometraje"]);
                    bus.Estado = dr["Estado"].ToString();
                    lista.Add(bus);
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return lista;
        }

        public Boolean InsertarBus(EntBus bus)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertaBus", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BusB", bus.BusB);
                cmd.Parameters.AddWithValue("@Marca", bus.Marca);
                cmd.Parameters.AddWithValue("@Modelo", bus.Modelo);
                cmd.Parameters.AddWithValue("@PisoBus", bus.PisoBus);
                cmd.Parameters.AddWithValue("@NPlaca", bus.NPlaca);
                cmd.Parameters.AddWithValue("@NChasis", bus.NChasis);
                cmd.Parameters.AddWithValue("@NMotor", bus.NMotor);
                cmd.Parameters.AddWithValue("@Capacidad", bus.Capacidad);
                cmd.Parameters.AddWithValue("@TipoMotor", bus.TipoMotor);
                cmd.Parameters.AddWithValue("@Combustible", bus.Combustible);
                cmd.Parameters.AddWithValue("@FechaAdquisicion", bus.FechaAdquisicion);
                cmd.Parameters.AddWithValue("@Kilometraje", bus.Kilometraje);
                cmd.Parameters.AddWithValue("@Estado", bus.Estado);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    inserta = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return inserta;
        }

        public Boolean EditarBus(EntBus bus)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
            cmd = new SqlCommand("spModificarBus", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BusB", bus.BusB);
                cmd.Parameters.AddWithValue("@Marca", bus.Marca);
                cmd.Parameters.AddWithValue("@Modelo", bus.Modelo);
                cmd.Parameters.AddWithValue("@PisoBus", bus.PisoBus);
                cmd.Parameters.AddWithValue("@NPlaca", bus.NPlaca);
                cmd.Parameters.AddWithValue("@NChasis", bus.NChasis);
                cmd.Parameters.AddWithValue("@NMotor", bus.NMotor);
                cmd.Parameters.AddWithValue("@Capacidad", bus.Capacidad);
                cmd.Parameters.AddWithValue("@TipoMotor", bus.TipoMotor);
                cmd.Parameters.AddWithValue("@Combustible", bus.Combustible);
                cmd.Parameters.AddWithValue("@FechaAdquisicion", bus.FechaAdquisicion);
                cmd.Parameters.AddWithValue("@Kilometraje", bus.Kilometraje);
                cmd.Parameters.AddWithValue("@Estado", bus.Estado);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    edita = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return edita;
        }

        public Boolean DeshabilitarBus(EntBus bus)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitaBus", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BusB", bus.BusB);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    delete = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return delete;
        }

        public Boolean ContarRegistro(ref int totalRegistros)
        {
            SqlCommand cmd = null;
            Boolean exito = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                string consulta = "SELECT COUNT(*) FROM Bus WHERE Estado = 'Activo'";  // Consulta para contar los registros

                cmd = new SqlCommand(consulta, cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();

                totalRegistros = (int)cmd.ExecuteScalar();  // Ejecutamos la consulta y obtenemos el valor de COUNT

                // Si hay registros, asignamos true a exito
                if (totalRegistros > 0)
                {
                    exito = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return exito;
        }

        public DataTable FiltrarBus(string filtro)
        {
            DataTable dtbus = new DataTable();
            string sql = "SELECT * FROM Bus WHERE BusB LIKE @Filtro OR Marca LIKE @Filtro OR Modelo LIKE @Filtro;";

            using (SqlConnection conexion = Conexion.Instancia.Conectar())  // Uso de la instancia de Conexion
            {
                if (conexion != null)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(sql, conexion);
                        command.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        dataAdapter.Fill(dtbus);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("No se pudieron mostrar los registros, error: " + ex.Message);
                    }
                }
            }
            return dtbus;  // Retorna el DataTable con los registros filtrados
        }

        public DataTable ObtenerBusOrdenado()
        {
            DataTable dtbus = new DataTable();
            string sql = "SELECT * FROM Bus ORDER BY Marca ASC";

            using (SqlConnection conexion = Conexion.Instancia.Conectar())  // Asumiendo que la conexión está gestionada por la clase Conexion
            {
                if (conexion != null)
                {
                    try
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conexion);
                        dataAdapter.Fill(dtbus);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener los clientes ordenados: " + ex.Message);
                    }
                }
            }

            return dtbus;
        }

        #endregion metodos
    }
}