<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.UserHomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 900px;">
        <ul style="list-style-type: none">
            <% foreach (var post in Model.Posts)
               { %>
            <li style="border-bottom: 1px solid darkgray">
                <p id="assigned_tag_string<%=post.Id%>" style="display: none">
                    [<%=string.Join(",", post.Tags.Select(x => x.Name))%>]
                </p>
                <div class="result clearfix">
                    <%--ICONS--%>
                    <div style="float: left">
                        <img src="<% 
                        switch (post.Sentiment.ToLower()) 
                        {
                            case "positivo":%>
                                <%=Url.Content("../../Content/sentiment_positive.png")%>
                                <%break;
                            case "neutro":%>
                                <%=Url.Content("../../Content/sentiment_neutral.png")%>
                                <%break;
                            case "negativo":%>
                                <%=Url.Content("../../Content/sentiment_negative.png")%>
                            <%break;
                            }%>" id="imgIconSentiment" class="icon sentiment" />
                        <img src="<% 
                        switch (post.SocialNetworkName.ToLower()) 
                        {
                            case "facebook":%>
                                <%=Url.Content("../../Content/facebook_icon.ico")%>
                                <%break;
                            case "twitter":%>
                                <%=Url.Content("../../Content/twitter_icon.ico")%>
                                <%break;
                            case "googleplus":%>
                                <%=Url.Content("../../Content/googlePlus.ico")%>
                                <%break;
                            default:%>
                                <%=Url.Content("../../Content/search_item_icon.gif")%>
                                <%break;
                            }%>" id="img1" class="icon"/>
                    </div>
                    <%--RATING--%>
                    <div style="float: right">
                        <div id="stars-wrapper<%=post.Id%>">
                            <select name="selrate">
                                <option value="1" <% if (post.Calification == 1){%> selected="selected" <% } %>>Pobre</option>
                                <option value="2" <% if (post.Calification == 2){%> selected="selected" <% } %>>No tan
                                    pobre</option>
                                <option value="3" <% if (post.Calification == 3){%> selected="selected" <% } %>>Promedio</option>
                                <option value="4" <% if (post.Calification == 4){%> selected="selected" <% } %>>Bueno</option>
                                <option value="5" <% if (post.Calification == 5){%> selected="selected" <% } %>>Perfecto</option>
                            </select>
                        </div>
                        <div id="save">
                        </div>
                    </div>

                    <%--POST CONTENT--%>
                    <div style="width: 90%">
                        <a href="<%=post.UrlPost%>" target="_blank" title="<%=post.Content%>">
                            <%=post.Content %></a>
                    </div>
                    
                    <div>
                        El
                        <%=post.CreatedAtShort%>
                        por
                        <img src="<%=post.ProfileImage%>" />
                        <a href="<%=post.UrlProfile%>" target="_blank">
                            <%=post.UserName%></a>
                    </div>
                    <div>
                        <p>
                            Tag:</p>
                        <ul id="ul_tags<%=post.Id%>">
                        </ul>
                    </div>
                    <div id="email">
                    </div>
                </div>
            </li>
            <% } %>
        </ul>
    </div>
    <script type="text/javascript">
        //Creo las estrellitas del rating
        $("div[id^='stars-wrapper']").each(function (i, e) {
            $(e).stars({
                inputType: "select"
            });
        });

        var tags = [<%=string.Join(",",Model.TagsStringsArray) %>];
        //Creo la lista con tags
        $("ul[id^='ul_tags']").each(function (i, e) {
            $(e).tagHandler({
                msgNoNewTag: "No tiene permisos para crear un nuevo tag",
                msgError: "No se pudo cargar la lista de tag",
                availableTags: tags,
                autocomplete: true,
                allowAdd: true
                //assignedTags: _.map(socialNetworkingItems.Tags, function (tag) {
                //return tag.Name;
            });
        });
    </script>
</asp:Content>
