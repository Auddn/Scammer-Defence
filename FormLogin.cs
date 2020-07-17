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
    public partial class FormLogin : Form
    {
        private SqlConnection sqlconn = null;
        private string constr = "SERVER = 127.0.0.1,1433; DATABASE = Project;" + "UID = sa2; PASSWORD = 1234;"; // localhost에서 클라우드 서비스로 바꿀 것

        bool LoginMode = false;
        private bool mouseDown;
        private Point lastLocation;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void FormLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void FormLogin_MouseUp(object sender, MouseEventArgs e)
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

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            this.Visible = false;             // 추가
            FormSignup showForm2 = new FormSignup();
            showForm2.ShowDialog();
        }

        internal void showDialog()
        {
            this.Visible = true;
        }

        private void buttonSignin_Click(object sender, EventArgs e)
        {
            if (swithchManagerMode.Checked == true)
            {
                DataSet ds = new DataSet();

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Administrator WHERE adminid = '" + InputId.Text + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(ds, "Login");
                }

                string pw = null;
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    pw = row["adminps"].ToString();
                }
                if (pw.Trim().Equals(InputPw.Text.ToString()))
                {

                    MessageBox.Show("로그인 성공!");
                }
                else
                {
                    MessageBox.Show("입력하신 아이디나 비밀번호가 일치하지 않습니다.");
                    return;
                }

                this.Visible = false;
                FormManager formManager = new FormManager();
                formManager.ShowDialog();
            }
            else // 관리자 모드가 아닐때
            {
                DataSet ds = new DataSet();

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string sql = "SELECT * FROM customer WHERE custid = '" + InputId.Text + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(ds, "Login");
                }

                string pw = null;
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    pw = row["custps"].ToString();
                }
                if (pw.Trim().Equals(InputPw.Text.ToString()))
                {

                    MessageBox.Show("로그인 성공!");
                }
                else
                {
                    MessageBox.Show("입력하신 아이디나 비밀번호가 일치하지 않습니다.");
                    return;
                }

                this.Visible = false;
                FormMain formMain = new FormMain();
                formMain.ShowDialog();
            }
        }

    }
}
