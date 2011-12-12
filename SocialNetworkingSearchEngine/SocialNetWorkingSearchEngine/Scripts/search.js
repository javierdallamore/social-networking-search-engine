$(document).ready(function () {

    $.socialNetworkingItemNamespace = {};
    $.socialNetworkingItemNamespace.searchResultsItemShowed = new Array();

    //Obtengo los tags
    var tagArrays = new Array();
    $.getJSON("Home/GetAllTags", {}, function (json) {
        _.each(json, function (tag) {
            tagArrays.push(tag.Name);
        });
    });

    $("#btnSearch").click(onClick);
    $("#imgLoading").hide();

    function onClick(e) {
        $("#imgLoading").show();
        var values = $(":checked").map(function (value, index) { return index.value; });
        var valuesAsString = _.reduce(values, function (memo, currentItem) { return memo + ',' + currentItem; });
        $("#result").html("");
        $.getJSON("Home/SearchResults", { parameters: $("#txtSearchPattern").val(), searchEngines: valuesAsString }, function (json) {

            var result_listTag = $("#search_result_list");
            result_listTag.html("");

            _.each(json, function (socialNetworkingItems) {

                $.socialNetworkingItemNamespace.searchResultsItemShowed[socialNetworkingItems.Id] = socialNetworkingItems;
                var itemId = socialNetworkingItems.Id + "ITEMDIV";
                var item_result = "<div id=\"" + itemId + "\"" + " class=\"result clearfix\">";    //result item
                item_result += "<div class=\"icon\">";
                item_result += "<img src=\"" + socialNetworkingItems.SentimentIconPath + "\"" + " class=\"icon sentiment\"\">"; //sentiment icon							                                  //icon
                item_result += "<img src=\"" + socialNetworkingItems.SocialNetworkIconPath + "\"" + " class=\"icon\"\">";   //social media icon               
                item_result += "</div>"; 						                                                            //icon
                item_result += "<div>"; 							                                                        //body
                item_result += "<div>"; //header post

                item_result += "<div style=\"float: right;\">";
                //item_result += "Rating: <span id=\"stars-cap\"></span>";
                item_result += "<div id=\"stars-wrapper" + socialNetworkingItems.Id + "\">";
                item_result += "<select name=\"selrate\">";
                item_result += "<option value=\"1\">Very poor</option>";
                item_result += "<option value=\"2\">Not that bad</option>";
                item_result += "<option value=\"3\">Average</option>";
                item_result += "<option value=\"4\">Good</option>";
                item_result += "<option value=\"5\">Perfect</option>";
                item_result += "</select>";
                item_result += "</div>";
                item_result += "</div>";

                item_result += "<div>";
                item_result += "<h3>"; 							                                                            //result title
                item_result += "<a href=\"" + socialNetworkingItems.UrlPost + "\" target=\"_blank\">" + socialNetworkingItems.Content + "<\a>"; //link
                item_result += "</h3>"; 							                                                        //result title
                item_result += "</div>";
                item_result += "</div>"; //header post
                item_result += "<div class=\"info\"> <p>";                                                                  //Info
                item_result += "El " + socialNetworkingItems.CreatedAtShort + " por ";
                item_result += "<img src=\"" + socialNetworkingItems.ProfileImage + "\" class=\"user_image\"\>";
                item_result += " <a href=\"" + socialNetworkingItems.UrlProfile + "\" target=\"_blank\">" + socialNetworkingItems.UserName + "<\a>";
                item_result += "</p></div>";                                                                                //Info
                item_result += "<div><p>Tag it:</p>"; 							                                            //result tag seccion
                item_result += "<ul></ul>";
                item_result += "</div>"; 						                                                            //result tag seccion

                item_result += "<div>"; 							                                                        //save
                item_result += "<a href=\"javascript:void(0)\" id=\"aSendEmail" + socialNetworkingItems.Id + "\">Send by Email</a>";
                item_result += "<input id=\"btnSave" + socialNetworkingItems.Id + "\" type=\"button\" value=\"Save\" />";
                item_result += "</div>";                                                                                    //save
                item_result += "<div id=\"divSendTo" + socialNetworkingItems.Id + "\" style=\"display: none;\" >";
                item_result += "<p style=\"margin: 10px 0px 0px 0px; line-height: 0;\"> Para: </p>";
                item_result += "<input id=\"txtDestinatary" + socialNetworkingItems.Id + "\" type=\"text\" style=\"height: 12px; vertical-align: middle; font-size: 12px\"/>";
                item_result += "<input id=\"btnSendEmail" + socialNetworkingItems.Id + "\" type=\"image\" src   =\"../Content/48x48-send_e-mail.png\" style=\"vertical-align: middle; height: 35px;\"/>";
                item_result += "</div>";

                item_result += "</div>"; 						                                                            //result item

                result_listTag.append(item_result);

                $("#aSendEmail" + socialNetworkingItems.Id).click(function () {
                    $("#divSendTo" + socialNetworkingItems.Id).slideToggle('slow', function () {

                    });
                });


                //Creo la lista con tags
                var itemTagContainers = $("#" + itemId + " ul");
                itemTagContainers.each(function (i, e) {
                    $(e).tagHandler({
                        availableTags: tagArrays,
                        autocomplete: true,
                        allowAdd: false,
                        assignedTags: _.map(socialNetworkingItems.Tags, function (tag) {
                            return tag.Name;
                        })
                    });
                });

                $("#btnSave" + socialNetworkingItems.Id).click(function (e) {
                    onSaveItemButtonClick(socialNetworkingItems.Id, $(this));
                });

                $("#btnSendEmail" + socialNetworkingItems.Id).click(function (e) {
                    onSendEmailItemButtonClick(socialNetworkingItems.Id, socialNetworkingItems.UrlPost, socialNetworkingItems.Content);
                });

                $("#stars-wrapper" + socialNetworkingItems.Id + " select option").each(function () {
                    if (parseInt($(this).val()) === socialNetworkingItems.Calification) {
                        $(this).attr("selected", "selected");
                    }
                });
            });

            //Create the calification control
            _.each($("#search_result_list"), function () {
                $("div[id^='stars-wrapper']").each(function (i, e) {
                    $(e).stars({
                        inputType: "select"
                    });
                });
            });
            $("#imgLoading").hide();
        });
    };

    function onSendEmailItemButtonClick(itemId, urlPost, content) {
        var mailBody = content + "\n\n" + urlPost;
        var destinataries = $("#txtDestinatary" + itemId).val();
        $.post("Home/SendMail", { to: destinataries, subject: 'Social networking', body: mailBody },
            function callback() {

            },
            function errCallback() {

            });
    };

    function onSaveItemButtonClick(itemId, button) {
        var itemDivId = itemId + "ITEMDIV";
        var itemAssignedTags = $("#" + itemDivId.toString() + " li.tagItem").map(function () {
            return $(this).text();
        });
        var item = $.socialNetworkingItemNamespace.searchResultsItemShowed[itemId];
        item.Tags.push(itemAssignedTags);

        //Add calification to entity
        var rankingControl = "#stars-wrapper" + itemId;
        var ui = $(rankingControl).data("stars");
        var itemCalification = ui.options.value;
        item.Calification = itemCalification;

        item.Tags = [];
        item.CurrentTags = _.reduce(itemAssignedTags, function (values, acc) { return acc + "," + values; });

        $.post('Home/SavePost', item).success(function (result) {
            success("Post saved successfully", button);
        }).error(function (result) {
            error("Error saving post", button);
        });
    };
});

function success(message, control) {
    $('.success-notification').remove();
    var $err = $('<div>').addClass('success-notification')
                             .html('<h2>' + message + '</h2>(click on this box to close)')
                             .css('left', control.position().left);
    control.after($err);
    $err.fadeIn('slow');
    setTimeout(function () {
        $('.success-notification').remove();
    }, 2000);
};

function error(message, control) {
    $('.error-notification').remove();
    var $err = $('<div>').addClass('error-notification')
                             .html('<h2>' + message + '</h2>(click on this box to close)')
                             .css('left', control.position().left);
    control.after($err);
    $err.fadeIn('fast');
    setTimeout(function () {
        $('.error-notification').remove();
    }, 2000);
};
