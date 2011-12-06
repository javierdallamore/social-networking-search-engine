$(document).ready(function() {
    $("#tuboton").click(onClick);

    function onClick(e) {
        var valoresDeCheckbox = $("#txtSearchEngines").val();
        $.getJSON("SearchResults", { parameters: $("#txtParameters").val(), searchEngines: valoresDeCheckbox }, function (json) {
            _.each(json, function (socialNetworkingSearchResult) {
                // mostras el socialNetworkingSearchResult.SocialNetworkingName
                _.each(socialNetworkingSearchResult.SocialNetworkingItems, function (socialNetworkingItems) {
                    // metes un link al socialNetworkingItems.Id
                    // mostras el socialNetworkingItems.StatusDate
                    // mostras el socialNetworkingItems.Content
                    // mostras el socialNetworkingItems.ProfileImage
                    // mostras el socialNetworkingItems.UserName
                    });
                });
            });
        };
});