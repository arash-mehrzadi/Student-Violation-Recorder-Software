using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
namespace uniproject__SVR_
{
    public partial class Formsetting : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        CurrencyManager cr;
        SqlCommand c1 = new SqlCommand();
        DataTable tb = new DataTable();
        int state;
        string asad;
        SQLiteConnection sc = new SQLiteConnection(@"Data Source=" + Application.StartupPath + @"\violationoption_database.db; Version=3");
        SQLiteCommand scm = new SQLiteCommand();
        SQLiteDataAdapter sda = new SQLiteDataAdapter();
        public Formsetting()
        {
            InitializeComponent();
        }

        private void Formsetting_Load(object sender, EventArgs e)
        {
            sc.Open();
        }
        public void adddata(string s)
        {
            //SqlCommand c1 = new SqlCommand();
            SQLiteCommand cma1 = new SQLiteCommand();
            //c1.CommandText = s;
            cma1.CommandText = s;
            //c1.Connection = conn;
            cma1.Connection = sc;
            //c1.ExecuteNonQuery();
            cma1.ExecuteNonQuery();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string s;
                s = "insert into violation_option values ('" + txtviolation.Text + "')";
                adddata(s);
                txtviolation.Text = "";
            }
            catch
            {
                MessageBox.Show("ورودی تکراری می باشد ");
                txtviolation.Text = "";
            }
        }
        public string getpass(string s)
        {
            string counter;
            SQLiteCommand cma1 = new SQLiteCommand();
            cma1.CommandText = s;
            cma1.Connection = sc;
            counter = cma1.ExecuteScalar().ToString();
            return counter;
        }

        private void btnpassword_Click(object sender, EventArgs e)
        {
            string countstring,updatestring;
            string pass;
            countstring = "select pass from password";
            pass = getpass(countstring);
            if(pass == txtpass.Text && txtnewpas.Text == txtnewpasstwo.Text)
            {
                updatestring = scm.CommandText = "update password set pass='"+txtnewpas.Text+"'";
                SQLiteCommand cma1 = new SQLiteCommand();
                cma1.CommandText = updatestring;
                cma1.Connection = sc;
                cma1.ExecuteNonQuery();
                txtpass.Text = "";
                txtnewpasstwo.Text = "";
                txtnewpas.Text = "";
            }
            else
            {
                MessageBox.Show("رمزعبور یا تکرار رمز جدید اشتباه است");
                txtpass.Text = "";
                txtnewpasstwo.Text = "";
                txtnewpas.Text = "";
            }
        }

        private void btndeleteviolation_Click(object sender, EventArgs e)
        {
            try
            {
                string s;
                s = "delete from violation_option where violationlist='" + txtdeleteviolation.Text + "'";
                adddata(s);
                txtdeleteviolation.Text = "";
            }
            catch
            {
                MessageBox.Show("ورودی معتبر نیست ");
                txtviolation.Text = "";
            }
        }
    }
}
