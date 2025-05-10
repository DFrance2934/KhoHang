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
    public partial class FrmChiTietHoaDon : DevExpress.XtraEditors.XtraForm
    {
        private long _idhd; // IDHD của hóa đơn cần xem chi tiết
        private HOADON _hoadon;
        private HANGHOA _hanghoa;
        private KHO _kho;
        public FrmChiTietHoaDon(long idhd)
        {
            InitializeComponent();
            _idhd = idhd;
        }      

        private void FrmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            _hoadon = new HOADON();
            _hanghoa = new HANGHOA();
            _kho = new KHO();

            LoadChiTietHoaDon();
        }
        private void LoadChiTietHoaDon()
        {
            try
            {
                using (var db = Entities.CreateEntities())
                {
                    // Lấy thông tin hóa đơn và khách hàng
                    var hoaDon = db.tb_HOADON.FirstOrDefault(hd => hd.IDHD == _idhd);
                    if (hoaDon == null)
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    // Lấy tên khách hàng
                    var khachHang = db.tb_KHACHHANG.FirstOrDefault(kh => kh.IDKH == hoaDon.IDKH);
                    lbTen.Text = $"{khachHang?.TENKH ?? "Không xác định"}";

                    // Lấy danh sách chi tiết hóa đơn
                    var chiTietHoaDon = db.tb_HOADON_CT
                        .Where(ct => ct.IDHD == _idhd)
                        .ToList();

                    // Hiển thị lên GridControl
                    gcChiTiet.DataSource = chiTietHoaDon.Select(ct => new
                    {
                        ct.IDHH,
                        TENHH = _hanghoa.getItem(ct.IDHH)?.TENHH,
                        ct.IDKHO,
                        TENKHO = _kho.getItem(ct.IDKHO)?.TENKHO,
                        ct.SOLUONG,
                        ct.DONGIA,
                        THANHTIEN = (ct.SOLUONG ?? 0) * (ct.DONGIA ?? 0)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}