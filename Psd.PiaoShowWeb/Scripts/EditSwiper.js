var mySwiper;
var myPath = "../../";
var loading = "<p>加载中...<img src='../content/image/loading.gif' width='20%'></p>";
var IsEdit = false;
var AppCode;
function keyevent() {
    if (event.keyCode == 13) page.SavePage();
}
document.onkeydown = keyevent;

//layui模块的定义
layui.define(['element', 'layer', 'form'],
    function (exports) {
        var layer = layui.layer,
            form = layui.form();
        exports('index', {}); //注意，这里是模块输出的核心，模块名必须和use时的模块名一致
    });
layui.use('upload',
    function () {
        layui.upload({
            url: '../../Upload/Upload/',
            //上传接口
            ext: 'jpg|png|gif|svg',
            success: function (res) { //上传成功后的回调
                // console.log(res); //res.pic
                $("#" + localStorage.getItem("KitId") + " img").attr("src", res.pic);
                layer.msg("上传成功！");
            }
        });
    });
layui.use('element',
    function () {
        var element = layui.element();

        //…
    });

function swiperload() {
    mySwiper = new Swiper('.swiper-container', {
        onInit: function (swiper) { //Swiper2.x的初始化是onFirstInit
            swiperAnimateCache(swiper); //隐藏动画元素
            swiperAnimate(swiper); //初始化完成开始动画
        },
        simulateTouch: false
    });
}
window.c = function (e) {
   // console.log(e.currentTarget.value);
    if (e.currentTarget.value < 0 || e.currentTarget.value == "") {
        e.currentTarget.value = 0;
    }
}
//页面工具
var page = {
    //预览操作
    PreviewPage: function () {
        layer.msg("页面预览");
        layer.open({
            title: "手机预览",
            type: 2,
            area: ["280px","536px"],
            content: myPath + "m/i/" + $("#AppCode").val(),
            btn: ["传送门----> 手机", "关闭"],
            btn1: function () {
                layer.open({
                    type: 1,
                    title: false,
                    closeBtn: 0,
                    area: ["170px", "170px"],
                    skin: 'layui-layer-nobg', //没有背景色
                    shadeClose: true,
                    content: "<div style='background-color: white;padding: 10px;' ><img src='https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=http://192.168.8.125:8083/m/i/" + $("#AppCode").val() + "'/></div>"
                });
            }
        });
    },
    //保存页面
    SavePage: function () {
        layer.msg("页面保存");
        pageEdit.getiphonepage();
    }
    //
}

