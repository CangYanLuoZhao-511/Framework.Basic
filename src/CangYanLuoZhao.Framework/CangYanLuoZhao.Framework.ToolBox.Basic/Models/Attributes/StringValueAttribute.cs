#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2024 苍煙落照保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MM-202402051433
 * 命名空间：CangYanLuoZhao.Framework.ToolBox.Basic.Models.Attributes
 * 唯一标识：1b782026-0c4e-4057-a57b-e4dededb41d3
 * 文件名：StringValueAttribute
 * 当前用户域：MM-202402051433
 * 创建者：苍煙落照
 * 电子邮箱：543730731@qq.com
 * 创建时间：2024/7/6 14:22:07
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：苍煙落照
 * 时间：2024/7/6 14:22:07
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

using System;
using System.Runtime.CompilerServices;

namespace CangYanLuoZhao.Framework.ToolBox.Basic.Models.Attributes
{
    /// <summary>
    /// StringValueAttribute 的摘要说明
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        public string StringValue
        {
            [CompilerGenerated]
            get;
            [CompilerGenerated]
            set;
        }
    }
}