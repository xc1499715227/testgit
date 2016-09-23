using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Configuration;
using System.Data;

namespace Itcastcater.DAL
{
    class SqliteHelper
    {
        //连接字符串
        private static readonly string str = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
       /// <summary>
       /// 增删改
       /// </summary>
       /// <param name="sql">sql语句</param>
       /// <param name="param">sql参数</param>
       /// <returns>受影响的行数</returns>
        public static int ExecuteNonquery(string sql,params SQLiteParameter[] param)
        {
            using (SQLiteConnection con = new SQLiteConnection(str))
            { 
                using (SQLiteCommand cmd=new SQLiteCommand(sql,con))
                {
                    con.Open();
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns>首行首列</returns>
        public static object  ExecuteScalar(string sql, params SQLiteParameter[] param)
        {
            using (SQLiteConnection con = new SQLiteConnection(str))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    con.Open();
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 多行查询
        /// </summary>
        /// <param name="sql"></param>
        /// <paramname="param"></param>
        /// <returns>SQLiteDataReader</returns>
        public static SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] param)
        {
            SQLiteConnection con = new SQLiteConnection(str);
            using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
            {
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }
                try
                {
                    con.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 查询多行数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns>返回一个表</returns>
        public static DataTable ExecuteTable(string sql, params SQLiteParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter sda=new SQLiteDataAdapter (sql,str))
            {
                if (param != null)
                {
                    sda.SelectCommand.Parameters.AddRange(param);
                }
                sda.Fill(dt);
            }
            return dt;
        }
    }
}
