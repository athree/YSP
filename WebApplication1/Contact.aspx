<%@ Page Title="联系方式" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebApplication1.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <%--<h3>联系方式</h3>--%>
    <address>
        <%--Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100--%>
    </address>

    <address>
        <strong>技术支持:</strong>   <%--<a href="mailto:Support@example.com"></a><br />--%>
        <%--<strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>--%>
    </address>
</asp:Content>
