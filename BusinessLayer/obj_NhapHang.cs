using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class obj_NhapHang
    {
        public int ID { get; set; }
        public int IDHH { get; set; }
        public string TENHH { get; set; }
        public string IDNCC { get; set; }
        public string TENNCC { get; set; }
        public string IDKHO { get; set; }
        public string TENKHO { get; set; }
        public Nullable<int> SOLUONG { get; set; }
        public Nullable<System.DateTime> NGAYNHAP { get; set; }
        public Nullable<double> DONGIA { get; set; }
    }
}
