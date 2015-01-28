<%@ Page Title="管理帐户" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="WebApplication1.Account.Manage" %>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>更改你的帐户设置</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>密码:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[更改]" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[创建]" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>

                    <%--
                        电话号码可用作双重身份验证系统中的第二验证因素。
                        请参见<a href="http://go.microsoft.com/fwlink/?LinkId=313242">此文章</a>，
                        以详细了解如何将此 ASP.NET 应用程序设置为支持使用 SMS 进行双重身份验证。
                        设置双重身份验证后，请取消注释以下块
                    --%>

                    
                    <%--
                    <% if (HasPhoneNumber)
                       { %>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[添加]" />
                    </dd>
                    <% }
                       else
                       { %>
                    <dd>
                        <asp:Label Text="" ID="PhoneNumber" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[更改]" /> &nbsp;|&nbsp;
                        <asp:LinkButton Text="[删除]" OnClick="RemovePhone_Click" runat="server" />
                    </dd>
                    <% } %>
                    --%>

                 <%--   <dt>双重身份验证:</dt>
                    <dd>
                        <p>
                            未配置双重身份验证提供程序。请参见<a href="http://go.microsoft.com/fwlink/?LinkId=313242">此文章</a>，
                            以详细了解如何将此 ASP.NET 应用程序设置为支持双重身份验证。
                        </p>--%>
                        <% if (TwoFactorEnabled)
                          { %> 
                        <%--
                        已启用
                        <asp:LinkButton Text="[禁用]" runat="server" CommandArgument="false" OnClick="TwoFactorDisable_Click" />
                        --%>
                        <% }
                          else
                          { %> 
                        <%--
                        已禁用
                        <asp:LinkButton Text="[启用]" CommandArgument="true" OnClick="TwoFactorEnable_Click" runat="server" />
                        --%>
                        <% } %>
                    </dd>
                </dl>
            </div>
        </div>
    </div>

</asp:Content>
