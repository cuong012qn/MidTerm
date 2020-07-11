using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTerm
{
    interface IBase
    {
        void ToiGian();
        string ConvertToString();
        double GiaTri();

        HonSo Cong(HonSo hs);
        HonSo Tru(HonSo hs);
        HonSo NghichDao();
    }
}
