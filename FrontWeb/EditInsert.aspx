<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditInsert.aspx.cs" Inherits="FrontWeb.EditInsert"  Async="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3><asp:Label runat="server" ID="lblTitulo"></asp:Label></h3>
    <h3><asp:Label runat="server" ID="lblTipo"></asp:Label></h3><br /><br />
    <div class="row">
        <div class="col-md-3">
            <label>ID:</label>
            <asp:TextBox onkeydown = "return (event.keyCode == 8 || event.keyCode == 9 || (event.keyCode >= 48 && event.keyCode <= 57))" ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        </div>
    <div class="row">
        <div class="col-md-3">
            <label>Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        </div>
    <div class="row">
        <div class="col-md-3">
            <label>Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        </div>
    <div class="row">
        <div class="col-md-3"></div>
        </div>
    <div class="row">
        <div class="col-md-3">
            <br />
            <asp:Button ID="btnEjecutar" runat="server" OnClick="btnEjecutar_Click" Text="Ejecutar" CssClass="btn btn-secondary" />
            &nbsp;&nbsp;
            <a href="Default.aspx">Regresar</a>
        </div>    
    </div>
</asp:Content>
