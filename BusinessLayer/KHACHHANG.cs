using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class KHACHHANG
    {
        Entities db;
        public KHACHHANG()
        {
            db = Entities.CreateEntities();
        }
        public tb_KHACHHANG getItem(int idkh)
        {
            return db.tb_KHACHHANG.FirstOrDefault(x => x.IDKH == idkh);
        }
        public List<tb_KHACHHANG> getAll()
        {
            return db.tb_KHACHHANG.ToList();
        }
        public tb_KHACHHANG TimKiemTheoSDT(string sdt)
        {
            return db.tb_KHACHHANG.FirstOrDefault(x => x.SDT == sdt);
        }
        public void add(tb_KHACHHANG khachhang)
        {
            try
            {
                db.tb_KHACHHANG.Add(khachhang);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void update(tb_KHACHHANG khachhang)
        {
            tb_KHACHHANG _khachhang = db.tb_KHACHHANG.FirstOrDefault(x => x.IDKH == khachhang.IDKH);
            _khachhang.IDKH = khachhang.IDKH;
            _khachhang.TENKH = khachhang.TENKH;
            _khachhang.SDT = khachhang.SDT;
            _khachhang.DIACHI = khachhang.DIACHI;
            _khachhang.EMAIL = khachhang.EMAIL;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
    }
}