var pageEdit = {
    //添加图片组件
    AddImgToIphone: function () {
        var pagecode = $("#EditPageCode").val();
        if (pagecode.length < 1) {
            layer.msg("您还没告诉我需要我将图片添加在哪个页面哦？");
            return false;
        }
        var imgcontrol = "";
        var data = {
            pagecode: pagecode,
            t: "image"
        }
        layer.msg("正在努力加载");
        $.post(myPath + "Api/EditPageCreateKitActionResult", data,
            function (res) {
                //alert(res.Msg);
                if (res.Result > 0) {
                    imgcontrol = res.Msg;
                }
                layer.closeAll();
                pageEdit.AddImgAction(imgcontrol);

            });

    },
    AddImgAction: function (ImgKitHtml) {
        // alert(imgcontrol);
        $("#iphone").append(ImgKitHtml);
        swiperload();
        $(".imgview").draggable({
            //containment: ".iphone",
            scroll: false
        },
            function () {
                // alert(1);
            }).resizable();

        $(".imgview").mouseover(function (event) { //鼠标经过事件
            $(this).css({
                "border": "1px dashed orange"
            });
        }).mouseout(function () { ////鼠标移出事件
            $(this).css({
                "border-color": "rgba(0,0,0,0)"
            });
            editmenu("#" + $(this).attr("id"));

        }).click(function (e) { //鼠标点击事件
            var sid = $(this).attr("id"); //组件id
            var stype = $(this).attr("Ptype"); //获取组件类型
            $(".imgview").removeClass("active");
            localStorage.setItem("KitId", sid); //缓存编辑中的组件id
            $(this).addClass("active");
            switch (stype) { //判断组件类型
                case "imgview":
                    //图形组件编辑
                    {
                        $(".control-animations,.control-duration,.control-delay,.control-angle,.control-imgupload,.control-link").show(500); //展开编辑器
                        var ele_effect = $(this).attr("swiper-animate-effect"); //获取元素动画
                        var ele_duration = $(this).attr("swiper-animate-duration"); //动画持续时间
                        var ele_delay = $(this).attr("swiper-animate-delay"); //动画延迟时间
                        //layer.alert("动画:" + ele_effect + "<br/>持续时间：" + ele_duration + "<br/>延迟时间：" + ele_delay);
                        $("#editcontrol-id").val(sid);
                        $("#animationsset").val(ele_effect); //赋值动画
                        $("#swiperanimateduration").val(ele_duration.substring(0, ele_duration.indexOf("s"))); //赋值动画时间
                        $("#swiperanimatedelay").val(ele_delay.substring(0, ele_delay.indexOf("s"))); //赋值延迟时间
                        var handle = $("#swiperanimateangle");
                        var matrix = $(this).find("img").css("transform").toString();
                        var imgdegZ = pageEdit.tools.getmatrixtodeg(matrix);
                        //  layer.msg("图片旋转角度为：" + imgdegZ);
                        //console.log(imgdegZ);
                        $("#sliderangle").slider({
                            value: imgdegZ
                        });
                        handle.text(imgdegZ);

                        break;
                    }
                default:
                    {
                        layer.msg("未知类型组件：" + stype);
                    }
            }
            event.stopPropagation();
        });
    },
    AddTextToIphone: function () {
        layer.msg("该功能正在开发中，尽请期待！");
    },
    //删除图片组件
    delimg: function (KitId) {
        $(localStorage.getItem("menuid")).remove();
        var data = {
            pagecode: $("#EditPageCode").val(),
            kitId: KitId
        };
        $.post(myPath + "Api/EditPageDelKitActionResult",
            data,
            function (res) {
                if (res.Result > 0) {
                    layer.alert(res.Msg);
                    layer.closeAll();
                } else {
                    layer.alert(res.Msg);
                }
            });

        $(".control-animations,.control-duration,.control-delay,.control-angle,.control-imgupload,.control-link").hide(500); //展开关闭
    },
    tools: {
        gettime: function () {
            // layer.msg(new Date() + new Date().getTime());
            return new Date().getTime() + parseInt(Math.random(0, 9999));
        },
        jsontostring: function (j) {
            return JSON.stringify(j);
        },
        /* 
         * 解析matrix矩阵，0°-360°，返回旋转角度
         * 当a=b||-a=b,0<=deg<=180
         * 当-a+b=180,180<=deg<=270
         * 当a+b=180,270<=deg<=360
         *
         * 当0<=deg<=180,deg=d;
         * 当180<deg<=270,deg=180+c;
         * 当270<deg<=360,deg=360-(c||d);
         * */
        getmatrix: function (a, b, c, d, e, f) {
            var aa = Math.round(180 * Math.asin(a) / Math.PI);
            var bb = Math.round(180 * Math.acos(b) / Math.PI);
            var cc = Math.round(180 * Math.asin(c) / Math.PI);
            var dd = Math.round(180 * Math.acos(d) / Math.PI);
            var deg = 0;
            if (aa == bb || -aa == bb) {
                deg = dd;
            } else if (- aa + bb == 180) {
                deg = 180 + cc;
            } else if (aa + bb == 180) {
                deg = 360 - cc || 360 - dd;
            }
            return deg >= 360 ? 0 : deg;
            //return (aa+','+bb+','+cc+','+dd);  
        },
        getmatrixtodeg: function (a) {

            //console.log(a);
            a = a.substring(7, a.length - 1).split(",");
            return pageEdit.tools.getmatrix(a[0], a[1], a[2], a[3], a[4], a[5]);
            //return (aa+','+bb+','+cc+','+dd);  
        }

    },
    Setbgimg: function () { //设置背景
        layer.msg("加载中....");
        $.getJSON("../../api/GetTemplatBgList", "",
            function (res) {
                var contents = "";
                var strimg = "<img src='../../template/bg/@url' onclick='@ck' width='100'style='margin:5px;'/>";
                for (var i = 0; i < res.bglist.length; i++) {
                    var newimg = strimg.replace("@url", res.bglist[i]);
                    newimg = newimg.replace("@ck", "pageEdit.changebg(\"" + res.bglist[i].replace("_thumb", "") + "\")");
                    contents += newimg;
                }
                contents = myPath +"Edit/AppBgPage/"+appCode;
                //页面层
                layer.open({
                    type: 2,
                    resize :false,
                    scrollbar:true,
                    title: '请选择背景图',
                    //加上边框
                    maxmin: false,
                    area: ['850px', '640px'],
                    //宽高
                    content: contents
                });
            });
    },
    changebg: function (img) {
        layer.closeAll();
        //layer.msg("../template/bg/" + img);
        if (img.indexOf("_thumb") > 0) {
            img = img.replace("_thumb", "");
        }
        var bgimgpath = myPath + img;
        $("#iphone").css({
            "background-image": "url('" + bgimgpath+"')",
            "background-size": "100% 100%",
            "background-repeat":"no-repeat"
        });
        var data = {
            t: "bg",
            val: img,
            appcode: $("#AppCode").val()
        }
        $.getJSON(myPath + "Api/EditAppBase", data,
            function (res) {
                console.log(res);
            });
    },
    Setbgmp3: function () { //设置背景mp3
        //console.log("开始请求数据");

        layer.msg("加载中....");
        $.getJSON("../../api/GetTemplateMp3List", "",
            function (res) {
                //console.log("请求已完成");
                layer.close(layer.index);
                var contents = "";
                var strimg = "<div onclick='@ck' class='layui-btn' target='_blank'  style='margin: 5px;min-width:180px;' >@mp3name</div>";
                for (var i = 0; i < res.mp3list.length; i++) {

                    var mp3url = "../../template/mp3/" + res.mp3list[i].filename;
                    var mp3name = res.mp3list[i].musicname == "" ? "无标题" + i : res.mp3list[i].musicname;
                    var newimp3 = strimg.replace("@mp3name", mp3name);
                    newimp3 = newimp3.replace("@mp3url", mp3url);
                    newimp3 = newimp3.replace("@ck", "pageEdit.changemp3(\"" + mp3url + "\",\"" + mp3name + "\")");
                    contents += newimp3;
                }
                //页面层
                layer.open({
                    type: 1,
                    title: '请选择音乐',
                    skin: 'layui-layer-rim',
                    //加上边框
                    area: ['800px', '640px'],
                    //宽高
                    content: contents

                });
            });
        //layer.alert("设置背景音乐");
    },
    changemp3: function (mp3, name) {
        layer.closeAll();
        //layer.msg("../template/bg/" + img);
        // layer.alert(mp3);
        layer.open({
            type: 0,
            title: '确认使用这首音乐吗？',
            skin: 'layui-layer-rim',
            //加上边框
            area: ['500px', '500px'],
            //宽高
            content: ('<audio autoplay="autoplay" style="display:hidden"><source src="' + mp3 + '" type="audio/mpeg"/></audio><img width="80%" style="margin-left:10%" ' +
                'src="http://mpic.tiankong.com/8eb/830/8eb830412d87dead55b43dfee7217827/640.jpg"/>'),
            yes: function (index, layero) {
                //do something
                layer.close(index); //如果设定了yes回调，需进行手工关闭
                layer.msg("以选中：" + name);
                var data = {
                    t: "mp3",
                    val: mp3,
                    appcode: $("#AppCode").val()
                }
                $.getJSON(myPath + "Api/EditAppBase", data,
                    function (res) {
                        layer.msg("设置成功");
                    });

            },
            cancel: function (index, layero) {
                layer.close(index);
                pageEdit.Setbgmp3();
                return false;
            }
        });
    },
    getiphonepage: function () { //获取页面
        var Items = $(".imgview");
        var eleList = {};
        var kitlist = [];
        for (var i = 0; i < Items.length; i++) {
            var ele = Items[i];
            // alert($(ele).attr("Ptype"));
            kitlist[i] = pageEdit.Get_ele.Get_image_data(ele);
        }
        eleList["length"] = Items.length;
        eleList["pagecode"] = $("#EditPageCode").val();
        eleList["kitlist"] = pageEdit.tools.jsontostring(kitlist);
        eleList["timestap"] = Date.UTC;
        if (eleList.pagecode.length < 1) {
            layer.msg("请选择编辑页面");
            return false;
        }
        // console.log(pageEdit.tools.jsontostring(eleList));
        // var bgimg = $("#iphone").css("backgroundImage"); //获取当前场景背景图片
        // bgimg = bgimg.substring(5, bgimg.length - 2); //获取背景图片路径 如：aa/bb/abc.jpg
        // var pageconfig = "{\"bgimg\":\"" + bgimg +
        //     "\"" +
        //     ",\"imglist\":" +
        //    +
        //     "}"; //当前页面配置文件 ：1. 背景图片 2.所有图片元素
        // console.log(eleList);
        // layer.alert(pageEdit.tools.jsontostring(eleList));
        $.post(myPath + "Api/EditPageSaveKitsActionResult", eleList,
            function (res, text) {
                //console.log(res);
                if (res.Result > 0) {
                    layer.msg(res.Msg);
                } else {
                    layer.msg(res.Msg);
                }
            });

    },
    Get_ele: {
        Get_image_data: function (e) { //获取图片组件配置信息
            var $e = $(e);
            var deg = $e.find("img").css('transform');
            //console.log(deg);
            var imgData = {
                "etype": $e.attr("ptype"),
                "Kid": $e.attr("id"),
                "link": $e.attr("link"),
                "effect": $e.attr("swiper-animate-effect"),
                "duration": $e.attr("swiper-animate-duration"),
                "delay": $e.attr("swiper-animate-delay"),
                "img": $e.find("img").attr("src"),
                "angleZ": pageEdit.tools.getmatrixtodeg(deg),
                "style": {
                    "top": $e.css("top"),
                    "left": $e.css("left"),
                    "width": $e.css("width"),
                    "height": $e.css("height")
                }
            };
            return imgData;
        }
    },
    //返回当前编辑组件
    getEditKit: function () {
        return $("#" + localStorage.getItem("KitId"));
    },
    //页面初始化加载
    GetPageList: function () {
        var appdata = {
            appCode: $("#AppCode").val()
        }
        $.getJSON("../../Api/GetPageList", appdata,
            function (res) {
                if (res.Result > 0) {
                    var json = $.parseJSON(res.Msg);
                    //console.log(json);
                    $(".pagemanange ul li").remove();
                    var EditPageCode = $("#EditPageCode").val();
                    for (var i = 0; i < json.pagelist.length; i++) {
                        $('<li value="' + json.pagelist[i].pagecode + '"><b class="btn-del btn-m" t="del"></b><b class="btn-up btn-m" t="up"></b><b class="btn-down btn-m " t="down"></b>' + "<span class='layui-icon'>&#xe60a;" + json.pagelist[i].name + '</span></li>').appendTo(".pagemanange ul").addClass("list-group-item");
                    }
                    
                    $($(".pagemanange ul li[value='" + EditPageCode + "']")).addClass("select").siblings().removeClass("select");
                    //页面选择
                    $(".pagemanange ul li").bind("click",
                        function () {
                            layer.msg("页面加载中。。。。");
                            $(this).addClass("select").siblings().removeClass("select");
                            $("#EditPageCode").val($(this).attr("value"));
                            appdata["pagecode"] = $(this).attr("value");

                            $("#iphone").html("");
                            $.post(myPath + "Api/GetAppPageKit", appdata,
                                function (res) {
                                    for (var j = 0; j < res.length; j++) {
                                        var img = res[j].KitContent;
                                       // img = "<img src=../../1.jpg>";
                                        var img0 = img.substring(img.indexOf("=") + 1, img.length - 1);

                                        img = "<img style='transform:rotate(" + res[j].KitAngleZ + "deg)' src=\"" + img0 + "\">";
                                       // console.log(img);
                                        var imgcontrol = "<div class='imgview ani' swiper-animate-effect='" + res[j].KitAnEffect +
                                            "' swiper-animate-duration='" + res[j].KitAnDuration + "s' swiper-animate-delay='" + res[j].KitAnDelay + "s' " +
                                            " Ptype='imgview' link='" + res[j].KitLinkType + "|" + res[j].KitLinkUrl + "'" +
                                            " id='" + res[j].ID + "' style='left:" + res[j].KitLeft + ";top:" + res[j].KitTop + ";z-index:1;width:" + res[j].KitWidth + ";height:" + res[j].KitHeight +
                                            "'>" + img + "</div >";

                                        pageEdit.AddImgAction(imgcontrol);
                                    }
                                    layer.closeAll();
                                });
                        });
                    $(".pagemanange ul li .btn-m").bind("click",
                        function () {
                            var data = {
                                type: $(this).attr("t"),
                                pagecode: $(this).parent().attr("value"),
                                appcode: appdata.appCode
                            }
                            if (data.type == "del") {
                                layer.confirm("纳尼？确认要删除这个页面吗？", {
                                    yes: function (index) {
                                        $.getJSON("../../Api/editAppPageBase", data,
                                            function (res) {
                                                if (res.Result > 0) {
                                                    pageEdit.GetPageList();
                                                    layer.close(index);
                                                    layer.msg(res.Msg);
                                                } else {
                                                    layer.alert(res.Msg);
                                                }
                                            });
                                    }
                                });
                                return false;
                            } else {

                                $.getJSON("../../Api/editAppPageBase", data,
                                    function (res) {
                                        if (res.Result > 0) {
                                            layer.msg(res.Msg);
                                            pageEdit.GetPageList();

                                        } else {
                                            layer.alert(res.Msg);
                                        }
                                    });
                                return false;
                            }

                            // alert(1);
                        });
                } else {
                    layer.alert(res.Msg);
                }

            });
    },
    //添加新页面
    AddPage: function (pageCode) {
        layer.prompt({
            title: '给我取很好的名字，方便明确本页主题',
            formType: 0
        },
            function (text, index) {
                if (text.length < 1) {
                    text = "无标题";
                }
                var Createdata = {
                    appCode: pageCode,
                    PageName: text
                }
                $.getJSON("../../Api/CreateAppPage", Createdata,
                    function (res) {
                        if (res.Result > 0) {
                            layer.msg(res.Msg);
                            layer.close(index);
                            pageEdit.GetPageList();
                        } else {
                            layer.msg(res.Msg);
                            layer.close(index);
                        }
                    });
            });
    },loadAppBase: function() {
        //编辑页面初始化加载
        pageEdit.changebg($("#Appbg").val());
    }
};

