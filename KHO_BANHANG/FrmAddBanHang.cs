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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO_BANHANG
{
    public partial class FrmAddBanHang : DevExpress.XtraEditors.XtraForm
    {
        public FrmAddBanHang()
        {
            InitializeComponent();
        }
        HOADON _hoadon;
        KHACHHANG _khachhang;
        HANGHOA _hanghoa;
        KHO _kho;
        NHOMHANGHOA _nhomhanghoa;
        TONKHO _tonkho;
        bool _them;
        bool _sua;
        List<tb_HOADON_CT> _chiTietHoaDon;
        int? _idkh;
        tb_HOADON _currentHoaDon;
        int? _idhd;
        private void FrmAddBanHang_Load(object sender, EventArgs e)
        {

            _hoadon = new HOADON();
            _khachhang = new KHACHHANG();
            _hanghoa = new HANGHOA();
            _kho = new KHO();
            _nhomhanghoa = new NHOMHANGHOA();
            _tonkho = new TONKHO();
            _chiTietHoaDon = new List<tb_HOADON_CT>();
            _idkh = null;
            _currentHoaDon = null;
            _idhd = null;
            _sua = false;
            _them = false;
            loadNhomHH();
            loadKho();
            loadDanhSachHangHoa();
            //dtNgayLap.Value = DateTime.Now;
            cboLoaiHH.SelectedIndexChanged += CboLoaiHH_SelectedIndexChanged;
            cboHangHoa.SelectedIndexChanged += CboHangHoa_SelectedIndexChanged;
        }

        private void CboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedValue != null)
            {
                int idhh = int.Parse(cboHangHoa.SelectedValue.ToString());
                loadKho(idhh);
                // Tự động điền đơn giá
                var hangHoa = _hanghoa.getItem(idhh);
                if (hangHoa != null)
                {
                    txtDonGia.Text = hangHoa.DONGIA.ToString();
                }
            }
            else
            {
                loadKho(null);
                txtDonGia.Text = "0";
            }
        }

        private void CboLoaiHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDanhSachHangHoa();
        }

        void loadNhomHH()
        {
            cboLoaiHH.DataSource = _nhomhanghoa.getAll();
            cboLoaiHH.DisplayMember = "TENNHOMHH";
            cboLoaiHH.ValueMember = "IDNHOM";
            cboLoaiHH.SelectedIndex = -1;
        }
        void loadDanhSachHangHoa()
        {

            if (cboLoaiHH.SelectedValue == null)
            {
                cboHangHoa.DataSource = _hanghoa.getAll();
            }
            else
            {
                int idNhom = int.Parse(cboLoaiHH.SelectedValue.ToString());
                cboHangHoa.DataSource = _hanghoa.getListByNhom(idNhom);

            }
            cboHangHoa.DisplayMember = "TENHH"; // Sửa: Hiển thị tên mặt hàng
            cboHangHoa.ValueMember = "IDHH"; // Sửa: Giá trị là IDHH của mặt hàng
            cboHangHoa.SelectedIndex = -1;

        }

        void loadKho()
        {
            cboKho.DataSource = _kho.getAll();
            cboKho.DisplayMember = "TENKHO";
            cboKho.ValueMember = "IDKHO";
            cboKho.SelectedIndex = -1; // Không chọn mặc định
        }
        void loadKho(int? idhh)
        {
            try
            {
                List<tb_KHO> danhSachKho;
                if (idhh == null)
                {
                    danhSachKho = _kho.getAll();
                }
                else
                {
                    danhSachKho = _kho.getKhoByHangHoa(idhh.Value);
                }

                if (danhSachKho == null || !danhSachKho.Any())
                {
                    MessageBox.Show(idhh == null
                        ? "Không có kho nào trong cơ sở dữ liệu!"
                        : $"Không có kho nào chứa mặt hàng {cboHangHoa.Text} với số lượng tồn lớn hơn 0!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKho.DataSource = null;
                }
                else
                {
                    cboKho.DataSource = danhSachKho;
                    cboKho.DisplayMember = "TENKHO";
                    cboKho.ValueMember = "IDKHO";
                    cboKho.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load danh sách kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadHoaDon()
        {
            HOADON _hoadon = new HOADON();
            gcChiTiet.DataSource = _hoadon.getList().Select(hd => new
            {
                IDHD = hd.IDHD,
                TENKH = hd.TENKH,
                SDT = hd.SDT,
                NGAYBAN = hd.NGAYLAP,
                THANHTIEN = hd.TONGTIEN
            }).ToList();
        }

        void _reset()
        {
            txtSDT.Text = "";
            txtTenKH.Text = "";
            cboHangHoa.Text = "";
            cboKho.Text = "";
            spSoLuong.Text = "0";
            txtDonGia.Text = "0";
            dtNgayLap.Value = DateTime.Now;
            _chiTietHoaDon.Clear();
            gcChiTiet.DataSource = null;
            _idkh = null; // Reset IDKH
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text.Trim();

            // Kiểm tra số điện thoại (10 chữ số)
            if (!Regex.IsMatch(sdt, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại phải gồm 10 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tìm kiếm khách hàng
            var khachHang = _khachhang.TimKiemTheoSDT(sdt);
            if (khachHang != null)
            {
                txtTenKH.Text = khachHang.TENKH;
                _idkh = khachHang.IDKH; // Lưu IDKH để sử dụng khi tạo hóa đơn
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng với số điện thoại này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Text = "";
                _idkh = null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu _idkh hoặc _currentHoaDon chưa được gán
            if (_idkh == null)
            {
                MessageBox.Show("Vui lòng tìm kiếm khách hàng trước khi thêm mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu _currentHoaDon chưa được gán, tạo mới dựa trên _idkh
            if (_currentHoaDon == null)
            {
                _currentHoaDon = new tb_HOADON
                {
                    IDKH = _idkh.Value,
                    NGAYLAP = dtNgayLap.Value, // Gán ngày hiện tại hoặc từ dtNgayLap nếu có
                    TONGTIEN = 0
                };
                //_currentHoaDon = _hoadon.add(_currentHoaDon); // Lưu hóa đơn mới vào DB
            }

            if (cboHangHoa.SelectedValue == null || cboKho.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn hàng hóa và kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idhh;
            string idkho;
            int soluong;
            float dongia;

            try
            {
                idhh = int.Parse(cboHangHoa.SelectedValue.ToString());
                idkho = cboKho.SelectedValue.ToString();
                soluong = int.Parse(spSoLuong.Text);
                dongia = float.Parse(txtDonGia.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu nhập không hợp lệ! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (soluong <= 0 || dongia <= 0)
            {
                MessageBox.Show("Số lượng và đơn giá phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tonKhoItem = _tonkho.getItem(idhh, idkho);
            if (tonKhoItem == null || tonKhoItem.SOLUONG < soluong)
            {
                MessageBox.Show($"Số lượng tồn trong kho {cboKho.Text} không đủ! Hiện tại chỉ còn {tonKhoItem?.SOLUONG ?? 0} đơn vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var chiTiet = new tb_HOADON_CT
            {
                IDHD = _currentHoaDon.IDHD,
                IDHH = idhh,
                IDKHO = idkho,
                SOLUONG = soluong,
                DONGIA = dongia,
                THANHTIEN = (int.Parse(soluong.ToString()) * int.Parse(dongia.ToString()))
            };

            // Lưu chi tiết hóa đơn vào SQL ngay lập tức
            try
            {
                //using (var db = Entities.CreateEntities())
                //{
                //    db.tb_HOADON_CT.Add(chiTiet);
                //    db.SaveChanges();

                //    // Cập nhật tồn kho
                //    _tonkho.CapNhatTonKho(idhh, idkho, -soluong);

                //    // Cập nhật tổng tiền hóa đơn
                //    _currentHoaDon.TONGTIEN = (_currentHoaDon.TONGTIEN ?? 0) + (soluong * dongia);
                //    var hoaDonInDb = db.tb_HOADON.FirstOrDefault(x => x.IDHD == _currentHoaDon.IDHD);
                //    if (hoaDonInDb != null)
                //    {
                //        hoaDonInDb.TONGTIEN = _currentHoaDon.TONGTIEN;
                //        db.SaveChanges();
                //    }
                //}

                _chiTietHoaDon.Add(chiTiet);
                TinhTongTien();
                gcChiTiet.DataSource = null;
                gcChiTiet.DataSource = _chiTietHoaDon.Select(ct => new
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TinhTongTien()
        {
            if (_chiTietHoaDon != null && _chiTietHoaDon.Any())
            {
                double tongTien = _chiTietHoaDon.Sum(ct => (ct.SOLUONG ?? 0) * (ct.DONGIA ?? 0));
                _currentHoaDon.TONGTIEN = (double?)tongTien; // Cập nhật tổng tiền cho _currentHoaDon
                txtThanhTien.Text = tongTien.ToString("N0"); // Hiển thị tổng tiền với định dạng số
            }
            else
            {
                _currentHoaDon.TONGTIEN = 0;
                txtThanhTien.Text = "0";
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Lấy chỉ số hàng được chọn trong gridview
            int rowIndex = gvChiTiet.FocusedRowHandle;
            if (rowIndex < 0 || rowIndex >= _chiTietHoaDon.Count)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                //using (var db = Entities.CreateEntities())
                //{
                //    using (var transaction = db.Database.BeginTransaction())
                //    {
                //        try
                //        {
                //            // Lấy chi tiết từ danh sách tạm dựa trên chỉ số hàng
                //            var chiTiet = _chiTietHoaDon[rowIndex];

                //            // Xóa chi tiết khỏi database
                //            var chiTietInDb = db.tb_HOADON_CT.FirstOrDefault(ct => ct.IDHD == chiTiet.IDHD && ct.IDHH == chiTiet.IDHH && ct.IDKHO == chiTiet.IDKHO);
                //            if (chiTietInDb != null)
                //            {
                //                // Cập nhật tồn kho (tăng số lượng)
                //                var tonKhoItem = db.tb_TONKHO.FirstOrDefault(tk => tk.IDHH == chiTiet.IDHH && tk.IDKHO == chiTiet.IDKHO);
                //                if (tonKhoItem != null)
                //                {
                //                    tonKhoItem.SOLUONG += chiTiet.SOLUONG ?? 0;
                //                }
                //                else
                //                {
                //                    MessageBox.Show($"Không tìm thấy kho cho IDHH {chiTiet.IDHH} và IDKHO {chiTiet.IDKHO}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //                    return;
                //                }

                //                db.tb_HOADON_CT.Remove(chiTietInDb);
                //                db.SaveChanges();
                //            }

                //            // Xóa chi tiết khỏi danh sách tạm
                //            _chiTietHoaDon.RemoveAt(rowIndex);

                //            // Cập nhật tổng tiền
                //            TinhTongTien();

                //            // Lưu lại tổng tiền vào database
                //            var hoaDonInDb = db.tb_HOADON.FirstOrDefault(hd => hd.IDHD == _currentHoaDon.IDHD);
                //            if (hoaDonInDb != null)
                //            {
                //                hoaDonInDb.TONGTIEN = _currentHoaDon.TONGTIEN;
                //                db.SaveChanges();
                //            }

                //            // Cập nhật gridview
                //            gcChiTiet.DataSource = null;
                //            gcChiTiet.DataSource = _chiTietHoaDon.Select(ct => new
                //            {
                //                ct.IDHH,
                //                TENHH = _hanghoa.getItem(ct.IDHH)?.TENHH,
                //                ct.IDKHO,
                //                TENKHO = _kho.getItem(ct.IDKHO)?.TENKHO,
                //                ct.SOLUONG,
                //                ct.DONGIA,
                //                THANHTIEN = (ct.SOLUONG ?? 0) * (ct.DONGIA ?? 0)
                //            }).ToList();

                //            transaction.Commit();
                //            MessageBox.Show("Xóa mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }
                //        catch (Exception ex)
                //        {
                //            transaction.Rollback();
                //            MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //    }
                //}
                _chiTietHoaDon.RemoveAt(rowIndex);
                TinhTongTien();
                gcChiTiet.DataSource = null;
                gcChiTiet.DataSource = _chiTietHoaDon.Select(ct => new
                {
                    ct.IDHH,
                    TENHH = _hanghoa.getItem(ct.IDHH)?.TENHH,
                    ct.IDKHO,
                    TENKHO = _kho.getItem(ct.IDKHO)?.TENKHO,
                    ct.SOLUONG,
                    ct.DONGIA,
                    THANHTIEN = (ct.SOLUONG ?? 0) * (ct.DONGIA ?? 0)
                }).ToList();

                MessageBox.Show("Xóa mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_currentHoaDon == null || !_chiTietHoaDon.Any())
            {
                MessageBox.Show("Không có hóa đơn hoặc chi tiết để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = Entities.CreateEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            // Lưu hóa đơn vào database
                            db.tb_HOADON.Add(_currentHoaDon);
                            db.SaveChanges();

                            // Cập nhật IDHD cho các chi tiết hóa đơn và lưu
                            foreach (var chiTiet in _chiTietHoaDon)
                            {
                                chiTiet.IDHD = _currentHoaDon.IDHD;
                                db.tb_HOADON_CT.Add(chiTiet);

                                // Cập nhật tồn kho
                                var tonKhoItem = db.tb_TONKHO.FirstOrDefault(tk => tk.IDHH == chiTiet.IDHH && tk.IDKHO == chiTiet.IDKHO);
                                if (tonKhoItem != null)
                                {
                                    tonKhoItem.SOLUONG -= chiTiet.SOLUONG ?? 0;
                                }
                            }

                            db.SaveChanges();

                            // Cập nhật tổng tiền
                            TinhTongTien();
                            var hoaDonInDb = db.tb_HOADON.FirstOrDefault(hd => hd.IDHD == _currentHoaDon.IDHD);
                            if (hoaDonInDb != null)
                            {
                                hoaDonInDb.TONGTIEN = _currentHoaDon.TONGTIEN;
                                db.SaveChanges();
                            }

                            transaction.Commit();
                            MessageBox.Show("Thanh toán và lưu hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Đóng form để FrmBanHang làm mới
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}