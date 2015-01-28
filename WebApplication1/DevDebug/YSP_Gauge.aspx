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
        <canvas id="myPic" style="z-index:-3;position:absolute;top:0;left:0"></canvas>
        <div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>

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

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>

    <script>
        
        $("[type=checkbox]").bootstrapSwitch();
        var canvas = document.getElementById('myPic');
        var ctx = canvas.getContext('2d');
        var w = this.innerWidth;
        var h=this.innerHeight;
        canvas.width=w;
        canvas.height=h;
        ctx.fillStyle ='gray';
        ctx.fillRect(w * 0.06, h * 0.01, w * 0.01, h * 0.1);
        ctx.fillRect(w * 0.06, h * 0.01, w * 0.82, w * 0.01);
        ctx.fillRect(w * 0.88, h * 0.01, w * 0.01, h * 0.1);
        ctx.fillRect(w * 0.65, h * 0.01, w * 0.01, h * 0.1);
    </script>
</body>
</html>
