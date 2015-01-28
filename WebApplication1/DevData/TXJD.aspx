<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TXJD.aspx.cs" Inherits="WebApplication1.TXJD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="modal-dialog">

            <div class="modal-content" style="width: 800px; height: 600px; overflow-y: scroll; overflow-x: hidden">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;
                    </button>
                    <h4 class="modal-title" id="TXJDModalLabel">铁芯接地</h4>

                </div>

                <div class="modal-body" id="TXJDModalBody">
                    <asp:formview runat="server"></asp:formview>
                     <p>testAAAAAAAAAAAAA  TXJD</p>
                    <p id="my"></p>
                    <asp:label id="myLabel" runat="server"></asp:label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
