$(function () {
    $(".navbar .container").css("margin", "0").css("width", "auto");
    $("#myModal").on("hidden.bs.modal", function () {
        $(this).removeData("bs.modal");
    }); 
    var bgColor=$("body").css("background-color");
    $("#myDev").css("background-color",bgColor ).css("border-color",bgColor);
    $("#map").height(window.innerHeight - 150 + 'px');//设置地图高度
    var map = new BMap.Map("map"); //添加地图到页面   

    
    var point = new BMap.Point(108.99985606913, 34.259565188471);
    //var marker = new BMap.Marker(point);
    //map.addOverlay(marker);
    map.centerAndZoom(point, 5);
    map.enableScrollWheelZoom();

    var top_left_control = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT });// 左上角，添加比例尺
    var top_left_navigation = new BMap.NavigationControl();  //左上角，添加默认缩放平移控件
    map.addControl(top_left_control);
    map.addControl(top_left_navigation);

    $("#closeMyDev").click(function () {
        $("#myDev").hide();
        map.enableScrollWheelZoom();
        map.enableDragging();
        map.enableDoubleClickZoom();
    });
   
    ////同步传输，得到地图上需要显示的点的数据
    $.ajax({
        type: "POST",
        url: "addMyMarkers.ashx",
        dataType: "json",
        async: false,

        //回调函数，data，返回值
        success: function (data) {            
            //if (result = null)
            //    alert("无地图数据或数据库连接出错！");
            //else {
                //遍历并解析得到的json数据
            $(data).each(function (i,item) {
                $(item).each(function(){
                    var lng = Number(this['Lng']);  //经度
                    var lat = Number(this['Lat']);  //纬度

                    //构造点和标注，在地图上显示
                    var point = new BMap.Point(lng, lat);
                    var locateID = this['_id'].$oid;
                    var title = this['CompName'];     //将标注的名称设为单位名称
                    var marker = new BMap.Marker(point, { title: title });
                    map.addOverlay(marker);

                    //为每一个marker添加侦听函数，单击时触发，获取设备信息
                    marker.addEventListener("click", function (e) {
                        map.setCenter(this.getPosition());
                        map.disableScrollWheelZoom();
                        map.disableDragging();
                        map.disableDoubleClickZoom();
                        //var point = new BMap.point(e.point.lng, e.point.lat);
                        var x = map.pointToPixel(e.point).x;
                        var y = map.pointToPixel(e.point).y;
                        var title = this.getTitle();  //title值为该点的单位名称
                        $("#myDev").getDev(x, y, locateID,title);
                    });
                })
                })
            //}
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //#3这个error函数调试时非常有用，如果解析不正确，将会弹出错误框
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus); // paser error;
        },

    });
   
})
$.fn.extend({getDev:function(left,top,locateID,title)
{
    //设置弹窗标题和位置
    $("#myDev").hide();
    $("#devTitle").text(title);
    //left += $("#map").offset().left;
    //alert(left.toString());
    //top+=top+$("#map").offset().top;
    $("#myDev").height(300).width(200);
    $("#myDev").css("z-index", 1);
    $("#myDev").css("left", left).css("top", top - 100);
    if ($("#myAccordion").length>0) { $("#myAccordion").remove();};

    
    
    var devContent = "";

    ////同步传输通过getDev.ashx处理程序取得json对象，拼接为html,即infoWindow要显示的内容，回传
    //JSON格式
    //传输的参数title是单位名称
    $.ajax({
        type: "POST",
        url: "getDev.ashx?CompName=" + encodeURI(title),       
        dataType: "json",
        async: false,

        //回调函数，result，返回值
        success: function (data) {

            devContent = "<div class='panel-group' id='myAccordion'>";           
            if (data == null) {
                devContent = "无设备！";
                return devContent;
            }
            else {
                var tempType = "";
                var tempText = "";
                var text = "";
                //定义折叠组前段标记

                // 遍历Json对象的属性
                $.each(data, function (i, item) {

                    //是新类型，则新建立一个折叠组
                    if (tempType != item.Type) {
                        devContent = devContent.concat(tempText);
                        tempType = item.Type;
                        text = "<div class='panel panel-default'>" +
                            "<div class='panel-heading'>" +
                             "<h4 class='panel-title'>" +
                            "<a data-toggle='collapse' data-parent='#myAccordion' href='#collapse" + item.Type + "'>" +
                            item.Type +
                            "</a></h4></div>" +
                            "<div id='collapse" + item.Type + "' class='panel-collapse collapse'>" +
                            "<div class='panel-body'>" +
                            "<a class='text-primary btn' title='" + item.Type + "' data-devId=" + item.DevID + ">" +
                            item.DevName +
                            "</a><br/>";
                        tempText = "</div></div></div>"                      
                      
                    }
                        //不是新类型，则在其原折叠组内添加设备
                    else {
                        text = "<a class='text-primary btn' title='" + item.Type + "' data-devId=" + item.DevID + ">" +
                            item.DevName +
                            "</a><br/>";
                    } 
                    devContent = devContent.concat(text);
                })
                //panel-default后段标记
                devContent = devContent.concat(tempText).concat("</div>");
            }}
    })
    devContent = devContent.concat("</div></div>");    //定义折叠组后段标记
    //$("#myDev").load(devContent);
    //if (devContent == "")
    //    alert("连接出错！");
    //else{
    $("#devTitle").after(devContent);  //将拼接好的内容字段插入页面中
  

    //点击具体设备号时的操作
    $(".text-primary").click(function () {

        
        var devId = $(this).data("devid");
        var devType = this.title;
        var devName = this.innerText;

    
   
        //调试按钮点击打开新窗口，调试页面
        //$("#DebugButton").attr("href", "../DevInfoes/Debug" + "?DevID="+devId ).attr("target", "_blank");
        
        
  
        switch (devType) {
            case "油色谱在线监测":
                //showPopWin('DevData/YSP?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName), 830, 430, null, true, true);
                $("#myModal").modal({ remote: 'DevData/YSP?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                break;
            case "GIS局放":
                $("#myModal").modal({ remote: 'DevData/GIS?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                break;
            case "变压器局放":
                $("#BYQModal").modal({ remote: 'DevData/GIS?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                break;
            case "开关柜测温":
                $("#KGGModal").modal({ remote: 'DevData/GIS?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                break;
            case "铁芯接地":
                $("#TXJDModal").modal({ remote: 'DevData/GIS?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                //$("#TXJDModal").modal('show');
                break;
            case "避雷器":
                $("#BLQModal").modal({ remote: 'DevData/GIS?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                break;
            case "容性设备":
                $("#RXSBModal").modal({ remote: 'DevData/GIS?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                break;
            case "SF6微水密度":
                $("#SF6WSModal").modal({ remote: 'DevData/GIS?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                break;
            case "SF6泄露":
                $("#SF6XLModal").modal({ remote: 'DevData/GIS?DevID=' + encodeURI(devId) + '&DevSite=' + encodeURI(title) + '&DevType=' + encodeURI(devType) + '&DevName=' + encodeURI(devName) });
                break;
        }
      
        //报警分析窗口的标题
        //$("#alarmModal .modal-title").text("报警分析" + "——" + title + " . " + devType + " . " + devName + "");


                          
    })
       
      
    

    $(".collapse").collapse;
    $("#myDev").show();
}
  
})