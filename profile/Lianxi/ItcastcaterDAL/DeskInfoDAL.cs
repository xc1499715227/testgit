using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using System.Data;
using System.Data.SQLite;

namespace Itcastcater.DAL
{
    public class DeskInfoDAL
    {
        /// <summary>
        /// 根据餐桌的id修改该餐桌的状态
        /// </summary>
        /// <param name="deskId">餐桌的id</param>
        /// <param name="temp">状态0--------空闲,1--------使用</param>
        /// <returns></returns>
        public int UpdateDeskStateByDeskId(int deskId, int temp)
        {
            string sql = "update DeskInfo set DeskState=@DeskState where DelFlag=0 and DeskId=@DeskId";
            SQLiteParameter[] param = { 
                     new SQLiteParameter("@DeskState",temp),
                 new SQLiteParameter("@DeskId",deskId)
                                      };
            return SqliteHelper.ExecuteNonquery(sql, param);
        }
        /// <summary>
        /// 根据房间的id删除删除
        /// </summary>
        /// <param name="roomId">房间的id</param>
        /// <returns></returns>
        public int DeleteDeskInfoByRoomId(int roomId)
        {
            string sql = "update DeskInfo set DelFlag=1 where RoomId=@RoomId";
            return SqliteHelper.ExecuteNonquery(sql, new SQLiteParameter("@RoomId", roomId));
        }
        /// <summary>
        /// 查询该房间下是否有正在使用的餐桌
        /// </summary>
        /// <param name="roomId">房间的id</param>
        /// <returns></returns>
        public object GetDeskInfoStateByRoomId(int roomId)
        {
            string sql = "select count(*) from deskinfo where delflag=0 and deskState=1 and RoomId=@RoomId";
            return SqliteHelper.ExecuteScalar(sql, new SQLiteParameter("@RoomId", roomId));

        }
        /// <summary>
        /// 根据房间的id查询该房间下的所有餐桌
        /// </summary>
        /// <param name="roomId">房间的id</param>
        /// <returns>餐桌对象集合</returns>
        public List<DeskInfo> GetDeskInfoByRoomId(int roomId)
        {
            string sql = "select * from DeskInfo where DelFlag=0 and RoomId=@RoomId";
            List<DeskInfo> list = new List<DeskInfo>();
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@RoomId", roomId));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    DeskInfo dk = RowToDeskInfo(item);
                    list.Add(dk);
                }
            }
            return list;
        }
        /// <summary>
        /// 删除餐桌
        /// </summary>
        /// <param name="id">餐桌id</param>
        /// <returns></returns>
        public int DeleteDeskById(int id)
        {
            string sql = "update DeskInfo set DelFlag=1 where DeskId=@DeskId";
            return  SqliteHelper.ExecuteNonquery(sql, new SQLiteParameter("@DeskId", id));
        }
        /// <summary>
        /// 根据餐桌的id查询该餐桌是不是空闲的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object SearchDeskById(int id)
        {
            string sql = "select count(*) from deskinfo where delflag=0 and deskid=@DeskId and deskState=0";
            return SqliteHelper.ExecuteScalar(sql, new SQLiteParameter("@DeskId", id));
        }
        //新增餐桌
        public int AddDesk(DeskInfo dk)
        {
            string sql = "insert into DeskInfo(RoomId,DeskName,DeskRemark,DeskRegion,DeskState,DelFlag,SubTime,SubBy) values(@RoomId,@DeskName,@DeskRemark,@DeskRegion,@DeskState,@DelFlag,@SubTime,@SubBy)";
            return AddAndUpdate(dk, sql, 3);
        }
        public int UpdateDeskByDeskId(DeskInfo dk)
        {
            string sql = "update DeskInfo set RoomId=@RoomId,DeskName=@DeskName,DeskRemark=@DeskRemark,DeskRegion=@DeskRegion where DeskId=@DeskId ";
            return AddAndUpdate(dk, sql, 4);    
        }
        //增加修改
        private int AddAndUpdate(DeskInfo dk, string sql, int tp)
        {
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            SQLiteParameter[] param = { 
                  new SQLiteParameter("@RoomId",dk.RoomId),
                    new SQLiteParameter("@DeskName",dk.DeskName),
                      new SQLiteParameter("@DeskRemark",dk.DeskRemark),
                        new SQLiteParameter("@DeskRegion",dk.DeskRegion)
                                      };
            list.AddRange(param);
            if (tp == 3)
            {
                list.Add(new SQLiteParameter("@DelFlag", dk.DelFlag));
                list.Add(new SQLiteParameter("@SubTime", dk.SubTime));
                list.Add(new SQLiteParameter("@SubBy", dk.SubBy));
                list.Add(new SQLiteParameter("@DeskState", dk.DeskState));

            }
            else if (tp == 4)
            {
                list.Add(new SQLiteParameter("@DeskId", dk.DeskId));
            }
            return SqliteHelper.ExecuteNonquery(sql, list.ToArray());
        }
        /// <summary>
        /// 根据餐桌id获取该餐桌对象
        /// </summary>
        /// <param name="deskId"></param>
        /// <returns></returns>
        public DeskInfo GetDeskInfoByDeskId(int deskId)
        {
            string sql = "select * from DeskInfo where DeskId=@DeskId and DelFlag=0";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@DeskId", deskId));
            DeskInfo dk = null;
            if (dt.Rows.Count > 0)
            {
                dk = RowToDeskInfo(dt.Rows[0]);

            }
            return dk;
        }
        /// <summary>
        /// 查询所有的餐桌
        /// </summary>
        /// <param name="delFlag">删除标识</param>
        /// <returns>餐桌对象集合</returns>
        public List<DeskInfo> GetAllDeskInfoByDelFlag(int delFlag)
        {
            string sql = "select * from DeskInfo where DelFlag=@delFlag";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@delFlag", delFlag));
            List<DeskInfo> list = new List<DeskInfo>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    DeskInfo dk = RowToDeskInfo(item);
                    list.Add(dk);
                }
            }
            return list;
        }
        //关系转对象
        private DeskInfo RowToDeskInfo(DataRow item)
        {
            DeskInfo desk = new DeskInfo();
            desk.DeskId = Convert.ToInt32(item["DeskId"]);
            desk.DeskName = item["DeskName"].ToString();
            desk.DeskRegion = item["DeskRegion"].ToString();
            desk.DeskRemark = item["DeskRemark"].ToString();
            desk.DeskState = Convert.ToInt32(item["DeskState"]);
            //状态
            desk.DeskStateString = Convert.ToInt32(item["DeskState"]) == 0 ? "空闲" : "开桌";

            desk.RoomId = Convert.ToInt32(item["RoomId"]);
            desk.SubBy = Convert.ToInt32(item["SubBy"]);
            desk.SubTime = Convert.ToDateTime(item["SubTime"]);
            return desk;
        }
    }
}
