using NetWatcher.Common.Helper;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NetWatcher.GetAreaForGuoJiaTongJiJu
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// HTTP请求帮助类
        /// </summary>
        HttpHelper _httpHelper = new HttpHelper();
        /// <summary>
        /// 网页编码方式
        /// </summary>
        Encoding encode = Encoding.GetEncoding("gb2312");
        object areaType;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新获取区域信息进度
        /// </summary>
        /// <param name="message">信息</param>
        private void SetLabelMessage(string message)
        {
            lbMessage.Text = message;
            Application.DoEvents(); // 需要快速更新label Text信息时，务必添加此行，否则可能没有信息更新效果
        }

        /// <summary>
        /// 添加节点，并更新区域信息获取进度
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="parentNode"></param>
        private void AddNodeToAreaTreeView(TreeNode treeNode, TreeNode parentNode)
        {
            string _message = string.Empty;
            string separator = " -> "; //分隔符

            if (parentNode != null)
            {
                _message = parentNode.FullPath.Replace("\\", separator) + separator + treeNode.Text;
                parentNode.Nodes.Add(treeNode);
            }
            else
            {
                _message = treeNode.Text;
                treeArea.Nodes.Add(treeNode);
            }

            SetLabelMessage(_message);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/");
        }

        private void btnGetAreaInfo_Click(object sender, EventArgs e)
        {
            DateTime _startTime = DateTime.Now;

            SetLabelMessage("程序正在获取远程数据，请稍候...");
            treeArea.Nodes.Clear();

            try
            {
                Uri _areaAddress;

                if (string.IsNullOrWhiteSpace(txtAreaAddress.Text) || !Uri.TryCreate(txtAreaAddress.Text, UriKind.Absolute, out _areaAddress))
                {
                    MessageBox.Show("请输入正确的URL地址（如：【http://www.baidu.com】）");
                    return;
                }

                var _domCode = _httpHelper.GetHtml(txtAreaAddress.Text, encode, false);

                if (!string.IsNullOrWhiteSpace(_domCode))
                {
                    string _areaMark = GetAreaMark(_domCode); // 获取区域类型（省/市/镇...)

                    if (string.IsNullOrWhiteSpace(_areaMark))
                    {
                        MessageBox.Show("未知区域！不能获取区域信息");
                        return;
                    }

                    areaType = Enum.Parse(typeof(AreaType), _areaMark);

                    switch ((AreaType)areaType)
                    {
                        case AreaType.province: GetProvince(_areaAddress, _domCode); break;
                        case AreaType.city: GetCity(_areaAddress, _domCode, null); break;
                        case AreaType.county: GetCounty(_areaAddress, _domCode, null); break;
                        case AreaType.town: GetTown(_areaAddress, _domCode, null); break;
                        case AreaType.village: GetVillage(_areaAddress, _domCode, null); break;
                        default: break;
                    }
                }

                // Clipboard.SetDataObject(_domCode); // 将内容放入剪贴板中
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序发现异常信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DateTime _endTime = DateTime.Now;

            SetLabelMessage($"程序执行完毕。用时：{ _endTime - _startTime }");
        }

        /// <summary>
        /// 获取区域类型
        /// </summary>
        /// <param name="areaHtml"></param>
        /// <returns></returns>
        private string GetAreaMark(string areaHtml)
        {
            Type _areaMark = typeof(AreaType);

            foreach (string _s in Enum.GetNames(_areaMark))
            {
                if (areaHtml.Contains($"class=\"{_s}table\"") || areaHtml.Contains($"class='{_s}table'"))
                    return _s;
            }

            return "";
        }

        #region 返回区域信息-old

        ///// <summary>
        ///// 省级信息
        ///// </summary>
        ///// <returns></returns>
        //private bool GetProvince(string domCode, out string areaId, out string areaName)
        //{
        //    string _pattern = "[\\d{6}]+";

        //    areaId = MatchHelper.GetString(domCode, _pattern);
        //    areaName = MatchHelper.RemoveHtmlTag(domCode);

        //    return !string.IsNullOrWhiteSpace(areaId);
        //}

        ///// <summary>
        ///// 市/区信息
        ///// </summary>
        ///// <returns></returns>
        //private bool GetCity(string domCode, out string areaId, out string areaName)
        //{
        //    string _pattern = "[\\d{6}]+";

        //    areaId = MatchHelper.GetString(domCode, _pattern);
        //    areaName = MatchHelper.RemoveHtmlTag(domCode);

        //    return !string.IsNullOrWhiteSpace(areaId);
        //}

        ///// <summary>
        ///// 县
        ///// </summary>
        ///// <returns></returns>
        //private bool GetCounty(string domCode, out string areaId, out string areaName)
        //{
        //    string _pattern = "[\\d{6}]+";

        //    areaId = MatchHelper.GetString(domCode, _pattern);
        //    areaName = MatchHelper.RemoveHtmlTag(domCode);

        //    return !string.IsNullOrWhiteSpace(areaId);
        //}

        ///// <summary>
        ///// 镇
        ///// </summary>
        ///// <returns></returns>
        //private bool GetTown(string domCode, out string areaId, out string areaName)
        //{
        //    string _pattern = "[\\d{6}]+";

        //    areaId = MatchHelper.GetString(domCode, _pattern);
        //    areaName = MatchHelper.RemoveHtmlTag(domCode);

        //    return !string.IsNullOrWhiteSpace(areaId);
        //}

        ///// <summary>
        ///// 村
        ///// </summary>
        ///// <returns></returns>
        //private bool GetVillage(string domCode, out string areaId, out string areaName)
        //{
        //    string _pattern = "[\\d{6}]+";

        //    areaId = MatchHelper.GetString(domCode, _pattern);
        //    areaName = MatchHelper.RemoveHtmlTag(domCode);

        //    return !string.IsNullOrWhiteSpace(areaId);
        //}

        #endregion
        #region 返回区域信息

        /// <summary>
        /// 省级信息
        /// </summary>
        /// <param name="uri">页面URL地址</param>
        /// <param name="domCode">HTML代码</param>
        private void GetProvince(Uri uri, string domCode)
        {
            string _pattern = "<tr class=(\"|')provincetr(\"|')>([\\s\\S]*?)<\\/tr>";
            string[] _trDoms = MatchHelper.GetStrings(domCode, _pattern);

            foreach (string _trDomStr in _trDoms)
            {
                ArrayList _alLinks = MatchHelper.GetHTMLATag(_trDomStr);
                foreach (object item in _alLinks)
                {
                    string _strCUrl = MatchHelper.GetHrefString(item.ToString());
                    string _areaId = MatchHelper.GetNumber(item.ToString());
                    string _areaName = MatchHelper.RemoveHtmlTag(item.ToString());

                    TreeNode _treeNode = new TreeNode() { Text = $"{_areaName}（{_areaId}）" };
                    AddNodeToAreaTreeView(_treeNode, null);

                    if (_treeNode != null)
                    {
                        // 获取下级区域信息，如果有则加载
                        Uri _uriChildren; // 下级区域的Url地址
                        if (Uri.TryCreate(uri, _strCUrl, out _uriChildren))
                        {
                            string _url = _uriChildren.AbsoluteUri;

                            // 获取数据
                            string _childrenPageDom = _httpHelper.GetHtml(_url, encode, false);

                            if (!string.IsNullOrWhiteSpace(_childrenPageDom) && !_childrenPageDom.Equals("404"))
                                GetCity(_uriChildren, _childrenPageDom, _treeNode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 市/区信息
        /// </summary>
        /// <param name="uri">页面URL地址</param>
        /// <param name="domCode">HTML代码</param>
        /// <param name="parentNode">父节点</param>
        private void GetCity(Uri uri, string domCode, TreeNode parentNode)
        {
            string _pattern = "<tr class=(\"|')citytr(\"|')>([\\s\\S]*?)<\\/tr>";
            string[] _trDoms = MatchHelper.GetStrings(domCode, _pattern);

            foreach (string _trDomStr in _trDoms)
            {
                ArrayList _alLinks = MatchHelper.GetHTMLATag(_trDomStr);
                if (_alLinks.Count > 0 && _alLinks.Count % 2 == 0)
                {
                    string _strCUrl = MatchHelper.GetHrefString(_alLinks[0].ToString());
                    string _areaId = MatchHelper.RemoveHtmlTag(_alLinks[0].ToString()).Substring(0, 4);
                    string _areaName = MatchHelper.RemoveHtmlTag(_alLinks[1].ToString());

                    TreeNode _treeNode = new TreeNode() { Text = $"{_areaName}（{_areaId}）" };

                    AddNodeToAreaTreeView(_treeNode, parentNode);

                    if (_treeNode != null)
                    {
                        // 获取下级区域信息，如果有则加载
                        Uri _uriChildren; // 下级区域的Url地址
                        if (Uri.TryCreate(uri, _strCUrl, out _uriChildren))
                        {
                            string _url = _uriChildren.AbsoluteUri;

                            // 获取数据
                            string _childrenPageDom = _httpHelper.GetHtml(_url, encode, false);

                            if (!string.IsNullOrWhiteSpace(_childrenPageDom) && !_childrenPageDom.Equals("404"))
                                GetCounty(_uriChildren, _childrenPageDom, _treeNode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 县
        /// </summary>
        /// <param name="uri">页面URL地址</param>
        /// <param name="domCode">HTML代码</param>
        /// <param name="parentNode">父节点</param>
        private void GetCounty(Uri uri, string domCode, TreeNode parentNode)
        {
            string _pattern = "<tr class=(\"|')countytr(\"|')>([\\s\\S]*?)<\\/tr>";
            string[] _trDoms = MatchHelper.GetStrings(domCode, _pattern);

            foreach (string _trDomStr in _trDoms)
            {
                ArrayList _alLinks = MatchHelper.GetHTMLATag(_trDomStr);
                if (_alLinks.Count > 0 && _alLinks.Count % 2 == 0)
                {
                    string _strCUrl = MatchHelper.GetHrefString(_alLinks[0].ToString());
                    string _areaId = MatchHelper.RemoveHtmlTag(_alLinks[0].ToString()).Substring(0, 6);
                    string _areaName = MatchHelper.RemoveHtmlTag(_alLinks[1].ToString());

                    TreeNode _treeNode = new TreeNode() { Text = $"{_areaName}（{_areaId}）" };

                    AddNodeToAreaTreeView(_treeNode, parentNode);

                    if (_treeNode != null)
                    {
                        // 获取下级区域信息，如果有则加载
                        Uri _uriChildren; // 下级区域的Url地址
                        if (Uri.TryCreate(uri, _strCUrl, out _uriChildren))
                        {
                            string _url = _uriChildren.AbsoluteUri;

                            // 获取数据
                            string _childrenPageDom = _httpHelper.GetHtml(_url, encode, false);

                            if (!string.IsNullOrWhiteSpace(_childrenPageDom) && !_childrenPageDom.Equals("404"))
                                GetTown(_uriChildren, _childrenPageDom, _treeNode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 镇
        /// </summary>
        /// <param name="uri">页面URL地址</param>
        /// <param name="domCode">HTML代码</param>
        /// <param name="parentNode">父节点</param>
        private void GetTown(Uri uri, string domCode, TreeNode parentNode)
        {
            string _pattern = "<tr class=(\"|')towntr(\"|')>([\\s\\S]*?)<\\/tr>";
            string[] _trDoms = MatchHelper.GetStrings(domCode, _pattern);

            foreach (string _trDomStr in _trDoms)
            {
                ArrayList _alLinks = MatchHelper.GetHTMLATag(_trDomStr);
                if (_alLinks.Count > 0 && _alLinks.Count % 2 == 0)
                {
                    string _strCUrl = MatchHelper.GetHrefString(_alLinks[0].ToString());
                    string _areaId = MatchHelper.RemoveHtmlTag(_alLinks[0].ToString()).Substring(0, 9);
                    string _areaName = MatchHelper.RemoveHtmlTag(_alLinks[1].ToString());

                    TreeNode _treeNode = new TreeNode() { Text = $"{_areaName}（{_areaId}）" };

                    AddNodeToAreaTreeView(_treeNode, parentNode);

                    //if (_treeNode != null)
                    //{
                    //    // 获取下级区域信息，如果有则加载
                    //    Uri _uriChildren; // 下级区域的Url地址
                    //    if (Uri.TryCreate(uri, _strCUrl, out _uriChildren))
                    //    {
                    //        string _url = _uriChildren.AbsoluteUri;

                    //        // 获取数据
                    //        string _childrenPageDom = _httpHelper.GetHtml(_url, encode, false);

                    //        if (!string.IsNullOrWhiteSpace(_childrenPageDom) && !_childrenPageDom.Equals("404"))
                    //            GetTown(_uriChildren, _childrenPageDom, _treeNode);
                    //    }
                    //}
                }
            }
        }

        /// <summary>
        /// 村
        /// </summary>
        /// <param name="uri">页面URL地址</param>
        /// <param name="domCode">HTML代码</param>
        /// <param name="parentNode">父节点</param>
        private void GetVillage(Uri uri, string domCode, TreeNode parentNode)
        {
            string _pattern = "<tr class=(\"|')villagetr(\"|')>([\\s\\S]*?)<\\/tr>";
        }

        #endregion

        private void treeArea_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
    }

    /// <summary>
    /// 区域类型
    /// </summary>
    public enum AreaType
    {
        /// <summary>
        /// 省
        /// </summary>
        province,
        /// <summary>
        /// 市/区
        /// </summary>
        city,
        /// <summary>
        /// 县
        /// </summary>
        county,
        /// <summary>
        /// 镇
        /// </summary>
        town,
        /// <summary>
        /// 村
        /// </summary>
        village
    }
}
