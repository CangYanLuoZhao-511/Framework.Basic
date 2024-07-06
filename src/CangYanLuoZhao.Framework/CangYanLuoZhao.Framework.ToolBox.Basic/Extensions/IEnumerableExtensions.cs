#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2024 苍煙落照保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MM-202402051433
 * 命名空间：CangYanLuoZhao.Framework.ToolBox.Basic.Extensions
 * 唯一标识：f8153340-2861-4ee7-b7bc-2da49be52f6e
 * 文件名：IEnumerableExtensions
 * 当前用户域：MM-202402051433
 * 创建者：苍煙落照
 * 电子邮箱：543730731@qq.com
 * 创建时间：2024/7/6 5:22:06
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：苍煙落照
 * 时间：2024/7/6 5:22:06
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CangYanLuoZhao.Framework.ToolBox.Basic.Models.Enums;

namespace CangYanLuoZhao.Framework.ToolBox.Basic.Extensions
{
    /// <summary>
    /// IEnumerableExtensions 的摘要说明
    /// </summary>
    public static class IEnumerableExtensions
    {    /// <summary>
         /// 判断是否为空集
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="source"></param>
         /// <returns></returns>
        public static bool IsNull<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        /// <summary>
        /// 判断是否不为空集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool NotNull<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="current"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToPage<T>(this IEnumerable<T> source, int current, int size)
        {
            return source.Skip((current - 1) * current).Take(size);
        }

        /// <summary>
        /// 扩展 Dictionary 根据Value反向查找Key的方法
        /// </summary>
        public static T1 Get<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> list, T2 t2)
        {
            foreach (KeyValuePair<T1, T2> obj in list)
                if (obj.Value.Equals(t2)) return obj.Key;
            return default(T1);
        }

        /// <summary>
        /// 扩展数组方法 可以在前或者后插入一个对象
        /// </summary>
        /// <param name="list"></param>
        /// <param name="obj">要插入的对象</param>
        /// <param name="place">位置 after/before</param>
        /// <returns></returns>
        public static IEnumerable<T> Inject<T>(this IEnumerable<T> list, T obj, ArrayEnum.ArrayInjectPlace place = ArrayEnum.ArrayInjectPlace.Top)
        {
            var enumerable = list as T[] ?? list.ToArray();
            var list2 = new T[enumerable.Count() + 1];
            var index = 0;
            foreach (var t in enumerable)
            {
                list2[place == ArrayEnum.ArrayInjectPlace.Bottom ? index : index + 1] = t;
            }
            list2[place == ArrayEnum.ArrayInjectPlace.Bottom ? enumerable.Count() : 0] = obj;
            return list2;
        }

        /// <summary>
        /// 将数组合并成为一个字符串
        /// </summary>
        public static string Join<T>(this IEnumerable<T> list, char? c = ',')
        {
            return list.Join(c.ToString());
        }

        public static string Join<T>(this IEnumerable<T> list, string split)
        {
            return string.Join(split, list);
        }

        /// <summary>
        /// 按指定条件过滤数组
        /// </summary>
        /// <param name="ac">默认为过滤重复</param>
        /// <param name="list"></param>
        /// <param name="filterRule"></param>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, ArrayEnum.ArrayFilterRule filterRule = ArrayEnum.ArrayFilterRule.NoRepeater)
        {
            if (!Enum.IsDefined(typeof(ArrayEnum.ArrayFilterRule), filterRule))
                throw new InvalidEnumArgumentException(nameof(filterRule), (int)filterRule,
                    typeof(ArrayEnum.ArrayFilterRule));
            var list2 = new List<T>();
            foreach (var t in list)
            {
                if (!list2.Contains(t)) list2.Add(t);
            }
            return list2;
        }

        /// <summary>
        /// 合并数组 并且去除重复项
        /// </summary>
        /// <param name="objList"></param>
        /// <param name="converter">要比较的字段</param>
        /// <param name="arr"></param>
        public static List<T> Merge<T, TOutput>(this IEnumerable<T> objList, Converter<T, TOutput> converter, params IEnumerable<T>[] arr)
        {
            var list = objList.ToList();
            foreach (var obj in arr)
            {
                list.AddRange(obj.ToList().FindAll(t => !list.Exists(t1 => converter(t1).Equals(converter(t)))));
            }
            return list;
        }



