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
using Курсач.Database;
using static Курсач.BufferClasses;

namespace Курсач.AddAndEditWindows
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditClient.xaml
    /// </summary>
    public partial class AddAndEditClient : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();
        Клиенты client = new Клиенты();

        public AddAndEditClient()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (AddEdit.Or)
            {
                case "Add":
                    Title = "Добавление клиента";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Добавление.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Добавить";
                    AddOrEdit.Click += Add_Click;
                    break;

                case "Edit":
                    Title = "Редактирование информации клиента";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Редактирование.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Изменить";
                    AddOrEdit.Click += Edit_Click;

                    client = db.Клиентыs.Find(Data.Id);
                    Surname.Text = client.Фамилия;
                    Name.Text = client.Имя;
                    Patronymic.Text = client.Отчество;
                    Adress.Text = client.Адрес;
                    TelephoneNumber.Text = client.НомерТелефона;
                    Email.Text = client.ЭлектроннаяПочта;
                    Login.Text = client.Логин;
                    Password.Text = client.Пароль;
                    break;
            }
        }

        bool CheckAndWriteData()
        {
            StringBuilder errors = new StringBuilder();

            if (Surname.Text.Length == 0) errors.AppendLine("Введите фамилию!");
            if (Name.Text.Length == 0) errors.AppendLine("Введите имя!");
            if (Adress.Text.Length == 0) errors.AppendLine("Введите адресс!");
            if (TelephoneNumber.Text.Length == 0) errors.AppendLine("Введите номер телефона!");
            if (!Email.Text.Contains("@")) errors.AppendLine("Введите действительную электронную почту!");
            if (Login.Text.Length == 0) errors.AppendLine("Введите логин!");
            if (Password.Text.Length == 0) errors.AppendLine("Введите пароль!");
            if (Password.Text != PasswordCheck.Text) errors.AppendLine("Пароли не совпадают!");

            if (errors.Length > 0)
            {
                MessageWindow message = new MessageWindow();
                message.View = "Information";
                message.Title = "Ошибка";
                message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                message.MessageText = errors.ToString();
                message.ShowMessageWindow();

                return false;
            }

            client.Фамилия = Surname.Text;
            client.Имя = Name.Text;
            client.Отчество = Patronymic.Text;
            client.Адрес = Adress.Text;
            client.НомерТелефона = TelephoneNumber.Text;
            client.ЭлектроннаяПочта = Email.Text;
            client.КодРоли = 3;
            client.Логин = Login.Text;
            client.Пароль = Password.Text;

            return true;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndWriteData() == false) return;

            try
            {
                db.Клиентыs.Add(client);
                db.SaveChanges();

                Messages.MessageAddSuccessfully();

                Close();
            }
            catch { }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndWriteData() == false) return;

            try
            {
                db.SaveChanges();

                Messages.MessageEditSuccessfully();

                Close();
            }
            catch { }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
