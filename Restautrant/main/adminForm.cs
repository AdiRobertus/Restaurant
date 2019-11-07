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
    public partial class adminForm : Core
    {
        
        public adminForm()
        {
            InitializeComponent();
            
        }

        private void adminForm_Load(object sender, EventArgs e)
        {
            var query = db.MsEmployee
                       .Where(s => s.Position == "admin")
                       .FirstOrDefault<MsEmployee>();

            label2.Text = query.Name;
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            Hide();
            new changepassword().ShowDialog();
            Show();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            Hide();
            new ManageEmployee().ShowDialog();
            Show();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Hide();
            new ManageMenu().ShowDialog();
            Show();
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            Hide();
            new admin.ManageMember().ShowDialog();
            Show();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Hide();
            new Order().ShowDialog();
            Show();
        }
    }
}
