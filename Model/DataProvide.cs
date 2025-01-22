using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPMUDMVVM.Model
{
    public class DataProvide
    {
        private static DataProvide _ins;
        public static DataProvide Ins { get { if (_ins == null) _ins = new DataProvide(); return _ins; }
            set { _ins = value; } }
        public KyThuatPhanMemUngDungEntities2 DB { get; set; }
         
        private DataProvide()
        {
            DB = new KyThuatPhanMemUngDungEntities2();
        }
        public class DataHelper
        {
            public static string GetTenDichBenh(string maDB)
            {
                if (string.IsNullOrEmpty(maDB))
                {
                    return "Không rõ";
                }

                // Tìm kiếm trong cơ sở dữ liệu
                var dichBenh = DataProvide.Ins.DB.Dichbenhs
                                .FirstOrDefault(db => db.MaDB == maDB);

                return dichBenh != null ? dichBenh.TenDB : "Không rõ";
            }
        }
    }
}
