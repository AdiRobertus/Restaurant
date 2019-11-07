using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restautrant.main
{
    public partial class Report : Core
    {
        public Report()
        {
            InitializeComponent();
        }

        List<orderDetail> orderDetails = new List<orderDetail>();

        void chart()
        {
            
        }

        void combobox1()
        {
            comboBox1.Items.Add("Januari");
            comboBox1.Items.Add("Februari");
            comboBox1.Items.Add("Maret");
            comboBox1.Items.Add("April");
            comboBox1.Items.Add("Mei");
            comboBox1.Items.Add("Juni");
            comboBox1.Items.Add("Juli");
            comboBox1.Items.Add("Agustus");
            comboBox1.Items.Add("September");
            comboBox1.Items.Add("Oktober");
            comboBox1.Items.Add("November");
            comboBox1.Items.Add("Desember");
        }

        void combobox2()
        {
            comboBox2.Items.Add("Januari");
            comboBox2.Items.Add("Februari");
            comboBox2.Items.Add("Maret");
            comboBox2.Items.Add("April");
            comboBox2.Items.Add("Mei");
            comboBox2.Items.Add("Juni");
            comboBox2.Items.Add("Juli");
            comboBox2.Items.Add("Agustus");
            comboBox2.Items.Add("September");
            comboBox2.Items.Add("Oktober");
            comboBox2.Items.Add("November");
            comboBox2.Items.Add("Desember");
        }

        void LoadData()
        {
            dataGridView1.DataSource = db.orderDetail
                .Select(X => new
                {
                    X.Id,
                    Month = X.OrderHeader.Date.Month,
                    Income = orderDetails.Sum(S => S.MsMenu.Price * S.Qty)
                }).ToArray();
        }
        private void Report_Load(object sender, EventArgs e)
        {
            combobox1();
            combobox2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
