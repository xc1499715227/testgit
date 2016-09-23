using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Itcastcater.BLL;
using ItcastCater.Model;
using Itcastcater.DAL;
namespace Itcastcater.BLL
{
    public class MemmberTypeBLL
    {
        MemmberTypeDAL dal = new MemmberTypeDAL();
        /// <summary>
        /// 查询所有没有被删除的会员类型
        /// </summary>
        /// <returns>会员类型集合</returns>
        public List<MemmberType> GetAllMemmberTypeByDelFlag()
        {
            return dal.GetAllMemmberTypeByDelFlag();
        }
    }
}
