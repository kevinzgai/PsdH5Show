using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Psd.H5Show.Common.DEncrypt;
using Psd.H5Show.Model;
using Shell32;
using user_account = Psd.H5Show.Model.user_account;
using System.Web.Script.Serialization;

namespace PsdH5ShowWebApp.Controllers
{
    using System.Text;

    public class ApiController : Controller
    {
        Psd.H5Show.Model.Tools.Messge Msg = new Psd.H5Show.Model.Tools.Messge();
        // GET: Api
        public ActionResult Index()
        {
            return View();
        }

        // GET: Api
        [HttpGet]
        public ActionResult GetTemplatBgList()
        {
            string path = Server.MapPath("/template/bg/");
            DirectoryInfo di = new DirectoryInfo(path);
            //找到该目录下的文件 
            FileInfo[] fis = di.GetFiles();
            string fileList = "{\"bglist\":[";
            foreach (FileInfo fi in fis)
            {
                // Response.Write(fi.Name);
                // Response.Write("<br>");
                if (fi.Name.IndexOf("_thumb") > 1)
                {
                    fileList += "\"" + fi.Name + "\",";
                }
            }
            fileList = fileList.Substring(0, fileList.Length - 1);
            fileList += "]}";
            return Content(fileList);
        }

        [HttpGet]
        public ActionResult GetTemplateMp3List()
        {
            string path = "~/template/mp3/";
            DirectoryInfo di = new DirectoryInfo(Server.MapPath(path));
            //找到该目录下的文件 
            FileInfo[] fis = di.GetFiles();
            String mp3data = "{\"mp3list\":[";
            foreach (FileInfo fi in fis)
            {
                mp3data += getmp3info(path + fi.Name) + ",";
            }

            mp3data = mp3data.Substring(0, mp3data.Length - 1) + "]}";
            // mp3data = mp3data.Replace("站长素材(sc.chinaz.com)", "票时代（www.5umovie.com）");
            return Content(mp3data);
        }

        /// <summary>
        /// 获取系统背景音乐列表 mp3path.
        /// </summary>
        /// <param name="mp3path">The mp3path.</param>
        /// <returns></returns>
        public string getmp3info(string mp3path)
        {
            string strMp3 = mp3path;
            string mp3InfoInterHtml = "{";
            ShellClass sh = new ShellClass();
            Folder dir = sh.NameSpace(Path.GetDirectoryName(Server.MapPath(strMp3)));
            FolderItem item = dir.ParseName(Path.GetFileName(Server.MapPath(strMp3)));
            mp3InfoInterHtml += "\"filename\":\"" + dir.GetDetailsOf(item, 0) + "\",";
            mp3InfoInterHtml += "\"musicsize\":\"" + dir.GetDetailsOf(item, 1) + "\",";
            mp3InfoInterHtml += "\"musicname\":\"" + dir.GetDetailsOf(item, 21) + "\",";
            mp3InfoInterHtml += "\"singer\":\"" + dir.GetDetailsOf(item, 13) + "\",";
            mp3InfoInterHtml += "\"album\":\"" + dir.GetDetailsOf(item, 14) + "\",";
            mp3InfoInterHtml += "\"duration\":\"" + dir.GetDetailsOf(item, 27) + "\"";
            mp3InfoInterHtml += "}";
            return mp3InfoInterHtml;
        }

        #region 创建App操作               

        /// <summary>
        /// 创建app页面
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        /// <param name="appCode">The application code.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateAppPage(string PageName, string appCode)
        {
            try
            {
                appCode = DESEncrypt.Decrypt(appCode);
            }
            catch (Exception)
            {
                Msg.Result = 0;
                Msg.Msg = "不要乱修改信息嘛，不然我会中毒的";
                throw;
            }
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (Request.Cookies["PsdH5ShowUserCode"] == null)
            {

                Msg.Result = 0;
                Msg.Msg = "哎哟，你还未登录呢";
            }
            else
            {
                user_account userAccount =
                    Psd.H5Show.BLL.AppTools.GetUserCodeToUseraccountModel(
                        Request.Cookies["PsdH5ShowUserCode"].Value.ToString());
                if (userAccount != null)
                {
                    List<apppagebase> appPageBasesList =
                        new Psd.H5Show.BLL.apppagebase().GetModelList(" Appid='" + appCode +
                                                                      "' and DELFLAG=0 order by orderby");
                    int maxindex = 0;
                    foreach (apppagebase appPageBases in appPageBasesList)
                    {
                        if (maxindex < appPageBases.OrderBy)
                            maxindex = appPageBases.OrderBy;
                    }

                    Psd.H5Show.Model.apppagebase appPageBase = new Psd.H5Show.Model.apppagebase();
                    appPageBase.AppId = int.Parse(appCode);
                    appPageBase.CreateTime = DateTime.Now;
                    appPageBase.Title = PageName;
                    appPageBase.OrderBy = maxindex + 1;
                    if (new Psd.H5Show.BLL.apppagebase().Add(appPageBase))
                    {
                        Msg.Result = 1;
                        Msg.Msg = "哎哟，已经为您创建了新的页面，快去发挥您的洪荒之力吧！";
                    }
                    else
                    {
                        Msg.Result = 0;
                        Msg.Msg = "报告大王，页面创建失败!";
                    }
                }
            }
            json.Data = Msg;
            return json;
        }

