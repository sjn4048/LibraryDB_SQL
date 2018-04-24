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
    public partial class Form_User : Form
    {
        SqlConnection sql_cnt;

        Label[] card_id;
        Label[] card_name;
        Label[] card_lendcount;
        Button[] delete_btn;

        const int MAX_ITEM_NUM = 10;
        const int y_step = 30;

        public Form_User(SqlConnection sql_cnt)
        {
            this.sql_cnt = sql_cnt;
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
                card_id[i].Text = string.Empty;
                card_name[i].Text = string.Empty;
                card_lendcount[i].Text = string.Empty;
            }
            SqlCommand command = sql_cnt.CreateCommand();
            command.CommandText = $@"SELECT * FROM reader_card WHERE reader_id like '%{keyword}%' or reader_name like '%{keyword}%';";
            var reader = command.ExecuteReader();
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                if (reader.Read() == false) break;
                card_id[i].Text = reader["reader_id"].ToString();
                card_name[i].Text = reader["reader_name"].ToString();
                item_num++;
            }
            reader.Close();

            for (int i = 0; i < item_num; i++)
            {
                int j = i;
                int count = 0;
                var query_command = sql_cnt.CreateCommand();
                query_command.CommandText = $@"SELECT reader_card.* FROM reader_card JOIN lend_list ON reader_card.reader_id = lend_list.reader_id WHERE reader_card.reader_id={card_id[j].Text} and back_date is null;";
                var query_reader = query_command.ExecuteReader();
                while (query_reader.Read()) { count++; }
                card_lendcount[i].Text = count.ToString();
                query_reader.Close();
            }
            for (int i = 0; i < item_num; i++)
            {
                int j = i;
                delete_btn[i].Show();

                delete_btn[i].Click += (s, arg) =>
                {
                    if (card_lendcount[j].Text != "0")
                    {
                        MessageBox.Show("Still books unreturned. Could not delete.");
                        return;
                    }
                    var delete_command = sql_cnt.CreateCommand();
                    delete_command.CommandText = $@"DELETE FROM reader_card WHERE reader_id={card_id[j].Text};";
                    delete_command.ExecuteNonQuery();
                    MessageBox.Show("Delete Success.");
                    search_btn.PerformClick();
                    return;
                };
            }
        }

        private void Dynamic_draw()
        {
            card_id = new Label[MAX_ITEM_NUM];
            card_name = new Label[MAX_ITEM_NUM];
            card_lendcount = new Label[MAX_ITEM_NUM];

            delete_btn = new Button[MAX_ITEM_NUM];
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                card_id[i] = new Label();
                card_name[i] = new Label();
                card_lendcount[i] = new Label();
                groupBox1.Controls.Add(card_id[i]);
                groupBox1.Controls.Add(card_name[i]);
                groupBox1.Controls.Add(card_lendcount[i]);
                card_id[i].AutoSize = true;
                card_name[i].AutoSize = true;
                card_lendcount[i].AutoSize = true;
                card_id[i].Location = new Point(30, i * y_step + 40);
                card_name[i].Location = new Point(150, i * y_step + 40);
                card_lendcount[i].Location = new Point(350, i * y_step + 40);
            }
        }

        private void InitializeBtn() // Initialize the return, modify, lend button
        {
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                delete_btn[i]?.Dispose();
                delete_btn[i] = new Button();
                groupBox1.Controls.Add(delete_btn[i]);
                delete_btn[i].Location = new Point(450, i * y_step + 35);
                delete_btn[i].Text = "删除";
                delete_btn[i].Hide();
                delete_btn[i].Size = new Size(50, 25);
            }
        }
    }
}
