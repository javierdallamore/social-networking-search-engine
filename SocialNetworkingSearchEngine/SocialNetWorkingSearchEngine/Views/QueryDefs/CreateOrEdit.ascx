<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Domain.QueryDef>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Query) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Query) %>
    <%: Html.ValidationMessageFor(model => model.Query) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.MinQueueLength) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.MinQueueLength) %>
    <%: Html.ValidationMessageFor(model => model.MinQueueLength) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.DaysOldestPost) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.DaysOldestPost) %>
    <%: Html.ValidationMessageFor(model => model.DaysOldestPost) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Enabled) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Enabled) %>
    <%: Html.ValidationMessageFor(model => model.Enabled) %>
</div>

    <%: Html.HiddenFor(model => model.SearchEnginesNames) %>

<%--<div class="editor-label">
    <%: Html.LabelFor(model => model.SearchEnginesNames) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.SearchEnginesNames) %>
    <%: Html.ValidationMessageFor(model => model.SearchEnginesNames) %>
</div>
--%>
