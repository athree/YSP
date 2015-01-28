<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectData.aspx.cs" Inherits="WebApplication1.DevInfoes.SelectData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $('#ErrorMsg').hide();
            $("td input[type='text']").width(60);
            $("#alarmSetModal tr").height(50);
            var errorMsg="<%=ViewState["ErrorMsg"].ToString()%>";
            if(errorMsg!="")
            {
                $('#ErrorMsg p').text(errorMsg);
                $('#ErrorMsg').show();
            }                

        })

   </script>


    
   <div class="form-group">
       <div id="ErrorMsg" class="alert alert-danger" >
    <button type="button" class="close" data-dismiss="alert">×</button>
    <%--<h4>出错!</h4>--%>
    <p></p>
    </div>
       </div>

    <div>
     <table style="width: 100%;text-align:center">
            <tr>
                <td>
                    <asp:Button ID="bt_Sel" runat="server" onclick="bt_Sel_Click"/>
                </td>
                <td>
                    <asp:Button ID="bt_return" runat="server" OnClick="bt_Return_Click" />
                </td>
            </tr>
            <tr>
                <td>
                <div class="table_style">
                <asp:CheckBoxList ID="cbl_Data" runat="server" AutoPostBack="True" 
                        RepeatColumns="4" RepeatDirection="Horizontal" 
                        onselectedindexchanged="cbl_Data_SelectedIndexChanged">
                    </asp:CheckBoxList>
                </div>
                    
                </td>
            </tr>
        </table>
        </div>
</asp:Content>
