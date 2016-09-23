using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using Itcastcater.DAL;
namespace Itcastcater.BLL
{
    public  class DeskInfoBLL
    {
        DeskInfoDAL dal = new DeskInfoDAL();
        /// <summary>
        /// 根据餐桌的id修改该餐桌的状态
        /// </summary>
        /// <param name="deskId">餐桌的id</param>
        /// <param name="temp">状态0--------空闲,1--------使用</param>
        /// <returns></returns>
        public bool UpdateDeskStateByDeskId(int deskId, int temp)
        {
            return dal.UpdateDeskStateByDeskId(deskId, temp) > 0 ? true : false;
        }
        /// <summary>
        /// 根据房间的id查询该房间下的所有餐桌
        /// </summary>
        /// <param name="roomId">房间的id</param>
        /// <returns>餐桌对象集合</returns>
        public List<DeskInfo> GetDeskInfoByRoomId(int roomId)
        {
            return dal.GetDeskInfoByRoomId(roomId);
        }
        /// <summary>
        /// 根据房间的id删除
        /// </summary>
        /// <param name="roomId">房间的id</param>
        /// <returns></returns>
        public bool DeleteDeskInfoByRoomId(int roomId)
        {
            return dal.DeleteDeskInfoByRoomId(roomId) >= 0 ? true : false;  //>0    >=0
        }
        /// <summary>
        /// 查询该房间下是否有正在使用的餐桌
        /// </summary>
        /// <param name="roomId">房间的id</param>
        /// <returns></returns>
        public bool GetDeskInfoStateByRoomId(int roomId)
        {
            return Convert.ToInt32(dal.GetDeskInfoStateByRoomId(roomId)) > 0 ? true : false;
        }
        //删除餐桌
        public bool DeleteDeskById(int id)
        {
            return dal.DeleteDeskById(id) > 0 ? true : false;
        }

        /// <summary>
        /// 根据餐桌的id查询该餐桌是不是空闲的
        /// </summary>
        /// <param name="id">餐桌id</param>
        /// <returns></returns>
        public bool SearchDeskById(int id)
        {
            return Convert.ToInt32(dal.SearchDeskById(id)) > 0 ? true : false;
        }
        /// <summary>
        /// 添加和修改餐桌
        /// </summary>
        /// <param name="dk">餐桌对象</param>
        /// <param name="temp">3增加4修改</param>
        /// <returns></returns>
        public bool SaveDesk(DeskInfo dk, int temp)
        {
            int r = -1;
            if (temp == 3)
            {
                r = dal.AddDesk(dk);
            }
            else if (temp == 4)
            {
                r = dal.UpdateDeskByDeskId(dk);
            }
            return r > 0 ? true : false;
        }
        /// <summary>
        /// 根据餐桌id获取该餐桌对象
        /// </summary>
        /// <param name="deskId">餐桌id</param>
        /// <returns></returns>
        public DeskInfo GetDeskInfoByDeskId(int deskId)
        {
            return dal.GetDeskInfoByDeskId(deskId);
        }
        /// <summary>
        /// 查询所有的餐桌
        /// </summary>
        /// <param name="delFlag">删除标识</param>
        /// <returns>餐桌对象集合</returns>
        public List<DeskInfo> GetAllDeskInfoByDelFlag(int delFlag)
        {
            return dal.GetAllDeskInfoByDelFlag(delFlag);
        }
    }
}
