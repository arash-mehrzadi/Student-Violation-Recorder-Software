using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace uniproject__SVR_
{
    public partial class Form1 : Form
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
        private IconButton currentBtn;
        private Panel rightBoarderBtn;
        private Form currentChildForm;
        public Form1()
        {
            InitializeComponent();
            rightBoarderBtn = new Panel();
            rightBoarderBtn.Size = new Size(7, 45);
            panelMenu.Controls.Add(rightBoarderBtn);
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if(senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                rightBoarderBtn.BackColor = color;
                rightBoarderBtn.Location = new Point(0, currentBtn.Location.Y);
                rightBoarderBtn.Visible = true;
                rightBoarderBtn.BringToFront();
                stateicon.IconChar = currentBtn.IconChar;
                stateicon.IconColor = color;
                lblstate.Text = currentBtn.Text;
            }
        }
        private void DisableButton()
        {
            if(currentBtn != null)
            {
                currentBtn.BackColor = Color.Black;
                currentBtn.ForeColor = Color.Snow;
                currentBtn.TextAlign = ContentAlignment.MiddleRight;
                currentBtn.IconColor = Color.DarkRed;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
            }
        }
        private void OpenChildForm(Form childForm)
        {
            if(currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            paneldesktop.Controls.Add(childForm);
            paneldesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            addbutton.Enabled = false;
            registerbutton.Enabled = false;
            settingbutton.Enabled = false;
            supportbutton.Enabled = false;
            iconButton1.Enabled = false;
            sc.Open();
        }
        private void addbutton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new FormAdd());
        }

        private void registerbutton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new Formsearch());
        }

        private void settingbutton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new Formsetting());
        }

        private void supportbutton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new Formcontact());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DisableButton();
            currentChildForm.Close();
            rightBoarderBtn.Visible = false;
            stateicon.IconChar = IconChar.Home;
            stateicon.IconColor = Color.DarkRed;
            lblstate.Text = "صفحه اصلی";
        }
        [DllImport("user32.DLL")]//, EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL")]//, EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void titlebar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        public string countercode(string s)
        {
            string counter;
            SQLiteCommand cma1 = new SQLiteCommand();
            cma1.CommandText = s;
            cma1.Connection = sc;
            counter = cma1.ExecuteScalar().ToString();
            return counter;
        }
        private void btnregister_Click(object sender, EventArgs e)
        {
            string countstring;
            string pass;
            countstring = "select pass from password";
            pass = countercode(countstring);
            if(btnregister.Text=="فعال سازی")
            {
                if (pass == txtpass.Text)
                {
                    addbutton.Enabled = true;
                    registerbutton.Enabled = true;
                    settingbutton.Enabled = true;
                    supportbutton.Enabled = true;
                    iconButton1.Enabled = true;
                    btnregister.Text = "غیرفعال سازی";
                    btnregister.IconColor = Color.DarkRed;
                    txtpass.Text = "";
                }
                else
                {
                    MessageBox.Show("wrong password!");
                }
            }
            else
            {
                addbutton.Enabled = false;
                registerbutton.Enabled = false;
                settingbutton.Enabled = false;
                supportbutton.Enabled = false;
                iconButton1.Enabled = false;
                btnregister.Text = "فعال سازی";
                btnregister.IconColor = Color.Lime;
                txtpass.Text = "";
                DisableButton();
                currentChildForm.Close();
                rightBoarderBtn.Visible = false;
                stateicon.IconChar = IconChar.Home;
                stateicon.IconColor = Color.DarkRed;
                lblstate.Text = "صفحه اصلی";
            }
        }
    }
}
