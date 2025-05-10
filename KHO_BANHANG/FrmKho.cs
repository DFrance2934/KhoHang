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
    public partial class FrmKho : DevExpress.XtraEditors.XtraForm
    {
        public FrmKho()
        {
            InitializeComponent();
        }
        KHO _kho;
        bool _them;
        string _idkho;
        private void FrmKho_Load(object sender, EventArgs e)
        {
            _kho = new KHO();
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
        }

        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _kho.getAll();
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
                _kho.delete(_idkho);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_KHO kho = new tb_KHO();
                kho.IDKHO = txtMa.Text;
                kho.TENKHO = txtTen.Text;
                kho.DIACHI = txtDiaChi.Text;
                kho.SDT = txtDienThoai.Text;
                kho.DISABLED = chkDisabled.Checked;
                _kho.add(kho);
            }
            else
            {
                tb_KHO kho = _kho.getItem(_idkho);
                kho.TENKHO = txtTen.Text;
                kho.DIACHI = txtDiaChi.Text;
                kho.SDT = txtDienThoai.Text;
                kho.DISABLED = chkDisabled.Checked;
                _kho.update(kho);
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
                _idkho = gvDanhSach.GetFocusedRowCellValue("IDKHO").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IDKHO").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENKHO").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }
    }
}