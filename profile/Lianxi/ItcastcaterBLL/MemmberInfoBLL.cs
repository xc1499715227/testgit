using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using Itcastcater.DAL;
namespace Itcastcater.BLL
{
    public class MemmberInfoBLL
    {
        MemmberInfoDAL dal = new MemmberInfoDAL();
        /// <summary>
        /// 新增和修改 
        /// </summary>
        /// <param name="memmber">会员对象</param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public bool SaveMemmber(MemmberInfo memmber, int temp)
        {
            int r = -1;
            if (temp == 1)
            {
                r = dal.AddMemmberInfo(memmber);
            }
            else if (temp == 2)
            {
                r = dal.UpdateMemmberInfo(memmber);
            }
            return r > 0 ? true : false;
        }
        
        //查询会员信息
        public MemmberInfo GetMemmberInfoMemmberId(int memmberId)
        {
            return dal.GetMemmberInfoByMemmberId(memmberId);
        }
        //删会员
        public bool DeleteMemmberByMemmberId(int memmberId)
        {
            return dal.DeleteMemmberByMemmberId(memmberId) > 0 ? true : false;
        }
        //未删除的会员信息
        public List<MemmberInfo> GetAllMemmberInfoByDelFlag(int delFlag)
        {
            return dal.GetAllMemmberInfoByDelFlag(delFlag);
        }
    }
}
