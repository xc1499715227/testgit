using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using ItcastCater.Model;
namespace Itcastcater.DAL
{
    public class ROrderDeskDAL
    {
        /// <summary>
        /// 向中间表插入一条数据
        /// </summary>
        /// <param name="rod">中间表对象</param>
        /// <returns></returns>
        public int AddROrderDesk(ROrderDesk rod)
        {
            string sql = "insert into R_Order_Desk(OrderId,DeskId) values(@OrderId,@DeskId)";
            SQLiteParameter[] param = { 
                        new SQLiteParameter("@OrderId",rod.OrderId),
                         new SQLiteParameter("@DeskId",rod.DeskId)
                                     };
            return SqliteHelper.ExecuteNonquery(sql, param);
        }
    }
}
