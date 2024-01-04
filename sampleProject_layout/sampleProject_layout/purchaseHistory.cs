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
    public partial class purchaseHistory : Form
    {
        public purchaseHistory()
        {
            InitializeComponent();
        }

        private void purchaseHistory_Load(object sender, EventArgs e)
        {
            main.dgvView("SELECT tblinvoice.invoiceId as 'Invoice ID', products.productName as 'Product', tblsales.quantity as 'Quantity', products.unitPrice as 'Price',tblsales.subTotal as 'Sub Total', tblinvoice.totalAmount as 'Total Amount', tblinvoice.amountTendered as 'Tendered', tblinvoice.dateRecorded as 'Date Record' from tblsales INNER JOIN tblinvoice ON tblinvoice.invoiceId=tblsales.invoiceId INNER JOIN products ON products.productId=tblsales.productId", dgvHistory);
        }
    }
}
