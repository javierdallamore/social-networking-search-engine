<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Core.Domain.Tag>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center">
        <p>
            <%: Html.ActionLink("Agregar", "Create") %>
        </p>
    </div>
    <table id="hor-minimalist-b" style="width: 350px; margin: 0 auto">
        <tr>
            <th>
            </th>
            <th>
                Tag
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
        </tr>
        <% } %>
    </table>
</asp:Content>
