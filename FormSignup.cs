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
    public partial class FormSignup : Form
    {
        private SqlConnection sqlconn = null;
        private string constr = "SERVER = 127.0.0.1,1433; DATABASE = Project;" + "UID = sa2; PASSWORD = 1234;";

        private bool mouseDown;
        private Point lastLocation;

        public FormSignup()
        {
            InitializeComponent();
        }

        private void FormSignup_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void FormSignup_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void FormSignup_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            buttonClose.Image = Scammer_Defance.Properties.Resources.buttonCloseMouseUp;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            buttonClose.Image = Scammer_Defance.Properties.Resources.buttonClose2;
        }

        private void buttonSignupin_Click(object sender, EventArgs e)
        {

            if ((textBoxPw.Text != textBoxCf.Text))
            {
                labelPw.Text = "The value is different from the confirm";
                labelCf.Text = "The value is different from the password";
                textBoxPw.Text = null;
                textBoxCf.Text = null;
            }
            else if (textBoxId.Text == "")
            {
                labelId.Text = "Please enter your username";
            }

            else if (textBoxPw.Text == "")
            {
                labelPw.Text = "Please enter your password";
            }

            else if (textBoxCf.Text == "")
            {
                labelPw.Text = "Please enter your password";
            }
            else
            {
                try
                {
                    sqlconn = new SqlConnection(constr);
                    sqlconn.Open();

                    DataSet ds = new DataSet();

                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.Open();
                        string sql = "INSERT INTO customer VALUES ('"+textBoxId.Text+"', '"+textBoxPw.Text+"')";
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                        adapter.Fill(ds, "enroll");

                    }
                    MessageBox.Show("등록 성공");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                this.Visible = false;
                FormLogin showFormLogin = new FormLogin();
                showFormLogin.showDialog();
            }
            
        }
 
        internal void ShowDialog()
        {
            this.Visible = true;
        }
    }
}
