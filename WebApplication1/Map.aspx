<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="WebApplication1.Map" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="aa" style="position: relative">
        <div id="map" style="width: 100%; height: 90%; overflow: hidden;">
        </div>
    </div>

    <%--div展示某站点的各类设备--%>
    <div id="myDev" style="position: absolute; left: 100px; top: 100px; width: 0; height: 0; z-index: -1; overflow: scroll; /*filter: alpha(opacity=80); -moz-opacity: 0.8; opacity: 0.8; */border-style: ridge; border-width: 5px">
        <a id="closeMyDev" class="btn btn-default btn-xs" style="float: right">x</a>
        <p></p>
        <p id="devTitle" class="lead" style="text-align: center; color: orange"></p>
    </div>


    <%--第一个modal弹窗，展示具体设备的运行状态33--%>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
     
    </div>
  
   

    <script src="http://api.map.baidu.com/api?v=2.0&ak=OY0PC22e3GNpu0vm2yOKGT6r"></script>
    <script src="Scripts/map.js"></script>
</asp:Content>
