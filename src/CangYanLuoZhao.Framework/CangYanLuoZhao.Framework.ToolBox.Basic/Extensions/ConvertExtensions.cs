#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2024 苍煙落照保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MM-202402051433
 * 命名空间：CangYanLuoZhao.Framework.ToolBox.Basic.Extensions
 * 唯一标识：e49cb19e-97ac-4f2f-9069-17a97d331432
 * 文件名：ConvertExtensions
 * 当前用户域：MM-202402051433
 * 创建者：苍煙落照
 * 电子邮箱：543730731@qq.com
 * 创建时间：2024/7/6 5:24:11
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：苍煙落照
 * 时间：2024/7/6 5:24:11
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

using System;
using System.Collections.Generic;
using System.Text;

namespace CangYanLuoZhao.Framework.ToolBox.Basic.Extensions
{
    /// <summary>
    /// ConvertExtensions 的摘要说明
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// 批量转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<T> ConvertAllToList<T>(this IEnumerable<string> source)
        {
            var resultList = new List<T>();
            var sourceType = typeof(T);
            var flag = true;

            foreach (var item in source)
            {
                if (sourceType.BaseType!.Name == "Enum")
                {
                    if (int.TryParse(item, out var value) && Enum.IsDefined(sourceType, value))
                    {
                        flag = Enum.TryParse(sourceType, item, out var enumResult);
                        if (flag)
                        {
                            resultList.Add((T)enumResult);
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                switch (sourceType.Name)
                {
                    case "Int32":
                        flag = int.TryParse(item, out var intResult);
                        resultList.Add((T)(object)intResult);
                        break;
                    case "Int64":
                        flag = long.TryParse(item, out var longResult);
                        resultList.Add((T)(object)longResult);
                        break;
                    case "Guid":
                        flag = Guid.TryParse(item, out var guidResult);
                        resultList.Add((T)(object)guidResult);
                        break;
                    case "Decimal":
                        flag = decimal.TryParse(item, out var decimalResult);
                        resultList.Add((T)(object)decimalResult);
                        break;
                    case "Double":
                        flag = double.TryParse(item, out var doubleResult);
                        resultList.Add((T)(object)doubleResult);
                        break;
                    case "String":
                        resultList.Add((T)(object)item);
                        break;
                    case "DateTime":
                        flag = DateTime.TryParse(item, out var timeResult);
                        resultList.Add((T)(object)timeResult);
                        break;
                    default:
                        throw new ArgumentException(nameof(sourceType),
                            $"转换失败，支支持转换为long、int、string、guid、datetime、decimal、double、enum");
                }

                if (!flag)
                    HandleErrorMessage(false, item);
            }

            return resultList;

            void HandleErrorMessage(bool canConvert, string errorSource)
            {
                if (canConvert) return;
                var sb = new StringBuilder();
                sb.Append($"转换目标类型：{sourceType.Name}。");
                sb.Append($"数据源为：{errorSource}");
                throw new ArgumentException(nameof(source), sb.ToString() + "转换失败，支支持转换为long、int、string、guid、datetime、decimal、double、enum");
            }
        }
    }
}