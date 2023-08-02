using Microsoft.Win32;
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
using static Курсач.BufferClasses;
using Курсач.Database;

namespace Курсач.AddAndEditWindows
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditStaff.xaml
    /// </summary>
    public partial class AddAndEditStaff : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();
        Сотрудники staff = new Сотрудники();
        OpenFileDialog open = new OpenFileDialog();

        public AddAndEditStaff()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (AddEdit.Or)
            {
                case "Add":
                    Title = "Добавление устройтсва";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Добавление.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Добавить";
                    AddOrEdit.Click += Add_Click;
                    break;

                case "Edit":
                    Title = "Редактирование информации";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Редактирование.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Изменить";
                    AddOrEdit.Click += Edit_Click;

                    staff = db.Сотрудникиs.Find(Data.Id);
                    Name.Text = staff.Имя;
                    Surname.Text = staff.Фамилия;
                    Patronymic.Text = staff.Отчество;
                    Experiance.Text = staff.Стаж.ToString();
                    Post.Text = staff.Должность;
                    Salary.Text = staff.Зарплата.ToString();
                    Date.Text = staff.ДатаПриёмаНаРаботу.ToString();
                    Login.Text = staff.Логин;
                    Password.Text = staff.Пароль;

                    if (staff.Фотография != null)
                    {
                        BitmapImage photo = new BitmapImage(new Uri(staff.ФотографияGet));
                        StaffPhoto.Source = photo;
                    }
                    break;
            }
        }

        bool CheckAndWriteData()
        {
            StringBuilder errors = new StringBuilder();

            if (Name.Text.Length == 0) errors.AppendLine("Введите имя!");
            if (Surname.Text.Length == 0) errors.AppendLine("Введите фамилию!");
            if (Post.Text.Length == 0) errors.AppendLine("Введите должность!");
            if (!decimal.TryParse(Salary.Text, out decimal salary) || salary <= 0) errors.AppendLine("Введите зарплату!");
            if (Date.Text.Length == 0) errors.AppendLine("Введите дату приёма на работу!");
            if (Login.Text.Length == 0) errors.AppendLine("Введите логин!");
            if (Password.Text.Length == 0) errors.AppendLine("Введите пароль!");

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

            staff.Имя = Name.Text;
            staff.Фамилия = Surname.Text;
            staff.Отчество = Patronymic.Text;

            if (Convert.ToInt32(Experiance.Text) < 80) staff.Стаж = Convert.ToInt32(Experiance.Text);
            else
            {
                MessageWindow message = new MessageWindow();
                message.View = "Information";
                message.Title = "Ошибка";
                message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                message.MessageText = "Стаж не модет быть больше 80";
                message.ShowMessageWindow();

                return false;
            }

            staff.Зарплата = salary;
            staff.ДатаПриёмаНаРаботу = Convert.ToDateTime(Date.Text);

            if (open.SafeFileName.Length != 0)
            {
                BitmapImage photo = new BitmapImage(new Uri(open.FileName));
                staff.Фотография = open.SafeFileName;
            }

            staff.КодРоли = 2;
            staff.Логин = Login.Text;
            staff.Пароль = Password.Text;

            return true;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndWriteData() == false) return;

            try
            {
                db.Сотрудникиs.Add(staff);
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

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            open.Filter = "Все файлы |*.*| Файлы *.jpg|*.jpg";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                BitmapImage photo = new BitmapImage(new Uri(open.FileName));
                StaffPhoto.Source = photo;
            }
        }
    }
}
