<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataBak.aspx.cs" Inherits="WebApplication1.Data.DataBak" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <p>备份路径：D:\data\backup</p>
    <span>当前自动备份间隔：</span>
    <asp:Label runat="server" ID="LB_mIn"></asp:Label>
    <span>个月</span>
    <div>       
        <span>修改备份间隔：</span>
    <asp:DropDownList runat="server" ID="DD_BakInter">
        <asp:ListItem Value="1">一个月</asp:ListItem>
        <asp:ListItem Value="2">两个月</asp:ListItem>
        <asp:ListItem Value="3">三个月</asp:ListItem>
    </asp:DropDownList>
    <asp:Button runat="server" ID="BT_Chage" OnClick="BT_Chage_Click" Text="确定"/>
        </div>
</asp:Content>
