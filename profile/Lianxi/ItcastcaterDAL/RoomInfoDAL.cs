using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using ItcastCater.Model;
namespace Itcastcater.DAL
{
    public class RoomInfoDAL
    {
        /// <summary>
        /// 根据房间id删除该房间
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public int DeleteRoomInfoByRoomId(int roomId)
        {
            string sql = "update RoomInfo set DelFlag=1 where RoomId=@RoomId";
            return SqliteHelper.ExecuteNonquery(sql, new SQLiteParameter("@RoomId", roomId));
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public int AddRoomInfo(RoomInfo room)
        { 
            string sql = "insert into RoomInfo(RoomName,RoomType,RoomMinimunConsume,RoomMaxConsumer,IsDefault,DelFlag,Subtime,SubBy) values(@RoomName,@RoomType,@RoomMinimunConsume,@RoomMaxConsumer,@IsDefault,@DelFlag,@Subtime,@SubBy)";
            return AddAndUpdate(room, sql, 1);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public int UpdateRoomInfo(RoomInfo room)
        { 
            string sql = "update RoomInfo set RoomName=@RoomName,RoomType=@RoomType,RoomMinimunConsume=@RoomMinimunConsume,RoomMaxConsumer=@RoomMaxConsumer,IsDefault=@IsDefault  where RoomId=@RoomId";
            return AddAndUpdate(room,sql,2);
        }

        private int AddAndUpdate(RoomInfo room, string sql, int temp)
        {
            SQLiteParameter[] param = { 
               new SQLiteParameter("@RoomName",room.RoomName),
                new SQLiteParameter("@RoomType",room.RoomType),
                 new SQLiteParameter("@RoomMinimunConsume",room.RoomMinimunConsume),
                  new SQLiteParameter("@RoomMaxConsumer",room.RoomMaxConsumer),
                   new SQLiteParameter("@IsDefault",room.IsDefault)
                                      };
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            list.AddRange(param);
            if (temp == 1)//新增
            {
                list.Add(new SQLiteParameter("@DelFlag", room.DelFlag));
                list.Add(new SQLiteParameter("@Subtime", room.SubTime));
                list.Add(new SQLiteParameter("@SubBy", room.SubBy));
            }
            else if (temp == 2)//修改
            {
                list.Add(new SQLiteParameter("@RoomId", room.RoomId));
            }
            return SqliteHelper.ExecuteNonquery(sql, list.ToArray());
        }

        /// <summary>
        /// 根据房间的id查询该房间的信息
        /// </summary>
        /// <param name="roomId">房间id</param>
        /// <returns>房间对象</returns>
        public RoomInfo GetRoomInfoByRoomId(int roomId)
        {
            string sql = "select * from RoomInfo where RoomId=@RoomId and DelFlag=0";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@RoomId", roomId));
            RoomInfo room = null;
            if (dt.Rows.Count > 0)
            {
                room = RowToRoomInfo(dt.Rows[0]);
            }
            return room;
        }
        /// <summary>
        /// 查询所有没有被删除的房间
        /// </summary>
        /// <param name="delFlag">删除标识</param>
        /// <returns>房间对象集合</returns>
        public List<RoomInfo> GetAllRoomInfoByDelFlag(int delFlag)
        {
            string sql = "select * from RoomInfo where DelFlag=@delFlag";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter
 ("@delFlag", delFlag));
            List<RoomInfo> list = new List<RoomInfo>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    RoomInfo room = RowToRoomInfo(item);
                    list.Add(room);
                }
            }
            return list;
        }
        //关系转对象
        private RoomInfo RowToRoomInfo(DataRow dr)
        {
            RoomInfo r = new RoomInfo();
            r.IsDefault = Convert.ToInt32(dr["IsDefault"]);
            r.RoomId = Convert.ToInt32(dr["RoomId"]);
            r.RoomMaxConsumer = Convert.ToDecimal(dr["RoomMaxConsumer"]);
            r.RoomMinimunConsume = Convert.ToDecimal(dr["RoomMinimunConsume"]);
            r.RoomName = dr["RoomName"].ToString();
            r.RoomType = Convert.ToInt32(dr["RoomType"]);
            r.SubBy = Convert.ToInt32(dr["SubBy"]);
            r.SubTime = Convert.ToDateTime(dr["SubTime"]);
            return r;
        }
    }
}
