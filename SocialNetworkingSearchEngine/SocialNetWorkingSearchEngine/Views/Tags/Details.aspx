<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Tag>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>Tag</legend>
    
        <div class="display-label">Name</div>
        <div class="display-field"><%: Model.Name %></div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Editar", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Volver a la Lista", "Index") %>
    </p>
</body>
</html>


