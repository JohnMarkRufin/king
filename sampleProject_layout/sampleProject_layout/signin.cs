using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sampleProject_layout
{
    public partial class signin : Form
    {
        public signin()
        {
            InitializeComponent();
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (tbxUser.Text == "USERNAME")
            {
                tbxUser.Text = "";
                tbxUser.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (tbxUser.Text == "")
            {
                tbxUser.Text = "USERNAME";
                tbxUser.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (tbxPassword.Text == "PASSWORD")
            {
                tbxPassword.Text = "";
                tbxPassword.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (tbxPassword.Text == "")
            {
                tbxPassword.Text = "PASSWORD";
                tbxPassword.ForeColor = Color.Silver;
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            checker.chkr = false;
            main.login(tbxUser.Text, tbxPassword.Text);

            if (checker.chkr == true)
            {
                new pharmacyForm().Show();
                Visible = false;
            }
            else
            {
                MessageBox.Show("INCORRECT USERNAME OR PASSWORD", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void label4_Click(object sender, EventArgs e)
        {
            new signup().Show();
            Visible = false;
        }

        private void showPassword_Click(object sender, EventArgs e)
        {
            tbxPassword.UseSystemPasswordChar = false;
            hidePassword.Visible = true;
            showPassword.Visible = false;
        }

        private void hidePassword_Click(object sender, EventArgs e)
        {
            tbxPassword.UseSystemPasswordChar = true;
            showPassword.Visible = true;
            hidePassword.Visible = false;
        }

        private void tbxUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
