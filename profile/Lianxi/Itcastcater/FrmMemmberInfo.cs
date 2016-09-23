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
    public partial class FrmMemmberInfo : Form
    {
        public FrmMemmberInfo()
        {
            InitializeComponent();
        }
        public event EventHandler evt;//事件
        private void FrmMemmberInfo_Load(object sender, EventArgs e)
        {
            LoadMemmberInfoByDelFlag(0); //加载所有会员，0代表未删除的
        }
        //加载所有会员
        private void LoadMemmberInfoByDelFlag(int p)
        {
            MemmberInfoBLL bll = new MemmberInfoBLL();
            dgvMemmber.AutoGenerateColumns = false;//禁止自动生成列
            dgvMemmber.DataSource = bll.GetAllMemmberInfoByDelFlag(p);
            dgvMemmber.SelectedRows[0].Selected = false;//默认第一行不选中
        }
        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //判断是否有选中的行
            if (dgvMemmber.SelectedRows.Count > 0)
            {
                //判断是否有选中的行
                int id=Convert.ToInt32(dgvMemmber.SelectedRows[0].Cells[0].Value);
                MemmberInfoBLL bll = new MemmberInfoBLL();
                if (bll.DeleteMemmberByMemmberId(id))
                {
                    MessageBox.Show("操作成功");
                    LoadMemmberInfoByDelFlag(0);
                }
                else
                {
                    MessageBox.Show("操作失败");
                }
            }
            else
            {
                MessageBox.Show("对不起请选中删除的行");
            }
        }
        //新增会员
        private void btnAddMemMber_Click(object sender, EventArgs e)
        {
            //FrmChangeMemmber fcm = new FrmChangeMemmber();
            //fcm.ShowDialog();
            ShowFrmChangeMemmber(1);  //1表示新增
        }
        //修改会员
        private void btnUpdateMember_Click(object sender, EventArgs e)
        {
            //FrmChangeMemmber fcm = new FrmChangeMemmber();
            //fcm.ShowDialog();
            if (dgvMemmber.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvMemmber.SelectedRows[0].Cells[0].Value);
                //根据id去数据库查询是否存在
                MemmberInfoBLL bll = new MemmberInfoBLL();
                MemmberInfo mem = bll.GetMemmberInfoMemmberId(id);
                fea.obj = mem;
                ShowFrmChangeMemmber(2); //2表示修改
            }
            else
            {
                MessageBox.Show("请选择要修改的行");
            }
        }
        FrmEventArgs fea = new FrmEventArgs();
        private void ShowFrmChangeMemmber(int p)
        {
            FrmChangeMemmber fcm = new FrmChangeMemmber();
            this.evt+=new EventHandler(fcm.SetText); //注册事件
           
            fea.Temp = p;//传的是新增或修改的标识
            if (this.evt != null) //执行事件之前 判断不能为空
            {
                this.evt(this, fea);    
            }
            //新增和修改窗体关闭后会员窗口刷新
            fcm.FormClosed+=new FormClosedEventHandler(FrmMemmberInfo_Load);
            fcm.ShowDialog();
        }
    }
}
