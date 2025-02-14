<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgenciaEmpleo.ascx.cs" Inherits="WebApplication1.PopUp.AgenciaEmpleo" %>
 <div class="popup-container">
            

            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label  ID="Label1" runat="server" Text="Localidad" ></asp:Label>
                       <%-- <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList> --%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            hola
                        </p>
                    </td>
                </tr>
            </table>      <table>
                <tr>
                    <td colspan="2">
                        <p>
                            hola
                        </p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            hola
                        </p>
                    </td>
                </tr>
            </table>
            
            <%--  <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField HeaderText="Adecco" />
                    <asp:BoundField HeaderText="Randstap"/>
                    <asp:BoundField  HeaderText="Otra"/>
                </Columns>
            </asp:GridView>
                --%>
        </div>

 <style>
    .popup-container {
        background-color: white;
        border: 1px solid #ccc;
        padding: 20px;
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        z-index: 999;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    }
</style>