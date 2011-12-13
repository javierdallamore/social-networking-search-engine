<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.SearchModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Search
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="splash">
        <%--Search seccion--%>
        <div class="search_block">
            <div id="search">
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <input id="txtSearchPattern" type="text">
                            </td>
                            <td>
                                <input id="btnSearch" type="button" value="Buscar">
                            </td>
                            <td><img id="imgLoading" src="../../Content/loading36.gif" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div>
                <div id="sources" class="sources_box clearfix">
                    <div style="float: left; width: 125px; font-size: 15px;">
                        <label>
                            <input type="checkbox" name="src[]" value="SavedPosts" checked="checked">
                            Local DB</label></div>
                    <div style="float: left; width: 125px; font-size: 15px;">
                        <label>
                            <input type="checkbox" name="src[]" value="FacebookSearchEngine" checked="checked">
                            Facebook</label></div>
                    <div style="float: left; width: 125px; font-size: 15px;">
                        <label>
                            <input type="checkbox" name="src[]" value="TwitterSearchEngine" checked="checked">
                            Twitter</label></div>
                </div>
            </div>
        </div>

        <%--Lext Boxes--%>
        <div id="boxesContainer">
        </div>

        <%--Search results--%>
        <div id="container_results">
            <div id="result_list">
                <div id="search_result_header"></div>
                    <ul id="search_result_list"></ul>
            </div>
        </div>
    </div>
</asp:Content>