        /// <summary>
        /// Deletes the application page.
        /// </summary>
        /// <param name="appCode">The application code.</param>
        /// <param name="pagecode">The pagecode.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult editAppPageBase(string appCode, string pagecode, string type)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (Request.Cookies["PsdH5ShowUserCode"] == null)
            {

                Msg.Result = 0;
                Msg.Msg = "哎哟，你还未登录呢";
            }
            else
            {

                pagecode = DESEncrypt.Decrypt(pagecode);
                appCode = DESEncrypt.Decrypt(appCode);
                user_account userAccount =
                    Psd.H5Show.BLL.AppTools.GetUserCodeToUseraccountModel(
                        Request.Cookies["PsdH5ShowUserCode"].Value.ToString());
                if (userAccount != null)
                {
                    switch (type)
                    {
                        case "del":
                            apppagebase appPageBase = new Psd.H5Show.BLL.apppagebase().GetModel(int.Parse(pagecode));
                            appPageBase.DelFlag = true;
                            if (new Psd.H5Show.BLL.apppagebase().Update(appPageBase))
                            {
                                List<apppagebase> appPageBasesList =
                                    new Psd.H5Show.BLL.apppagebase().GetModelList(" Appid='" + appCode +
                                                                                  "' and DELFLAG=0 order by orderby");
                                foreach (apppagebase appPageBases in appPageBasesList)
                                {
                                    if (appPageBases.OrderBy >= appPageBase.OrderBy)
                                    {
                                        appPageBases.OrderBy = appPageBases.OrderBy - 1;
                                        new Psd.H5Show.BLL.apppagebase().Update(appPageBases);
                                    }
                                }
                                Msg.Result = 1;
                                Msg.Msg = "欧耶，页面删除成功";
                            }
                            else
                            {
                                Msg.Result = 0;
                                Msg.Msg = "哎哟，页面删除失败咯";
                            }
                            break;
                        case "up":
                            Msg.Result = 1;
                            Msg.Msg = "欧耶，向上移动";
                            changepage(appCode, pagecode, -1);
                            break;
                        case "down":
                            Msg.Result = 1;
                            Msg.Msg = "欧耶，向下移动";
                            changepage(appCode, pagecode, 1);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Msg.Result = 0;
                    Msg.Msg = "哎哟，获取用户发生错误";
                }
            }
            json.Data = Msg;
            return json;
        }

        /// <summary>
        /// 获取app页面列表
        /// </summary>
        /// <param name="appCode">The application code.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageList(string appCode)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            appCode = DESEncrypt.Decrypt(appCode);
            if (appCode != null)
            {
                apppagebase appPageBase = new apppagebase();
                DataTable dataTable =
                    new Psd.H5Show.BLL.apppagebase().GetList(" appid='" + appCode + "' and DELFLAG=0 order by orderby")
                        .Tables[0];

                if (dataTable.Rows.Count > 0)
                {
                    string result = "{\"pagelist\": [";
                    Msg.Result = 1;
                    foreach (DataRow datarow in dataTable.Rows)
                    {
                        string str = "{\"name\": \"@name\",\"pagecode\": \"@pagecode\",\"index\": \"@index\"},";
                        str = str.Replace("@name", datarow["TITLE"].ToString());
                        str = str.Replace("@pagecode", DESEncrypt.Encrypt(datarow["ID"].ToString()));
                        str = str.Replace("@index", datarow["orderby"].ToString());
                        result += str;
                    }
                    result = result.Substring(0, result.Length - 1);
                    result += "]}";
                    Msg.Msg = result;
                }
                else
                {
                    Msg.Result = 0;
                    Msg.Msg = "无页面，请添加";


                }


            }
            else
            {
                Msg.Result = 0;
                Msg.Msg = "appCode错误";
            }
            json.Data = Msg;
            return json;
        }

