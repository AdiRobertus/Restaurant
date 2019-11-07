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
    public partial class chefForm : Core
    {
        public chefForm()
        {
            InitializeComponent();
        }

        private void chefForm_Load(object sender, EventArgs e)
        {
            var chef = db.MsEmployee
                .Where(X => X.Position == "chef")
                .FirstOrDefault<MsEmployee>();

            label2.Text = chef.Name;
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            Hide();
            new changepassword().ShowDialog();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Hide();
            new admin.ViewOrder().ShowDialog();
            Show();
        }
    }
}
