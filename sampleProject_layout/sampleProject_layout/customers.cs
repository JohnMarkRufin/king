using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace sampleProject_layout
{
    public partial class customers : Form
    {

        public customers()
        {
            InitializeComponent();
            timer1.Start();

            btnDeleteCustomer = new Button();
            btnDeleteCustomer.Text = "Delete Customer";
            btnDeleteCustomer.Click += btnDeleteCustomer_Click; // Updated event handler assignment

            // Add the button to the form
            Controls.Add(btnDeleteCustomer);
        }
        int indexRow;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbxCustname.Text == "" || tbxContact.Text == "" || tbxAddress.Text == "")
            {
                MessageBox.Show("PLEASE FILL ALL DATA", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                main.saveData("insert into customers (customerName,contact,address) values ('" + tbxCustname.Text + "','" + tbxContact.Text + "','" + tbxAddress.Text + "')");
                cbxCustName.Items.Clear();
                main.cbxLoadCustomerName("select customername from customers", cbxCustName);
                MessageBox.Show("SAVE SUCCESSFULLY", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                tbxCustname.Text = "";
                tbxContact.Text = "";
                tbxAddress.Text = "";
            }

        }

        private void customers_Load(object sender, EventArgs e)
        {
            main.maxId();
            lblInvoice.Text = "INVOICE ID: " + setandGet.maxId.ToString();

            main.getSales("select max(salesid) as general from tblsales", lblSales);
            main.getSales("select sum(subtotal) as general from tblsales", lblIncome);

            main.dgvView("select productid as 'ID', productname as 'Product List', unitprice as 'Price', unitstock as 'Stock Availble' from products order by productname asc", dgvProducts);
            main.cbxLoadCustomerName("select customername from customers", cbxCustName);
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            main.dgvView("select productid as 'ID',productname as 'Product List', unitprice as 'Price', unitstock as 'Stock Availble' from products where productname like '" + tbxSearch.Text + "%'", dgvProducts);
            if (tbxSearch.Text == "")
            {
                main.dgvView("select productid as 'ID', productname as 'Product List', unitprice as 'Price', unitstock as 'Stock Availble' from products order by productname asc", dgvProducts);
            }
        }
        private void cbxCustname_SelectedIndexChanged(object sender, EventArgs e)
        {
            main.getCustomerId("select customerId from customers where customername = '" + cbxCustName.SelectedItem + "'", tbxCustomerId);
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //display data in textbox once the cell is click
            if (dgvProducts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                btnUpdateQty.Show();
                btnAddDgv.Show();

                dgvProducts.CurrentRow.Selected = true;

                lblId.Text = dgvProducts.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                tbxProduct.Text = dgvProducts.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                tbxPrice.Text = dgvProducts.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                lblStock.Text = dgvProducts.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();

                tbxAmount.Text = "";
                tbxQty.Text = "";
            }
        }
        private void btnAddDgv_Click(object sender, EventArgs e)
        {
            if (tbxProduct.Text == "" || tbxPrice.Text == "")
            {
                MessageBox.Show("SELECT PRODUCT FIRST");
            }
            else if (tbxQty.Text == "")
            {
                MessageBox.Show("PLEASE INPUT QUANTITY");
            }
            else
            {
                int quantity = int.Parse(tbxQty.Text);
                int availableStock = int.Parse(lblStock.Text);

                if (quantity > availableStock)
                {
                    MessageBox.Show("ORDER QUANTITY EXCEEDS AVAILABLE STOCK", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    decimal result = decimal.Parse(tbxQty.Text) * decimal.Parse(tbxPrice.Text);
                    tbxAmount.Text = result.ToString();

                    main.addRow(lblId.Text, tbxProduct.Text, tbxPrice.Text, tbxQty.Text, lblStock.Text, tbxAmount.Text, dgvInvoice);

                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                    buttonColumn.HeaderText = "Action";
                    buttonColumn.Text = "Delete";
                    buttonColumn.Name = "btnDelete";
                    buttonColumn.UseColumnTextForButtonValue = true;
                    dgvInvoice.Columns.Add(buttonColumn);

                    lblTotal.Text = "0";
                    for (int i = 0; i < dgvInvoice.Rows.Count; i++)
                    {
                        lblTotal.Text = Convert.ToString(decimal.Parse(lblTotal.Text) + decimal.Parse(dgvInvoice.Rows[i].Cells[5].Value.ToString()));
                    }

                    tbxQty.Text = "";
                }
            }
        }


        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAddDgv.Hide();
            btnUpdateQty.Show();
            DataGridViewRow row = dgvInvoice.Rows[indexRow];

            tbxProduct.Text = row.Cells[1].Value.ToString();
            tbxPrice.Text = row.Cells[2].Value.ToString();
            tbxQty.Text = row.Cells[3].Value.ToString();
            tbxAmount.Text = row.Cells[4].Value.ToString();
        }

        private void dgvInvoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvInvoice.Columns[6].Index && e.RowIndex >= 0)
            {
                dgvInvoice.Rows.RemoveAt(e.RowIndex);
                tbxProduct.Text = "";
                tbxQty.Text = "";
                tbxPrice.Text = "";
                tbxAmount.Text = "";
            }

            lblTotal.Text = "0";
            for (int i = 0; i < dgvInvoice.Rows.Count; i++)
            {
                lblTotal.Text = Convert.ToString(decimal.Parse(lblTotal.Text) + decimal.Parse(dgvInvoice.Rows[i].Cells[5].Value.ToString()));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbxQty.Text == "")
            {
                MessageBox.Show("PLEASE INPUT QUANTITY");
            }
            else if (dgvInvoice.RowCount == 0)
            {
                MessageBox.Show("ADD ORDER FIRST", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                int quantity = int.Parse(tbxQty.Text);
                int availableStock = int.Parse(lblStock.Text);

                if (quantity > availableStock)
                {
                    MessageBox.Show("ORDER QUANTITY EXCEEDS AVAILABLE STOCK", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal result = decimal.Parse(tbxQty.Text) * decimal.Parse(tbxPrice.Text);
                tbxAmount.Text = result.ToString();
                DataGridViewRow updateData = dgvInvoice.Rows[indexRow];

                updateData.Cells[1].Value = tbxProduct.Text;
                updateData.Cells[2].Value = tbxPrice.Text;
                updateData.Cells[3].Value = tbxQty.Text;
                updateData.Cells[5].Value = tbxAmount.Text;

                lblTotal.Text = "0";
                for (int i = 0; i < dgvInvoice.Rows.Count; i++)
                {
                    lblTotal.Text = Convert.ToString(decimal.Parse(lblTotal.Text) + decimal.Parse(dgvInvoice.Rows[i].Cells[5].Value.ToString()));
                }
                tbxQty.Text = "";
            }
        }


        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            try {
                double amountTendered, total;
                amountTendered = double.Parse(tbxAmountTendered.Text);
                total = double.Parse(lblTotal.Text);

                if (tbxCustomerId.Text == "")
                {
                    MessageBox.Show("SELECT CUSTOMER FIRST", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (dgvInvoice.RowCount == 0)
                {
                    MessageBox.Show("ADD ORDER FIRST", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (tbxAmountTendered.Text == "0" || tbxAmountTendered.Text == "")
                {
                    MessageBox.Show("INPUT CASH TO CONFIRM", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (amountTendered < total)
                {
                    MessageBox.Show("NOT ENOUGH CASH, PLEASE TRY AGAIN", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    for (int i = 0; i < dgvInvoice.Rows.Count; i++)
                    {
                        main.saveData("insert into tblsales (invoiceid,productid,unitprice,quantity,subtotal) values ('"
                            + setandGet.maxId + "','"
                            + dgvInvoice.Rows[i].Cells[0].Value + "','"
                            + dgvInvoice.Rows[i].Cells[2].Value + "','"
                            + dgvInvoice.Rows[i].Cells[3].Value + "','"
                            + dgvInvoice.Rows[i].Cells[5].Value + "')");
                    }
                    foreach (DataGridViewRow row in dgvInvoice.Rows)
                    {
                        int prodId = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                        int result = Convert.ToInt32(row.Cells["Stock Availble"].Value) - Convert.ToInt32(row.Cells["Qty"].Value);

                        main.saveData("update products set unitstock = '" + result + "' where productid = '" + prodId + "'");


                    }
                    main.saveData("insert into tblinvoice (customerid,totalamount,daterecorded,amounttendered,userid) values ('" + cbxCustName.SelectedItem + "','" + lblTotal.Text + "','" + lblDateTime.Text + "','" + tbxAmountTendered.Text + "', '" + setandGet.userid + "')");
                    main.getSales("select max(salesid) as general from tblsales", lblSales);
                    main.getSales("select sum(subtotal) as general from tblsales", lblIncome);


                    MessageBox.Show("ORDER CONFIRMED", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    main.dgvView("select productid as 'ID', productname as 'Product List', unitprice as 'Price', unitstock as 'Stock Availble' from products order by productname asc", dgvProducts);
                    dgvInvoice.Rows.Clear();
                    tbxAmountTendered.Text = "0";
                    lblChang.Text = "";
                    lblTotal.Text = "";
                }
            }
            catch(Exception)
            {

            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            this.lblDateTime.Text = dt.ToString("G");
        }

        private void tbxAmountTendered_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal result = decimal.Parse(tbxAmountTendered.Text) - decimal.Parse(lblTotal.Text);
                lblChang.Text = result.ToString();
            }
            catch(Exception)
            {

            }
        }

        private void tbxContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar);
        }

        private void tbxQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbxAmountTendered_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbxQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }



        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (cbxCustName.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to delete.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string selectedCustomer = cbxCustName.SelectedItem.ToString();

                // Perform the soft delete by updating the IsDeleted flag
                main.saveData($"UPDATE customers SET IsDeleted = 1 WHERE customerName = '{selectedCustomer}'");

                // Update the combo box and clear the form
                cbxCustName.Items.Clear();
                main.cbxLoadCustomerName("SELECT customername FROM customers WHERE IsDeleted = 0", cbxCustName);

                tbxCustname.Text = "";
                tbxContact.Text = "";
                tbxAddress.Text = "";

                MessageBox.Show("Customer marked as deleted successfully.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbxContact_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbxSearch_Enter(object sender, EventArgs e)
        {
            if (tbxSearch.Text == "SEARCH PRODUCT")
            {
                tbxSearch.Text = "";
                tbxSearch.ForeColor = Color.Black;
            }
        }
    }
}
