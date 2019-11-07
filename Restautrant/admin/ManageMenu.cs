using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restautrant
{
    public partial class ManageMenu : Core, ICrud<MsMenu>
    {
        public ManageMenu()
        {
            InitializeComponent();
        }

        public MsMenu Data { get; set; }
        public bool IsInserting { get; set; }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            if (Controls.IsAnyEmpty())
            {
                MessageBox.Show("tidak boleh kosong");
                return false;
            }

            if (!tb_pricel.Text.IsNumber())
            {
                MessageBox.Show("harga harus nomor");
                return false;
            }

            if (!tb_protein.Text.IsNumber())
            {
                MessageBox.Show("protein harus nomor");
                return false;
            }

            if (!tb_carbon.Text.IsNumber())
            {
                MessageBox.Show("carbon harus nomor");
                return false;
            }

            return true;
        }

        public void LoadData()
        {
            dgv.DataSource = db.MsMenu
                .Select(X => new {
                    X.id,
                    X.Name,
                    X.Photo,
                    X.Price,
                    X.Carbo,
                    X.Protein
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
                Data = new MsMenu();
            }

            Data.Name = tb_name.Text;
            Data.Price = int.Parse(tb_pricel.Text.ToString());
            Data.Photo = tb_photo.Text;
            Data.Carbo = int.Parse(tb_carbon.Text);
            Data.Protein = int.Parse(tb_protein.Text);

            if (IsInserting)
            {
                db.MsMenu.Add(Data);
            }

            db.SaveChanges();
            LoadData();
        }

        private void ManageMenu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            IsInserting = true;
            SaveData();
            Controls.ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            IsInserting = false;
            Controls.ClearFields();
            SaveData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure ?", "confirmation", MessageBoxButtons.YesNoCancel)==DialogResult.OK)
            {
                db.MsMenu.Remove(Data);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            try
            { 
                int id = int.Parse(dgv[0, e.RowIndex].Value.ToString());
                Data = db.MsMenu.Find(id);
                tb_name.Text = Data.Name;
                tb_pricel.Text = Data.Price.ToString();
                tb_photo.Text = Data.Photo;
                tb_protein.Text = Data.Protein.ToString();
                tb_carbon.Text = Data.Carbo.ToString();
            }
            catch
            {

            }
        }

        private void btn_photo_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
                tb_photo.Text = Path.GetFileName(open.FileName);
            }
        }
    }
}
