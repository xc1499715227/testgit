using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using ItcastCater.Model;
namespace Itcastcater.DAL
{
    public class MemmberInfoDAL
    {

        /// <summary>
        /// 新增会员
        /// </summary>
        /// <param name="mem"></param>
        /// <returns></returns>
        public int AddMemmberInfo(MemmberInfo mem)
        {
            string sql = "insert into MemmberInfo(MemName,MemMobilePhone,MemType,MemNum,MemGender,MemDiscount,MemMoney,DelFlag,SubTime,MemIntegral,MemEndServerTime,MemBirthdaty) values(@MemName,@MemMobilePhone,@MemType,@MemNum,@MemGender,@MemDiscount,@MemMoney,@DelFlag,@SubTime,@MemIntegral,@MemEndServerTime,@MemBirthdaty)";
            return AddAndUpdate(sql, mem, 1);
        }
        /// <summary>
        /// 修改会员
        /// </summary>
        /// <param name="mem"></param>
        /// <returns></returns>
        public int UpdateMemmberInfo(MemmberInfo mem)
        {
            string sql = "update MemmberInfo set MemName=@MemName,MemMobilePhone=@MemMobilePhone,MemType=@MemType,MemNum=@MemNum,MemGender=@MemGender,MemDiscount=@MemDiscount,MemMoney=@MemMoney,MemIntegral=@MemIntegral,MemEndServerTime=@MemEndServerTime,MemBirthdaty=@MemBirthdaty where MemmberId=@MemmberId";
            return AddAndUpdate(sql, mem, 2);
        }
        //temp 1是新增，2是修改
        public int AddAndUpdate(string sql, MemmberInfo mem, int temp)
        {
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            SQLiteParameter[] param = { 
                    new SQLiteParameter("@MemName",mem.MemName),
                    new SQLiteParameter("@MemMobilePhone",mem.MemMobilePhone),
                    new SQLiteParameter("@MemType",mem.MemType),
                    new SQLiteParameter("@MemNum",mem.MemNum),
                    new SQLiteParameter("@MemGender",mem.MemGender),
                    new SQLiteParameter("@MemDiscount",mem.MemDiscount),
                    new SQLiteParameter("@MemMoney",mem.MemMoney),
                    new SQLiteParameter("@MemIntegral",mem.MemIntegral),
                    new SQLiteParameter("@MemEndServerTime",mem.MemEndServerTime),
                    new SQLiteParameter("@MemBirthdaty",mem.MemBirthdaty)                                               };
            list.AddRange(param);
            if (temp == 1)  //新增
            {
                list.Add(new SQLiteParameter("@DelFlag", mem.DelFlag));
                list.Add(new SQLiteParameter("@SubTime", mem.SubTime));
            }
            else if (temp == 2)  //修改
            {
                list.Add(new SQLiteParameter("@MemmberId", mem.MemmberId));
            }
            return SqliteHelper.ExecuteNonquery(sql, list.ToArray());
        }

        /// <summary>
        /// 根据id查询该会员
        /// </summary>
        /// <param name="memmberId">会员id</param>
        /// <returns>会员对象</returns>
        public MemmberInfo GetMemmberInfoByMemmberId(int memmberId)
        {
            string sql = "select * from MemmberInfo where MemmberId=@memmberId";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@MemmberId", memmberId));
            MemmberInfo mem = null;
            if (dt.Rows.Count > 0)
            {
                mem = RowToMemmberInfo(dt.Rows[0]);
            }
            return mem;
        }
        /// <summary>
        /// 根据会员ID删除该会员
        /// </summary>
        /// <param name="memmberId">会员id</param>
        /// <returns>受影响行数</returns>
        public int DeleteMemmberByMemmberId(int memmberId)
        {
            string sql = "update MemmberInfo set DelFlag=1 where MemmberId=@MemmberId";
            return SqliteHelper.ExecuteNonquery(sql, new SQLiteParameter("@MemmberId", memmberId));
        
        }
        public List<MemmberInfo> GetAllMemmberInfoByDelFlag(int delFlag)
        {
            string sql = "select * from MemmberInfo where DelFlag=@DelFlag";
            DataTable dt = SqliteHelper.ExecuteTable(sql, new SQLiteParameter("@DelFlag", delFlag));
            List<MemmberInfo> list = new List<MemmberInfo>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemmberInfo mem = RowToMemmberInfo(item);
                    list.Add(mem);
                }
            }
            return list;
        }
        //关系转对象
        private MemmberInfo RowToMemmberInfo(DataRow dr)
        {
            MemmberInfo mem = new MemmberInfo();
            mem.MemAddress = dr["MemAddress"].ToString();
            mem.MemBirthdaty = Convert.ToDateTime(dr["MemBirthdaty"]);
            mem.MemDiscount = Convert.ToDecimal(dr["MemDiscount"]);
            mem.MemEndServerTime = Convert.ToDateTime(dr["MemEndServerTime"]);
            mem.MemGender = dr["MemGender"].ToString();
            mem.MemIntegral = Convert.ToInt32(dr["MemIntegral"]);
            mem.MemmberId = Convert.ToInt32(dr["MemmberId"]);
            mem.MemMobilePhone = dr["MemMobilePhone"].ToString();
            mem.MemMoney = Convert.ToDecimal(dr["MemMoney"]);
            mem.MemName = dr["MemName"].ToString();
            mem.MemNum = dr["MemNum"].ToString();
            //mem.MemTpName
            mem.MemType = Convert.ToInt32(dr["MemType"]);
            mem.SubTime = Convert.ToDateTime(dr["SubTime"]);
            return mem;
        }
    }
}
