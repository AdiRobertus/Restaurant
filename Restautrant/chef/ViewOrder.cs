using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restautrant.admin
{
    public partial class ViewOrder : Core
    {
        public ViewOrder()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            string id = cbOrder.Text.ToString();
            dataGridView1.DataSource = db.orderDetail
                .Where(X => X.Orderid == id)
                .Select(X => new {
                    X.Id,
                    X.MsMenu.Name,
                    X.Qty
                }).ToArray();

            dataGridView1.Columns["Id"].Visible = false;
        }

        void LoadData2()
        {
            dataGridView2.DataSource = db.orderDetail
                .Select(X => new {
                    X.Id,
                    X.MsMenu.Name,
                    X.Qty
                }).ToArray();
        }

        void cbLoader()
        {
            if (cbSort.SelectedItem == "Id")
                dataGridView2.DataSource = db.orderDetail
                .Select(X => new {
                    X.Id,
                    X.MsMenu.Name,
                    X.Qty
                }).OrderBy(X => X.Id)
                .ToArray();

            if (cbSort.SelectedItem == "Name")
                dataGridView2.DataSource = db.orderDetail
                .Select(X => new {
                    X.Id,
                    X.MsMenu.Name,
                    X.Qty
                }).OrderBy(X => X.Name)
                .ToArray();

            if (cbSort.SelectedItem == "Qty")
                dataGridView2.DataSource = db.orderDetail
                .Select(X => new {
                    X.Id,
                    X.MsMenu.Name,
                    X.Qty
                }).OrderBy(X => X.Qty)
                .ToArray();
        }
        private void ViewOrder_Load(object sender, EventArgs e)
        {
            cbOrder.DisplayMember = "Orderid";
            cbOrder.ValueMember = "Id";

            cbOrder.DataSource = db.orderDetail
                .Select(X => new {
                    X.Id,
                    X.Orderid
                }).ToArray();

            var cbx = new DataGridViewComboBoxColumn();
            cbx.HeaderText = "";
            cbx.Items.Add("Deliver");
            cbx.Items.Add("Cooking");
            cbx.Items.Add("Preparing");

            dataGridView1.Columns.Add(cbx);

            LoadData2();
        }

        private void cbOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbLoader();
        }
    }
}
