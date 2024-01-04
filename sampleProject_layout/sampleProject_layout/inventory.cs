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
    public partial class inventory : Form
    {
        
        public inventory()
        {
            InitializeComponent();
        }

        private void inventory_Load(object sender, EventArgs e)
        {
            LoadData();
            main.cbxLoadCategory("select categoryname from productcategory", cbxCat);
        }

        private void LoadData()
        {
            main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where IsDeleted = 0 order by productName asc", dgvInventory);
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where productname like '" + tbxSearch.Text + "%'", dgvInventory);
            if (tbxSearch.Text == "")
            {
                LoadData();
            }
        }

        private void cbxCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxCat.SelectedItem.ToString() == "View All")
            {
                LoadData();
            }
            else if(cbxCat.SelectedItem.ToString() == "Vitamins & Supplements")
            {
                main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where categoryid = 1", dgvInventory);
            }
            else if (cbxCat.SelectedItem.ToString() == "Adult Multivitamins")
            {
                main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where categoryid = 2", dgvInventory);
            }
            else if (cbxCat.SelectedItem.ToString() == "Cold Medicines")
            {
                main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where categoryid = 3", dgvInventory);
            }
            else if (cbxCat.SelectedItem.ToString() == "Cough Medicines")
            {
                main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where categoryid = 4", dgvInventory);
            }
            else if (cbxCat.SelectedItem.ToString() == "Allergy Care")
            {
                main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where categoryid = 5", dgvInventory);
            }
            else if (cbxCat.SelectedItem.ToString() == "Headache, Fever & Flu")
            {
                main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where categoryid = 6", dgvInventory);
            }
            else if (cbxCat.SelectedItem.ToString() == "Healthy Aging")
            {
                main.dgvView("select productName as 'Medicine', categoryid as 'Category',unitprice as 'Price', unitstock as 'Stocks', ExpireDate as 'Expiration Date' from products where categoryid = 7", dgvInventory);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            // Open the editProduct form
            editProduct editForm = new editProduct();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // If the user clicks OK in the edit form, refresh the data
                LoadData();
            }
        }

        private void btnSupplyNewProducts_Click(object sender, EventArgs e)
        {
            // Open the addProduct form
            addProduct addForm = new addProduct();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // If the user clicks OK in the add form, refresh the data
                LoadData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
