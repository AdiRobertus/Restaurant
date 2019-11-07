using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restautrant.cashier
{
    public partial class Payment : Core
    {
        private OrderHeader OrderHeader;
        public Payment()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            dataGridView1.DataSource = db.orderDetail
                .Select(X => new {
                    X.Id,
                    X.MsMenu.Name,
                    X.Qty,
                    X.MsMenu.Price,
                    Total = X.Qty * X.MsMenu.Price
                }).ToArray();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            comboBox1.DisplayMember = "Orderid";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource = db.orderDetail
                .Select(X => new {
                    X.Id,
                    X.Orderid
                }).ToArray();
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = comboBox1.Text.ToString();
            dataGridView1.DataSource = db.orderDetail
                .Where(X => X.Orderid == id)
                .Select(X => new {
                    X.Id,
                    X.MsMenu.Name,
                    X.Qty,
                    X.MsMenu.Price,
                    Total = X.Qty * X.MsMenu.Price
                }).ToArray();

            dataGridView1.Columns["Id"].Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderHeader = new OrderHeader();
            OrderHeader.Id = comboBox1.SelectedItem.ToString();
            OrderHeader.Employeeid = "EM0001";
            OrderHeader.Date = DateTime.Now.Date;
            OrderHeader.PaymentType = comboBox1.SelectedItem.ToString();
            OrderHeader.CardNumber = textBox1.Text;
            OrderHeader.Bank = comboBox1.SelectedItem.ToString();

            db.OrderHeader.Add(OrderHeader);
            db.SaveChanges();
            LoadData();
        }
    }
}
