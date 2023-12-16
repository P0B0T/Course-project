using System;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using static Курсач.BufferClasses;
using Курсач.Database;

namespace Курсач.AddAndEditWindows
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditSuppliers.xaml
    /// </summary>
    public partial class AddAndEditSuppliers : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();
        Поставщики supplier = new Поставщики();

        public AddAndEditSuppliers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (AddEdit.Or)
            {
                case "Add":
                    Title = "Добавление поставщика";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Добавление.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Добавить";
                    AddOrEdit.Click += Add_Click;
                    break;

                case "Edit":
                    Title = "Редактирование информации";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Редактирование.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Изменить";
                    AddOrEdit.Click += Edit_Click;

                    supplier = db.Поставщикиs.Find(Data.Id);
                    NameCompany.Text = supplier.НазваниеКомпании;
                    ContactPerson.Text = supplier.КонтактноеЛицо;
                    TelephoneNumber.Text = supplier.НомерТелефона;
                    Adress.Text = supplier.Адрес;
                    Email.Text = supplier.ЭлектроннаяПочта;
                    break;
            }
        }

        bool CheckAndWriteData()
        {
            StringBuilder errors = new StringBuilder();

            if (NameCompany.Text.Length == 0) errors.AppendLine("Введите название компании!");
            if (Adress.Text.Length == 0) errors.AppendLine("Введите адресс!");
            if (!Email.Text.Contains("@")) errors.AppendLine("Введите действительную электронную почту!");

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

            supplier.НазваниеКомпании = NameCompany.Text;
            supplier.КонтактноеЛицо = ContactPerson.Text;
            supplier.НомерТелефона = TelephoneNumber.Text;
            supplier.Адрес = Adress.Text;
            supplier.ЭлектроннаяПочта = Email.Text;

            return true;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndWriteData() == false) return;

            try
            {
                db.Поставщикиs.Add(supplier);
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
