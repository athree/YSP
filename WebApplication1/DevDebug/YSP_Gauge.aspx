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
        </div>
        <canvas id="myPic" style="z-index:-3;position:absolute;top:0;left:0"></canvas>
        <div>
            
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
        var timer1 = setInterval(getGcVal, 8000);
        
      
        var getGcVal = function () {
            $.ajax({
                type: "POST",
                //contentType: "application/json",
                url: "GetGcVal.ashx",
                //url: "YSP_Gauge.aspx/GetGcVal",
                dataType: "json",
                async: true,
                success: function (data) {
                    for (key in data) {
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
        }
       
       
        
      

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
