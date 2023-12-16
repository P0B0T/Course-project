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
    /// Логика взаимодействия для AddAndEditRepair.xaml
    /// </summary>
    public partial class AddAndEditRepair : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();
        Ремонты repair = new Ремонты();

        public AddAndEditRepair()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Devices.ItemsSource = db.Устройстваs.ToList();
            Devices.DisplayMemberPath = "Модель";

            Staff.ItemsSource = db.Сотрудникиs.ToList();
            Staff.DisplayMemberPath = "ФИО";

            switch (AddEdit.Or)
            {
                case "Add":
                    Title = "Добавление ремонта";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Добавление.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Добавить";
                    AddOrEdit.Click += Add_Click;
                    break;

                case "Edit":
                    Title = "Редактирование информации";
                    Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Редактирование.png", UriKind.RelativeOrAbsolute));

                    AddOrEdit.Content = "Изменить";
                    AddOrEdit.Click += Edit_Click;

                    repair = db.Ремонтыs.Find(Data.Id);
                    Devices.Text = repair.КодУстройстваNavigation.Модель;
                    Staff.Text = repair.КодСотрудникаNavigation.ФИО;
                    DateIn.Text = repair.ДатаПриёма.ToString();
                    DateOut.Text = repair.ДатаОкончания.ToString();
                    Cost.Text = repair.СтоимостьРемонта.ToString();
                    DescriptionProblem.Text = repair.ОписаниеПроблемы;
                    DescriptionWork.Text = repair.ОписаниеПроделаннойРаботы;
                    break;
            }
        }

        bool CheckAndWriteData()
        {
            StringBuilder errors = new StringBuilder();

            DateTime.TryParse(DateOut.Text, out DateTime dateOut);
            DateTime.TryParse(DateIn.Text, out DateTime dateIn);

            if (Devices.SelectedItem == null) errors.AppendLine("Выберите устройство!");
            if (Staff.SelectedItem == null) errors.AppendLine("Выберите сотрудника!");
            if (DateIn.Text.Length == 0) errors.AppendLine("Выберите дату приёма!");
            if (DateOut.Text.Length == 0) errors.AppendLine("Введите дату окончания!");
            if (dateOut < dateIn) errors.AppendLine("Дата окончания не может быть раньше даты приёма!");
            if (!decimal.TryParse(Cost.Text, out decimal cost) || cost <= 0) errors.AppendLine("Введите стоимость ремонта!");
            if (DescriptionProblem.Text.Length == 0) errors.AppendLine("Введите описание проблемы!");
            if (DescriptionWork.Text.Length == 0) errors.AppendLine("Введите описание проделанной работы!");

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

            repair.КодУстройства = ((Устройства)Devices.SelectedItem).Код;
            repair.КодСотрудника = ((Сотрудники)Staff.SelectedItem).Код;
            repair.ДатаПриёма = dateIn;
            repair.ДатаОкончания = dateOut;
            repair.СтоимостьРемонта = cost;
            repair.ОписаниеПроблемы = DescriptionProblem.Text;
            repair.ОписаниеПроделаннойРаботы = DescriptionWork.Text;

            return true;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndWriteData() == false) return;

            try
            {
                db.Ремонтыs.Add(repair);
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
