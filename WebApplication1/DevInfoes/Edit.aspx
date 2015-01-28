<%@ Page Title="修改设备信息" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="WebApplication1.DevInfoes.Edit" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <asp:FormView runat="server" ID="MyFormView"
            ItemType="WebApplication1.Models.DevInfo" DefaultMode="Edit" DataKeyNames="DevID"
            UpdateMethod="UpdateItem" SelectMethod="GetItem"
            OnItemCommand="ItemCommand" RenderOuterTable="false">
            <EmptyDataTemplate>
                未找到该设备！
            </EmptyDataTemplate>
            <EditItemTemplate>
                <fieldset class="form-horizontal">
                    <legend><%:Title %></legend>
                    <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />

                    <asp:DynamicControl Mode="Edit" DataField="DevName" runat="server" />

                    <%--<asp:DynamicControl Mode="Edit" DataField="DevID" runat="server" />--%>
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="col-sm-2 control-label">设备编号</asp:Label>
                        <div class="col-sm-3">
                            <asp:DynamicControl runat="server" DataField="DevID" ID="DevID" Mode="ReadOnly" />
                        </div>
                    </div>
                   
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="col-sm-2 control-label">设备类型</asp:Label>
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="MyDevType" SelectMethod="GetDevType" ItemType="WebApplication1.Models.DevType" DataTextField="TypeName" DataValueField="TypeName" CssClass="form-control DDDropDownList" />
                     </div>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="col-sm-2 control-label">单位名称</asp:Label>
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="MyCompName" SelectMethod="GetDevSite" ItemType="WebApplication1.Models.DevSite" DataTextField="CompName" DataValueField="CompName" CssClass="form-control DDDropDownList" />
                        </div>
                    </div>
                    <asp:DynamicControl Mode="Edit" DataField="GPRSID" runat="server" />
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Button runat="server" ID="UpdateButton" CommandName="Update" Text="更新" CssClass="btn btn-primary" />
                            <asp:Button runat="server" ID="CancelButton" CommandName="Cancel" Text="取消" CausesValidation="false" CssClass="btn btn-default" />
                        </div>
                    </div>
                </fieldset>
            </EditItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

