using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ItcastCater.Model;
using Itcastcater.BLL;
namespace Itcastcater
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        //登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
           //获取账号密码，再判断账号密码不为空
            string name = txtName.Text.Trim();
            string pwd = txtPwd.Text;
            string msg = "";
            if (CheckEmpty(name, pwd))
            {
                UserInfoBLL bll = new UserInfoBLL();
                if (bll.GetUserPwdByLoginName(name, pwd, out msg))
                {
                    msgDiv1.MsgDivShow(msg, 1,Bind);
                }
                else
                {
                    msgDiv1.MsgDivShow(msg, 1);
                }
            }
        }
        //判断账号密码不为空
        private bool CheckEmpty(string name, string pwd)
        {
             
            if (string.IsNullOrEmpty(name))
            {
                msgDiv1.MsgDivShow("账号不能为空", 1);
                return false;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                msgDiv1.MsgDivShow("密码不能为空", 1);
                return false;
            }
            return true;
        }
        void Bind()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        //关闭窗体
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
