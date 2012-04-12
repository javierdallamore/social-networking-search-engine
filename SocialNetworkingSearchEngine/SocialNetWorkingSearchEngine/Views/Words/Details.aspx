<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Word>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>Word</legend>
    
        <div class="display-label">Name</div>
        <div class="display-field"><%: Model.Name %></div>
    
        <div class="display-label">Sentiment</div>
        <div class="display-field"><%: Model.Sentiment %></div>
    
        <div class="display-label">Weigth</div>
        <div class="display-field"><%: Model.Weigth %></div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Editar", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Volver a la Lista", "Index") %>
    </p>
</body>
</html>


