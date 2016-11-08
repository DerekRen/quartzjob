using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzJob.Common
{
    class PageStyle
    {
        private static string prvePage = "上一页";
        private static string nextPage = "下一页";
        #region 分页函数(控件+样式)
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="currentPage">当前页码</param>
        /// <param name="totalRecord">记录总条数</param>
        /// <param name="perPageCount">每页显示条数</param>
        /// <param name="strPage">自由内容,传入空则为"共?页"</param>
        /// <param name="urlLink">分页地址</param>
        /// <param name="title">写在"第N页"之前的话:鼠标放在页码上会有 title + "第N页"</param>
        /// <param name="flag">0.html--url虚拟化</param>
        /// <param name="newMonthed">是否是新方法</param>
        /// <param name="strMethod">特殊调用js的方法名称:QueryDataList</param>
        /// <returns></returns>
        public static string DisplayPagers(int currentPage, int totalRecord, int perPageCount, string strPage, string urlLink, string title, int flag, string strMethod)
        {
            int totalPageCount = 1;//总页数
            //int maxPages = 15000;//允许呈现的最大页码
            int extendPages = 5;//以当前页码为中心,两侧允许出现的最多页码数(包含[..]和固定页码),如:[1][2][..][50][51][52(中心)][53][54][..][98][99]
            int mustShowPages = 2;//在首尾必须显示的页码数,如:[1][2]...[]...[98][99]

            var strJsContent = string.Empty;
            if (!String.IsNullOrEmpty(strMethod))
            {
                strJsContent = "javascript:" + strMethod + "({0});";
            }
            else
            {
                strJsContent = "javascript:QueryDataList({0});";
            }
            if (totalRecord % perPageCount == 0)
            {
                totalPageCount = (totalRecord / perPageCount);
            }
            else
            {
                totalPageCount = (totalRecord / perPageCount) + 1;
            }

            //if (totalPageCount >= maxPages)//限定页数不超过maxPages(99)
            //{
            //    totalPageCount = maxPages;
            //}

            if (currentPage > totalPageCount)//如果当前页数大于总页数,调整为最后页
            {
                currentPage = totalPageCount;
            }
            if (currentPage <= 0)//如果当前页数小于等于0,调整为第一页
            {
                currentPage = 1;
            }

            //开始组合分页控件
            StringBuilder strControl = new StringBuilder();
            strControl.Append("<div class='list_search_page'>");
            strControl.Append("<div class=\"pagination\">");
            strControl.AppendFormat("<span style=\"float: left; margin: 2px;\">页次：{0}/{1}页 共{2}条</span>", currentPage.ToString(), totalPageCount.ToString(), totalRecord.ToString());
            if (currentPage > 1)//如果当前页不等于1则显示上一页
            {
                if (flag == 0)
                {
                    strControl.AppendFormat("<a title='首页' href=\"{0}\" class=\"first_page02 border_gray\">首页</a>", urlLink + "1.html");
                    strControl.AppendFormat("<a title='{1}' href=\"{0}\" class='previous_page02 border_gray'>{1}</a>", urlLink + (currentPage - 1).ToString() + ".html", prvePage, (currentPage - 1).ToString());
                }
                else
                {
                    strControl.AppendFormat("<a title='首页' href=\"{0}\" class=\"first_page02 border_gray\">首页</a>", String.Format(strJsContent, 1));
                    strControl.AppendFormat("<a title='{0}' href=\"{1}\" class='previous_page02 border_gray'>{0}</a>", prvePage, String.Format(strJsContent, (currentPage - 1)));
                }
            }
            else
            {
                strControl.Append("<span title=\"首页\" class=\"first_page01 border_gray\">首页</span>");
                strControl.Append("<span title=\"上一页\" class=\"previous_page01 border_gray\">上一页</span>");
            }

            if (totalPageCount <= extendPages + 1)//如果总页数小于等于6则页数全部显示
            {
                for (int i = 1; i <= totalPageCount; i++)
                {
                    if (i == currentPage)
                    {
                        strControl.AppendFormat("<span class='on_page border_gray'>{0}</span>", i.ToString());
                    }
                    else
                    {
                        if (flag == 0)
                        {
                            strControl.AppendFormat("<a title='{2}第{1}页'  href=\"{0}\" class='choose_page border_gray'>{1}</a>", urlLink + i.ToString() + ".html", i.ToString(), title);
                        }
                        else
                        {
                            strControl.AppendFormat("<a title='{0}第{1}页'  href=\"{2}\" class='choose_page border_gray'>{1}</a>", title, i, String.Format(strJsContent, i));
                        }
                    }
                }
            }
            else//如果总页数大于6
            {
                //中间页码部位
                for (int i = 0; i <= extendPages * 2; i++)//extendPages * 2(10)|mustShowPages(2)
                {
                    if (i < mustShowPages || i > extendPages * 2 - mustShowPages)//头尾页码
                    {
                        if ((i + 1 == currentPage && i < mustShowPages) || (totalPageCount - extendPages * 2 + i == currentPage && i > extendPages * 2 - mustShowPages))//当前页
                        {
                            strControl.AppendFormat("<span class='on_page border_gray'>{0}</span>", currentPage.ToString());
                        }
                        else if (i < mustShowPages)
                        {
                            if (flag == 0)
                            {
                                strControl.AppendFormat("<a title='{2}第{1}页'  href=\"{0}\" class='choose_page border_gray'>{1}</a>", urlLink + (i + 1).ToString() + ".html", (i + 1).ToString(), title);
                            }
                            else
                            {
                                strControl.AppendFormat("<a title='{0}第{1}页' href=\"{2}\" class='choose_page border_gray'>{1}</a>", title, i + 1, String.Format(strJsContent, i + 1));
                            }
                        }
                        else if (i > extendPages * 2 - mustShowPages)
                        {
                            if (flag == 0)
                            {
                                strControl.AppendFormat("<a title='{2}第{1}页'  href=\"{0}\" class='choose_page border_gray'>{1}</a>", urlLink + (totalPageCount - extendPages * 2 + i).ToString() + ".html", (totalPageCount - extendPages * 2 + i).ToString(), title);
                            }
                            else
                            {
                                strControl.AppendFormat("<a title='{0}第{1}页' href=\"{2}\"  class='choose_page border_gray'>{1}</a>", title, (totalPageCount - extendPages * 2 + i), String.Format(strJsContent, (totalPageCount - extendPages * 2 + i)));
                            }
                        }
                    }
                    else if ((currentPage - extendPages + i > mustShowPages) && (currentPage - extendPages + i <= totalPageCount - mustShowPages))//中部页码
                    {
                        if (i == extendPages)//当前页
                        {
                            strControl.AppendFormat("<span class='on_page border_gray'>{0}</span>", currentPage.ToString());
                        }
                        else if ((i == mustShowPages && currentPage - 1 > extendPages) || (i == extendPages * 2 - mustShowPages && totalPageCount - currentPage > extendPages))//可能是[...]/[页码]的特殊位置
                        {
                            strControl.Append("<span class='more_page'>...</span>");
                        }
                        else
                        {
                            if (flag == 0)
                            {
                                strControl.AppendFormat("<a title='{2}第{1}页'  href=\"{0}\" class='choose_page border_gray'>{1}</a>", urlLink + (currentPage - extendPages + i).ToString() + ".html", (currentPage - extendPages + i).ToString(), title);
                            }
                            else
                            {
                                strControl.AppendFormat("<a title='{0}第{1}页' href=\"{2}\" class='choose_page border_gray'>{1}</a>", title, (currentPage - extendPages + i), String.Format(strJsContent, (currentPage - extendPages + i)));
                            }
                        }
                    }
                }
            }

            if (currentPage != totalPageCount)//如果当前页不是最后一页,则出现下一页按钮
            {
                if (flag == 0)
                {
                    strControl.AppendFormat("<a title='{1}' href=\"{0}\" class='next_page02 border_gray'>{1}</a>", urlLink + (currentPage + 1).ToString() + ".html", nextPage, (currentPage + 1).ToString());
                    strControl.AppendFormat("<a  title='末页' href=\"{0}\" class=\"last_page02 border_gray\">末页</a>", urlLink + totalPageCount + ".html");
                }
                else
                {
                    strControl.AppendFormat("<a title='{0}' href=\"{1}\" class='next_page02 border_gray'>{0}</a>", nextPage, String.Format(strJsContent, (currentPage + 1)));
                    strControl.AppendFormat("<a title='末页' href=\"{0}\" class=\"last_page02 border_gray\">末页</a>", String.Format(strJsContent, totalPageCount));
                }
            }
            else
            {
                strControl.Append("<span class=\"next_page01 border_gray\">下一页</span>");
                strControl.AppendFormat("<span  class=\"last_page01 border_gray\">末页</span>");
            }
            strControl.Append("</div>");
            strControl.Append("</div>");//分页控件组合完毕

            if (totalRecord <= perPageCount)//只有一页的不显示分页控件
            {
                return "";
            }
            return strControl.ToString();
        }
        #endregion
    }
}
