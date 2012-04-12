<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Word>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Delete</title>
</head>
<body>
    <h3>¿Esta seguro que quiere Eliminar?</h3>
    <fieldset>
        <legend>Word</legend>
    
        <div class="display-label">Name</div>
        <div class="display-field"><%: Model.Name %></div>
    
        <div class="display-label">Sentiment</div>
        <div class="display-field"><%: Model.Sentiment %></div>
    
        <div class="display-label">Weigth</div>
        <div class="display-field"><%: Model.Weigth %></div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="Eliminar" /> |
            <%: Html.ActionLink("Volver a la Lista", "Index") %>
        </p>
    <% } %>
    
</body>
</html>


