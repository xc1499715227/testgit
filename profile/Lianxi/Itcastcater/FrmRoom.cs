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
    public partial class FrmRoom : Form
    {
        public FrmRoom()
        {
            InitializeComponent();
        }
        public void SetText(object sender, EventArgs e)
        {
        }
        private void FrmRoom_Load(object sender, EventArgs e)
        {
            //加载所有没被删除的房间
            //加载所有没被删除的餐桌
            LoadDeskInfoByDelFlag(0);
            LoadRoomInfoByDelFlag(0);
        }
        private void LoadDeskInfoByDelFlag(int p)
        {
            DeskInfoBLL bll = new DeskInfoBLL();
            dgvDeskInfo.AutoGenerateColumns = false;
            dgvDeskInfo.DataSource = bll.GetAllDeskInfoByDelFlag(p);
            dgvDeskInfo.SelectedRows[0].Selected = false;
        }
        private void LoadRoomInfoByDelFlag(int p)
        {
            RoomInfoBLL bll = new RoomInfoBLL();
            dgvRoomInfo.AutoGenerateColumns = false;
            dgvRoomInfo.DataSource = bll.GetAllRoomInfoByDelFlag(p);
            dgvRoomInfo.SelectedRows[0].Selected = false;
        }
        public event EventHandler evt;
        //增加房间
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            ShowFrmChangeRoom(1);
        }
        //修改房间
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvRoomInfo.SelectedRows.Count > 0)
            {
                //获取id
                int id = Convert.ToInt32(dgvRoomInfo.SelectedRows[0].Cells[0].Value);
                //根据这个id查询数据库这个id对应的所有的数据
                RoomInfoBLL bll = new RoomInfoBLL();
                RoomInfo room = bll.GetRoomInfoByRoomId(id);
                if (room != null)
                {
                    fea.obj = room;
                    ShowFrmChangeRoom(2);
                }
            }
        }
        FrmEventArgs fea = new FrmEventArgs();
        //显示新增或修改房间的窗体：1-新增 2-修改
        private void ShowFrmChangeRoom(int p)
        {
            FrmChangeRoom fcr = new FrmChangeRoom();
            //注册事件
            this.evt+=new EventHandler(fcr.SetText);
            fea.Temp = p; //准备传参的值
            if (this.evt != null)
            {
                this.evt(this, fea);
            }
            fcr.FormClosed += new FormClosedEventHandler(fcr_FormClosed);
            fcr.ShowDialog();
        }
        //都刷新一下
        void fcr_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadRoomInfoByDelFlag(0);
            LoadDeskInfoByDelFlag(0);
        }
        public event EventHandler evtDesk; //定义餐桌事件
        //增加餐桌
        private void btnAddDesk_Click(object sender, EventArgs e)
        {
            ShowFrmChangeDesk(3);
        }
        //修改餐桌
        private void btnUpdateDesk_Click(object sender, EventArgs e)
        {
            if (dgvDeskInfo.SelectedRows.Count > 0)
            {
                //获取选中餐桌的id
                int id = Convert.ToInt32(dgvDeskInfo.SelectedRows[0].Cells[0].Value);
                //根据餐桌的id查询该行数据,返回的是餐桌的对象
                DeskInfoBLL bll = new DeskInfoBLL();
                DeskInfo dk = bll.GetDeskInfoByDeskId(id);
                dk.DeskId = id;
                fea.obj = dk;
                ShowFrmChangeDesk(4);
            }
        }
        private void ShowFrmChangeDesk(int p)
        {
            FrmChangeDesk fcd = new FrmChangeDesk();
            //注册事件
            this.evtDesk+=new EventHandler(fcd.SetText);
            fea.Temp = p;
            if (this.evtDesk != null)
            {
                this.evtDesk(this, fea);
            }
            fcd.FormClosed+=new FormClosedEventHandler(fcr_FormClosed);
            fcd.ShowDialog();
        }
        //删除餐桌
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //判断是否有选中的行
            if (dgvDeskInfo.SelectedRows.Count > 0)
            {
                //获取id
                int id = Convert.ToInt32(dgvDeskInfo.SelectedRows[0].Cells[0].Value);
                //餐桌的状态 1-使用  0-空闲
                DeskInfoBLL bll = new DeskInfoBLL();
                if (bll.SearchDeskById(id))  //空闲可以删除
                {
                    if (bll.DeleteDeskById(id))
                    {
                        MessageBox.Show("操作成功");
                        LoadDeskInfoByDelFlag(0);
                    }
                    else
                    {
                        MessageBox.Show("操作失败");
                    }
                }
                else
                {
                    MessageBox.Show("餐桌正在使用，不能删除");
                }
            }
            else
            {
                MessageBox.Show("请选中行");
            }
        }
        //房间的删除
        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvRoomInfo.SelectedRows.Count > 0)
            {    
                if(DialogResult.OK==MessageBox.Show("您确定删除吗？","删除",MessageBoxButtons.OKCancel))
                {
                    DeskInfoBLL bll=new DeskInfoBLL();
                    int roomId = Convert.ToInt32(dgvRoomInfo.SelectedRows[0].Cells[0].Value);
                    //可以删除了，这个房间是否有餐桌，这个房间餐桌是否在被使用
                    if (bll.GetDeskInfoStateByRoomId(roomId))
                    {
                        MessageBox.Show("房间下还有正在使用的餐桌，不能删除");
                    }
                    else
                    {
                        //先删除餐桌，再删除房间
                        RoomInfoBLL rbll=new RoomInfoBLL();
                        if (bll.DeleteDeskInfoByRoomId(roomId) && rbll.DeleteRoomInfoByRoomId(roomId))
                        {
                            MessageBox.Show("操作成功！");
                            LoadDeskInfoByDelFlag(0);
                            LoadRoomInfoByDelFlag(0);
                        }
                        else
                        {
                            MessageBox.Show("操作失败！");
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("请选中删除的行");
            }
        }
    }
}
