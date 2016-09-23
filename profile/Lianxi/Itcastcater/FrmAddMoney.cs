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
    public partial class FrmAddMoney : Form
    {
        public FrmAddMoney()
        {
            InitializeComponent();
        }
        private int ID { get; set; }
        public void SetText(object sender, EventArgs e)
        {
            FrmEventArgs fea = e as FrmEventArgs;
            labDeskName.Text = fea.Name;
            this.ID = fea.Temp;//把订单的id存起来
        }
        //窗体加载的时候
        private void FrmAddMoney_Load(object sender, EventArgs e)
        {
            //加载所有产品
            LoadProductInfoByDelFlag(0);
            LoadProductAndCategory();
            LoadROrderProduct();
        }
        #region 所有产品
        //加载所有产品
        private void LoadProductInfoByDelFlag(int p)
        {
            ProductInfoBLL bll = new ProductInfoBLL();
            dgvProduct.AutoGenerateColumns = false;//禁止自动生成列
            dgvProduct.DataSource = bll.GetAllProductInfoByDelFlag(p);
            dgvProduct.SelectedRows[0].Selected = false;//默认第一行不选中
        }
        #endregion 所有产品
        #region 节点
        //加载节点树
        private void LoadProductAndCategory()
        {
            CategoryInfoBLL bll = new CategoryInfoBLL();
            List<CategoryInfo> list = bll.GetAllCategoryInfoByDelFlag(0);
            LoadCategory(list, tvCategory.Nodes);
        }
        //加载所有的商品类别
        private void LoadCategory(List<CategoryInfo> list, TreeNodeCollection tnc)
        {
            foreach (CategoryInfo item in list)
            {
                TreeNode tn = tnc.Add(item.CatName);
                LoadProdcut(item.CatId, tn.Nodes);
            }
        }
        //根据商品类别id查找该类别下所有的产品
        private void LoadProdcut(int p, TreeNodeCollection tnc)
        {
            ProductInfoBLL bll = new ProductInfoBLL();
            List<ProductInfo> list = bll.GetProductInfoByCatId(p);
            foreach (ProductInfo item in list)
            {
                tnc.Add(item.ProName + "===" + item.ProPrice + "元");
            }
        }
        #endregion 节点树
        //点菜
        private void dgvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //首先获取该餐桌的订单id
            //获取点菜的菜的id
            //插入到订单和菜单的中间表中
            if (dgvProduct.SelectedRows.Count > 0)
            {
                //首先获取该餐桌的订单id

                //获取点菜的菜的id
                int proId = Convert.ToInt32(dgvProduct.SelectedRows[0].Cells[0].Value);
                //插入到订单和菜单的中间中
                //数量
                int count = string.IsNullOrEmpty(txtCount.Text) || txtCount.Text == "1" ? 1 : Convert.ToInt32(txtCount.Text);//应该做一个是不是数字的判断
                R_OrderInfo_ProductBLL bll = new R_OrderInfo_ProductBLL();
                R_OrderInfo_Product roip = new R_OrderInfo_Product();
                roip.DelFlag = 0;
                roip.OrderId = this.ID;
                roip.ProId = proId;
                roip.State = 0;
                roip.SubTime = System.DateTime.Now;
                roip.UnitCount = count;
                if (bll.AddR_OrderInfo_Product(roip))
                {
                    LoadROrderProduct();
                    //显示点的菜
                    txtCount.Text = "";
                }
                else
                {
                    MessageBox.Show("对不起这个菜没了,今天厨师心情不好");
                }
            }
        }
        //加载点的菜
        private void LoadROrderProduct()
        {
            R_OrderInfo_ProductBLL bll = new R_OrderInfo_ProductBLL();
            dgvROrderProduct.AutoGenerateColumns = false;
            dgvROrderProduct.DataSource = bll.GetROrderInfoProduct(this.ID);
            //dgvROrderProduct.SelectedRows[0].Selected = false;

            //计算总金额还有总数量
            R_OrderInfo_Product r = bll.GetMoneyAndCount(this.ID);
            labSumMoney.Text = r.MONEY.ToString();//总金额
            labCount.Text = r.CT.ToString();//总数量
        }
        //退菜
        private void btnDeleteRorderPro_Click(object sender, EventArgs e)
        {
            //获取当前选中的id
            if (dgvROrderProduct.SelectedRows.Count > 0)
            {
                //获取当前选中的id
                int id = Convert.ToInt32(dgvROrderProduct.SelectedRows[0].Cells[0].Value);
                R_OrderInfo_ProductBLL bll = new R_OrderInfo_ProductBLL();
                if (bll.UpdateROrderProduct(id))
                {
                    MessageBox.Show("退菜成功");
                    LoadROrderProduct();//刷新
                }
                else
                {
                    MessageBox.Show("退菜失败");
                }
            }
        }
        //项目搜索：拼音/编码
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ProductInfoBLL bll = new ProductInfoBLL();
            string txt = txtSearch.Text;
            int n = 0;
            if (!string.IsNullOrEmpty(txt))
            {
                if (char.IsLetter(txt[0]))//是不是字母
                {
                    n = 2;
                }
                else//不是字母
                {
                    n = 1;
                }
            }
            dgvProduct.AutoGenerateColumns = false;
            dgvProduct.DataSource = bll.GetProductBySpellOrNum(txt, n);
            dgvProduct.SelectedRows[0].Selected = false;
        }
        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
