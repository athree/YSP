<%@ Page Title="管理帐户" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="WebApplication1.Account.Manage" %>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-info"><%: Title %>：</h2>

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
                    <dt>密码：</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[更改]" Visible="false" ID="ChangePassword" runat="server" />                       
                    </dd>                   
                </dl>
            </div>
        </div>
    </div>

</asp:Content>
