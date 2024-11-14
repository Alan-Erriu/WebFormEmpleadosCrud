<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Registrar nuevos Empleados</h1>
            <p class="lead">Bienvenido</p>
        </section>

        <div class="row">
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Inicie sesión para continuar </h2>              
                <p>
                    <a class="btn btn-default" href="/Login">Iniciar sesión &raquo;</a>
                </p>
            </section>
        </div>
    </main>

</asp:Content>
