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
    public partial class cashierForm : Core
    {
        public cashierForm()
        {
            InitializeComponent();
        }

        private void cashierForm_Load(object sender, EventArgs e)
        {
            var cashier = db.MsEmployee
                .Where(X => X.Position == "cashier")
                .FirstOrDefault<MsEmployee>();

            label2.Text = cashier.Name;
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            Hide();
            new changepassword().ShowDialog();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Hide();
            new cashier.Payment().ShowDialog();
            Show();
        }
    }
}
