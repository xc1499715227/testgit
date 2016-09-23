using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Itcastcater.BLL;
using ItcastCater.Model;
namespace Itcastcater
{
    public partial class FrmChangeDesk : Form
    {
        public FrmChangeDesk()
        {
            InitializeComponent();
        }
        private void LoadRoomType()
        {
            RoomInfoBLL bll = new RoomInfoBLL();
            List<RoomInfo> list = bll.GetAllRoomInfoByDelFlag(0);
            list.Insert(0, new RoomInfo() { RoomId = -1, RoomName = "请选择" });
            cmdRoom.DataSource = list;
            cmdRoom.DisplayMember = "RoomName";
            cmdRoom.ValueMember = "RoomId";

        }
        private int Temp { get; set; }
        public void SetText(object sender, EventArgs e)
        {
            FrmEventArgs fea = e as FrmEventArgs;
            this.Temp = fea.Temp; //标识
            LoadRoomType();//加载房间类型
            if (fea.Temp == 3)   //新增
            {
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        TextBox tb = item as TextBox;
                        tb.Text = "";
                    }
                }
            
            }
            else if (fea.Temp == 4) //修改
            {
                DeskInfo dk = fea.obj as DeskInfo;
                txtDeskName.Text = dk.DeskName;
                txtDeskRegion.Text = dk.DeskRegion;
                txtDeskRemark.Text = dk.DeskRemark;
                labId.Text = dk.DeskId.ToString();
                cmdRoom.SelectedValue = dk.RoomId;
            }
            //清空文本框
            //加载房间类型
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        { 
            //判断是新增还是修改
            if (CheckEmpty())
            {
                DeskInfo dk = new DeskInfo();
                dk.DeskName = txtDeskName.Text;
                dk.DeskRegion = txtDeskRegion.Text;
                dk.DeskRemark = txtDeskRemark.Text;
                
                if (this.Temp == 3)
                {
                    dk.DelFlag = 0;
                    dk.DeskState = 0;
                    dk.SubTime = System.DateTime.Now;
                    dk.SubBy = 1;
                    dk.RoomId = Convert.ToInt32(cmdRoom.SelectedValue);
                }
                else if (this.Temp == 4)
                { 
                    dk.DeskId = Convert.ToInt32(labId.Text);
                }
                DeskInfoBLL bll = new DeskInfoBLL();
                string msg = bll.SaveDesk(dk, this.Temp) ? "操作成功" : "操作失败";
                MessageBox.Show(msg);
                this.Close();
            }
        }
        //不能为空
        private bool CheckEmpty()
        {
            if (string.IsNullOrEmpty(txtDeskName.Text))
            {
                MessageBox.Show("餐桌的名字不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtDeskRegion.Text))
            {
                MessageBox.Show("描述信息不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtDeskRemark.Text))
            {
                MessageBox.Show("备注不能为空");
                return false;
            }
            return true;
        }
    }
}
