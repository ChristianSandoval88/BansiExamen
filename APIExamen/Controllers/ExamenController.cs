using APIExamen.Data;
using APIExamen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace APIExamen.Controllers
{
    public class ExamenController : ApiController
    {
        private BdiExamenEntities db = new BdiExamenEntities();
        
        [ResponseType(typeof(List<tblExamen>))]
        public List<tblExamen> GetRegistros([FromUri] FiltroBusqueda filtros)
        {
            var registros = db.tblExamen.ToList();
            if (!string.IsNullOrWhiteSpace(filtros.Nombre))
                registros = registros.Where(r => r.Nombre.ToLower().Contains(filtros.Nombre.ToLower())).ToList();
            if (!string.IsNullOrWhiteSpace(filtros.Descripcion))
                registros = registros.Where(r => r.Descripcion.ToLower().Contains(filtros.Descripcion.ToLower())).ToList();
            return registros;
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExamen(tblExamen examen)
        {
            if (!db.tblExamen.Any(q => q.IdExamen == examen.IdExamen))
            {
                return Ok(new ApiResponse() { Exito = false, Descripcion = "NO existe un registro con el ID:" + examen.IdExamen.ToString() });
            }
            db.Entry(examen).State = EntityState.Modified;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok(new ApiResponse() { Exito = true, Descripcion = "" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Ok(new ApiResponse() { Exito = false, Descripcion = "ERROR: " + ex.Message });
                }
            }
        }

        [ResponseType(typeof(tblExamen))]
        public async Task<IHttpActionResult> PostExamen(tblExamen examen)
        {
            if (db.tblExamen.Any(q => q.IdExamen == examen.IdExamen))
            {
                return Ok(new ApiResponse() { Exito = false, Descripcion = "Ya existe un registro con el ID:" + examen.IdExamen.ToString() });
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.tblExamen.Add(examen);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok(new ApiResponse() { Exito = true, Descripcion = "" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Ok(new ApiResponse() { Exito = false, Descripcion = "ERROR: "+ex.Message });
                }
            }
        }

        [ResponseType(typeof(tblExamen))]
        public async Task<IHttpActionResult> DeleteExamen(int id)
        {
            tblExamen examen = await db.tblExamen.FindAsync(id);
            if (examen == null)
            {
                return Ok(new ApiResponse() { Exito = false, Descripcion = $"No se encontró el ID: {id}" });
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.tblExamen.Remove(examen);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return Ok(new ApiResponse() { Exito = true, Descripcion = "" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Ok(new ApiResponse() { Exito = false, Descripcion = "ERROR: " + ex.Message });
                }
            }
        }
    }
}