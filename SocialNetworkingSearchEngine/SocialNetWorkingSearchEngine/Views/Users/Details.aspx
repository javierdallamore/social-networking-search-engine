<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.User>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Details</title>
</head>
<body>
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
    <p>
    
        <%: Html.ActionLink("Editar", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Volver a la Lista", "Index") %>
    </p>
</body>
</html>


