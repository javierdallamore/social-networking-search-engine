<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.QueryDef>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Create</title>
</head>
<body>
    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
        <fieldset>
            <legend>QueryDef</legend>
    
    		<%: Html.Partial("CreateOrEdit", Model) %>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>
    <% } %>
    
    <div>
        <%: Html.ActionLink("Volver a la Lista", "Index") %>
    </div>
</body>
</html>

