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
    public partial class FrmNhapHang : DevExpress.XtraEditors.XtraForm
    {
        public FrmNhapHang()
        {
            InitializeComponent();
        }
        NHAPHANG _nhaphang;
        HANGHOA _hanghoa;
        NHACUNGCAP _nhacungcap;
        KHO _kho;
        bool _them;
        string _id;
        List<obj_NhapHang> _lstNH = new List<obj_NhapHang>();
        private void FrmNhapHang_Load(object sender, EventArgs e)
        {
            _nhaphang = new NHAPHANG();
            _hanghoa = new HANGHOA();
            _nhacungcap = new NHACUNGCAP();
            _kho = new KHO();
            showHideControl(true);
            _enabled(false);
            loadHangHoa();
            loadKho();
            loadNhaCC();
            loadData();

        }
        void loadHangHoa()
        {
            cboHangHoa.DataSource = _hanghoa.getAll();
            cboHangHoa.DisplayMember = "TENHH";
            cboHangHoa.ValueMember = "IDHH";
        }
        void loadNhaCC()
        {
            cboNhaCungCap.DataSource = _nhacungcap.getAll();
            cboNhaCungCap.DisplayMember = "TENNCC";
            cboNhaCungCap.ValueMember = "IDNCC";
        }
        void loadKho()
        {
            cboKho.DataSource = _kho.getAll();
            cboKho.DisplayMember = "TENKHO";
            cboKho.ValueMember = "IDKHO";
        }
        void loadData()
        {
            gcDanhSach.DataSource = _nhaphang.getList();
            gvDanhSach.OptionsBehavior.Editable = false;
            _lstNH = _nhaphang.getListFull(int.Parse(cboHangHoa.SelectedValue.ToString()));
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            //btnSua.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }

        void _enabled(bool t)
        {
            cboHangHoa.Enabled = t;
            cboKho.Enabled = t;
            spSoLuong.Enabled = t;
            dtNgayNhap.Enabled = t;
        }

        void _reset()
        {
            cboHangHoa.Text = "";
            cboKho.Text = "";
            spSoLuong.Text = "";
            dtNgayNhap.Text = "";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        //private void btnSua_Click(object sender, EventArgs e)
        //{
        //    cboHangHoa.Enabled = false;
        //    cboNhaCungCap.Enabled = false;
        //    _them = false;
        //    _enabled(true);
        //    showHideControl(false);
            
        //}

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_NHAPHANG nh = new tb_NHAPHANG();
                nh.DONGIA = float.Parse(spDonGia.Text);
                nh.SOLUONG = int.Parse(spSoLuong.Text);
                nh.IDHH = int.Parse(cboHangHoa.SelectedValue.ToString());
                nh.IDNCC = cboNhaCungCap.SelectedValue.ToString();
                nh.IDKHO = cboKho.SelectedValue.ToString();
                nh.NGAYNHAP = dtNgayNhap.Value;

                var _nh = _nhaphang.add(nh);
            }
            //else
            //{

            //    tb_NHAPHANG nh = _nhaphang.getItem(int.Parse(_id));

            //    if (nh != null)
            //    {
            //        if (!float.TryParse(spDonGia.Text, out float donGia))
            //        {
            //            MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        if (!int.TryParse(spSoLuong.Text, out int soLuong))
            //        {
            //            MessageBox.Show("Số lượng không hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }

            //        nh.DONGIA = donGia;
            //        nh.SOLUONG = soLuong;
            //        // Gán dữ liệu cho các thuộc tính
            //        nh.DONGIA = float.Parse(spDonGia.Text);
            //        nh.SOLUONG = int.Parse(spSoLuong.Text);
            //        nh.NGAYNHAP = dtNgayNhap.Value;

            //        var _nh = _nhaphang.update(nh);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không tìm thấy dữ liệu nhập hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }

            //}
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _id = gvDanhSach.GetFocusedRowCellValue("ID").ToString();
                cboNhaCungCap.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDNCC");
                cboHangHoa.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDHH");
                cboKho.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDKHO");
                spDonGia.Text = gvDanhSach.GetFocusedRowCellValue("DONGIA").ToString();
                spSoLuong.Text = gvDanhSach.GetFocusedRowCellValue("SOLUONG").ToString();
                //dtNgayNhap = gvDanhSach.GetFocusedRowCellValue("NGAYNHAP").ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _nhaphang.delete(int.Parse(_id));
            }
            loadData();
        }
    }
}