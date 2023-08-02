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
    /// Логика взаимодействия для AddAndEditAccessories.xaml
    /// </summary>
    public partial class AddAndEditAccessories : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();
        КомплектующиеЗапчасти accesory = new КомплектующиеЗапчасти();

        public AddAndEditAccessories()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Supplier.ItemsSource = db.Поставщикиs.ToList();
            Supplier.DisplayMemberPath = "НазваниеКомпании";

            switch (AddEdit.Or)
            {
                case "Add":
                    Title = "Добавление комплектующего";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Добавление.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Добавить";
                    AddOrEdit.Click += Add_Click;
                    break;

                case "Edit":
                    Title = "Редактирование информации";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Редактирование.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Изменить";
                    AddOrEdit.Click += Edit_Click;

                    accesory = db.КомплектующиеЗапчастиs.Find(Data.Id);
                    Name.Text = accesory.Название;
                    Description.Text = accesory.Описание;
                    Manufacturer.Text = accesory.Производитель;
                    Cost.Text = accesory.Стоимость.ToString();
                    Supplier.Text = accesory.КодПоставщикаNavigation.НазваниеКомпании;
                    break;
            }
        }

        bool CheckAndWriteData()
        {
            StringBuilder errors = new StringBuilder();

            if (Name.Text.Length == 0) errors.AppendLine("Введите название!");
            if (Manufacturer.Text.Length == 0) errors.AppendLine("Введите производителя");
            if (!decimal.TryParse(Cost.Text, out decimal cost) || cost <= 0) errors.AppendLine("Введите стоимость!");
            if (Supplier.SelectedItem == null) errors.AppendLine("Выберите поставщика!");

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

            accesory.Название = Name.Text;
            accesory.Описание = Description.Text;
            accesory.Производитель = Manufacturer.Text;
            accesory.Стоимость = cost;
            accesory.КодПоставщика = ((Поставщики)Supplier.SelectedItem).Код;

            return true;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndWriteData() == false) return;

            try
            {
                db.КомплектующиеЗапчастиs.Add(accesory);
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
