using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using System.Data;
using System.Data.SQLite;
namespace Itcastcater.DAL
{
    public class R_OrderInfo_ProductDAL
    {
        /// <summary>
        /// 退菜
        /// </summary>
        /// <param name="ropId">中间表的主键id</param>
        /// <returns></returns>
        public int UpdateROrderProduct(int ropId)
        {
            string sql = "update R_OrderInfo_Product set DelFlag=1 where ROrderProId=@ROrderProId";
            return SqliteHelper.ExecuteNonquery(sql, new SQLiteParameter("@ROrderProId", ropId));
        }
        /// <summary>
       /// 根据订单的id查询总数量和总金额
       /// </summary>
       /// <param name="orderId">订单的id</param>
       /// <returns></returns>
       public R_OrderInfo_Product GetMoneyAndCount(int orderId)
       {
           string sql = "select count(*),sum(UnitCount*ProPrice)from R_OrderInfo_Product inner join ProductInfo on R_OrderInfo_Product.ProId=ProductInfo.ProId where OrderId=@OrderId";
           //释放    
           SQLiteDataReader sda= SqliteHelper.ExecuteReader(sql, new SQLiteParameter("@OrderId",orderId));
           R_OrderInfo_Product r = null;
             if (sda.HasRows)
             {
               while (sda.Read())
               {
                   try
                   {
                       r = new R_OrderInfo_Product();
                       r.CT = Convert.ToInt32(sda[0]);
                       r.MONEY = Convert.ToDecimal(sda[1]);
                   }
                   catch    //没有消费时会报错 ，try一下
                   {
                       r = new R_OrderInfo_Product();
                       r.CT =0 ;
                       r.MONEY = 0;
                   }
               }
             }
           return r;
       }
        /// <summary>
        /// 查询点的菜,根据订单的id
        /// </summary>
        /// <param name="orderId">订单的id</param>
        /// <returns>菜的集合</returns>
        public List<R_OrderInfo_Product> GetROrderInfoProduct(int orderId)
        {
            string sql = "select ROrderProId, ProName,ProPrice,UnitCount,ProUnit,CatName,R_OrderInfo_Product.SubTime from R_OrderInfo_Product inner join CategoryInfo on CategoryInfo.CatId=ProductInfo.CatId inner join ProductInfo on ProductInfo.ProId=R_OrderInfo_Product.ProId where R_OrderInfo_Product.DelFlag=0 and OrderId=@OrderId";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@OrderId", orderId));
            List<R_OrderInfo_Product> list = new List<R_OrderInfo_Product>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    R_OrderInfo_Product rop = RowToROrderInfoProduct(item);
                    list.Add(rop);
                }
            }
            return list;

        }
        //关系转对象
        private R_OrderInfo_Product RowToROrderInfoProduct(DataRow dr)
        {
            R_OrderInfo_Product rop = new R_OrderInfo_Product();
            rop.CatName = dr["CatName"].ToString();
            rop.ProPrice = Convert.ToDecimal(dr["ProPrice"]);
            rop.ProUnit = dr["ProUnit"].ToString();
            rop.ROrderProId = Convert.ToInt32(dr["ROrderProId"]);
            rop.SubTime = Convert.ToDateTime(dr["SubTime"]);
            rop.UnitCount = Convert.ToDecimal(dr["UnitCount"]);
            rop.ProName = dr["ProName"].ToString();
            rop.ProMoney = rop.ProPrice * rop.UnitCount;//这个菜的总价
            return rop;

        }
        /// <summary>
        /// 向中间表中插入一条数据,加菜
        /// </summary>
        /// <param name="roip">对象</param>
        /// <returns></returns>
        public int AddR_OrderInfo_Product(R_OrderInfo_Product roip)
        {
            string sql = "insert into R_OrderInfo_Product(OrderId,ProId,DelFlag,SubTime,State,UnitCount) values(@OrderId,@ProId,@DelFlag,@SubTime,@State,@UnitCount)";
            SQLiteParameter[] param = { 
                     new SQLiteParameter("@OrderId",roip.OrderId), 
                      new SQLiteParameter("@ProId",roip.ProId), 
                       new SQLiteParameter("@DelFlag",roip.DelFlag), 
                        new SQLiteParameter("@SubTime",roip.SubTime), 
                         new SQLiteParameter("@State",roip.State), 
                          new SQLiteParameter("@UnitCount",roip.UnitCount)
                                     
                                     };

            return SqliteHelper.ExecuteNonquery(sql, param);
        }
    }
}
