<%@ Page Title="站点列表" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.DevSites.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2><%:Title %></h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="添加新地点" />
    </p>
    <div>
        <asp:ListView id="ListView1" runat="server"
            DataKeyNames="LocateID" 
			ItemType="IMserver.Models.DevSite"
            SelectMethod="GetData">
            <EmptyDataTemplate>
                当前无数据！
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table">
                    <thead>
                        <tr class="warning">
                            <th>
								<asp:LinkButton Text="单位名称" CommandName="Sort" CommandArgument="CompName" runat="Server" />
							</th>
                            <th>
								<asp:LinkButton Text="经度" CommandName="Sort" CommandArgument="Lng" runat="Server" />
							</th>
                            <th>
								<asp:LinkButton Text="纬度" CommandName="Sort" CommandArgument="Lat" runat="Server" />
							</th>
                            <th>
								<asp:LinkButton Text="详细地址" CommandName="Sort" CommandArgument="Address" runat="Server" />
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
                        <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn" />
                        <asp:NumericPagerField ButtonType="Button"  NumericButtonCssClass="btn" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn" />
                    </Fields>
				</asp:DataPager>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
							<td>
								<asp:DynamicControl runat="server" DataField="CompName" ID="CompName" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Lng" ID="Lng" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Lat" ID="Lat" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Address" ID="Address" Mode="ReadOnly" />
							</td>
                    <td>
					  <%--  <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/DevSites/Details", Item.LocateID) %>' Text="Details" /> --%>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/DevSites/Edit", Item.LocateID) %>' Text="修改" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/DevSites/Delete", Item.LocateID) %>' Text="删除" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

