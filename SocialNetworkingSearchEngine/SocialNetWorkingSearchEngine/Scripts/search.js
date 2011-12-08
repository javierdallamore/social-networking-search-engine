$(document).ready(function () {
    $("#btnSearch").click(onClick);
    // Ejemplo, modificar lo que veas necesario
    function onClick(e) {
        var values = $(":checked").map(function (value, index) { return index.value; });
        var valuesAsString = _.reduce(values, function (memo, currentItem) { return memo + ',' + currentItem });
        $("#result").html("");
        $.getJSON("Home/SearchResults", { parameters: $("#txtSearchPattern").val(), searchEngines: valuesAsString }, function (json) {
            var resultHeaderTag = $("#search_result_header");
            var result_listTag = $("#search_result_list");
            result_listTag.html("");

            _.each(json, function (socialNetworkingSearchResult) {
                var result = "<h3>" + socialNetworkingSearchResult.SocialNetworkingName + " (results: " + socialNetworkingSearchResult.SocialNetworkingItems.length + ")</h3>";
                resultHeaderTag.html("");
                resultHeaderTag.append(result);

                _.each(socialNetworkingSearchResult.SocialNetworkingItems, function (socialNetworkingItems) {
                    var item_result = "<div class=\"result clearfix\">";	                                                     //result item
                    item_result += "<div class=\"icon\">";
                    item_result += "<img src=\"" + socialNetworkingItems.SentimentIconPath + "\"" + " class=\"icon sentiment\"\">"; //sentiment icon							                                  //icon
                    item_result += "<img src=\"" + socialNetworkingItems.SocialNetworkIconPath + "\"" + " class=\"icon\"\">";           //social media icon               
                    item_result += "</div>"; 						                                                            //icon
                    item_result += "<div>"; 							                                                        //body
                    item_result += "<h3>"; 							                                                            //result title
                    item_result += "<a href=\"\" target=\"_blank\">" + socialNetworkingItems.Content + "<\a>"; 		            //link
                    item_result += "</h3>"; 							                                                        //result title
                    item_result += "<div>"; 							                                                        //result description
                    item_result += "</div>";                                                                                    //result description
                    item_result += "<div class=\"info\"> <p>";                                                                  //Info
                    item_result += "El " + socialNetworkingItems.CreatedAtShort + " por ";
                    item_result += "<img src=\"" + socialNetworkingItems.ProfileImage + "\" class=\"user_image\"\>";
                    item_result += " " + socialNetworkingItems.UserName;
                    item_result += "</p></div>";                                                                                //Info
                    item_result += "<div><p>Tag it:</p>"; 							                                            //result tag seccion
                    item_result += "<ul></ul>";
                    item_result += "</div>"; 						                                                            //result tag seccion
                    item_result += "</div>"; 						                                                            //result item

                    result_listTag.append(item_result);
                });
            });
            var itemTagContainers = $('#search_result_list ul');
            itemTagContainers.each(function (i, e) {
                $(e).tagHandler({
                    autocomplete: true
                });
            });
        });
    };
});