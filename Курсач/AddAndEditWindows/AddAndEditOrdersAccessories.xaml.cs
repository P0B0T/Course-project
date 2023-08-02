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
    /// Логика взаимодействия для AddAndEditOrdersAccessories.xaml
    /// </summary>
    public partial class AddAndEditOrdersAccessories : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();
        ЗаказыКомплектующих order = new ЗаказыКомплектующих();

        public AddAndEditOrdersAccessories()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Client.ItemsSource = db.Клиентыs.ToList();
            Client.DisplayMemberPath = "ФИО";

            Accessory.ItemsSource = db.КомплектующиеЗапчастиs.ToList();
            Accessory.DisplayMemberPath = "Название";

            switch (AddEdit.Or)
            {
                case "Add":
                    Title = "Добавление заказа";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Добавление.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Добавить";
                    AddOrEdit.Click += Add_Click;
                    break;

                case "Edit":
                    Title = "Редактирование информации";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Редактирование.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Изменить";
                    AddOrEdit.Click += Edit_Click;

                    order = db.ЗаказыКомплектующихs.Find(Data.Id);
                    Client.Text = order.КодКлиентаNavigation.ФИО;
                    Accessory.Text = order.КодКомплектующегоNavigation.Название;
                    Quantity.Text = order.Количество;
                    Cost.Text = order.Стоимость.ToString();
                    OrderDate.Text = order.ДатаЗаказа.ToString();
                    OrderStatus.Text = order.СтатусЗаказа;
                    break;
            }
        }

        bool CheckAndWriteData()
        {
            StringBuilder errors = new StringBuilder();

            if (Client.SelectedItem == null) errors.AppendLine("Выберите клиента!");
            if (Accessory.SelectedItem == null) errors.AppendLine("Выберите запчасть!");
            if (Quantity.Text.Length == 0 || Quantity.Text.Contains("-") || Quantity.Text.Contains("0")) errors.AppendLine("Введите количество!");
            if (!decimal.TryParse(Cost.Text, out decimal cost) || cost <= 0) errors.AppendLine("Введите стоимость!");
            if (OrderDate.Text.Length == 0) errors.AppendLine("Выберите дату!");
            if (OrderStatus.Text.Length == 0) errors.AppendLine("Введите статус заказа!");

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

            order.КодКлиента = ((Клиенты)Client.SelectedItem).Код;
            order.КодКомплектующего = ((КомплектующиеЗапчасти)Accessory.SelectedItem).Код;
            order.Количество = Quantity.Text;
            order.Стоимость = cost;
            order.ДатаЗаказа = Convert.ToDateTime(OrderDate.Text);
            order.СтатусЗаказа = OrderStatus.Text;

            return true;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndWriteData() == false) return;

            try
            {
                db.ЗаказыКомплектующихs.Add(order);
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
