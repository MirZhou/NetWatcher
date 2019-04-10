using System.Collections;
using System.Text.RegularExpressions;

namespace NetWatcher.Common.Helper
{
    /// <summary>
    /// 正则表达式帮助类
    /// <para>创建：2015.11.06 周光兵</para>
    /// <para>修改：2015.11.13 添加方法：GetHrefString 周光兵</para>
    /// </summary>
    public class MatchHelper
    {
        /// <summary>
        /// 正则：获取HTML标签中的href内容
        /// </summary>
        private static string patternGetHref = "(?<=href=[\"'])[\\S\\s]+?(?=[\"'])";
        /// <summary>
        /// 正则：获取HTML标签中的a标签
        /// </summary>
        private static string patternGetTAGA = @"<a([\s\S]*?)/a>";
        /// <summary>
        /// 正则：获取数字字符串
        /// </summary>
        private static string patternGetNumber = @"[\d{6}]+";

        public static string GetString(string input, string pattern)
        {
            return Regex.Match(input, pattern).ToString();
        }

        public static string[] GetStrings(string input, string pattern)
        {
            MatchCollection _mc = Regex.Matches(input, pattern);

            string[] _strs = new string[_mc.Count];

            for (int i = 0; i < _mc.Count; i++)
            {
                _strs[i] = _mc[i].Value;
            }

            return _strs;
        }

        /// <summary>
        /// 返回数字字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetNumber(string input)
        {
            return GetString(input, patternGetNumber);
        }

        /// <summary>
        /// 返回HTML代码中的网址
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static ArrayList GetHyperLinks(string htmlCode)
        {
            ArrayList _al = new ArrayList();

            Regex _regex = new Regex(@"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            MatchCollection _m = _regex.Matches(htmlCode);

            for (int i = 0; i < _m.Count; i++)
            {
                bool _rep = false;
                string _strNew = _m[i].ToString();

                // 过滤重复的URL
                foreach (string str in _al)
                {
                    if (str == _strNew)
                    {
                        _rep = true;
                        break;
                    }
                }

                if (!_rep) _al.Add(_strNew);
            }

            _al.Sort();

            return _al;
        }

        /// <summary>
        /// 返回HTML代码中的所有a标签列表
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns>a标签列表</returns>
        public static ArrayList GetHTMLATag(string htmlCode)
        {
            ArrayList _al = new ArrayList();

            Regex _regex = new Regex(patternGetTAGA, RegexOptions.IgnoreCase);
            MatchCollection _m = _regex.Matches(htmlCode);

            for (int i = 0; i < _m.Count; i++)
            {
                bool _rep = false;
                string _strNew = _m[i].ToString();

                // 过滤重复的URL
                foreach (string str in _al)
                {
                    if (str == _strNew)
                    {
                        _rep = true;
                        break;
                    }
                }

                if (!_rep) _al.Add(_strNew);
            }

            return _al;
        }

        /// <summary>
        /// 返回HTML a标签中的href内容
        /// <para>如:<a href="http://www.baidu.com/">百度</a></para> 返回字符串:"http://www.baidu.com/"
        /// </summary>
        /// <param name="htmlCode">HTML代码</param>
        /// <returns>a标签中的href内容</returns>
        public static string GetHrefString(string htmlCode)
        {
            return GetString(htmlCode, patternGetHref);
        }

        /// <summary>
        /// 去除HTML标签
        /// </summary>
        /// <param name="htmlCode">HTML代码</param>
        /// <returns></returns>
        public static string RemoveHtmlTag(string htmlCode)
        {
            return ReplaceHtmlTag(htmlCode, "");
        }

        /// <summary>
        /// 替换HMTL标签内容
        /// </summary>
        /// <param name="htmlCode">HTML代码</param>
        /// <param name="replaceMent">替换内容</param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string htmlCode, string replaceMent)
        {
            return Regex.Replace(htmlCode, "<[^>]*>", replaceMent, RegexOptions.IgnoreCase);
        }
    }
}
