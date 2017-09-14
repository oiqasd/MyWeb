using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSSqlVisual
{
    public partial class TablesForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTables();
                drpPageIndex.SelectedIndex = 0;
                GetData();
            }
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        void GetTables()
        {
            drpTable.DataTextField = "TABLE_NAME";
            drpTable.DataValueField = "TABLE_NAME";
            drpTable.DataSource = DataAccess.Instance.GetTablesName("TopJetP2P");
            drpTable.DataBind();
        }

        void GetData()
        {

            int index = drpPageIndex.SelectedIndex;
            int pages;

            //tbdataGrid.DataSource = DataAccess.Instance.GetDataByTable(drpTable.SelectedValue, Convert.ToInt32(drpPageSize.SelectedValue), index, out  pages);
            //tbdataGrid.DataBind();

            tbview.DataSource = DataAccess.Instance.GetDataByTable(drpTable.SelectedValue, Convert.ToInt32(drpPageSize.SelectedValue), index, out  pages);
            tbview.DataBind();

            if (pages != drpPageIndex.Items.Count)
            {
                List<PageModel> pagelist = new List<PageModel>();
                if (pages > 0)
                    for (int i = 1; i <= pages; i++)
                    {
                        pagelist.Add(new PageModel() { Value = i, Name = i });
                    }
                else
                    pagelist.Add(new PageModel() { Value = 1, Name = 1 });

                drpPageIndex.DataTextField = "Value";
                drpPageIndex.DataValueField = "Name";
                drpPageIndex.DataSource = pagelist;
                drpPageIndex.DataBind();
            }

        }



        protected void btnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        /// <summary>
        /// 表名重选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

    }
}