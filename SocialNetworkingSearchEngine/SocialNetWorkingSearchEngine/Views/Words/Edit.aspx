<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Word>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Edit</title>
</head>
<body>
    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
        <fieldset>
            <legend>Word</legend>
    
            <%: Html.HiddenFor(model => model.Id) %>
    		<%: Html.Partial("CreateOrEdit", Model) %>
            <p>
                <input type="submit" value="Grabar" />
            </p>
        </fieldset>
    <% } %>
    
    <div>
        <%: Html.ActionLink("Volver a la Lista", "Index") %>
    </div>
</body>
</html>


