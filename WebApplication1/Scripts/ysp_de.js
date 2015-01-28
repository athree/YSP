$(function () {

    //更改input里的值则触发，提交时可使用
    $("#sysSet :input").change(function () {
        alert("系统设置" + this.name + ",,,,," + this.value);
        compareVal(this.name, this.value);

    });
    $("#calParam :input").change(function () {
        alert("计算参数" + this.name + ",,,,," + this.value);

    });
    $("#ctrlParam :input").change(function () {
        alert("控制参数" + this.name + ",,,,," + this.value);
        compareVal(this.name, this.value);
    });
    $("#stateCtrl :input").change(function () {
        alert("状态/控制" + this.name + ",,,,," + this.value);
        compareVal(this.name, this.value);
    });




    var dbFlag = 0;
    var db;
    if ("indexedDB" in window)
    {
        dbFlag = 1;
        console.log("===Store in indexedDB===");
        var indexDB = window.indexedDB || window.webkitIndexedDB || window.mozIndexedDB;
        var dbName = "siml";
        var tableName = "val";
        var openRequest = indexedDB.open(dbName);
        openRequest.onupgradeneeded = function (e) {
            var db = e.target.result;

            //如果找不到该表，则重新创建一个
            if (!db.objectStoreNames.contains(tableName)) {
                console.log("Create Table:" + tableName);
                var objStore = db.createObjectStore(tableName, { keyPath: "key" });

            }
        }
        openRequest.onsuccess = function (e) {
            db = e.target.result;
            db.onerror = function (event) {
                alert("Database Error:" + event.target.erroCode);
                console.dir(event.target);

            };
            if (db.objectStoreNames.contains(tableName)) {
                var transaction = db.transaction([tableName], "readwrite");
                transaction.oncomplete = function () {
                    console.log("Open Table:" + tableName);

                }
                transaction.onerror = function () {
                    console.dir(event);
                }
                var store = transaction.objectStore(tableName);
                var ob = store.get("4");
                ob.onsuccess = function (e) {
                    if (e.target.result == null)
                        init(db);
                }

            }
        }
        openRequest.onerror = function () {
            console.log("Open Database Failed!");
        }
    }
    else
    {
        console.log("===Store in array!===");
        var valArr = new Array();
        for (var i = 0; i < 366; i++)
        {
            valArr[i] = new Array();
        }
        initArr();
    }
    
   


    function init(db) {  //读取xml文件，初始化IndexdDB
        console.log("Init Table:"+tableName);
        $.ajax({
            url: "../Content/SimlVal.xml",
            dataType: 'xml',
            type: 'GET',
            timeout: 2000,
            error: function (xml) {
                alert("加载XML文件出错！");
            },
            success: function (xml) {
                $(xml).find("add").each(function (i) {
                    var key = $(this).attr("key");
                    var type = $(this).attr("type");
                    var min = Number($(this).attr("min"));
                    var max = Number($(this).attr("max"));
                    var simlVal = { "key": key, "type": type, "min": min, "max": max };                
                    var transaction = db.transaction([tableName], "readwrite");
                    transaction.oncomplete = function () {
                    
                    }
                    transaction.onerror = function () {
                        console.dir(event);
                    }
                    var store = transaction.objectStore([tableName]);
                    store.add(simlVal);
                    console.log("Add data with key:" + key);
               
                
                });            
                console.log("Init Complete!");
                db.version = 2;
            }
        });
   

    }  


    function initArr() {
        
        $.ajax({
            url: "../Content/SimlVal.xml",
            dataType: 'xml',
            type: 'GET',
            timeout: 2000,
            error: function (xml) {
                alert("加载XML文件出错！");
            },
            success: function (xml) {
                $(xml).find("add").each(function (i) {
                    var k = $(this).attr("key");
                    valArr[k][0] = k;
                    valArr[k][1] = $(this).attr("type");
                    valArr[k][2] = Number($(this).attr("min"));
                    valArr[k][3] = Number($(this).attr("max"));
                    console.log("Add data with key:"+k);
                })
            }

        })
    }


    function compareVal(name,val) {
        var splitArr = name.split("_"),
            myRange,
            timeA = new Date(),
            k = splitArr[1];
        if (splitArr[0] == "TB") {  ////只对TextBox执行比较，下拉列表和开关不用比
            var range = new Array(),
            ready;
            if (dbFlag) {
                var transaction = db.transaction([tableName], "readwrite");
                transaction.oncomplete = function () {
                    ready = 1;
                    console.log("Open Table:" + tableName + ",transaction complete!" + new Date().getSeconds() + new Date().getMilliseconds());

                }
                transaction.onerror = function () {
                    compareVal(name, val);
                }
                var store = transaction.objectStore(tableName);
                var ob = store.get(k);
                ob.onsuccess = function (e) {
                    console.log("ob  success");
                    var data = e.target.result;
                    range[0] = data.min;
                    range[1] = data.max;
                    range[2] = data.type;
                    doCompare(name, val, range);
                }
            }
            else {
                var data = valArr[Number(k)];
                range[0] = data[2];
                range[1] = data[3];
                range[2] = data[1];
                doCompare(name, val, range);

            }
        }       
        
    }


    function doCompare(name,val,myRange)
    {
       
        if(myRange!=null)
        {
            if (myRange[2] == "1" || myRange[2] == "5") {//ushort类型检测，不能带小数
                if (val<0 || val * 10 % 10 != 0) {
                    $("#" + name).css("background-color", "red")
                    alert("错误！<br/>" + "必须为正整数！");
                    $("select").attr("disabled", "disabled");
                    $("input").attr("disabled", "disabled");
                    $(".bootstrap-switch input").bootstrapSwitch("readonly", true);
                    $("#" + name).removeAttr("disabled");
                    $(".btn-danger").hide();
                    return;
                }
            }
            else {
                console.log(name + "数据类型检测通过！" + "type=" + myRange[2]);
            }
            if (val < myRange[0] || val > myRange[1]) {
                $("#" + name).css("background-color", "red")
                alert("错误！<br/>" + "数值范围应在" + myRange[0] + "到" + myRange[1] + "之间");
                $("select").attr("disabled", "disabled");
                $("input").attr("disabled", "disabled");
                $(".bootstrap-switch input").bootstrapSwitch("readonly", true);
                $("#" + name).removeAttr("disabled");
                $(".btn-danger").hide();
                return;
            }
            else {
                console.log(name+"数值范围内。");
            }

        }       
        else
        {
            console.log(name + "无数值范围！");
           
        }
        $("#" + name).css("background-color", "white");
        $(".btn-danger").show();
        $("select").removeAttr("disabled");
        $("input").removeAttr("disabled");
        $(".bootstrap-switch input").bootstrapSwitch("readonly", false)
    }

})