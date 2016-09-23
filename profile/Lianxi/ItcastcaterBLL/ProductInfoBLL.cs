using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using Itcastcater.DAL;
namespace Itcastcater.BLL
{
    public class ProductInfoBLL
    {
        ProductInfoDAL dal = new ProductInfoDAL();
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="txt">编号或者是拼音</param>
        /// <param name="temp">标识1--编号,2---拼音,3---啥也没有</param>
        /// <returns></returns>
        public List<ProductInfo> GetProductBySpellOrNum(string txt, int temp)
        {
            return dal.GetProductBySpellOrNum(txt, temp);
        }
        //模糊查询
        public List<ProductInfo> GetProductByProNum(string proNum)
        {
            return dal.GetProductByProNum(proNum);
        }
        /// <summary>
        /// 根据商品类别的id查询商品
        /// </summary>
        /// <param name="catId">商品类别id</param>
        /// <returns></returns>
        public List<ProductInfo> GetProductInfoByCatId(int catId)
        {
            return dal.GetProductInfoByCatId(catId);
        }
        /// <summary>
        /// 新增和修改
        /// </summary>
        /// <param name="pro">对象--修改</param>
        /// <param name="temp">标识 3--新增，4</param>
        /// <returns></returns>
        public bool SaveProduct(ProductInfo pro, int temp)
        {
            int r = -1;
            if (temp == 3)
            {
                r = dal.AddProduct(pro);
            }
            else if (temp == 4)
            {
                r = dal.UpdateProduct(pro);
            }
            return r > 0 ? true : false;
        }
        /// <summary>
        /// 根据产品的id查询该产品的信息
        /// </summary>
        /// <param name="proId">产品id</param>
        /// <returns>对象</returns>
        public ProductInfo GetProductInfoByProId(int proId)
        {
            return dal.GetProductInfoByProId(proId);
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteProductById(int id)
        {
            return dal.DeleteProductById(id) > 0 ? true : false;
        }
        /// <summary>
        /// 查询所有没被删除的产品
        /// </summary>
        /// <param name="delFlag">删除标识</param>
        /// <returns></returns>
        public List<ProductInfo> GetAllProductInfoByDelFlag(int delFlag)
        {
            return dal.GetAllProductInfoByDelFlag(delFlag);
        }
    }
}
