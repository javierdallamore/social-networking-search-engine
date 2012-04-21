<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Core.Domain.Word>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#filtrar").click(function() {
                filtrar();
            });
        });

        function filtrar() {
            var like = $("#txtFilter").val();
            location.href = "?like=" + like;
            return;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Buscar: <input id="txtFilter" type="text" value=""/> <button id="filtrar">Filtrar</button>
    </p>
    <p>
        <%: Html.ActionLink("Agregar", "Create") %>
    </p>
    <table>
        <tr>
            <th></th>
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
    
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%: Html.ActionLink("Editar", "Edit", new { id=item.Id }) %> |
                <%: Html.ActionLink("Detalles", "Details", new { id=item.Id }) %> |
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
</asp:Content>

