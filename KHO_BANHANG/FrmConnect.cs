using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO_BANHANG
{
    public partial class FrmConnect : DevExpress.XtraEditors.XtraForm
    {
        public FrmConnect()
        {
            InitializeComponent();
        }
        SqlConnection GetCon(string server, string username, string pass, string database)
        {
            return new SqlConnection("Data Source=" + server + "; Initial Catalog=" + database + "; User ID=" + username + "; Password=" + pass + ";");
        }
        private void FrmConnect_Load(object sender, EventArgs e)
        {

        }

        private void btnKiemtra_Click(object sender, EventArgs e)
        {
            SqlConnection con = GetCon(txtServer.Text, txtUsername.Text, txtPassWord.Text, cboDatabase.Text);
            try
            {
                con.Open();
                MessageBox.Show("Success!");
            }
            catch
            {
                MessageBox.Show("Unsuccess!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string enCryptServ = Encryptor.Encrypt(txtServer.Text, "qwertyuiop", true);
            string enCryptUser = Encryptor.Encrypt(txtUsername.Text, "qwertyuiop", true);
            string enCryptPass = Encryptor.Encrypt(txtPassWord.Text, "qwertyuiop", true);
            string enCryptData = Encryptor.Encrypt(cboDatabase.Text, "qwertyuiop", true);
            connect cn = new connect(enCryptServ, enCryptUser, enCryptPass, enCryptData);
            cn.SaveFile();
            MessageBox.Show("Lưu file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cboDatabase_MouseClick(object sender, MouseEventArgs e)
        {
            cboDatabase.Items.Clear();
            string Conn = "server=" + txtServer.Text + "; User ID=" + txtUsername.Text + "; pwd=" + txtPassWord.Text + ";";
            SqlConnection con = new SqlConnection(Conn);
            con.Open();
            string sql = "Select name from sys.databases where name not in ('master','tempdb','model','msdb')";
            SqlCommand cmd = new SqlCommand(sql, con);
            IDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboDatabase.Items.Add(dr[0].ToString());
            }
        }
    }
}