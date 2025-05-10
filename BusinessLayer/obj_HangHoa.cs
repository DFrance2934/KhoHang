using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class obj_HangHoa
    {
        public int IDHH { get; set; }
        public string TENHH { get; set; }
        public Nullable<double> DONGIA { get; set; }
        public string MOTA { get; set; }
        public Nullable<bool> DISABLED { get; set; }
        public string DVT { get; set; }
        public string IDNCC { get; set; }
        public string TENNCC {  get; set; }
        public Nullable<int> IDNHOM { get; set; }
        public string TENNHOMHH { get; set; }
    }
}
