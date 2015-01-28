<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GIS.aspx.cs" Inherits="WebApplication1.DevData.GIS" %>

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
                    <h4 class="modal-title" id="myModalLabel">运行状态--<%=devSite %>.<%=devType%>.<%=devName%></h4>

                </div>

                <div class="modal-body" id="GISModalBody">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
