<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectGasData.aspx.cs" Inherits="WebApplication1.DevInfoes.SelectGasData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align:center">
        <asp:GridView ID="GridView1" runat="server" Width="100%" 
            AutoGenerateSelectButton="True" DataKeyNames="Num" 
            ondatabound="GridView1_DataBound" onrowcreated="GridView1_RowCreated" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" AllowPaging="True" 
            onpageindexchanging="GridView1_PageIndexChanging" PageSize="20">
            <SelectedRowStyle BackColor="#CCFFCC" />
        </asp:GridView>
        <asp:Button ID="BT_Return" runat="server" onclick="BT_Return_Click"/>
    </div>
</asp:Content>
