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
    public partial class FrmNhomHangHoa : DevExpress.XtraEditors.XtraForm
    {
        public FrmNhomHangHoa()
        {
            InitializeComponent();
        }
        NHOMHANGHOA _nhomhanghoa;
        bool _them;
        string _idnhom;
        private void FrmNhomHangHoa_Load(object sender, EventArgs e)
        {
            _nhomhanghoa = new NHOMHANGHOA();
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
            gcDanhSach.DataSource = _nhomhanghoa.getAll();
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
                tb_NHOMHH nhomhanghoa = new tb_NHOMHH();
                nhomhanghoa.TENNHOMHH = txtTen.Text;
                _nhomhanghoa.add(nhomhanghoa);
            }
            else
            {
                tb_NHOMHH nhomhanghoa = _nhomhanghoa.getItem(int.Parse(_idnhom));
                nhomhanghoa.TENNHOMHH = txtTen.Text;
                _nhomhanghoa.update(nhomhanghoa);
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
                _idnhom = gvDanhSach.GetFocusedRowCellValue("IDNHOM").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IDNHOM").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENNHOMHH").ToString();
            }
        }
    }
}