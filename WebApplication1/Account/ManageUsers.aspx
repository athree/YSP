<%@ Page Title="用户管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="WebApplication1.Account.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-info"><%:Title %></h2>
    <asp:PlaceHolder runat="server" ID="Message" Visible="false">
          <p class="text-danger">
                  <asp:Literal runat="server" ID="MsgText" />
          </p>
    </asp:PlaceHolder>
    <blockquote class="pull-right">
        <a href="Register.aspx">添加新用户</a>
        </blockquote>
    <asp:ListView runat="server" ID="AllUsers" AllowPaging="True" OnItemDeleting="AllUsers_ItemDeleting" DataKeyNames="Username">
        <LayoutTemplate>
            <table class="table table-striped table-hover">
            <tr class="warning">
                <th>用户名</th>
                <th>上次登录时间</th>
                <th>操作</th>
            </tr> 
                <tr>
                     <td runat="server" id="itemPlaceholder">
                       </td>
                </tr>
               
                </table>
            <asp:DataPager PageSize="10"  runat="server">
					<Fields>
                        <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-warning" />
                        <asp:NumericPagerField ButtonType="Button"  NumericButtonCssClass="btn" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-warning" />
                    </Fields>
				</asp:DataPager>
        </LayoutTemplate>
       
        <ItemTemplate>
           
            <tr>
                <td>
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text='<%# Eval("CreationDate") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("LastLoginDate") %>'></asp:Label>
            </td>
                <td><asp:LinkButton runat="server" ID="DeleteUser" Text="删除" CommandName="Delete" /></td>
            </tr>
           
        </ItemTemplate>
        
            
        
    </asp:ListView>
    
    
</asp:Content>
