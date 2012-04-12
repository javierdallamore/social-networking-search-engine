<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Core.Domain.Word>>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Index</title>
</head>
<body>
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
</body>
</html>


