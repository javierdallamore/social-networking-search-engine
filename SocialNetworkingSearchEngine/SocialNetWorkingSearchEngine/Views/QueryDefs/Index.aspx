<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.CrudViewModel<Core.Domain.QueryDef>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Index</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#filtrar").click(function() {
                filtrar();
            });
        });

        function filtrar() {
            var like = $("#Filter").val();
            location.href = "?like=" + like;
            return;
        }
    </script>
</head>
<body>
        <p>
            Buscar: <%: Html.EditorFor(model => model.Filter,new{Id = "txtFilter"}) %> <button id="filtrar">Filtrar</button>
        </p>
    <p>
        <%: Html.ActionLink("Agregar", "Create") %>
    </p>
    <table>
        <tr>
            <th></th>
            <th>
                Query
            </th>
            <th>
                MinQueueLength
            </th>
            <th>
                DaysOldestPost
            </th>
            <th>
                Enabled
            </th>
            <th>
                SearchEnginesNames
            </th>
            <th>
                Posts
            </th>
            <th>
                SearchEnginesNamesList
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
    			<%: item.Query %>
            </td>
            <td>
    			<%: item.MinQueueLength %>
            </td>
            <td>
    			<%: item.DaysOldestPost %>
            </td>
            <td>
    			<%: item.Enabled %>
            </td>
            <td>
    			<%: item.SearchEnginesNames %>
            </td>
            <td>
    			<%: Html.DisplayTextFor(_ => item.Posts).ToString() %>
            </td>
            <td>
    			<%: (item.SearchEnginesNamesList == null ? "None" : item.SearchEnginesNamesList.Count.ToString()) %>
            </td>
        </tr>  
    <% } %>
    
    </table>
    <%: Html.Pager(Model,new PagerOptions { PageIndexParameterName = "page",FirstPageText = "primera", LastPageText = "última", PrevPageText = "anterior", NextPageText = "siguiente"})  %>
</body>
</html>


