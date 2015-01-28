<%@ Page Title="添加站点" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Insert.aspx.cs" Inherits="WebApplication1.DevSites.Insert" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
         <div id="MyDiv">
            
            <%-- <div class="form-group">--%>
             <div id="MyContainer" style="float:left;padding:5px">
                 <asp:Label runat="server" ID="Label1" Text="输入地址："></asp:Label>

             </div>
             <asp:TextBox runat="server" ID="MyText" style="width:300px;"></asp:TextBox>                    
               
             <asp:label runat="server" ID="SearchLocate" CssClass="btn btn-default" Text="在地图中查找" />
             <asp:label runat="server" ID="ChooseLocate" CssClass="btn btn-default" data-toggle="modal" data-target="#myModal" Text=" 确认位置 " />
            <%-- </div>--%>


              <div id="createNewSiteMap" style="width:auto;height:400px;margin:auto"> </div>
              <div class="modal fade" ID="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                             &times;

                        </button>
                        <h4 class="modal-title" id="myModalLabel">
                            添加站点

                        </h4>

                    </div>
                        <div class="modal-body">
                             <asp:FormView runat="server" ItemType="WebApplication1.Models.DevSite" DefaultMode="Insert"
                                 InsertItemPosition="FirstItem" InsertMethod="InsertItem"
                                 OnItemCommand="ItemCommand" RenderOuterTable="false">
                                 <InsertItemTemplate>
                                     <fieldset class="form-horizontal">
                                         <legend><%:Title %></legend>
                                         <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />                             
                                         <div class="form-group">
                                             <asp:DynamicControl ID="CompName" Mode="Insert" DataField="CompName" runat="server" />
                                            
                                             <asp:DynamicControl ID="Address" Mode="Insert" DataField="Address" runat="server" />
                                            
                                             <asp:DynamicControl Mode="Insert" DataField="Lng" runat="server" />
                                            
						                     <asp:DynamicControl Mode="Insert" DataField="Lat" runat="server" />
                                           
                                         </div>   
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
                                         
                    </div>               

              </div>

         </div>  

         </div>

    </div>


     <script src="http://api.map.baidu.com/api?v=1.4"></script>
        <%: Scripts.Render("~/Scripts/getLocate.js") %>
</asp:Content>
