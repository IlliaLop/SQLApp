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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.passField.AutoSize=false; 
            this.passField.Size = new Size(this.passField.Size.Width,54); // для того щоб текстбокси логіну і паролю були однакові
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e) // при наведенні на кнопку закриття червоне підсвічування
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e) //при відведенні від кнопки закриття чорне підсвічування
        {
            CloseButton.ForeColor = Color.Black;
        }
        Point lastPoint;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e) // передвигання вікна
        {
            if(e.Button==MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint=new Point(e.X,e.Y); // координата при натисканні на вікно
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            String loginUser = loginField.Text; // присвоювання тексту з кнопки логіну
            String passUser=passField.Text; // присвоювання тексту з кнопки паролю
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`=@uL AND `pass`=@uP",db.getConnection()); // @uL і @uP заглушки
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser; // заміна заглушок на нормальні символи
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser; // заміна заглушок на нормальні символи
            adapter.SelectCommand = command; // адаптер дозволяє вибирати команди в бд
            adapter.Fill(table); // заповнення данними наш table
            if(table.Rows.Count>0) // перевірка чи такий користувач є в бд
            {
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("No");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            this.Hide();
            registerForm.Show();
        }

    }
}
