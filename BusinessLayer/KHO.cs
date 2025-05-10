using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class KHO
    {
        Entities db;
        TONKHO _tonkho;
        public KHO()
        {
            db = Entities.CreateEntities();
            _tonkho = new TONKHO();
        }
        public tb_KHO getItem(string idkho)
        {
            return db.tb_KHO.FirstOrDefault(x=>x.IDKHO == idkho);
        }
        public List<tb_KHO> getAll()
        {
            return db.tb_KHO.ToList();
        }
        public List<tb_KHO> getKhoByHangHoa(int idhh)
        {
            var tonKhoList = _tonkho.getList()
                .Where(x => x.IDHH == idhh && x.SOLUONG > 0)
                .Select(x => x.IDKHO)
                .ToList();

            return db.tb_KHO
                .Where(x => tonKhoList.Contains(x.IDKHO))
                .ToList();
        }
        public void add(tb_KHO kho)
        {
            try
            {
                db.tb_KHO.Add(kho);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void update(tb_KHO kho)
        {
            tb_KHO _kho = db.tb_KHO.FirstOrDefault(x=>x.IDKHO == kho.IDKHO);
            _kho.IDKHO = kho.IDKHO;
            _kho.TENKHO = kho.TENKHO;
            _kho.SDT = kho.SDT;
            _kho.DIACHI = kho.DIACHI;
            _kho.DISABLED = kho.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string idkho)
        {
            tb_KHO kho = db.tb_KHO.FirstOrDefault(x => x.IDKHO == idkho);
            kho.DISABLED = true;
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
