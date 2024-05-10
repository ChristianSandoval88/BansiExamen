using APIExamen.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiExamenDLL
{
    public class StoredProceduresMethods
    {
        private static List<string> connectionStrings = new List<string>()
        {
            "data source=ChrisSandoval\\SQLEXPRESS;initial catalog=BdiExamen;integrated security=True;MultipleActiveResultSets=True;"
        };
        public static List<tblExamen> SPConsultar(FiltroBusqueda filtros)
        {
            List<tblExamen> registros = new List<tblExamen>();
            using (SqlConnection connection = new SqlConnection(connectionStrings.First()))
            {
                SqlCommand command = new SqlCommand("spConsultar", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", filtros.Nombre);
                command.Parameters.AddWithValue("@Descripcion", filtros.Descripcion);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                        registros.Add(new tblExamen() { IdExamen = Convert.ToInt32(reader[0]),Nombre = reader[1].ToString(), Descripcion = reader[2].ToString() });
                        
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
            return registros;
        }
        public static ApiResponse SPAgregar(tblExamen examen)
        {
            return StoredProcedureResponse("spAgregar",examen);
        }
        public static ApiResponse SPActualizar(tblExamen examen)
        {
            return StoredProcedureResponse("spActualizar", examen);
        }
        public static ApiResponse SPEliminar(tblExamen examen)
        {
            return StoredProcedureResponse("spEliminar", examen);
        }
        private static ApiResponse StoredProcedureResponse(string spName, tblExamen examen)
        {
            ApiResponse response = new ApiResponse();
            using (SqlConnection connection = new SqlConnection(connectionStrings.First()))
            {
                SqlCommand command = new SqlCommand(spName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", examen.IdExamen);
                if (!string.IsNullOrWhiteSpace(examen.Nombre))
                {
                    command.Parameters.AddWithValue("@Nombre", examen.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", examen.Descripcion);
                }
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        response.Exito = Convert.ToInt32(reader[0]) == 0 ? true : false;
                        response.Descripcion = reader[1].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    response.Exito = false;
                    response.Descripcion = ex.Message;
                }
            };
            return response;
        }
    }
}