//预览编辑元素动画
var ChangeSwiper = {
    swiperanimateeffect: function (x, obj) { //切换效果，例如 fadeInUp
        $(obj).attr("swiper-animate-effect", x);
        $(obj).removeClass().addClass(x + " imgview ani active");
        swiperload();
    },
    swiperanimateduration: function (x, obj) { //可选，动画持续时间（单位秒），例如 0.5s
        $(obj).attr("swiper-animate-duration", x + "s");
        $(obj).removeClass().addClass($("#animationsset").value + " imgview ani active");
        swiperload();
    },
    swiperanimatedelay: function (x, obj) { //可选，动画延迟时间（单位秒），例如 0.3s
        $(obj).attr("swiper-animate-delay", x + "s");
        $(obj).removeClass().addClass($("#animationsset").value + " imgview ani active");
        swiperload();
    }
}

//组件右键菜单
var editmenu = function (obj) {
    localStorage.setItem("menuid", obj);
}

/*页面加载时 初始化加载 操作*/
$(function () {
    // swiperload();
    localStorage.removeItem("KitId");
    localStorage.removeItem("menuid");
    //组件操作
    $("#animationsset").change(function (e) { //动画持续时间
        e.preventDefault();
        ChangeSwiper.swiperanimateeffect($(this).val(), $("#" + localStorage.getItem("KitId")));
    });
    $("#swiperanimateduration").change(function (e) { //动画持续时间
        e.preventDefault();
        ChangeSwiper.swiperanimateduration($(this).val(), $("#" + localStorage.getItem("KitId")));
    });
    $("#swiperanimatedelay").change(function (e) { //动画延迟执行时间
        e.preventDefault();
        ChangeSwiper.swiperanimatedelay($(this).val(), $("#" + localStorage.getItem("KitId")));
    });

    $(".pagemanange ul li").append(loading);

    pageEdit.GetPageList();
    pageEdit.loadAppBase();

    appCode = $("#AppCode").val();
    //$(".pagemanange ul li").find("p").remove();删除加载 
    context.init({
        fadeSpeed: 100,
        filter: function ($obj) { },
        above: 'auto',
        preventDoubleContext: true,
        compress: false
    });
    //组件右键菜单
    context.attach("#iphone div", [{
        text: '向上移动一层',
        action: function (e) {
            var zindex = $(localStorage.getItem("menuid")).css("z-index");
            if (zindex < 1) {
                zindex = 1;
            } else {
                zindex = 1;
            }
            //console.log(zindex);
            $(localStorage.getItem("menuid")).css("z-index", parseInt(zindex) + 1);
            // alert("AA");
        }
    },
    {
        text: '向下移动一层',
        action: function (e) {
            $(localStorage.getItem("menuid")).css("z-index", $(localStorage.getItem("menuid")).css("z-index") - 1);
        }
    }
        ,
    {
        text: '上顶层',
        action: function (e) {
            $(localStorage.getItem("menuid")).css("z-index", 9999);
        }
    }

        ,
    {
        text: '下低层',
        action: function (e) {
            $(localStorage.getItem("menuid")).css("z-index", 0);
        }
    }
        ,
    {
        text: '删除',
        action: function (e) {
            layer.confirm("确定要删除这个组件吗？",
                function () {
                    pageEdit.delimg(localStorage.getItem("KitId"));
                });
        }
    },
    {
        text: '关闭',
        action: function (e) {
            e.preventDefault();
        }
    }]);
    /*
    滑动模块
    */
    var handle = $("#swiperanimateangle");
    $("#sliderangle").slider({
        range: "min",
        max: 360,
        value: 0,
        //    create: function () {
        //        handle.text($(this).slider("value"));
        //    },
        slide: function (event, ui) {
            handle.text(ui.value);
            var angle = "rotateZ(" + ui.value + "deg)";
            $("#" + localStorage.getItem("KitId") + " img").css({
                "transform": angle,
                "-ms-transform": angle,
                /* Internet Explorer */
                "-moz-transform": angle,
                /* Firefox */
                "-webkit-transform": angle,
                /* Safari 和 Chrome */
                "-o-transform": angle
                /* Opera */
            });
            localStorage.setItem("#" + localStorage.getItem("KitId") + "rotateZ", angle);
            //   swiperload();
        }
    });
    //sliderangle
    $(".control-link div button").click(function (e) {
        var showindex = $(this).attr("s");
        $(this).removeClass("layui-btn-primary").siblings().addClass("layui-btn-primary");
        $("#linktype" + showindex).show().siblings().hide();
        if ("0" == showindex) { //切换到连接到当前场景
            $("#linktype" + showindex + " select option").remove();
            var appdata = {
                appCode: $("#AppCode").val()
            }
            $.getJSON("../../Api/GetPageList", appdata,
                function (res) {
                    $("#linktype" + showindex + " select").append('<option value="0">请选择当前场景</option>');
                    if (res.Result > 0) {
                        var json = $.parseJSON(res.Msg);
                        for (var i = 0; i < json.pagelist.length; i++) {
                            $('<option value="' + json.pagelist[i].pagecode + '">' + json.pagelist[i].name + '</option>').appendTo("#linktype" + showindex + " select");
                        }
                        $("#linktype" + showindex + " select").change(function () {
                            var setlink = $(pageEdit.getEditKit()).attr("link", "0|" + $(this).val());
                            //  console.log(setlink);
                            layer.msg("已设置场景：");
                        });
                    } else layer.alert(res.Msg);
                });
        } else {
            $("#linktype" + showindex).find("input").val("");
            $("#linktype" + showindex + " input").change(function () {
                if (!Reg.internetUrl($(this).val())) {
                    layer.msg("链接格式不正确");
                    this.value = "";
                    return false;
                } else {
                    var setlink = $(pageEdit.getEditKit()).attr("link", "1|" + $(this).val());
                    layer.msg("已设置连接为:" + $(this).val());
                    // console.log(setlink);
                }
            });
        }
    });
    $("#iphone").click(function (event) {
        event.stopPropagation();
    });
    $(".swiper-wrapper ").click(function () { //释放选中
        // layer.msg("空白地方点击:释放");
        $(".control-animations,.control-duration,.control-delay,.control-angle,.control-imgupload,.control-link").hide(500); //展开关闭
        $("#" + localStorage.getItem("KitId")).removeClass("active");
    });
});