using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class HOADONCT
    {
        Entities db;
        public HOADONCT()
        {
            db = Entities.CreateEntities();
        }

        public tb_HOADON_CT getItem(int id)
        {
            return db.tb_HOADON_CT.FirstOrDefault(x => x.IDCT == id);
        }

        public List<obj_HoaDonCT> GetListByKhachHang(int idkh)
        {
            var query = from hd in db.tb_HOADON
                        join ct in db.tb_HOADON_CT on hd.IDHD equals ct.IDHD
                        join hh in db.tb_HANGHOA on ct.IDHH equals hh.IDHH
                        join k in db.tb_KHO on ct.IDKHO equals k.IDKHO
                        where hd.IDKH == idkh
                        select new obj_HoaDonCT
                        {
                            IDCT = ct.IDCT,
                            IDHD = hd.IDHD,
                            IDHH = ct.IDHH,
                            TENHH = hh.TENHH,
                            IDKHO = ct.IDKHO,
                            TENKHO = k.TENKHO,
                            SOLUONG = ct.SOLUONG,
                            DONGIA = ct.DONGIA,
                            THANHTIEN = ct.SOLUONG * ct.DONGIA
                        };
            return query.ToList();
        }
    }
}
