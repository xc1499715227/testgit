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
    public partial class FrmChangeProduct : Form
    {
        public FrmChangeProduct()
        {
            InitializeComponent();
        }
        private int Tp { get; set; }
        private void LoadCategory()
        {
            CategoryInfoBLL bll = new CategoryInfoBLL();
            List<CategoryInfo> list = bll.GetAllCategoryInfoByDelFlag(0);
            list.Insert(0, new CategoryInfo() { CatId = -1, CatName = "请选择" });
            cmbCategory.DataSource = list;
            cmbCategory.DisplayMember = "CatName";
            cmbCategory.ValueMember = "CatId";
        }
        public void SetText(object sender, EventArgs e)
        {
            LoadCategory();

            FrmEventArgs  fea = e as FrmEventArgs;
            this.Tp = fea.Temp;
            if (fea.Temp == 3)//新增
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
            else if (fea.Temp == 4)//修改
            {
                ProductInfo pro = fea.obj as ProductInfo;
                txtCost.Text = pro.ProCost.ToString();
                txtName.Text = pro.ProName;
                txtNum.Text = pro.ProNum;
                txtPrice.Text = pro.ProPrice.ToString();
                txtRemark.Text = pro.Remark;
                txtSpell.Text = pro.ProSpell;
                txtStock.Text = pro.ProStock.ToString();
                txtUnit.Text = pro.ProUnit.ToString();
                cmbCategory.SelectedValue = pro.CatId;

                //id存起来
                labId.Text = pro.ProId.ToString();
            }
        }
        //新增和修改产品
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckEmpty())
            {
                ProductInfo pro = new ProductInfo();
                pro.CatId = Convert.ToInt32(cmbCategory.SelectedValue);
                pro.ProCost = Convert.ToDecimal(txtCost.Text);
                pro.ProName = txtName.Text;
                pro.ProNum = txtNum.Text;
                pro.ProPrice = Convert.ToDecimal(txtPrice.Text);
                pro.ProSpell = txtSpell.Text;
                pro.ProStock = Convert.ToDecimal(txtStock.Text);
                pro.ProUnit = txtUnit.Text;
                pro.Remark = txtRemark.Text;
                if (this.Tp == 3)//新增
                {
                    pro.DelFlag = 0;
                    pro.SubBy = 1;
                    pro.SubTime = System.DateTime.Now;
                }
                else if (this.Tp == 4)//修改
                {
                    pro.ProId = Convert.ToInt32(labId.Text);
                }
                ProductInfoBLL bll = new ProductInfoBLL();
                string msg = bll.SaveProduct(pro, this.Tp) ? "操作成功" : "操作失败";
                MessageBox.Show(msg);
                this.Close();
            }
        }
        //产品不能为空
        private bool CheckEmpty()
        {
            if (string.IsNullOrEmpty(txtCost.Text))
            {
                MessageBox.Show("商品成本不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("商品名称不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtNum.Text))
            {
                MessageBox.Show("商品编号不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("商品价格不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                MessageBox.Show("商品备注不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtSpell.Text))
            {
                MessageBox.Show("商品拼音不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtStock.Text))
            {
                MessageBox.Show("商品库存不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(txtUnit.Text))
            {
                MessageBox.Show("商品单位不能为空");
                return false;
            }
            return true;
        }
    }
}
