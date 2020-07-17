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
    public partial class FormManager : Form
    {
        string eNum, numid;
        private SqlConnection sqlconn = null;
        private string constr = "SERVER = 127.0.0.1,1433; DATABASE = Project;" + "UID = sa2; PASSWORD = 1234;";

        private bool mouseDown;
        private Point lastLocation;

        public FormManager()
        {
            InitializeComponent();

            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                string sql = "SELECT * FROM Info";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(ds, "Info");
            }

            TempGridView.DataSource = ds.Tables[0];
        }

        private void FormManager_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void FormManager_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void FormManager_MouseUp(object sender, MouseEventArgs e)
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
                    string sql = "SELECT numid,Contents,CallNumber,Date FROM Info WHERE CallNumber = '" + FindNumber.Text + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(ds, "findNumber");
                }
                string n = null;
                string d = null;
                string c = null;
                string id = null;

                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    id = row["numid"].ToString();
                    eNum = row["CallNumber"].ToString();
                    d = row["Date"].ToString();
                    n = row["CallNumber"].ToString();
                    c = row["Contents"].ToString();
                }
                PhoneNumber.Text = n;
                Date.Text = d;
                Contents.Text = c;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TempGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            eNum = TempGridView.Rows[e.RowIndex].Cells["CallNumber"].Value.ToString();
            numid = TempGridView.Rows[e.RowIndex].Cells["numid"].Value.ToString();
            PhoneNumber.Text = TempGridView.Rows[e.RowIndex].Cells["CallNumber"].Value.ToString();
            Date.Text = TempGridView.Rows[e.RowIndex].Cells["Date"].Value.ToString();
            Contents.Text = TempGridView.Rows[e.RowIndex].Cells["Contents"].Value.ToString();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand();

                conn.Open();
                command.Connection = conn;
                command.CommandText = "DELETE FROM Info WHERE numid = " + numid;
                command.ExecuteNonQuery();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.Connection = conn;
                command.CommandText = "INSERT INTO Info(CallNumber, Date, Contents) VALUES('" + PhoneNumber.Text + "','" + Date.Text + "', '" + Contents.Text + "');";
                command.ExecuteNonQuery();
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            FormLogin login = new FormLogin();
            login.showDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string n = PhoneNumber.Text;
            string c = Contents.Text;
            string d = Date.Text;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.Connection = conn;
                command.CommandText = "UPDATE Info SET CallNumber = '" + n + "', Date = '"+ d +"', Contents = '"+c+"' WHERE numid = '" +numid+"'";
                command.ExecuteNonQuery();

                conn.Close();
                
            }
        }
    }
}
