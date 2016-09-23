using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using System.Data;

namespace Itcastcater.DAL
{
    public class MemmberTypeDAL
    {
        public List<MemmberType> GetAllMemmberTypeByDelFlag()
        {
            string sql = "select MemType,MemTpName from MemmberType where DelFlag=0";

            DataTable dt = SqliteHelper.ExecuteTable(sql);
            List<MemmberType> list = new List<MemmberType>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemmberType memtp = RowToMemmberType(item);
                    list.Add(memtp);
                }
            }
            return list;
        }
        //关系转对象
        private MemmberType RowToMemmberType(DataRow item)
        {
            MemmberType mem = new MemmberType();
            mem.MemTpName = item["MemTpName"].ToString();
            mem.MemType = Convert.ToInt32(item["MemType"]);
            return mem;
        }
    }
}
