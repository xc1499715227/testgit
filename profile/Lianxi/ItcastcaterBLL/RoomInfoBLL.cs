using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Itcastcater.DAL;
using ItcastCater.Model;
namespace Itcastcater.BLL
{
    public class RoomInfoBLL
    {
        RoomInfoDAL dal = new RoomInfoDAL();

        /// <summary>
        /// 根据房间id删除该房间
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public bool DeleteRoomInfoByRoomId(int roomId)
        {
            return dal.DeleteRoomInfoByRoomId(roomId) > 0 ? true : false;
        }
        /// <summary>
        /// 新增或者修改
        /// </summary>
        /// <param name="room"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public bool SaveRoom(RoomInfo room, int temp)
        {
            int r = -1;
            if (temp == 1)
            {
                r = dal.AddRoomInfo(room);
            }
            else if (temp == 2)
            {
                r = dal.UpdateRoomInfo(room);
            }
            return r > 0 ? true : false;
        }

        /// <summary>
        /// 根据房间的id查询该房间信息
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns>房间对象</returns>
        public RoomInfo GetRoomInfoByRoomId(int roomId)
        {
            return dal.GetRoomInfoByRoomId(roomId);
        }
        /// <summary>
        /// 查询所有没有被删除的房间
        /// </summary>
        /// <param name="delFlag">删除标识</param>
        /// <returns>房间对象集合</returns>
        public List<RoomInfo> GetAllRoomInfoByDelFlag(int delFlag)
        { 
            return dal.GetAllRoomInfoByDelFlag(delFlag);
        }
    }
}
