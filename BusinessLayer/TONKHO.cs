using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TONKHO
    {
        Entities db;
        public TONKHO()
        {
            db = Entities.CreateEntities();
        }
        public tb_TONKHO getItem(int idhh,  string idkho)
        {
            return db.tb_TONKHO.FirstOrDefault(x => x.IDHH == idhh &&
                                                      x.IDKHO == idkho);
        }
        public void CapNhatTonKho(int idhh, string idkho, int chenhlech)
        {
            var tonKho = getItem(idhh, idkho);
            if (tonKho != null)
            {
                tonKho.SOLUONG += chenhlech;
                if (tonKho.SOLUONG < 0)
                {
                    tonKho.SOLUONG = 0;
                }
                db.SaveChanges();
            }
            else
            {
                // Nếu chưa có tồn kho, thì tạo mới
                var newTonKho = new tb_TONKHO
                {
                    IDHH = idhh,
                    IDKHO = idkho,
                    SOLUONG = chenhlech
                };
                db.tb_TONKHO.Add(newTonKho);
                db.SaveChanges();
            }
        }
        public List<obj_TonKho> getList()
        {
            var query = from tk in db.tb_TONKHO
                        join hh in db.tb_HANGHOA on tk.IDHH equals hh.IDHH
                        join k in db.tb_KHO on tk.IDKHO equals k.IDKHO
                        select new obj_TonKho
                        {
                            IDHH = tk.IDHH,
                            IDKHO = tk.IDKHO,
                            TENHH = hh.TENHH,
                            TENKHO = k.TENKHO,
                            SOLUONG = tk.SOLUONG
                        };

            return query.ToList();
        }

    }
}
