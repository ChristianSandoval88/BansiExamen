<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FrontWeb._Default" Async="true" enableEventValidation="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Examen - Christian Sandoval</h3>
    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                <asp:ListItem Selected="True" Value="0">Web Service</asp:ListItem>
                <asp:ListItem Value="1">Stored Procedure</asp:ListItem>
            </asp:DropDownList>
    <br><br>
    <div class="row">
        <div class="col-md-2">
            <label>Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <label>Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <label></label>
            <asp:Button ID="btnEjecutar" runat="server" OnClick="btnEjecutar_Click" Text="Buscar" CssClass="btn btn-secondary" />
        </div>    
        <div class="col-md-2">
            <br />
            <asp:LinkButton runat="server" ID="btnNuevo" OnClick="btnNuevo_Click" CssClass="btn btn-primary">Nuevo Registro</asp:LinkButton>
        </div>  
    </div>
    <div class="row">
        <div class="col-md-12">
            <br />
            <br />
            <asp:GridView ID="gvRegistros" runat="server" Width="100%" CssClass="table table-stripe"
                DataKeyNames="ID" OnRowDeleting="gvRegistros_RowDeleting">
                <AlternatingRowStyle BackColor="#DBDBDB" ForeColor="Black" />
                <Columns>
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:Button ID="btnEditRecord"
                                OnCommand="btnEditRecord_Command"
                                CommandArgument='<%# Eval("ID").ToString() %>'
                                CommandName="Edit" runat="server" Text="Editar" CssClass="btn btn-secondary" />
                            <asp:Button ID="btnDeleteRecord"
                                OnCommand="btnDeleteRecord_Command"
                                CommandArgument='<%# Eval("ID").ToString() %>'
                                CommandName="Delete" runat="server" Text="Delete"  CssClass="btn btn-secondary" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            </asp:GridView>

        </div>
    </div>

</asp:Content>
