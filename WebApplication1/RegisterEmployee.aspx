<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterEmployee.aspx.cs" Inherits="WebApplication1.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="emailUserTitle"></h2>
    <br />
    <h3>Formulario Empleados</h3>
    <asp:Label ID="lbl_nombre" runat="server" Text="Ingrese su nombre"></asp:Label>
    <asp:Label ID="lbl_error" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" placeholder="E001"></asp:TextBox>
    <asp:RequiredFieldValidator 
    ID="rfv_nombre" 
    runat="server" 
    ControlToValidate="txt_nombre" 
    InitialValue="" 
    ErrorMessage="El nombre es obligatorio" 
    ForeColor ="Red"
    ValidationGroup="employeeForm" 
        />
    <br />
    <asp:Label ID="lbl_apellido" runat="server" Text="Ingrese su apellido"></asp:Label>
    <asp:TextBox ID="txt_apellido" runat="server" CssClass="form-control" placeholder="E001"></asp:TextBox>
    <asp:Label ID="lbl_numero_celular" runat="server" Text="Ingrese su número celular"></asp:Label>
    <asp:TextBox ID="txt_numero_celular" runat="server" CssClass="form-control" placeholder="11235878454" TextMode="Number"></asp:TextBox>
    <asp:Label ID="lbl_fn" runat="server" Text="Ingrese su fecha de nacimiento"></asp:Label>
    <asp:TextBox ID="txt_fecha_nacimiento" runat="server" CssClass="form-control" placeholder="yyyy-dd-mm" TextMode="date"></asp:TextBox>
    <asp:Label ID="Label1" runat="server" Text="Ingrese su puesto"></asp:Label>
    <asp:DropDownList ID="ddl_puestos" runat="server" CssClass="form-control" ></asp:DropDownList>
    <asp:Button ID="btn_crear"
        runat="server" CssClass="btn btn-success" 
        Text="Crear nuevo empleado" OnClick="btn_crear_Click" 
        OnClientClick="return confirm('¿Está seguro de que quiere crear?');" 
        CausesValidation="true"
        ValidationGroup="employeeForm" 
        />
     <asp:Button ID="Button2"
     runat="server" CssClass="btn bs-tooltip-top" 
     Text="Editar empleado" OnClick="btn_editar_Click" 
     OnClientClick="return confirm('¿Está seguro de que quiere actualizar?');" 
     CausesValidation="true"
     ValidationGroup="employeeForm" 
     />
 
 <!-- hacer tabla puesto id,desc, traerlo con dr, mappear el dropDownList,agregar paginacion al grid, agregar modal al boton eliminar --> 

    <asp:GridView ID="grid_empleados" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="Ridge" BorderWidth="1px" CellPadding="4" CssClass="table"  GridLines="None" RowHeaderColumn="puesto" Width="301px" 
        OnRowDeleting="grid_empleados_RowDeleting" DataKeyNames="user_id" OnSelectedIndexChanged="grid_empleados_SelectedIndexChanged" OnRowDataBound="grid_empleados_RowDataBound">
        <Columns>
            <asp:CommandField ShowDeleteButton="True"/>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="user_id" HeaderText="ID" />
            <asp:BoundField DataField="name" HeaderText="Nombre" />
            <asp:BoundField DataField="last_name" HeaderText="Apellido" />
            <asp:BoundField DataField="phone_number" HeaderText="Numero Celular" />
            <asp:BoundField DataField="date_of_birth" HeaderText="Fecha nacimiento" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
            <asp:BoundField DataField="position" HeaderText="Puesto" />
            <asp:BoundField />
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="White" BorderStyle="Solid" ForeColor="#003399" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView>
    <script>
        var emailSesion = localStorage.getItem("emailClient")
        document.getElementById("emailUserTitle").innerText = "Usuario: "+" "+ emailSesion;
    </script>

</asp:Content>

