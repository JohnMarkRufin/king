using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sampleProject_layout
{
    public partial class editProduct : Form
    {
        public editProduct()
        {
            InitializeComponent();
        }

        private void editProduct_Load(object sender, EventArgs e)
        {
            main.cbxLoadCategory("select categoryname from productcategory", cbxCategory);
            main.dgvView("select productid as 'ID', productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products order by productName asc", dgvEditProduct);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbxProduct.Text == "" || tbxPrice.Text == "" || tbxStock.Text == "")
            {
                MessageBox.Show("PLEASE ENTER ALL DATA TO UPDATE", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DialogResult dr = MessageBox.Show("ARE YOU SURE YOU WANT TO UPDATE THIS ITEM?", "UPDATE", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.Yes)
                {
                    main.cbxLoad("select categoryid from productcategory where categoryname = '" + cbxCategory.Text + "'");
                    main.saveData("update products set productname = '" + this.tbxProduct.Text + "',categoryid= '" + setandGet.categoryId + "',unitprice = '" + this.tbxPrice.Text + "',unitstock = '" + this.tbxStock.Text + "',expiredate= '" + this.expDate.Text + "',userid= '" + setandGet.userid + "' where productid = '" + tbxId.Text + "'");
                    main.dgvView("select productid as 'ID', productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products order by productName asc", dgvEditProduct);

                    tbxId.Text = "";
                    tbxProduct.Text = "";
                    tbxPrice.Text = "";
                    tbxStock.Text = "";
                }
            }
        }
        private void dgvEditProduct_Click(object sender, EventArgs e)
        {
            tbxId.Text = dgvEditProduct.CurrentRow.Cells[0].Value.ToString();
            tbxProduct.Text = dgvEditProduct.CurrentRow.Cells[1].Value.ToString();
            cbxCategory.Text = dgvEditProduct.CurrentRow.Cells[2].Value.ToString();
            tbxPrice.Text = dgvEditProduct.CurrentRow.Cells[3].Value.ToString();
            tbxStock.Text = dgvEditProduct.CurrentRow.Cells[4].Value.ToString();
            expDate.Text = dgvEditProduct.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbxId.Text == "")
            {
                MessageBox.Show("PLEASE SELECT PRODUCT FIRST", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DialogResult dr = MessageBox.Show("ARE YOU SURE YOU WANT TO DELETE THIS ITEM?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.Yes)
                {
                    // Instead of directly deleting, update IsDeleted to true
                    main.saveData("update products set IsDeleted = 1 where productid = '" + tbxId.Text + "'");
                    main.dgvView("select productid as 'ID', productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where IsDeleted = 0 order by productName asc", dgvEditProduct);

                    tbxId.Text = "";
                    tbxProduct.Text = "";
                    tbxPrice.Text = "";
                    tbxStock.Text = "";
                }
            }
        }

        private void tbxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar);
        }

        private void tbxStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            main.dgvView("select productid as 'ID', productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where productname like '" + tbxSearch.Text + "%'", dgvEditProduct);
            if(tbxSearch.Text == "")
            {
                main.dgvView("select productid as 'ID', productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products order by productName asc", dgvEditProduct);
            }
        }

        private void tbxPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
