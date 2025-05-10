using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class HOADON
    {
        Entities db;
        public HOADON()
        {
            db = Entities.CreateEntities();
        }
        public tb_HOADON getItem(int id)
        {
            return db.tb_HOADON.FirstOrDefault(x => x.IDHD == id);
        }
        public List<obj_HoaDon> getList()
        {
            var query = from hd in db.tb_HOADON
                        join kh in db.tb_KHACHHANG on hd.IDKH equals kh.IDKH

                        select new obj_HoaDon
                        {
                            IDHD = hd.IDHD,
                            IDKH = kh.IDKH,
                            TENKH = kh.TENKH,
                            SDT = kh.SDT,
                            NGAYLAP = hd.NGAYLAP,
                            TONGTIEN = hd.TONGTIEN
                        };

            return query.ToList();
        }
        public List<tb_HOADON> getListByKhachHang(int idkh)
        {
            return db.tb_HOADON.Where(x => x.IDKH == idkh).ToList();
        }
        public tb_HOADON add(tb_HOADON hd)
        {
            try
            {
                db.tb_HOADON.Add(hd);
                db.SaveChanges();               
                return hd;
            }

            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

        }
        public tb_HOADON BanHang(tb_HOADON hoaDon, List<tb_HOADON_CT> chiTietHoaDon)
        {
            try
            {
                // 1. Kiểm tra tồn kho trước khi bán
                TONKHO tonKho = new TONKHO();
                foreach (var ct in chiTietHoaDon)
                {
                    var ton = tonKho.getItem(ct.IDHH, ct.IDKHO);
                    int soLuong = ct.SOLUONG ?? 0; // Chuyển đổi từ int? sang int, mặc định là 0 nếu null
                    if (ton == null || ton.SOLUONG < soLuong)
                    {
                        throw new Exception($"Không đủ hàng tồn kho cho mã hàng {ct.IDHH} trong kho {ct.IDKHO}. Số lượng tồn: {ton?.SOLUONG ?? 0}");
                    }
                }

                // 2. Thêm hóa đơn
                hoaDon.TONGTIEN = chiTietHoaDon.Sum(ct => (ct.SOLUONG ?? 0) * ct.DONGIA);
                db.tb_HOADON.Add(hoaDon);
                db.SaveChanges();

                // 3. Thêm chi tiết hóa đơn
                foreach (var ct in chiTietHoaDon)
                {
                    ct.IDHD = hoaDon.IDHD;
                    db.tb_HOADON_CT.Add(ct);

                    // 4. Cập nhật tồn kho
                    int soLuong = ct.SOLUONG ?? 0; // Chuyển đổi từ int? sang int
                    tonKho.CapNhatTonKho(ct.IDHH, ct.IDKHO, -soLuong); // Truyền int thay vì int?
                }
                db.SaveChanges();

                return hoaDon;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi bán hàng: " + ex.Message);
            }
        }
        public void delete(int id)
        {
            try
            {
                var hd = db.tb_HOADON.FirstOrDefault(x => x.IDHD == id);
                if (hd == null)
                {
                    throw new Exception($"Không tìm thấy hóa đơn với ID {id}");
                }

                var chiTiet = db.tb_HOADON_CT.Where(x => x.IDHD == id).ToList();
                TONKHO tonKho = new TONKHO();
                foreach (var ct in chiTiet)
                {
                    int soLuong = ct.SOLUONG ?? 0; // Chuyển đổi từ int? sang int, mặc định là 0 nếu null
                    tonKho.CapNhatTonKho(ct.IDHH, ct.IDKHO, soLuong); // Truyền int thay vì int?
                    db.tb_HOADON_CT.Remove(ct);
                }

                db.tb_HOADON.Remove(hd);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa: " + ex.Message);
            }
        }
    }
}