        /// <summary>
        /// 获取数组的索引项。 如果超出则返回类型的默认值
        /// </summary>
        public static T GetIndex<T>(this IEnumerable<T> list, int index)
        {
            var enumerable = list as T[] ?? list.ToArray();
            if (index >= enumerable.Count() || index < 0) return default;
            return enumerable.ToArray()[index];
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“＆”字符拼接成字符串
        /// </summary>
        /// <param name="list"></param>
        public static string ToQueryString<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list)
        {
            return list.ToList().ConvertAll(t => $"{t.Key}={t.Value}").Join("&");
        }

        /// <summary>
        /// 除去数组中的空值和指定名称的参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filter">过滤规则 默认做为空判断</param>
        public static IEnumerable<KeyValuePair<TKey, TValue>> Filter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list, Func<TKey, TValue, bool> filter = null)
        {
            filter ??= (key, value) => !string.IsNullOrEmpty(key.ToString()) && value != null;
            foreach (var item in list)
            {
                if (filter(item.Key, item.Value))
                    yield return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 不包含指定的Key
        /// </summary>
        public static IEnumerable<KeyValuePair<TKey, TValue>> Filter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list, params TKey[] filter)
        {
            return list.Filter((key, value) => !string.IsNullOrEmpty(key.ToString()) && value != null && !filter.Contains(key));
        }

        /// <summary>
        /// 按照Key从小大大拍列
        /// </summary>
        /// <param name="list"></param>
        public static void Sort<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            _ = list.ToList().OrderBy(t => t.Key);
        }

        /// <summary>
        /// 获取父级的继承树（最多支持32级）
        /// 包括自己
        /// </summary>
        /// <param name="obj">要查找的数组对象</param>
        /// <param name="id">当前对象的主键值</param>
        /// <param name="value">主键字段</param>
        /// <param name="parent">父级字段</param>
        /// <returns></returns>
        public static List<T> GetParent<T, TValue>(this List<T> obj, TValue id, Func<T, TValue> value, Func<T, TValue> parent)
        {
            var count = 0;
            var t = obj.Find(m => value.Invoke(m).Equals(id));
            var list = new List<T>();
            while (t != null)
            {
                if (count > 32) break;
                list.Add(t);
                t = obj.Find(m => value.Invoke(m).Equals(parent.Invoke(t)));
                count++;
            }
            return list;
        }

        /// <summary>
        /// 获取子集列表（包括自己）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="parent"></param>
        /// <param name="list"></param>
        public static void GetChild<T, TValue>(this List<T> obj, TValue id, Func<T, TValue> value, Func<T, TValue> parent, ref List<T> list)
        {
            list ??= new List<T>();
            var objT = obj.Find(t => value.Invoke(t).Equals(id));
            if (objT == null) return;
            {
                list.Add(objT);
                foreach (var t in obj.FindAll(m => parent.Invoke(m).Equals(id)))
                {
                    obj.GetChild(value.Invoke(t), value, parent, ref list);
                }
            }
        }

        /// <summary>
        /// 获取树形结构的子集执行方法
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="id">当前父节点</param>
        /// <param name="value">获取主键的委托</param>
        /// <param name="parent">获取父值的委托</param>
        /// <param name="action">委托执行的方法 int 为当前的深度</param>
        /// <param name="depth">当前的深度</param>
        public static void GetTree<T, TValue>(this List<T> obj, TValue id, Func<T, TValue> value, Func<T, TValue> parent, Action<T, int> action, int depth = 0)
        {
            foreach (var t in obj.FindAll(m => parent.Invoke(m).Equals(id)))
            {
                action.Invoke(t, depth + 1);
                obj.GetTree(value.Invoke(t), value, parent, action, depth + 1);
            }
        }


    }

}
