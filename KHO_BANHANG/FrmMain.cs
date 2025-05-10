using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KHO_BANHANG
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {

        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            FrmBaoCao frm = new FrmBaoCao();
            frm.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void addForm(Form form)
        {
            form.TopLevel = false;
            pnMain.Controls.Clear();
            pnMain.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
        }
        private void btnKho_Click(object sender, EventArgs e)
        {
            FrmKho frm = new FrmKho();
            addForm(frm);
        }
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FrmKhachHang frm = new FrmKhachHang();
            addForm(frm);
        }
        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            FrmNhaCungCap frm = new FrmNhaCungCap();
            addForm(frm);
        }
        private void btnNhomHangHoa_Click(object sender, EventArgs e)
        {
            FrmNhomHangHoa frm = new FrmNhomHangHoa();
            addForm(frm);
        }

        private void btnDonViTinh_Click(object sender, EventArgs e)
        {
            FrmDonViTinh frm = new FrmDonViTinh();
            addForm(frm);
        }

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            FrmHangHoa frm =new FrmHangHoa();
            addForm(frm);
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            FrmNhapHang frm =new FrmNhapHang();
            addForm(frm);
        }

        private void btnTonKho_Click(object sender, EventArgs e)
        {
            FrmTonKho frm =new FrmTonKho();
            addForm(frm);
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            FrmBanHang frm =new FrmBanHang();
            addForm(frm);
        }
    }
}
