<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YSP_De.aspx.cs" Inherits="WebApplication1.DevDebug.YSP_De" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Content/bootstrap-superhero.css" rel="stylesheet" />
    <link href="/Content/Site.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.10.2.js"></script>
    <script src="/Scripts/bootstrap.js"></script>
    <script src="/Scripts/meny.js"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/bootstrap-switch.js"></script>
    <script>
        $(function () {

            //开关的生成
            $("input[type='checkbox']").bootstrapSwitch();
            $("input[type='radio']").bootstrapSwitch("radioAllOff", true);

            var meny = Meny.create({
                // The element that will be animated in from off screen
                menuElement: document.querySelector('.meny'),
                contentsElement: document.querySelector('.contents'),

                // The alignment of the menu (top/right/bottom/left)
                position: 'right',

                // The height of the menu (when using top/bottom position)
                height: 300,

                // The width of the menu (when using left/right position)
                width: 500,

                // The angle at which the contents will rotate to.
                angle: 31,

                // The mouse distance from menu position which can trigger menu to open.
                threshold: 20,

                // Width(in px) of the thin line you see on screen when menu is in closed position.
                overlap: 0,

                // The total time taken by menu animation.
                transitionDuration: '0.8s',

                // Transition style for menu animations
                transitionEasing: 'ease-in-out',

                // Gradient overlay for the contents
                gradient: 'rgba(0,0,0,0.20) 0%, rgba(0,0,0,0.65) 100%)',

                // Use mouse movement to automatically open/close
                mouse: false,

                // Use touch swipe events to open/close
                touch: true
            });
            //$(".meny").height($(".contents").height());
            //控制打开和关闭菜单的按钮动作
            $("#openM").click(function () { meny.open(); $("#devParam").height(window.innerHeight * 0.98); });
            $("#closeM").click(function () { meny.close(); });

            //外观控制
            $(".col-sm-2").width("100%");
            $(".col-sm-3").width("100%");
            $("#HJWWcoll tr").height(93);
            $(".meny input[type!=submit]").attr("class", "form-control");
            $(".meny input[type!=submit]").width(55).height(5);


            //$(".meny input").width("90px").height("23px");
            //$("input[type='text']").width(55).height(5);
            $("select").attr("class", "form-control").width(55).height(5);
            $("select").css("padding", "2px").css("font-size", "10px");
            $("#Form1").width($("body")[0].clientWidth);
            //$("#currentState").height($("#Form1").height()* 0.48);

            $("#fixParamModal a").text("设置").click(function () { $("#kModal").modal('show'); });
            $("#calParam table a").text("设置").click(function () { $("#kModal").modal('show'); });

            $('.modal-body input').attr("class", "form-control");
            $(".modal-body input[type='text']").css("height", "28px");
            $("#fixParamModal input[type='text']").css("width", "66px");
            $("#kModal input[type='text']").attr("class", "form-control").width(55).height(5);;
            //$(".col-sm-3").width("20%");
            //$("#currentState").height(window.innerHeight*0.3).width(window.innerWidth*0.7);



            $(".form-control").css("padding", "0");

            $("#ShowDiag").click(function () {
                if ("<%=Session["DataID_List"]%>" == "")
                    alert("请先选择数据！");
            });

            $("table[title='zktq']").css("display", "table");
            $("#DD_152").change(function () {
                if (this.value == 0) {
                    $("table[title='zktq']").css("display", "table");
                    $("table[title='motq']").css("display", "none");
                    $("table[title='dktq']").css("display", "none");
                }
                else if (this.value == 1) {
                    $("table[title='zktq']").css("display", "none");
                    $("table[title='motq']").css("display", "table");
                    $("table[title='dktq']").css("display", "none");
                }
                else if (this.value == 2) {
                    $("table[title='zktq']").css("display", "none");
                    $("table[title='motq']").css("display", "none");
                    $("table[title='dktq']").css("display", "table");
                }
            })



        })
    </script>
