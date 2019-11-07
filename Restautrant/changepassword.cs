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
    public partial class changepassword : Core
    {
        public bool IsInserting { get; set; }
        public MsEmployee Data { get; set; }
        public changepassword()
        {
            InitializeComponent();
        }

        bool IsValid()
        {
            if (Controls.IsAnyEmpty())
            {
                MessageBox.Show("All must be fill");
                return false;
            }

            string password = textBox1.Text;
            string password2 = textBox2.Text;
            string password3 = textBox3.Text;

            if (password2 != password3)
            {
                MessageBox.Show("Password not same");
                return false;
            }
            return true;
        }

        public void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";      }

        public void SaveData()
        {
            if (!IsValid())
            {
                return;
            }

            if (IsInserting)
            {
                Data = new MsEmployee();
            }

            Data.Password = textBox2.Text;

            if (IsInserting)
            {
                db.MsEmployee.Add(Data);
            }

            db.SaveChanges();
            clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Data = new MsEmployee();
            string id = Data.Id;
            db.MsEmployee.Find(id);

            if (Data == null)
            {
                MessageBox.Show("isi");
                return;
            }
            IsInserting = false;

            SaveData();
            
        }
    }
}
