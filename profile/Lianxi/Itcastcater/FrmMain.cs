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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        //加载房间  
        
        private void LoadRoomInfoByDelFlag(int p)
        {
            RoomInfoBLL bll = new RoomInfoBLL();
            List<RoomInfo> listRoom = bll.GetAllRoomInfoByDelFlag(0);
             
            for (int i = listRoom.Count - 1; i >= 0; i--)
            {
                TabPage tp = new TabPage();
                tp.Tag = listRoom[i];//存房间的对象
                tp.Text = listRoom[i].RoomName; //存房间的名字
                ListView lv = new ListView();
                lv.LargeImageList = imageList1;//给listView控件绑定图片集合
                lv.BackColor = Color.White;
                lv.Dock = DockStyle.Fill; //让listview控件在父容器中
                lv.MultiSelect = false; //只能选一个
                lv.View = View.LargeIcon;
                tp.Controls.Add(lv); 
                tcin.TabPages.Add(tp);
            }      
        }
        //加载餐桌
        private void LoadDeskInfoByTabPage(TabPage tp)
        {
            //获取房间的id
            RoomInfo room = tp.Tag as RoomInfo;
            DeskInfoBLL bll = new DeskInfoBLL();
            List<DeskInfo> listDesk = bll.GetDeskInfoByRoomId(Convert.ToInt32(room.RoomId));
            ListView lv = tp.Controls[0] as ListView;
            lv.Clear();
            for (int i = 0; i < listDesk.Count; i++)
            {
                //判断餐桌状态显示对象的图片
                lv.Items.Add(listDesk[i].DeskName, Convert.ToInt32(listDesk[i].DeskState));
                lv.Items[i].Tag = listDesk[i];//餐桌对象
            }
        }
        //会员管理
        private void button4_Click(object sender, EventArgs e)
        {
            FrmMemmberInfo fm = new FrmMemmberInfo();
            fm.ShowDialog();
        }
        //房间设置
        private void btnRoom_Click(object sender, EventArgs e)
        {
            FrmChangeRoom();
            
        }
        #region wenti
       // public event EventHandler evt ;
        private void FrmChangeRoom()
        {
            FrmRoom fr = new FrmRoom();
            //this.evt+= new EventHandler(fr.SetText);
            fr.FormClosed += new FormClosedEventHandler(fr_FormClosed);
            fr.ShowDialog();
        }
        void fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                tcin.Controls.Clear();
                LoadRoomInfoByDelFlag(0);
                TabPage tp = tcin.TabPages[0];
                //属性值更改时发生
                tcin.SelectedIndexChanged += new EventHandler(tcin_SelectedIndexChanged);
                LoadDeskInfoByTabPage(tp);
            }
            catch  
            {
                LoadRoomInfoByDelFlag(0);
                TabPage tp = tcin.TabPages[0];
                //属性值更改时发生
                tcin.SelectedIndexChanged += new EventHandler(tcin_SelectedIndexChanged);
                LoadDeskInfoByTabPage(tp);
            }
        }
        #endregion wenti
        //商品管理
        private void btnCategory_Click(object sender, EventArgs e)
        {
            FrmCategory fc = new FrmCategory();
            fc.Show();
        }
        //主界面加载
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //加载房间
            LoadRoomInfoByDelFlag(0);
            //加载餐桌
            TabPage tp = tcin.TabPages[0];
            //属性值更改时发生
            tcin.SelectedIndexChanged+=new EventHandler(tcin_SelectedIndexChanged);
             LoadDeskInfoByTabPage(tp);
             labTime.Text = "当前时间:      " + DateTime.Now.ToLongTimeString();
             button2.Hide();
        }
        void tcin_SelectedIndexChanged (object sender,EventArgs  e)
        {
            LoadDeskInfoByTabPage(tcin.TabPages[tcin.SelectedIndex]);
        }
        public event EventHandler evtBill;//开单的事件
        //顾客开单
        private void button1_Click(object sender, EventArgs e)
        {
            TabPage tp=tcin.TabPages[tcin.SelectedIndex];
            //获取当前选中房间的名字
            RoomInfo room = tp.Tag as RoomInfo;
            FrmEventArgs fea = new FrmEventArgs();
            fea.Money = Convert.ToDecimal(room.RoomMinimunConsume);//最低消费
            fea.Name = room.RoomName;//房间的名字
            ListView lv = tp.Controls[0] as ListView;//获取当前选项卡中的listview控件
            //判断是否有选中的餐桌
            if (lv.SelectedItems.Count > 0)
            { 
                //获取当前选中的餐桌
                DeskInfo dk = lv.SelectedItems[0].Tag as DeskInfo;
                if (dk.DeskState == 0)
                {
                    fea.obj = dk;    //餐桌对象
                    FrmBilling fbi = new FrmBilling();
                    this.evtBill += new EventHandler(fbi.SetText);//注册事件
                    if (this.evtBill != null)
                    {
                        this.evtBill(this, fea);
                    }
                    fbi.FormClosed += new FormClosedEventHandler(fbi_FormClosed);
                    fbi.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择未开单的餐桌");
                }
            }
            else
            {
                MessageBox.Show("看准目标再下手");
            }
        }
        //开单窗体关闭后刷新
        void fbi_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadDeskInfoByTabPage(tcin.SelectedTab);
        }
        //增加消费
        public event EventHandler evtAddMoney;
        private void btnAddMoney_Click(object sender, EventArgs e)
        {
           //获取当前选中的选项卡
            TabPage tp = tcin.TabPages[tcin.SelectedIndex];
            //获取当前选中房间的名字
            RoomInfo room = tp.Tag as RoomInfo;
            FrmEventArgs fea = new FrmEventArgs();
            fea.Money = Convert.ToDecimal(room.RoomMinimunConsume);//最低消费
            fea.Name = room.RoomName;//房间的名字
            //最低消费--坑
            //获取当前选项卡中的listview控件
            ListView lv = tp.Controls[0] as ListView;
            //判断是否有选中的餐桌
            if (lv.SelectedItems.Count > 0)
            {
                //获取当前选中的餐桌
                DeskInfo dk = lv.SelectedItems[0].Tag as DeskInfo;
                if (dk.DeskState == 1)
                {
                    fea.Name = dk.DeskName;//餐桌的编号
                    //订单的id,根据餐桌的id查找订单的id
                    OrderInfoBLL obll = new OrderInfoBLL();
                    int orderId = obll.GetOrderIdByDeskId(dk.DeskId);
                    fea.Temp = orderId;//订单的id
                    //好大的一个坑,还没传值呢
                    FrmAddMoney fam = new FrmAddMoney();
                    this.evtAddMoney += new EventHandler(fam.SetText);//注册事件
                    if (this.evtAddMoney != null)
                    {
                        this.evtAddMoney(this, fea);
                    }
                    fam.FormClosed += new FormClosedEventHandler(fbi_FormClosed);
                    fam.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择开单的餐桌");
                }

            }
            else
            {
                MessageBox.Show("看准目标再下手");
            }
        }
        private int ID { get; set; }
        //结账
        public event EventHandler evtMoney1;
        private void button3_Click(object sender, EventArgs e)
        {
            //获取当前选中的选项卡
            TabPage tp = tcin.TabPages[tcin.SelectedIndex];
            //获取当前选中房间的名字
            RoomInfo room = tp.Tag as RoomInfo;
            FrmEventArgs fea = new FrmEventArgs();
            fea.Money = Convert.ToDecimal(room.RoomMinimunConsume);//最低消费
            fea.Name = room.RoomName;//房间的名字
            //最低消费--坑
            //获取当前选项卡中的listview控件
            ListView lv = tp.Controls[0] as ListView;
            //判断是否有选中的餐桌
            if (lv.SelectedItems.Count > 0)
            {
                //获取当前选中的餐桌
                DeskInfo dk = lv.SelectedItems[0].Tag as DeskInfo;
                if (dk.DeskState == 1)
                {
                    fea.Name = dk.DeskName;//餐桌的编号
                    //订单的id,根据餐桌的id查找订单的id
                    OrderInfoBLL obll = new OrderInfoBLL();
                    int orderId = obll.GetOrderIdByDeskId(dk.DeskId);
                    fea.Temp = orderId;//订单的id
                    //好大的一个坑,还没传值呢
                    FrmMoney1 fam = new FrmMoney1();
                    this.evtMoney1 += new EventHandler(fam.SetText);//注册事件
                    if (this.evtMoney1 != null)
                    {
                        this.evtMoney1(this, fea);
                    }
                    fam.FormClosed += new FormClosedEventHandler(fbi_FormClosed);
                    fam.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择开单的餐桌");
                }

            }
            else
            {
                MessageBox.Show("看准目标再下手");
            }
        }

        private void btnYinCang_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Dock = DockStyle.Fill;
            button2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel1.Dock = DockStyle.None;
            panel2.Dock = DockStyle.Top;
             panel2.Anchor = AnchorStyles.Top;
             panel2.Anchor = AnchorStyles.Bottom;
             //panel2.Anchor = AnchorStyles.Left;
            //  panel2.Anchor = AnchorStyles.Right;     
            button2.Hide();
        }
    }
}
