<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="YSP_A.aspx.cs" Inherits="WebApplication1.DevAlarm.YSP_A" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="../Scripts/bootstrap-switch.js"></script>
    <script>
        $(function () {
            $('#ErrorMsg').hide();
            $("td input[type='text']").width(60);
            $("#alarmSetModal tr").height(50);
            $("input[type='checkbox']").bootstrapSwitch();
            $("input[type='radio']").bootstrapSwitch();

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
    <h4>出错!</h4>
    <p></p>
    </div>
      
             <table style="width: 100%;">
                        <tr>
                            <td>
                                <h3><%=devSite %> > <%=devType%> > <%=devName%></h3>
                            </td>
                            <td>
                                <asp:Button ID="BT_Sel" runat="server" OnClick="BT_Sel_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BT_Abs" runat="server" OnClick="BT_Abs_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BT_Rel" runat="server" OnClick="BT_Rel_Click" />
                            </td>
                            <td>
                                <%--<asp:Button ID="BT_Analysis" runat="server" OnClick="BT_Analysis_Click" />--%>
                            </td>
                        </tr>
                    </table>
      <asp:Panel ID="Panel_Result" runat="server" Visible="False">
          <asp:GridView ID="GridView1" runat="server" Width="100%" BorderStyle="Groove" BorderColor="#99FFCC">
                </asp:GridView>
          </asp:Panel>
    <div class="form-group-sm" style="width:645px;float:left">
        <div class="col-md-3">
        <asp:DropDownList runat="server" ID="ShowDiagDrop" CssClass="form-control">
            <asp:ListItem Text="大卫三角形法"></asp:ListItem>
            <asp:ListItem Text="立体图示法"></asp:ListItem>
            <asp:ListItem Text="三比值法"></asp:ListItem>
        </asp:DropDownList>
            </div>
        <asp:Button runat="server" ID="ShowDiagButton" Text="诊断" OnClick="ShowDiagButton_Click"/>
        <asp:Label runat="server" ID="LB_Ratio"></asp:Label>
    </div>
    <a class="lead text-info">诊断结果:</a>
    <div style="float: right">
            <a class="btn btn-default" href="../DevDebug/YSP_De" + "?DevID=" + <%=devId%>">调试</a>
     </div>
  </div>
    <div class="form-group">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="ShowDiagUpdate" style="float: left">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="800"></asp:Timer>

            <asp:Image ID="Diag" runat="server" Width="625" Height="305" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="position:relative;left:20px">
    <asp:TextBox ID="TB_Result" runat="server" width="40%" Height="305" TextMode="MultiLine" CssClass="form-control" BorderStyle="None"></asp:TextBox>
    </div>
    </div>

   
    <div style="width: 33.5%; float: left;margin-right:3.5%">

        <table class="table table-striped table-hover" >
            <tr>
                <td>
                    <h4>分析及报警</h4>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>自动阈值告警</td>
                <td><asp:checkbox  runat="server" ID="SW_286"/></td>
                <td></td>
            </tr>
            <tr>
                <td>自动诊断设置</td>
                <td><asp:checkbox  runat="server" ID="SW_287" checked="true"/></td>
                <td></td>
            </tr>
            <tr>
                <td>自动告警功能启用最小日期</td>
                <td><asp:Label runat="server" ID="LB_288"></asp:Label></td>
                <td>天</td>
            </tr>
            <tr>
                <td>报警功能</td>
                <td><p class="btn btn-warning btn-sm" id="alarmSet" data-toggle="modal" data-target="#alarmSetModal">设置</p></td>
                <td></td>
            </tr>
          
        </table>

       
         <table class="table table-striped table-hover ">
        <tr>
            <td>
                <h4>采样参数</h4>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>下次采样时间</td>
            <td><asp:Label runat="server" ID="LB_135"></asp:Label></td>
            <td></td>
        </tr>
        <tr>
            <td>载气压力检测实际值</td>
            <td><asp:Label runat="server" ID="LB_145"></asp:Label></td>
            <td>KPA</td>
        </tr>
    </table>

        
    </div>


    <div style="width:30%;float:right">
    <table class="table table-striped table-hover ">
        <tr>
            <td>
                <h4>检测辅助控制</h4>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>冷井温度实际采样值</td>
            <td><asp:Label runat="server" ID="LB_63"></asp:Label></td>
            <td>℃</td>
            <td></td>
        </tr>
        <tr>
            <td>色谱柱温度实际采样值</td>
            <td><asp:Label runat="server" ID="LB_64"></asp:Label></td>
            <td>℃</td>
        </tr>
        <tr>
            <td>传感器温度实际采样值</td>
            <td><asp:Label runat="server" ID="LB_62" ></asp:Label></td>
            <td>℃</td>
        </tr>

    </table>

    <table class="table table-striped table-hover ">
        <tr>
            <td>
                <h4>工作参数</h4>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>采样开始时间</td>
            <td>300</td>
            <td>毫秒</td>
        </tr>
        <tr>
            <td>采样间隔时间</td>
            <td><asp:Label runat="server" ID="LB_136" ></asp:Label></td>
            <td>毫秒</td>
        </tr>
        <tr>
            <td>下位机时间</td>
            <td><asp:Label runat="server" ID="LB_154" ></asp:Label></td>
            <td></td>
        </tr>

    </table>
       </div>
   
     <table class="table table-striped table-hover " style="width:30%;margin-bottom:3.5%">
            <tr>
                <td>
                    <h4>脱气机工作状态</h4>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>真空度压力检测值</td>
                <td><asp:Label runat="server" ID="LB_11" ></asp:Label></td>
                <td>KPA</td>
            </tr>
            <tr>
                <td>油压检测值</td>
                <td><asp:Label runat="server" ID="LB_13" ></asp:Label></td>
                <td>KPA</td>
            </tr>
            <tr>
                <td>油杯液位状态</td>
                <td><asp:Label runat="server" ID="LB_14" ></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>气杯液位状态</td>
                <td><asp:Label runat="server" ID="LB_15" ></asp:Label></td>
                <td></td>
            </tr>

            <tr>
                <td>气缸进到位</td>
                <td><asp:Label runat="server" ID="LB_16" ></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>气缸退到位</td>
                <td><asp:Label runat="server" ID="LB_17" ></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>油缸进到位</td>
                <td><asp:Label runat="server" ID="LB_18" ></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td>油缸退到位</td>
                <td><asp:Label runat="server" ID="LB_19" ></asp:Label></td>
                <td></td>
            </tr>
          <tr>
                <td>脱气次数</td>
                <td><asp:Label runat="server" ID="LB_7" ></asp:Label></td>
                <td>次</td>
            </tr>
         <tr>
                <td>置换次数</td>
                <td><asp:Label runat="server" ID="LB_8" ></asp:Label></td>
             <td>次</td>
            </tr>


        </table>
       


      <div class="modal fade" id="alarmSetModal" tabindex="-1" role="dialog"
            aria-labelledby="gasModalTitle" aria-hidden="True">
            <div class="modal-dialog" style="width: 800px">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close"
                            data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="gasModalTitle">报警阈值设置</h4>
                    </div>

                    <div class="modal-body">
                       
                       
                <table style="width:100%">
                    <tr>
                        <td colspan="18" style="text-align:center">
                            <span style="font-weight: bold; text-decoration: underline;">
                                <%= GasAttention%></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Gas %>
                        </td>
                        <td>
                            <%=H2%>
                        </td>
                        <td>
                            <%=CO%>
                        </td>
                        <td>
                            <%=CH4%>
                        </td>
                        <td>
                            <%=C2H2%>
                        </td>
                        <td>
                            <%=C2H4%>
                        </td>
                        <td>
                            <%=C2H6%>
                        </td>
                        <td>
                            <%=CO2%>
                        </td>
                        <td>
                            <%=ZongT%>
                        </td>
                        <td>
                            <%=PPM%>
                        </td>
                        <td>
                            <%=AW%>
                        </td>
                        <td>
                            <%=T%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Level1 %>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_H2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_CO_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_CH4_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_C2H2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_C2H4_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_C2H6_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_CO2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_Total_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_PPM_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_AW_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_T_1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Level2 %>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_H2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_CO_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_CH4_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_C2H2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_C2H4_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_C2H6_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_CO2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_Total_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_PPM_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_AW_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_T_2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                        <table style="width:100%">
                    <tr>
                        <td colspan="18" style="text-align:center">
                            <span style="font-weight: bold; text-decoration: underline;">
                                <%=AbsAttention %></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Gas %>
                        </td>
                        <td>
                            <%=H2%>
                        </td>
                        <td>
                            <%=CO%>
                        </td>
                        <td>
                            <%=CH4%>
                        </td>
                        <td>
                            <%=C2H2%>
                        </td>
                        <td>
                            <%=C2H4%>
                        </td>
                        <td>
                            <%=C2H6%>
                        </td>
                        <td>
                            <%=CO2%>
                        </td>
                        <td>
                            <%=ZongT%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Level1 %>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_H2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_CO_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_CH4_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_C2H2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_C2H4_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_C2H6_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_CO2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_Total_1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Level2 %>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_H2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_CO_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_CH4_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_C2H2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_C2H4_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_C2H6_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_CO2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ABS_Total_2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="18" style="text-align:center">
                            <span style="font-weight: bold; text-decoration: underline;">
                                <%= RelAttention%></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Gas %>
                        </td>
                        <td>
                            <%=H2%>
                        </td>
                        <td>
                            <%=CO%>
                        </td>
                        <td>
                            <%=CH4%>
                        </td>
                        <td>
                            <%=C2H2%>
                        </td>
                        <td>
                            <%=C2H4%>
                        </td>
                        <td>
                            <%=C2H6%>
                        </td>
                        <td>
                            <%=CO2%>
                        </td>
                        <td>
                            <%=ZongT%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Level1 %>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_H2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_CO_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_CH4_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_C2H2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_C2H4_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_C2H6_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_CO2_1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_Total_1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Level2 %>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_H2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_CO_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_CH4_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_C2H2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_C2H4_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_C2H6_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_CO2_2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_REL_Total_2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="18" style="text-align:center">
                            
                            <%--<asp:Button ID="BT_Set" runat="server" OnClick="BT_Set_Click" />
                            <asp:Button ID="BT_Close" runat="server" OnClientClick="window.top.hidePopWin();return false;" />
                            <asp:Button ID="BT_Default" runat="server" OnClick="BT_Default_Click" />--%>
                            <a ID="BT_Set" class="btn btn-default">设置 </a>
                            <a ID="BT_Default" class="btn btn-default">还原默认值</a>
                            
                        </td>
                    </tr>
                </table>
           
                       
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default"
                                data-dismiss="modal">
                                关闭
                            </button>
                            <button type="button" class="btn btn-primary">
                                提交更改
                            </button>
                        </div>
                    </div>
                    </div>
                </div>
          </div>
  
</asp:Content>


