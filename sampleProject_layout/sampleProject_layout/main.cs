using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sampleProject_layout
{
    class main
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "database=127.0.0.1;port=3306;username=root;password=;database=pharmacydb";
            MySqlConnection con = new MySqlConnection(sql);

            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySql Connection! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }



        public static void saveData(string sql)
        {
            string query = sql;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error!!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }
        public static void dgvView(string query, DataGridView dgv)
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            con.Close();
        }
        public static void cbxLoad(string query)
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);

            MySqlDataReader result = cmd.ExecuteReader();
            while (result.Read())
            {
                setandGet.categoryId = Convert.ToInt32(result["categoryid"].ToString());
               
            }
        }
        public static void signup(string fname, string lname, string address, string username, string password)
        {
            string query = "insert into users (Firstname,Lastname,Contactno,Username,Password) values ('" + fname + "','" + lname + "','" + address + "','" + username + "','" + password + "')";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            try
            {
                MySqlDataReader read = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
        public static void login(string username, string password)
        {
            string query = "select * from users where username = '" + username + "' and password = '" + password + "'";
            MySqlConnection con = main.GetConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);

            try
            {
                MySqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    setandGet.userid = read.GetInt32(0);
                    setandGet.firstName = read.GetString(1);
                    setandGet.lastName = read.GetString(2);
                    checker.chkr = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
        public static void AddForm(Form f, Panel panel)
        {
            panel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            panel.Controls.Add(f);
            f.Show();
        }
        public static void cbxLoadCategory(string sql, ComboBox cbxCategory)
        {
                MySqlConnection con = GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    cbxCategory.Items.Add(rd["categoryname"].ToString());
                }
        }
        public static void cbxLoadCustomerName(string sql, ComboBox cbxCustomerName)
        {
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                cbxCustomerName.Items.Add(rd["customername"].ToString());
            }
        }
        public static void cbxLoadSupplierName(string sql, ComboBox cbxSupplier)
        {
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                cbxSupplier.Items.Add(rd["name"].ToString());
            }
        }
        public static void maxId()
        {
            string query = "select max(invoiceid) as  maxId from tblinvoice";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            try
            {
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (((rd["maxId"]).ToString()) == "")
                    {
                        setandGet.maxId = 1;
                    }
                    else
                    {
                        int idnum = int.Parse((rd["maxId"]).ToString());
                        setandGet.maxId = idnum + 1;
                    }
                }
                rd.Close();
            }
            catch(MySqlException)
            {

            }
        }
        public static void getCustomerId(string sql, TextBox tbxCustomerId)
        {
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                tbxCustomerId.Text = read["customerid"].ToString();
            }
        }
        public static void getSupplierId(string sql, TextBox tbxSupplierId)
        {
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                tbxSupplierId.Text = read["supplierid"].ToString();
            }
        }
        public static void addRow(string id, string product, string qty, string price, string stock, string amount,DataGridView dgv)
        {
            dgv.ColumnCount = 6;
            dgv.Columns[0].Name = "ID";
            dgv.Columns[1].Name = "Product";
            dgv.Columns[2].Name = "Price";
            dgv.Columns[3].Name = "Qty";
            dgv.Columns[4].Name = "Stock Availble";
            dgv.Columns[5].Name = "SubTotal";

            String[] row = { id, product, qty, price, stock, amount };
            dgv.Rows.Add(row);
        }
        public static void addRowSupplier(string product, string category,string qty, string price, string amount, DataGridView dgv)
        {
            dgv.ColumnCount = 5;
            dgv.Columns[0].Name = "Product";
            dgv.Columns[1].Name = "Category";
            dgv.Columns[2].Name = "Qty";
            dgv.Columns[3].Name = "Price";
            dgv.Columns[4].Name = "Sub Total";

            String[] row = {  product, category,qty, price, amount };
            dgv.Rows.Add(row);
        }
        public static void addRowSupplier2(string id,string product, string qty, string price, string amount, DataGridView dgv)
        {
            dgv.ColumnCount = 5;
            dgv.Columns[0].Name = "ID";
            dgv.Columns[1].Name = "Product";
            dgv.Columns[2].Name = "Qty";
            dgv.Columns[3].Name = "Price";
            dgv.Columns[4].Name = "Sub Total";

            String[] row = {id, product, qty, price, amount };
            dgv.Rows.Add(row);
        }
        public static void getSales(string sql, Label lblSales)
        {
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                lblSales.Text = read["general"].ToString();
            }
            con.Close();
        }
    }
    }
        class checker
    {
        public static Boolean chkr = false;
       }