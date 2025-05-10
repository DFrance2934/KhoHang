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
    public partial class FrmDonViTinh : DevExpress.XtraEditors.XtraForm
    {
        public FrmDonViTinh()
        {
            InitializeComponent();
        }
        DONVITINH _donvitinh;
        bool _them;
        string _iddvt;
        private void FrmDonViTinh_Load(object sender, EventArgs e)
        {
            _donvitinh = new DONVITINH();
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
        }

        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
        }
        void loadData()
        {
            gcDanhSach.DataSource = _donvitinh.getAll();
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
                tb_DVT donvitinh = new tb_DVT();
                donvitinh.TENDVT = txtTen.Text;
                _donvitinh.add(donvitinh);
            }
            else
            {
                tb_DVT donvitinh = _donvitinh.getItem(int.Parse(_iddvt));
                donvitinh.TENDVT = txtTen.Text;
                _donvitinh.update(donvitinh);
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
                _iddvt = gvDanhSach.GetFocusedRowCellValue("IDDVT").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IDDVT").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENDVT").ToString();
            }
        }
    }
}