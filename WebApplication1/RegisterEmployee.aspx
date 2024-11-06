<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterEmployee.aspx.cs" Inherits="WebApplication1.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Formulario Empleados</h3>
    <asp:Label ID="lbl_nombre" runat="server" Text="Ingrese su nombre"></asp:Label>
    <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" placeholder="E001"></asp:TextBox>
    <asp:Label ID="lbl_apellido" runat="server" Text="Ingrese su apellido"></asp:Label>
    <asp:TextBox ID="txt_apellido" runat="server" CssClass="form-control" placeholder="E001"></asp:TextBox>
    <asp:Label ID="lbl_numero_celular" runat="server" Text="Ingrese su número celular"></asp:Label>
    <asp:TextBox ID="txt_numero_celular" runat="server" CssClass="form-control" placeholder="11235878454" TextMode="Number"></asp:TextBox>
    <asp:Label ID="lbl_fn" runat="server" Text="Ingrese su fecha de nacimiento"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="yyyy-dd-mm" TextMode="date"></asp:TextBox>
    <asp:Label ID="Label1" runat="server" Text="Ingrese su puesto"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
    <asp:Button ID="btn_crear" runat="server" CssClass="btn btn-success" Text="Crear con js" />
    <asp:Button ID="btn_borrar" runat="server" CssClass="btn btn-danger" Text="Borrar" />
    <asp:Button ID="btn_editar" runat="server" CssClass="btn btn-info" Text="crear con c#" OnClick="CreateEmployee" />

    <asp:GridView ID="grid_empleados" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="Ridge" BorderWidth="1px" CellPadding="4" CssClass="table" DataKeyNames="id,id_puesto" GridLines="None" RowHeaderColumn="puesto" Width="301px">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="numero_celular" HeaderText="Numero Celular" />
            <asp:BoundField DataField="fecha_nacimiento" HeaderText="Fecha nacimiento" />
            <asp:BoundField DataField="puesto" HeaderText="Puesto" />
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

    <script type="text/javascript">
        function RegisterEmployee() {
            let name2 = document.getElementById("<%= txt_nombre.ClientID %>").value;
            var name = document.getElementById("txt_nombre").value;
            console.log("testeando el dom");
            alert("nombre: " + name2);
        }
    </script>
</asp:Content>

