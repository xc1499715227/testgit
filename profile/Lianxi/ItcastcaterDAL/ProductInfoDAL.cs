using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using ItcastCater.Model;
namespace Itcastcater.DAL
{
    public class ProductInfoDAL
    {
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="txt">编号或者是拼音</param>
        /// <param name="temp">标识1--编号,2---拼音,3---啥也没有</param>
        /// <returns></returns>
        public List<ProductInfo> GetProductBySpellOrNum(string txt, int temp)
        {
            string sql = "select * from ProductInfo where DelFlag=0";
            List<ProductInfo> list = new List<ProductInfo>();
            List<SQLiteParameter> listParam = new List<SQLiteParameter>();
            if (temp == 1)
            {

                sql += " and ProNum like @ProNum";
                listParam.Add(new SQLiteParameter("@ProNum", "%" + txt + "%"));
            }
            else if (temp == 2)
            {
                sql += " and ProSpell like @ProSpell";
                listParam.Add(new SQLiteParameter("@ProSpell", "%" + txt + "%"));
            }
            DataTable dt = SqliteHelper.ExecuteTable(sql, listParam.ToArray());
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ProductInfo pro = RowToProductInfo(item);
                    list.Add(pro);
                }
            }
            return list;
        }
        //模糊查询
        public List<ProductInfo> GetProductByProNum(string proNum)
        {
            string sql = "select * from ProductInfo where DelFlag=0 and ProNum like @ProNum";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@ProNum", "%" + proNum + "%"));
            List<ProductInfo> list = new List<ProductInfo>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ProductInfo pro = RowToProductInfo(item);
                    list.Add(pro);
                }
            }
            return list;
        }
        /// <summary>
        /// 根据商品类别的id查询商品
        /// </summary>
        /// <param name="catId">商品类别id</param>
        /// <returns></returns>
        public List<ProductInfo> GetProductInfoByCatId(int catId)
        {
            string sql = "select * from ProductInfo where DelFlag=0 and CatId=@CatId";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@CatId", catId));

            List<ProductInfo> list = new List<ProductInfo>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ProductInfo pro = RowToProductInfo(item);
                    list.Add(pro);
                }
            }
            return list;
        }
        //新增
        public int AddProduct(ProductInfo pro)
        {
            string sql = "insert into ProductInfo(CatId,ProName,ProCost,ProSpell,ProPrice,ProUnit,Remark,DelFlag,SubTime,ProStock,ProNum,SubBy) values(@CatId,@ProName,@ProCost,@ProSpell,@ProPrice,@ProUnit,@Remark,@DelFlag,@SubTime,@ProStock,@ProNum,@SubBy)";
            return AddAndUpdate(pro, sql, 3);
        }
        //修改
        public int UpdateProduct(ProductInfo pro)
        {
            string sql = "update ProductInfo set CatId=@CatId,ProName=@ProName,ProCost=@ProCost,ProSpell=@ProSpell,ProPrice=@ProPrice,ProUnit=@ProUnit,Remark=@Remark,ProStock=@ProStock,ProNum=@ProNum where ProId=@ProId";
            return AddAndUpdate(pro, sql, 4);
        }
        public int AddAndUpdate(ProductInfo pro, string sql, int temp)
        {
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            SQLiteParameter[] param = { 
            new SQLiteParameter("@CatId",pro.CatId),
            new SQLiteParameter("@ProName",pro.ProName),
            new SQLiteParameter("@ProCost",pro.ProCost),
            new SQLiteParameter("@ProSpell",pro.ProSpell),
            new SQLiteParameter("@ProPrice",pro.ProPrice),
            new SQLiteParameter("@ProUnit",pro.ProUnit),
            new SQLiteParameter("@Remark",pro.Remark),
            new SQLiteParameter("@ProStock",pro.ProStock),
            new SQLiteParameter("@ProNum",pro.ProNum)
                                     
                                     };
            list.AddRange(param);
            if (temp == 3)//新增
            {
                list.Add(new SQLiteParameter("@DelFlag", pro.DelFlag));
                list.Add(new SQLiteParameter("@SubTime", pro.SubTime));
                list.Add(new SQLiteParameter("@SubBy", pro.SubBy));
            }
            else if (temp == 4)//修改
            {
                list.Add(new SQLiteParameter("@ProId", pro.ProId));
            }
            return SqliteHelper.ExecuteNonquery(sql, list.ToArray());
        }
        /// <summary>
        /// 根据产品的id查询该产品的信息
        /// </summary>
        /// <param name="proId">产品id</param>
        /// <returns>对象</returns>
        public ProductInfo GetProductInfoByProId(int proId)
        {
            string sql = "select * from ProductInfo where ProId=@ProId";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@ProId", proId));
            ProductInfo pro = null;
            if (dt.Rows.Count > 0)
            {
                pro = RowToProductInfo(dt.Rows[0]);
            }
            return pro;
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteProductById(int id)
        {
            string sql = "update ProductInfo set DelFlag=1 where ProId=@ProId";
            return SqliteHelper.ExecuteNonquery(sql, new SQLiteParameter("@ProId", id));
        }
        /// <summary>
        /// 查询所有没被删除的产品
        /// </summary>
        /// <param name="delFlag">删除标识</param>
        /// <returns></returns>
        public List<ProductInfo> GetAllProductInfoByDelFlag(int delFlag)
        {
            string sql = "select * from ProductInfo where DelFlag=@DelFlag";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@DelFlag", delFlag));
            List<ProductInfo> list = new List<ProductInfo>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ProductInfo pro = RowToProductInfo(item);
                    list.Add(pro);
                }
            }
            return list;
        }
        //关系转对象
        private ProductInfo RowToProductInfo(DataRow dr)
        {
            ProductInfo pro = new ProductInfo();
            pro.CatId = Convert.ToInt32(dr["CatId"]);
            pro.DelFlag = Convert.ToInt32(dr["DelFlag"]);
            pro.ProCost = Convert.ToDecimal(dr["ProCost"]);
            pro.ProId = Convert.ToInt32(dr["ProId"]);
            pro.ProName = dr["ProName"].ToString();
            pro.ProNum = dr["ProNum"].ToString();
            pro.ProPrice = Convert.ToDecimal(dr["ProPrice"]);
            pro.ProSpell = dr["ProSpell"].ToString();
            pro.ProStock = Convert.ToDecimal(dr["ProStock"]);
            pro.ProUnit = dr["ProUnit"].ToString();
            pro.Remark = dr["Remark"].ToString();
            pro.SubBy = Convert.ToInt32(dr["SubBy"]);
            pro.SubTime = Convert.ToDateTime(dr["SubTime"]);
            return pro;
        }
    }
}
