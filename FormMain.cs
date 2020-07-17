using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scammer_Defance
{
    public partial class FormMain : Form
    {
        private SqlConnection sqlconn = null;
        private string constr = "SERVER = 127.0.0.1,1433; DATABASE = Project;" + "UID = sa2; PASSWORD = 1234;";

        private bool mouseDown;
        private Point lastLocation;

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location; 
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ButtonClose_MouseEnter(object sender, EventArgs e)
        {
            buttonClose.Image = Scammer_Defance.Properties.Resources.buttonCloseMouseUp;
        }

        private void ButtonClose_MouseLeave(object sender, EventArgs e)
        {
            buttonClose.Image = Scammer_Defance.Properties.Resources.buttonClose2;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlconn = new SqlConnection(constr);
                sqlconn.Open();
                
                DataSet ds = new DataSet();

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string sql = "SELECT Contents FROM Info WHERE CallNumber = '" + CallNumber.Text + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(ds, "Info");
                }
                string d = null;
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    d = row["contents"].ToString();
                }
                guna2TextBox1.Text = d;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            FormLogin login = new FormLogin();
            login.showDialog();
        }
    }
}
