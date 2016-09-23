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
    public partial class FrmChangeMemmber : Form
    {
        public FrmChangeMemmber()
        {
            InitializeComponent();
        }
        //加载所有会员类别
        public void LoadMemmberType()
        {
            MemmberTypeBLL bll = new MemmberTypeBLL();
            List<MemmberType> list = bll.GetAllMemmberTypeByDelFlag();
            list.Insert(0, new MemmberType() { MemType = -1, MemTpName = "请选择" });
            cmbMemType.DataSource = list;
            cmbMemType.DisplayMember = "MemTpName";
            cmbMemType.ValueMember = "MemType";
        }
        private int Tp { get; set; }  //存标识
        //传值用的
        public void SetText(object sender, EventArgs e)
        {
            LoadMemmberType();
            FrmEventArgs fea = e as FrmEventArgs;
            this.Tp = fea.Temp;
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tb = item as TextBox; //将控件转为文件框
                    tb.Text = "";//清空所有文本框
                }
              }

            if (fea.Temp == 2)  //修改
            {
               MemmberInfo mem = fea.obj as MemmberInfo;
               //设置所有文本框的内容
               txtBirs.Text = mem.MemBirthdaty.ToString();//生日
               txtMemDiscount.Text = mem.MemDiscount.ToString();//折扣
               txtMemIntegral.Text = mem.MemIntegral.ToString();//积分
               txtmemMoney.Text = mem.MemMoney.ToString();//余额
               txtMemName.Text = mem.MemName;//会员名字
               txtMemNum.Text = mem.MemNum;//会员编号
               txtMemPhone.Text = mem.MemMobilePhone;//手机
               dtEndServerTime.Value = Convert.ToDateTime(mem.MemEndServerTime);//有效时间
               rdoMan.Checked = mem.MemGender == "男" ? true : false;
               rdoWomen.Checked = mem.MemGender == "女" ? true : false;
               labId.Text = mem.MemmberId.ToString();
               cmbMemType.SelectedValue = mem.MemType;
            }
            else if (fea.Temp == 1)   //新增
            {
                txtMemIntegral.Text = "0";
            }
              
        }

        private void btnOk_Click(object sender, EventArgs e)
        {  
            //获取会员信息
            //不为空
            MemmberInfo mem = new MemmberInfo();
            if (IsCheck())
            {
                mem.MemAddress = txtAddress.Text;
                mem.MemBirthdaty = Convert.ToDateTime(txtBirs.Text);//生日最好用控件
                mem.MemDiscount = Convert.ToDecimal(txtMemDiscount.Text);//折扣
                mem.MemEndServerTime = dtEndServerTime.Value;
                mem.MemGender = IsGender();
                mem.MemIntegral = Convert.ToInt32(txtMemIntegral.Text);
                mem.MemMobilePhone = txtMemPhone.Text;
                mem.MemMoney = Convert.ToDecimal(txtmemMoney.Text);
                mem.MemName = txtMemName.Text;
                mem.MemNum = txtMemNum.Text;
                mem.MemType = Convert.ToInt32(cmbMemType.SelectedValue);
            }
            else
            {
                //MessageBox.Show("不能为空"); 
                return;   //没的话，直接确定会生成空值，报错
            }
          MemmberInfoBLL bll = new MemmberInfoBLL();
            //新增还是修改
            if (this.Tp == 1)
            {
                mem.DelFlag = 0;
                mem.SubTime = System.DateTime.Now;  
            }
            else if (this.Tp == 2)
            {
                mem.MemmberId = Convert.ToInt32(labId.Text);
            }
            string st = bll.SaveMemmber(mem, this.Tp) ? "操作成功" : "操作失败";
            MessageBox.Show(st);
            this.Close();
        }
        //判断男女
        private string IsGender()
        {
            string str = "";
            if (rdoMan.Checked)
            {
                str= "男";
            }
            else if(rdoWomen.Checked)
            {
                str= "女";
            }
            return str;
        }
        //判断文本框不能为空
        private bool IsCheck()
        {
            if (string.IsNullOrEmpty(txtBirs.Text))
            {
                MessageBox.Show("生日不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtMemDiscount.Text))
            {
                MessageBox.Show("折扣不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtMemIntegral.Text))
            {
                MessageBox.Show("积分不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtmemMoney.Text))
            {
                MessageBox.Show("余额不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtMemName.Text))
            {
                MessageBox.Show("名字不能为空");
                return false;

            }
            if (string.IsNullOrEmpty(txtMemNum.Text))
            {
                MessageBox.Show("编号不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtMemPhone.Text))
            {
                MessageBox.Show("电话不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(dtEndServerTime.Text))
            {
                MessageBox.Show("有效期不能为空");
                return false;
            }
            return true;
        }
        //取消
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
