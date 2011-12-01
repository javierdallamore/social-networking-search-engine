function search() {
    $.getJSON("Searcht", { parameters: $("#txtParameters").val(), searchEngines: $("#txtSearchEngines").val() }, function (json) {
        var result = "";
        _.each(json[0].SocialNetworkingItems, function (item) { result += item.Content + "\n"; });
        $("#txtResult").val(result);
    });
};