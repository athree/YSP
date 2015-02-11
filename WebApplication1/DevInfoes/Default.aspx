<%@ Page Title="设备列表" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.DevInfoes.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2 class="text-info"><%:Title %></h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="添加新设备" />
    </p>
    <div>
        <asp:ListView id="ListView1" runat="server"
            DataKeyNames="DevID" 
			ItemType="IMserver.Models.DevInfo"
            SelectMethod="GetData">
            <EmptyDataTemplate>
                当前无数据！
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table">
                    <thead>
                        <tr class="warning">
                             <th>
								<asp:LinkButton Text="设备名称" CommandName="Sort" CommandArgument="DevName" runat="Server" />
							</th>
                            <th>
								<asp:LinkButton Text="设备编号" CommandName="Sort" CommandArgument="DevID" runat="Server" />
							</th>
                            <th>
								<asp:LinkButton Text="类别" CommandName="Sort" CommandArgument="Type" runat="Server" />
							</th>
                            <th>
								<asp:LinkButton Text="单位名称" CommandName="Sort" CommandArgument="CompName" runat="Server" />
							</th>
                            <th>
								<asp:LinkButton Text="GPRS号" CommandName="Sort" CommandArgument="GPRSID" runat="Server" />
							</th>
                            <th>
								<asp:LinkButton Text="添加时间" CommandName="Sort" CommandArgument="AddTime" runat="Server" />
							</th>
                          <%--  <th>
								<asp:LinkButton Text="删除时间" CommandName="Sort" CommandArgument="DeleteTime" runat="Server" />
							</th>--%>
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
								<asp:DynamicControl runat="server" DataField="DevName" ID="DynamicControl1" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="DevID" ID="DevID" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Type" ID="Type" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="CompName" ID="CompName" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="GPRSID" ID="GPRSID" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="AddTime" ID="AddTime" Mode="ReadOnly" />
							</td>
							<%--<td>
								<asp:DynamicControl runat="server" DataField="DeleteTime" ID="DeleteTime" Mode="ReadOnly" />
							</td>--%>
                    <td>
					   <%-- <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/DevInfo_view/Details", Item.DevID) %>' Text="详情" /> | --%>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/DevInfoes/Edit", Item.DevID) %>' Text="编辑" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/DevInfoes/Delete", Item.DevID) %>' Text="删除" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

