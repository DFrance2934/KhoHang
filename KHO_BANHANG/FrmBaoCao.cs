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
using System.Windows.Forms.DataVisualization.Charting;

namespace KHO_BANHANG
{
    public partial class FrmBaoCao : DevExpress.XtraEditors.XtraForm
    {
        public FrmBaoCao()
        {
            InitializeComponent();
            dtBatDau.Value = DateTime.Today.AddDays(-7); // Mặc định 7 ngày trước
            dtKetThuc.Value = DateTime.Today.AddDays(+1); // Mặc định đến hôm nay

            // Gắn sự kiện ValueChanged cho cả hai DateTimePicker
            dtBatDau.ValueChanged += new EventHandler(dtThoiGian_Changed);
            dtKetThuc.ValueChanged += new EventHandler(dtThoiGian_Changed);

            // Tải báo cáo ban đầu
            CapNhatBaoCao();
        }

        private void dtThoiGian_Changed(object sender, EventArgs e)
        {
            CapNhatBaoCao();
        }

        private void FrmBaoCao_Load(object sender, EventArgs e)
        {

        }

        private void CapNhatBaoCao()
        {
            DateTime tuNgay = dtBatDau.Value;
            DateTime denNgay = dtKetThuc.Value;

            // Kiểm tra ngày hợp lệ
            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = Entities.CreateEntities())
                {
                    // 1. Tổng doanh thu
                    double tongDoanhThu = db.tb_HOADON
                        .Where(hd => hd.NGAYLAP >= tuNgay && hd.NGAYLAP <= denNgay)
                        .Sum(hd => hd.TONGTIEN ?? 0.0);
                    lbDoanhThu.Text = $"{tongDoanhThu:N0}";

                    // 2. Số lượng khách hàng (đếm số khách hàng duy nhất)
                    int soKhachHang = db.tb_HOADON
                        .Where(hd => hd.NGAYLAP >= tuNgay && hd.NGAYLAP <= denNgay)
                        .Select(hd => hd.IDKH)
                        .Count();
                    lbKhachHang.Text = $"{soKhachHang}";

                    // 3. Số lượng hàng hóa đã bán
                    int soHangHoa = db.tb_HOADON_CT
                        .Where(ct => ct.tb_HOADON.NGAYLAP >= tuNgay && ct.tb_HOADON.NGAYLAP <= denNgay)
                        .Sum(ct => ct.SOLUONG ?? 0);
                    lbHangHoa.Text = $"{soHangHoa}";

                    // 4. Lợi nhuận (tổng doanh thu - chi phí nhập hàng)
                    var chiTietBanHang = db.tb_HOADON_CT
                        .Where(ct => ct.tb_HOADON.NGAYLAP >= tuNgay && ct.tb_HOADON.NGAYLAP <= denNgay)
                        .GroupBy(ct => ct.IDHH)
                        .Select(g => new
                        {
                            IDHH = g.Key,
                            SoLuongBan = g.Sum(ct => ct.SOLUONG ?? 0),
                            DonGiaBan = g.Average(ct => ct.DONGIA ?? 0.0) // Giá bán trung bình
                        }).ToList();

                    double tongLoiNhuan = 0.0;
                    foreach (var item in chiTietBanHang)
                    {
                        // Lấy giá nhập gần nhất từ tb_NHAPHANG trước ngày bán
                        var giaNhap = db.tb_NHAPHANG
                            .Where(nh => nh.IDHH == item.IDHH && nh.NGAYNHAP <= denNgay)
                            .OrderByDescending(nh => nh.NGAYNHAP)
                            .Select(nh => nh.DONGIA)
                            .FirstOrDefault();

                        if (giaNhap.HasValue)
                        {
                            double loiNhuanHang = (item.DonGiaBan - giaNhap.Value) * item.SoLuongBan;
                            tongLoiNhuan += loiNhuanHang;
                        }
                        else
                        {
                            // Nếu không có giá nhập, giả định lợi nhuận bằng giá bán (có thể điều chỉnh logic nếu cần)
                            double loiNhuanHang = item.DonGiaBan * item.SoLuongBan;
                            tongLoiNhuan += loiNhuanHang;
                        }
                    }

                    lbLoiNhuan.Text = $"{tongLoiNhuan:N0}";

                    // 5. Vẽ biểu đồ đường
                    var doanhThuTheoNgay = db.tb_HOADON
                     .Where(hd => hd.NGAYLAP >= tuNgay && hd.NGAYLAP <= denNgay)
                     .GroupBy(hd => System.Data.Entity.DbFunctions.TruncateTime(hd.NGAYLAP))
                     .Select(g => new
                     {
                         Ngay = g.Key,
                         TongTien = g.Sum(hd => (double?)hd.TONGTIEN ?? 0.0) // Xử lý null an toàn
                     })
                     .OrderBy(x => x.Ngay)
                     .ToList();

                    // Lấy tất cả giá nhập từ tb_NHAPHANG để tối ưu
                    var giaNhapDict = db.tb_NHAPHANG
                        .Where(nh => nh.NGAYNHAP <= denNgay)
                        .GroupBy(nh => nh.IDHH)
                        .ToDictionary(
                            g => g.Key,
                            g => g.OrderByDescending(nh => nh.NGAYNHAP).First().DONGIA ?? 0.0
                        );
                    // Lấy dữ liệu thô từ tb_HOADON_CT và tính lợi nhuận bằng LINQ to Objects
                    var loiNhuanTheoNgayRaw = db.tb_HOADON_CT
                        .Where(ct => ct.tb_HOADON.NGAYLAP >= tuNgay && ct.tb_HOADON.NGAYLAP <= denNgay)
                        .Select(ct => new
                        {
                            Ngay = System.Data.Entity.DbFunctions.TruncateTime(ct.tb_HOADON.NGAYLAP),
                            ct.IDHH,
                            SoLuong = ct.SOLUONG ?? 0,
                            DonGiaBan = ct.DONGIA ?? 0.0
                        })
                        .ToList();
                    // Nhóm và tính lợi nhuận theo ngày bằng LINQ to Objects
                    var loiNhuanTheoNgay = loiNhuanTheoNgayRaw
                        .GroupBy(x => x.Ngay)
                        .Select(g => new
                        {
                            Ngay = g.Key,
                            LoiNhuan = g.Sum(x =>
                            {
                                double giaNhap = giaNhapDict.ContainsKey(x.IDHH) ? giaNhapDict[x.IDHH] : 0.0;
                                return (x.DonGiaBan - giaNhap) * x.SoLuong;
                            })
                        })
                        .OrderBy(x => x.Ngay)
                        .ToList();
                    // Xóa các series và chart area cũ nếu cần
                    chartDoanhThu.Series.Clear();
                    chartDoanhThu.ChartAreas.Clear();
                    chartDoanhThu.Legends.Clear(); // Ẩn legend nếu không cần

                    // Thêm ChartArea mới
                    ChartArea chartArea = new ChartArea("MainArea");
                    chartDoanhThu.ChartAreas.Add(chartArea);

                    // Tạo Series mới
                    Series seriesDoanhThu = new Series("Doanh Thu")
                    {
                        ChartType = SeriesChartType.Column, // Biểu đồ cột
                        XValueType = ChartValueType.Date,   // Trục X là ngày
                        YValueType = ChartValueType.Double,
                        ChartArea = "MainArea",
                        IsValueShownAsLabel = true, // Hiển thị giá trị trên đầu mỗi cột
                        LabelFormat = "N0"
                    };

                    // Thêm dữ liệu vào Series
                    foreach (var item in doanhThuTheoNgay)
                    {
                        seriesDoanhThu.Points.AddXY(item.Ngay, item.TongTien);
                    }
                    // Tạo Series cho Lợi Nhuận (biểu đồ đường)
                    Series seriesLoiNhuan = new Series("Lợi Nhuận")
                    {
                        ChartType = SeriesChartType.Line,
                        XValueType = ChartValueType.Date,
                        YValueType = ChartValueType.Double,
                        ChartArea = "MainArea",
                        IsValueShownAsLabel = true, // Hiển thị giá trị trên mỗi dấu chấm đỏ
                        LabelFormat = "N0",
                        Color = System.Drawing.Color.Red,
                        BorderWidth = 3,
                        MarkerStyle = MarkerStyle.Circle, // Thêm điểm đánh dấu hình tròn
                        MarkerSize = 6, // Kích thước điểm đánh dấu
                        MarkerColor = System.Drawing.Color.Red // Điểm đánh dấu cùng màu với đường
                    };
                    // Thêm dữ liệu vào Series Lợi Nhuận
                    foreach (var item in loiNhuanTheoNgay)
                    {
                        seriesLoiNhuan.Points.AddXY(item.Ngay, item.LoiNhuan);
                    }
                    // Thêm Series vào Chart
                    chartDoanhThu.Series.Add(seriesDoanhThu);
                    chartDoanhThu.Series.Add(seriesLoiNhuan);

                    // Cấu hình trục X/Y
                    chartArea.AxisX.Title = "Thời Gian";
                    chartArea.AxisY.Title = "Doanh Thu / Lợi Nhuận (VND)";
                    chartArea.AxisY.LabelStyle.Format = "N0";
                    chartArea.AxisX.LabelStyle.Angle = -45;
                    chartArea.AxisX.IntervalType = DateTimeIntervalType.Days;
                    chartArea.AxisX.Interval = 1;
                    chartArea.AxisX.LabelStyle.Format = "dd/MM/yyyy";
                    chartArea.AxisX.MajorGrid.Enabled = false;
                    chartArea.AxisY.MajorGrid.Enabled = true;

                    // Hiển thị legend để phân biệt Doanh Thu và Lợi Nhuận
                    Legend legend = new Legend("Legend");
                    chartDoanhThu.Legends.Add(legend);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
    
}