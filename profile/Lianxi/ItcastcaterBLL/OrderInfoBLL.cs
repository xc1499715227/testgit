using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using Itcastcater.DAL;
namespace Itcastcater.BLL
{
    public class OrderInfoBLL
    {
        OrderInfoDAL dal = new OrderInfoDAL();
        /// <summary>
        /// 根据餐桌的id查询正在使用的订单id
        /// </summary>
        /// <param name="deskId">餐桌的id</param>
        /// <returns>订单的id</returns>
        public int GetOrderIdByDeskId(int deskId)
        {
            return Convert.ToInt32(dal.GetOrderIdByDeskId(deskId));
        }
        /// <summary>
        /// 插入一个订单返回该订单的id
        /// </summary>
        /// <param name="order">对象</param>
        /// <returns></returns>
        public int AddOrderInfo(OrderInfo order)
        {
            return dal.AddOrderInfo(order);
        }
    }
}
