<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.CrudViewModel<Core.Domain.QueryDef>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center">
        <p>
            Buscar: <%: Html.EditorFor(model => model.Filter,new{Id = "txtFilter"}) %> <button id="filtrar">Filtrar</button>
        </p>
    <p>
        <%: Html.ActionLink("Agregar", "Create") %>
    </p>
    </div>
    <table id="hor-minimalist-b" style="width: 1024px; margin: 0 auto">
        <tr>
            <th></th>
            <th>
                Texto de busqueda
            </th>
            <th>
                MinQueueLength
            </th>
            <th>
                DaysOldestPost
            </th>
            <th>
                Habilitada
            </th>
            <th>
                Redes sociales a buscar
            </th>
            <th>
                Posts
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
                <% foreach (var searchEngineName in item.SearchEnginesNamesList)
                   {%>
                       <%:searchEngineName%></br>
                   <%}%>
            </td>
            <td>
    			<%: item.Posts.Count() %>
            </td>
        </tr>  
    <% } %>
    
    </table>
    <%: Html.Pager(Model,new PagerOptions { PageIndexParameterName = "page",FirstPageText = "primera", LastPageText = "última", PrevPageText = "anterior", NextPageText = "siguiente"})  %>
    </table>
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


