var tagArrays = new Array();

function getPath(relativePath) {
    return basepath + relativePath;
}

$(document).ready(function () {
    $.socialNetworkingItemNamespace = {};
    $.socialNetworkingItemNamespace.searchResultsItemShowed = new Array();

    //Obtengo los tags
    
    $.getJSON(basepath + "Home/GetAllTags", {}, function (json) {
        _.each(json, function (tag) {
            tagArrays.push(tag.Name);
        });
    });
    
    $("#btnSearch").click(function () {
        search(getTextPattern(), getSelectedSearchEngines());
    });

    $("#txtSearchPattern").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#btnSearch").click();
        }
    });

    $("#imgLoading").hide();
});

//function onSendEmailItemButtonClick(itemId, urlPost, content, button) {
//    var mailBody = content + "\n\n" + urlPost;
//    var destinataries = $("#txtDestinatary" + itemId).val();
//    $("#imgLoading").show();
//    $.post("Home/SendMail", { to: destinataries, subject: 'Social networking', body: mailBody })
//    .success(function (e) {
//        success("Email enviado exitosamente", button);
//        $("#imgLoading").hide();
//    }).error(
//    function (e) {
//        error("Ha ocurrido un error al intentar enviar el mail", button);
//        $("#imgLoading").hide();
//    });
//};

function onSendEmailItemButtonClick(itemId, urlPost, content, button) {
    var item = $.socialNetworkingItemNamespace.searchResultsItemShowed[itemId];
    var destinataries = $("#txtDestinatary" + itemId).val();
    $("#imgLoading").show();

    var urlIconNetwork = window.location.origin + "/" + $("#imgIconFrom" + itemId).attr("src").substr(3);
    var urlIconSentiment = window.location.origin + "/" + $("#imgIconSentiment" + itemId).attr("src").substr(3);

    $.post(basepath + "Home/SendPostToMail", { to: destinataries, subject: 'Social networking', content: item.Content, urlPost: item.UrlPost,
        createdAt: item.CreatedAt, usrName: item.UserName, urlUser: item.UrlProfile, urlImgNetwork: urlIconNetwork,
        urlImgProfile: item.ProfileImage, urlImgSentiment: urlIconSentiment
    })
    .success(function (e) {
        success("Email enviado exitosamente", button);
        $("#imgLoading").hide();
    }).error(
    function (e) {
        error("Ha ocurrido un error al intentar enviar el mail", button);
        $("#imgLoading").hide();
    });
};

function onSaveItemButtonClick(itemId, button) {
    var itemDivId = itemId + "ITEMDIV";
    var item = $.socialNetworkingItemNamespace.searchResultsItemShowed[itemId];

    //Add calification to entity
    var rankingControl = "#stars-wrapper" + itemId;
    var ui = $(rankingControl).data("stars");
    var itemCalification = ui.options.value;
    item.Calification = itemCalification;

    var itemAssignedTags = $("#" + itemDivId.toString() + " li.tagItem").map(function () {
        return $(this).text();
    });

    if (itemAssignedTags.length == 0 && (item.Calification == undefined || item.Calification == null || item.Calification == 0)) {
        error("Debe agregar al menos un tag o calificar el post para poder guardarlo", button);
        return;
    }

    item.Tags = [];
    item.CurrentTags = undefined;
    if (itemAssignedTags.length > 0) {
        item.CurrentTags = _.reduce(itemAssignedTags, function (values, acc) { return acc + "," + values; });
    }

    $.post(basepath + 'Home/SavePost', item).success(function (result) {
        success("Post guardado correctamente", button);
    }).error(function (result) {
        error("Error al guardar post", button);
    });
};

function success(message, control) {
    $('.success-notification').remove();
    var $err = $('<div>').addClass('success-notification')
                             .html('<h2>' + message + '</h2>(click para cerrar)')
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
                             .html('<h2>' + message + '</h2>(click para cerrar)')
                             .css('left', control.position().left);
    control.after($err);
    $err.fadeIn('fast');
    setTimeout(function () {
        $('.error-notification').remove();
    }, 4000);
};

