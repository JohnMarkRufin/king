using MySql.Data.MySqlClient;
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
    public partial class addProduct : Form
    {
        
        public addProduct()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(tbxMed.Text == "" || tbxPrice.Text == "")
            {
                MessageBox.Show("PLEASE FILL ALL DATA", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                main.cbxLoad("select categoryid from productcategory where categoryname = '" + cbxCategory.Text + "'");
                main.saveData("insert into products(productname,categoryid,unitprice,unitstock,expiredate,userid) values ('" + tbxMed.Text + "','" + setandGet.categoryId + "','" + decimal.Parse(tbxPrice.Text) + "','" + '0' + "','" + dateTimePicker1.Text + "', '" + setandGet.userid + "')");
                MessageBox.Show("ITEM SUCCESSFULLY SAVED", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                cbxCategory.SelectedIndex = -1;
                tbxMed.Text = "";
                tbxPrice.Text = "";
            }
        }

        private void addProduct_Load(object sender, EventArgs e)
        {
            main.cbxLoadCategory("select categoryname from productcategory", cbxCategory);
        }

        private void tbxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar);
        }

        private void tbxQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar);
        }
    }
}
