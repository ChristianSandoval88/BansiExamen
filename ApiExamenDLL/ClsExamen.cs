using APIExamen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiExamenDLL
{
    public class ClsExamen
    {
        private readonly TipoAccesoDatos tipoAccesoDatos;

        public ClsExamen(TipoAccesoDatos tipoAccesoDatos)
        {
            this.tipoAccesoDatos = tipoAccesoDatos;
        }
        public async Task<List<tblExamen>> ConsultarExamenAsync(FiltroBusqueda filtros)
        {
            if (tipoAccesoDatos == TipoAccesoDatos.WebService)
                return await HttpMethods.WSConsultarAsync(filtros);
            else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
                return StoredProceduresMethods.SPConsultar(filtros);
            return new List<tblExamen>();
        }
        public async Task<ApiResponse> AgregarExamenAsync(tblExamen examen)
        {
            if (tipoAccesoDatos == TipoAccesoDatos.WebService)
                return await HttpMethods.WSAgregarAsync(examen);
            else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
                return StoredProceduresMethods.SPAgregar(examen);
            return new ApiResponse();
        }
        public async Task<ApiResponse> ActualizarExamenAsync(tblExamen examen)
        {
            if (tipoAccesoDatos == TipoAccesoDatos.WebService)
                return await HttpMethods.WSActualizarAsync(examen);
            else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
                return StoredProceduresMethods.SPActualizar(examen);
            return new ApiResponse();
        }
        public async Task<ApiResponse> EliminarExamenAsync(tblExamen examen)
        {
            if (tipoAccesoDatos == TipoAccesoDatos.WebService)
                return await HttpMethods.WSEliminarAsync(examen);
            else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
                return StoredProceduresMethods.SPEliminar(new tblExamen() { IdExamen = examen.IdExamen, Nombre="", Descripcion=""});
            return new ApiResponse();
        }
    }
}
