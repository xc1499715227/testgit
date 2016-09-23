using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Itcastcater.DAL;
using ItcastCater.Model;
namespace Itcastcater.BLL
{
    public class R_OrderInfo_ProductBLL
    {
        R_OrderInfo_ProductDAL dal = new R_OrderInfo_ProductDAL();
        /// <summary>
        /// 退菜
        /// </summary>
        /// <param name="ropId">中间表的主键id</param>
        /// <returns></returns>
        public bool UpdateROrderProduct(int ropId)
        {
            return dal.UpdateROrderProduct(ropId) > 0 ? true : false;
        }
        /// <summary>
        /// 根据订单的id查询总数量和总金额
        /// </summary>
        /// <param name="orderId">订单的id</param>
        /// <returns></returns>
        public R_OrderInfo_Product GetMoneyAndCount(int orderId)
        {
            return dal.GetMoneyAndCount(orderId);
        }
        /// <summary>
        /// 查询点的菜,根据订单的id
        /// </summary>
        /// <param name="orderId">订单的id</param>
        /// <returns>菜的集合</returns>
        public List<R_OrderInfo_Product> GetROrderInfoProduct(int orderId)
        {
            return dal.GetROrderInfoProduct(orderId);
        }
        /// <summary>
        /// 向中间表中插入一条数据,加菜
        /// </summary>
        /// <param name="roip">对象</param>
        /// <returns></returns>
        public bool AddR_OrderInfo_Product(R_OrderInfo_Product roip)
        {
            return dal.AddR_OrderInfo_Product(roip) > 0 ? true : false;
        }
    }
}
