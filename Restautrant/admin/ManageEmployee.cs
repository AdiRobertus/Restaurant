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
    public partial class ManageEmployee : Core, ICrud<MsEmployee>
    {
        public ManageEmployee()
        {
            InitializeComponent();
        }

        public MsEmployee Data { get; set; }
        public bool IsInserting { get; set; }

        public void Clear()
        {
            tb_name.Text = "";
            tb_email.Text = "";
            tb_hp.Text = "";
        }

        public bool IsValid()
        {
            if (tb_name.Text == "" || tb_email.Text == "" || tb_hp.Text == "")
            {
                MessageBox.Show("Harap Semua Diisi");
                return true;
            }
            if (!tb_hp.Text.IsNumber())
            {
                MessageBox.Show("Handphone harus nomor");
                return true;
            }
            return false;
        }

        public void LoadData()
        {
            dgv.DataSource = db.MsEmployee
                .Select(X => new {
                    X.Id,
                    X.Name,
                    X.Email,
                    X.Handphone,
                    X.Position
                }).ToArray();
        }

        public void SaveData()
        {
            if (IsValid())
            {
                return;
            }
            if (IsInserting)
            {
                Data = new MsEmployee();
            }

            Data.Id = "EM0004";
            Data.Name = tb_name.Text;
            Data.Email = tb_email.Text;
            Data.Handphone = tb_hp.Text;
            Data.Password = "Password";
            Data.Position = cb_position.Text;

            if (IsInserting)
            {
                db.MsEmployee.Add(Data);
            }

            db.SaveChanges();
            Clear();
            LoadData();
        }

        private void ManageEmployee_Load(object sender, EventArgs e)
        {
            cb_position.ValueMember = "Id";
            cb_position.DisplayMember = "Position";
            cb_position.DataSource = db.MsEmployee
                .Select(X => new {
                    X.Id,
                    X.Position
                }).ToArray();

            LoadData();
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            dgv.DataSource = db.MsEmployee
                .Where(X =>
                    X.Id.ToString() == tb_search.Text).ToArray()
                .Select(X => new
                {
                    X.Id,
                    X.Name,
                    X.Email,
                    X.Handphone,
                    X.Position
                }).ToArray();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                string id = dgv[0, e.RowIndex].Value.ToString();
                Data = db.MsEmployee.Find(id);
                tb_name.Text = Data.Name.ToString();
                tb_email.Text = Data.Email.ToString();
                tb_hp.Text = Data.Handphone;
                cb_position.Text = Data.Position.ToString();
            }
            catch
            {

            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            IsInserting = true;
            SaveData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Data == null)
            {
                MessageBox.Show("Select data first");
                return;
            }
            IsInserting = false;
            SaveData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Data == null)
            {
                MessageBox.Show("Select data");
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo)== DialogResult.Yes)
            {
                db.MsEmployee.Remove(Data);
                db.SaveChanges();
                LoadData();
            }
        }
    }
}
