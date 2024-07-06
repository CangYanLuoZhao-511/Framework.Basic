using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.String;

namespace CangYanLuoZhao.Framework.ToolBox.Basic.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNull(this string source)
        {
            return IsNullOrEmpty(source);
        }

        /// <summary>
        /// 判断字符串是否不为空
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool NotNull(this string source)
        {
            return !IsNullOrEmpty(source);
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this string source)
        {
            return IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 判断字符串是否不为空
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool NotWhiteSpace(this string source)
        {
            return !IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 动态生成编码
        /// </summary>
        /// <param name="prefix">前缀如GY</param>
        /// <param name="code"></param>
        /// <returns>比如：GYA001~GYZ999</returns>
        public static string DynamicGeneratorCode(this string prefix, string code)
        {
            try
            {
                if (code.IsNull())
                    throw new ArgumentNullException(nameof(code));

                //通过索引获取第一个字符
                var firstChar = code.Trim()[0];

                //如果第一个字符为字母A001
                if (!char.IsLetter(firstChar))
                    return $"{prefix}{(Convert.ToInt32(code) + 1).ToString().PadLeft(code.Length, '0')}";
                //取出字符保留数字部分
                var maxNumber = int.Parse(code.TrimStart(firstChar));

                //数字最大值+1的位数大于原位数说明进位了 这时需要重置数字 如999 重置为001
                if ((maxNumber + 1).ToString().Length > code.Length - 1)
                {
                    //如果第一个字符不为Z 字符就不能自增
                    return !firstChar.Equals('Z') ?
                        //返回前缀名如GY +字母自增如A-->B ，进位是1的左边补位0的个数为原字符长度-1
                        //实现字符增位 就是先将当前字符+1如A65-》66 转为ASCII码就成了B
                        $"{prefix}{(char)((byte)firstChar + 1)}{"1".PadLeft(code.Length - 1, '0')}" : null;
                }

                return $"{prefix}{firstChar}{(maxNumber + 1).ToString().PadLeft(code.Length - 1, '0')}";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 字符串连接
        /// <summary>
        /// 给字符串两边添加单引号（同时会将内部单引号替换成双单引号）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string QuotedStr(this string source)
        {
            return "'" + source.Replace("'", "''") + "'";
        }

        /// <summary>
        /// 给字符串两边添加括号
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns>返回带括号的字符串</returns>
        public static string BracketStr(this string source)
        {
            return "(" + source + ")";
        }


        /// <summary>
        /// 字符串加指定字符包含
        /// </summary>
        /// <param name="source"></param>
        /// <param name="quot"></param>
        /// <returns></returns>
        public static string QuotedWith(this string source, string quot)
        {
            return Concat(new string[]
            {
                "'",
                quot,
                source.Replace("'", "''"),
                quot,
                "'"
            });
        }


        /// <summary>
        /// 使用And连接两个SQL条件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string ConcatSQL(this string source, string condition)
        {
            if (source.Trim() == "" || condition.Trim() == "")
            {
                return source + condition;
            }
            return source + " and " + condition;
        }
        #endregion

        #region 字符串比较填充

        /// <summary>
        /// 字符串比较
        /// </summary>
        /// <param name="source1">被比较字符串</param>
        /// <param name="source2">比较字符串</param>
        /// <returns></returns>
        public static bool SameText(this string source1, string source2)
        {
            return Compare(source1, source2, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// 向右补充字符串长度
        /// </summary>
        /// <param name="source"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static string PadRightCN(this string source, int totalWidth)
        {
            var num = 0;
            checked
            {
                num += source.Select(c => (int)c).Count(num2 => num2 > 127);

                return source.PadRight(totalWidth - num);
            }
        }

        /// <summary>
        /// 向左补充字符串长度
        /// </summary>
        /// <param name="source"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static string PadLeftCN(this string source, int totalWidth)
        {
            var num = 0;
            checked
            {
                num += source.Select(c => (int)c).Count(num2 => num2 > 127);
                return source.PadLeft(totalWidth - num);
            }
        }

        #endregion

        #region 字符串截取
        /// <summary>
        /// 按指定间隔符，连接字符串
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="splitStr">间隔符</param>
        /// <returns></returns>
        public static string BuildWith(this string[] array, string splitStr)
        {
            var text = "";
            var str = "";
            foreach (var str2 in array)
            {
                text = text + str + str2;
                str = splitStr;
            }
            return text;
        }

        /// <summary>
        /// 按指定间隔符，连接字符串
        /// </summary>
        /// <param name="list">list列表</param>
        /// <param name="splitStr">间隔符</param>
        /// <returns></returns>
        public static string BuildWith(this List<string> list, string splitStr)
        {
            var text = "";
            var str = "";
            foreach (var current in list)
            {
                text = text + str + current;
                str = splitStr;
            }
            return text;
        }


        /// <summary>
        /// 字符串拆分成list
        /// </summary>
        /// <param name="source">源字段串</param>
        /// <returns></returns>
        public static List<string> StringSplitToList(string source)
        {
            var listField = new List<string>();
            var array = source.Split(new char[]
            {
                ',',
                ';'
            });
            foreach (var str in array)
            {
                if (str.Length > 0 && !listField.Contains(str))
                {
                    listField.Add(str);
                }
            }
            return listField;
        }


        /// <summary>
        /// 按指定分割符，字符串拆分成list
        /// </summary>
        /// <param name="source">源字段串</param>
        /// <param name="splitStr">分割字符</param>
        /// <returns></returns>
        public static List<string> StringSplitToList(string source, char splitStr)
        {
            var listField = new List<string>();
            var array = source.Split(splitStr);
            foreach (var str in array)
            {
                if (str.Length > 0 && !listField.Contains(str))
                {
                    listField.Add(str);
                }
            }
            return listField;
        }


        /// <summary>
        /// 截取中间字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="start">开始字符串</param>
        /// <param name="end">末尾字符串</param>
        /// <returns></returns>
        public static string SubMiddleString(string source, string start, string end)
        {
            var l_ret = "";
            //空为0
            var i = source.IndexOf(start, StringComparison.Ordinal);
            if (i < 0) return l_ret;
            var j = source.IndexOf(end, i + start.Length, StringComparison.Ordinal);
            if (j > 0)
            {
                l_ret = source.Substring(i + start.Length, j - i - start.Length);
            }
            return l_ret;
        }

        #endregion

        #region 删除字符串
        /// <summary>
        /// 删除最后结尾的逗号
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>string</returns>
        public static string DelLastComma(string source)
        {
            return source[..source.LastIndexOf(",", StringComparison.Ordinal)];
        }

        /// <summary>
        /// 删除最后结尾指定字符
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="charSource">字符</param>
        /// <returns>string</returns>
        public static string DelLastComma(string source, char charSource)
        {
            return source[..source.LastIndexOf(charSource)];
        }
        #endregion

        #region 字符串重命名
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToUpperFirstWord(string source)
        {
            if (source.IsNull())
            {
                return Empty;
            }
            return source[..1].ToUpper() + source[1..].ToLower();
        }

        /// <summary>
        /// 驼峰命名法(第一个单字以小写字母+第二个单字大写首字母)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToCamelName(string source)
        {
            var result = new StringBuilder();
            if (IsNullOrEmpty(source))
            {
                return Empty;
            }

            if (!source.Contains("_"))// 不含下划线，仅将首字母大写
            {
                return ToUpperFirstWord(source);
            }
            // 用下划线将原始字符串分割
            var camels = source.Split('_');
            foreach (var camel in camels)
            {
                // 跳过原始字符串中开头、结尾的下换线或双重下划线
                if (camel.IsNull())
                {
                    continue;
                }

                if (result.Length == 0) // 第一个驼峰片段，全部字母都小写
                {
                    result.Append(camel.ToLower());
                }
                else
                {
                    // 其他的驼峰片段，首字母大写,其它小写
                    result.Append(camel[..1].ToUpper());
                    result.Append(camel[1..].ToLower());
                }
            }
            return result.ToString();
        }

        #endregion      
    }
}