function buildBoxes(statBoxs) {
    $("#boxesContainer").html("");
    _.each(statBoxs, function (box) {
        var boxHtml = "";
        boxHtml += '<div id="' + box.Title + 'box" class="box_segment">';
        boxHtml += '    <h4> ' + box.Title + '</h4> ';
        boxHtml += '    <table class="tableBox">';
        _.each(box.StatItems, function (item) {
            var html = "";
            html += '        <tr>';
            html += '             <td width="80px" style="overflow:hidden;max-width:85px;">';
            html += '                ' + '<a href="#" onclick="callSearch(\'' + item.Title + '\');">' + item.Title + '</a>';
            html += '            </td>';
            html += '            <td >';
            html += '            <div class="chart_bar" style="width:' + item.ValuePercent / 3 + 'px;">&nbsp;</div>';
            html += '            </td>';
            html += '            <td style="text-align:right;">' + item.ValueText + '</td>';
            html += '        </tr>';
            boxHtml += html;
        });
        boxHtml += '    </table>';
        boxHtml += '</div>';

        $("#boxesContainer").append(boxHtml);
    });
};

function callSearch(title) {
    var correctSentimentParam = null;
    var correctSocialNetworkParam = null;
    var correctUserParam = null;
    switch (title.toLowerCase()) {
        case "positivas":
            correctSentimentParam = "positivo";
            break;
        case "negativas":
            correctSentimentParam = "negativo";
            break;
        case "neutras":
            correctSentimentParam = "neutro";
            break;
        case "facebook":
            correctSocialNetworkParam = "facebook";
            break;
        case "twitter":
            correctSocialNetworkParam = "twitter";
            break;
        default:
            correctUserParam = title;
    }

    search(getTextPattern(), getSelectedSearchEngines(), correctSentimentParam, correctSocialNetworkParam, correctUserParam);
};

function getTextPattern() {
    return $("#txtSearchPattern").val();
};

function getSelectedSearchEngines() {
    return $(":checked").map(function (value, index) { return index.value; });
};

