$(document).ready(function () {
    $("#btnSearch").click(onClick);

    function onClick(e) {
        var values = $(":checked").map(function (index, item) { return item.value; });
        var valuesAsString = _.reduce(values, function (all, item) { return all + ',' + item });
        $("#result").html("");
        $.getJSON("Home/SearchResults", { parameters: $("#txtParameters").val(), searchEngines: valuesAsString }, function (json) {
            _.each(json, function (socialNetworkingSearchResult) {
                // mostras el socialNetworkingSearchResult.SocialNetworkingName
                _.each(socialNetworkingSearchResult.SocialNetworkingItems, function (socialNetworkingItems) {
                    // metes un link al socialNetworkingItems.Id
                    // mostras el socialNetworkingItems.StatusDate
                    // mostras el socialNetworkingItems.Content
                    // mostras el socialNetworkingItems.ProfileImage
                    // mostras el socialNetworkingItems.UserName
                    // Ejemplo
                    var result = "<li>";
                    result += "<div>" + socialNetworkingItems.StatusDate + "</div>";
                    result += "<div><b>" + socialNetworkingItems.UserName + "</b></div>";
                    result += "<div><img src='" + socialNetworkingItems.ProfileImage + "' alt='Profile img' /></div>";
                    result += "<div>" + socialNetworkingItems.Content + "</div>";
                    result += "<br />";
                    result += "</li >";
                    $("#result").append(result);
                });
            });
        });
    };
});