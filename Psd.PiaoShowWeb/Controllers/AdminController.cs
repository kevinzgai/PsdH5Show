using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Psd.H5Show.BLL;
using Psd.H5Show.Common.DEncrypt;
using Psd.H5Show.Model.Tools;
using appbase = Psd.H5Show.Model.appbase;
using user_account = Psd.H5Show.Model.user_account;

namespace PsdH5ShowWebApp.Controllers
{
    public class AdminController : Controller
    {
        Messge msgModel = new Messge();
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }

        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        // GET: Admin/Loginaction
        [HttpPost]
        public ActionResult Loginaction()
        {
            string login_username = Request.Form["login_username"].ToString().Trim();
            string login_password = DEncrypt.Encrypt(Request.Form["login_password"].ToString().Trim());
            Psd.H5Show.BLL.user_account userAccountBll = new Psd.H5Show.BLL.user_account();

            List<Psd.H5Show.Model.user_account> userAccountModelList =
                userAccountBll.GetModelList(
                    string.Format("STATE='1' and DELFLAG='0' and UserCode='{0}' and `Password` ='{1}'", login_username,
                        login_password));
            if (userAccountModelList.Count > 0)
            {
                msgModel.Result = 1;
                msgModel.Msg = "登录成功";
                HttpCookie cookie = new HttpCookie("PsdH5ShowUserCode");
                cookie.Value = DEncrypt.Encrypt(userAccountModelList[0].UserCode);
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Add(cookie);
            }
            else
            {
                msgModel.Result = 0;
                msgModel.Msg = "账号或密码错误";
            }
            JsonResult Js = new JsonResult();
            Js.Data = msgModel;
            Js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Js;
        }





        // GET: Admin/Applist
        [HttpGet]
        public ActionResult Applist()
        {
            Psd.H5Show.Model.user_account loginUserModel = new Psd.H5Show.Model.user_account();
            if (Request.Cookies["PsdH5ShowUserCode"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                loginUserModel = GetUserCodeToUseraccountModel(Request.Cookies["PsdH5ShowUserCode"].Value);
                List<Psd.H5Show.Model.appbase> appBaseModelList =
                    new Psd.H5Show.BLL.appbase().GetModelList(string.Format("Uid='{0}' and DELFLAG=0", loginUserModel.ID));
                for (int i = 0; i < appBaseModelList.Count; i++)
                {
                    appBaseModelList[i].Memo1 = DESEncrypt.Encrypt(Convert.ToString(appBaseModelList[i].ID));
                }

                ViewBag.Applist = appBaseModelList;
            }
            return View();
        }

        // post: Admin/Appcreateaction
        [HttpPost]
        public ActionResult Appcreateaction()
        {
            JsonResult Js = new JsonResult();
            Js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Psd.H5Show.Model.user_account loginUserModel = new Psd.H5Show.Model.user_account();
            Psd.H5Show.Model.appbase AppBaseUserModel = new Psd.H5Show.Model.appbase();

            string AppName = Request.Form["AppName"];
            string AppDesribe = Request.Form["AppDesribe"];

            if (Request.Cookies["PsdH5ShowUserCode"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                if (AppName.Length < 2 || AppDesribe.Length < 2)
                {
                    msgModel.Result = 0;
                    msgModel.Msg = "应用标题或者描述长度不够";
                }
                else
                {

                    loginUserModel = GetUserCodeToUseraccountModel(Request.Cookies["PsdH5ShowUserCode"].Value);
                    AppBaseUserModel.AppName = AppName;
                    AppBaseUserModel.AppDescribe = AppDesribe;
                    AppBaseUserModel.Uid = loginUserModel.ID;
                    if (new Psd.H5Show.BLL.appbase().Add(AppBaseUserModel))
                    {
                        msgModel.Result = 1;
                        msgModel.Msg = "添加成功";
                    }
                    else
                    {
                        msgModel.Result = 0;
                        msgModel.Msg = "添加失败，请联系管理员";
                    }
                }

            }

            Js.Data = msgModel;
            return Js;
        }


        // get: Admin/AppEditaction
        [HttpPost]
        public ActionResult AppEditaction()
        {
            string loginUserCode = null;
            user_account userAccount = null;
            JsonResult Js = new JsonResult();
            Js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (Request.Cookies["PsdH5ShowUserCode"] == null)
            {
                msgModel.Result = 0;
                msgModel.Msg = "操作失败，请重新登陆!";
            }
            else
            {
                loginUserCode = Request.Cookies["PsdH5ShowUserCode"].Value.ToString();
            }

            userAccount = GetUserCodeToUseraccountModel(loginUserCode);//获取当前登录用户实体
            string editType = Request["edittype"].ToString().Trim();
            int AppId =int.Parse(DESEncrypt.Decrypt(Request["code"].ToString().Trim()));
            
            
            //判断操作权限
            if (!AppTools.IsMemberApp(userAccount.UserCode,AppId))
            {
                msgModel.Result = 0;
                msgModel.Msg = "请不要非法操作，您没有权限操作此App";
                Js.Data = msgModel;
                return Js;
            }
            switch (editType)
            {
                case "del":

                    try
                    {


                        if (AppTools.DelApp(AppId, userAccount))
                        {
                            msgModel.Result = 1;
                            msgModel.Msg = "删除成功";
                        }
                        else
                        {
                            msgModel.Result = 0;
                            msgModel.Msg = "删除失败，可能是权限不正确";

                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    break;
                case "update":
                    try
                    {
                        appbase appbase = new Psd.H5Show.BLL.appbase().GetModel(AppId);

                        appbase.AppName= Request["name"].ToString();
                        appbase.AppDescribe = Request["des"].ToString();

                        if (new Psd.H5Show.BLL.appbase().Update(appbase))
                        {
                            msgModel.Result = 1;
                            msgModel.Msg = "更新成功";
                        }
                        else
                        {
                            msgModel.Result = 0;
                            msgModel.Msg = "更新失败，可能是权限不正确";
                        }
                    }
                    catch (Exception e)
                    {
                        msgModel.Result = 0;
                        msgModel.Msg = e.ToString();
                        throw;
                    }
                  

                    break;
                default:
                    break;

            }
            Js.Data = msgModel;
            return Js;
        }

       

        /// <summary>
        /// 根据被加密过的UserCode返回用户信息实体
        /// </summary>
        /// <param name="EnUSERCODE">被加密过的UserCode</param>
        /// <returns></returns>
        public static Psd.H5Show.Model.user_account GetUserCodeToUseraccountModel(string EnUSERCODE)
        {
            string UserCode = DEncrypt.Decrypt(EnUSERCODE);
            object obj = new Psd.H5Show.BLL.user_account().GetModelList(string.Format("USERCODE='{0}'", UserCode))[0];
            if (obj != null)
            {
                return obj as Psd.H5Show.Model.user_account;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取当前登陆用户
        /// </summary>
        /// <param name="contentHttpContext"></param>
        /// <returns></returns>
        public Psd.H5Show.Model.user_account GetUserAccount(HttpContextBase contentContext)
        {
            
            if (contentContext.Request.Cookies["PsdH5ShowUserCode"] == null)
            {
                RedirectToAction("Login", "Admin");
                return null;
            }
            else
            {
                return GetUserCodeToUseraccountModel(HttpContext.Request.Cookies["PsdH5ShowUserCode"].ToString());

            }
        }
    }
}