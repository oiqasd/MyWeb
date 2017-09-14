using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSSqlVisual
{
    public partial class custom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpTable.DataTextField = "TABLE_NAME";
                drpTable.DataValueField = "TABLE_NAME";
                drpTable.DataSource = DataAccess.Instance.GetTablesName("TopJetP2P");
                drpTable.DataBind();

                GetTables();
            }
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        void GetTables()
        {

            if (!string.IsNullOrEmpty(drpTable.SelectedValue))
            {
                var data = DataAccess.Instance.GetColumnsNames(drpTable.SelectedValue);

                drpCol.DataTextField = "NAME";
                drpCol.DataValueField = "NAME";

                drpCol.DataSource = data;
                drpCol.DataBind();


                drpsel.DataTextField = "NAME";
                drpsel.DataValueField = "NAME";

                drpsel.DataSource = data;
                drpsel.DataBind();


                lblMsg.Text = string.Empty;
            }
        }

        protected void drpTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTables();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtvalue.Text))
            {
                lblMsg.Text = "字段值不能为空。";
                txtvalue.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtsel.Text))
            {
                lblMsg.Text = "条件不能为空。";
                txtsel.Focus();
                return;
            }

            string sql = string.Format("update {0} set {1}='{2}' where {3}='{4}'", drpTable.SelectedValue, drpCol.SelectedValue, txtvalue.Text.Trim(), drpsel.SelectedValue, txtsel.Text.Trim());

            var ret = DataAccess.Instance.ExcuseSql(sql);

            lblMsg.Text = ret.ToString();
        }


    }
}