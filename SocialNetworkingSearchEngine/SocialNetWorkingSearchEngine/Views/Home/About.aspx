<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="about">
        <h2>
            Acerca de nosotros</h2>
        <p>
            Desarrollado por <a href="http://pichersandpichers.com.ar">P&P</a> y <a href="http://arsoft.com.ar/en/home.asp">ARSoft</a>.
        </p>
    </div>
</asp:Content>