        #endregion

        #region  页面操作

        /// <summary>
        /// 改变页面出场顺序
        /// </summary>
        /// <param name="appCode">appid</param>
        /// <param name="pagecode">页面id</param>
        /// <param name="Todo">-1 向上移动 1向下移动</param>
        /// <returns></returns>
        public bool changepage(string appCode, string pagecode, int Todo)
        {
            List<apppagebase> appPageBasesList =
                new Psd.H5Show.BLL.apppagebase().GetModelList(" Appid='" + appCode + "' and DELFLAG=0 order by orderby");
            int minindex = 0;
            int maxindex = 0;
            bool isgo = true;
            foreach (apppagebase appPageBases in appPageBasesList)
            {
                if (minindex > appPageBases.OrderBy)
                    minindex = appPageBases.OrderBy;
                if (maxindex < appPageBases.OrderBy)
                    maxindex = appPageBases.OrderBy;
            }

            for (int i = 0; i < appPageBasesList.Count; i++)
            {
                if (appPageBasesList[i].ID.ToString().Equals(pagecode))
                {
                    appPageBasesList[i].OrderBy = appPageBasesList[i].OrderBy + Todo; //防止错误操作
                    if (appPageBasesList[i].OrderBy <= minindex) //最小的时候判断
                    {
                        isgo = false;
                    }
                    else if (appPageBasesList[i].OrderBy > maxindex) //最大的时候判断
                    {
                        isgo = false;
                    }
                    if (isgo)
                    {
                        new Psd.H5Show.BLL.apppagebase().Update(appPageBasesList[i]);
                        if (Todo > 0)
                        {
                            //下调
                            appPageBasesList[i + 1].OrderBy = appPageBasesList[i + 1].OrderBy - 1;
                            new Psd.H5Show.BLL.apppagebase().Update(appPageBasesList[i + 1]);
                        }
                        else
                        {
                            //上调
                            appPageBasesList[i - 1].OrderBy = appPageBasesList[i - 1].OrderBy + 1;
                            new Psd.H5Show.BLL.apppagebase().Update(appPageBasesList[i - 1]);
                        }
                    }
                }

            }
            return isgo;
        }

