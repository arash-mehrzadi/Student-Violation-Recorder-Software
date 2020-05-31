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
    public partial class FormAdd : Form
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
        public FormAdd()
        {
            InitializeComponent();
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            string violationoption = "select * from violation_option";
            scm.CommandText = violationoption;
            scm.Connection = sc;
            sda.SelectCommand = scm;
            ds.Clear();
            sda.Fill(ds, "T1");
            //cr = (CurrencyManager)this.BindingContext[ds, "t1"];
            //sc.Open();
            //string result = scm.ExecuteScalar().ToString();
            fillgrid(violationoption);
        }
        public void fillgrid(string s = "select * from IDB_database")
        {
            //cmd1.CommandText = s;
            scm.CommandText = s;
            //cmd1.Connection = conn;
            scm.Connection = sc;
            //da.SelectCommand = cmd1;
            sda.SelectCommand = scm;
            ds.Clear();
            sda.Fill(ds, "T1");
            dataGridView1.DataBindings.Clear();
            dataGridView1.DataBindings.Add("datasource", ds, "T1");
            //txtviolationsearch.DataBindings.Clear();
            //txtviolationsearch.DataBindings.Add("text", ds, "T1.violationlist");
            cr = (CurrencyManager)this.BindingContext[ds, "t1"];
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
        public int countercode(string s)
        {
            string counter;
            int output;
            SQLiteCommand cma1 = new SQLiteCommand();
            cma1.CommandText = s;
            cma1.Connection = sc;
            counter = cma1.ExecuteScalar().ToString();
            output = Convert.ToInt32(counter);
            return output;
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            sc = new SQLiteConnection(@"Data Source=" + Application.StartupPath + @"\IDB.db; Version=3");
            sc.Open();
            string s;
            string countstring;
            int counter;
            countstring = "select count(*) from IDB_database";
            counter = countercode(countstring);
            //MessageBox.Show(counter.ToString());
            s = "insert into IDB_database values ('" + txtfirstname.Text +" "+txtlastname.Text+"','" + txtssn.Text + "','" + txtviolationsearch.Text + "','" + txtyear.Text + "','" + txtmonth.Text + "','" + txtday.Text + "','" +txtexplanation.Text + "','" +counter+ "')";
            adddata(s);
            txtfirstname.Text = "";
            txtlastname.Text = "";
            txtssn.Text = "";
            txtviolationsearch.Text = "";
            txtday.Text = "";
            txtmonth.Text = "";
            txtyear.Text = "";
            txtexplanation.Text = "";
            //string code;
            //code = cr.Count.ToString();
            //MessageBox.Show(code);
        }

        private void txtviolationsearch_TextChanged(object sender, EventArgs e)
        {
            sc = new SQLiteConnection(@"Data Source=" + Application.StartupPath + @"\violationoption_database.db; Version=3");
            sc.Open();
            string a;
            a = "select * From violation_option where violationlist like '%" + txtviolationsearch.Text + "%'";
            fillgrid(a);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtviolationsearch.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
