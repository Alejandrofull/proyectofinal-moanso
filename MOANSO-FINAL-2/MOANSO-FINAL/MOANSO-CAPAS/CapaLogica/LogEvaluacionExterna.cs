using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class LogEvaluacionExterna
    {
        #region sigleton
        //Patron Singleton
        // Variable estática para la instancia
        private static readonly LogEvaluacionExterna _instancia = new LogEvaluacionExterna();
        //privado para evitar la instanciación directa
        public static LogEvaluacionExterna Instancia
        {
            get
            {
                return LogEvaluacionExterna._instancia;
            }
        }
        #endregion singleton


        public string GenerarNumeroBoleta2()
        {
            DatEvaluacionExterna ventaDAL = new DatEvaluacionExterna(); // Instanciamos la capa de datos
            int reg = ventaDAL.ObtenerNumeroBoleta2(); // Llamamos al método de la capa de datos

            string r = "";

            // Determinamos el prefijo basado en el número de registros
            if (reg >= 1 && reg < 10)
            {
                r = "000";
            }
            else if (reg >= 10 && reg < 100)
            {
                r = "00";
            }
            else if (reg >= 100 && reg < 1000)
            {
                r = "0";
            }

            // Generamos el número de boleta
            return r + reg.ToString();
        }



        public string GenerarNumeroBoleta()
        {
            DatEvaluacionExterna ventaDAL = new DatEvaluacionExterna(); // Instanciamos la capa de datos
            int reg = ventaDAL.ObtenerNumeroBoleta(); // Llamamos al método de la capa de datos

            string r = "";

            // Determinamos el prefijo basado en el número de registros
            if (reg >= 1 && reg < 10)
            {
                r = "000";
            }
            else if (reg >= 10 && reg < 100)
            {
                r = "00";
            }
            else if (reg >= 100 && reg < 1000)
            {
                r = "0";
            }

            // Generamos el número de boleta
            return r + reg.ToString();
        }

        public void InsertaEvaluacionExterna(EntEvaluacionExterna bus)
        {
            DatEvaluacionExterna.Instancia.InsertarEvaluacionExterna(bus);
        }

        public void DeshabilitarEvaluacionExterna2(EntEvaluacionExterna bus)
        {
            DatEvaluacionExterna.Instancia.DeshabilitarEvaluacionExterna(bus);
        }

        public Boolean ContarTotalEvaluacionExterna(EntEvaluacionExterna bus, ref int totalRegistros)
        {
            Boolean exito = false;

            // Llamamos al método de la capa de acceso a datos para contar los registros
            exito = DatEvaluacionExterna.Instancia.ContarRegistro(ref totalRegistros);

            return exito;
        }

        public List<string> ObtenerCodigosBuses(string codigoTE)
        {
            try
            {
                return DatEvaluacionExterna.Instancia.ObtenerCodigosBuses(codigoTE);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los códigos de buses en la capa lógica: " + ex.Message);
            }
        }

        // Método para obtener los códigos de proveedores
        public List<string> ObtenerCodigosProveedores(string codigoTE)
        {
            try
            {
                return DatEvaluacionExterna.Instancia.ObtenerCodigosProveedores(codigoTE);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los códigos de proveedores en la capa lógica: " + ex.Message);
            }
        }

        public EntProveedor ObtenerDatosProve(string codigoBus)
        {
            try
            {
                return DatEvaluacionExterna.Instancia.ObtenerDatosProve(codigoBus);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos del bus en la capa lógica: " + ex.Message, ex);
            }
        }

        public EntBus ObtenerDatosBus(string codigoBus)
        {
            try
            {
                return DatEvaluacionExterna.Instancia.ObtenerDatosBus(codigoBus);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos del bus en la capa lógica: " + ex.Message, ex);
            }
        }
    }
}