        /// <summary>
        /// 获取页面所有元素.
        /// </summary>
        /// <param name="pagecode">页面编号</param>
        /// <param name="appcode">App编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAppPageKit(string pagecode, string appcode)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            List<apppageinfo> AppPageKitList = new List<apppageinfo>();
            user_account userAccount = new user_account();
            apppagebase appPageBase = new apppagebase();
            appbase appBase = new appbase();
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            pagecode = DESEncrypt.Decrypt(pagecode);
            if (pagecode == null)
            {
                Msg.Result = 0;
                Msg.Msg = "错误编码";
            }
            else
            {
                appPageBase = new Psd.H5Show.BLL.apppagebase().GetModel(int.Parse(pagecode));
                appBase = new Psd.H5Show.BLL.appbase().GetModel(appPageBase.AppId);
                if (Request.Cookies["PsdH5ShowUserCode"] == null)
                {

                    Msg.Result = 0;
                    Msg.Msg = "哎哟，你还未登录呢";
                }
                else
                {
                    userAccount =
                        Psd.H5Show.BLL.AppTools.GetUserCodeToUseraccountModel(
                            Request.Cookies["PsdH5ShowUserCode"].Value.ToString());
                    if (!userAccount.ID.Equals(appBase.Uid))
                    {
                        Msg.Result = 0;
                        Msg.Msg = "无权限编辑此页面";
                    }
                    else
                    {
                        AppPageKitList = new Psd.H5Show.BLL.apppageinfo().GetModelList(" Pageid='" + pagecode + "'");

                        json.Data = AppPageKitList;
                    }
                }
            }
            return json;
        }

        #endregion


        #region 页面元素操作 重要核心功能

        /// <summary>
        /// 修改页面基本信息
        /// </summary>
        /// <param name="t">操作类型.</param>
        /// <param name="val">The value.</param>
        /// <param name="appcode">The appcode.</param>
        /// <returns></returns>
        public ActionResult EditAppBase(string t, string val, string appcode)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                appbase appBase = GetAppbase(appcode);
                switch (t)
                {
                    case "bg":
                        appBase.BgImg = val;
                        break;
                    case "mp3":
                        appBase.BgMusic = val;
                        break;

                    default:
                        Msg.Result = 0;
                        Msg.Msg = "错误操作";
                        break;
                }
                if (new Psd.H5Show.BLL.appbase().Update(appBase))
                {
                    Msg.Result = 1;
                    Msg.Msg = "ok";
                }
                else
                {
                    Msg.Result = 0;
                    Msg.Msg = "更新失败";
                }
            }
            catch (Exception es)
            {
                Msg.Result = 0;
                Msg.Msg = es.ToString();
                throw;
            }
            json.Data = Msg;
            return json;
        }

        /// <summary>
        /// 保存页面结果
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="pagecode">The pagecode.</param>
        /// <param name="kitlist">The kitlist.</param>
        /// <returns></returns>
        public ActionResult EditPageSaveKitsActionResult(string length, string pagecode, string kitlist)
        {
            kitlist = "{\"KitList\":" + kitlist + "}";
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Psd.H5Show.Model.Tools.kitmodel pageKitModel = ser.Deserialize<Psd.H5Show.Model.Tools.kitmodel>(kitlist);
            foreach (Psd.H5Show.Model.Tools.KitList pageKit in pageKitModel.KitList)
            {
                apppageinfo AppPageInfo = new Psd.H5Show.BLL.apppageinfo().GetModel(int.Parse(pageKit.Kid));
                AppPageInfo.KitAnDelay = double.Parse(pageKit.delay.Substring(0, pageKit.delay.Length - 1)); //运行时间
                AppPageInfo.KitAnDuration = double.Parse(pageKit.duration.Substring(0, pageKit.duration.Length - 1));
                string lnk = pageKit.link;
                string[] link = lnk.Split('|');
                AppPageInfo.KitLinkType = int.Parse(link[0]);
                AppPageInfo.KitAnEffect = pageKit.effect;
                AppPageInfo.KitLinkUrl = link[1];
                AppPageInfo.KitAngleZ = pageKit.angleZ.ToString();
                AppPageInfo.KitContent = "<img src=" + pageKit.img + ">";
                AppPageInfo.KitWidth = pageKit.style.width;
                AppPageInfo.KitHeight = pageKit.style.height;
                AppPageInfo.KitLeft = pageKit.style.left;
                AppPageInfo.KitTop = pageKit.style.top;
                if (new Psd.H5Show.BLL.apppageinfo().Update(AppPageInfo))
                {
                    Msg.Result = 1;
                    Msg.Msg = "哟嚯！保存成功，赶紧预览下看看您的作品吧！";
                }
                else
                {
                    Msg.Result = 0;
                    Msg.Msg = "哎哟，竟然保存失败了，您可以Ctrl+F5刷新下 再看看吧";
                }
            }
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;


            jsonResult.Data = Msg;
            return jsonResult;
        }
        [HttpPost]
        public ActionResult EditPageBgimgActionResult(string appcode, string t)
        {
            string aFirstName = t.Substring(t.LastIndexOf("/") + 1, (t.LastIndexOf(".") - t.LastIndexOf("/") + 3)); //文件名
            string iFilePage = string.Format("~/upload/{0}/appbg/{1}", appcode, aFirstName);
            iFilePage = this.Server.MapPath(iFilePage);
            FileInfo f = new FileInfo(iFilePage);
            try
            {
                f.Delete();
                this.Msg.Result = 1;
                this.Msg.Msg = "删除成功";
            }
            catch
            {
                this.Msg.Result = 0;
                this.Msg.Msg = "删除失败，请稍后重试";
            }
            return Json(this.Msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建组件操作
        /// </summary>
        /// <param name="pagecode">页面编码.</param>
        /// <param name="t">创建控件类型.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPageCreateKitActionResult(string pagecode, string t)
        {
            user_account userAccount = new user_account();
            apppagebase appPageBase = new apppagebase();
            appbase appBase = new appbase();
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            pagecode = DESEncrypt.Decrypt(pagecode);
            if (pagecode == null)
            {
                Msg.Result = 0;
                Msg.Msg = "错误编码";
            }
            else
            {
                appPageBase = new Psd.H5Show.BLL.apppagebase().GetModel(int.Parse(pagecode));
                appBase = new Psd.H5Show.BLL.appbase().GetModel(appPageBase.AppId);
                if (Request.Cookies["PsdH5ShowUserCode"] == null)
                {

                    Msg.Result = 0;
                    Msg.Msg = "哎哟，你还未登录呢";
                }
                else
                {
                    userAccount =
                        Psd.H5Show.BLL.AppTools.GetUserCodeToUseraccountModel(
                            Request.Cookies["PsdH5ShowUserCode"].Value.ToString());
                    if (!userAccount.ID.Equals(appBase.Uid))
                    {
                        Msg.Result = 0;
                        Msg.Msg = "无权限编辑此页面";
                    }
                    else
                    {


                        switch (t)
                        {
                            case "image": //图片类型  图片控件初始化加载 配置

                                apppageinfo appPageInfo = new apppageinfo();
                                appPageInfo.PageId = int.Parse(pagecode);
                                appPageInfo.KitAnDelay = 0.5;
                                appPageInfo.KitAnDuration = 0.5;
                                appPageInfo.KitAnEffect = "fadeInUp";
                                appPageInfo.KitAngleZ = "0";
                                appPageInfo.KitLeft = "0px";
                                appPageInfo.KitTop = "0px";
                                appPageInfo.KitType = t;
                                appPageInfo.KitStyle = "";
                                appPageInfo.KitLinkUrl = "";
                                appPageInfo.KitLinkType = 0;
                                appPageInfo.KitWidth = "auto";
                                appPageInfo.KitHeight = "auto";
                                appPageInfo.KitContent = "<img src=\"../../content/image/demo.png\">";
                                appPageInfo.ID = Psd.H5Show.BLL.AppTools.getMaxIdToTable("apppageinfo") + 1;
                                var imgcontrol =
                                    "<div class='imgview ani' swiper-animate-effect='" + appPageInfo.KitAnEffect +
                                    "' swiper-animate-duration='" + appPageInfo.KitAnDuration +
                                    "s' swiper-animate-delay='" + appPageInfo.KitAnDelay + "s' width='" +
                                    appPageInfo.KitWidth + "' " +
                                    " Ptype='imgview' link='" + appPageInfo.KitLinkType + "|" + appPageInfo.KitLinkUrl +
                                    "' id='" + appPageInfo.ID.ToString() + "' style='style='left:" + appPageInfo.KitTop +
                                    ";top:" + appPageInfo.KitTop +
                                    ";z-index:1'>" + appPageInfo.KitContent + "</div >";
                                try
                                {
                                    new Psd.H5Show.BLL.apppageinfo().Add(appPageInfo);
                                    List<apppageinfo> KitList =
                                        new Psd.H5Show.BLL.apppageinfo().GetModelList(" PageId='" + pagecode + "'");
                                    Msg.Result = 1;
                                    Msg.Msg = imgcontrol;
                                }
                                catch (Exception es)
                                {
                                    Msg.Msg = es.ToString();
                                    throw;
                                }


                                break;
                            default:
                                break;
                        }
                    }
                }

            }
            jsonResult.Data = Msg;
            return jsonResult;
        }


        /// <summary>
        /// 创建组件操作
        /// </summary>
        /// <param name="pagecode">页面编码.</param>
        /// <param name="KitId">组件id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPageDelKitActionResult(string pagecode, string kitId)
        {
            user_account userAccount = new user_account();
            apppagebase appPageBase = new apppagebase();
            appbase appBase = new appbase();
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            pagecode = DESEncrypt.Decrypt(pagecode);
            if (pagecode == null)
            {
                Msg.Result = 0;
                Msg.Msg = "错误编码";
            }
            else
            {
                appPageBase = new Psd.H5Show.BLL.apppagebase().GetModel(int.Parse(pagecode));
                appBase = new Psd.H5Show.BLL.appbase().GetModel(appPageBase.AppId);
                if (Request.Cookies["PsdH5ShowUserCode"] == null)
                {

                    Msg.Result = 0;
                    Msg.Msg = "哎哟，你还未登录呢";
                }
                else
                {
                    userAccount =
                        Psd.H5Show.BLL.AppTools.GetUserCodeToUseraccountModel(
                            Request.Cookies["PsdH5ShowUserCode"].Value.ToString());
                    if (!userAccount.ID.Equals(appBase.Uid))
                    {
                        Msg.Result = 0;
                        Msg.Msg = "无权限编辑此页面";
                    }
                    else
                    {
                        if (new Psd.H5Show.BLL.apppageinfo().Delete(int.Parse(kitId)))
                        {
                            Msg.Result = 1;
                            Msg.Msg = "删除成功";
                        }
                        else
                        {
                            Msg.Result = 0;
                            Msg.Msg = "删除失败";
                        }


                    }
                }
                jsonResult.Data = Msg;
            }
            return jsonResult;
        }


        /// <summary>
        /// Gets the appbase.
        /// </summary>
        /// <param name="appcode">The appcode.</param>
        /// <returns></returns>
        public appbase GetAppbase(string appcode)
        {
            appcode = DESEncrypt.Decrypt(appcode);
            return new Psd.H5Show.BLL.appbase().GetModel(int.Parse(appcode));
        }
        #endregion
    }
}