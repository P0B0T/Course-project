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
using Microsoft.Win32;

namespace Курсач.AddAndEditWindows
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditDevices.xaml
    /// </summary>
    public partial class AddAndEditDevices : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();
        Устройства device = new Устройства();
        OpenFileDialog open = new OpenFileDialog();

        public AddAndEditDevices()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Client.ItemsSource = db.Клиентыs.ToList();
            Client.DisplayMemberPath = "ФИО";

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

                    device = db.Устройстваs.Find(Data.Id);
                    Model.Text = device.Модель;
                    Manufacturer.Text = device.Производитель;
                    DeviceType.Text = device.ТипУстройства;
                    YearOfRealease.Text = device.ГодВыпуска.ToString();
                    SerialNumber.Text = device.СерийныйНомер.ToString();
                    Client.Text = device.КодКлиентаNavigation.ФИО;

                    if (device.ФотоУстройства != null)
                    {
                        BitmapImage photo = new BitmapImage(new Uri(device.ФотоУстройстваGet));
                        DevicePhoto.Source = photo;
                    }
                    break;
            }
        }

        bool CheckAndWriteData()
        {
            StringBuilder errors = new StringBuilder();

            if (Model.Text.Length == 0) errors.AppendLine("Введите модель!");
            if (Manufacturer.Text.Length == 0) errors.AppendLine("Введите производителя!");
            if (DeviceType.Text.Length == 0) errors.AppendLine("Введите тип устройства!");
            if (!short.TryParse(YearOfRealease.Text, out short yearOfRealease) || yearOfRealease < 1950 || yearOfRealease > DateTime.Now.Year) errors.AppendLine("Введите год выпуска!");
            if (Client.SelectedItem == null) errors.AppendLine("Выберите клиента!");

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

            device.Модель = Model.Text;
            device.Производитель = Manufacturer.Text;
            device.ТипУстройства = DeviceType.Text;
            device.ГодВыпуска = yearOfRealease;
            device.КодКлиента = ((Клиенты)Client.SelectedItem).Код;

            if (open.SafeFileName.Length != 0)
            {
                BitmapImage photo = new BitmapImage(new Uri(open.FileName));
                device.ФотоУстройства = open.SafeFileName;
            }

            return true;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndWriteData() == false) return;

            try
            {
                db.Устройстваs.Add(device);
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
                DevicePhoto.Source = photo;
            }
        }
    }
}
