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
    public partial class Form1 : Form
    {
        SqlConnection sql_cnt;
        public bool AdminPrivilege = false; //debug
        public int Card_ID = -1;
        public string Username;
        Label[] book_id;
        Label[] book_name;
        Label[] author;
        Label[] price;
        Label[] left_num;
        Button[] modify_btn;
        Button[] lend_btn;
        Button[] back_btn;
        const int MAX_ITEM_NUM = 10;
        const int y_step = 30;

        public Form1()
        {
            InitializeComponent();
            #region dynamic_draw
            book_id = new Label[MAX_ITEM_NUM];
            book_name = new Label[MAX_ITEM_NUM];
            author = new Label[MAX_ITEM_NUM];
            price = new Label[MAX_ITEM_NUM];
            left_num = new Label[MAX_ITEM_NUM];
            modify_btn = new Button[MAX_ITEM_NUM];
            lend_btn = new Button[MAX_ITEM_NUM];
            back_btn = new Button[MAX_ITEM_NUM];
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                book_id[i] = new Label();
                book_name[i] = new Label();
                author[i] = new Label();
                price[i] = new Label();
                left_num[i] = new Label();
                groupBox1.Controls.Add(book_id[i]);
                groupBox1.Controls.Add(book_name[i]);
                groupBox1.Controls.Add(author[i]);
                groupBox1.Controls.Add(price[i]);
                groupBox1.Controls.Add(left_num[i]);
                book_id[i].AutoSize = true;
                book_name[i].AutoSize = true;
                author[i].AutoSize = true;
                price[i].AutoSize = true;
                left_num[i].AutoSize = true;
                book_id[i].Location = new Point(30, i * y_step + 40);
                book_name[i].Location = new Point(100, i * y_step + 40);
                author[i].Location = new Point(220, i * y_step + 40);
                price[i].Location = new Point(350, i * y_step + 40);
                left_num[i].Location = new Point(400, i * y_step + 40);
            }
            #endregion
            sql_cnt = new DB_Connector().Connect();
        }

        private void user_login_btn_Click(object sender, EventArgs e)
        {
            if (this.Card_ID == -1)
            {
                new Form_login(sql_cnt).ShowDialog(this);
                if (this.Card_ID != -1)
                {
                    this.name_label.Text = "欢迎你，" + Username + (AdminPrivilege ? " (Administrator)" : string.Empty);
                    user_login_btn.Text = "登出";
                    user_register_btn.Enabled = false;
                    search_btn.PerformClick();
                }
            }
            else
            {
                this.Card_ID = -1;
                this.AdminPrivilege = false;
                this.Username = string.Empty;
                this.name_label.Text = "未登录";
                user_login_btn.Text = "登录";
                foreach (var btns in modify_btn)
                {
                    btns.Enabled = false;
                }
                user_register_btn.Enabled = true;
                search_btn.PerformClick();
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if (this.AdminPrivilege == false)
            {
                MessageBox.Show("Plz login administrator first.");
            }
            else
                new Form_AddBook(sql_cnt).ShowDialog(this);
        }

        private void user_register_btn_Click(object sender, EventArgs e)
        {
            new Form_login(sql_cnt, isRegister: true).ShowDialog(this);
            search_btn.PerformClick();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            if (Card_ID == -1)
            {
                user_login_btn.PerformClick();
            }
            if (Card_ID == -1)
            {
                return;
            }
            InitializeBtn();
            string keyword = textBox1.Text.Trim();
            int item_num = 0;
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                book_id[i].Text = string.Empty;
                book_name[i].Text = string.Empty;
                author[i].Text = string.Empty;
                price[i].Text = string.Empty;
                left_num[i].Text = string.Empty;
            }
            SqlCommand command = sql_cnt.CreateCommand();
            command.CommandText = $@"SELECT * FROM book_info WHERE book_id like '%{keyword}%' or book_name like '%{keyword}%' or author like '%{keyword}%';";
            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; i < MAX_ITEM_NUM; i++)
                {
                    if (reader.Read() == false) break;
                    book_id[i].Text = reader["book_id"].ToString();
                    book_name[i].Text = reader["book_name"].ToString();
                    author[i].Text = reader["author"].ToString();
                    price[i].Text = reader["price"].ToString();
                    left_num[i].Text = reader["left_num"].ToString();
                    item_num++;
                }
            }
            for (int i = 0; i < item_num; i++)
            {
                int j = i;
                modify_btn[i].Enabled = this.AdminPrivilege;
                modify_btn[i].Show();
                modify_btn[i].Click += (s, arg) =>
                {
                    new Form_Modify(sql_cnt, book_id[j].Text, book_name[j].Text, author[j].Text, price[j].Text, left_num[j].Text).ShowDialog(this);
                    search_btn.PerformClick();
                };
                lend_btn[i].Show();
                var query_command = sql_cnt.CreateCommand();
                query_command.CommandText = $@"SELECT * FROM lend_list WHERE book_id={book_id[j].Text} and reader_id={Card_ID} and back_date is null;";
                var reader = query_command.ExecuteReader();
                if (reader.HasRows)
                {
                    back_btn[i].Enabled = true;
                    lend_btn[i].Enabled = false;
                }
                else
                    lend_btn[i].Enabled = (left_num[i].Text != "0" && Card_ID != -1);
                reader.Close();

                lend_btn[i].Click += (s, arg) =>
                {
                    try
                    {
                        {
                            var insert_command = sql_cnt.CreateCommand();
                            insert_command.CommandText = $@"INSERT INTO lend_list (book_id, reader_id, lend_date) VALUES ({book_id[j].Text}, {Card_ID}, '{DateTime.Now.ToString("yyyy-MM-dd")}');"
                            + $@"UPDATE book_info SET left_num=left_num-1 WHERE book_id={book_id[j].Text};";
                            insert_command.ExecuteNonQuery();
                            MessageBox.Show("Success.");
                            search_btn.PerformClick();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error Occured.");
                    }
                    return;
                };
                back_btn[i].Show();
                back_btn[i].Click += (s, arg) =>
                {
                    try
                    {
                        var update_command = sql_cnt.CreateCommand();
                        update_command.CommandText = $@"UPDATE lend_list SET back_date='{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE book_id={book_id[j].Text} and reader_id={Card_ID};"
                        + $@"UPDATE book_info SET left_num=left_num+1 WHERE book_id={book_id[j].Text};";
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

        private void InitializeBtn() // Initialize the return, modify, lend button
        {
            for (int i = 0; i < MAX_ITEM_NUM; i++)
            {
                modify_btn[i]?.Dispose();
                lend_btn[i]?.Dispose();
                back_btn[i]?.Dispose();
                modify_btn[i] = new Button();
                lend_btn[i] = new Button();
                back_btn[i] = new Button();
                groupBox1.Controls.Add(modify_btn[i]);
                groupBox1.Controls.Add(lend_btn[i]);
                groupBox1.Controls.Add(back_btn[i]);
                modify_btn[i].Location = new Point(450, i * y_step + 35);
                lend_btn[i].Location = new Point(500, i * y_step + 35);
                back_btn[i].Location = new Point(550, i * y_step + 35);
                modify_btn[i].Text = "修改";
                lend_btn[i].Text = "借阅";
                back_btn[i].Text = "归还";
                modify_btn[i].Hide();
                lend_btn[i].Hide();
                back_btn[i].Hide();
                modify_btn[i].Size = new Size(50, 25);
                lend_btn[i].Size = new Size(50, 25);
                back_btn[i].Size = new Size(50, 25);
                this.back_btn[i].Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Card_ID == -1)
            {
                user_login_btn.PerformClick();
            }
            if (Card_ID == -1)
            {
                return;
            }
            if (AdminPrivilege)
            {
                new Form_User(sql_cnt).ShowDialog(this);    
            }
            else
            {
                new Form_Manage(sql_cnt, Card_ID.ToString()).ShowDialog(this);
            }
            search_btn.PerformClick();
        }
    }
}
