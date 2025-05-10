using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO_BANHANG
{
    public partial class FrmNhaCungCap : DevExpress.XtraEditors.XtraForm
    {
        public FrmNhaCungCap()
        {
            InitializeComponent();
        }
        NHACUNGCAP _nhacungcap;
        bool _them;
        string _idncc;
        private void FrmNhaCungCap_Load(object sender, EventArgs e)
        {
            _nhacungcap = new NHACUNGCAP();
            loadData();
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }

        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            txtDienThoai.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
            txtMa.Enabled = t;
            txtEmail.Enabled = t;
        }

        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Checked = false;
            txtEmail.Text = "";
        }
        void loadData()
        {
            gcDanhSach.DataSource = _nhacungcap.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            txtMa.Enabled = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            txtMa.Enabled = false;
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _nhacungcap.delete(_idncc);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_NHACUNGCAP nhacungcap = new tb_NHACUNGCAP();
                nhacungcap.IDNCC = txtMa.Text;
                nhacungcap.TENNCC = txtTen.Text;
                nhacungcap.DIACHI = txtDiaChi.Text;
                nhacungcap.SDT = txtDienThoai.Text;
                nhacungcap.EMAIL = txtEmail.Text;
                nhacungcap.DISABLED = chkDisabled.Checked;
                _nhacungcap.add(nhacungcap);
            }
            else
            {
                tb_NHACUNGCAP nhacungcap = _nhacungcap.getItem(_idncc);
                nhacungcap.TENNCC = txtTen.Text;
                nhacungcap.DIACHI = txtDiaChi.Text;
                nhacungcap.EMAIL= txtEmail.Text;
                nhacungcap.SDT = txtDienThoai.Text;
                nhacungcap.DISABLED = chkDisabled.Checked;
                _nhacungcap.update(nhacungcap);
            }
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idncc = gvDanhSach.GetFocusedRowCellValue("IDNCC").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IDNCC").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENNCC").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }
    }
}