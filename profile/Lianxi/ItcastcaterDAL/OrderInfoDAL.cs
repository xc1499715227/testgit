using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using System.Data;
using System.Data.SQLite;
namespace Itcastcater.DAL
{
    public class OrderInfoDAL
    {
        /// <summary>
        /// 根据餐桌的id查询正在使用的订单id
        /// </summary>
        /// <param name="deskId">餐桌的id</param>
        /// <returns>订单的id</returns>
        public object GetOrderIdByDeskId(int deskId)
        {
            string sql = "select OrderInfo.OrderId from R_Order_Desk inner join OrderInfo on R_Order_Desk.OrderId=OrderInfo.OrderId where OrderState=1 and DeskId=@DeskId";
            return SqliteHelper.ExecuteScalar(sql, new SQLiteParameter("@DeskId", deskId));
        }

        /// <summary>
        /// 插入一个订单返回该订单的id
        /// </summary>
        /// <param name="order">对象</param>
        /// <returns></returns>
        public int AddOrderInfo(OrderInfo order)
        {
            string sql = "insert into OrderInfo(SubTime,Remark,OrderState,DelFlag,SubBy) values(@SubTime,@Remark,@OrderState,@DelFlag,@SubBy);select last_insert_rowid();";
            SQLiteParameter[] param = { 
                      new SQLiteParameter("@SubTime",order.SubTime),
                      new SQLiteParameter("@Remark",order.Remark),
                      new SQLiteParameter("@OrderState",order.OrderState),
                      new SQLiteParameter("@DelFlag",order.DelFlag),
                      new SQLiteParameter("@SubBy",order.SubBy) 
                                    };
            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, param));
        }
        
    }
}
