using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class NHACUNGCAP
    {
        Entities db;
        public NHACUNGCAP()
        {
            db = Entities.CreateEntities();
        }
        public tb_NHACUNGCAP getItem(string idncc)
        {
            return db.tb_NHACUNGCAP.FirstOrDefault(x => x.IDNCC == idncc);
        }
        public List<tb_NHACUNGCAP> getAll()
        {
            return db.tb_NHACUNGCAP.ToList();
        }
        public void add(tb_NHACUNGCAP nhacungcap)
        {
            try
            {
                db.tb_NHACUNGCAP.Add(nhacungcap);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void update(tb_NHACUNGCAP nhacungcap)
        {
            tb_NHACUNGCAP _nhacungcap = db.tb_NHACUNGCAP.FirstOrDefault(x => x.IDNCC == nhacungcap.IDNCC);
            _nhacungcap.IDNCC = nhacungcap.IDNCC;
            _nhacungcap.TENNCC = nhacungcap.TENNCC;
            _nhacungcap.SDT = nhacungcap.SDT;
            _nhacungcap.DIACHI = nhacungcap.DIACHI;
            _nhacungcap.EMAIL = nhacungcap.EMAIL;
            _nhacungcap.DISABLED = nhacungcap.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string idncc)
        {
            tb_NHACUNGCAP nhacungcap = db.tb_NHACUNGCAP.FirstOrDefault(x => x.IDNCC == idncc);
            nhacungcap.DISABLED = true;
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
