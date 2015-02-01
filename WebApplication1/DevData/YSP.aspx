<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YSP.aspx.cs" Inherits="WebApplication1.DevData.YSP" EnableViewState="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <%--<script src="../Scripts/jquery-1.10.2.min.js"></script>--%>
</head>
<body>
    <form id="YSPform" runat="server">
     
        <div class="modal-dialog">

            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;
                    </button>
                    <h4 class="modal-title" id="myModalLabel">运行状态</h4>
                     <h4><%=devSite %>.<%=devType%>.<%=devName%></h4>
                     <p class="h4 text-success">时间:<%: DateTime.Now %> </p>
                    <div style="float:right">
                    <a class="ntwinkle" ></a>
                        <a>通讯</a>
                        </div>
                </div>
              
                <div class="modal-body" id="myModalBody" style="height:350px">
                   <div id="alarmDiv" style="width:100%;height:55px">
                       <table>
                           <thead>
                               <tr>
                                   <td style="width:150px">
                                       报警列表
                                   </td>
                                   <td>
                                       开始时间
                                   </td>
                                   <td>
                                       结束时间
                                   </td>
                               </tr>
                           </thead>
                           <tr>
                               <td >
                                   
                                   <asp:DropDownList runat="server" width="110px">
                            </asp:DropDownList>
                               </td>
                               <td>
                                    <asp:TextBox runat="server" onFocus="WdatePicker({isShowClear:false})" ID="TB_Time" CssClass="col-sm-8">                  
                                        </asp:TextBox>
                               </td>
                               <td>
                                  
                        <asp:TextBox runat="server" onFocus="WdatePicker({isShowClear:false})" ID="TextBox1" CssClass="col-sm-8">                  
                                        </asp:TextBox>
                               </td>
                               <td>
                                    <%--<asp:Button runat="server" CssClass="btn btn-primary" ID="BT_Data" OnClick="BT_Data_Click" Text="提取数据"></asp:Button>--%>  
                                    <input type="button" runat="server" class="btn btn-primary" id="BT_Data" onclick="BT_Data_Click" text="提取数据"/> 
                               
                               </td>
                           </tr>
                       </table>
                      
                                                    
                        </div>                      
                 
                    <%--左侧，显示环境状态--%>
                    <div class="form-group" style="width: 34%; left: 15px; float: left">
                        <p class="lead">环境状态</p>                       

                        <table class="table table-stripped">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="柜外温度"></asp:Label>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_89" Text="20"></asp:Label>
                                    </td>
                                    <td>℃</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="柜内温度"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_88" Text="20"></asp:Label>
                                    </td>
                                    <td>℃</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server"  Text="载气压力"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_145" Text="20"></asp:Label>
                                    </td>
                                    <td>KPA</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server"  Text="油温"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_87" Text="20"></asp:Label>
                                    </td>
                                    <td>℃</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="色谱柱温度"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_64" Text="20"></asp:Label>
                                    </td>
                                    <td>℃</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="检测室温度"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_62" Text="20"></asp:Label>
                                    </td>
                                    <td>℃</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server"  Text="萃取温度"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_63" Text="20"></asp:Label>
                                    </td>
                                    <td>℃</td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                        

                        <%--右侧，显示运行状态--%>
                        <div class="form-group" style="width: 64%; float: right">
                            <p class="lead"> 数据</p>
                          



                            <%--<a id="DebugButton" class="btn btn-default" style="position: fixed; right: 0px; bottom: 0px">调试</a>--%>
                        <%--</div>--%>

                        
                        <table class="table table-stripped">
                            <tbody>
                                <tr>
                                    <td>
                                    <a class="twinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="H2"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_166" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                    
                                    <td>
                                    <a class="twinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="CO"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_167" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                </tr>
                                <tr>
                                    <td>
                                    <a class="twinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="CO2"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_169" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="CH4"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_168" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                </tr>
                                <tr>
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" text="C2H2"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_170" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                              
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="C2H4"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_171" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                </tr>
                                <tr>
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="C2H6"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_172" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="总烃"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_176" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                </tr>
                                <tr>
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="总可燃烧气体"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_rs" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="微水"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_175" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                </tr>
                                <tr>
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="微水温度"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_174" Text="20"></asp:Label>
                                    </td>
                                    <td>℃</td>
                                    
                                    <td>
                                    <a class="ntwinkle" href="../DevAlarm/YSP_A" target="_blank"></a>
                                        </td>
                                    <td>
                                        <asp:Label runat="server" Text="微水活性"></asp:Label>
                                         </td>
                                    <td>
                                        <asp:Label runat="server" ID="LB_173" Text="20"></asp:Label>
                                    </td>
                                    <td>ppm</td>
                                </tr>
                                </tbody>
                            </table>
                    </div>
                </div>

                   
            </div>
            </div>
       
    </form>

     <script>
         $(function () {
             $(".badge").css("background-color", "green");
             //运行状态的标题，加上站点名称和设备名称
             //$("#DebugButton").attr("href", "../DevInfoes/Debug" + "?DevID=" + <%=devId%>).attr("target", "_blank");

            $("td .form-group").css("margin", "8%").css("margin-left", "0px").css("margin-right", "0px").height(40);
            $(".col-sm-2").css("width", "40%");
            $(".col-sm-3").css("width", "60%");

            //$("#myModal input").attr("disabled", "disabled");
        })

    </script>
</body>
</html>
