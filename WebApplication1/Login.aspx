<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Iniciar Sesión</h2>
        <div class="form-group">
            <label for="useremail">Email</label>
            <input type="email" id="useremail" name="useremail" class="form-control" placeholder="Email" required />
        </div>

        <div class="form-group">
            <label for="password">Contraseña</label>
            <input type="password" id="password" name="password" class="form-control" placeholder="Contraseña" required />
            <br />
             <button ID="btn_login">Iniciar sesión</button>
        </div>
    <script>
   
        document.getElementById("btn_login").addEventListener("click", function (event) {
            event.preventDefault();
            
            let emailInput = document.getElementById("useremail").value;
            let passwordInput = document.getElementById("password").value;

            if (emailInput === "" || passwordInput === "") return alert("Ambos campos son requeridos")
            const credentialsData = {
                client_email:emailInput,
                client_password:passwordInput
            }
            try{

            fetch("Login.aspx/HandleLogin", {
                method: "POST",
                body: JSON.stringify({ credentials: credentialsData }),
                headers: {
                    "Content-Type": "application/json;charset=utf-8"
                }})
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response}`);
                    }
                    return response.json();
                })
                .then(data => {
                    alert("Ingreso exitoso" + " " + data.d.client_email)
                    localStorage.setItem("emailClient", data.d.client_email)
                    window.location.href = "/RegisterEmployee";
                })
                .catch(error => {
                    alert("Email o Contraseña incorrecta")
                    console.log("Error:", error);
                });
            } catch (ex) {
                console.log(ex)
            }
        });
     
    </script>
</asp:Content>
