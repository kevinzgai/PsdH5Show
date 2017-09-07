using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Psd.H5Show.BLL;
using Psd.H5Show.Common.DEncrypt;
using appbase = Psd.H5Show.Model.appbase;
using user_account = Psd.H5Show.Model.user_account;

namespace PsdH5ShowWebApp.Controllers
{
    using Shell32;
    using System.IO;

    public class EditController : Controller
    {
        // GET: Edit
        public ActionResult Index(string id)
        {
            appbase appbase = new appbase();
            user_account userAccount = new user_account();
            if (Request.Cookies["PsdH5ShowUserCode"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            if (id != null)
            {
                try
                {
                    id = DESEncrypt.Decrypt(id);
                    if (id != null)
                    {
                        userAccount = AppTools.GetUserCodeToUseraccountModel(
                      Request.Cookies["PsdH5ShowUserCode"].Value);
                        if (!isMemberApp(userAccount.UserCode, int.Parse(id)))
                        {
                            return RedirectToAction("Index", "Error", new { id = 404 ,msg="无权限请求"});
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Error", new { id = 404 ,msg="错误请求"});
                    }
                  
                    //判断操作权限

                }
                catch (Exception es)
                {
                    return RedirectToAction("Index", "Error", new { id = 404, msg = es.ToString() });
                    throw;
                }

                appbase = new Psd.H5Show.BLL.appbase().GetModel(int.Parse(id));
            }
            else
            {
                return RedirectToAction("Applist", "Admin");
            }
            ViewBag.appCode = DESEncrypt.Encrypt(appbase.ID.ToString());
            ViewBag.AppBase = appbase;
            //Response.Write(id);
            return View();
        }

        // GET: Edit
        public ActionResult AppBgPage(string id)
        {
            string path = Server.MapPath("/template/bg/");
            string diypath = Server.MapPath("/upload/"+id+ "/appbg");
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo appbg = new DirectoryInfo(diypath);
            Directory.CreateDirectory(diypath);
            //找到该目录下的文件 
            FileInfo[] fis = di.GetFiles();
            ViewBag.ImgList = fis;
            ViewBag.AppImgList = appbg.GetFiles();
            ViewBag.AppCode = id;

            return View();
        }

        // GET: Edit
        public ActionResult AppMp3Page(string id)
        {
            string  syspath = Server.MapPath("/template/mp3/");
            string diypath = Server.MapPath("/upload/" + id + "/mp3");
            getmp3info("/template/mp3/");

            ShellClass sh = new ShellClass();
            Folder dir = sh.NameSpace(Path.GetDirectoryName(diypath));
            FolderItem item = dir.ParseName(Path.GetFileName(diypath));
            DirectoryInfo di = new DirectoryInfo(syspath);
            DirectoryInfo appbg = new DirectoryInfo(diypath);
            Directory.CreateDirectory(diypath);
            //找到该目录下的文件 
            FileInfo[] fis = di.GetFiles();
            ViewBag.ImgList = fis;
            ViewBag.AppImgList = appbg.GetFiles();
            ViewBag.AppCode = id;

            return View();
        }


        /// <summary>
        /// 获取音乐详情.
        /// </summary>
        /// <param name="mp3path">The mp3path.</param>
        /// <returns></returns>
        public Psd.H5Show.Model.Tools.MusicInfo getmp3info(string mp3path,string filename)
        {
            Psd.H5Show.Model.Tools.MusicInfo musicInfoModel = new Psd.H5Show.Model.Tools.MusicInfo();
            ShellClass sh = new ShellClass();
            Folder dir = sh.NameSpace(Path.GetDirectoryName(Server.MapPath(mp3path)));
            foreach (FolderItem item in dir)
            {

            }
            FolderItem item = dir.ParseName(Path.GetFileName(Server.MapPath(filename)));
            musicInfoModel.filename = dir.GetDetailsOf(item, 0);
            musicInfoModel.musicsize = dir.GetDetailsOf(item, 1);
            musicInfoModel.musicname = dir.GetDetailsOf(item, 21);
            musicInfoModel.singer = dir.GetDetailsOf(item, 13);
            musicInfoModel.album = dir.GetDetailsOf(item, 14);
            musicInfoModel.duration = dir.GetDetailsOf(item, 27);
            return musicInfoModel;
        }
        /// <summary>
        /// 判断是否有权限编辑此app
        /// </summary>
        /// <param name="userCode">The user code.</param>
        /// <param name="appId">The application id.</param>
        /// <returns>
        ///   <c>true</c> if [is member application] [the specified user code]; otherwise, <c>false</c>.
        /// </returns>
        public bool isMemberApp(string userCode, int appId)
        {
            bool isauth = false;
            DataSet dataSet =
                new Psd.H5Show.BLL.user_account().GetList("DELFLAG=0 and DELFLAG = 0 and USERCODE = '" + userCode + "'");
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                if (new Psd.H5Show.BLL.appbase().GetList("appbase.DELFLAG=0  and  appbase.ID=" + appId + " and UID=" +
                                                         dataSet.Tables[0].Rows[0]["ID"]).Tables[0].Rows.Count > 0)
                {
                    isauth = true;
                }
            }

            return isauth;
        }
    }
}
