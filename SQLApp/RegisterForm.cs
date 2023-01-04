using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, 54);
            userNameField.Text = "Введіть ім'я";
            userSurnameField.Text = "Прізвище";
            userSurnameField.ForeColor = Color.Gray;
            userNameField.ForeColor = Color.Gray;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastPoint;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void userNameField_Enter(object sender, EventArgs e)
        {
            if(userNameField.Text== "Введіть ім'я")
            {
                userNameField.Text = "";
                userNameField.ForeColor = Color.Black;
            }
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            if(userNameField.Text=="")
            {
                userNameField.Text = "Введіть ім'я";
                userNameField.ForeColor = Color.Gray;
            }
        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Прізвище")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;
            }
        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if(userSurnameField.Text == "")
            {
                userSurnameField.Text = "Прізвище";
                userSurnameField.ForeColor = Color.Gray;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (userNameField.Text == "Введіть ім'я")
            {
                MessageBox.Show("Введіть ім'я");
                return;
            }
            if (userSurnameField.Text == "Прізвище")
            {
                MessageBox.Show("Введіть прізвище");
                return;
            }
            if (isUserExists())
            {
                MessageBox.Show("Таикй логін вже є, введіть інший");
                return;
            }
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)",db.getConnection());
            command.Parameters.Add("@login",MySqlDbType.VarChar).Value= loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userNameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurnameField.Text;
            db.openConnection();
            if(command.ExecuteNonQuery()==1)
            {
                MessageBox.Show("Реєстрація успішна");
            }
            else
            {
                MessageBox.Show("Реєстрація не успішна");
            }
            db.closeConnection();
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }
        public Boolean isUserExists()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`=@uL", db.getConnection()); // @uL і @uP заглушки
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text; // заміна заглушок на нормальні символи
            adapter.SelectCommand = command; // адаптер дозволяє вибирати команди в бд
            adapter.Fill(table); // заповнення данними наш table
            if (table.Rows.Count > 0) // перевірка чи такий користувач є в бд
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }
    }
}
