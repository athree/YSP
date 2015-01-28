<%@ Page Title="删除设备" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="WebApplication1.DevInfoes.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>确定删除该设备？</h3>
        <asp:FormView runat="server"
            ItemType="WebApplication1.Models.DevInfo" DataKeyNames="DevID"
            DeleteMethod="DeleteItem" SelectMethod="GetItem"
            OnItemCommand="ItemCommand" RenderOuterTable="false">
            <EmptyDataTemplate>
                未找到该设备！
            </EmptyDataTemplate>
            <ItemTemplate>
                <fieldset class="form-horizontal">

                            <div class="row">
								<div class="col-sm-2 text-right">
									<strong>设备名称</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="DevName" ID="DynamicControl1" Mode="ReadOnly" />
								</div>
							</div>
                    
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>设备编号</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="DevID" ID="DevID" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>类别</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Type" ID="Type" Mode="ReadOnly" />
								</div>
							</div>
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
									<strong>GPRS号</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="GPRSID" ID="GPRSID" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>添加时间</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="AddTime" ID="AddTime" Mode="ReadOnly" />
								</div>
							</div>
							<%--<div class="row">
								<div class="col-sm-2 text-right">
									<strong>删除时间</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="DeleteTime" ID="DeleteTime" Mode="ReadOnly" />
								</div>
							</div>--%>
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

