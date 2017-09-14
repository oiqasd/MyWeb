using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProcessKill
{
    public partial class frmProcessKill : Form
    {
        public frmProcessKill()
        {
            InitializeComponent();

        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (listProcess.SelectedItems.Count <= 0)
            {
                return;
            }
            Process[] process = Process.GetProcesses();

            foreach (Process pro in process)
            {
                if (pro.ProcessName.ToLower() == listProcess.SelectedItems[0].Text.ToLower())
                {
                    pro.Kill();
                }
            }

            MessageBox.Show("执行完毕");
            DataLoad();
        }

        private void frmProcessKill_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        void DataLoad()
        {
            Process[] process = Process.GetProcesses();

            listProcess.Clear();
            listProcess.Columns.Add("Name");
            listProcess.Columns.Add("Id");
            listProcess.Columns.Add("StartTime");
            listProcess.Columns.Add("DicInfo");
            listProcess.View = System.Windows.Forms.View.Details;
            listProcess.FullRowSelect = true;
            listProcess.GridLines = true;
            listProcess.Columns[0].Width = 100;
            listProcess.Columns[1].Width = 50;
            listProcess.Columns[2].Width = 120;
            listProcess.Columns[3].Width = 120;
            listProcess.MultiSelect = false;
            listProcess.BeginUpdate();
            foreach (Process pro in process)
            {
                ListViewItem item = new ListViewItem();
                item.Text = pro.ProcessName;
                item.SubItems.Add(pro.Id.ToString());
                try
                {
                    item.SubItems.Add(pro.StartTime.ToString());
                }
                catch (Exception ex)
                {
                    item.SubItems.Add(ex.Message);
                }
                try
                {
                    item.SubItems.Add(pro.MainModule.FileName.ToString());
                }
                catch (Exception ex)
                {
                    item.SubItems.Add(ex.Message);
                }

                listProcess.Items.Add(item);

            }

            listProcess.EndUpdate();
        }
    }
}
