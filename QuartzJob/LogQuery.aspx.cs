using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuartzJob
{
    public partial class LogQuery : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
            }
            if (hifAdress.Value != "")
            {
                ExecStartupScript("document.getElementById('iframequery').src='" + this.hifAdress.Value.Substring(1) + "?date=" + DateTime.Now + "';");
            }
        }
        /// <summary>
        /// 页面加载完成后执行客户端脚本
        /// </summary>
        /// <param name="script">要执行的函数Refresh();...</param>
        public void ExecStartupScript(string script)
        {
            ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page, typeof(Page), Guid.NewGuid().ToString(), script, true);
        }
        /// <summary>
        /// /弹出消息
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void Alert(string msg)
        {
            msg = msg.Replace("\r\n", "\\r\\n").Replace("'", "\\'");
            string info = "alert('" + msg + "');";
            ExecStartupScript(info);
        }
        /// <summary>
        /// 绑定树
        /// </summary>
        private void BindTree()
        {
            var RelationPath = "UploadFile";
            string path = HttpContext.Current.Server.MapPath(RelationPath) + @"\Log"; //+ @"\Log\Exception"   //Server.MapPath(". ") + @"\UploadFile\Log";
            TreeNode tr = new TreeNode("Web日志", "A1");
            string url = HttpContext.Current.Request.Url.ToString();
            string no_http = url.Substring(url.IndexOf("//") + 2);
            string host_url = "http://" + no_http.Substring(0, no_http.IndexOf("/") + 1);
            Hidhttphost.Value = host_url;// url.Substring(0, url.IndexOf("JobConfiguration"));
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] dis = di.GetDirectories();
            foreach (DirectoryInfo diss in dis)
            {
                TreeNode td = new TreeNode();
                td.Text = diss.Name;
                td.Value = "B1" + diss.Name;
                td.CollapseAll();
                DirectoryInfo[] disOK = diss.GetDirectories();
                foreach (DirectoryInfo disk in disOK)
                {
                    FileInfo[] fs = disk.GetFiles();
                    if (fs.Length > 0)
                    {
                        if (fs.Length == 1)
                        {
                            if (fs[0].Length > 0)
                            {
                                TreeNode tn = new TreeNode();
                                tn.Text = diss.Name + @"/" + disk.Name;
                                tn.Value = "A" + Hidhttphost.Value + RelationPath + @"/Log/" + disk.Parent.ToString() + @"/" + disk.Name + @"/" + fs[0].Name;
                                td.ChildNodes.AddAt(0, tn);
                                if (DateTime.Parse(disk.Name) >= DateTime.Now.Date)
                                {
                                    tn.ExpandAll();
                                    tn.Parent.Expand();
                                }
                            }
                        }
                        else
                        {
                            TreeNode tn = new TreeNode();
                            tn.Text = diss.Name + @"/" + disk.Name;
                            tn.Value = "B1" + Hidhttphost.Value + RelationPath + @"/Log/" + disk.Parent.ToString() + @"/" + disk.Name + @"/" + fs[0].Name;
                            tn.Collapse();
                            for (int i = 0; i < fs.Length; i++)
                            {
                                if (fs[i].Length > 0)
                                {
                                    TreeNode tn1 = new TreeNode();
                                    tn1.Text = fs[i].Name.Replace(@"Exception@", "");
                                    tn1.Value = "A" + Hidhttphost.Value + RelationPath + @"/Log/" + disk.Parent.ToString() + @"/" + disk.Name + @"/" + fs[i].Name;
                                    tn.ChildNodes.AddAt(0, tn1);
                                }
                            }
                            td.ChildNodes.AddAt(0, tn);
                            if (DateTime.Parse(disk.Name) >= DateTime.Now.Date)
                            {
                                tn.ExpandAll();
                                tn.Parent.Expand();
                            }
                        }
                    }
                }
                tr.ChildNodes.AddAt(0, td);
            }
            this.TreeView1.Nodes.AddAt(0, tr);
        }

        /// <summary>
        /// 树节点选择改变事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (this.TreeView1.SelectedNode.Value != "A1" && this.TreeView1.SelectedNode.Value != "B1" && this.TreeView1.SelectedNode.Value.Substring(0, 2) != "B1")
            {
                string address = this.TreeView1.SelectedNode.Value.Substring(1);
                this.hifAdress.Value = this.TreeView1.SelectedNode.Value;
                ExecStartupScript("document.getElementById('iframequery').src='" + address + "?date=" + DateTime.Now + "';");

            }
            else
            {
                this.hifAdress.Value = "";
            }
        }
    }

}