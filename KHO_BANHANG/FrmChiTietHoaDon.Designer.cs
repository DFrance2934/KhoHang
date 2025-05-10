namespace KHO_BANHANG
{
    partial class FrmChiTietHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gcChiTiet = new DevExpress.XtraGrid.GridControl();
            this.gvChiTiet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IDCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IDHH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IDHD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IDKHO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TENHH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TENKHO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SOLUONG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DONGIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.THANHTIEN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChiTiet)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcChiTiet
            // 
            this.gcChiTiet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcChiTiet.Location = new System.Drawing.Point(0, 68);
            this.gcChiTiet.MainView = this.gvChiTiet;
            this.gcChiTiet.Name = "gcChiTiet";
            this.gcChiTiet.Size = new System.Drawing.Size(1133, 637);
            this.gcChiTiet.TabIndex = 10;
            this.gcChiTiet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChiTiet});
            // 
            // gvChiTiet
            // 
            this.gvChiTiet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IDCT,
            this.IDHH,
            this.IDHD,
            this.IDKHO,
            this.TENHH,
            this.TENKHO,
            this.SOLUONG,
            this.DONGIA,
            this.THANHTIEN});
            this.gvChiTiet.GridControl = this.gcChiTiet;
            this.gvChiTiet.Name = "gvChiTiet";
            this.gvChiTiet.OptionsView.ShowGroupPanel = false;
            this.gvChiTiet.RowHeight = 25;
            // 
            // IDCT
            // 
            this.IDCT.Caption = "IDCT";
            this.IDCT.FieldName = "IDCT";
            this.IDCT.MinWidth = 25;
            this.IDCT.Name = "IDCT";
            this.IDCT.Width = 94;
            // 
            // IDHH
            // 
            this.IDHH.Caption = "IDHH";
            this.IDHH.FieldName = "IDHH";
            this.IDHH.MinWidth = 25;
            this.IDHH.Name = "IDHH";
            this.IDHH.Width = 94;
            // 
            // IDHD
            // 
            this.IDHD.Caption = "IDHD";
            this.IDHD.FieldName = "IDHD";
            this.IDHD.MinWidth = 25;
            this.IDHD.Name = "IDHD";
            this.IDHD.Width = 94;
            // 
            // IDKHO
            // 
            this.IDKHO.Caption = "IDKHO";
            this.IDKHO.FieldName = "IDKHO";
            this.IDKHO.MinWidth = 25;
            this.IDKHO.Name = "IDKHO";
            this.IDKHO.Width = 94;
            // 
            // TENHH
            // 
            this.TENHH.Caption = "TÊN HÀNG HÓA";
            this.TENHH.FieldName = "TENHH";
            this.TENHH.MaxWidth = 400;
            this.TENHH.MinWidth = 25;
            this.TENHH.Name = "TENHH";
            this.TENHH.Visible = true;
            this.TENHH.VisibleIndex = 0;
            this.TENHH.Width = 94;
            // 
            // TENKHO
            // 
            this.TENKHO.Caption = "KHO";
            this.TENKHO.FieldName = "TENKHO";
            this.TENKHO.MaxWidth = 300;
            this.TENKHO.MinWidth = 25;
            this.TENKHO.Name = "TENKHO";
            this.TENKHO.Visible = true;
            this.TENKHO.VisibleIndex = 1;
            this.TENKHO.Width = 94;
            // 
            // SOLUONG
            // 
            this.SOLUONG.Caption = "SỐ LƯỢNG";
            this.SOLUONG.FieldName = "SOLUONG";
            this.SOLUONG.MaxWidth = 100;
            this.SOLUONG.MinWidth = 25;
            this.SOLUONG.Name = "SOLUONG";
            this.SOLUONG.Visible = true;
            this.SOLUONG.VisibleIndex = 2;
            this.SOLUONG.Width = 94;
            // 
            // DONGIA
            // 
            this.DONGIA.Caption = "ĐƠN GIÁ";
            this.DONGIA.FieldName = "DONGIA";
            this.DONGIA.MaxWidth = 200;
            this.DONGIA.MinWidth = 25;
            this.DONGIA.Name = "DONGIA";
            this.DONGIA.Visible = true;
            this.DONGIA.VisibleIndex = 3;
            this.DONGIA.Width = 94;
            // 
            // THANHTIEN
            // 
            this.THANHTIEN.Caption = "THÀNH TIỀN";
            this.THANHTIEN.FieldName = "THANHTIEN";
            this.THANHTIEN.MaxWidth = 200;
            this.THANHTIEN.MinWidth = 25;
            this.THANHTIEN.Name = "THANHTIEN";
            this.THANHTIEN.Visible = true;
            this.THANHTIEN.VisibleIndex = 4;
            this.THANHTIEN.Width = 94;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.lbTen);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1133, 68);
            this.panel1.TabIndex = 11;
            // 
            // lbTen
            // 
            this.lbTen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbTen.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTen.Location = new System.Drawing.Point(157, 12);
            this.lbTen.Name = "lbTen";
            this.lbTen.Size = new System.Drawing.Size(281, 40);
            this.lbTen.TabIndex = 1;
            this.lbTen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên khách hàng";
            // 
            // FrmChiTietHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 705);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gcChiTiet);
            this.Name = "FrmChiTietHoaDon";
            this.Text = "Chi Tiết Hóa Đơn";
            this.Load += new System.EventHandler(this.FrmChiTietHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChiTiet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcChiTiet;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChiTiet;
        private DevExpress.XtraGrid.Columns.GridColumn IDCT;
        private DevExpress.XtraGrid.Columns.GridColumn IDHH;
        private DevExpress.XtraGrid.Columns.GridColumn IDHD;
        private DevExpress.XtraGrid.Columns.GridColumn IDKHO;
        private DevExpress.XtraGrid.Columns.GridColumn TENHH;
        private DevExpress.XtraGrid.Columns.GridColumn TENKHO;
        private DevExpress.XtraGrid.Columns.GridColumn SOLUONG;
        private DevExpress.XtraGrid.Columns.GridColumn DONGIA;
        private DevExpress.XtraGrid.Columns.GridColumn THANHTIEN;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTen;
        private System.Windows.Forms.Label label1;
    }
}