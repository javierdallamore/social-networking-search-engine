<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.SearchModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Search
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <input type="text" id="txtParameters" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="checkbox" name="engine" value="SearchEngineMock" id="chSearchEngineMock" />
                <label for="chSearchEngineMock">SearchEngineMock</label>
            </td>
        </tr>
        <tr>
            <td>
                <input type="checkbox" name="engine" id="chTwitterSearchEngine" value="TwitterSearchEngine" checked="checked" />
                <label for="chTwitterSearchEngine">TwitterSearchEngine</label>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnSearch" value="Search" />
            </td>
        </tr>
    </table>
    <ul id="result"></ul>
</asp:Content>
