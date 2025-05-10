using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO_BANHANG
{
    public partial class FrmTonKho : DevExpress.XtraEditors.XtraForm
    {
        public FrmTonKho()
        {
            InitializeComponent();
        }
        TONKHO _tonkho;
        List<obj_TonKho> _lstTK = new List<obj_TonKho>();
        private void FrmTonKho_Load(object sender, EventArgs e)
        {
            _tonkho = new TONKHO();
            loadData();
        }
        void loadData()
        {
            gcDanhSach.DataSource = _tonkho.getList();
            gvDanhSach.OptionsBehavior.Editable = false;          
        }
    }
}