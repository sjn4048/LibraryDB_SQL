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
    public partial class Form_Manage : Form
    {
        SqlConnection sql_cnt;
        string Card_ID;

        Label[] sernum;
        Label[] book_name;
        Label[] lend_date;
        Label[] back_date;
        Button[] back_btn;

        const int MAX_ITEM_NUM = 10;
        const int y_step = 30;

        public Form_Manage(SqlConnection sql_cnt, string id)
        {
            this.sql_cnt = sql_cnt;
            this.Card_ID = id;
            InitializeComponent();
            Dynamic_draw();
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            InitializeBtn();
            string keyword = textBox1.Text.Trim();
            int item_num = 0;
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                sernum[i].Text = string.Empty;
                book_name[i].Text = string.Empty;
                lend_date[i].Text = string.Empty;
                back_date[i].Text = string.Empty;
            }
            SqlCommand command = sql_cnt.CreateCommand();
            command.CommandText = $@"SELECT lend_list.*, book_info.* FROM lend_list JOIN book_info ON lend_list.book_id = book_info.book_id WHERE lend_list.reader_id={Card_ID};";
            var reader = command.ExecuteReader();
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                if (reader.Read() == false) break;
                sernum[i].Text = reader["sernum"].ToString();
                book_name[i].Text = reader["book_name"].ToString();
                lend_date[i].Text = ((DateTime)reader["lend_date"]).ToString("yyyy-MM-dd");
                try
                {
                    back_date[i].Text = ((DateTime)reader["back_date"]).ToString("yyyy-MM-dd");
                }
                catch
                {
                    back_date[i].Text = "/";
                    back_btn[i].Enabled = true;
                }
                item_num++;
            }
            reader.Close();

            for (int i = 0; i < item_num; i++)
            {
                int j = i;
                back_btn[i].Show();

                back_btn[i].Click += (s, arg) =>
                {
                    try
                    {
                        var update_command = sql_cnt.CreateCommand();
                        update_command.CommandText = $@"UPDATE lend_list SET back_date='{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE sernum='{sernum[j].Text}' and reader_id={Card_ID};"
                        + $@"UPDATE book_info SET left_num=left_num+1 WHERE book_name='{book_name[j].Text}';";
                        update_command.ExecuteNonQuery();
                        MessageBox.Show("Success.");
                        search_btn.PerformClick();
                    }
                    catch
                    {
                        MessageBox.Show("Error Occured.");
                    }
                    return;
                };
            }
        }

        private void Dynamic_draw()
        {
            sernum = new Label[MAX_ITEM_NUM];
            book_name = new Label[MAX_ITEM_NUM];
            lend_date = new Label[MAX_ITEM_NUM];
            back_date = new Label[MAX_ITEM_NUM];

            back_btn = new Button[MAX_ITEM_NUM];
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                sernum[i] = new Label();
                book_name[i] = new Label();
                lend_date[i] = new Label();
                back_date[i] = new Label();
                groupBox1.Controls.Add(sernum[i]);
                groupBox1.Controls.Add(book_name[i]);
                groupBox1.Controls.Add(lend_date[i]);
                groupBox1.Controls.Add(back_date[i]);
                sernum[i].AutoSize = true;
                book_name[i].AutoSize = true;
                lend_date[i].AutoSize = true;
                back_date[i].AutoSize = true;
                sernum[i].Location = new Point(20, i * y_step + 40);
                book_name[i].Location = new Point(100, i * y_step + 40);
                lend_date[i].Location = new Point(250, i * y_step + 40);
                back_date[i].Location = new Point(350, i * y_step + 40);
            }
        }

        private void InitializeBtn() // Initialize the return, modify, lend button
        {
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                back_btn[i]?.Dispose();
                back_btn[i] = new Button();
                back_btn[i].Hide();
                groupBox1.Controls.Add(back_btn[i]);
                back_btn[i].Location = new Point(450, i * y_step + 35);
                back_btn[i].Text = "归还";
                back_btn[i].Enabled = false;
                back_btn[i].Size = new Size(50, 25);
            }
        }
    }
}
