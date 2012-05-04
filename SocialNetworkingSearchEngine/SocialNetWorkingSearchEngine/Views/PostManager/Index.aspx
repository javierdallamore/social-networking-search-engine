<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.PostManagerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .hlt {
            background-color: yellow;
        }
        .hlt_negative {
            background-color: red;
        }
        .hlt_positive {
            background-color: greenyellow;
        }
        .result_list li {
            margin-bottom: 20px;
            margin-top: 15px;
        }
        #result_list {
            width: 1024px; 
            margin: 0 auto;
        }
        
        #result_list li select{
            padding: 0px;
            font-weight: bold;
            font-size: 0.8em;
        }
    </style>
    
    <div style="padding: 10px;background-color: #EEEEEE; margin-bottom: 15px">
            <div style="text-align: right;">
                Usuario: <%=HttpContext.Current.User.Identity.Name %>
                <%=Html.ActionLink("(X)","LogOff", "Account",null,new {Title="Cerrar sesion"}) %>
            </div>
    </div>
    <div id="container">
        <ul id="result_list" style="list-style-type: none">
            <% foreach (var post in Model.Posts)
               { %>
            <li id="post_list_item_<%=post.Id %>">
                <div class="result clearfix">
                    <%--ICONS--%>
                    <div style="float: left">
                        <img src="<% 
                        switch (post.Sentiment.ToLower()) 
                        {
                            case "positivo":%>
                                <%=Url.Content("~/Content/sentiment_positive.png")%>
                                <%break;
                            case "neutro":%>
                                <%=Url.Content("~/Content/sentiment_neutral.png")%>
                                <%break;
                            case "negativo":%>
                                <%=Url.Content("~/Content/sentiment_negative.png")%>
                            <%break;
                            }%>" id="imgIconSentiment" class="icon sentiment" />
                        <img src="<% 
                        switch (post.SocialNetworkName.ToLower()) 
                        {
                            case "facebook":%>
                                <%=Url.Content("~/Content/facebook_icon.ico")%>
                                <%break;
                            case "twitter":%>
                                <%=Url.Content("~/Content/twitter_icon.ico")%>
                                <%break;
                            case "google +":%>
                                <%=Url.Content("~/Content/Google-Plus-48.png")%>
                                <%break;
                            default:%>
                                <%=Url.Content("~/Content/search_item_icon.gif")%>
                                <%break;
                            }%>" id="img1" class="icon" />
                    </div>
                    <%--RATING--%>
                    <div style="float: right">
                        <div id="stars-wrapper_<%=post.Id%>">
                            <select name="selrate">
                                <option value="1" <% if (post.Calification == 1){%> selected="selected" <% } %>>Pobre</option>
                                <option value="2" <% if (post.Calification == 2){%> selected="selected" <% } %>>No tan
                                    pobre</option>
                                <option value="3" <% if (post.Calification == 3){%> selected="selected" <% } %>>Promedio</option>
                                <option value="4" <% if (post.Calification == 4){%> selected="selected" <% } %>>Bueno</option>
                                <option value="5" <% if (post.Calification == 5){%> selected="selected" <% } %>>Perfecto</option>
                            </select>
                        </div>
                        <br/>
                        <div id="sentiment_<%=post.Id %>">
                            <span>Sentimiento:</span>
                            <br />
                            <%=Html.DropDownList("option_sentiment_" + post.Id,new SelectList(new List<string>{"negativo","positivo","neutro"},post.Sentiment.ToLower()))%>
                        </div>
                        <br/>
                        <div id="save">
                            <input id="button_save_<%=post.Id%>" type="image" src="<%=Url.Content("~/Content/Save-icon.png") %>" style="height: 20px"/>
                        </div>
                    </div>
                    <%--POST CONTENT--%>
                    <div id="post_content<%=post.Id %>" style="width: 85%">
                        <a href="<%=post.UrlPost%>" target="_blank" title="<%=post.Content.Replace("\"","&quot;")%>">
                            <%=post.Content %></a>
                    </div>
                    <div style="width: 85%">
                        El
                        <%=post.CreatedAtShort%>
                        por
                        <img src="<%=post.ProfileImage%>" />
                        <a href="<%=post.UrlProfile%>" target="_blank">
                            <%=post.UserName%></a>
                    </div>
                    <br/>
                    <%--TAGS CONTAINER--%>
                    <div style="width: 85%">
                        <span>Tag:</span>
                        <ul id="ul_tags_<%=post.Id%>">
                        </ul>
                    </div>
                    <div id="email">
                    </div>
                </div>
                
                <%--Campos ocultos--%>
                <p id="assigned_tag_string_array_<%=post.Id%>" style="display: none">
                    <%=string.Join(",", post.PostTags.Select(x => x.Tag.Name))%>
                </p>
                <p id="query_words_<%=post.Id %>" style="display: none">
                    <%=string.Join(",", post.QueryDef.Query.Split(' ')) %>
                </p>
            </li>
            <% } %>
        </ul>
        <div style="text-align: right">
            <%:Html.ActionLink("Mas posts >>","Index") %>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function() {
            var negativeWords = [<%=string.Join(",", Model.NegativeWords.Select(x => "'" + x.Name + "'"))%>];
            var positiveWords = [<%=string.Join(",", Model.PositiveWords.Select(x => "'" + x.Name + "'"))%>];

            //Creo las estrellitas del rating
            $("div[id^='stars-wrapper']").each(function(i, e) {
                $(e).stars({
                    inputType: "select"
                });
            });

            var tags = [<%=string.Join(",",Model.TagsStringsArray) %>];

            //Creo la lista con tags
            $("ul[id^='ul_tags']").each(function(i, e) {
                var assigned_tags_id_field = "assigned_tag_string_array_" + e.id.substr(e.id.lastIndexOf('_') + 1);
                var assigned_tags_string = $("#" + assigned_tags_id_field).text().trim();
                var assigned_tags_array = (assigned_tags_string == "") ? [] : assigned_tags_string.split(",");
                $(e).tagHandler({
                    msgNoNewTag: "No tiene permisos para crear un nuevo tag",
                    msgError: "No se pudo cargar la lista de tag",
                    availableTags: tags,
                    autocomplete: true,
                    allowAdd: true,
                    assignedTags: assigned_tags_array
                });
            });

            //Atacho el save a cada item
            $("input[id^='button_save']").each(function(i, e) {
                $(e).click(function(event) {
                    var item_id = e.id.substr(e.id.lastIndexOf("_") + 1);
                    var item_rating = $("#stars-wrapper_" + item_id).data("stars").options.value;
                    var item_tags = $("#ul_tags_" + item_id).tagHandler("getTags");
                    var item_sentiment = $("#sentiment_" + item_id + " option:selected").val();

                    $.ajax({
                        url: '/PostManager/UpdatePost',
                        type: 'POST',
                        dataType: 'json',
                        traditional: 'true',
                        data: { idPost: item_id, rating: item_rating, sentiment: item_sentiment, tags: item_tags },
                        success: function (data) {
                            save_complete(data);
                        }
                    });
                });
            });

            highlight_query_words();
            
            highlight_words(negativeWords,"hlt_negative");
            highlight_words(positiveWords,"hlt_positive");
        });
        
        function save_complete(responseText, textStatus) {
                alert(responseText);
        }
            
        function highlight_query_words() {
            $("li[id^='post_list_item']").each(function(i, e) {
                var query_words = $("[id^='query_words']", e).text().trim().split(",");
                _.each(query_words,function(word) {
                    var regex = new RegExp('\\b' + word + '\\b', "gi");
                    $("[id^='post_content'] a",e).html( $("[id^='post_content'] a",e).html().replace(regex ,"<span class='hlt'>"+word+"</span>"));
                });
            });
        }
        
        function highlight_words(wordsArray, classStyle) {
            $("li[id^='post_list_item']").each(function(i, e) {
                _.each(wordsArray,function(word) {
                    var regex = new RegExp('\\b' + word + '\\b', "gi");
                    $("[id^='post_content'] a",e).html( $("[id^='post_content'] a",e).html().replace(regex ,"<span class='" + classStyle + "'>"+word+"</span>"));
                });
            });
        }
    </script>
</asp:Content>
