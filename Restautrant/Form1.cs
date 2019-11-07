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
    public partial class Form1 : Core
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        bool IsValid()
        {
            if (Controls.IsAnyEmpty())
            {
                MessageBox.Show("Don't leave any empty");
                return false;
            }
            return true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string password = textBox1.Text.ToSHA256();

            var employee = db.MsEmployee
                .FirstOrDefault(X => 
                        X.Email == textBox1.Text &&
                        X.Password == textBox2.Text
                    );

            
            if (employee == null)
            {
                MessageBox.Show("User not found");
                return;
            }
            
            Hide();

            switch (employee.Position)
            {
                case "cashier" : new cashierForm().ShowDialog();
                    break;
                case "chef": new chefForm().ShowDialog();
                    break;
                case "admin": new adminForm().ShowDialog();
                    break;
                default:
                    MessageBox.Show("Job Type not found");
                    break;
            }
            Show();
           
        }
    }
}
