<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.CrudViewModel<Core.Domain.User>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center">
        <p>
            Buscar:
            <%: Html.EditorFor(model => model.Filter,new{style="font-size: 0.9em" ,Id = "txtFilter"}) %>
            <button id="filtrar">
                Filtrar</button>
        </p>
        <p>
            <%: Html.ActionLink("Agregar", "Create") %>
        </p>
        </div>
        <table id="hor-minimalist-b" style="width: 600px; margin: 0 auto">
            <tr>
                <th>
                </th>
                <th>
                    Login
                </th>
                <th>
                    Nombre
                </th>
                <th>
                    Es Administrador
                </th>
                <th>
                    Posts asignados
                </th>
            </tr>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%: Html.ActionLink("Editar", "Edit", new { id=item.Id }) %>
                    |
                    <%: Html.ActionLink("Detalles", "Details", new { id=item.Id }) %>
                    |
                    <%: Html.ActionLink("Eliminar", "Delete", new { id=item.Id }) %>
                </td>
                <td>
                    <%: item.Login %>
                </td>
                <td>
                    <%: item.Name %>
                </td>
                <td>
                    <%: item.IsAdmin %>
                </td>
                <td>
                    <%: item.AssignedPosts.Count() %>
                </td>
            </tr>
            <% } %>
        </table>
        <%: Html.Pager(Model,new PagerOptions { PageIndexParameterName = "page",FirstPageText = "primera", LastPageText = "última", PrevPageText = "anterior", NextPageText = "siguiente"})  %>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#filtrar").click(function () {
                filtrar();
            });
        });

        function filtrar() {
            var like = $("#Filter").val();
            location.href = "?like=" + like;
            return;
        }
    </script>
</asp:Content>
