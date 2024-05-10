using APIExamen.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI;

namespace FrontWeb
{
    public partial class _Default : Page
    {
        DataTable table = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsCallback)
            {
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Descripcion", typeof(string));
            }
        }

        protected async void btnEjecutar_Click(object sender, EventArgs e)
        {
            await ConsultarExamenAsync();
        }
        private async Task ConsultarExamenAsync()
        {
            var registros = await ConsultarRegistrosAsync();
            foreach (var registro in registros)
            {
                table.Rows.Add(registro.IdExamen, registro.Nombre, registro.Descripcion);
            }
            gvRegistros.DataSource = table;
            gvRegistros.DataBind();
        }
        private async Task<List<tblExamen>> ConsultarRegistrosAsync() => await ExamenDll().ConsultarExamenAsync(
            new FiltroBusqueda() { Nombre = txtNombre.Text.Trim(), Descripcion = txtDescripcion.Text.Trim() });

        private async Task<ApiResponse> EliminarAsync(tblExamen examen) => await ExamenDll().EliminarExamenAsync(examen);

        private ApiExamenDLL.ClsExamen ExamenDll()
        {
            var tipo = ddlTipo.SelectedIndex == 0 ? TipoAccesoDatos.WebService : TipoAccesoDatos.StoredProcedures;
            return new ApiExamenDLL.ClsExamen(tipo);
        }


        protected void btnEditRecord_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("EditInsert.aspx?id=" + index.ToString() + "&tipo=" + ddlTipo.SelectedIndex.ToString());

        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditInsert.aspx?tipo=" + ddlTipo.SelectedIndex.ToString());
        }
        protected async void btnDeleteRecord_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            tblExamen examen = new tblExamen() { IdExamen = index, Nombre = txtNombre.Text.Trim(), Descripcion = txtDescripcion.Text.Trim() };
            var registro = await EliminarAsync(examen);
            if (!registro.Exito)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "errorEliminar", "alert('" + registro.Descripcion + "')", true);
                return;
            }
            await ConsultarExamenAsync();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OkEliminar", "alert('Se eliminó el registro correctamente')", true);
            return;
        }
        protected void gvRegistros_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }
    }
}