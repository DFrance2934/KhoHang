using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class NHAPHANG
    {
        Entities db;
        public NHAPHANG()
        {
            db = Entities.CreateEntities();
        }
        public tb_NHAPHANG getItem(int id)
        {
            return db.tb_NHAPHANG.FirstOrDefault(x => x.ID == id);
        }
        //public List<tb_NHAPHANG> getAll()
        //{
        //    return db.tb_NHAPHANG.ToList();
        //}
        public List<obj_NhapHang> getListFull(int id)
        {
            var lst = db.tb_NHAPHANG.Where(x=>x.ID == id).ToList();
            List<obj_NhapHang> lstObj = new List<obj_NhapHang>();
            obj_NhapHang nh;
            foreach (var item in lst)
            {
                nh = new obj_NhapHang();
                nh.ID = item.ID;
                nh.IDHH = item.IDHH;
                var t = db.tb_HANGHOA.FirstOrDefault(x => x.IDHH==item.IDHH);
                nh.TENHH = t.TENHH;
                nh.IDNCC = item.IDNCC;
                var c = db.tb_NHACUNGCAP.FirstOrDefault(x => x.IDNCC == item.IDNCC);
                nh.TENNCC = c.TENNCC;
                nh.IDKHO = item.IDKHO;
                var k = db.tb_KHO.FirstOrDefault(x => x.IDKHO==item.IDKHO);
                nh.TENKHO = k.TENKHO;
                nh.DONGIA = item.DONGIA;
                nh.SOLUONG = item.SOLUONG;
                nh.NGAYNHAP = item.NGAYNHAP;

                lstObj.Add(nh);
            }
            return lstObj;
        }
        public List<obj_NhapHang> getList()
        {
            var query = from nh in db.tb_NHAPHANG
                        join hh in db.tb_HANGHOA on nh.IDHH equals hh.IDHH
                        join ncc in db.tb_NHACUNGCAP on nh.IDNCC equals ncc.IDNCC
                        join k in db.tb_KHO on nh.IDKHO equals k.IDKHO
                        
                        select new obj_NhapHang
                        {
                            ID = nh.ID,
                            IDHH = hh.IDHH,
                            IDKHO = k.IDKHO,
                            IDNCC = ncc.IDNCC,
                            TENHH = hh.TENHH,
                            TENNCC = ncc.TENNCC,
                            TENKHO = k.TENKHO,
                            SOLUONG = nh.SOLUONG,
                            NGAYNHAP = nh.NGAYNHAP,
                            DONGIA = nh.DONGIA
                        };

            return query.ToList();
        }
        public tb_NHAPHANG add(tb_NHAPHANG nh)
        {
            try
            {
                
                db.tb_NHAPHANG.Add(nh);
                db.SaveChanges();
                int soluongMoi = nh.SOLUONG ?? 0;
                TONKHO tonKho = new TONKHO();
                //var ton = tonKho.getItem(nh.IDHH, nh.IDKHO);
                //int soluongTonKho = ton?.SOLUONG ?? 0;
                tonKho.CapNhatTonKho(nh.IDHH, nh.IDKHO, soluongMoi);
                return nh;
            }

            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

        }
        //public tb_NHAPHANG update(tb_NHAPHANG nh)
        //{
        //    var _nh = db.tb_NHAPHANG.FirstOrDefault(x => x.ID == nh.ID);
        //    int soluongCu = _nh.SOLUONG ?? 0;
        //    int soluongMoi = nh.SOLUONG ?? 0;
        //    string idhhCu = _nh.IDHH.ToString();
        //    string idkhoCu = _nh.IDKHO;

        //    string idhhMoi = nh.IDHH.ToString();
        //    string idkhoMoi = nh.IDKHO;
        //    _nh.IDHH = nh.IDHH;
        //    _nh.IDKHO = nh.IDKHO;
        //    _nh.IDNCC = nh.IDNCC;
        //    _nh.SOLUONG = nh.SOLUONG;
        //    _nh.NGAYNHAP = nh.NGAYNHAP;
        //    _nh.DONGIA = nh.DONGIA;
        //    //try
        //    //{

        //    //    int chenhlech = soluongMoi - soluongCu;
        //    //    db.SaveChanges();
        //    //    TONKHO tonKho = new TONKHO();
        //    //    tonKho.CapNhatTonKho(nh.IDHH, nh.IDKHO, chenhlech);
        //    //    return _nh;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw new Exception("Lỗi: " + ex.Message);
        //    //}
        //    try
        //    {
        //        db.SaveChanges();

        //        TONKHO tonKho = new TONKHO();

        //        // Nếu mặt hàng hoặc kho bị thay đổi
        //        if (idhhCu != idhhMoi || idkhoCu != idkhoMoi)
        //        {
        //            // Trừ số lượng cũ khỏi tồn kho cũ
        //            tonKho.CapNhatTonKho(int.Parse(idhhCu), idkhoCu, -soluongCu);

        //            // Cộng số lượng mới vào tồn kho mới
        //            tonKho.CapNhatTonKho(nh.IDHH, nh.IDKHO, soluongMoi);
        //        }
        //        else
        //        {
        //            // Cập nhật chênh lệch số lượng
        //            int chenhlech = soluongMoi - soluongCu;
        //            tonKho.CapNhatTonKho(nh.IDHH, nh.IDKHO, chenhlech);
        //        }

        //        return _nh;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Lỗi cập nhật phiếu nhập: " + ex.Message);
        //    }

        //}
        public void delete(int id)
        {
            try
            {
                var nh = db.tb_NHAPHANG.FirstOrDefault(x => x.ID == id);
                if (nh != null)
                {
                    // Cập nhật tồn kho trước khi xóa
                    int soluongCu = nh.SOLUONG ?? 0;
                    TONKHO tonKho = new TONKHO();
                    tonKho.CapNhatTonKho(nh.IDHH, nh.IDKHO, -soluongCu); // trừ lại số lượng nhập

                    db.tb_NHAPHANG.Remove(nh);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Không tìm thấy phiếu nhập hàng có ID: " + id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa: " + ex.Message);
            }
        }
    }
}
