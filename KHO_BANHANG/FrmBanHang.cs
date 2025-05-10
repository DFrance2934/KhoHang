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
    public partial class FrmBanHang : DevExpress.XtraEditors.XtraForm
    {
        public FrmBanHang()
        {
            InitializeComponent();
        }
        HOADON _hoadon;
        KHACHHANG _khachhang;
        string _idhd;
        bool _them;
        private void FrmBanHang_Load(object sender, EventArgs e)
        {
            _hoadon = new HOADON();
            _khachhang = new KHACHHANG();
            loadData();
        }
        void loadData()
        {
            gcDanhSach.DataSource = _hoadon.getList();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmAddBanHang frm = new FrmAddBanHang();
            frm.ShowDialog();
            LoadHoaDon();
        }
        private void LoadHoaDon()
        {
            gcDanhSach.DataSource = null; // Xóa dữ liệu cũ trước
            gcDanhSach.DataSource = _hoadon.getList().Select(hd => new
            {
                IDHD = hd.IDHD,
                TENKH = hd.TENKH,
                SDT = hd.SDT,
                NGAYLAP = hd.NGAYLAP,
                TONGTIEN = hd.TONGTIEN
            }).ToList();
            gcDanhSach.RefreshDataSource(); // Làm mới dữ liệu
            gvDanhSach.RefreshData(); // Làm mới GridView
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int idhd = (int)gvDanhSach.GetFocusedRowCellValue("IDHD");
            // Thêm MessageBox xác nhận
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return; // Hủy nếu chọn No
            }
            try
            {
                _hoadon.delete(idhd);
                LoadHoaDon();
                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_DoubleClick(object sender, EventArgs e)
        {
            // Lấy giá trị IDHD từ hàng được chọn
            var idhdValue = gvDanhSach.GetFocusedRowCellValue("IDHD");
            if (idhdValue == null || idhdValue == DBNull.Value)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long idhd;
            try
            {
                idhd = Convert.ToInt64(idhdValue); // Chuyển đổi an toàn sang long
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Giá trị IDHD không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (idhd == 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form chi tiết hóa đơn
            FrmChiTietHoaDon frmChiTiet = new FrmChiTietHoaDon(idhd);
            frmChiTiet.ShowDialog();
        }
    }

}