</head>
<body>
    <form runat="server" id="Form1" style="position: absolute; top: 3px">
        <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <h2><%=ViewState["DevSite"]%> > <%=ViewState["DevType"]%> > <%=ViewState["DevName"]%></h2>
        <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
        <%--整体页面--%>
        <div class="meny-right">
            <%--各种设置参数--%>
            <div class="meny" style="height: 95%; width: 32%">
                <div style="position: absolute; left: 0px; top: 0px">
                    <a class="btn btn-primary" id="closeM">..>>>>>>>></a>
                </div>
                <%-- 参数设置 --%>
                <div id="devParam">

                    <div class="panel panel-default" style="height: 90%; overflow: scroll; width: 99%;">
                        <div class="panel-heading">
                            <h3 class="panel-title"></h3>
                        </div>
                        <div class="panel-body">

                            <%-- 标签页标题 --%>
                            <div id="paramTab">

                                <ul id="myTab" class="nav nav-pills">
                                    <li class="active"><a href="#sysSet" data-toggle="tab">系统设置</a></li>
                                    <li><a href="#calParam" tabindex="-1" data-toggle="tab">计算参数</a></li>
                                    <li><a href="#ctrlParam" tabindex="-1" data-toggle="tab">控制参数</a></li>
                                    <li><a href="#stateCtrl" tabindex="-1" data-toggle="tab">状态/控制</a></li>

                                </ul>

                            </div>

                            <%-- 标签页内容 --%>
                            <div id="paramContent" class="tab-content">

                                <%-- 系统设置  表格--%>
                                <div class="tab-pane fade active in" id="sysSet">

                                    <asp:UpdatePanel runat="server" ID="UP_SysSet">
                                        <ContentTemplate>

                                            <table class="table table-striped table-hover" style="font-size: 10px">


                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <p>支持CO2</p>
                                                            <asp:DropDownList runat="server" ID="DD_150">
                                                                <asp:ListItem Value="1">是</asp:ListItem>
                                                                <asp:ListItem Value="0">否</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <p>支持H2O</p>
                                                            <asp:DropDownList runat="server" ID="DD_151">
                                                                <asp:ListItem Value="1">是</asp:ListItem>
                                                                <asp:ListItem Value="0">否</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <p>脱气方式</p>
                                                            <asp:DropDownList runat="server" ID="DD_152">
                                                                <asp:ListItem Value="0">真空脱气</asp:ListItem>
                                                                <asp:ListItem Value="1">膜脱气</asp:ListItem>
                                                                <asp:ListItem Value="2">顶空脱气</asp:ListItem>
                                                            </asp:DropDownList></td>


                                                        <td>
                                                            <p>软件版本</p>
                                                            <asp:TextBox runat="server" ID="TB_149">35</asp:TextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td class="RightPadding">
                                                            <p lang="zh-cn">
                                                                <%=PressLevel%>
                                                            </p>

                                                            <asp:TextBox ID="TB_182" runat="server"></asp:TextBox>
                                                        </td>

                                                        <td>
                                                            <p><%=Altitude%></p>
                                                            <asp:TextBox ID="TB_184" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="RightPadding">
                                                            <p lang="zh-cn">
                                                                <%=A %>
                                                            </p>

                                                            <asp:TextBox ID="TB_183_1" runat="server"></asp:TextBox>
                                                        </td>

                                                        <td>
                                                            <p lang="zh-cn">
                                                                <%=B %>
                                                            </p>

                                                            <asp:TextBox ID="TB_183_2" runat="server"></asp:TextBox>
                                                        </td>

                                                        <td>
                                                            <p lang="zh-cn">
                                                                <%=OilDenisity%>
                                                            </p>

                                                            <asp:TextBox ID="TB_180" runat="server"></asp:TextBox>
                                                        </td>

                                                        <td>
                                                            <p><%=OilTotal%></p>

                                                            <asp:TextBox ID="TB_181" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button runat="server" ID="SetTime" class="btn btn-default" Text="手动对时" OnClick="SetTime_Click"></asp:Button></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="ReadSys" class="btn btn-default" Text="读取" OnClick="ReadSys_Click"></asp:Button></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="SetSys" class="btn btn-danger" Text="设置" OnClick="SetSys_Click"></asp:Button></td>

                                                        <td></td>
                                                    </tr>

                                                </tbody>
                                            </table>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>





                                <%-- 计算参数 xml --%>
                                <div class="tab-pane fade" id="calParam">
                                    <%-- <asp:FormView runat="server" DefaultMode="ReadOnly"
                                RenderOuterTable="false" ID="calParaForm" ItemType="IMserver.Models.GasFixPara" SelectMethod="GasFixPara_GetItem">
                                <EmptyDataTemplate>
                                    <p>无数据！</p>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <fieldset class="form-horizontal">
                                        <table class="table table-striped table-hover ">
                                            <tbody>

                                                <tr class="active">
                                                    <td>设备选择<asp:DropDownList ID="DevDrop" runat="server"></asp:DropDownList></td>
                                                    <td>气体选择<asp:DropDownList ID="GasDrop" runat="server"></asp:DropDownList></td>
                                                </tr>
                                                <tr class="active">
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="LeftStart" Mode="Edit" />
                                                    </td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="RightStart" Mode="Edit" />
                                                    </td>
                                                </tr>
                                                <tr class="active">
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="LeftTmin" Mode="Edit" />
                                                    </td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="LeftTMax" Mode="Edit" />
                                                    </td>
                                                </tr>
                                                <tr class="active">
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="RightTMin" Mode="Edit" />
                                                    </td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="RightTMax" Mode="Edit" />
                                                    </td>
                                                </tr>
                                                <tr class="active">
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="PeakWidth" Mode="Edit" />
                                                    </td>
                                                    <td>
                                                        <asp:DynamicControl runat="server" DataField="MultiK" Mode="Edit" />
                                                    </td>
                                                </tr>
                                                <tr class="active">
                                                    <td>
                                                        <p class="btn btn-default" data-toggle="modal" data-target="#kModal">K值设定</p>
                                                    </td>
                                                    <td>
                                                        <p class="btn btn-default" data-toggle="modal" data-target="#jModal">J值设定</p>
                                                    </td>
                                                </tr>

                                            </tbody>

                                        </table>
                                    </fieldset>
                                </ItemTemplate>
                            </asp:FormView>--%>
                                    <%-- <table class="table table-striped table-hover ">
                                        <tbody>
                                            
                                           
                                            <tr class="active">
                                                <td>修正参数</td>
                                                <td>
                                                    <p class="btn btn-info" data-toggle="modal" data-target="#fixParamModal">修改</p>
                                                </td>
                                            </tr>
                                            </tbody>
                                        </table>--%>
                                    <table class="table table-stripped table-hover">
                                        <thead>
                                            <tr>
                                                <th style="width: 70px">
                                                    <%= Gas %></th>
                                                <th style="width: 70px">
                                                    <%= High%></th>
                                                <th style="width: 70px">
                                                    <%= Left%></th>
                                                <th style="width: 70px">
                                                    <%= Right%></th>
                                                <th style="width: 70px">
                                                    <%= LeftMin%></th>
                                                <th style="width: 70px">
                                                    <%= LeftMax%></th>
                                                <th style="width: 70px">
                                                    <%= RightMin%></th>
                                                <th style="width: 70px">
                                                    <%= RightMax%></th>
                                                <th style="width: 70px">
                                                    <%= TopWidth%></th>
                                                <th style="width: 70px">
                                                    <%= K%></th>
                                            </tr>
                                        </thead>
                                        <tr>
                                            <td>

                                                <span lang="zh-cn">H2</span></td>
                                            <td>
                                                <asp:TextBox ID="TextBox1" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBox8" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox15" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox36" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox43" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox50" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox57" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox64" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <a id="LB_K_CO"></a>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span lang="zh-cn">CO</span>                    </td>
                                            <td>
                                                <asp:TextBox ID="TextBox2" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBox9" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox16" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox37" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox44" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox51" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox58" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox65" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <a id="LB_K_CO0"></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span lang="zh-cn">CH4</span></td>
                                            <td>
                                                <asp:TextBox ID="TextBox3" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBox10" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox17" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox38" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox45" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox52" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox59" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox66" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <a id="LB_K_CO1"></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span lang="zh-cn">C2H2</span></td>
                                            <td>
                                                <asp:TextBox ID="TextBox4" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBox11" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox18" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox39" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox46" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox53" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox60" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox67" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <a id="LB_K_CO2"></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span lang="zh-cn">C2H4</span></td>
                                            <td>
                                                <asp:TextBox ID="TextBox5" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBox12" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox19" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox40" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox47" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox54" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox61" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox68" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <a id="LB_K_CO3"></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span lang="zh-cn">C2H6</span></td>
                                            <td>
                                                <asp:TextBox ID="TextBox6" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBox13" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox20" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox41" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox48" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox55" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox62" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox69" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <a id="LB_K_CO4"></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span lang="zh-cn">CO2</span></td>
                                            <td>
                                                <asp:TextBox ID="TextBox14" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBox21" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox22" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox42" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox49" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox56" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox63" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox70" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <a id="LB_K_CO5"></a>
                                            </td>
                                        </tr>

                                    </table>

                                    <div class="col-md-3"><%=Erase%></div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="tb_EraseRangeStart" runat="server" Width="70px"></asp:TextBox>
                                    </div>

                                    <div class="col-md-1"><%=To%> </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="tb_EraseRangeEnd" runat="server" Width="70px"></asp:TextBox>
                                    </div>


                                    <table style="width: 100%;">
                                        <tr style="height: 35px">
                                            <td>
                                                <%= Text_Var_H2oVarName%></td>
                                            <td>
                                                <%= Text_Var_ParamA%></td>
                                            <td>
                                                <%= Text_Var_ParamK%></td>
                                            <td>
                                                <%= Text_Var_ParamB%></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span lang="zh-cn">AW</span></td>
                                            <td>
                                                <asp:TextBox ID="TB_AW_A" runat="server" Width="50px"
                                                    BackColor="#CCCCCC" Enabled="False" EnableTheming="True">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_AW_K" runat="server" Width="50px"
                                                    BackColor="#CCCCCC" Enabled="False">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_AW_B" runat="server" Width="50px">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span lang="zh-cn">T</span></td>
                                            <td>
                                                <asp:TextBox ID="TB_T_A" runat="server" Width="50px"
                                                    BackColor="#CCCCCC" Enabled="False">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_T_K" runat="server" Width="50px"
                                                    BackColor="#CCCCCC" Enabled="False">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_T_B" runat="server" Width="50px">0</asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>




                                    <br />
                                    <div class="col-md-3"></div>
                                    <div class="col-md-3"><a class="btn btn-default">读取</a></div>
                                    <div class="col-md-3"><a class="btn btn-danger">设置</a></div>

                                </div>


                                <%-- 控制参数--%>
                                <div class="tab-pane fade" id="ctrlParam">
                                    <asp:UpdatePanel runat="server" ID="UP_CtrlParam">
                                        <ContentTemplate>
                                            <%--脱气装置--%>
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#ctrlParam" href="#CP_TQcoll">脱气装置</a>
                                                    </h4>
                                                </div>
                                                <div id="CP_TQcoll" class="panel-collapse collapse">
                                                    <div class="panel-body">

                                                        <table class="table table-striped table-hover " title="zktq">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <p>真空脱气装置</p>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>

                                                                </tr>
                                                                <tr>
                                                                    <td>循环时间（分钟）<asp:TextBox runat="server" ID="TB_4"></asp:TextBox></td>
                                                                    <td>抽空次数（次）<asp:TextBox runat="server" ID="TB_5"></asp:TextBox></td>
                                                                    <td>清洗次数（次）<asp:TextBox runat="server" ID="TB_6"></asp:TextBox></td>
                                                                    <td>脱气次数（次）<asp:TextBox runat="server" ID="TB_7"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>

                                                                    <td>置换次数（次）</td>
                                                                    <td>预计脱气完成时间(秒）</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_8"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_9"></asp:TextBox></td>

                                                                </tr>
                                                            </tbody>
                                                        </table>


                                                        <table class="table table-striped table-hover " title="motq" style="display: none">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <p>膜脱气装置</p>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>油泵连续工作时间（分钟）</td>
                                                                    <td>排气阀清洗时间（秒）</td>
                                                                    <td>排气阀连续工作时间（秒）</td>
                                                                    <td>排气阀间隔停止时间（秒）</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_36"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_37"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_39"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_41"></asp:TextBox></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>气泵清洗时间（秒）</td>
                                                                    <td>气泵连续工作时间（秒）</td>
                                                                    <td>气泵间隔停止时间（秒）</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_38"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_40"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_42"></asp:TextBox></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table class="table table-striped table-hover " title="dktq" style="display: none">
                                                            <tbody>

                                                                <tr>
                                                                    <td>顶空脱气装置
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>搅拌开始时刻（秒）</td>

                                                                    <td>搅拌工作时间（秒）</td>

                                                                    <td>清洗泵开始工作时刻（秒）</td>
                                                                    <td>清洗泵工作时间（秒）</td>


                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_51"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_52"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_53"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_54"></asp:TextBox></td>
                                                                </tr>


                                                                <tr>
                                                                    <td>置换阀开始工作时刻（秒）</td>

                                                                    <td>置换阀工作时间（秒）</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_55"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_56"></asp:TextBox>

                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--检测辅助控制--%>
                                            <div class="panel panel-success">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#ctrlParam" href="#CP_JCFZcoll">检测辅助控制</a>
                                                    </h4>
                                                </div>
                                                <div id="CP_JCFZcoll" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <table class="table table-striped table-hover ">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <p>传感器室</p>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td>传感器室制冷开始时刻（秒）</td>
                                                                    <td>传感器室连续工作时间（秒）</td>
                                                                    <td>传感器室温度设置值（℃）</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_78"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_79"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_80"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>


                                                                    <td>传感器室温度设置P值</td>
                                                                    <td>传感器室温度设置I值</td>

                                                                    <td>传感器室温度设置D值</td>

                                                                    <td>传感器室温度控制PID范围</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_81"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_82"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_83"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_84"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>冷井</p>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td>冷井启动开始时刻（秒）</td>

                                                                    <td>冷井连续工作时间（秒）</td>
                                                                    <td>冷井温度设置值（℃）</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_66"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_67"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_68"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>


                                                                    <td>冷井温度设置P值</td>

                                                                    <td>冷井温度设置I值</td>

                                                                    <td>冷井温度设置D值</td>

                                                                    <td>冷井温度控制PID范围</td>

                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_69"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_70"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_71"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_72"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>色谱柱</p>
                                                                    </td>
                                                                </tr>

                                                                <tr>

                                                                    <td>色谱柱温度设置值（℃）</td>
                                                                </tr>
                                                                <tr>


                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_73"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>

                                                                    <td>色谱柱温度设置P值</td>

                                                                    <td>色谱柱温度设置I值</td>

                                                                    <td>色谱柱温度设置D值</td>

                                                                    <td>色谱柱温度控制PID范围</td>

                                                                </tr>


                                                                <tr>

                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_74"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_75"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_76"></asp:TextBox></td>

                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_77"></asp:TextBox></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>

                                            <%--环境及外围控制检测--%>
                                            <div class="panel panel-danger">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#ctrlParam" href="#CP_HJWWcoll">环境及外围控制检测</a>
                                                    </h4>
                                                </div>
                                                <div id="CP_HJWWcoll" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <table class="table table-striped table-hover ">
                                                            <tbody>
                                                                <tr>
                                                                    <td>风扇持续工作时间（秒）</td>

                                                                    <td>风扇停止时间（秒）</td>

                                                                    <td>柜体空调采样前开启时刻（秒）</td>

                                                                    <td>柜体空调连续工作时间（秒）</td>


                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_97"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_98"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_99"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_100"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>

                                                                    <td>伴热带采样前开始时刻（秒）</td>

                                                                    <td>伴热带采样前工作时间（秒）</td>

                                                                    <td>载气发生器排水阀启动时刻（秒）</td>

                                                                    <td>载气发生器排水阀工作时间（秒）</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_101"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_102"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_103"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_104"></asp:TextBox></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--采样设置--%>
                                            <div class="panel panel-warning">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#ctrlParam" href="#CP_CYKZcoll">采样控制</a>
                                                    </h4>
                                                </div>
                                                <div id="CP_CYKZcoll" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <table class="table table-striped table-hover ">
                                                            <tbody>
                                                                <tr>
                                                                    <td>标定次数（次）</td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_131"></asp:TextBox></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>采样前吹扫阀工作时间（秒）</td>

                                                                    <td>定量阀工作时间（秒）</td>

                                                                    <td>定量阀打开后，吹扫阀延迟打开的时间（秒）</td>

                                                                    <td>采样结束吹扫阀工作时间（秒）</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_107"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_108"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_109"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_110"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>六组分传感器</p>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td>六组分采样间隔（毫秒）</td>

                                                                    <td>六组分采样点数（点）</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_123"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_124"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>六组分传感器恢复阀采样前开始时刻（秒）</td>

                                                                    <td>六组分传感器恢复阀采样前工作时间（秒）</td>

                                                                    <td>六组分传感器恢复阀采样后开始时刻（秒）</td>

                                                                    <td>六组分传感器恢复阀采样后工作时间（秒）</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_111"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_112"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_113"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_114"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>六组分传感器加热开始时刻（秒）</td>

                                                                    <td>六组分传感器加热工作时间（秒）</td>

                                                                    <td>采样结束六组分传感器加热工作时间（秒）</td>


                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_120"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_121"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_122"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>CO2传感器</p>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>CO2采样间隔（毫秒）</td>

                                                                    <td>CO2采样点数（点）</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_127"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_128"></asp:TextBox></td>
                                                                </tr>

                                                                <tr>
                                                                    <td>CO2传感器加热开始时刻（秒）</td>

                                                                    <td>CO2传感器工作时间（秒）</td>



                                                                    <td>CO2气路切换开始时刻(秒)</td>

                                                                    <td>CO2气路工作时间（秒）</td>


                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_125"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_126"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_129"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_130"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>微水传感器</p>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td>微水传感器采样间隔（毫秒)</td>

                                                                    <td>微水传感器AW的采样点数(点）</td>

                                                                    <td>微水传感器T的采样点数（点）</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_117"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_118"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_119"></asp:TextBox></td>

                                                                </tr>
                                                                <tr>
                                                                    <td>微水传感器延时开始加热时刻（秒）</td>

                                                                    <td>微水传感器采样开始时间（秒）</td>



                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_115"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="TB_116"></asp:TextBox></td>
                                                                </tr>

                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3"></div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" ID="ReadCtrl" class="btn btn-default" Text="读取" OnClick="ReadCtrl_Click"></asp:Button></div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" ID="SetCtrl" class="btn btn-danger" Text="设置" OnClick="SetCtrl_Click"></asp:Button></div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>



                                </div>


                                <%-- 状态/控制 --%>
                                <div class="tab-pane fade" id="stateCtrl">
                                    <asp:UpdatePanel runat="server" ID="UP_StateCtrl">
                                        <ContentTemplate>

                                            <%--脱气装置--%>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#stateCtrl" href="#TQcoll">脱气装置</a>
                                                    </h4>
                                                </div>
                                                <div id="TQcoll" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <table class="table table-responsive" title="zktq">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <p>真空脱气装置</p>
                                                                    </td>

                                                                </tr>

                                                                <tr>


                                                                    <td>油压检测值(KPA)<asp:Label runat="server" ID="LB_13"></asp:Label></td>
                                                                    <td>油杯液位状态<asp:Label runat="server" ID="LB_14"></asp:Label></td>

                                                                    <td>油缸进到位<asp:Label runat="server" ID="LB_18"></asp:Label></td>
                                                                    <td>油缸退到位<asp:Label runat="server" ID="LB_19"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>气泵气压检测值(KPA)<asp:Label runat="server" ID="LB_12"></asp:Label></td>
                                                                    <td>气杯液位状态<asp:Label runat="server" ID="LB_15"></asp:Label></td>
                                                                    <td>气缸进到位<asp:Label runat="server" ID="LB_16"></asp:Label></td>
                                                                    <td>气缸退到位<asp:Label runat="server" ID="LB_17"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>真空度压力(KPA)</p>
                                                                        <asp:Label runat="server" ID="LB_11">60</asp:Label></td>
                                                                    <td>
                                                                        <p>油泵</p>

                                                                        <asp:CheckBox runat="server" ID="SW_21" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>油泵转速控制(VDC)<asp:TextBox runat="server" ID="TB_22"></asp:TextBox></td>
                                                                    <td>
                                                                        <p>进出油阀</p>
                                                                        <asp:CheckBox runat="server" ID="SW_23" /></td>
                                                                    <td>
                                                                        <p>YV10阀</p>
                                                                        <asp:CheckBox runat="server" ID="SW_24" /></td>
                                                                    <td>
                                                                        <p>YV11阀</p>
                                                                        <asp:CheckBox runat="server" ID="SW_25" /></td>
                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <p>YV12阀</p>
                                                                        <asp:CheckBox runat="server" ID="SW_26" /></td>

                                                                    <td>
                                                                        <p>YV14阀</p>
                                                                        <asp:CheckBox runat="server" ID="SW_27" /></td>
                                                                    <td>
                                                                        <p>YV15阀</p>
                                                                        <asp:CheckBox runat="server" ID="SW_28" /></td>

                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>气泵</p>
                                                                        <asp:CheckBox runat="server" ID="SW_33" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>气缸YV4阀<asp:CheckBox runat="server" ID="SW_29" /></td>
                                                                    <td>气缸YV5阀<asp:CheckBox runat="server" ID="SW_30" /></td>
                                                                    <td>气缸YV6阀<asp:CheckBox runat="server" ID="SW_31" /></td>
                                                                    <td>气缸YV7阀<asp:CheckBox runat="server" ID="SW_32" /></td>

                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table class="table table-striped table-hover " title="motq" style="display: none">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <h4>膜脱气装置无状态/控制量！</h4>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table class="table table-responsive" title="dktq" style="display: none">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <p>顶空脱气装置</p>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>搅拌开关</p>

                                                                        <asp:CheckBox runat="server" ID="SW_45" /></td>

                                                                    <td>
                                                                        <p>液位A</p>
                                                                        <asp:Label runat="server" ID="LB_47"></asp:Label></td>

                                                                    <td>
                                                                        <p>液位B</p>
                                                                        <asp:Label runat="server" ID="LB_48"></asp:Label></td>

                                                                    <td>
                                                                        <p>搅拌缸检测温度（℃）</p>
                                                                        <asp:Label runat="server" ID="LB_49"></asp:Label></td>

                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>


                                            <%--检测辅助控制--%>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#stateCtrl" href="#JCFZcoll">检测辅助控制</a>
                                                    </h4>
                                                </div>
                                                <div id="JCFZcoll" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <table class="table table-striped table-hover ">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <p>冷井开关</p>
                                                                        <asp:CheckBox runat="server" ID="SW_59" /></td>


                                                                    <td>
                                                                        <p>传感室制冷</p>
                                                                        <asp:CheckBox runat="server" ID="SW_60" /></td>

                                                                </tr>
                                                                <tr>
                                                                    <td>传感器室温度实际采样值（℃）<asp:Label runat="server" ID="LB_62"></asp:Label></td>
                                                                    <td>冷井温度实际采样值（℃）<asp:Label runat="server" ID="LB_63"></asp:Label></td>
                                                                    <td>色谱柱实际采样值（℃）<asp:Label runat="server" ID="LB_64"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>


                                            <%--环境及外围控制检测--%>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#stateCtrl" href="#HJWWcoll">环境及外围控制检测</a>
                                                    </h4>
                                                </div>
                                                <div id="HJWWcoll" class="panel-collapse collapse">
                                                    <div class="panel-body">

                                                        <table class="table table-striped table-hover ">
                                                            <tbody>
                                                                <tr>
                                                                    <td>油温（℃）
                                                             
                                                                <asp:Label runat="server" ID="LB_87"></asp:Label></td>

                                                                    <td>柜内温度（℃）

                                                                <asp:Label runat="server" ID="LB_88"></asp:Label></td>

                                                                    <td>柜外温度（℃）

                                                                <asp:Label runat="server" ID="LB_89"></asp:Label></td>

                                                                    <td>伴热带温度（℃）
                                                                <asp:Label runat="server" ID="LB_96"></asp:Label></td>

                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                        <p>载气发生器气泵开关</p>
                                                                        <asp:CheckBox runat="server" ID="SW_92" /></td>

                                                                    <td>
                                                                        <p>柜体空调开关</p>
                                                                        <br />
                                                                        <asp:CheckBox runat="server" ID="SW_93" /></td>

                                                                    <td>
                                                                        <p>伴热带立即加热</p>
                                                                        <br />
                                                                        <asp:CheckBox runat="server" ID="SW_94" /></td>

                                                                    <td>
                                                                        <p>载气发生器排水阀</p>
                                                                        <asp:CheckBox runat="server" ID="SW_95" /></td>

                                                                </tr>

                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>


                                            <%--采样设置--%>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#stateCtrl" href="#CYSZcoll">采样设置</a>
                                                    </h4>
                                                </div>
                                                <div id="CYSZcoll" class="panel-collapse collapse">
                                                    <div class="panel-body">
                                                        <table class="table table-striped table-hover ">
                                                            <tbody>
                                                                <tr>

                                                                    <td>标定（立即启动）<asp:RadioButton runat="server" ID="SW_133" /></td>

                                                                    <td>采样（立即启动）<asp:RadioButton runat="server" ID="SW_134" /></td>

                                                                    <td>下次采样时间<asp:TextBox runat="server" ID="TB_135"></asp:TextBox></td>

                                                                    <td>采样间隔（分钟）<asp:TextBox runat="server" ID="TB_136"></asp:TextBox></td>


                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p>载气压力检测实际值(KPA)</p>
                                                                        <asp:Label runat="server" ID="TB_145"></asp:Label></td>
                                                                    <td>
                                                                        <p>六组分传感器手动立即启动</p>
                                                                        <asp:CheckBox runat="server" ID="SW_138" /></td>

                                                                    <td>
                                                                        <p>微水传感器加热手动立即启动</p>
                                                                        <asp:CheckBox runat="server" ID="SW_139" /></td>

                                                                    <td>
                                                                        <p>微水传感器手动立即启动</p>
                                                                        <asp:CheckBox runat="server" ID="SW_140" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>定量阀手动立即启动<asp:CheckBox runat="server" ID="SW_141" /></td>

                                                                    <td>吹扫阀手动立即启动<asp:CheckBox runat="server" ID="SW_142" /></td>

                                                                    <td>CO2切换阀手动立即启动<asp:CheckBox runat="server" ID="SW_143" /></td>

                                                                    <td>吹气阀手动立即启动<asp:CheckBox runat="server" ID="SW_144" />


                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>



                                            <br />
                                            <div class="col-md-3"></div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" ID="ReadState" class="btn btn-default" Text="读取" OnClick="ReadState_Click"></asp:Button></div>
                                            <div class="col-md-3">
                                                <asp:Button runat="server" ID="SetState" class="btn btn-danger" Text="设置" OnClick="SetState_Click"></asp:Button></div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>



            <%--运行图和谱图--%>
            <div class="contents">


                <%--打开右侧隐藏菜单--%>
                <div style="float: right; height: 20%; text-align: center">
                    <a class="btn btn-primary" id="openM"><<<<<<<<...</a>
                </div>

                <%-- 设备运行图 --%>

                
                    <div>
                <div class="col-md-2 form-group">

                    <asp:Button ID="ExecuteButton" runat="server" class="btn btn-primary" Text="立即启动" OnClick="ExecuteButton_Click" />

                    <asp:Button ID="StopButton" runat="server" class="btn btn-primary" Text="停止" OnClick="StopButton_Click" />

                </div>

                <table class="table-stripped" style="width: 70%">
                    <tr>
                        <td>设备状态切换：</td>
                        <td>
                            <asp:DropDownList ID="DD_156" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">空闲</asp:ListItem>
                                <asp:ListItem Value="1">调试</asp:ListItem>
                                <asp:ListItem Value="2">工作</asp:ListItem>

                            </asp:DropDownList></td>
                        <td colspan="10">当前工作流程：</td>
                        <td>
                            <asp:DropDownList ID="DD_157" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem Value="0">脱气</asp:ListItem>
                                <asp:ListItem Value="1">采样</asp:ListItem>
                                <asp:ListItem Value="2">定标</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        <td colspan="10">下位机时间：</td>
                        <td>
                            <asp:Label ID="LB_154" runat="server"><%:DateTime.Now%></asp:Label></td>
                    </tr>
                </table>
                        </div>
                <iframe id="Frame1" src="YSP_Gauge.aspx" height="400" width="100%"></iframe>


                
                <%--实时谱图--%>

                <div id="tempChart">

                    <%--<table class="table table-striped form-group" style="text-align: center">
                        <tbody>

                            <tr>

                                <td colspan="15">
                                    <asp:Button runat="server" Text="选择数据" CssClass="btn btn-default" OnClick="SelectGas_Click" ID="SelectGas" />
                                </td>

                                <td>
                                    <asp:Button runat="server" Text="查看谱图" CssClass="btn btn-default" ID="ShowDiag" />
                                </td>
                            </tr>
                        </tbody>

                    </table>--%>
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel1">

                        <ContentTemplate>

                            <asp:Chart ID="Chart1" runat="server" Style="margin: 0px; height: 45%; width: 100%" Height="230px" Width="1000px" BackColor="WhiteSmoke"
                                BorderlineDashStyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                                BorderlineWidth="1" BorderlineColor="26, 59, 105" EnableViewState="True">
                                <Legends>
                                    <asp:Legend Enabled="False" Name="Default" BackColor="Transparent">
                                    </asp:Legend>
                                </Legends>
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="FastLine" Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                        BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent">
                                        <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero="False" Maximum="5000" Minimum="0" IsLogarithmic="False" Interval="200" Crossing="Min" IntervalType="Number">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                        </AxisX>
                                        <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero=" False">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                        </AxisY>

                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>

                            <asp:Timer ID="Timer1" runat="server" Interval="800" OnTick="Timer1_Tick" Enabled="False">
                            </asp:Timer>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>


        </div>





        <%-- 修正参数 --%>
        <div class="modal fade" id="fixParamModal" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="True">
            <div class="modal-dialog" style="width: 800px">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close"
                            data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" id="gasModalTitle">修正参数设置</h4>
                    </div>

                    <div class="modal-body">
                        <%-- <table class="table table-stripped table-hover">
                            <thead>
                                <tr>
                                    <th style="width: 70px">
                                        <%= Gas %></th>
                                    <th style="width: 70px">
                                        <%= High%></th>
                                    <th style="width: 70px">
                                        <%= Left%></th>
                                    <th style="width: 70px">
                                        <%= Right%></th>
                                    <th style="width: 70px">
                                        <%= LeftMin%></th>
                                    <th style="width: 70px">
                                        <%= LeftMax%></th>
                                    <th style="width: 70px">
                                        <%= RightMin%></th>
                                    <th style="width: 70px">
                                        <%= RightMax%></th>
                                    <th style="width: 70px">
                                        <%= TopWidth%></th>
                                    <th style="width: 70px">
                                        <%= K%></th>
                                </tr>
                            </thead>
                            <tr>
                                <td>

                                    <span lang="zh-cn">H2</span></td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBox8" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox15" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox36" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox43" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox50" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox57" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox64" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <a id="LB_K_CO"></a>

                                </td>
                            </tr>
                            <tr>
                                <td><span lang="zh-cn">CO</span>                    </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBox9" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox16" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox37" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox44" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox51" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox58" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox65" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <a id="LB_K_CO0"></a>
                                </td>
                            </tr>
                            <tr>
                                <td><span lang="zh-cn">CH4</span></td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBox10" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox17" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox38" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox45" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox52" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox59" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox66" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <a id="LB_K_CO1"></a>
                                </td>
                            </tr>
                            <tr>
                                <td><span lang="zh-cn">C2H2</span></td>
                                <td>
                                    <asp:TextBox ID="TextBox4" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBox11" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox18" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox39" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox46" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox53" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox60" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox67" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <a id="LB_K_CO2"></a>
                                </td>
                            </tr>
                            <tr>
                                <td><span lang="zh-cn">C2H4</span></td>
                                <td>
                                    <asp:TextBox ID="TextBox5" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBox12" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox19" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox40" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox47" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox54" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox61" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox68" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <a id="LB_K_CO3"></a>
                                </td>
                            </tr>
                            <tr>
                                <td><span lang="zh-cn">C2H6</span></td>
                                <td>
                                    <asp:TextBox ID="TextBox6" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBox13" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox20" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox41" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox48" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox55" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox62" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox69" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <a id="LB_K_CO4"></a>
                                </td>
                            </tr>
                            <tr>
                                <td><span lang="zh-cn">CO2</span></td>
                                <td>
                                    <asp:TextBox ID="TextBox14" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>

                                    <asp:TextBox ID="TextBox21" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox22" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox42" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox49" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox56" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox63" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox70" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <a id="LB_K_CO5"></a>
                                </td>
                            </tr>

                        </table>

                        <div class="col-md-3"><%=Erase%></div>
                        <div class="col-md-2">
                            <asp:TextBox ID="tb_EraseRangeStart" runat="server" Width="70px"></asp:TextBox>
                        </div>

                        <div class="col-md-1"><%=To%> </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="tb_EraseRangeEnd" runat="server" Width="70px"></asp:TextBox>
                        </div>


                        <table style="width: 100%;">
                            <tr style="height: 35px">
                                <td>
                                    <%= Text_Var_H2oVarName%></td>
                                <td>
                                    <%= Text_Var_ParamA%></td>
                                <td>
                                    <%= Text_Var_ParamK%></td>
                                <td>
                                    <%= Text_Var_ParamB%></td>
                            </tr>
                            <tr>
                                <td>
                                    <span lang="zh-cn">AW</span></td>
                                <td>
                                    <asp:TextBox ID="TB_AW_A" runat="server" Width="50px"
                                        BackColor="#CCCCCC" Enabled="False" EnableTheming="True">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TB_AW_K" runat="server" Width="50px"
                                        BackColor="#CCCCCC" Enabled="False">1</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TB_AW_B" runat="server" Width="50px">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span lang="zh-cn">T</span></td>
                                <td>
                                    <asp:TextBox ID="TB_T_A" runat="server" Width="50px"
                                        BackColor="#CCCCCC" Enabled="False">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TB_T_K" runat="server" Width="50px"
                                        BackColor="#CCCCCC" Enabled="False">1</asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TB_T_B" runat="server" Width="50px">0</asp:TextBox>
                                </td>
                            </tr>
                        </table>--%>

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


        <%--K值设置窗口--%>
        <div class="modal fade" id="kModal" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="True">
            <div class="modal-dialog" style="width: 800px">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close"
                            data-dismiss="modal" aria-hidden="true">
                            &times;
                        </button>
                        <h4 class="modal-title" id="kModalTitle">K值设定 </h4>
                    </div>

                    <div class="modal-body">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:UpdatePanel ID="UpdatePanelK" runat="server">
                                <ContentTemplate>

                                    <table style="width: 758px; border-width: 1px; border-color: #cccccc" class="table table-stripped">
                                        <tr>
                                            <td style="width: 40px; height: 36px"></td>
                                            <td style="width: 120px; height: 36px">
                                                <%=kvalue%>
                                            </td>
                                            <td style="width: 120px; height: 36px">
                                                <%=mifixvalue%>
                                            </td>
                                            <td style="width: 120px; height: 36px">
                                                <%=nifixvalue%>
                                            </td>
                                            <td style="width: 120px; height: 36px">
                                                <%=min %>
                                            </td>
                                            <td style="width: 120px; height: 36px">
                                                <%=max %>
                                            </td>
                                            <td style="width: 120px; height: 36px">
                                                <%=jizhun %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K1
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K1" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI1" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI1" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K1_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K1_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI1" runat="server"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>K2
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K2" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI2" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI2" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K2_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K2_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI2" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K3
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K3" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI3" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI3" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K3_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K3_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K4
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K4" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI4" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI4" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K4_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K4_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI4" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K5
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K5" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI5" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI5" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K5_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K5_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI5" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K6
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K6" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI6" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI6" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K6_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K6_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI6" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K7
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K7" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI7" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI7" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K7_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K7_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI7" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K8
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K8" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI8" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI8" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K8_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K8_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI8" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K9
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K9" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI9" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI9" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K9_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K9_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI9" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K10
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K10" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI10" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI10" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K10_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K10_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI10" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K11
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K11" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI11" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI11" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K11_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K11_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI11" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>K12
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K12" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_MI12" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_NI12" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K12_MinArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_K12_MaxArea" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TB_JIZHI12" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>

                                                <asp:Button ID="BT_SetK" runat="server" ValidationGroup="1" Text="设置" Width="60px" UseSubmitBehavior="False" />
                                            </td>
                                            <td>

                                                <asp:Button ID="BT_Cancle" runat="server" ValidationGroup="2" Text="取消" Width="80px" />
                                            </td>
                                            <td>

                                                <asp:Button ID="CalcB" runat="server" Width="60px" Text="计算" OnClick="Button1_Click" />

                                            </td>

                                        </tr>
                                    </table>

                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </asp:Panel>
                    </div>
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
                <!-- /.modal-content -->
            </div>
            <!-- /.modal -->

        </div>



   <footer></footer>


    </form>



    <script src="../Scripts/ysp_de.js"></script>
</body>
</html>
