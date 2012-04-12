<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Domain.Word>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Name) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Name) %>
    <%: Html.ValidationMessageFor(model => model.Name) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Sentiment) %>
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.Sentiment,new List<SelectListItem>(){new SelectListItem(){Text = "Positivo"},new SelectListItem(){Text = "Negativo"}}) %>
    <%: Html.ValidationMessageFor(model => model.Sentiment) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Weigth) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Weigth) %>
    <%: Html.ValidationMessageFor(model => model.Weigth) %>
</div>

