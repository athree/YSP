<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>我的远程调试系统</h1>
        <p class="lead">该远程调试系统通过GPRS设备实现远程设备信息查看，以及调试。</p>
        <p><a class="btn btn-primary btn-lg">更多 &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-3">
            <h2>分布情况</h2>
            <p>
               
            </p>
            <p>
                <a class="btn btn-default" href="map">查看 &raquo;</a>
            </p>
        </div>
<%--        <div class="col-md-4">
            <h2>报表</h2>
            <p>
                报表设置及导出
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">查看 &raquo;</a>
            </p>
        </div>--%>
        <div class="col-md-3">
            <h2>设备管理</h2>
            <p>
            </p>
            <p>
                <a class="btn btn-default" href="DevInfoes/Default">查看 &raquo;</a>
            </p>
        </div>
          <div class="col-md-3">
            <h2>站点管理</h2>
            <p>
               
            </p>
            <p>
                <a class="btn btn-default" href="DevSites/Default">查看 &raquo;</a>
            </p>
        </div>
        
        <div class="col-md-3">
            <h2>数据备份</h2>
            <p>
            </p>
            <p>
                <a class="btn btn-default" href="Data/DataBak">查看 &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
