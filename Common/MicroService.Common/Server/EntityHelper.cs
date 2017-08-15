﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroService.Models;
using iFramework.Util;

namespace MicroService.Common
{
    /// <summary>
    /// 实体的帮助的类
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// 实体的验证信息到模型的验证信息的转换
        /// </summary>
        /// <param name="validResult"></param>
        /// <returns></returns>
        public static ModelValidationResult ToModelValidationResult(this DbEntityValidationResult validResult)
        {
            ModelValidationResult modelValidResult = new ModelValidationResult() { IsValid = true };
            if (validResult != null)
            {
                modelValidResult.IsValid = validResult.IsValid;

                foreach (var item in validResult.ValidationErrors)
                {
                    modelValidResult.ValidationErrors.Add(new ModelValidationError() { ErrorMessage = item.ErrorMessage, PropertyName = item.PropertyName });
                }
            }
            return modelValidResult;
        }

        #region 得到名字
        /// <summary>
        /// 得到模型默认的名字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetDefaultNames(string name, bool includeBu = true, bool includeCrtUsr = true, bool includeCrtTime = true, bool includeChgUsr = true, bool includeChgTime = true)
        {
            if (includeBu)
            {
                yield return EntityHelper.GetBuzRkNameByName(name);
            }
            if (includeCrtUsr)
            {
                yield return EntityHelper.GetCreatedUsrNameByName(name);
            }
            if (includeCrtTime)
            {
                yield return EntityHelper.GetCreatedTimeNameByName(name);
            }
            if (includeChgUsr)
            {
                yield return EntityHelper.GetChangedUsrNameByName(name);
            }
            if (includeChgTime)
            {
                yield return EntityHelper.GetChangedTimeNameByName(name);
            }
 
        }
        /// <summary>
        /// 得到商务域名称
        /// </summary>
        /// <returns></returns>
        public static string GetBuzRkName(object model)
        {
            string fieldname = string.Format("{0}_BU_RK", GetModelName(model));
            return fieldname;
        }
        /// <summary>
        /// 创建修改人
        /// </summary>
        /// <returns></returns>
        public static string GetChangedUsrName(object model)
        {
            string fieldname = string.Format("{0}_CHG_USR_RK", GetModelName(model));
            return fieldname;
        }
        /// <summary>
        /// 得到创建时间名字
        /// </summary>
        /// <returns></returns>
        public static string GetChangedTimeName(object model)
        {
            string fieldname = string.Format("{0}_CHG_TIME", GetModelName(model));
            return fieldname;
        }
        /// <summary>
        /// 创建修改人
        /// </summary>
        /// <returns></returns>
        public static string GetCreatedUsrName(object model)
        {
            string fieldname = string.Format("{0}_CRT_USR_RK", GetModelName(model));
            return fieldname;
        }
        /// <summary>
        /// 得到创建时间名字
        /// </summary>
        /// <returns></returns>
        public static string GetCreatedTimeName(object model)
        {
            string fieldname = string.Format("{0}_CRT_TIME", GetModelName(model));
            return fieldname;
        }

        /// <summary>
        /// 得到商务域名称
        /// </summary>
        /// <returns></returns>
        public static string GetBuzRkNameByName(string name)
        {
            string fieldname = string.Format("{0}_BU_RK", GetModelNameByName(name));
            return fieldname;
        }
        /// <summary>
        /// 创建修改人
        /// </summary>
        /// <returns></returns>
        public static string GetChangedUsrNameByName(string name)
        {
            string fieldname = string.Format("{0}_CHG_USR_RK", GetModelNameByName(name));
            return fieldname;
        }
        /// <summary>
        /// 得到创建时间名字
        /// </summary>
        /// <returns></returns>
        public static string GetChangedTimeNameByName(string name)
        {
            string fieldname = string.Format("{0}_CHG_TIME", GetModelNameByName(name));
            return fieldname;
        }
        /// <summary>
        /// 创建修改人
        /// </summary>
        /// <returns></returns>
        public static string GetCreatedUsrNameByName(string name)
        {
            string fieldname = string.Format("{0}_CRT_USR_RK", GetModelNameByName(name));
            return fieldname;
        }
        /// <summary>
        /// 得到创建时间名字
        /// </summary>
        /// <returns></returns>
        public static string GetCreatedTimeNameByName(string name)
        {
            string fieldname = string.Format("{0}_CRT_TIME", GetModelNameByName(name));
            return fieldname;
        }
        #endregion
        #region 设置值


        /// <summary>
        /// 设置创建时间
        /// </summary>
        public static void SetCreatedTime(object model, bool overideValue = false)
        {
            string fieldname = string.Format("{0}_CRT_TIME", GetModelName(model));
            if (model.HasProperty(fieldname))
            {
                if (overideValue == false && model.HasValue(fieldname))
                {
                    return;
                }

                model.SetValue(fieldname, HMTDateTime.Now);
            }
        }

        /// <summary>
        /// 设置修改日期
        /// </summary>
        public static void SetChangedTime(object model, bool overideValue = true)
        {
            string fieldname = string.Format("{0}_CHG_TIME", GetModelName(model));
            if (model.HasProperty(fieldname))
            {
                if (overideValue == false && model.HasValue(fieldname))
                {
                    return;
                }
                model.SetValue(fieldname, HMTDateTime.Now);
            }
        }

        /// <summary>
        /// 设置创建人
        /// </summary>
        public static void SetCreatedUsr(object model, bool overideValue = false, string UsrRK = "")
        {
            string fieldname = string.Format("{0}_CRT_USR_RK", GetModelName(model));
            if (model.HasProperty(fieldname))
            {
                if (overideValue == false && model.HasValue(fieldname))
                {
                    return;
                }
                var theValue = UsrRK;
                if (string.IsNullOrEmpty(theValue))
                {
                    theValue = UserInfo.GetUserInfo().BU_User;
                }
                if (string.IsNullOrWhiteSpace(theValue))
                {
                    return;
                }

                model.SetValue(fieldname, theValue);
            }
        }

        


        /// <summary>
        /// 设置修改人
        /// </summary>
        public static void SetChangedUsr(object model, bool overideValue = true, string UsrRK = "")
        {
            string fieldname = string.Format("{0}_CHG_USR_RK", GetModelName(model));

            if (model.HasProperty(fieldname))
            {
                if (overideValue == false && model.HasValue(fieldname))
                {
                    return;
                }
                var theValue = UsrRK;
                if (string.IsNullOrEmpty(theValue))
                {
                    theValue = UserInfo.GetUserInfo().BU_User;
                }
                if (string.IsNullOrWhiteSpace(theValue))
                {
                    return;
                }
                model.SetValue(fieldname, theValue);
            }
        }

        #endregion 
        /// <summary>
        /// 获取当前的前缀名
        /// </summary>
        /// <returns></returns>
        private static string GetModelName(object obj)
        {
            return obj.GetType().Name.Split(new char[] { '_' })[0];
        }
        /// <summary>
        /// 获取当前的前缀名
        /// </summary>
        /// <returns></returns>
        private static string GetModelNameByName(string name)
        {
            return name.Split(new char[] { '_' })[0];
        }
        public static IEnumerable<IEntityMapper> GetEntityMappers()
        {
            var result = (IEnumerable<IEntityMapper>)Type.GetType("iiERP.XMap.EntityMapper,iiERP.XMap").GetMethod("GetEntityMappers").Invoke(null, null);
            if (result != null)
            {
                return result;
            }
            else
            {
                return new IEntityMapper[]{};
            }
        }

    }
}
