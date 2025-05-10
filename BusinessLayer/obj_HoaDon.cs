using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class obj_HoaDon
    {
        public int IDHD { get; set; }
        public int IDKH { get; set; }
        public string TENKH { get; set; }
        public string SDT { get; set; }
        public Nullable<System.DateTime> NGAYLAP { get; set; }
        public Nullable<double> TONGTIEN { get; set; }
    }
}
