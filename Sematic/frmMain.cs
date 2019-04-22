using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Semantic_Triangle
{
    public partial class frm_Ve : Form
    {
        private Triangle triangle = new Triangle();
        private Graphics g;

        // các flag điều khiển các sự kiện.
        // private bool isBtnKichHoa_Clicked = false;
        private bool isBtnTinh_Clicked = false;
    
        private bool isBtnThemCT_Clicked = false;
        private bool isBtnNap_Clicked = false;
        public frm_Ve()
        {
            InitializeComponent();
            g = this.panel2.CreateGraphics();


            // set size hiện thị ra màn hình
            this.Size = new Size(1300, 730);

        }


        private void frm_Ve_Load(object sender, EventArgs e)
        {
            DrawMatrix(triangle.Arr, g, new Point(25, 25));
            // unable các checkbox...
            List<CheckBox> chkLists = this.grYeuTo.Controls.OfType<CheckBox>().ToList();
            foreach (var chkTemp in chkLists)
            {
                chkTemp.Enabled = false;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // vẽ lại matrix khi panel2 mất focus
            DrawMatrix(triangle.Arr, e.Graphics, new Point(25, 25));
        }

        // vẽ bảng trạng thái biểu diễn cho ma trận đồ thị kề.
        private void DrawMatrix(int[,] arrTriangle, Graphics g, Point diemVeBang)
        {
            Point temp = new Point(diemVeBang.X, diemVeBang.Y);

            int nDong = arrTriangle.GetLength(0); //  = 16
            int nCot = Triangle.nColumn;
            // size của mỗi hình chữ nhật con được vẽ ra.
            int size = 22;
            Rectangle re;
            // tọa độ vẽ bảng trong panel
            for (int i = 0; i < nDong; i++)
            {
                temp.Y += size;

                for (int j = 0; j < nCot; j++)
                {
                    temp.X += size;

                    re = new Rectangle(temp.X, temp.Y, size, size);




                    if (arrTriangle[i, j] == -1)
                    {
                        g.FillRectangle(Brushes.LightBlue, re);
                        g.DrawString(arrTriangle[i, j].ToString(), this.Font, Brushes.Purple,
                       new PointF(temp.X + size / 2 - 2, temp.Y + size / 2 - 3));
                    }
                    else if (arrTriangle[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.GreenYellow, re);
                        g.DrawString(arrTriangle[i, j].ToString(), this.Font, Brushes.Blue,
                       new PointF(temp.X + size / 2 - 2, temp.Y + size / 2 - 3));
                    }
                    else
                    {
                        g.FillRectangle(Brushes.DimGray, re);
                        g.DrawString("-", this.Font, Brushes.Black,
                      new PointF(temp.X + size / 2 - 2, temp.Y + size / 2 - 3));
                    }
                    g.DrawRectangle(Pens.Black, re);
                }
                temp.X = diemVeBang.X;
            }

            // cập lai tọa độ ban đầu
            temp.X = diemVeBang.X;
            temp.Y = diemVeBang.Y;

            Point temp1 = new Point(temp.X, temp.Y);
            for (int i = 0; i <= nCot; i++)
            {
                Rectangle rectangle = new Rectangle(temp1.X, temp1.Y, size, size);
                g.FillRectangle(Brushes.LightGreen, rectangle);
                g.DrawString("(" + i.ToString() + ")", this.Font, Brushes.Black,
              new PointF(temp1.X + size / 2 - 9, temp1.Y + size / 2 - 3));
                g.DrawRectangle(Pens.Black, rectangle);
                temp1.X += size;
            }


            Point temp2 = new Point(temp.X, temp.Y);
            for (int i = 0; i <= nDong; i++)
            {
                Rectangle rectangle = new Rectangle(temp2.X, temp2.Y, size, size);
                g.FillRectangle(Brushes.LightGreen, rectangle);
                g.DrawString(Triangle.arrYeuTo[i], this.Font, Brushes.Black,
                    new PointF(temp2.X + size / 2 - 9, temp2.Y + size / 2 - 3));
                g.DrawRectangle(Pens.Black, rectangle);
                temp2.Y += size;
            }
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            // enable các checkbox...
            List<CheckBox> chkLists = this.grYeuTo.Controls.OfType<CheckBox>().ToList();
            foreach (var chkTemp in chkLists)
            {
                if (chkTemp.Checked == false)
                    chkTemp.Enabled = false;
            }


            // tạo danh sách các CheckBox nằm trong panel3
            // mỗi checkbox tuong ung voi cac yeu to cua tam giac
            //if (!isBtnKichHoa_Clicked)
            //{
            //    Array.Copy(triangle.ArrOri, triangle.Arr, triangle.ArrOri.Length);

            //}


            if (isBtnThemCT_Clicked)
            {
                Array.Copy(triangle.ArrOri, triangle.Arr, triangle.ArrOri.Length);
                int[] temp = CongThucThemVao();
                triangle.ThemCongThuc(temp);
                // cập nhật lại arrDem sau khi thêm công thức vào.
                Triangle.arrDem = triangle.DemYeuTo(-1);
            }

            if (isBtnNap_Clicked)
            {
                //c1
                chkLists = this.grYeuTo.Controls.OfType<CheckBox>().ToList();
                foreach (var chkTemp in chkLists)
                {
                    //    foreach (Control x in this.Controls)
                    //{
                    //    if (x is TextBox)
                    //    {
                    //        ((TextBox)x).Text = String.Empty;
                    //    }
                    //}
                    //c2
                    if (chkTemp.Checked == true)
                    {
                        int sttItem = Status_Check(chkTemp);
                        triangle.KhoiTaoMangTG(sttItem);
                    }
                }
            }



            // vẽ lại bảng sau khi qua bước khởi tạo
            DrawMatrix(triangle.Arr, g, new Point(25, 25));

            //flag xử lý xự kiện 
            //this.isBtnKichHoa_Clicked = false;
            this.isBtnTinh_Clicked = false;
           
            this.isBtnThemCT_Clicked = false;
            this.isBtnNap_Clicked = false;

            // cập nhật nội dung textbox giá trị trống
            this.txtKichHoat.Text = "";
            // xóa  nội dụng trong collection dictionary
            Triangle.dinhKichHoat.Clear();
        }


        private void btnCongThuc_Click(object sender, EventArgs e)
        {
            this.pnHienThi.BackgroundImage = global::Sematic.Properties.Resources.tamgiac1;
            this.pnHienThi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }


        //hiển thị các công thức được sử dụng trong chương trình lên pnHienThi
        private void pnHienThi_DoubleClick(object sender, EventArgs e)
        {
            this.pnHienThi.BackgroundImage = global::Sematic.Properties.Resources.Cong_Thuc;
            this.pnHienThi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        // kiểm tra có phải các checkbox đã bị nhấn hay chưa và trả về giá trị tương ứng
        private int Status_Check(object sender)
        {
            CheckBox checkBox = sender as CheckBox;
            // trả về giá trị lần lượt từ 0 - 15 từ trên xuống theo thư tự của các checkbox
            switch (checkBox.Name)
            {
                case "chkCanhA":
                    {
                        return 0;
                    }

                case "chkCanhB":
                    {
                        return 1;
                    }
                case "chkCanhC":
                    {
                        return 2;
                    }

                case "chkGocA":
                    {
                        return 3;
                    }
                case "chkGocB":
                    {
                        return 4;
                    }

                case "chkGocC":
                    {
                        return 5;
                    }
                case "chkNuaChuVi":
                    {
                        return 6;
                    }

                case "chkDienTich":
                    {
                        return 7;
                    }
                case "chkDttMa":
                    {
                        return 8;
                    }

                case "chkDttMb":
                    {
                        return 9;
                    }
                case "chkDttMc":
                    {
                        return 10;
                    }

                case "chkDcHa":
                    {
                        return 11;
                    }
                case "chkDcHb":
                    {
                        return 12;
                    }

                case "chkDcHc":
                    {
                        return 13;
                    }
                case "chkNgoaiTiep":
                    {
                        return 14;
                    }

                case "chKNoiTiep":
                    {
                        return 15;
                    }
                default:
                    return -2;
            }
        }

        // hàm trả về mảng chứa các yếu tố của công thức vừa thêm vào.
        private int[] CongThucThemVao()
        {
            int[] arr = new int[16];
            List<CheckBox> chkLists = this.grYeuTo.Controls.OfType<CheckBox>().ToList();
            foreach (var chkTemp in chkLists)
            {
                if (chkTemp.Checked == true)
                {
                    int sttItem = Status_Check(chkTemp);
                    arr[sttItem] = -1;
                }
            }
            return arr;
        }
        private void btnThemCT_Click(object sender, EventArgs e)
        {
            // ẩn các text box
            List<TextBox> txtLists = this.grYeuTo.Controls.OfType<TextBox>().ToList();
            foreach (var txtTemp in txtLists)
            {
                txtTemp.Visible =false;
            }


            // thay màu nền và tiêu đề của group box
            this.grYeuTo.Text = "Chọn yếu tố có trong công thức cần thêm vào:";
            this.grYeuTo.BackColor = System.Drawing.Color.Lime;

            //flag xử lý sự kiện 
            // this.isBtnKichHoa_Clicked = false;
            this.isBtnThemCT_Clicked = true;

            // recover list check box
            List<CheckBox> chkLists = this.grYeuTo.Controls.OfType<CheckBox>().ToList();
            foreach (var chkTemp in chkLists)
            {
                    chkTemp.Checked = false;
                    chkTemp.Enabled = true;
            }

           
            

        }

        private void btnKichHoat_Click(object sender, EventArgs e)
        {
            //flag xử lý xự kiện 
            // this.isBtnKichHoa_Clicked = true;
            this.isBtnTinh_Clicked = false;
            // cập nhật nội dung textbox giá trị trống
            this.txtKichHoat.Text = "";
            //xử lý
            triangle.LanTruyenKichHoat();

            //vẽ hình
            DrawMatrix(triangle.Arr, g, new Point(this.Size.Width / 2 + 5, 25));

            //cập nhật nội dụng textbox 
            string strKichHoat = "";
            foreach (KeyValuePair<int, string> entry in Triangle.dinhKichHoat)
            {
                strKichHoat += " " + "(" + entry.Key.ToString() + ")" + " -> " + entry.Value + "  ";
            }
            //set textbox
            this.txtKichHoat.Text = strKichHoat;

        }

        public void TinhKetQuaa()
        {
            int yeuToTinh = -1;
            foreach (KeyValuePair<int, string> entry in Triangle.dinhKichHoat)
            {

                for(int i = 0;i < 17; i++)
                {
                    if ( entry.Value == Triangle.arrYeuTo[i])
                    {
                        yeuToTinh = i;
                        break;
                    }
                }
                
                triangle.TinhToanCacYeuTo(entry.Key- 1, yeuToTinh - 1);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtKichHoat_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTinh_Click(object sender, EventArgs e)
        {


            // ẩn các text box
            List<TextBox> txtLists = this.grYeuTo.Controls.OfType<TextBox>().ToList();
            foreach (var txtTemp in txtLists)
            {
                txtTemp.Visible = false;
            }


            this.isBtnTinh_Clicked = true;
            // kiểm tra nếu checkbox nào có trạng thái true thì gán lại fasle.
            List<CheckBox> chkLists = this.grYeuTo.Controls.OfType<CheckBox>().ToList();
            foreach (var chkTemp in chkLists)
            {
                // enable checkbox đã khởi tạo lúc ban đầu.
                if (chkTemp.Checked == true)
                {
                    chkTemp.Enabled = false;

                }
                else
                {
                    chkTemp.Enabled = true;
                }
            }

            // thay màu nền và tiêu đề của group box
            this.grYeuTo.Text = "Chọn yếu tố cần tính: ";
            this.grYeuTo.BackColor = System.Drawing.Color.DarkKhaki;



        }


        private void chkCanhA_Click(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }

        }

        private void chkCanhB_Click(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }

        }

        private void chkCanhC_Click(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkGocA_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkGocB_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkGocC_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkNuaChuVi_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkDienTich_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkĐttMa_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkĐttMb_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkĐtMc_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkĐcaoA_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkĐcHb_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chcĐcHc_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chkNgoaiTiep_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void chKNoiTiep_CheckedChanged(object sender, EventArgs e)
        {
            if (isBtnTinh_Clicked)
            {
                CheckBox chkTemp = sender as CheckBox;
                int temp = Status_Check(chkTemp);
                string message = triangle.CheckDinhKichHoat(Triangle.arrYeuTo[temp + 1]);
                MessageBox.Show(message, "Kết Quả");
            }
        }

        private void btnNap_Click(object sender, EventArgs e)
        {

            // ẩn các text box
            List<TextBox> txtLists = this.grYeuTo.Controls.OfType<TextBox>().ToList();
            foreach (var txtTemp in txtLists)
            {
                txtTemp.Visible = false;
            }


            //cập nhật lại triangle.Arr
            Array.Copy(triangle.ArrOri, triangle.Arr, triangle.ArrOri.Length);

            // vẽ lại bảng sau khi nhấn btnNapCT
            DrawMatrix(triangle.Arr, g, new Point(25, 25));


            this.isBtnNap_Clicked = true;
            this.isBtnTinh_Clicked = false;

            // thay tiêu đề của group box
            this.grYeuTo.Text = "Nạp yếu tố có đã biết: ";
            // recover list check box
            List<CheckBox> chkLists = this.grYeuTo.Controls.OfType<CheckBox>().ToList();
            foreach (var chkTemp in chkLists)
            {
                // enable checkbox đã khởi tạo lúc ban đầu.

                chkTemp.Checked = false;
                chkTemp.Enabled = true;
            }

        }

        // restart lai các chức năng và làm lại từ đầu.
        private void btnRestart_Click(object sender, EventArgs e)
        {
            // ẩn cách text box
            List<TextBox> txtLists = this.grYeuTo.Controls.OfType<TextBox>().ToList();
            foreach (var txtTemp in txtLists)
            {
                txtTemp.Text = "";
                txtTemp.Visible = false;
            }


            //restart checklists
            // kiểm tra nếu checkbox nào có trạng thái true thì gán lại fasle.
            List<CheckBox> chkLists = this.grYeuTo.Controls.OfType<CheckBox>().ToList();
            foreach (var chkTemp in chkLists)
            {
                chkTemp.Checked = false;
                chkTemp.Enabled = false;

            }
            // cập nhật lại mảng với giá trị ban đầu...
            Array.Copy(triangle.ArrOri, triangle.Arr, triangle.ArrOri.Length);
          

            // cập nhật lại số công thức đã cho sẵn
            Triangle.nColumn = 15;
            btnKhoiTao_Click(sender, e);
            this.grThuTuKichHoat.Text = "Thứ tự được kích hoạt";
            // xuất thông báo restart thành công
            MessageBox.Show("Restart thành công!", "Kết Quả");

           

            // refresh lại cái panel
            this.panel2.Refresh();
        }

        private void btnTinhCuThe_Click(object sender, EventArgs e)
        {
            
            //restart lai trước khi tính
            btnRestart_Click(sender, e);
          

            // hiện các text box
            List<TextBox> txtLists = this.grYeuTo.Controls.OfType<TextBox>().ToList();
            foreach (var txtTemp in txtLists)
            {
                txtTemp.Visible = true;
            }

        }

        private void txtCanhA_KeyDown(object sender, KeyEventArgs e)
        {
            
                
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkCanhA.Enabled = true;
                chkCanhA.Checked = true;
                

                    Double.TryParse(txtCanhA.Text, out triangle.arrKetQua[0]);
    


            }
        }

        private void txtCanhB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkCanhB.Enabled = true;
                chkCanhB.Checked = true;

                Double.TryParse(txtCanhB.Text, out triangle.arrKetQua[1]);

            }
        }

        private void txtCanhC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkCanhC.Enabled = true;
                chkCanhC.Checked = true;

                Double.TryParse(txtCanhC.Text, out triangle.arrKetQua[2]);
            }
        }

        private void txtGocA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkGocA.Enabled = true;
                chkGocA.Checked = true;
                double temp;
                double.TryParse(txtGocA.Text, out temp);
                triangle.arrKetQua[3] = Math.PI * temp / 180.0;
            }
        }

        
        private void txtGocB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkGocB.Enabled = true;
                chkGocB.Checked = true;
                double temp;
                double.TryParse(txtGocB.Text, out temp);
                triangle.arrKetQua[3] = Math.PI * temp / 180.0;
            }
        }

        private void txtGocC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkGocC.Enabled = true;
                chkGocC.Checked = true;
                double temp;
                Double.TryParse(txtGocC.Text, out temp);
                triangle.arrKetQua[3] = Math.PI * temp / 180.0;
            }
        }

       

        private void txtChuvi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkNuaChuVi.Enabled = true;
                chkNuaChuVi.Checked = true;

                Double.TryParse(txtChuvi.Text, out triangle.arrKetQua[6]);
            }
        }

        private void txtS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkDienTich.Enabled = true;
                chkDienTich.Checked = true;

                Double.TryParse(txtS.Text, out triangle.arrKetQua[7]);
            }
        }

        private void txtMa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkDttMa.Enabled = true;
                chkDttMa.Checked = true;

                Double.TryParse(txtMa.Text, out triangle.arrKetQua[8]);
            }
        }

        private void txtMb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkDttMb.Enabled = true;
                chkDttMb.Checked = true;

                Double.TryParse(txtMb.Text, out triangle.arrKetQua[9]);
            }
        }

        private void txtMc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkDttMc.Enabled = true;
                chkDttMc.Checked = true;

                Double.TryParse(txtMc.Text, out triangle.arrKetQua[10]);
            }
        }

        private void txtHa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkDcHa.Enabled = true;
                chkDcHa.Checked = true;

                Double.TryParse(txtHa.Text, out triangle.arrKetQua[11]);
            }
        }

        private void txtHb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkDcHb.Enabled = true;
                chkDcHb.Checked = true;

                Double.TryParse(txtHb.Text, out triangle.arrKetQua[12]);
            }
        }

        private void txtHc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkDcHc.Enabled = true;
                chkDcHc.Checked = true;

                Double.TryParse(txtHc.Text, out triangle.arrKetQua[13]);
            }
        }

        private void txtR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chkNgoaiTiep.Enabled = true;
                chkNgoaiTiep.Checked = true;

                Double.TryParse(txtR.Text, out triangle.arrKetQua[14]);
            }
        }

        private void txxtr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                isBtnNap_Clicked = true;
                chKNoiTiep.Enabled = true;
                chKNoiTiep.Checked = true;

                Double.TryParse(txxtr.Text, out triangle.arrKetQua[15]);
            }
        }

        private void btnnKetQua_Click(object sender, EventArgs e)
        {
           
            if (Triangle.dinhKichHoat.Count != 0 )
            {
                this.grThuTuKichHoat.Text = "Kết quả tính toán là: ";
                this.txtKichHoat.Text = "" ;

                try
                {
                    TinhKetQuaa();
                    for(int i = 0; i < 16; i++)
                    {
                        triangle.arrKetQua[i] = Math.Round(triangle.arrKetQua[i], 2);
                    }
                    String result = "Cạnh a =  " + triangle.arrKetQua[0] 
                        + "   Cạnh b =  " + triangle.arrKetQua[1] 
                        + "   Cạnh c =  " + triangle.arrKetQua[2] 
                        + "   Góc A =  " + triangle.arrKetQua[3] 
                        + "   Góc B =  " + triangle.arrKetQua[4] 
                        + "   Góc C =  " + triangle.arrKetQua[5]
                        + "   p =  " + triangle.arrKetQua[6] 
                        + "   S =  " + triangle.arrKetQua[7] 
                        + "   Ma =  " + triangle.arrKetQua[8] 
                        + "   Mb =  " + triangle.arrKetQua[9]
                        + "   Mc =  " + triangle.arrKetQua[10] 
                        + "   Ha =  " + triangle.arrKetQua[11]
                        + "   Hb =  " + triangle.arrKetQua[12] 
                        + "   Hc =  " + triangle.arrKetQua[13] 
                        + "   R =  " + triangle.arrKetQua[14] 
                        + "   r =  " + triangle.arrKetQua[15] ;

                    this.txtKichHoat.Text = result;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Lỗi xảy ra");
                }

            }else
            {
                MessageBox.Show("Không đủ dữ liệu để tính!", "Kết Quả");

            }

        }
    }


}
