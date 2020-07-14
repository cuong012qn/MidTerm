using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace MidTerm
{
    class HonSo : IBase
    {
        #region Properties
        #region Private Prop
        private int _PhanNguyen;
        private int _PhanTu;
        private int _PhanMau;
        #endregion

        #region Getter-Setter
        public int PhanNguyen
        {
            get { return _PhanNguyen; }
            set { _PhanNguyen = value; }
        }

        public int PhanTu
        {
            get { return _PhanTu; }
            set { _PhanTu = value; }
        }

        public int PhanMau
        {
            get { return _PhanMau; }
            set
            {
                //Kiểm tra mẫu khác 0
                if (Convert.ToInt32(value) == 0) throw new DivideByZeroException();
                else _PhanMau = value;
            }
        }
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor tạo hỗn số
        /// </summary>
        /// <param name="x">Phần nguyên</param>
        /// <param name="y">Phần tử</param>
        /// <param name="z">Phần mẫu</param>
        public HonSo(int x, int y, int z)
        {
            PhanMau = z;
            PhanNguyen = x;
            PhanTu = y;
            ToiGian();
        }

        /// <summary>
        /// Constructor mặc định
        /// </summary>
        public HonSo()
        {
            _PhanNguyen = 0;
            _PhanTu = 0;
            _PhanMau = 1;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Nhập hỗn số
        /// </summary>
        public void Nhap()
        {
            int x = 0, y = 0, z = 0;
            do
            {
                try
                {
                    Console.WriteLine("Nhap hon so x(y/z): ");
                    Console.Write("Nhap x: ");
                    x = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Nhap y: ");
                    y = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Nhap z: ");
                    z = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (z < 0) Console.WriteLine("z = 0\nNhap lai!");
                else
                {
                    PhanNguyen = x;
                    PhanTu = y;
                    PhanMau = z;
                    ToiGian();
                    break;
                }
            } while (true);
        }
        /// <summary>
        /// Tối giản hỗn số
        /// </summary>
        public void ToiGian()
        {
            //Chuyển dạng   -x(y/z)|x(-y/z)|x(y/-z)|
            //              -x(-y/-z)|x(-y/-z)|-x(-y/z)|-x(y/-z)
            //Thành dạng chuẩn x(y/z)
            int count = 0;
            if (_PhanNguyen < 0) { count++; _PhanNguyen = Math.Abs(_PhanNguyen); }
            if (_PhanTu < 0) { count++; _PhanTu = Math.Abs(_PhanTu); }
            if (_PhanMau < 0) { count++; _PhanMau = Math.Abs(_PhanMau); }

            //Tìm ước chung lớn nhất của tử và mẫu
            int uocchung = UCLN(_PhanTu, _PhanMau);

            //Lần lượt chia phần tử và mẫu cho ước chung của mẫu và tử
            _PhanTu /= uocchung;
            _PhanMau /= uocchung;

            //Chuyển về dạng ban đầu x(y/z) => x(-y/z)
            if (count % 2 == 1) _PhanTu *= -1;
        }

        /// <summary>
        /// Tìm ước chung lớn nhất
        /// </summary>
        /// <param name="a">Số thứ nhất</param>
        /// <param name="b">Số thứ hai</param>
        /// <returns>Trả về ước chung lớn nhất của 2 số</returns>
        private int UCLN(int a, int b)
        {
            int res = 0;
            for (int i = 1; i < Math.Min(a, b); i++)
            {
                if (a % i == 0 && b % i == 0) res = i;
            }
            return res;
        }

        /// <summary>
        /// Tìm bội chung nhỏ nhất
        /// </summary>
        /// <param name="a">Số thứ nhất</param>
        /// <param name="b">Số thứ hai</param>
        /// <returns>BCNN = (a * b)/UCLN(a,b)</returns>
        private int BCNN(int a, int b) => a * b / UCLN(a, b);

        /// <summary>
        /// Trả về x(y/z) với 
        /// x: Phần nguyên
        /// y: Phần tử
        /// z: Phần mẫu
        /// </summary>
        /// <returns>x(y/z)</returns>
        public string ConvertToString()
        {
            if (_PhanNguyen == 0) return string.Format("{0}/{1}", _PhanTu, _PhanMau);

            return string.Format("{0}({1}/{2})", _PhanNguyen, _PhanTu, _PhanMau);
        }

        /// <summary>
        /// Cộng 2 hỗn số
        /// </summary>
        /// <param name="hs">Hỗn số</param>
        /// <returns>Kết quả của phép cộng 2 hỗn số</returns>
        public HonSo Cong(HonSo hs)
        {
            HonSo res;
            //Nếu mẫu hỗn số hiện tại bằng mẫu hỗn số được truyền vào
            if (this._PhanMau.Equals(hs.PhanMau))
                res = new HonSo(this._PhanNguyen + hs.PhanNguyen,
                    this._PhanTu + hs.PhanTu, hs.PhanMau);
            else
            {
                //Nếu mẫu 2 hỗn số khác nhau
                //Tìm bội chung nhỏ nhất của 2 mẫu
                int boichung = BCNN(hs.PhanMau, this._PhanMau);

                //Phần nguyên (kết quả) = this._PhanNguyen + hs.PhanNguyen
                //Phần tử (kết quả) = (_PhanTu * boichung / _PhanMau) + (hs.PhanTu * boichung/hs.PhanMau)
                //Phần mẫu (kết quả) = Bội chung 2 mẫu
                res = new HonSo(this._PhanNguyen + hs.PhanNguyen,
                    (_PhanTu * boichung / _PhanMau) + (hs.PhanTu * boichung / hs.PhanMau), boichung);
            }

            return res;
        }

        /// <summary>
        /// Trừ 2 hỗn số
        /// </summary>
        /// <param name="hs">Hỗn số</param>
        /// <returns>Kết quả của phép trừ 2 hỗn số</returns>
        public HonSo Tru(HonSo hs)
        {
            HonSo res;
            //Nếu mẫu hỗn số hiện tại bằng mẫu hỗn số được truyền vào
            if (this._PhanMau.Equals(hs.PhanMau))
                res = new HonSo(this._PhanNguyen - hs.PhanNguyen,
                    this._PhanTu + hs.PhanTu, hs.PhanMau);
            else
            {
                //Nếu mẫu 2 hỗn số khác nhau
                //Tìm bội chung nhỏ nhất của 2 mẫu
                int boichung = BCNN(hs.PhanMau, this._PhanMau);
                //Phần nguyên (kết quả) = this._PhanNguyen - hs.PhanNguyen
                //Phần tử (kết quả) = (_PhanTu * boichung / _PhanMau) - (hs.PhanTu * boichung/hs.PhanMau)
                //Phần mẫu (kết quả) = Bội chung 2 mẫu
                res = new HonSo(this._PhanNguyen - hs.PhanNguyen,
                    (_PhanTu * boichung / _PhanMau) - (hs.PhanTu * boichung / hs.PhanMau), boichung);
            }

            return res;
        }

        /// <summary>
        /// Giá trị số thực
        /// </summary>
        /// <returns>Trả về giá trị số thực của hỗn số</returns>
        public double GiaTri()
        {
            return ((Convert.ToDouble(_PhanNguyen) * Convert.ToDouble(_PhanMau)) + Convert.ToDouble(_PhanTu)) / Convert.ToDouble(_PhanMau);
        }

        /// <summary>
        /// Nghịch đảo hỗn số
        /// x(y/z) => x(z/y) 
        /// </summary>
        /// <returns>Trả về dạng nghịch đảo</returns>
        public HonSo NghichDao()
        {
            HonSo honso = new HonSo(0, _PhanMau, (_PhanNguyen * _PhanMau) + _PhanTu);
            return honso;
        }

        #endregion
    }
}
