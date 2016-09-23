using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using Itcastcater.DAL;
namespace Itcastcater.BLL
{
    public class CategoryInfoBLL
    {
        CategoryInfoDAL dal = new CategoryInfoDAL();
        //删除商品类别
        public bool DeleteCategoryById(int id)
        {
            return dal.DeleteCategoryById(id) > 0 ? true : false;
        }
        /// <summary>
        /// 新增和修改商品类别
        /// </summary>
        /// <param name="cat">商品对象</param>
        /// <param name="temp">1---新增,2----修改</param>
        /// <returns></returns>
        public bool SaveCategory(CategoryInfo cat, int temp)
        {
            int r = -1;
            if (temp == 1)
            {
                r = dal.AddCategoryInfo(cat);
            }
            else if (temp == 2)
            {
                r = dal.UpdateCategoryInfo(cat);
            }
            return r > 0 ? true : false;
        }

        /// <summary>
        /// 根据id查对象
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public CategoryInfo GetCategoryInfoByCaitId(int id)
        {
            return dal.GetCategoryInfoByCaitId(id);
        }
        /// <summary>
        /// 查询所有没被删除的商品类别
        /// </summary>
        /// <param name="delFlag">删除标识-0没删除,1删除</param>
        /// <returns></returns>
        public List<CategoryInfo> GetAllCategoryInfoByDelFlag(int delFlag)
        {
            return dal.GetAllCategoryInfoByDelFlag(delFlag);
        }
    }
}
