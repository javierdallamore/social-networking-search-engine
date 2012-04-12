<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Tag>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Delete</title>
</head>
<body>
    <h3>¿Esta seguro que quiere Eliminar?</h3>
    <fieldset>
        <legend>Tag</legend>
    
        <div class="display-label">Name</div>
        <div class="display-field"><%: Model.Name %></div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="Eliminar" /> |
            <%: Html.ActionLink("Volver a la Lista", "Index") %>
        </p>
    <% } %>
    
</body>
</html>


