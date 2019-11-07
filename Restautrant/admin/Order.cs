using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restautrant
{
    public partial class Order : Core
    {
        public Order()
        {
            InitializeComponent();
        }

        MsMenu msMenu = new MsMenu();
        orderDetail Order1;
        orderDetail Order2;
        OrderHeader Order3;
        List<orderDetail> OrderDetails = new List<orderDetail>();
        List<OrderHeader> orderHeaders = new List<OrderHeader>();

        void ClearTotal()
        {
            lblCarbo.Text = "Carbo : 0";
            lblProtein.Text = "Protein : 0";
            lblTotal.Text = "0.00";
        }
        void setTotal()
        {
            int qty = int.Parse(textBox2.Text.ToString());
            int carbo = OrderDetails.Sum(X => X.MsMenu.Carbo * qty);
            int protein = OrderDetails.Sum(X => X.MsMenu.Protein * qty);
            double price = OrderDetails.Sum(X => X.MsMenu.Price * qty);

            lblCarbo.Text = $"Carbo : {carbo}";
            lblProtein.Text = $"Protein : {protein}";
            lblTotal.Text = $"Total : {price}";
        }

        public void LoadData()
        {
            dataGridView1.DataSource = db.MsMenu
                .Select(X => new
                {
                    X.id,
                    X.Name,
                    X.Price,
                    X.Carbo,
                    X.Protein
                }).ToArray();

            dataGridView1.Columns["id"].Visible = false;

            dataGridView2.DataSource = OrderDetails
                .Select(X => new {
                    X.Id,
                    Menu = X.MsMenu.Name,
                    Qty = int.Parse(textBox2.Text.ToString()),
                    Carbo = X.MsMenu.Carbo,
                    Protein = X.MsMenu.Protein

                }).ToArray();
            dataGridView2.Columns["Id"].Visible = false;
        }
        
        private void Order_Load(object sender, EventArgs e)
        {
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource = db.MsMember
                .Select(X => new {
                    X.Id,
                    X.Name
                }).ToArray();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox2.Text == null)
            {
                MessageBox.Show("Menu atau Qty wajib diisi");
                return;
            }
            if (Order1 == null)
            {
                MessageBox.Show("Select data first");
                return;
            }

            OrderDetails.Add(Order1);
            setTotal();
            Order1 = null;
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            try { 
                int id = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
                Order1 = db.orderDetail.Find(id);

                msMenu = db.MsMenu.Find(id);

                
                textBox1.Text = msMenu.Name;

                if (textBox1.Text.Equals("Risotto"))
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/risotto.jpeg";
                }
                else if(textBox1.Text.Equals("Lasagna"))
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/lasagna.jpg";
                }
                else if (textBox1.Text.Equals("Mac and Chesse"))
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/mac and cheese.jpg";
                }
                else if (textBox1.Text.Equals("Pepperoni Pizza"))
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/pepperoni pizza.jpg";
                }
                else if (textBox1.Text.Equals("Matcha Latte"))
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/matcha latte.jpg";
                }
                else if (textBox1.Text.Equals("Cheese Burger"))
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/cheese burger.jpeg";
                }
                else if (textBox1.Text.Equals("French Fries"))
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/french fries.jpg";
                }
                else if (textBox1.Text.Equals("Iced Coffee"))
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/iced coffee.jpg";
                }
                else
                {
                    pictureBox1.ImageLocation = "C:/Users/master/source/repos/Restautrant/Restautrant/image/nopict.png";
                }
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Order2 == null)
            {
                MessageBox.Show("pilih item yang akan dihapus");
                return;
            }
            OrderDetails.Remove(Order2);
            ClearTotal();
            Order2 = null;
            LoadData();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            int menuId = int.Parse(dataGridView2[0, e.RowIndex].Value.ToString());

            Order2 = db.orderDetail.Find(menuId);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string orderId = DateTime.Now.ToString("yyyyMMdd") + "0001";
            string employeeId = "EM0001";
            int member = int.Parse(comboBox1.SelectedIndex.ToString());

            Order3 = new OrderHeader();

            Order3.Id = orderId;
            Order3.Employeeid = employeeId;
            Order3.Memberid = member;
            Order3.Date = DateTime.Now.Date;
            Order3.PaymentType = "";
            Order3.CardNumber = "";
            Order3.Bank = "";

            db.OrderHeader.Add(Order3);
              db.SaveChanges();
            
        }
    }
}
