using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Psd.H5Show.Common.DEncrypt;
using Psd.H5Show.Model;

namespace PsdH5ShowWebApp.Controllers
{
    public class mController : Controller
    {
        // GET: m/i/123
        public ActionResult I(String id)
        {
            string Pagehtml = "";
            id = DESEncrypt.Decrypt(id);
            if (id != null)
            {


                appbase AppBase = new Psd.H5Show.BLL.appbase().GetModel(int.Parse(id));
                List<apppagebase> appPageBases = new Psd.H5Show.BLL.apppagebase().GetModelList(" Appid='" + AppBase.ID + "' and DELFLAG=0 order by orderby");

                foreach (apppagebase appPageBase in appPageBases)
                {
                    string PageList = "<div class='swiper-slide' title='@title'>";

                    List<apppageinfo> appPageInfos = new Psd.H5Show.BLL.apppageinfo().GetModelList(" PAGEID='" + appPageBase.ID + "'");
                    foreach (apppageinfo appPageInfo in appPageInfos)
                    {

                        switch (appPageInfo.KitType)
                        {
                            case "image":
                                string imgSrc = appPageInfo.KitContent;
                                string[] ims = imgSrc.Split('=');
                                imgSrc = ims[1].Substring(0, ims[1].Length - 1);

                                string KitItem = "<div class='ani' swiper-animate-effect='@effect' swiper-animate-duration='@durationss' " +
                                       "swiper-animate-delay='@delayss' style='position:absolute !important;left: @left; top: @top;" +
                                       " z-index: @zindex; width: @width; height: @height; ' >" +
                                                 "<img style=\"transform:rotate(@transformdeg); width:100%;height:100%;\" src=\'../../" +imgSrc+"\'></div>";
                                KitItem = KitItem.Replace("@effect", appPageInfo.KitAnEffect);
                                KitItem = KitItem.Replace("@durations", appPageInfo.KitAnDuration.ToString());
                                KitItem = KitItem.Replace("@delays", appPageInfo.KitAnDelay.ToString());
                                KitItem = KitItem.Replace("@left", appPageInfo.KitLeft.ToString());
                                KitItem = KitItem.Replace("@top", appPageInfo.KitTop.ToString());
                                KitItem = KitItem.Replace("@width", appPageInfo.KitWidth.ToString());
                                KitItem = KitItem.Replace("@height", appPageInfo.KitHeight.ToString());
                                KitItem = KitItem.Replace("@transform", appPageInfo.KitAngleZ.ToString());
                                PageList += KitItem;
                                break;

                        }

                    }

                    PageList = PageList.Replace("@title", appPageBase.Title);

                    PageList += "</div>";
                    Pagehtml += PageList;
                }

                ViewBag.AppBase = AppBase;
            }
            else
            {
                Pagehtml = "非法参数";
            }
            ViewBag.Pagehtml = Pagehtml;

            return View();
        }
    }
}