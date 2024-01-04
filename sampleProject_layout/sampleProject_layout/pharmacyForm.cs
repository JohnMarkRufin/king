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
    public partial class pharmacyForm : Form
    {
        bool drag = false;
        Point start = new Point(0, 0);
        public pharmacyForm()
        {
            InitializeComponent();
            timer1.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "INVENTORY";
            panel1.BackColor = Color.FromArgb(215, 228, 242);
            main.AddForm(new inventory(), mainPanel);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "CUSTOMER";
            panel1.BackColor = Color.FromArgb(215, 228, 242);
            main.AddForm(new customers(), mainPanel);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("ARE YOU SURE YOU WANT TO SIGN OUT?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.Yes)
            {
                new signin().Show();
                Visible = false;
            }
        }

        private void pharmacyForm_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Welcome Back \n" + setandGet.lastName + ", " + setandGet.firstName + " !";
            panel1.BackColor = Color.FromArgb(215, 228, 242);
            main.AddForm(new inventory(), mainPanel);
        }

        

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "PURCHASE HISTORY";
            panel1.BackColor = Color.FromArgb(215, 228, 242);
            main.AddForm(new purchaseHistory(), mainPanel);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start.X, p.Y - start.Y);
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start = new Point(e.X, e.Y);
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            this.lblDateTime.Text = dt.ToString("G");
        }
    }
}
