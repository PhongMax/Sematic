using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Semantic_Triangle
{
    class Triangle
    {
        // mặc định là 15
        public static int nColumn = 15;
        //mảng lưu tổng  số các yếu tố của các công thức
        public static int[] arrDem;
        private int[,] arr;

        // lưu giá trị đỉnh, và công thức tương ứng đã kích hoạt... 
        public static Dictionary<int, string> dinhKichHoat = new Dictionary<int, string>();
        public int[,] Arr
        {
            get { return this.arr; }
            set { this.Arr = value; }
        }
        //mảng int lưu giá trị mảng hai chiều ban đầu của tam giác
        private int[,] arrOri;

        // mảng double lưu giá trị thực của các yếu tố
        public double[] arrKetQua = new double[16];
        public int[,] ArrOri
        {
            get { return this.arrOri; }
        }

        public static string[] arrYeuTo = new string[17]
            {"   ", "a", "b" , "c", "A", "B", "C","p", "S", "Ma", "Mb", "Mc","Ha", "Hb","Hc", "R", "r"};



        // kiểm tra dictionary
        public string CheckDinhKichHoat(string strCheck)
        {
            string result = "";
            // duyệt dictionary
            foreach (KeyValuePair<int, string> entry in Triangle.dinhKichHoat)
            {
                result += " " + "(" + entry.Key.ToString() + ")" + " -> " + entry.Value + "  ";
                if (entry.Value == strCheck)
                {
                    return result;
                }
            }
            return "Dữ liệu chưa đủ, bạn hãy thêm tri thức vào !!!";
        }

        public Triangle()
        {
            // mặc định 15 công thức được thêm vào sẵn.
            arrDem = new int[nColumn];
            // Khai báo biến mảng 2 chiều arrTamGiac, có 16 hàng và 200 cột
            // tương ứng 16 yếu tố, và Max 200 công thức
            arr = new int[16, 200];
            arrOri = new int[16, 200];
            // gán giá tri = 0 cho bộ mảng có giá trị 
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    arr[i, j] = 0;
                }
            }

            // A[i,j] = -1 nghĩa là công thức j có yếu tố i hay cột j chứa các yếu tố là hàng i
            // cột j = 0
            arr[3, 0] = -1;
            arr[4, 0] = -1;
            arr[5, 0] = -1;
            // cột j = 1 
            arr[0, 1] = -1;
            arr[1, 1] = -1;
            arr[2, 1] = -1;
            arr[6, 1] = -1;
            // cột j = 2
            arr[0, 2] = -1;
            arr[1, 2] = -1;
            arr[2, 2] = -1;
            arr[5, 2] = -1;

            // cột j = 3   
            arr[0, 3] = -1;
            arr[1, 3] = -1;
            arr[5, 3] = -1;
            arr[7, 3] = -1;

            // cột j = 4
            arr[6, 4] = -1;
            arr[7, 4] = -1;
            arr[15, 4] = -1;

            // cột j = 5
            arr[0, 5] = -1;
            arr[2, 5] = -1;
            arr[3, 5] = -1;
            arr[5, 5] = -1;

            // cột j = 6
            arr[0, 6] = -1;
            arr[1, 6] = -1;
            arr[3, 6] = -1;
            arr[4, 6] = -1;
            // cột j = 7   
            arr[0, 7] = -1;
            arr[1, 7] = -1;
            arr[2, 7] = -1;
            arr[6, 7] = -1;
            arr[7, 7] = -1;

            // cột j = 8
            arr[0, 8] = -1;
            arr[1, 8] = -1;
            arr[2, 8] = -1;
            arr[8, 8] = -1;
            // cột j = 9   
            arr[0, 9] = -1;
            arr[1, 9] = -1;
            arr[2, 9] = -1;
            arr[9, 9] = -1;
            // cột j = 10
            arr[0, 10] = -1;
            arr[1, 10] = -1;
            arr[2, 10] = -1;
            arr[10, 10] = -1;

            // cột j = 11  
            arr[0, 11] = -1;
            arr[11, 11] = -1;
            arr[7, 11] = -1;
            // cột j = 12
            arr[1, 12] = -1;
            arr[12, 12] = -1;
            arr[7, 12] = -1;

            // cột j = 13  
            arr[2, 13] = -1;
            arr[13, 13] = -1;
            arr[7, 13] = -1;

            // cột j = 14
            arr[0, 14] = -1;
            arr[1, 14] = -1;
            arr[2, 14] = -1;
            arr[14, 14] = -1;
            arr[7, 14] = -1;

            // đếm tổng các yếu tố của các công thức lưu vào arrDem
            Triangle.arrDem = DemYeuTo(-1);

            Array.Copy(this.arr, this.arrOri, this.arr.Length);

        }

        // xử lý quá trình thêm công thức vào dữ liệu
        public void ThemCongThuc(int[] arrThem)
        {
            for (int i = 0; i < 16; i++)
            {
                this.arr[i, nColumn] = arrThem[i];
            }
            nColumn++;

            //cập nhật lại arrOri sau mỗi lần thêm
            Array.Copy(arr, arrOri, arr.Length);
        }

        //Gán giá trị 1 vào các yếu tố được kích hoạt
        public void KhoiTaoMangTG(int sttItem)
        {
            for (int i = 0; i < nColumn; i++)
            {
                if (this.arr[sttItem, i] == -1)
                {
                    this.arr[sttItem, i] = 1;
                }

            }
        }

        private int TimKichHoat()
        {
            // n là số ô có giá trị = -1 trong 1 cột của ma trận

            int n = 0, dong = -1;

            for (int i = 0; i < nColumn; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (this.Arr[j, i] == -1)
                    {
                        n++;
                        dong = j;
                    }
                }
                // nếu n = 1 thì đã tìm ra yếu tố và công thức cần kích hoạt và trả về vị trí j
                if (n == 1)
                {
                    return dong;
                }
                // cập nhật lại biến n.
                n = 0;

            }

            // nếu không được kích hoạt thì trả về 0
            return -1;
        }

        // trả về mảng chứa số phần từ =1 hoặc =-1 trong 1 cột của ma trận.
        public int[] DemYeuTo(int value)
        {
            int[] arrDem = new int[nColumn];

            for (int i = 0; i < nColumn; i++)
            {
                int dem = 0;
                for (int j = 0; j < 16; j++)
                {
                    if (this.arr[j, i] == value)
                    {
                        dem++;
                    }

                }

                arrDem[i] = dem;
            }
            return arrDem;
        }

        // kiểm tra xem công thức nào đã được kích hoạt...
        private void KiemTraKichHoat(int dong)
        {
            int[] arrTest = new int[nColumn];
            arrTest = DemYeuTo(1);

            // so sánh
            for (int cot = 0; cot < arrTest.Length; cot++)
            {
                if (arrTest[cot] == Triangle.arrDem[cot] & !Triangle.dinhKichHoat.ContainsKey(cot + 1))
                {

                    Triangle.dinhKichHoat.Add(cot + 1, Triangle.arrYeuTo[dong + 1]);
                }
            }

        }


        // lan truyền kích hoạt các yếu tố và công thức trong ma trận
        public void LanTruyenKichHoat()
        {
            int dong;
            for (int cot = 0; cot < nColumn; cot++)
            {
                dong = TimKichHoat();
                if (dong >= 0)
                {
                    KhoiTaoMangTG(dong);
                    KiemTraKichHoat(dong);

                }

            }

        }





        private double giaiPhuongTrinh(double a, double b, double c)
        {
            double d = b * b - 4 * a * c;
            double x1, x2;
            if (d == 0)
            {
                x1 = -b / (2.0 * a);
                x2 = x1;
                return x1;
            }
            else if (d > 0)
            {

                x1 = (-b + Math.Sqrt(d)) / (2 * a);
                x2 = (-b - Math.Sqrt(d)) / (2 * a);
                if (x1 > 0)
                {
                    return x1;
                }
                else if (x2 > 0)
                {
                    return x2;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        //   try catch ở ngoài hàm gọi cái hàm này....
        public void TinhToanCacYeuTo(int congThuc, int yeuToTinh)
        {

            switch (congThuc)
            {
                // xử lý công thức thứ (1)
                case 0:
                    switch (yeuToTinh)
                    {
                        case 3:
                            this.arrKetQua[3] = Math.PI - this.arrKetQua[4] - this.arrKetQua[5];

                            break;

                        case 4:
                            this.arrKetQua[4] = Math.PI - this.arrKetQua[3] - this.arrKetQua[5];
                            break;

                        case 5:
                            this.arrKetQua[5] = Math.PI - this.arrKetQua[3] - this.arrKetQua[4];
                            break;
                    }
                    break;
                //xử lý công thức thứ (2)
                case 1:
                    switch (yeuToTinh)
                    {
                        case 0:
                            this.arrKetQua[0] = 2 * this.arrKetQua[6] - this.arrKetQua[1] - this.arrKetQua[2];
                            break;

                        case 1:
                            this.arrKetQua[1] = 2 * this.arrKetQua[6] - this.arrKetQua[0] - this.arrKetQua[2];
                            break;

                        case 2:
                            this.arrKetQua[2] = 2 * this.arrKetQua[6] - this.arrKetQua[0] - this.arrKetQua[1];
                            break;

                        case 6:
                            this.arrKetQua[6] = (this.arrKetQua[0] + this.arrKetQua[1] + this.arrKetQua[2]) / 2.0;
                            break;
                    }
                    break;
                //xử lý công thức thứ (3)
                case 2:

                    switch (yeuToTinh)
                    {
                        case 0:
                            double a = 1.0;
                            double b = -2 * this.arrKetQua[1] * Math.Cos(this.arrKetQua[5]);
                            double c = Math.Pow(this.arrKetQua[1], 2) - Math.Pow(this.arrKetQua[2], 2);
                            this.arrKetQua[0] = giaiPhuongTrinh(a, b, c);
                            break;

                        case 1:
                            double a1 = 1.0;
                            double b1 = -2 * this.arrKetQua[0] * Math.Cos(this.arrKetQua[5]);
                            double c1 = Math.Pow(this.arrKetQua[0], 2) - Math.Pow(this.arrKetQua[2], 2);
                            this.arrKetQua[1] = giaiPhuongTrinh(a1, b1, c1);
                            break;

                        case 2:
                            this.arrKetQua[2] = Math.Sqrt(Math.Pow(this.arrKetQua[0], 2) + Math.Pow(this.arrKetQua[1], 2)
                                - 2 * this.arrKetQua[0] * this.arrKetQua[1] * Math.Cos(this.arrKetQua[5]));
                            break;

                        case 5:
                            double temp = (Math.Pow(this.arrKetQua[0], 2) + Math.Pow(this.arrKetQua[1], 2) - Math.Pow(this.arrKetQua[2], 2)) /
                            (2 * this.arrKetQua[0] * this.arrKetQua[1]);
                            this.arrKetQua[5] = Math.Acos(temp);
                            break;

                    }
                    break;
                // xử lý công thức thứ (4)
                case 3:
                    switch (yeuToTinh)
                    {
                        case 0:
                            this.arrKetQua[0] = (2 * this.arrKetQua[7]) / (this.arrKetQua[1] * Math.Sin(this.arrKetQua[5]));
                            break;

                        case 1:
                            this.arrKetQua[1] = (2 * this.arrKetQua[7]) / (this.arrKetQua[0] * Math.Sin(this.arrKetQua[5]));
                            break;

                        case 5:
                            double temp = (2 * this.arrKetQua[7]) / (this.arrKetQua[0] * this.arrKetQua[1]);
                            this.arrKetQua[5] = Math.Asin(temp);
                            break;
                        case 7:
                            this.arrKetQua[7] = 0.5 * this.arrKetQua[0] * this.arrKetQua[1] * Math.Sin(this.arrKetQua[5]);
                            break;
                    }
                    break;
                //xử lý công thức thứ (5)
                case 4:
                    switch (yeuToTinh)
                    {
                        case 6:
                            this.arrKetQua[6] = this.arrKetQua[7] / this.arrKetQua[15];
                            break;

                        case 7:
                            this.arrKetQua[7] = this.arrKetQua[6] * this.arrKetQua[15];
                            break;

                        case 15:
                            this.arrKetQua[15] = this.arrKetQua[7] / this.arrKetQua[6];
                            break;
                    }
                    break;
                //xử lý công thức thứ (6)
                case 5:
                    switch (yeuToTinh)
                    {
                        case 0:
                            this.arrKetQua[0] = (this.arrKetQua[2] * Math.Sin(this.arrKetQua[3])) / Math.Sin(this.arrKetQua[5]);
                            break;

                        case 2:
                            this.arrKetQua[2] = (this.arrKetQua[0] * Math.Sin(this.arrKetQua[5])) / Math.Sin(this.arrKetQua[3]);

                            break;

                        case 3:
                            double temp = (this.arrKetQua[0] * Math.Sin(this.arrKetQua[5])) / this.arrKetQua[2];
                            this.arrKetQua[3] = Math.Asin(temp);

                            break;
                        case 5:
                            double temp1 = (this.arrKetQua[2] * Math.Sin(this.arrKetQua[3])) / this.arrKetQua[0];
                            this.arrKetQua[5] = Math.Asin(temp1);
                            break;
                    }
                    break;
                //xử lý công thức thứ (7)
                case 6:
                    switch (yeuToTinh)
                    {
                        case 0:
                            this.arrKetQua[0] = (this.arrKetQua[1] * Math.Sin(this.arrKetQua[3])) / Math.Sin(this.arrKetQua[4]);
                            break;

                        case 1:
                            this.arrKetQua[1] = (this.arrKetQua[0] * Math.Sin(this.arrKetQua[4])) / Math.Sin(this.arrKetQua[3]);
                            break;

                        case 3:
                            double temp = (this.arrKetQua[0] * Math.Sin(this.arrKetQua[4])) / this.arrKetQua[1];
                            this.arrKetQua[3] = Math.Asin(temp);
                            break;
                        case 4:
                            double temp1 = (this.arrKetQua[1] * Math.Sin(this.arrKetQua[3])) / this.arrKetQua[0];
                            this.arrKetQua[4] = Math.Asin(temp1);

                            break;
                    }
                    break;
                //xử lý công thức thứ (8)
                case 7:
                    switch (yeuToTinh)
                    {
                        case 0:
                            double a = Math.Pow(this.arrKetQua[7], 2) / ((this.arrKetQua[6] - this.arrKetQua[1]) * (this.arrKetQua[6] - this.arrKetQua[2]));

                            this.arrKetQua[0] = (Math.Pow(this.arrKetQua[6], 2) - a) / this.arrKetQua[6];
                            break;

                        case 1:
                            double b = Math.Pow(this.arrKetQua[7], 2) / ((this.arrKetQua[6] - this.arrKetQua[0]) * (this.arrKetQua[6] - this.arrKetQua[2]));
                            this.arrKetQua[1] = (Math.Pow(this.arrKetQua[6], 2) - b) / this.arrKetQua[6];
                            break;

                        case 2:
                            double c = Math.Pow(this.arrKetQua[7], 2) / ((this.arrKetQua[6] - this.arrKetQua[0]) * (this.arrKetQua[6] - this.arrKetQua[1]));
                            this.arrKetQua[2] = (Math.Pow(this.arrKetQua[6], 2) - c) / this.arrKetQua[6];
                            break;
                        case 6:
                            this.arrKetQua[6] = (this.arrKetQua[0] + this.arrKetQua[1] + this.arrKetQua[2]) / 2.0;
                            break;
                        case 7:
                            this.arrKetQua[7] = Math.Sqrt(this.arrKetQua[6] * (this.arrKetQua[6] - this.arrKetQua[0]) * (this.arrKetQua[6] - this.arrKetQua[1])
                                * (this.arrKetQua[6] - this.arrKetQua[2]));
                            break;
                    }
                    break;
                //xử lý công thức thứ (9)
                case 8:
                    switch (yeuToTinh)
                    {
                        case 0:

                            this.arrKetQua[0] = Math.Sqrt(2 * Math.Pow(this.arrKetQua[2], 2) + 2 * Math.Pow(this.arrKetQua[1], 2) - 4 * Math.Pow(this.arrKetQua[8], 2));
                            break;

                        case 1:
                            this.arrKetQua[1] = Math.Sqrt((4 * Math.Pow(this.arrKetQua[8], 2) - 2 * Math.Pow(this.arrKetQua[2], 2) + Math.Pow(this.arrKetQua[0], 2)) / 2);
                            break;

                        case 2:
                            this.arrKetQua[2] = Math.Sqrt((4 * Math.Pow(this.arrKetQua[8], 2) - 2 * Math.Pow(this.arrKetQua[1], 2) + Math.Pow(this.arrKetQua[0], 2)) / 2);
                            break;

                        case 8:
                            this.arrKetQua[8] = Math.Sqrt((2 * (Math.Pow(this.arrKetQua[2], 2) + Math.Pow(this.arrKetQua[1], 2)) - Math.Pow(this.arrKetQua[0], 2)) / 4);
                            break;
                    }
                    break;
                //xử lý công thức thứ (10)
                case 9:
                    switch (yeuToTinh)
                    {
                        case 0:

                            this.arrKetQua[0] = Math.Sqrt((4 * Math.Pow(this.arrKetQua[9], 2) - 2 * Math.Pow(this.arrKetQua[2], 2) + Math.Pow(this.arrKetQua[1], 2)) / 2);
                            break;

                        case 1:
                            this.arrKetQua[1] = Math.Sqrt(2 * Math.Pow(this.arrKetQua[2], 2) + 2 * Math.Pow(this.arrKetQua[0], 2) - 4 * Math.Pow(this.arrKetQua[9], 2));
                            break;

                        case 2:
                            this.arrKetQua[2] = Math.Sqrt((4 * Math.Pow(this.arrKetQua[9], 2) - 2 * Math.Pow(this.arrKetQua[0], 2) + Math.Pow(this.arrKetQua[1], 2)) / 2);
                            break;

                        case 9:
                            this.arrKetQua[9] = Math.Sqrt((2 * (Math.Pow(this.arrKetQua[2], 2) + Math.Pow(this.arrKetQua[0], 2)) - Math.Pow(this.arrKetQua[1], 2)) / 4);
                            break;
                    }
                    break;
                //xử lý công thức thứ (11)
                case 10:
                    switch (yeuToTinh)
                    {
                        case 0:

                            this.arrKetQua[0] = Math.Sqrt((4 * Math.Pow(this.arrKetQua[10], 2) - 2 * Math.Pow(this.arrKetQua[1], 2) + Math.Pow(this.arrKetQua[2], 2)) / 2);
                            break;

                        case 1:
                            this.arrKetQua[1] = Math.Sqrt((4 * Math.Pow(this.arrKetQua[10], 2) - 2 * Math.Pow(this.arrKetQua[0], 2) + Math.Pow(this.arrKetQua[2], 2)) / 2);
                            break;

                        case 2:
                            this.arrKetQua[2] = Math.Sqrt(2 * Math.Pow(this.arrKetQua[0], 2) + 2 * Math.Pow(this.arrKetQua[1], 2) - 4 * Math.Pow(this.arrKetQua[10], 2));
                            break;
                        case 10:
                            this.arrKetQua[10] = Math.Sqrt((2 * (Math.Pow(this.arrKetQua[1], 2) + Math.Pow(this.arrKetQua[0], 2)) - Math.Pow(this.arrKetQua[2], 2)) / 4);
                            break;
                    }
                    break;
                //xử lý công thức thứ (12)
                case 11:
                    switch (yeuToTinh)
                    {
                        case 0:
                            this.arrKetQua[0] = (2 * this.arrKetQua[7]) / this.arrKetQua[11];
                            break;

                        case 7:
                            this.arrKetQua[7] = 0.5 * this.arrKetQua[0] * this.arrKetQua[11];
                            break;

                        case 11:
                            this.arrKetQua[11] = (2 * this.arrKetQua[7]) / this.arrKetQua[0];
                            break;
                    }
                    break;
                //xử lý công thức thứ (13)
                case 12:
                    switch (yeuToTinh)
                    {
                        case 1:
                            this.arrKetQua[1] = (2 * this.arrKetQua[7]) / this.arrKetQua[12];
                            break;

                        case 7:
                            this.arrKetQua[7] = 0.5 * this.arrKetQua[1] * this.arrKetQua[12];
                            break;

                        case 12:
                            this.arrKetQua[12] = (2 * this.arrKetQua[7]) / this.arrKetQua[1];
                            break;
                    }

                    break;
                //xử lý công thức thứ (4)
                case 13:
                    switch (yeuToTinh)
                    {
                        case 2:
                            this.arrKetQua[2] = (2 * this.arrKetQua[7]) / this.arrKetQua[13];
                            break;

                        case 7:
                            this.arrKetQua[7] = 0.5 * this.arrKetQua[2] * this.arrKetQua[13];
                            break;

                        case 13:
                            this.arrKetQua[13] = (2 * this.arrKetQua[7]) / this.arrKetQua[2];
                            break;
                    }
                    break;
                //xử lý công thức thứ (15)
                case 14:
                    switch (yeuToTinh)
                    {
                        case 0:
                            this.arrKetQua[0] = (4 * this.arrKetQua[7] * this.arrKetQua[14]) / (this.arrKetQua[1] * this.arrKetQua[2]);
                            break;

                        case 1:
                            this.arrKetQua[1] = (4 * this.arrKetQua[7] * this.arrKetQua[14]) / (this.arrKetQua[0] * this.arrKetQua[2]);
                            break;

                        case 2:
                            this.arrKetQua[2] = (4 * this.arrKetQua[7] * this.arrKetQua[14]) / (this.arrKetQua[1] * this.arrKetQua[0]);
                            break;
                        case 7:
                            this.arrKetQua[7] = (this.arrKetQua[0] * this.arrKetQua[1] * this.arrKetQua[2]) / (4 * this.arrKetQua[14]);
                            break;
                        case 14:
                            this.arrKetQua[14] = (this.arrKetQua[0] * this.arrKetQua[1] * this.arrKetQua[2]) / (4 * this.arrKetQua[7]);
                            break;
                    }
                    break;
            }
        }
    }
}
