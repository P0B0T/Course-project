using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Курсач.AddAndEditWindows;
using Курсач.Database;
using static Курсач.BufferClasses;

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();

        public Autorization()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            db.Ролиs.Load();

            Login.Focus();

            DataAutorization.Login = false;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var client = db.Клиентыs.Where(c => c.Логин == Login.Text && c.Пароль == Password.Password);

            var staff = db.Сотрудникиs.Where(s => s.Логин == Login.Text && s.Пароль == Password.Password);

            if (client.Count() == 1)
            {
                DataAutorization.Login = true;
                DataAutorization.Surname = client.First().Фамилия;
                DataAutorization.Name = client.First().Имя;
                DataAutorization.Patronymic = client.First().Отчество;
                DataAutorization.Right = client.First().КодРолиNavigation.Роль;
                Close();
            }
            else if (staff.Count() == 1)
            {
                DataAutorization.Login = true;
                DataAutorization.Surname = staff.First().Фамилия;
                DataAutorization.Name = staff.First().Имя;
                DataAutorization.Patronymic = staff.First().Отчество;
                DataAutorization.Right = staff.First().КодРолиNavigation.Роль;
                Close();
            }
            else
            {
                MessageWindow message = new MessageWindow();
                message.View = "Information";
                message.Title = "Ошибка";
                message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                message.MessageText = "Неверный логин или пароль";
                message.ShowMessageWindow();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            DataAutorization.Login = true;
            DataAutorization.Surname = "Незарегестрированный пользователь";
            DataAutorization.Name = null;
            DataAutorization.Patronymic = null;
            DataAutorization.Right = "Гость";
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            AddEdit.Or = "Add";

            AddAndEditClient addClient = new AddAndEditClient();
            addClient.ShowDialog();
        }
    }
}
