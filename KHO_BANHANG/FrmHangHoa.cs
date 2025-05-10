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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KHO_BANHANG
{
    public partial class FrmHangHoa : DevExpress.XtraEditors.XtraForm
    {
        public FrmHangHoa()
        {
            InitializeComponent();
        }
        HANGHOA _hanghoa;
        DONVITINH _donvitinh;
        NHACUNGCAP _nhacungcap;
        NHOMHANGHOA _nhomhanghoa;
        bool _them;
        string _idhh;
        List<obj_HangHoa> _lstHH = new List<obj_HangHoa>();
        private void FrmHangHoa_Load(object sender, EventArgs e)
        {
            _hanghoa = new HANGHOA();
            _donvitinh = new DONVITINH();
            _nhacungcap = new NHACUNGCAP();
            _nhomhanghoa = new NHOMHANGHOA();

            showHideControl(true);
            _enabled(false);

            loadNhom();
            loadNhaCC();
            loadDVT();

            loadData();

        }



        void loadNhom()
        {
            cboNhom.DataSource = _nhomhanghoa.getAll();
            cboNhom.DisplayMember = "TENNHOMHH";
            cboNhom.ValueMember = "IDNHOM";
        }
        void loadNhaCC()
        {
            cboNCC.DataSource = _nhacungcap.getAll();
            cboNCC.DisplayMember = "TENNCC";
            cboNCC.ValueMember = "IDNCC";
        }
        void loadDVT()
        {
            cboDVT.DataSource = _donvitinh.getAll();
            cboDVT.DisplayMember = "TENDVT";
            cboDVT.ValueMember = "IDDVT";
        }
        void loadData()
        {
            gcDanhSach.DataSource = _hanghoa.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
            _lstHH = _hanghoa.getListByNhomFull(int.Parse(cboNhom.SelectedValue.ToString()));
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
            txtMota.Enabled = t;
            cboDVT.Enabled = t;
            spDonGia.Enabled = t;
            cboNCC.Enabled = t;
            cboNhom.Enabled = t;
            cboDVT.Enabled= t;
            chkDisabled.Enabled = t;
        }

        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            cboDVT.Text = "";
            cboNCC.Text = "";
            cboNhom.Text = "";
            txtMota.Text = "";
            spDonGia.Text = "";

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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _hanghoa.delete(_idhh);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_HANGHOA hh = new tb_HANGHOA();
                hh.TENHH = txtTen.Text;
                hh.DONGIA = float.Parse(spDonGia.Text);
                hh.IDNHOM = int.Parse(cboNhom.SelectedValue.ToString());
                hh.MOTA = txtMota.Text;
                hh.IDNCC = cboNCC.SelectedValue.ToString();
                hh.DVT = cboDVT.Text;
                hh.DISABLED = chkDisabled.Checked;

                var _hh = _hanghoa.add(hh);
            }
            else
            {
                tb_HANGHOA hh = _hanghoa.getItem(int.Parse(_idhh));
                hh.TENHH = txtTen.Text;
                hh.DONGIA = float.Parse(spDonGia.Text);
                hh.IDNHOM = int.Parse(cboNhom.SelectedValue.ToString());
                hh.MOTA = txtMota.Text;
                hh.IDNCC = cboNCC.SelectedValue.ToString();
                hh.DVT = cboDVT.Text;
                hh.DISABLED = chkDisabled.Checked;
                var _hh = _hanghoa.update(hh);

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
            _reset();
            loadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idhh = gvDanhSach.GetFocusedRowCellValue("IDHH").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IDHH").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENHH").ToString();
                cboNCC.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDNCC");
                cboDVT.Text = gvDanhSach.GetFocusedRowCellValue("DVT").ToString();
                cboNhom.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDNHOM");
                spDonGia.Text = gvDanhSach.GetFocusedRowCellValue("DONGIA").ToString();
                txtMota.Text = gvDanhSach.GetFocusedRowCellValue("MOTA").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }
    }
}