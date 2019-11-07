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
    public partial class ManageMember : Core, ICrud<MsMember>
    {
        public ManageMember()
        {
            InitializeComponent();
        }

        public MsMember Data { get; set; }
        public bool IsInserting { get; set; }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            if (Controls.IsAnyEmpty(tb_search))
            {
                MessageBox.Show("tidak boleh kosong");
                return false;
            }

            if (tb_email.Text.IsEmail())
            {
                return false;
            }
            return true;
        }

        public void LoadData()
        {
            dgv.DataSource = db.MsMember
                .Select(X => new {
                    X.Id,
                    X.Name,
                    X.Email,
                    X.Handphone,
                    X.JoinDate
                }).ToArray();
        }

        public void SaveData()
        {
            if (!IsValid())
            {
                return;
            }
            if (IsInserting)
            {
                Data = new MsMember();
            }

            Data.Name = tb_name.Text;
            Data.Email = tb_email.Text;
            Data.Handphone = tb_Handphone.Text;
            Data.JoinDate = DateTime.Now.Date;

            if (IsInserting)
            {
                db.MsMember.Add(Data);
            }

            db.SaveChanges();

            LoadData();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            try {
                Data = new MsMember();
                int id = int.Parse(dgv[0, e.RowIndex].Value.ToString());
                db.MsMember.Find(id);
                tb_name.Text = Data.Name.ToString();
                tb_email.Text = Data.Email.ToString();
                tb_Handphone.Text = Data.Handphone.ToString();
            }catch{

            }       
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            IsInserting = true;
            SaveData();
            Controls.ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Data == null)
            {
                MessageBox.Show("select data first");
                return;
            }

            IsInserting = false;
            SaveData();
            Controls.ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Data == null)
            {
                MessageBox.Show("select data first");
                return;
            }
            if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNoCancel)==DialogResult.Yes)
            {
                db.MsMember.Remove(Data);
                db.SaveChanges();
                LoadData();
            }
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            dgv.DataSource = db.MsMember
                .Where(X => X.Name == tb_search.Text).ToArray()
                .Select(X => new
                {
                    X.Id,
                    X.Name,
                    X.Email,
                    X.Handphone,
                    X.JoinDate
                }).ToArray();
        }

        private void ManageMember_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
