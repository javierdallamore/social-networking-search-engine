<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Domain.User>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Login) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Login) %>
    <%: Html.ValidationMessageFor(model => model.Login) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Name) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Name) %>
    <%: Html.ValidationMessageFor(model => model.Name) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.IsAdmin) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.IsAdmin) %>
    <%: Html.ValidationMessageFor(model => model.IsAdmin) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.HashedPass) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.HashedPass) %>
    <%: Html.ValidationMessageFor(model => model.HashedPass) %>
</div>

