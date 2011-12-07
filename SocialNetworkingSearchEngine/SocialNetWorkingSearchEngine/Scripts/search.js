$(document).ready(function () {
    $("#btnSearch").click(onClick);
    // Ejemplo, modificar lo que veas necesario
    function onClick(e) {
        var values = $(":checked").map(function (index, item) { return item.value; });
        var valuesAsString = _.reduce(values, function (all, item) { return all + ',' + item });
        $("#result").html("");
        $.getJSON("Home/SearchResults", { parameters: $("#txtParameters").val(), searchEngines: valuesAsString }, function (json) {
            var resultTag = $("#result");

            _.each(json, function (socialNetworkingSearchResult) {
                var result = "<li>";
                result += "<div><h3>" + socialNetworkingSearchResult.SocialNetworkingName + " (results: " + socialNetworkingSearchResult.SocialNetworkingItems.length + ")</h3></div>";
                result += "</li>";
                resultTag.append(result);

                _.each(socialNetworkingSearchResult.SocialNetworkingItems, function (socialNetworkingItems) {
                    var resultItem = "<li>";
                    resultItem += "<div>" + socialNetworkingItems.StatusDate + "</div>";
                    resultItem += "<div><b>" + socialNetworkingItems.UserName + "</b></div>";
                    resultItem += "<div><img src='" + socialNetworkingItems.ProfileImage + "' alt='Profile img' /></div>";
                    resultItem += "<div>" + socialNetworkingItems.Content + "</div>";
                    resultItem += "<br />";
                    resultItem += "</li >";
                    resultTag.append(resultItem);
                });
            });
        });
    };
});