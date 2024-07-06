#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2024 苍煙落照保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MM-202402051433
 * 命名空间：CangYanLuoZhao.Framework.ToolBox.Basic.Extensions
 * 唯一标识：a8b10728-2968-4da9-9e9e-3b6eedaa2910
 * 文件名：EnumExtensions
 * 当前用户域：MM-202402051433
 * 创建者：苍煙落照
 * 电子邮箱：543730731@qq.com
 * 创建时间：2024/7/6 5:25:50
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：苍煙落照
 * 时间：2024/7/6 5:25:50
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

using System;
using System.ComponentModel;
using CangYanLuoZhao.Framework.ToolBox.Basic.Models.Attributes;

namespace CangYanLuoZhao.Framework.ToolBox.Basic.Extensions
{
    /// <summary>
    /// EnumExtensions 的摘要说明
    /// </summary>
    public static class EnumExtensions
    {

        /// <summary>
        /// 获取特性 (DescriptionAttribute) 的说明；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            return fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attrs && attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// 直接获取特性（更轻量、更容易使用，不用封装“获取每一个自定义特性”的扩展方法）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// 获取特性 (DisplayAttribute) 的名称；如果未使用该特性，则返回枚举的名称。
        /// Days.Saturday.GetStringValue() 输出星期六
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            return fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) is StringValueAttribute[] attrs && attrs.Length > 0 ? attrs[0].StringValue : enumValue.ToString();
        }
    }
}