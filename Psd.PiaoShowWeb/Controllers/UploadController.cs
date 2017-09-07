using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsdH5ShowWebApp.Controllers
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Script.Serialization;
    using System.Web.Security;

    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        //public/Upload  
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase upImg)
        {
            upImg = Request.Files[0];
            if (string.IsNullOrEmpty(upImg.FileName)
                 )
            {
                return Json(new
                {
                    pic ="no  image",
                    error ="无图片"

                });
            }
            string fileName = System.IO.Path.GetFileName(upImg.FileName);
            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileName.Substring(fileName.IndexOf(".")); ;
            string filePhysicalPath = Server.MapPath("~/upload/" + fileName);
            string pic = "", error = "";
            try
            {
                upImg.SaveAs(filePhysicalPath);
                pic = "/upload/" + fileName;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return Json(new
            {
                pic = pic,
                error = error
            });
        }
        /// <summary>
        /// Uploads the img.
        /// </summary>
        /// <param name="Filedata">The filedata.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImg(HttpPostedFileBase Filedata)
        {
            // 没有文件上传，直接返回
            if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
            {
                return HttpNotFound();
            }
           

            //获取文件完整文件名(包含绝对路径)
            //文件存放路径格式：/files/upload/{日期}/{md5}.{后缀名}
            //例如：/files/upload/20130913/43CA215D947F8C1F1DDFCED383C4D706.jpg
            string fileMD5 = new Random().Next(0001,9999).ToString();
            string FileEextension = Path.GetExtension(Filedata.FileName);
            string uploadDate = DateTime.Now.ToString("yyyyMMdd");

            string imgType = Request["imgType"];
            string Appcode = Request["appcode"];
            string virtualPath = "/";
            if (imgType == "normal")
            {
                virtualPath = string.Format("~/upload/{0}/appbg/{1}{2}",Appcode, fileMD5, FileEextension);
            }
            else
            {
                virtualPath = string.Format("~/upload2/{0}/appbg/{1}{2}", Appcode, fileMD5, FileEextension);
            }
            string fullFileName = this.Server.MapPath(virtualPath);

            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            if (!System.IO.File.Exists(fullFileName))
            {
                Filedata.SaveAs(fullFileName);
            }

            var data = new { imgtype = imgType, imgpath = virtualPath.Remove(0, 1) };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
   
}
