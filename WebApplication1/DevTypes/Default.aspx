<%@ Page Title="设备列表" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.DevTypes.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2 class="text-info"><%:Title %></h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="添加新类别" />
    </p>
    <div>
        <asp:ListView id="ListView1" runat="server"
            DataKeyNames="TypeID" 
			ItemType="WebApplication1.Models.DevType"
            SelectMethod="GetData">
            <EmptyDataTemplate>
                当前无数据！
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table">
                    <thead>
                        <tr class="warning">
                            
                            <th>
								<asp:LinkButton Text="设备类别" CommandName="Sort" CommandArgument="Type" runat="Server" />
							</th>
                           
                            <th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
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
								<asp:DynamicControl runat="server" DataField="TypeName" ID="DynamicControl1" Mode="ReadOnly" />
							</td>
							
                    <td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/DevTypes/Edit", Item.TypeId.ToString()) %>' Text="编辑" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/DevTypes/Delete", Item.TypeId.ToString()) %>' Text="删除" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

