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

namespace LibraryDB
{
    public partial class Form_login : Form
    {
        SqlConnection sqlConnection;

        bool IsRegister;
        public Form_login(SqlConnection sqlCnt, bool isRegister = false)
        {
            this.sqlConnection = sqlCnt;
            this.IsRegister = isRegister;
            InitializeComponent();
            if (isRegister)
            {
                this.Text = button1.Text = "Register";
                this.label3.Enabled = this.checkBox1.Enabled = false;
            }
        }

        private Form_login()
        {

        }

        private void Form_login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = sqlConnection.CreateCommand();
            if (this.IsRegister)
            {
                try
                {
                    command.CommandText = $@"INSERT INTO reader_card (reader_name, passwd) VALUES ('{username_textbox.Text}', '{pwd_textbox.Text}');";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Success.");
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Invalid information.");
                }
            }
            else if (checkBox1.Checked) // 管理员
            {
                command.CommandText = $@"SELECT * FROM administrator WHERE admin_name='{username_textbox.Text}' and passwd='{pwd_textbox.Text}'";
                var reader = command.ExecuteReader();
                if (reader.HasRows) //存在匹配的结果
                {
                    Form1 f1 = (Form1)this.Owner;
                    reader.Read();
                    f1.AdminPrivilege = true;
                    f1.Username = username_textbox.Text;
                    f1.Card_ID = int.Parse(reader["admin_id"].ToString());
                    MessageBox.Show("Login Success.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong username or password.");
                }
                reader.Close();
            }
            else // 普通用户
            {
                command.CommandText = $@"SELECT * FROM reader_card WHERE reader_name='{username_textbox.Text}' and passwd='{pwd_textbox.Text}'";
                var reader = command.ExecuteReader();
                if (reader.HasRows) //存在匹配的结果
                {
                    Form1 f1 = (Form1)this.Owner;
                    reader.Read();
                    f1.Card_ID = int.Parse(reader["reader_id"].ToString());
                    f1.Username = username_textbox.Text;
                    MessageBox.Show("Login Success.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong username or password.");
                }
                reader.Close();
            }
        }
    }
}
