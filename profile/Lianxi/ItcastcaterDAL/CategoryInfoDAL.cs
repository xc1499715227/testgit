using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using ItcastCater.Model;
namespace Itcastcater.DAL
{
    public class CategoryInfoDAL
    {
        public int DeleteCategoryById(int id)
        {
            string sql = "update CategoryInfo set DelFlag=1 where CatId=@CatId";
            return SqliteHelper.ExecuteNonquery(sql, new SQLiteParameter("@CatId", id));
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public int AddCategoryInfo(CategoryInfo cat)
        {
            string sql = "insert into CategoryInfo(CatName,CatNum,Remark,DelFlag,SubTime,SubBy) values(@CatName,@CatNum,@Remark,@DelFlag,@SubTime,@SubBy)";
            return AddAndUpdate(sql, cat, 1);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public int UpdateCategoryInfo(CategoryInfo cat)
        {
            string sql = "update CategoryInfo set CatName=@CatName,CatNum=@CatNum,Remark=@Remark where CatId=@CatId";
            return AddAndUpdate(sql, cat, 2);

        }//合并
        private int AddAndUpdate(string sql, CategoryInfo cat, int temp)
        {
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            SQLiteParameter[] param = { 
                new SQLiteParameter("@CatName",cat.CatName),
                new SQLiteParameter("@CatNum",cat.CatNum),
                new SQLiteParameter("@Remark",cat.Remark)
                                     
                                     };
            list.AddRange(param);
            if (temp == 1)
            {
                list.Add(new SQLiteParameter("@DelFlag", cat.DelFlag));
                list.Add(new SQLiteParameter("@SubTime", cat.SubTime));
                list.Add(new SQLiteParameter("@SubBy", cat.SubBy));
            }
            else if (temp == 2)
            {
                list.Add(new SQLiteParameter("@CatId", cat.CatId));
            }
            return SqliteHelper.ExecuteNonquery(sql, list.ToArray());
        }
        /// <summary>
        /// 根据id查对象
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public CategoryInfo GetCategoryInfoByCaitId(int id)
        {
            string sql = "select * from CategoryInfo where CatId=@CatId";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@CatId", id));
            CategoryInfo cat = null;
            if (dt.Rows.Count > 0)
            {
                cat = RowToCategoryInfo(dt.Rows[0]);
            }
            return cat;
        }
        /// <summary>
        /// 查询所有没被删除的商品类别
        /// </summary>
        /// <param name="delFlag">0没被删除，1被删除</param>
        /// <returns></returns>
        public List<CategoryInfo> GetAllCategoryInfoByDelFlag(int delFlag)
        {
            string sql = "select * from CategoryInfo where DelFlag=@DelFlag";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@DelFlag", delFlag));
            List<CategoryInfo> list = new List<CategoryInfo>();
            if (dt.Rows.Count > 0)//有数据
            {
                foreach (DataRow item in dt.Rows)
                {
                    CategoryInfo ct = RowToCategoryInfo(item);
                    list.Add(ct);
                }
            }
            return list;
        }
        //关系转对象
        private CategoryInfo RowToCategoryInfo(DataRow dr)
        {
            CategoryInfo ct = new CategoryInfo();
            ct.CatId = Convert.ToInt32(dr["CatId"]);
            ct.CatName = dr["CatName"].ToString();
            ct.CatNum = dr["CatNum"].ToString();
            ct.DelFlag = Convert.ToInt32(dr["DelFlag"]);
            ct.Remark = dr["Remark"].ToString();
            ct.SubBy = Convert.ToInt32(dr["SubBy"]);
            ct.SubTime = Convert.ToDateTime(dr["SubTime"]);
            return ct;
        }
    }
}
