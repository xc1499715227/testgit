using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItcastCater.Model;
using Itcastcater.DAL;
namespace Itcastcater.BLL
{
    public  class UserInfoBLL
    {
        UserInfoDAL dal = new UserInfoDAL();
        //判断用户是否登录成功
        public bool GetUserPwdByLoginName(string loginName, string pwd, out string msg)
        {
            bool flag = false;
            UserInfo user = dal.GetUserPwdByLoginName(loginName);
            if (user != null)
            {
                //存在
                if (user.Pwd == pwd)
                {
                    msg = "登录成功";
                    flag = true;
                }
                else
                {
                    msg = "账号或者密码错误";
                }
            }
            else
            {
                msg = "用户名不存在";
            }
            return flag;
        }
    }
}
