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
    public partial class Formsearch : Form
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
        SQLiteConnection sc = new SQLiteConnection(@"Data Source=" + Application.StartupPath + @"\IDB.db; Version=3");
        SQLiteCommand scm = new SQLiteCommand();
        SQLiteDataAdapter sda = new SQLiteDataAdapter();
        public Formsearch()
        {
            InitializeComponent();
        }

        private void Formsearch_Load(object sender, EventArgs e)
        {
            sc.Open();
            fillgrid("select * from IDB_database");
        }
        public void fillgrid(string s = "select * from IDB_database ")
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
            cr = (CurrencyManager)this.BindingContext[ds, "t1"];
        }
        private void txtname_TextChanged(object sender, EventArgs e)
        {
            string a;
            a = "select * from IDB_database where name like '%" + txtname.Text + "%'";
            fillgrid(a);
        }

        private void txtssn_TextChanged(object sender, EventArgs e)
        {
            string a;
            a = "select * from IDB_database where SSN like '%" + txtssn.Text + "%'";
            fillgrid(a);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtrname.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtrssn.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtrviolationtype.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtryear.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtrmonth.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtrday.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtrinformation.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            lblcode.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (btnedith.Text == "ویرایش")
            {
                txtrname.ReadOnly = false;
                txtrssn.ReadOnly = false;
                txtrday.ReadOnly = false;
                txtrmonth.ReadOnly = false;
                txtryear.ReadOnly = false;
                txtrinformation.ReadOnly = false;
                txtrviolationtype.ReadOnly = false;
                btnedith.Text = "ثبت";
            }
            else if(btnedith.Text=="ثبت")
            {
                txtrname.ReadOnly = true;
                txtrssn.ReadOnly = true;
                txtrday.ReadOnly = true;
                txtrmonth.ReadOnly = true;
                txtryear.ReadOnly = true;
                txtrinformation.ReadOnly = true;
                txtrviolationtype.ReadOnly = true;
                btnedith.Text = "ویرایش";
                cr = (CurrencyManager)this.BindingContext[ds, "t1"];
                //SqlCommand c3 = new SqlCommand();
                scm.CommandText = "update IDB_database set name=@p1,SSN=@p2,violationtype=@p3,year=@p4,month=@p5,day=@p6,info=@p7 where code=@p8";
                scm.Parameters.AddWithValue("p1", txtrname.Text);
                scm.Parameters.AddWithValue("p2", txtrssn.Text);
                scm.Parameters.AddWithValue("p3", txtrviolationtype.Text);
                scm.Parameters.AddWithValue("p4", txtryear.Text);
                scm.Parameters.AddWithValue("p5", txtrmonth.Text);
                scm.Parameters.AddWithValue("p6", txtrday.Text);
                scm.Parameters.AddWithValue("p7", txtrinformation.Text);
                scm.Parameters.AddWithValue("p8", lblcode.Text);
                scm.Connection = sc;
                sda.SelectCommand = scm;
                //sc.Open();
                scm.ExecuteNonQuery();
                fillgrid();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogresulat = MessageBox.Show("آیا مطمئن هستید","هشدار",MessageBoxButtons.YesNo);
                if(dialogresulat==DialogResult.Yes)
                {
                    cr = (CurrencyManager)this.BindingContext[ds, "t1"];
                    scm.CommandText = "delete from IDB_database where code='" + lblcode.Text+"'";
                    scm.Connection = sc;
                    sda.SelectCommand = scm;
                    scm.ExecuteNonQuery();
                    fillgrid();
                }
            }
            catch
            {
                MessageBox.Show("invalid syntax");
            }
        }
    }
}
