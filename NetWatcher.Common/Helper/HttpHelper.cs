using System.IO;
using System.Net;
using System.Text;

namespace NetWatcher.Common.Helper
{
    /// <summary>
    /// Html请求帮助类
    /// <para>仿造【博客园 - 上位者的怜悯 原文地址："http://www.cnblogs.com/lianmin/p/4227242.html"】</para>
    /// <para>创建：2015.11.05</para>
    /// </summary>
    public class HttpHelper
    {
        // 用于保存Cookie
        private CookieCollection _cookieCollection = new CookieCollection();
        public CookieCollection CookieCollection
        {
            get { return _cookieCollection; }
            set { _cookieCollection = value; }
        }

        private CookieContainer _cookieContainer = new CookieContainer();
        public CookieContainer CookieContainer
        {
            get { return _cookieContainer; }
            set { _cookieContainer = value; }
        }

        // 请求头
        private string _referer;
        public string Referer
        {
            get { return _referer; }
            set { _referer = value; }
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encode">编码方式</param>
        /// <param name="saveCookie">是否保存Cookie信息</param>
        /// <returns></returns>
        public string GetHtml(string url, Encoding encode, bool saveCookie)
        {
            HttpWebRequest _hrq = WebRequest.Create(url) as HttpWebRequest;
            _hrq.CookieContainer = CookieContainer;
            if (!string.IsNullOrWhiteSpace(Referer))
                _hrq.Referer = Referer;


            HttpWebResponse _hrp;
            try
            {
                _hrp = _hrq.GetResponse() as HttpWebResponse;

                if (saveCookie)
                {
                    CookieCollection = _hrp.Cookies;
                    CookieContainer.GetCookies(_hrq.RequestUri);
                }

                Referer = string.Empty;

                using (StreamReader _sr = new StreamReader(_hrp.GetResponseStream(), encode))
                {
                    return _sr.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse _response = ex.Response as HttpWebResponse;

                if (_response != null && _response.StatusCode == HttpStatusCode.NotFound)
                    return "404";
                else
                    return ex.Message;

            }
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postStr">请求参数</param>
        /// <param name="encode">编码方式</param>
        /// <param name="saveCookie">是否保存Cookie信息</param>
        /// <returns></returns>
        public string PostHtml(string url, string postStr, Encoding encode, bool saveCookie)
        {
            HttpWebRequest _hrq = WebRequest.Create(url) as HttpWebRequest;
            _hrq.CookieContainer = CookieContainer;
            if (!string.IsNullOrWhiteSpace(Referer))
                _hrq.Referer = Referer;
            _hrq.Method = "POST";
            _hrq.ContentType = "application/x-www-form-urlencoded";
            _hrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.71 Safari/537.36";
            _hrq.ProtocolVersion = HttpVersion.Version10;

            byte[] _bytes = encode.GetBytes(postStr);
            _hrq.ContentLength = _bytes.Length;
            using (Stream _stream = _hrq.GetRequestStream())
            {
                _stream.Write(_bytes, 0, _bytes.Length);
            }

            HttpWebResponse _hrp = _hrq.GetResponse() as HttpWebResponse;

            if (saveCookie)
            {
                CookieCollection = _hrp.Cookies;
                CookieContainer.GetCookies(_hrq.RequestUri);
            }

            Referer = string.Empty;

            using (StreamReader _sr = new StreamReader(_hrp.GetResponseStream(), encode))
            {
                return _sr.ReadToEnd();
            }
        }
    }
}
