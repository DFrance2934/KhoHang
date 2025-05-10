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
    public partial class FrmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public FrmKhachHang()
        {
            InitializeComponent();
        }
        KHACHHANG _khachhang;
        bool _them;
        string _idkh;
        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            _khachhang = new KHACHHANG();
            loadData();
            showHideControl(true);
            _enabled(false);
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }

        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            txtDienThoai.Enabled = t;
            txtDiaChi.Enabled = t;
            txtEmail.Enabled = t;
        }

        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtEmail.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
        }
        void loadData()
        {
            gcDanhSach.DataSource = _khachhang.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_KHACHHANG khachhang = new tb_KHACHHANG();
                khachhang.TENKH = txtTen.Text;
                khachhang.DIACHI = txtDiaChi.Text;
                khachhang.SDT = txtDienThoai.Text;
                khachhang.EMAIL = txtEmail.Text;
                _khachhang.add(khachhang);
            }
            else
            {
                tb_KHACHHANG khachhang = _khachhang.getItem(int.Parse(_idkh));
                khachhang.TENKH = txtTen.Text;
                khachhang.EMAIL = txtEmail.Text;
                khachhang.DIACHI = txtDiaChi.Text;
                khachhang.SDT = txtDienThoai.Text;
                _khachhang.update(khachhang);
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
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idkh = gvDanhSach.GetFocusedRowCellValue("IDKH").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IDKH").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENKH").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
            }
        }
    }
}