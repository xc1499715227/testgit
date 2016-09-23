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
    public partial class FrmMoney1 : Form
    {
        public FrmMoney1()
        {
            InitializeComponent();
        }
        private int ID { get; set; }
        public void SetText(object sender, EventArgs e)
        {
            FrmEventArgs fea = e as FrmEventArgs;
            this.ID = fea.Temp;//把订单的id存起来
        }
        private void LoadROrderProduct()
        {
            R_OrderInfo_ProductBLL bll = new R_OrderInfo_ProductBLL();
            //计算总金额还有总数量
            R_OrderInfo_Product r = bll.GetMoneyAndCount(this.ID);
            label1.Text = "亲，您共消费了"+r.MONEY.ToString()+"元";//总金额
        }

        private void FrmMoney1_Load(object sender, EventArgs e)
        {
            LoadROrderProduct();
        }
    }
}
