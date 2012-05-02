<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.QueryDef>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>QueryDef</legend>
    
        <div class="display-label">Query</div>
        <div class="display-field"><%: Model.Query %></div>
    
        <div class="display-label">MinQueueLength</div>
        <div class="display-field"><%: Model.MinQueueLength %></div>
    
        <div class="display-label">DaysOldestPost</div>
        <div class="display-field"><%: Model.DaysOldestPost %></div>
    
        <div class="display-label">Enabled</div>
        <div class="display-field"><%: Model.Enabled %></div>
    
        <div class="display-label">SearchEnginesNames</div>
        <div class="display-field"><%: Model.SearchEnginesNames %></div>
    
        <div class="display-label">Posts</div>
        <div class="display-field"><%: Html.DisplayTextFor(_ => Model.Posts).ToString() %></div>
    
        <div class="display-label">SearchEnginesNamesList</div>
        <div class="display-field"><%: (Model.SearchEnginesNamesList == null ? "None" : Model.SearchEnginesNamesList.Count.ToString()) %></div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Editar", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Volver a la Lista", "Index") %>
    </p>
</body>
</html>


