<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SocialNetWorkingSearchEngine.Models.SearchModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Search
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table>
<tr><td><input type="text" id="txtParameters" /></td></tr>
<tr><td>
<input type="text" id="txtSearchEngines" /></td></tr>
<tr><td><input type="button" id="btnSearch" onclick="search();" /></td></tr>

<tr><td><textarea id="txtResult"></textarea></td></tr>
</table>
</asp:Content>