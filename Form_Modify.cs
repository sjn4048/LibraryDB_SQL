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
    public partial class Form_Modify : Form
    {
        string ID;
        SqlConnection sqlCnt;
        public Form_Modify()
        {
            InitializeComponent();
        }

        public Form_Modify(SqlConnection sqlcnt, string id, string name, string author, string price, string left_num)
        {
            InitializeComponent();
            name_textbox.Text = name;
            author_textbox.Text = author;
            price_textbox.Text = price;
            left_textbox.Text = left_num;
            ID = id;
            sqlCnt = sqlcnt;
        }

        private void Form_Modify_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var command = sqlCnt.CreateCommand();
            command.CommandText = $@"UPDATE book_info SET book_name='{name_textbox.Text}', author='{author_textbox.Text}', price={price_textbox.Text}, left_num={left_textbox.Text} WHERE book_id={ID}";
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Success.");
                this.Close();
                return;
            }
            catch
            {
                MessageBox.Show("Error Occurred.");
            }
        }
    }
}