function search(parameters, searchEngines, sentiment, socialNetwork, user) {

    $("#imgLoading").show();

    if (searchEngines === undefined || searchEngines.length == 0 ||
            parameters === "" || parameters === null || parameters === undefined) {
        $("#imgLoading").hide();
        error("El ingreso de par&aacute;metros de b&uacute;squeda y selecci&oacute;n de al menos un motor de b&uacute;squeda es obligatorio", $("#btnSearch"));
        return;
    }
    var searchEnginesAsString = _.reduce(searchEngines, function (memo, currentItem) { return memo + ',' + currentItem; });

    $.getJSON(basepath + "Home/SearchResults", { parameters: parameters, searchEngines: searchEnginesAsString, sentiment: sentiment, socialNetworking: socialNetwork, userName: user }, function (json) {

        var result_listTag = $("#search_result_list");
        result_listTag.html("");

        buildBoxes(json.StatBoxs);

        _.each(json.Items, function (socialNetworkingItems) {

            $.socialNetworkingItemNamespace.searchResultsItemShowed[socialNetworkingItems.Id] = socialNetworkingItems;
            var itemId = socialNetworkingItems.Id + "ITEMDIV";
            var item_result = "<div id=\"" + itemId + "\"" + " class=\"result clearfix\">";    //result item
            item_result += "<div class=\"icon\">";
            item_result += "<img id=\"imgIconSentiment" + socialNetworkingItems.Id + "\"  class=\"icon sentiment\"\">"; //sentiment icon							                                  //icon
            item_result += "<img id=\"imgIconFrom" + socialNetworkingItems.Id + "\"  class=\"icon\"\">";   //social media icon               
            item_result += "</div>"; //icon
            item_result += "<div>"; //body
            item_result += "<div>"; //header post

            item_result += "<div style=\"float: right;\">";
            item_result += "<div id=\"stars-wrapper" + socialNetworkingItems.Id + "\">";
            item_result += "<select name=\"selrate\">";
            item_result += "<option value=\"1\">Very poor</option>";
            item_result += "<option value=\"2\">Not that bad</option>";
            item_result += "<option value=\"3\">Average</option>";
            item_result += "<option value=\"4\">Good</option>";
            item_result += "<option value=\"5\">Perfect</option>";
            item_result += "</select>";
            item_result += "</div>";
            item_result += "<div style=\"float: right\">"; //Save
            item_result += "<input id=\"btnSave" + socialNetworkingItems.Id + "\" type=\"image\" class='dynamicSrc' src=\"Content/Save-icon.png\" style=\"vertical-align: middle; height: 20px;\"/>";
            item_result += "</div>";
            item_result += "</div>";

            item_result += "<div>";
            item_result += "<h3>"; //result title
            item_result += "<a href=\"" + socialNetworkingItems.UrlPost + "\" target=\"_blank\" title=\"" + socialNetworkingItems.Content + "\">" + socialNetworkingItems.Content.substring(0, 150) + "..." + "<\a>"; //link
            item_result += "</h3>";
            item_result += "</div>";
            item_result += "</div>"; //header post
            item_result += "<div class=\"info\"> <p>";  //Info
            item_result += "El " + socialNetworkingItems.CreatedAtShort + " por ";
            item_result += "<img src=\"" + socialNetworkingItems.ProfileImage + "\" class=\"user_image\"\>";
            item_result += " <a href=\"" + socialNetworkingItems.UrlProfile + "\" target=\"_blank\">" + socialNetworkingItems.UserName + "<\a>";
            item_result += "</p></div>";  //Info
            item_result += "<div style=\"float: left\"><p>Tag:</p></div>"; //result tag seccion
            item_result += "<ul></ul>";
            item_result += "</div>"; 				//result tag seccion

            item_result += "<div>"; //Send email and save
            item_result += "<a href=\"javascript:void(0)\" id=\"aSendEmail" + socialNetworkingItems.Id + "\">Enviar e-mail</a>";
            item_result += "</div>";
            item_result += "<div id=\"divSendTo" + socialNetworkingItems.Id + "\" style=\"display: none;\" >";
            item_result += "<p style=\"margin: 10px 0px 0px 0px; line-height: 0;\"> Para: </p>";
            item_result += "<input id=\"txtDestinatary" + socialNetworkingItems.Id + "\" type=\"text\" style=\"height: 12px; vertical-align: middle; font-size: 12px\"/>";
            item_result += "<input id=\"btnSendEmail" + socialNetworkingItems.Id + "\" type=\"image\" class='dynamicSrc' src=\"Content/48x48-send_e-mail.png\" style=\"vertical-align: middle; height: 35px;\"/>";
            item_result += "</div>";
            item_result += "</div>"; 	//result item

            result_listTag.append(item_result);

            //Creo la lista con tags
            var itemTagContainers = $("#" + itemId + " ul");
            itemTagContainers.each(function (i, e) {
                $(e).tagHandler({
                    msgNoNewTag: "No tiene permisos para crear un nuevo tag",
                    msgError: "No se pudo cargar la lista de tag",
                    availableTags: tagArrays,
                    autocomplete: true,
                    allowAdd: false,
                    assignedTags: _.map(socialNetworkingItems.Tags, function (tag) {
                        return tag.Name;
                    })
                });
            });

            // sentiment image
            if (socialNetworkingItems.Sentiment != "undefined" && socialNetworkingItems.Sentiment != null) {
                switch (socialNetworkingItems.Sentiment.toLowerCase()) {
                    case "positivo":
                        $("#imgIconSentiment" + socialNetworkingItems.Id).attr("src", "Content/sentiment_positive.png");
                        break;
                    case "neutro":
                        $("#imgIconSentiment" + socialNetworkingItems.Id).attr("src", "Content/sentiment_neutral.png");
                        break;
                    case "negativo":
                        $("#imgIconSentiment" + socialNetworkingItems.Id).attr("src", "Content/sentiment_negative.png");
                        break;
                    default:
                        $("#imgIconSentiment" + socialNetworkingItems.Id).attr("src", "");
                };
            };

            // network from image
            if (socialNetworkingItems.SocialNetworkName != "undefined" && socialNetworkingItems.SocialNetworkName != null) {
                switch (socialNetworkingItems.SocialNetworkName.toLowerCase()) {
                    case "facebook":
                        $("#imgIconFrom" + socialNetworkingItems.Id).attr("src", "Content/facebook_icon.ico");
                        break;
                    case "twitter":
                        $("#imgIconFrom" + socialNetworkingItems.Id).attr("src", "Content/twitter_icon.ico");
                        break;
                    default:
                        $("#imgIconFrom" + socialNetworkingItems.Id).attr("src", "Content/search_item_icon.gif");
                };
            };



            $("#btnSave" + socialNetworkingItems.Id).click(function (e) {
                onSaveItemButtonClick(socialNetworkingItems.Id, $(this));
            });

            $("#btnSendEmail" + socialNetworkingItems.Id).click(function (e) {
                onSendEmailItemButtonClick(socialNetworkingItems.Id, socialNetworkingItems.UrlPost, socialNetworkingItems.Content, $(this));
            });

            $("#aSendEmail" + socialNetworkingItems.Id).click(function () {
                $("#divSendTo" + socialNetworkingItems.Id).slideToggle('slow', function () {

                });
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

        $(".dynamicSrc,#imgIconFrom,#imgIconSentiment").each(function (e) { e.src = getPath(e.src); });
    });
};
