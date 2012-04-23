<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.User>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Delete</title>
</head>
<body>
    <h3>¿Esta seguro que quiere Eliminar?</h3>
    <fieldset>
        <legend>User</legend>
    
        <div class="display-label">Login</div>
        <div class="display-field"><%: Model.Login %></div>
    
        <div class="display-label">Name</div>
        <div class="display-field"><%: Model.Name %></div>
    
        <div class="display-label">IsAdmin</div>
        <div class="display-field"><%: Model.IsAdmin %></div>
    
        <div class="display-label">HashedPass</div>
        <div class="display-field"><%: Model.HashedPass %></div>
    
        <div class="display-label">AssignedPosts</div>
        <div class="display-field"><%: Html.DisplayTextFor(_ => Model.AssignedPosts).ToString() %></div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="Eliminar" /> |
            <%: Html.ActionLink("Volver a la Lista", "Index") %>
        </p>
    <% } %>
    
</body>
</html>


