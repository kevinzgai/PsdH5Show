using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Psd.H5Show.Common.DEncrypt;
using Psd.H5Show.DBUtility;

namespace Psd.H5Show.BLL
{
   public class AppTools
    {
        /// <summary>
        /// Determines whether [is member application] [the specified user code].
        /// </summary>
        /// <param name="userCode">The user code.</param>
        /// <param name="appId">The application identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is member application] [the specified user code]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMemberApp(string userCode, int appId)
        {
            bool isauth = false;
            DataSet dataSet =
                new Psd.H5Show.BLL.user_account().GetList("DELFLAG=0  and USERCODE = '" + userCode + "'");
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                if (new Psd.H5Show.BLL.appbase().GetList("DELFLAG=0  and  ID=" + appId + " and UID=" +
                                                         dataSet.Tables[0].Rows[0]["ID"]).Tables[0].Rows.Count > 0)
                {
                    isauth = true;
                }
            }

            return isauth;
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
        /// Deletes the application.
        /// 1.判断是否有权限删除该App
        /// 2.非删除 只是把删除标识改成1
        /// </summary>
        /// <param name="appid">The appid.</param>
        /// <param name="uid">The userid.当前登陆用户</param>
        /// <returns>是否删除成功</returns>
        public static bool DelApp(int appid, Psd.H5Show.Model.user_account userAccount)
        {
            bool isdel = false;

            try
            {
                Model.appbase appBaseModel = new Model.appbase();
                appBaseModel = new Psd.H5Show.BLL.appbase().GetModel(appid);
                appBaseModel.DelFlag = true;
                if (userAccount.ID.Equals(appBaseModel.Uid))
                {
                    isdel = new Psd.H5Show.BLL.appbase().Update(appBaseModel);
                }
                else
                {
                    isdel = false;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return isdel;
        }

        /// <summary>
        /// 获取表格id最大值
        /// </summary>
        /// <param name="tabel">表格名称</param>
        /// <returns></returns>
        public static int getMaxIdToTable(string tabel)
        {
            string sql = "SELECT	MAX(ID) FROM " + tabel;
           return DbHelperMySQL.GetMaxID("Id", tabel);
        }

    }

}
