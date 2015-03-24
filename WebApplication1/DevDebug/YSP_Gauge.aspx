<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YSP_Gauge.aspx.cs" Inherits="WebApplication1.DevDebug.YSP_Gauge" EnableSessionState="false"%>
<%@ Register Assembly="DundasWebGauge" Namespace="Dundas.Gauges.WebControl" TagPrefix="DGWC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <script src="../Scripts/jquery-1.10.2.js"></script>
    <script src="/Scripts/bootstrap.js"></script>
    <script src="../Scripts/bootstrap-switch.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="devLayer">
            <%--<div style="position: absolute; top: 10%; left: 3%; width: 130px; text-align: center">--%>
            <%--<div id="GC_12" style="position: absolute;width:180px;height:180px;visibility:hidden"></div>
            <div id="GC_11" style="position: absolute;width:180px;height:180px;visibility:hidden"></div>--%>
                        
                       <%-- </div>--%>
        </div>
        <canvas id="myPic" style="z-index:-3;position:absolute;top:0;left:0"></canvas>
        <div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" UpdateMode="Always">
              <%--  <ContentTemplate>

                    <div style="position: absolute; top: 10%; left: 2%; text-align: center; border: ridge" class="twinc">
                        <div>
                            <DGWC:GaugeContainer ID="GC_12" runat="server" BackColor="Black" Height="66px" Width="71px">
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="30">
                                    </DGWC:InputValue>
                                </Values>
                                <CircularGauges>
                                    <DGWC:CircularGauge Name="Default">
                                        <Scales>
                                            <DGWC:CircularScale BorderWidth="1" FillColor="Transparent" FillGradientEndColor="Black" FillGradientType="TopBottom" Name="Default" Width="0">
                                                <LabelStyle TextColor="White" />
                                                <MajorTickMark Shape="Rectangle" Width="4" />
                                            </DGWC:CircularScale>
                                        </Scales>
                                        <Ranges>
                                            <DGWC:CircularRange FillColor="Red" FillGradientType="None" Name="Range1" />
                                        </Ranges>
                                        <Pointers>
                                            <DGWC:CircularPointer CapWidth="30" FillColor="Red" FillGradientEndColor="White" Name="Default" NeedleStyle="NeedleStyle11" ValueSource="Default" />

                                        </Pointers>
                                        <PivotPoint X="50" Y="50" />
                                        <Location X="0" Y="0" />
                                        <Size Height="100" Width="100" />
                                        <BackFrame BackColor="Black" BackGradientEndColor="126, 126, 126" BackGradientType="TopBottom" FrameColor="Gray" FrameGradientEndColor="Black" FrameShape="CustomCircular2" FrameStyle="Edged" FrameWidth="10" />
                                    </DGWC:CircularGauge>
                                </CircularGauges>
                                <Labels>
                                    <DGWC:GaugeLabel BackColor="Gray" BackGradientEndColor="" Name="Label1" Parent="CircularGauges.Default" Text="KPA" TextAlignment="BottomCenter">
                                        <Location X="28" Y="28" />
                                        <Size Height="14" Width="45" />
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <BackFrame FrameStyle="Simple" BackColor="Black" BackGradientEndColor="126, 126, 126" BackGradientType="TopBottom" FrameColor="Gray" FrameGradientEndColor="Black" FrameShape="CustomCircular10" FrameWidth="5" />
                            </DGWC:GaugeContainer>
                        </div>
                        <div>
                            <p>气泵压力检测值：</p>
                            <asp:Label runat="server" ID="LB_21"></asp:Label>
                            <span>KPA</span>
                        </div>
                    </div>
                    <div style="position: absolute; top: 10%; left: 20%; border: ridge" class="twinc">
                        <asp:CheckBox runat="server" Text="载气发生器气泵" ID="SW_92" />
                    </div>
                    <div style="position: absolute; text-align: center; top: 10%; left: 62%; border: ridge" class="twinc">
                        <div>
                            <DGWC:GaugeContainer ID="GC_145" runat="server" BackColor="Black" Height="66px" Width="71px" ImageType="Bmp">
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="30">
                                    </DGWC:InputValue>
                                </Values>
                                <CircularGauges>
                                    <DGWC:CircularGauge Name="Default">
                                        <Scales>
                                            <DGWC:CircularScale BorderWidth="1" FillColor="Transparent" FillGradientEndColor="Black" FillGradientType="TopBottom" Name="Default" Width="0">
                                                <LabelStyle TextColor="White" />
                                                <MajorTickMark Shape="Rectangle" Width="4" />
                                            </DGWC:CircularScale>
                                        </Scales>
                                        <Ranges>
                                            <DGWC:CircularRange FillColor="Red" FillGradientType="None" Name="Range1" />
                                        </Ranges>
                                        <Pointers>
                                            <DGWC:CircularPointer CapWidth="30" FillColor="Red" FillGradientEndColor="White" Name="Default" NeedleStyle="NeedleStyle11" ValueSource="Default" />

                                        </Pointers>
                                        <PivotPoint X="50" Y="50" />
                                        <Location X="0" Y="0" />
                                        <Size Height="100" Width="100" />
                                        <BackFrame BackColor="Black" BackGradientEndColor="126, 126, 126" BackGradientType="TopBottom" FrameColor="Gray" FrameGradientEndColor="Black" FrameShape="CustomCircular3" FrameStyle="Edged" FrameWidth="10" />
                                    </DGWC:CircularGauge>
                                </CircularGauges>
                                <Labels>
                                    <DGWC:GaugeLabel BackColor="Gray" BackGradientEndColor="" Name="Label1" Parent="CircularGauges.Default" Text="KPA" TextAlignment="BottomCenter">
                                        <Location X="28" Y="28" />
                                        <Size Height="14" Width="45" />
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <BackFrame FrameStyle="None" BackColor="Black" BackGradientEndColor="126, 126, 126" BackGradientType="TopBottom" FrameColor="Gray" FrameGradientEndColor="Black" FrameShape="Rectangular" FrameWidth="5" />
                            </DGWC:GaugeContainer>
                        </div>
                        <div>
                            <p>载气压力：</p>
                            <asp:Label runat="server" ID="Lb_145"></asp:Label>
                            <span>KPA</span>
                        </div>
                    </div>



                    <div style="position: absolute; top: 25%; left: 13%; border: ridge" class="twinc">
                        <div>
                            <asp:CheckBox runat="server" Text="YV7" ID="SW_32" />
                            <asp:CheckBox runat="server" Text="YV5" ID="SW_30" />
                        </div>
                        <div>
                            <asp:CheckBox runat="server" Text="YV4" ID="SW_29" />
                            <asp:CheckBox runat="server" Text="YV6" ID="SW_31" />
                        </div>
                    </div>
                    <div style="position: absolute; top: 25%; left: 25%; border: ridge" class="twinc">
                        <asp:CheckBox runat="server" Text="YV10" ID="SW_24" />
                        <asp:CheckBox runat="server" Text="定量阀" ID="SW_141" />
                        <asp:CheckBox runat="server" Text="冷井" ID="SW_59" />
                    </div>
                    <div style="position: absolute; top: 20%; left: 54%; text-align: center" class="twinc">
                        <DGWC:GaugeContainer ID="GC_64" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" AutoLayout="False"
                            runat="server" Width="48px" Height="120px"
                            ShadowIntensity="20" BackColor="White">
                            <Labels>

                                <DGWC:GaugeLabel Name="Label2" TextColor="White" BackGradientEndColor="" TextShadowOffset="1" Parent="LinearGauges.Default"
                                    BackColor="" Text="油缸">
                                    <Size Height="10" Width="28"></Size>
                                    <Location Y="88.2" X="12"></Location>
                                </DGWC:GaugeLabel>
                            </Labels>
                            <Values>
                                <DGWC:InputValue Name="Default" Value="60"></DGWC:InputValue>
                            </Values>
                            <BackFrame FrameWidth="5" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Rectangular"
                                FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                            <LinearGauges>
                                <DGWC:LinearGauge Name="Default">
                                    <Scales>
                                        <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100" FillGradientEndColor="Black"
                                            Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1" Name="ScaleF" Position="60">
                                            <LabelStyle Font="Microsoft Sans Serif, 10pt" TextColor="White" Placement="Outside"></LabelStyle>
                                            <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside"></MajorTickMark>
                                            <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside"></MinorTickMark>
                                        </DGWC:LinearScale>

                                    </Scales>
                                    <BackFrame FrameWidth="5" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom" FrameColor="Gray"
                                        BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                    <Size Height="100" Width="100"></Size>
                                    <Location Y="0" X="0"></Location>
                                    <Pointers>
                                        <DGWC:LinearPointer FillGradientType="LeftRight"
                                            FillGradientEndColor="DarkRed" Placement="Inside" FillColor="Red" Type="Bar"
                                            ScaleName="ScaleF" ThermometerBulbSize="25" ShadowOffset="1" Name="Default" ValueSource="Default" ThermometerStyle="Standard" Interactive="True"></DGWC:LinearPointer>
                                    </Pointers>
                                </DGWC:LinearGauge>
                            </LinearGauges>
                        </DGWC:GaugeContainer>
                        <p>
                            色谱柱：
        <asp:Label runat="server" ID="Lb_64"></asp:Label>
                            度
                        </p>
                    </div>
                    <div style="position: absolute; top: 40%; left: 62%" class="twinc">
                        <asp:CheckBox runat="server" Text="CO2切换阀" ID="SW_143" />
                        <asp:CheckBox runat="server" Text="吹扫阀" ID="SW_142" />
                    </div>

                    <div style="position: absolute; top: 40%; left: 2%; text-align: center" class="twinc">
                        <div>
                            <DGWC:GaugeContainer ID="GC_11" runat="server" BackColor="Black" Height="66px" Width="71px">
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="30">
                                    </DGWC:InputValue>
                                </Values>
                                <CircularGauges>
                                    <DGWC:CircularGauge Name="Default">
                                        <Scales>
                                            <DGWC:CircularScale BorderWidth="1" FillColor="Transparent" FillGradientEndColor="Black" FillGradientType="TopBottom" Name="Default" Width="0">
                                                <LabelStyle TextColor="White" />
                                                <MajorTickMark Shape="Rectangle" Width="4" />
                                            </DGWC:CircularScale>
                                        </Scales>
                                        <Ranges>
                                            <DGWC:CircularRange FillColor="Red" FillGradientType="None" Name="Range1" />
                                        </Ranges>
                                        <Pointers>
                                            <DGWC:CircularPointer CapWidth="30" FillColor="Red" FillGradientEndColor="White" Name="Default" NeedleStyle="NeedleStyle11" ValueSource="Default" />

                                        </Pointers>
                                        <PivotPoint X="50" Y="50" />
                                        <Location X="0" Y="0" />
                                        <Size Height="100" Width="100" />
                                        <BackFrame BackColor="Black" BackGradientEndColor="126, 126, 126" BackGradientType="TopBottom" FrameColor="Gray" FrameGradientEndColor="Black" FrameShape="CustomCircular3" FrameStyle="Edged" FrameWidth="10" />
                                    </DGWC:CircularGauge>
                                </CircularGauges>
                                <Labels>
                                    <DGWC:GaugeLabel BackColor="Gray" BackGradientEndColor="" Name="Label1" Parent="CircularGauges.Default" Text="压力表1" TextAlignment="BottomCenter">
                                        <Location X="28" Y="28" />
                                        <Size Height="14" Width="45" />
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <BackFrame FrameStyle="None" BackColor="Black" BackGradientEndColor="126, 126, 126" BackGradientType="TopBottom" FrameColor="Gray" FrameGradientEndColor="Black" FrameShape="AutoShape" FrameWidth="5" />
                            </DGWC:GaugeContainer>
                        </div>
                        <div>
                            <p>真空度压力检测：</p>
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                            <span>KPA</span>
                        </div>
                    </div>
                    <div style="position: absolute; top: 42%; left: 20%" class="twinc">
                        <asp:CheckBox runat="server" Text="YV14" ID="SW_27" />
                        <asp:CheckBox runat="server" Text="YV15" ID="SW_28" />
                        <asp:CheckBox runat="server" Text="气杯液位" ID="SW_15" />
                        <asp:CheckBox runat="server" Text="YV12" ID="SW_26" />
                        <asp:CheckBox runat="server" Text="YV11" ID="SW_25" />

                    </div>



                    <div style="position: absolute; top: 55%; left: 12%; text-align: center" class="twinc">
                        <div>
                            <DGWC:GaugeContainer ID="GC_87" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" AutoLayout="False"
                                runat="server" Width="48px" Height="120px"
                                ShadowIntensity="20" BackColor="White">
                                <Labels>

                                    <DGWC:GaugeLabel Name="Label2" TextColor="White" BackGradientEndColor="" TextShadowOffset="1" Parent="LinearGauges.Default"
                                        BackColor="" Text="气杯">
                                        <Size Height="10" Width="28"></Size>
                                        <Location Y="88.2" X="12"></Location>
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="60"></DGWC:InputValue>
                                </Values>
                                <BackFrame FrameWidth="5" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Rectangular"
                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                <LinearGauges>
                                    <DGWC:LinearGauge Name="Default">
                                        <Scales>
                                            <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100" FillGradientEndColor="Black"
                                                Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1" Name="ScaleF" Position="60">
                                                <LabelStyle Font="Microsoft Sans Serif, 10pt" TextColor="White" Placement="Outside"></LabelStyle>
                                                <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside"></MajorTickMark>
                                                <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside"></MinorTickMark>
                                            </DGWC:LinearScale>

                                        </Scales>
                                        <BackFrame FrameWidth="5" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom" FrameColor="Gray"
                                            BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                        <Size Height="100" Width="100"></Size>
                                        <Location Y="0" X="0"></Location>
                                        <Pointers>
                                            <DGWC:LinearPointer FillGradientType="LeftRight"
                                                FillGradientEndColor="DarkRed" Placement="Inside" FillColor="Red" Type="Bar"
                                                ScaleName="ScaleF" ThermometerBulbSize="25" ShadowOffset="1" Name="Default" ValueSource="Default" ThermometerStyle="Standard" Interactive="True"></DGWC:LinearPointer>
                                        </Pointers>
                                    </DGWC:LinearGauge>
                                </LinearGauges>
                            </DGWC:GaugeContainer>
                        </div>
                        <div>
                            <p>
                                油温：
        <asp:Label runat="server" ID="Label2"></asp:Label>
                                度
                            </p>
                        </div>
                    </div>
                    <div style="position: absolute; top: 55%; left: 18%" class="twinc">
                        <asp:CheckBox runat="server" Text="油泵" ID="SW_21" />
                        <asp:CheckBox runat="server" Text="伴热带加热" ID="SW_94" />
                    </div>
                    <div style="position: absolute; top: 62%; left: 18%; text-align: center" class="twinc">
                        <div>
                            <DGWC:GaugeContainer ID="GC_22" runat="server" BackColor="Black" Height="66px" Width="71px">
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="30">
                                    </DGWC:InputValue>
                                </Values>
                                <CircularGauges>
                                    <DGWC:CircularGauge Name="Default">
                                        <Scales>
                                            <DGWC:CircularScale BorderWidth="1" FillColor="Transparent" FillGradientEndColor="Black" FillGradientType="TopBottom" Name="Default" Width="0">
                                                <LabelStyle TextColor="White" />
                                                <MajorTickMark Shape="Rectangle" Width="4" />
                                            </DGWC:CircularScale>
                                        </Scales>
                                        <Ranges>
                                            <DGWC:CircularRange FillColor="Red" FillGradientType="None" Name="Range1" />
                                        </Ranges>
                                        <Pointers>
                                            <DGWC:CircularPointer CapWidth="30" FillColor="Red" FillGradientEndColor="White" Name="Default" NeedleStyle="NeedleStyle11" ValueSource="Default" />

                                        </Pointers>
                                        <PivotPoint X="50" Y="50" />
                                        <Location X="0" Y="0" />
                                        <Size Height="100" Width="100" />
                                        <BackFrame BackColor="Black" BackGradientEndColor="126, 126, 126" BackGradientType="TopBottom" FrameColor="Gray" FrameGradientEndColor="Black" FrameShape="CustomCircular3" FrameStyle="Edged" FrameWidth="10" />
                                    </DGWC:CircularGauge>
                                </CircularGauges>
                                <Labels>
                                    <DGWC:GaugeLabel BackColor="Gray" BackGradientEndColor="" Name="Label1" Parent="CircularGauges.Default" Text="度" TextAlignment="BottomCenter">
                                        <Location X="28" Y="28" />
                                        <Size Height="14" Width="45" />
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <BackFrame FrameStyle="None" BackColor="Black" BackGradientEndColor="126, 126, 126" BackGradientType="TopBottom" FrameColor="Gray" FrameGradientEndColor="Black" FrameShape="Rectangular" FrameWidth="5" />
                            </DGWC:GaugeContainer>
                        </div>
                        <div>
                            <p>油泵转速：</p>
                            <asp:Label runat="server" ID="Label3"></asp:Label>
                            <span>VDC</span>
                        </div>
                    </div>
                    <div style="position: absolute; top: 55%; left: 32%; text-align: center" class="twinc">
                        <div>
                            <DGWC:GaugeContainer ID="GC_105" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" AutoLayout="False"
                                runat="server" Width="48px" Height="120px"
                                ShadowIntensity="20" BackColor="White">
                                <Labels>

                                    <DGWC:GaugeLabel Name="Label2" TextColor="White" BackGradientEndColor="" TextShadowOffset="1" Parent="LinearGauges.Default"
                                        BackColor="" Text="气杯">
                                        <Size Height="10" Width="28"></Size>
                                        <Location Y="88.2" X="12"></Location>
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="60"></DGWC:InputValue>
                                </Values>
                                <BackFrame FrameWidth="5" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Rectangular"
                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                <LinearGauges>
                                    <DGWC:LinearGauge Name="Default">
                                        <Scales>
                                            <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100" FillGradientEndColor="Black"
                                                Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1" Name="ScaleF" Position="60">
                                                <LabelStyle Font="Microsoft Sans Serif, 10pt" TextColor="White" Placement="Outside"></LabelStyle>
                                                <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside"></MajorTickMark>
                                                <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside"></MinorTickMark>
                                            </DGWC:LinearScale>

                                        </Scales>
                                        <BackFrame FrameWidth="5" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom" FrameColor="Gray"
                                            BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                        <Size Height="100" Width="100"></Size>
                                        <Location Y="0" X="0"></Location>
                                        <Pointers>
                                            <DGWC:LinearPointer FillGradientType="LeftRight"
                                                FillGradientEndColor="DarkRed" Placement="Inside" FillColor="Red" Type="Bar"
                                                ScaleName="ScaleF" ThermometerBulbSize="25" ShadowOffset="1" Name="Default" ValueSource="Default" ThermometerStyle="Standard" Interactive="True"></DGWC:LinearPointer>
                                        </Pointers>
                                    </DGWC:LinearGauge>
                                </LinearGauges>
                            </DGWC:GaugeContainer>
                        </div>
                        <div>
                            <p>
                                伴热带：
        <asp:Label runat="server" ID="LB_105"></asp:Label>
                                度
                            </p>
                        </div>
                    </div>
                    <div style="position: absolute; top: 55%; left: 40%; text-align: left; line-height: 3" class="twinc">
                        <div>
                            <asp:CheckBox runat="server" Text="进出油阀" ID="SW_23" />
                        </div>
                        <div>
                            <asp:CheckBox runat="server" Text="微水传感器加热" ID="SW_139" />
                        </div>
                        <div>
                            <asp:CheckBox runat="server" Text="微水传感器" ID="SW_140" />
                        </div>
                    </div>
                    <div style="position: absolute; top: 60%; left: 51%; text-align: left; line-height: 3" class="twinc">
                        <div>
                            <asp:CheckBox runat="server" Text="六组分传感器" ID="SW_138" />
                        </div>
                        <div>
                            <asp:CheckBox runat="server" Text="传感室制冷" ID="SW_60" />
                        </div>
                    </div>


                    <div style="position: absolute; text-align: center; top: 55%; left: 62%" class="twinc">
                        <div>
                            <DGWC:GaugeContainer ID="GC_63" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" AutoLayout="False"
                                runat="server" Width="48px" Height="120px"
                                ShadowIntensity="20" BackColor="White">
                                <Labels>

                                    <DGWC:GaugeLabel Name="Label2" TextColor="White" BackGradientEndColor="" TextShadowOffset="1" Parent="LinearGauges.Default"
                                        BackColor="" Text="气缸">
                                        <Size Height="10" Width="28"></Size>
                                        <Location Y="88.2" X="12"></Location>
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="60"></DGWC:InputValue>
                                </Values>
                                <BackFrame FrameWidth="5" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Rectangular"
                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                <LinearGauges>
                                    <DGWC:LinearGauge Name="Default">
                                        <Scales>
                                            <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100" FillGradientEndColor="Black"
                                                Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1" Name="ScaleF" Position="60">
                                                <LabelStyle Font="Microsoft Sans Serif, 10pt" TextColor="White" Placement="Outside"></LabelStyle>
                                                <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside"></MajorTickMark>
                                                <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside"></MinorTickMark>
                                            </DGWC:LinearScale>

                                        </Scales>
                                        <BackFrame FrameWidth="5" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom" FrameColor="Gray"
                                            BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                        <Size Height="100" Width="100"></Size>
                                        <Location Y="0" X="0"></Location>
                                        <Pointers>
                                            <DGWC:LinearPointer FillGradientType="LeftRight"
                                                FillGradientEndColor="DarkRed" Placement="Inside" FillColor="Red" Type="Bar"
                                                ScaleName="ScaleF" ThermometerBulbSize="25" ShadowOffset="1" Name="Default" ValueSource="Default" ThermometerStyle="Standard" Interactive="True"></DGWC:LinearPointer>
                                        </Pointers>
                                    </DGWC:LinearGauge>
                                </LinearGauges>
                            </DGWC:GaugeContainer>
                        </div>
                        <div>

                            <p>
                                冷井：
        <asp:Label runat="server" ID="LB_63"></asp:Label>
                                度
                            </p>
                        </div>
                    </div>
                    <div style="position: absolute; top: 55%; left: 70%; text-align: center" class="twinc">
                        <div>
                            <DGWC:GaugeContainer ID="GC_62" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" AutoLayout="False"
                                runat="server" Width="48px" Height="120px"
                                ShadowIntensity="20" BackColor="White">
                                <Labels>

                                    <DGWC:GaugeLabel Name="Label2" TextColor="White" BackGradientEndColor="" TextShadowOffset="1" Parent="LinearGauges.Default"
                                        BackColor="" Text="气杯">
                                        <Size Height="10" Width="28"></Size>
                                        <Location Y="88.2" X="12"></Location>
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="60"></DGWC:InputValue>
                                </Values>
                                <BackFrame FrameWidth="5" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Rectangular"
                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                <LinearGauges>
                                    <DGWC:LinearGauge Name="Default">
                                        <Scales>
                                            <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100" FillGradientEndColor="Black"
                                                Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1" Name="ScaleF" Position="60">
                                                <LabelStyle Font="Microsoft Sans Serif, 10pt" TextColor="White" Placement="Outside"></LabelStyle>
                                                <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside"></MajorTickMark>
                                                <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside"></MinorTickMark>
                                            </DGWC:LinearScale>

                                        </Scales>
                                        <BackFrame FrameWidth="5" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom" FrameColor="Gray"
                                            BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                        <Size Height="100" Width="100"></Size>
                                        <Location Y="0" X="0"></Location>
                                        <Pointers>
                                            <DGWC:LinearPointer FillGradientType="LeftRight"
                                                FillGradientEndColor="DarkRed" Placement="Inside" FillColor="Red" Type="Bar"
                                                ScaleName="ScaleF" ThermometerBulbSize="25" ShadowOffset="1" Name="Default" ValueSource="Default" ThermometerStyle="Standard" Interactive="True"></DGWC:LinearPointer>
                                        </Pointers>
                                    </DGWC:LinearGauge>
                                </LinearGauges>
                            </DGWC:GaugeContainer>
                        </div>
                        <div>
                            <p>
                                传感器室：
        <asp:Label runat="server" ID="LB_62"></asp:Label>
                                度
                            </p>
                        </div>
                    </div>

                    <div style="position: absolute; left: 80%; top: 10%" class="twinc">
                        <div style="text-align: center; float: left">
                            <DGWC:GaugeContainer ID="GC_88" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" AutoLayout="False"
                                runat="server" Width="48px" Height="120px"
                                ShadowIntensity="20" BackColor="White">
                                <Labels>

                                    <DGWC:GaugeLabel Name="Label2" TextColor="White" BackGradientEndColor="" TextShadowOffset="1" Parent="LinearGauges.Default"
                                        BackColor="" Text="气杯">
                                        <Size Height="10" Width="28"></Size>
                                        <Location Y="88.2" X="12"></Location>
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="60"></DGWC:InputValue>
                                </Values>
                                <BackFrame FrameWidth="5" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Rectangular"
                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                <LinearGauges>
                                    <DGWC:LinearGauge Name="Default">
                                        <Scales>
                                            <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100" FillGradientEndColor="Black"
                                                Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1" Name="ScaleF" Position="60">
                                                <LabelStyle Font="Microsoft Sans Serif, 10pt" TextColor="White" Placement="Outside"></LabelStyle>
                                                <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside"></MajorTickMark>
                                                <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside"></MinorTickMark>
                                            </DGWC:LinearScale>

                                        </Scales>
                                        <BackFrame FrameWidth="5" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom" FrameColor="Gray"
                                            BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                        <Size Height="100" Width="100"></Size>
                                        <Location Y="0" X="0"></Location>
                                        <Pointers>
                                            <DGWC:LinearPointer FillGradientType="LeftRight"
                                                FillGradientEndColor="DarkRed" Placement="Inside" FillColor="Red" Type="Bar"
                                                ScaleName="ScaleF" ThermometerBulbSize="25" ShadowOffset="1" Name="Default" ValueSource="Default" ThermometerStyle="Standard" Interactive="True"></DGWC:LinearPointer>
                                        </Pointers>
                                    </DGWC:LinearGauge>
                                </LinearGauges>
                            </DGWC:GaugeContainer>
                            <p>
                                柜内温度：
        <asp:Label runat="server" ID="LB_88"></asp:Label>
                                度
                            </p>
                        </div>
                        <div style="text-align: center;">
                            <DGWC:GaugeContainer ID="GC_89" ImageUrl="~/TempImages/GaugePic_#SEQ(300,3)" AutoLayout="False"
                                runat="server" Width="48px" Height="120px"
                                ShadowIntensity="20" BackColor="White">
                                <Labels>

                                    <DGWC:GaugeLabel Name="Label2" TextColor="White" BackGradientEndColor="" TextShadowOffset="1" Parent="LinearGauges.Default"
                                        BackColor="" Text="气杯">
                                        <Size Height="10" Width="28"></Size>
                                        <Location Y="88.2" X="12"></Location>
                                    </DGWC:GaugeLabel>
                                </Labels>
                                <Values>
                                    <DGWC:InputValue Name="Default" Value="60"></DGWC:InputValue>
                                </Values>
                                <BackFrame FrameWidth="5" Style="edged" BackGradientEndColor="126, 126, 126" Shape="Rectangular"
                                    FrameColor="Gray" BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                <LinearGauges>
                                    <DGWC:LinearGauge Name="Default">
                                        <Scales>
                                            <DGWC:LinearScale StartMargin="9" Interval="10" FillGradientType="TopBottom" Maximum="100" FillGradientEndColor="Black"
                                                Minimum="0" Width="0" FillColor="Transparent" BorderWidth="1" Name="ScaleF" Position="60">
                                                <LabelStyle Font="Microsoft Sans Serif, 10pt" TextColor="White" Placement="Outside"></LabelStyle>
                                                <MajorTickMark Width="2" BorderWidth="0" FillColor="White" Length="7" Placement="Outside"></MajorTickMark>
                                                <MinorTickMark Width="1.7" BorderWidth="0" FillColor="White" Length="4" Placement="Outside"></MinorTickMark>
                                            </DGWC:LinearScale>

                                        </Scales>
                                        <BackFrame FrameWidth="5" Style="edged" Shape="Rectangular" FrameGradientType="TopBottom" FrameColor="Gray"
                                            BackColor="Black" FrameGradientEndColor="Black" BackGradientType="TopBottom"></BackFrame>
                                        <Size Height="100" Width="100"></Size>
                                        <Location Y="0" X="0"></Location>
                                        <Pointers>
                                            <DGWC:LinearPointer FillGradientType="LeftRight"
                                                FillGradientEndColor="DarkRed" Placement="Inside" FillColor="Red" Type="Bar"
                                                ScaleName="ScaleF" ThermometerBulbSize="25" ShadowOffset="1" Name="Default" ValueSource="Default" ThermometerStyle="Standard" Interactive="True"></DGWC:LinearPointer>
                                        </Pointers>
                                    </DGWC:LinearGauge>
                                </LinearGauges>
                            </DGWC:GaugeContainer>
                            <p>
                                柜外温度：
        <asp:Label runat="server" ID="LB_89"></asp:Label>
                                度
                            </p>
                        </div>
                        <div style="line-height: 3">
                            <asp:CheckBox runat="server" Text="标定" ID="SW_133" />
                            <asp:CheckBox runat="server" Text="采样" ID="SW_134" />
                            <asp:CheckBox runat="server" Text="柜体空调" ID="SW_93" />
                        </div>
                    </div>
                    </div>

                </ContentTemplate>--%>
            </asp:UpdatePanel>

        </div>
    </form>


        <script src="../Scripts/echarts-all.js"></script>
    <script src="../Scripts/event.js"></script>
    <script>
        $("[type=checkbox]").bootstrapSwitch();
        var canvas = document.getElementById('myPic'),
            ctx = canvas.getContext('2d'),
            w = this.innerWidth,
            h = this.innerHeight,
            r = h / 30,
            gcList = new Object(),
            gc_option = {
                backgroundColor: 'lightblue',
                tooltip: {
                    formatter: "{a}  : {c} {b}",
                    position: function (array) {
                        array[0] += 100;
                        return array;
                    }
                },
                toolbox: {
                    show: true,
                    feature: {
                        //mark : {show: true},
                        //restore : {show: true},
                        //saveAsImage : {show: true}
                    },
                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                        fontWeight: 1
                    }
                },
                series: [
                    {
                        name: '真空度压力',
                        type: 'gauge',
                        splitNumber: 5,       // 分割段数，默认为5
                        axisLine: {            // 坐标轴线
                            lineStyle: {       // 属性lineStyle控制线条样式                           
                                color: [[0.2, '#228b22'], [0.8, '#48b'], [1, '#ff4500']],
                                width: 3
                            }
                        },
                        axisTick: {            // 坐标轴小标记
                            splitNumber: 10,   // 每份split细分多少段
                            length: 5,        // 属性length控制线长
                            lineStyle: {       // 属性lineStyle控制线条样式
                                color: 'auto'

                            }
                        },
                        axisLabel: {           // 坐标轴文本标签，详见axis.axisLabel
                            margin: 8,
                            textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                                color: 'auto',
                                fontWeight: '1'
                            }
                        },
                        splitLine: {           // 分隔线
                            show: true,        // 默认显示，属性show控制显示与否
                            length: 10,         // 属性length控制线长
                            lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                                color: 'auto'
                            }
                        },
                        pointer: {
                            width: 3
                        },
                        title: {
                            show: true,
                            offsetCenter: [0, '-20%'],       // x, y，单位px
                            textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                                fontWeight: '2'
                            }
                        },
                        detail: {
                            formatter: '{value}',
                            textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                                color: 'auto',
                                fontWeight: '4'
                            }
                        },
                        data: [{ value: 50, name: 'KPA' }]
                    }
                ]
            },
            gcl_option = {
                backgroundColor: 'lightblue',
                title: {
                    text: '油温',
                    x: 10,
                    y: 10,
                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                        color: 'blue',
                        fontSize: '18'
                    }

                },
                tooltip: {
                    //trigger: 'axis',
                    position: function (array) {
                        array[0] += 55;
                        array[1] -= 60;
                        return array;        }
                },

                toolbox: {
                    show: true,

                },
                calculable: false,
                xAxis: [
                    {
                        type: 'category',
                        data: [''],
                        show: false,
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        max: 100,
                        show: true,
                    }
                ],
                series: [
                       {
                        name: '实际采样/检测值',
                        type: 'bar',
                        data: [50],
                        markPoint: {
                            data: [
                                { type: 'max', name: '单位（度）' },

                            ]                           
                        },


                    }
                ]
            }
        gcOpList = new Object();

       
        gcOpList['GC_11'] = {name:'真空度压力',data:[{value:'0',name:'KPA'}]};
        gcOpList['GC_12'] = { name: '气泵压力', data: [{ value: '0', name: 'KPA' }] };
        gcOpList['GC_13'] = {name:'油压',data:[{value:'0',name:'KPA'}]};
        gcOpList['GC_22'] = { name: '油泵转速', data: [{ value: '0', name: 'VDC' }] };
        gcOpList['GC_145'] = { name: '载气压力', data: [{ value: '0', name: 'KPA' }] };
        gcOpList['GCL_87'] = { name: '油温', data: [1] };
        gcOpList['GCL_63'] = { name: '冷井温度', data: [0] };
        gcOpList['GCL_62'] = { name: '传感器室温度', data: [0] };
        gcOpList['GCL_64'] = { name: '色谱柱温度', data: [0] };
        gcOpList['GCL_88'] = { name: '柜内温度', data: [0] };
        gcOpList['GCL_89'] = { name: '柜外温度', data: [0] };
        gcOpList['GCL_96'] = { name: '伴热带温度', data: [0] };
        

        canvas.width = w;
        canvas.height = h;
        drawLine(ctx, r);
       
        eventUtil.addHandler(document, 'click', function (e) {  //单击页面其它地方，隐藏仪表
            var domL = document.getElementsByTagName('div');
            for (i = 0; i < domL.length; i++) {
                if (domL[i].id!="" && domL[i].id.match("GC"))
                    domL[i].style.visibility = "hidden";

            }          
        });

        
      
        
        $.ajax({
            type: "POST",
            //contentType: "application/json",
            url: "GetGcVal.ashx",
            //url: "YSP_Gauge.aspx/GetGcVal",
            dataType: "json",
            async:true,
            success: function (data) {
                for (key in data)
                {
                    if (Number(key) > 22 && Number(key) < 145) { //柱形仪表
                        var gcName = "GCL_" + key;
                        gcOpList[gcName].series[0].data[0] = data[key];
                        gcl_option.title.text = gcOpList[gcName].name;
                        gcl_option.series[0].data = gcOpList[gcName].data;
                        gcList[gcName].setOption(gcl_option);
                    }
                    else {                                       //圆形仪表
                        var gcName = "GC_" + key;
                        gcOpList[gcName].data[0].value = data[key];
                        gc_option.series[0].name = gcOpList[gcName].name;
                        gc_option.series[0].data = gcOpList[gcName].data;
                        gcList[gcName].setOption(gc_option);
                    }
                }
                    
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //#3这个error函数调试时非常有用，如果解析不正确，将会弹出错误框
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.readyState);
                alert(textStatus); // paser error;
            }
        });
       
        
      

        function drawCircle(x, y) {
            ctx.moveTo(x, y);
            ctx.arc(x, y, r, 0, 2 * Math.PI);
            ctx.stroke();
        }
        function drawRec(x, y) {
            ctx.rect(x,y,3*r,1.2*r);
        }
        function drawSq(x, y) {
            ctx.rect(x,y,3*r,3*r)
        }
        function drawLr(x, y) {
            ctx.rect(x, y, 2*r, 2.8 * r);
        }
        function appendCircle(x, y, r, name) {
            //取得设备层
            var devLayer=document.getElementById("devLayer");

            //创建仪表并添加到页面
            var gcName=name.toUpperCase(),  
                gc = document.createElement("div");
            gc.id=gcName;
            gc.style.position="absolute";
            gc.style.width="180px";
            gc.style.height = "180px";
            gc.style.zIndex = 2;
            gc.style.visibility="hidden";
            devLayer.appendChild(gc);            
            
            gcList[gcName] = echarts.init(gc);            
            gc_option.series[0].name = gcOpList[gcName].name;
            gc_option.series[0].data = gcOpList[gcName].data;
            gcList[gcName].setOption(gc_option);

            //创建一个div对象
            var div = document.createElement("a");            
            //设置div样式，设置为圆形
            div.className = "twinc";
            div.id = name;
            div.style.position = "absolute";
            div.style.left = x -r/2+ "px";
            div.style.top = y +-r/2+ "px";
            div.style.width = r + "px";
            div.style.height = r + "px";
            div.style.borderwidth = r + "px";
            div.style.borderRadius = r + "px";
   
            //给div对象添加事件，单击会显示仪表
            eventUtil.addHandler(div, 'click', function (e) {
                e = eventUtil.getEvent(e);
                var gcName = name.toUpperCase();
                var gauge = document.getElementById(gcName);
                leftL = (x + 180) > window.innerWidth ? x - 180 : x;
                topL = (y + 180) > window.innerHeight ? y - 180 : y;
                gauge.style.left = leftL + "px";
                gauge.style.top = topL + "px";
                gauge.style.visibility = "visible";
                eventUtil.stopPropagation(e);
            });
            //将div对象加入到页面中
            devLayer.appendChild(div);
            //str = "<div class='twinc' style='width:"+r+"px;height:"+r+"px;border-width:"+r+"px"+"'><div>";
        }
        function appendRec(x, y, name) {
            //取得设备层
            var devLayer = document.getElementById("devLayer");

            //创建仪表并添加到页面
            var gcName = name.toUpperCase(),
                gc = document.createElement("div");
            gc.id = gcName;
            gc.style.position = "absolute";
            gc.style.width = "115px";
            gc.style.height = "200px";
            gc.style.zIndex = 2;
            gc.style.visibility = "hidden";
            devLayer.appendChild(gc);

            gcList[gcName] = echarts.init(gc);
            gcl_option.title.text = gcOpList[gcName].name;
            gcl_option.series[0].data = gcOpList[gcName].data;
            gcList[gcName].setOption(gcl_option);


            //创建一个div对象
            var div = document.createElement("a");
            //设置div样式，设置为圆形
            div.className = "twinc";
            div.id = name;
            div.style.position = "absolute";
            div.style.left = x  + "px";
            div.style.top = y+ "px";
            div.style.width = 3*r + "px";
            div.style.height = 1.2*r + "px";
          
            //给div对象添加事件，单击会显示仪表
            eventUtil.addHandler(div, 'click', function (e) {
                e = eventUtil.getEvent(e);
                var gcName = name.toUpperCase();
                var gauge = document.getElementById(gcName);
                leftL = (x + 115) > window.innerWidth ? x - 115 : x;
                topL = (y + 200) > window.innerHeight ? y - 200 : y;
                gauge.style.left = leftL + "px";
                gauge.style.top = topL + "px";
                gauge.style.visibility = "visible";
                eventUtil.stopPropagation(e);
            });



            //将div对象加入到页面中
            devLayer.appendChild(div);
            
        }
        function drawLine(ctx, r) {
            var left = (w-73*r)/2;  //左边距
            var top = 3*r;   //上边距
            ctx.strokeStyle = "lightblue";
            ctx.lineWidth = 1.5;

           
            drawCircle(left + r, top + r); //12
            appendCircle(left + r, top + r,2* r, "gc_12"); //添加gc_12

            ctx.moveTo(left + r,top+r );
            ctx.lineTo(left + 10 * r, top + r);

            drawRec(left + 10 * r, top+0.3*r);
            ctx.moveTo(left + 6*r, top + r);
            ctx.lineTo(left + 6 * r, top + 11 * r);

            drawRec(left + 4.5 * r, top + 5.4 * r);
            drawRec(left + 4.5 * r, top + 7 * r);
            drawLr(left + 15 * r, top + 5.4 * r);
            drawLr(left + 15 * r, top + 9 * r);

            drawRec(left + 4.5 * r, top + 9 * r);
            drawRec(left + 4.5 * r, top + 10.5 * r);
            drawLr(left + 18 * r, top + 5.4 * r);
            drawLr(left + 18 * r, top + 9 * r);
            ctx.stroke();

            drawCircle(left + r, top + 20 * r); // 11
            appendCircle(left + r, top + 20 * r, 2 * r, "gc_11");  //添加gc_11


            drawRec(left + 6.5 * r, top + 19.4 * r); //  27
            //appendRect(left + 6.5 * r, top + 19.4 * r,"gcl_27");  //添加gcl_27

            drawRec(left + 14 * r, top + 19.4 * r);   //28
            drawSq(left + 20 * r, top + 18.5 * r);   //15
            drawRec(left + 25.5 * r, top + 19.4 * r);  //26

            drawSq(left + 30.5 * r, top + 19.5 * r); //油杯

            drawCircle(left + 40 * r, top + 21 * r); //13
            appendCircle(left + 40 * r, top + 21 * r, 2 * r, "gc_13");   //添加gc_13

            drawSq(left + 30 * r, top + 11 * r);  //定量

            drawRec(left + 35 * r, top + 19.4 * r);  //25

            drawRec(left + 35 * r, top + 24 * r);   //23

            drawRec(left + 41 * r, top + 24 * r); //139


            drawRec(left + 39 * r, top + 17 * r); //63
            appendRec(left + 39 * r, top + 17 * r,"gcl_63");            //添加gcl_63

            drawRec(left + 40.5 * r, top + 15.5 * r); //59




            drawRec(left + 2 * r, top + 22 * r);  //96
            appendRec(left + 2 * r, top + 22 * r, "gcl_96");       //添加gcl_96
            drawRec(left + 6.5 * r, top + 22 * r);
            drawCircle(left + 14 * r, top + 22.5 * r);   //22
            appendCircle(left + 14 * r, top + 22.5 * r,2*r,"gc_22"); //添加gc_22
            drawRec(left + 22 * r, top + 22 * r);    //87
            appendRec(left + 22 * r, top + 22 * r, "gcl_87");    //添加gcl_87

            ctx.stroke();

            ctx.beginPath();
            ctx.strokeStyle = "green";
            //画Y7，Y6到气缸的连接线
            ctx.moveTo(left + 6*r, top + 6*r);
            ctx.lineTo(left + 15*r , top + 6*r);
            ctx.moveTo(left + 6*r, top + 11*r);
            ctx.lineTo(left + 15*r , top + 11*r);
            //气缸连接的仪表
            ctx.moveTo(left + 15.5 * r, top + 11.5 * r );
            ctx.lineTo(left + 15.5 * r , top + 15.5 * r);
            ctx.lineTo(left + r, top + 15.5 * r);
            ctx.lineTo(left + r, top + 20.5 * r);
            //drawCircle(left + r, top + 20 * r);

            ctx.moveTo(left + 8 * r, top + 15.5 * r);
            ctx.lineTo(left + 8 * r, top + 20.5 * r);
            //drawRec(left + 6.5 * r, top + 19.4 * r);

            ctx.moveTo(left + 15.5 * r, top + 15.5 * r);
            ctx.lineTo(left + 15.5 * r, top + 20 * r);
            //drawRec(left + 14 * r, top + 20 * r);

            

            ctx.lineTo(left + 27 * r, top + 20 * r);
            //drawSq(left + 20 * r, top + 29 * r);
            //drawRec(left + 25.5 * r, top + 20 * r);

            ctx.lineTo(left + 27 * r, top + 24.5 * r);
            ctx.lineTo(left + 32 * r, top + 24.5 * r);
            ctx.lineTo(left + 32 * r, top + 22.5 * r);
            //drawSq(left + 30 * r, top + 19.5 * r); //油杯
            ctx.moveTo(left + 33.5 * r, top + 21 * r);
            ctx.lineTo(left + 40 * r, top + 21 * r);
            //drawCircle(left + 40 * r, top + 21 * r); //13


            ctx.moveTo(left + 16.5 * r, top + 11.5 * r);
            ctx.lineTo(left + 16.5 * r, top + 15.5 * r);
            ctx.lineTo(left + 35 * r, top + 15.5 * r);
            ctx.lineTo(left + 35 * r, top + 13 * r);
            ctx.lineTo(left + 33 * r, top + 13 * r);
            //drawSq(left + 30 * r, top + 11 * r);  //定量
            ctx.stroke();


            ctx.beginPath();
            ctx.strokeStyle = "yellow";
            //油缸的连接线
            ctx.moveTo(left + 6 * r, top + 7.5 * r);
            ctx.lineTo(left + 18 * r, top + 7.5* r);
            ctx.moveTo(left + 6 * r, top + 9.5 * r);
            ctx.lineTo(left + 18 * r, top + 9.5 * r);


            ctx.moveTo(left + 19 * r, top + 11.5 * r);
            ctx.lineTo(left + 19 * r, top + 18 * r);
            ctx.lineTo(left + 36.5 * r, top + 18 * r);
            //drawRec(left + 35 * r, top + 19.4 * r);  //25
            ctx.lineTo(left + 36.5 * r, top + 24.5 * r);
            //drawRec(left + 35 * r, top + 24 * r);   //23
            ctx.lineTo(left + 42 * r, top + 24.5 * r);

            //drawCircle(left + 41 * r, top + 24.5 * r); //139
            ctx.stroke();


            ctx.beginPath();
            ctx.strokeStyle = "lightblue";
            ctx.moveTo(left + 3 * r, top + 22.5 * r);
            //ctx.lineTo(left + 3 * r, top + 22 * r);
            ctx.lineTo(left + 30.5 * r, top + 22.5 * r);
            


            ctx.moveTo(left + 31 * r, top + 14 * r);
            ctx.lineTo(left + 31 * r, top + 17.5 * r);
            ctx.lineTo(left + 39 * r, top + 17.5 * r);
            //drawCircle(left + 39 * r, top + 17.5 * r); //63

            ctx.moveTo(left + 32 * r, top + 14 * r);
            ctx.lineTo(left + 32 * r, top + 16 * r);
            ctx.lineTo(left + 43 * r, top + 16 * r);
            //drawRec(left + 40.5 * r, top + 15.5 * r); //59


            ctx.moveTo(left + 33 * r, top + 12 * r);
            ctx.lineTo(left + 42 * r, top + 12 * r);
            ctx.lineTo(left + 42 * r, top + 6 * r);
            ctx.lineTo(left + 47 * r, top + 6 * r);
            ctx.rect(left + 47 * r, top + 5 * r, 4 * r, 6 * r); //色谱柱
            appendRec(left + 48 * r, top + 5.5 * r, "gcl_64");              //添加gcl_64

            ctx.moveTo(left + 51 * r, top + 10 * r);
            ctx.lineTo(left + 55 * r, top + 10 * r);
            ctx.lineTo(left + 55 * r, top + 12 * r);
            drawRec(left + 53.5 * r, top + 12 * r); //143

            ctx.moveTo(left + 54 * r, top + 13.2 * r);
            ctx.lineTo(left + 54 * r, top + 15 * r);
            ctx.lineTo(left + 50 * r, top + 15 * r);
            ctx.lineTo(left + 50 * r, top + 20 * r);
            drawRec(left + 48.5 * r, top + 17 * r); //142
            ctx.rect(left + 46 * r, top + 20 * r, 8 * r, 6 * r); //传感器室
            appendRec(left + 50 * r, top + 24 * r, "gcl_62");       //添加gcl_62

            ctx.moveTo(left + 56 * r, top + 13.2 * r);
            ctx.lineTo(left + 56 * r, top + 15 * r);
            ctx.lineTo(left + 56 * r, top + 17 * r);
            drawSq(left + 54.5 * r, top + 17 * r);  //CO2传感器

            ctx.moveTo(left + 30 * r, top + 12 * r);
            ctx.lineTo(left + 25 * r, top + 12 * r);
            ctx.lineTo(left + 25 * r, top);
            ctx.lineTo(left + 50 * r, top);
            ctx.lineTo(left + 50 * r, top + r);
            drawRec(left + 48.5 * r, top + r); //稳流阀

            ctx.moveTo(left + 32 * r, top + 11 * r);
            ctx.lineTo(left + 32 * r, top + 1.5 * r);
            ctx.lineTo(left + 60 * r, top + 1.5 * r);
            ctx.lineTo(left + 60 * r, top + 25 * r);
            ctx.lineTo(left + 54 * r, top + 25 * r);
            drawRec(left + 58.5 * r, top + 1 * r);  //稳压阀


            ctx.moveTo(left + 60 * r, top + 1.5 * r);
            ctx.lineTo(left + 65 * r, top + 1.5 * r);
            ctx.lineTo(left + 65 * r, top + 7 * r);
            drawCircle(left + 65 * r, top + 1.5 * r); //145
            appendCircle(left + 65 * r, top + 1.5 * r,2*r,"gc_145");   //添加gc_145
            var a=ctx.rect(left + 64 * r, top + 7 * r, 2 * r, 14 * r); //载气

            drawRec(left + 23.5 * r, top + 6 * r);// 24

            drawRec(left + 70 * r, top + 0.5 * r);  //88
            appendRec(left+70*r,top+0.5*r,"gcl_88");      //添加gcl_88
            drawRec(left + 70 * r, top + 6 * r);  //89
            appendRec(left + 70 * r, top + 6 * r, "gcl_89");  //添加gcl_89
            drawRec(left + 70 * r, top + 11.5 * r);  //133
            drawRec(left + 70 * r, top + 17 * r);  //134
            drawRec(left + 70 * r, top + 22.5 * r);  //93

            ctx.stroke();
        }
    </script>
</body>
</html>
