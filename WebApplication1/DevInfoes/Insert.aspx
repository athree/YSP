<%@ Page Title="添加设备" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="WebApplication1.DevInfoes.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <asp:FormView runat="server" ID="MyFormView"
            ItemType="IMserver.Models.DevInfo" DefaultMode="Insert"
            InsertItemPosition="FirstItem" InsertMethod="InsertItem"
            OnItemCommand="ItemCommand" RenderOuterTable="false">
            <InsertItemTemplate>
                <fieldset class="form-horizontal">
				<legend><%:Title %></legend>
		        <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
                    <asp:DynamicControl Mode="Insert" DataField="DevName" runat="server"/>
                    <asp:DynamicControl Mode="Insert" DataField="DevID" runat="server"/>
					<%--<asp:DynamicControl Mode="Insert" DataField="Type" runat="server"/>--%>
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="col-sm-2 control-label">设备类型</asp:Label>
                            <div class="col-sm-3">
                                <asp:DropDownList runat="server" ID="MyDevType" SelectMethod="GetDevType" ItemType="IMserver.Models.DevType" DataTextField="TypeName" DataValueField="TypeName" CssClass="form-control DDDropDownList"/>
                            </div>

                    </div>
					<%--<asp:DynamicControl Mode="Insert" DataField="CompName" runat="server"/>--%>
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-sm-2 control-label">单位名称</asp:Label>
                    <div class="col-sm-3">
                    <asp:DropDownList runat="server" ID="MyCompName" SelectMethod="GetDevSite" ItemType="IMserver.Models.DevSite" DataTextField="CompName" DataValueField="CompName" CssClass="form-control DDDropDownList" />	
					</div>
                    </div>      
                    <asp:DynamicControl Mode="Insert" DataField="GPRSID" runat="server"/>              
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button runat="server" ID="InsertButton" CommandName="Insert" Text="添加" CssClass="btn btn-primary" />
                            <asp:Button runat="server" ID="CancelButton" CommandName="Cancel" Text="取消" CausesValidation="false" CssClass="btn btn-default" />
                        </div>
					</div>
                </fieldset>
            </InsertItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>
