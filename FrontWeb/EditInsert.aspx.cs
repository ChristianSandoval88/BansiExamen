using APIExamen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontWeb
{
    public enum TipoAccion { insertar, actualizar }
    public partial class EditInsert : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitulo.Text = "Crear exámen";
                btnEjecutar.Text = "Guardar";
                int id = -1;
                if (string.IsNullOrWhiteSpace(Request.QueryString["tipo"]))
                {
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                if (Request.QueryString["tipo"] != "0" && Request.QueryString["tipo"] != "1")
                {
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]) && !Int32.TryParse(Request.QueryString["id"], out id))
                {
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                lblTipo.Text = Request.QueryString["tipo"] == "0" ? "Por Web Service" : "Por StoredProcedure";
                if (id >= 0)
                {
                    txtID.Enabled = false;
                    lblTitulo.Text = "Editar exámen";
                    btnEjecutar.Text = "Actualizar";
                    var registros = await ConsultarRegistrosAsync();
                    bool encontrado = false;
                    foreach (var registro in registros)
                    {
                        if (registro.IdExamen == id)
                        {
                            encontrado = true;
                            txtDescripcion.Text = registro.Descripcion;
                            txtNombre.Text = registro.Nombre;
                            txtID.Text = registro.IdExamen.ToString();
                        }
                    }
                    if (!encontrado)
                    {
                        Response.Redirect("Default.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
            }
        }

        private async Task<List<tblExamen>> ConsultarRegistrosAsync() => await ExamenDll().ConsultarExamenAsync(
            new FiltroBusqueda() { Nombre = "", Descripcion = "" });
        private ApiExamenDLL.ClsExamen ExamenDll()
        {
            var tipoAccesoDatos = lblTipo.Text.ToLower().Contains("web") ? TipoAccesoDatos.WebService : TipoAccesoDatos.StoredProcedures;
            return new ApiExamenDLL.ClsExamen(tipoAccesoDatos);
        }
        private async Task<ApiResponse> AgregarAsync(tblExamen examen) => await ExamenDll().AgregarExamenAsync(examen);
        private async Task<ApiResponse> ActualizarAsync(tblExamen examen) => await ExamenDll().ActualizarExamenAsync(examen);
        protected async void btnEjecutar_Click(object sender, EventArgs e)
        {
            var accion = string.IsNullOrWhiteSpace(Request.QueryString["id"]) ? TipoAccion.insertar:TipoAccion.actualizar;
            if (!ValidarFormulario(accion.ToString())) return;
            tblExamen examen = new tblExamen() { IdExamen = Convert.ToInt32(txtID.Text.Trim()), Nombre = txtNombre.Text.Trim(), Descripcion = txtDescripcion.Text.Trim() };
            ApiResponse registro = new ApiResponse();
            if (accion == TipoAccion.insertar)
                registro = await AgregarAsync(examen);
            else if (accion == TipoAccion.actualizar)
                registro = await ActualizarAsync(examen);
            if (!registro.Exito)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "errorInsert2", "alert('" + registro.Descripcion + "')", true);
                return;
            }

            Response.Redirect("Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private bool ValidarFormulario(string accion)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text.Trim()) || !Int32.TryParse(txtID.Text.Trim(), out int id))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Debe ingresar un número en el campo ID para " + accion + " un registro.')", true);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text.Trim()) || string.IsNullOrWhiteSpace(txtDescripcion.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error1", "alert('Debe ingresar todos los campos para " + accion + " un registro.')", true);
                return false;
            }
            return true;
        }
    }
}