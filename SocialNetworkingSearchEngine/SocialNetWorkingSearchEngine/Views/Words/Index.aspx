<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.CrudViewModel<Core.Domain.Word>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center">
        <p>
            Buscar:
            <%: Html.EditorFor(model => model.Filter,new{Id = "txtFilter"}) %>
            <button id="filtrar">
                Filtrar</button>
        </p>
        <p>
            <%: Html.ActionLink("Agregar", "Create") %>
        </p>
    </div>
    <table id="hor-minimalist-b" style="width: 450px; margin: 0 auto">
        <tr>
            <th>
            </th>
            <th>
                Palabra
            </th>
            <th>
                Sentimiento
            </th>
            <th>
                Peso
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
                <%: item.Name %>
            </td>
            <td>
                <%: item.Sentiment %>
            </td>
            <td>
                <%: item.Weigth %>
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
