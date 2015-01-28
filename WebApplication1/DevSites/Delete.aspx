<%@ Page Title="删除站点" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="WebApplication1.DevSites.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>确定删除该站点？</h3>
        <asp:FormView runat="server"
            ItemType="WebApplication1.Models.DevSite" DataKeyNames="LocateID"
            DeleteMethod="DeleteItem" SelectMethod="GetItem"
            OnItemCommand="ItemCommand" RenderOuterTable="false">
            <EmptyDataTemplate>
                找不到该站点！
            </EmptyDataTemplate>
            <ItemTemplate>
                <fieldset class="form-horizontal">
                    <legend></legend>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>单位名称</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="CompName" ID="CompName" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>经度</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Lng" ID="Lng" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>纬度</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Lat" ID="Lat" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>详细地址</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Address" ID="Address" Mode="ReadOnly" />
								</div>
							</div>
                 	<div class="row">
					  &nbsp;
					</div>
					<div class="form-group">
						<div class="col-sm-offset-2 col-sm-10">
							<asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" CssClass="btn btn-danger" />
							<asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" CssClass="btn btn-default" />
						</div>
					</div>
                </fieldset>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

