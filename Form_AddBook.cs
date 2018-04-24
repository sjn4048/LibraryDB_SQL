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
    public partial class Form_AddBook : Form
    {
        SqlConnection sqlCnt;
        public Form_AddBook()
        {
            
        }

        public Form_AddBook(SqlConnection sqlCnt)
        {
            this.sqlCnt = sqlCnt;
            InitializeComponent();
        }

        private void commit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                var books_info = richTextBox1.Text.Split('\n');
                var command = sqlCnt.CreateCommand();
                command.CommandText = "INSERT INTO book_info (book_name, author, price, left_num) VALUES ";
                foreach (var book in books_info)
                {
                    var info = book.Split('&');
                    if (book != books_info.Last())
                        command.CommandText += $"('{info[0]}','{info[1]}', {info[2]}, {info[3]}), ";
                    else
                        command.CommandText += $"('{info[0]}','{info[1]}', {info[2]}, {info[3]});";
                }
                command.ExecuteNonQuery();
                MessageBox.Show("Success.");
                this.richTextBox1.Text = string.Empty;
            }
            catch
            {
                MessageBox.Show("Error Occurred. Please Retry.");
            }
        }

        private void Form_AddBook_Load(object sender, EventArgs e)
        {
            
        }
    }
}
