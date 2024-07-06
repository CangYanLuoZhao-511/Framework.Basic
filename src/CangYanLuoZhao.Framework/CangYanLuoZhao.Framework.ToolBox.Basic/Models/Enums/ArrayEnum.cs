#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2024 苍煙落照保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MM-202402051433
 * 命名空间：CangYanLuoZhao.Framework.ToolBox.Basic.Models.Enums
 * 唯一标识：f2c3ea83-9cde-41cd-87f4-b60530a0be92
 * 文件名：ArrayEnum
 * 当前用户域：MM-202402051433
 * 创建者：苍煙落照
 * 电子邮箱：543730731@qq.com
 * 创建时间：2024/7/6 14:59:56
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：苍煙落照
 * 时间：2024/7/6 14:59:56
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

namespace CangYanLuoZhao.Framework.ToolBox.Basic.Models.Enums
{
    /// <summary>
    /// ArrayEnum 的摘要说明
    /// </summary>
    public class ArrayEnum
    {
        /// <summary>
        /// 数组过滤规则
        /// </summary>
        public enum ArrayFilterRule
        {
            /// <summary>
            /// 过滤重复
            /// </summary>
            NoRepeater
        }

        /// <summary>
        /// 插入数组的位置
        /// </summary>
        public enum ArrayInjectPlace
        {
            /// <summary>
            /// 在顶部插入
            /// </summary>
            Top,
            /// <summary>
            /// 在尾部追加
            /// </summary>
            Bottom
        }
    }
}