using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class HANGHOA
    {
        Entities db;
        public HANGHOA()
        {
            db = Entities.CreateEntities();
        }
        public tb_HANGHOA getItem(int idhh)
        {
            return db.tb_HANGHOA.FirstOrDefault(x => x.IDHH == idhh);
        }
        public List<tb_HANGHOA> getAll()
        {
            return db.tb_HANGHOA.ToList();
        }
        public List<tb_HANGHOA> getListByNhom(int idNhom)
        {
            return db.tb_HANGHOA.Where(x => x.IDNHOM == idNhom).ToList();
        }

        //public List<tb_HANGHOA> getListByKeyword(string keyword)
        //{
        //    return db.tb_HANGHOA.Where(ts => ts.TENHH.Contains(keyword)).ToList();
        //}
        public List<obj_HangHoa> getListByNhomFull(int idNhom)
        {
            var lst = db.tb_HANGHOA.Where(x => x.IDNHOM == idNhom).ToList();
            List<obj_HangHoa> lstObj = new List<obj_HangHoa>();
            obj_HangHoa hh;
            foreach (var item in lst)
            {
                hh = new obj_HangHoa();
                hh.IDHH = item.IDHH;
                hh.TENHH = item.TENHH;
                hh.IDNHOM = item.IDNHOM;
                var n = db.tb_NHOMHH.FirstOrDefault(x => x.IDNHOM == item.IDNHOM);
                hh.TENNHOMHH = n.TENNHOMHH;
                hh.DVT = item.DVT;
                hh.DONGIA = item.DONGIA;
                hh.IDNCC = item.IDNCC;
                var c = db.tb_NHACUNGCAP.FirstOrDefault(x => x.IDNCC == item.IDNCC);
                hh.TENNCC = c.TENNCC;
                hh.MOTA = item.MOTA;
                lstObj.Add(hh);
            }
            return lstObj;
        }
        public tb_HANGHOA add(tb_HANGHOA hh)
        {
            try
            {
                db.tb_HANGHOA.Add(hh);
                db.SaveChanges();
                return hh;
            }

            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

        }
        public tb_HANGHOA update(tb_HANGHOA hh)
        {
            tb_HANGHOA _hh = db.tb_HANGHOA.FirstOrDefault(x => x.IDHH == hh.IDHH);
            _hh.TENHH = hh.TENHH;
            _hh.DVT = hh.DVT;
            _hh.DONGIA = hh.DONGIA;
            _hh.IDNCC = hh.IDNCC;
            _hh.IDNHOM = hh.IDNHOM;
            _hh.MOTA = hh.MOTA;
            _hh.DISABLED = hh.DISABLED;

            try
            {
                db.SaveChanges();
                return _hh;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

        }
        public void delete(string idhh)
        {
            tb_HANGHOA _hh = db.tb_HANGHOA.FirstOrDefault(x => x.IDHH.ToString() == idhh);

            _hh.DISABLED = true;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

        }
    }
}
