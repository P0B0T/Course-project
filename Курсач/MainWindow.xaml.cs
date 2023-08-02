using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Курсач.AddAndEditWindows;
using Курсач.Database;
using static Курсач.BufferClasses;

namespace Курсач
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        РемонтКомпьютернойТехникиGetContext db = РемонтКомпьютернойТехникиGetContext.GetContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        void StandartSize()
        {
            MinWidth = 800;
            MinHeight = 600;
            ResizeMode = ResizeMode.CanResize;
        }

        void Enter()
        {
            Autorization autorization = new Autorization();
            autorization.ShowDialog();

            if (DataAutorization.Login == false) Close();

            switch (DataAutorization.Right)
            {
                case "Администратор":
                    StandartSize();

                    GuestGrid.Visibility = Visibility.Hidden;
                    StaffGrid.Visibility = Visibility.Hidden;
                    ClientGrid.Visibility = Visibility.Hidden;

                    AdminGrid.Visibility = Visibility.Visible;
                    break;
                case "Сотрудник":
                    StandartSize();

                    GuestGrid.Visibility = Visibility.Hidden;
                    AdminGrid.Visibility = Visibility.Hidden;
                    ClientGrid.Visibility = Visibility.Hidden;

                    StaffGrid.Visibility = Visibility.Visible;
                    break;
                case "Клиент":
                    StandartSize();

                    GuestGrid.Visibility = Visibility.Hidden;
                    AdminGrid.Visibility = Visibility.Hidden;
                    StaffGrid.Visibility = Visibility.Hidden;

                    ClientGrid.Visibility = Visibility.Visible;
                    break;
                default:
                    MinHeight = 400;
                    MinWidth = 500;
                    Height = 400;
                    Width = 500;
                    ResizeMode = ResizeMode.NoResize;

                    AdminGrid.Visibility = Visibility.Hidden;
                    StaffGrid.Visibility = Visibility.Hidden;
                    ClientGrid.Visibility = Visibility.Hidden;

                    GuestGrid.Visibility = Visibility.Visible;
                    break;
            }

            Title = "Вход выполнен." + " " + DataAutorization.Surname + " " + DataAutorization.Name + " " + DataAutorization.Patronymic + " - " + DataAutorization.Right;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Enter();
        }

        void RepairsVisible()
        {
            SearchCheck.Parameter = "Repairs";

            gbSearch.Header = "Поиск по ФИО клиента";

            gbFilter.Visibility = Visibility.Visible;
            gbFilter.Header = "Фильтр по дате заказа";

            db.Клиентыs.Load();
            db.Устройстваs.Load();
            db.Сотрудникиs.Load();

            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.КодКлиентаNavigation.Фамилия"), Header = "Фамилия клиента" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.КодКлиентаNavigation.Имя"), Header = "Имя клиента" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.КодКлиентаNavigation.Отчество"), Header = "Отчество клиента" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.ТипУстройства"), Header = "Тип устройства" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("ДатаПриёма") { StringFormat = "dd/MM/yyyy" }, Header = "Дата приёма" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("ДатаОкончания") { StringFormat = "dd/MM/yyyy" }, Header = "Дата окончания" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("СтоимостьРемонта"), Header = "Стоимость ремонта" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("ОписаниеПроблемы"), Header = "Описание проблемы" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("ОписаниеПроделаннойРаботы"), Header = "Описание проделанной работы" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодСотрудникаNavigation.Фамилия"), Header = "Фамилия сотрудника" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодСотрудникаNavigation.Имя"), Header = "Имя сотрудника" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодСотрудникаNavigation.Отчество"), Header = "Отчество сотрудника" });

            dgOutput.ItemsSource = db.Ремонтыs.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RepairsVisible();
        }

        void Visibledg()
        {
            dgOutput.Columns.Clear();
            lvOutputDevices.Visibility = Visibility.Collapsed;
            dgOutput.Visibility = Visibility.Visible;
            lvOutputStaff.Visibility = Visibility.Collapsed;
        }

        private void Repairs_Click(object sender, RoutedEventArgs e)
        {
            Visibledg();

            RepairsVisible();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            Visibledg();

            SearchCheck.Parameter = "Clients";

            gbSearch.Header = "Поиск по ФИО клиента";

            gbFilter.Visibility = Visibility.Hidden;

            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Фамилия"), Header = "Фамилия" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Имя"), Header = "Имя" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Отчество"), Header = "Отчество" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Адрес"), Header = "Адрес" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("НомерТелефона"), Header = "Номер телефона" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("ЭлектроннаяПочта"), Header = "Электронная почта" });

            dgOutput.ItemsSource = db.Клиентыs.ToList();
        }

        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            SearchCheck.Parameter = "Devices";

            gbSearch.Header = "Поиск по модели устройтсва";

            gbFilter.Visibility = Visibility.Visible;
            gbFilter.Header = "Фильтр по типу устройства";

            dgOutput.Visibility = Visibility.Collapsed;

            lvOutputDevices.Visibility = Visibility.Visible;
            lvOutputStaff.Visibility = Visibility.Collapsed;

            db.Клиентыs.Load();
            lvOutputDevices.ItemsSource = db.Устройстваs.ToList();
        }

        private void Accessories_Click(object sender, RoutedEventArgs e)
        {
            Visibledg();

            SearchCheck.Parameter = "Accessories";

            gbSearch.Header = "Поиск по названию комплектующего";

            gbFilter.Visibility = Visibility.Visible;
            gbFilter.Header = "Фильтр по названию";

            db.Поставщикиs.Load();

            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Название"), Header = "Название" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Описание"), Header = "Описание" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Производитель"), Header = "Производитель" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Стоимость"), Header = "Стоимость" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодПоставщикаNavigation.НазваниеКомпании"), Header = "Название компании поставщика" });

            dgOutput.ItemsSource = db.КомплектующиеЗапчастиs.ToList();
        }

        private void OrdersAccessories_Click(object sender, RoutedEventArgs e)
        {
            Visibledg();

            SearchCheck.Parameter = "OrdersAccessories";

            gbSearch.Header = "Поиск по ФИО заказчика";

            gbFilter.Visibility = Visibility.Visible;
            gbFilter.Header = "Фильтр по дате заказа";

            db.Клиентыs.Load();
            db.КомплектующиеЗапчастиs.Load();
            db.Поставщикиs.Load();

            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКлиентаNavigation.Фамилия"), Header = "Фамилия заказчика" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКлиентаNavigation.Имя"), Header = "Имя заказчика" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКлиентаNavigation.Отчество"), Header = "Отчество заказчика" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.Название"), Header = "Название комплектующего/запчасти" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.Описание"), Header = "Описание" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.Производитель"), Header = "Производитель" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.Стоимость"), Header = "Стоимость" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.КодПоставщикаNavigation.НазваниеКомпании"), Header = "Название компании поставщика" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Количество"), Header = "Количество" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Стоимость"), Header = "Стоимость заказа" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("ДатаЗаказа") { StringFormat = "dd/MM/yyyy" }, Header = "Дата заказа" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("СтатусЗаказа"), Header = "Статус заказа" });

            dgOutput.ItemsSource = db.ЗаказыКомплектующихs.ToList();
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            Visibledg();

            SearchCheck.Parameter = "Suppliers";

            gbSearch.Header = "Поиск по названию компании";

            gbFilter.Visibility = Visibility.Hidden;

            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("НазваниеКомпании"), Header = "Название компании" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("КонтактноеЛицо"), Header = "Контактное лицо" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("НомерТелефона"), Header = "Номер телефона" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("Адрес"), Header = "Адрес" });
            dgOutput.Columns.Add(new DataGridTextColumn { Binding = new Binding("ЭлектроннаяПочта"), Header = "Электронная почта" });

            dgOutput.ItemsSource = db.Поставщикиs.ToList();
        }

        private void Staff_Click(object sender, RoutedEventArgs e)
        {
            SearchCheck.Parameter = "Staff";

            gbSearch.Header = "Поиск по ФИО сотрудника";

            gbFilter.Visibility = Visibility.Visible;
            gbFilter.Header = "Фильтр по должности";

            dgOutput.Visibility = Visibility.Collapsed;
            lvOutputDevices.Visibility = Visibility.Collapsed;

            lvOutputStaff.Visibility = Visibility.Visible;

            lvOutputStaff.ItemsSource = db.Сотрудникиs.ToList();
        }

        private void ReEntry_Click(object sender, RoutedEventArgs e)
        {
            Enter();
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow message = new MessageWindow();
            message.View = "Information";
            message.Title = "О программе";
            message.Width += 500;
            message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Информация.png", UriKind.RelativeOrAbsolute));
            message.MessageText = "Эта программа позволяет комфортно вести учёт работы сервиса по ремонту компьютерной техники. \nПодробнее о работе программы смотрите в приложенной документации.\n";
            message.ShowMessageWindow();
        }

        private void Developer_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://p0b0t.neocities.org", UseShellExecute = true });
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text.Length == 0)
            {
                MessageWindow message = new MessageWindow();
                message.View = "Information";
                message.Title = "Ошибка";
                message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                message.MessageText = "Введите параметр поиска!";
                message.ShowMessageWindow();

                return;
            }

            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    foreach (var item in dgOutput.Items)
                    {
                        if (((Ремонты)item).КодУстройстваNavigation.КодКлиентаNavigation.ФИО.ToLower().Contains(tbSearch.Text.ToLower()))
                        {
                            dgOutput.SelectedItem = item;
                            dgOutput.ScrollIntoView(item);
                            dgOutput.Focus();
                            break;
                        }
                    }
                    break;

                case "Clients":
                    foreach (var item in dgOutput.Items)
                    {
                        if (((Клиенты)item).ФИО.ToLower().Contains(tbSearch.Text.ToLower()))
                        {
                            dgOutput.SelectedItem = item;
                            dgOutput.ScrollIntoView(item);
                            dgOutput.Focus();
                            break;
                        }
                    }
                    break;

                case "Devices":
                    foreach (var item in lvOutputDevices.Items)
                    {
                        if (((Устройства)item).Модель.ToLower().Contains(tbSearch.Text.ToLower()))
                        {
                            lvOutputDevices.SelectedItem = item;
                            lvOutputDevices.ScrollIntoView(item);
                            lvOutputDevices.Focus();
                            break;
                        }
                    }
                    break;

                case "Accessories":
                    foreach (var item in dgOutput.Items)
                    {
                        if (((КомплектующиеЗапчасти)item).Название.ToLower().Contains(tbSearch.Text.ToLower()))
                        {
                            dgOutput.SelectedItem = item;
                            dgOutput.ScrollIntoView(item);
                            dgOutput.Focus();
                            break;
                        }
                    }
                    break;

                case "OrdersAccessories":
                    foreach (var item in dgOutput.Items)
                    {
                        if (((ЗаказыКомплектующих)item).КодКлиентаNavigation.ФИО.ToLower().Contains(tbSearch.Text.ToLower()))
                        {
                            dgOutput.SelectedItem = item;
                            dgOutput.ScrollIntoView(item);
                            dgOutput.Focus();
                            break;
                        }
                    }
                    break;

                case "Suppliers":
                    foreach (var item in dgOutput.Items)
                    {
                        if (((Поставщики)item).НазваниеКомпании.ToLower().Contains(tbSearch.Text.ToLower()))
                        {
                            dgOutput.SelectedItem = item;
                            dgOutput.ScrollIntoView(item);
                            dgOutput.Focus();
                            break;
                        }
                    }
                    break;

                case "Staff":
                    foreach (var item in lvOutputStaff.Items)
                    {
                        if (((Сотрудники)item).ФИО.ToLower().Contains(tbSearch.Text.ToLower()))
                        {
                            lvOutputStaff.SelectedItem = item;
                            lvOutputStaff.ScrollIntoView(item);
                            lvOutputStaff.Focus();
                            break;
                        }
                    }
                    break;
            }
        }
        private void Origin_Click(object sender, RoutedEventArgs e)
        {
            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    dgOutput.ItemsSource = db.Ремонтыs.ToList();
                    break;

                case "Clients":
                    dgOutput.ItemsSource = db.Клиентыs.ToList();
                    break;

                case "Devices":
                    lvOutputDevices.ItemsSource = db.Устройстваs.ToList();
                    break;

                case "Accessories":
                    dgOutput.ItemsSource = db.КомплектующиеЗапчастиs.ToList();
                    break;

                case "OrdersAccessories":
                    dgOutput.ItemsSource = db.ЗаказыКомплектующихs.ToList();
                    break;

                case "Suppliers":
                    dgOutput.ItemsSource = db.Поставщикиs.ToList();
                    break;

                case "Staff":
                    lvOutputStaff.ItemsSource = db.Сотрудникиs.ToList();
                    break;
            }
        }

        private void bFilter_Click(object sender, RoutedEventArgs e)
        {
            if (tbFilter.Text.Length == 0)
            {
                MessageWindow message = new MessageWindow();
                message.View = "Information";
                message.Title = "Ошибка";
                message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                message.MessageText = "Введите параметр фильтрации!";
                message.ShowMessageWindow();

                return;
            }

            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    if (!DateTime.TryParse(tbFilter.Text, out DateTime date))
                    {
                        MessageWindow message = new MessageWindow();
                        message.View = "Information";
                        message.Title = "Ошибка";
                        message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                        message.MessageText = "Введите дату!";
                        message.ShowMessageWindow();

                        return;
                    }

                    dgOutput.ItemsSource = db.Ремонтыs.Where(d => d.ДатаПриёма == date).ToList();
                    break;

                case "Devices":
                    lvOutputDevices.ItemsSource = db.Устройстваs.Where(d => d.ТипУстройства.ToLower() == tbFilter.Text.ToLower()).ToList();
                    break;

                case "Accessories":
                    dgOutput.ItemsSource = db.КомплектующиеЗапчастиs.Where(d => d.Название.ToLower() == tbFilter.Text.ToLower()).ToList();
                    break;

                case "OrdersAccessories":
                    if (!DateTime.TryParse(tbFilter.Text, out DateTime dateOrders))
                    {
                        MessageWindow message = new MessageWindow();
                        message.View = "Information";
                        message.Title = "Ошибка";
                        message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                        message.MessageText = "Введите дату!";
                        message.ShowMessageWindow();

                        return;
                    }

                    dgOutput.ItemsSource = db.ЗаказыКомплектующихs.Where(d => d.ДатаЗаказа == dateOrders).ToList();
                    break;

                case "Staff":
                    lvOutputStaff.ItemsSource = db.Сотрудникиs.Where(d => d.Должность.ToLower() == tbFilter.Text.ToLower()).ToList();
                    break;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEdit.Or = "Add";

            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    AddAndEditRepair addRepair = new AddAndEditRepair();
                    addRepair.ShowDialog();

                    dgOutput.ItemsSource = db.Ремонтыs.ToList();
                    dgOutput.Focus();
                    break;

                case "Clients":
                    AddAndEditClient addClient = new AddAndEditClient();
                    addClient.ShowDialog();

                    dgOutput.ItemsSource = db.Клиентыs.ToList();
                    dgOutput.Focus();
                    break;

                case "Devices":
                    AddAndEditDevices addDevice = new AddAndEditDevices();
                    addDevice.ShowDialog();

                    lvOutputDevices.ItemsSource = db.Устройстваs.ToList();
                    lvOutputDevices.Focus();
                    break;

                case "Accessories":
                    AddAndEditAccessories addAccessory = new AddAndEditAccessories();
                    addAccessory.ShowDialog();

                    dgOutput.ItemsSource = db.КомплектующиеЗапчастиs.ToList();
                    dgOutput.Focus();
                    break;

                case "OrdersAccessories":
                    AddAndEditOrdersAccessories addOrderAccessory = new AddAndEditOrdersAccessories();
                    addOrderAccessory.ShowDialog();

                    dgOutput.ItemsSource = db.ЗаказыКомплектующихs.ToList();
                    dgOutput.Focus();
                    break;

                case "Suppliers":
                    AddAndEditSuppliers addSupplier = new AddAndEditSuppliers();
                    addSupplier.ShowDialog();

                    dgOutput.ItemsSource = db.Поставщикиs.ToList();
                    dgOutput.Focus();
                    break;

                case "Staff":
                    AddAndEditStaff addStaff = new AddAndEditStaff();
                    addStaff.ShowDialog();

                    lvOutputStaff.ItemsSource = db.Сотрудникиs.ToList();
                    lvOutputStaff.Focus();
                    break;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AddEdit.Or = "Edit";

            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    if (dgOutput.SelectedIndex != -1)
                    {
                        var item = (Ремонты)dgOutput.Items[dgOutput.SelectedIndex];
                        Data.Id = item.Код;

                        AddAndEditRepair editRepair = new AddAndEditRepair();
                        editRepair.ShowDialog();

                        dgOutput.Items.Refresh();
                        dgOutput.Focus();
                    }
                    else ShowMessageError();
                    break;

                case "Clients":
                    if (dgOutput.SelectedIndex != -1)
                    {
                        var item = (Клиенты)dgOutput.Items[dgOutput.SelectedIndex];
                        Data.Id = item.Код;

                        AddAndEditClient editClient = new AddAndEditClient();
                        editClient.ShowDialog();

                        dgOutput.Items.Refresh();
                        dgOutput.Focus();
                    }
                    else ShowMessageError();
                    break;

                case "Devices":
                    if (lvOutputDevices.SelectedIndex != -1)
                    {
                        var item = (Устройства)lvOutputDevices.Items[lvOutputDevices.SelectedIndex];
                        Data.Id = item.Код;

                        AddAndEditDevices editDevice = new AddAndEditDevices();
                        editDevice.ShowDialog();

                        lvOutputDevices.Items.Refresh();
                        lvOutputDevices.Focus();
                    }
                    else ShowMessageError();
                    break;

                case "Accessories":
                    if (dgOutput.SelectedIndex != -1)
                    {
                        var item = (КомплектующиеЗапчасти)dgOutput.Items[dgOutput.SelectedIndex];
                        Data.Id = item.Код;

                        AddAndEditAccessories editAccessory = new AddAndEditAccessories();
                        editAccessory.ShowDialog();

                        dgOutput.Items.Refresh();
                        dgOutput.Focus();
                    }
                    else ShowMessageError();
                    break;

                case "OrdersAccessories":
                    if (dgOutput.SelectedIndex != -1)
                    {
                        var item = (ЗаказыКомплектующих)dgOutput.Items[dgOutput.SelectedIndex];
                        Data.Id = item.Код;

                        AddAndEditOrdersAccessories editOrderAccessory = new AddAndEditOrdersAccessories();
                        editOrderAccessory.ShowDialog();

                        dgOutput.Items.Refresh();
                        dgOutput.Focus();
                    }
                    else ShowMessageError();
                    break;

                case "Suppliers":
                    if (dgOutput.SelectedIndex != -1)
                    {
                        var item = (Поставщики)dgOutput.Items[dgOutput.SelectedIndex];
                        Data.Id = item.Код;

                        AddAndEditSuppliers editSupplier = new AddAndEditSuppliers();
                        editSupplier.ShowDialog();

                        dgOutput.Items.Refresh();
                        dgOutput.Focus();
                    }
                    else ShowMessageError();
                    break;

                case "Staff":
                    if (lvOutputStaff.SelectedIndex != -1)
                    {
                        var item = (Сотрудники)lvOutputStaff.Items[lvOutputStaff.SelectedIndex];
                        Data.Id = item.Код;

                        AddAndEditStaff editStaff = new AddAndEditStaff();
                        editStaff.ShowDialog();

                        lvOutputStaff.Items.Refresh();
                        lvOutputStaff.Focus();
                    }
                    else ShowMessageError();
                    break;
            }
        }
        void ShowMessageError()
        {
            MessageWindow message2 = new MessageWindow();
            message2.View = "Information";
            message2.Title = "Ошибка";
            message2.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
            message2.MessageText = "Выберите запись!";
            message2.ShowMessageWindow();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ShowMessageYesNo()
            {
                MessageWindow message = new MessageWindow();
                message.View = "YES/NO";
                message.Title = "Удаление записи";
                message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Вопрос.png", UriKind.RelativeOrAbsolute));
                message.MessageText = "Удалить запись?";
                message.ShowMessageWindow();

                return message.Result;
            }

            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    if (ShowMessageYesNo() == MessageBoxResult.Yes)
                    {
                        if (dgOutput.SelectedIndex != -1)
                        {
                            var item = (Ремонты)dgOutput.Items[dgOutput.SelectedIndex];
                            db.Ремонтыs.Remove(item);
                            db.SaveChanges();

                            dgOutput.ItemsSource = db.Ремонтыs.ToList();
                        }
                        else ShowMessageError();
                    }
                    break;

                case "Clients":
                    if (ShowMessageYesNo() == MessageBoxResult.Yes)
                    {
                        if (dgOutput.SelectedIndex != -1)
                        {
                            var item = (Клиенты)dgOutput.Items[dgOutput.SelectedIndex];
                            db.Клиентыs.Remove(item);
                            db.SaveChanges();

                            dgOutput.ItemsSource = db.Клиентыs.ToList();
                        }
                        else ShowMessageError();
                    }
                    break;

                case "Devices":
                    if (ShowMessageYesNo() == MessageBoxResult.Yes)
                    {
                        if (lvOutputDevices.SelectedIndex != -1)
                        {
                            var item = (Устройства)lvOutputDevices.Items[lvOutputDevices.SelectedIndex];
                            db.Устройстваs.Remove(item);
                            db.SaveChanges();

                            lvOutputDevices.ItemsSource = db.Устройстваs.ToList();
                        }
                        else ShowMessageError();
                    }
                    break;

                case "Accessories":
                    if (ShowMessageYesNo() == MessageBoxResult.Yes)
                    {
                        if (dgOutput.SelectedIndex != -1)
                        {
                            var item = (КомплектующиеЗапчасти)dgOutput.Items[dgOutput.SelectedIndex];
                            db.КомплектующиеЗапчастиs.Remove(item);
                            db.SaveChanges();

                            dgOutput.ItemsSource = db.КомплектующиеЗапчастиs.ToList();
                        }
                        else ShowMessageError();
                    }
                    break;

                case "OrdersAccessories":
                    if (ShowMessageYesNo() == MessageBoxResult.Yes)
                    {
                        if (dgOutput.SelectedIndex != -1)
                        {
                            var item = (ЗаказыКомплектующих)dgOutput.Items[dgOutput.SelectedIndex];
                            db.ЗаказыКомплектующихs.Remove(item);
                            db.SaveChanges();

                            dgOutput.ItemsSource = db.ЗаказыКомплектующихs.ToList();
                        }
                        else ShowMessageError();
                    }
                    break;

                case "Suppliers":
                    if (ShowMessageYesNo() == MessageBoxResult.Yes)
                    {
                        if (dgOutput.SelectedIndex != -1)
                        {
                            var item = (Поставщики)dgOutput.Items[dgOutput.SelectedIndex];
                            db.Поставщикиs.Remove(item);
                            db.SaveChanges();

                            dgOutput.ItemsSource = db.Поставщикиs.ToList();
                        }
                        else ShowMessageError();
                    }
                    break;

                case "Staff":
                    if (ShowMessageYesNo() == MessageBoxResult.Yes)
                    {
                        if (lvOutputStaff.SelectedIndex != -1)
                        {
                            var item = (Сотрудники)lvOutputStaff.Items[lvOutputStaff.SelectedIndex];
                            db.Сотрудникиs.Remove(item);
                            db.SaveChanges();

                            lvOutputStaff.ItemsSource = db.Сотрудникиs.ToList();
                        }
                        else ShowMessageError();
                    }
                    break;
            }
        }

        private void EnterOrRegister_Click(object sender, RoutedEventArgs e)
        {
            Enter();
        }

        void dgStaffVisibility()
        {
            dgOutputStaff.Columns.Clear();
            lvOutputDevicesStaff.Visibility = Visibility.Hidden;
            dgOutputStaff.Visibility = Visibility.Visible;

        }

        private void RepairsStaff_Click(object sender, RoutedEventArgs e)
        {
            SearchCheck.Parameter = "Repairs";

            dgStaffVisibility();

            gbFilterStaff.Header = "Фильтр по дате заказа";

            db.Клиентыs.Load();
            db.Устройстваs.Load();

            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.КодКлиентаNavigation.Фамилия"), Header = "Фамилия клиента" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.КодКлиентаNavigation.Имя"), Header = "Имя клиента" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.КодКлиентаNavigation.Отчество"), Header = "Отчество клиента" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.ТипУстройства"), Header = "Тип устройства" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("ДатаПриёма") { StringFormat = "dd/MM/yyyy" }, Header = "Дата приёма" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("ДатаОкончания") { StringFormat = "dd/MM/yyyy" }, Header = "Дата окончания" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("СтоимостьРемонта"), Header = "Стоимость ремонта" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("ОписаниеПроблемы"), Header = "Описание проблемы" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("ОписаниеПроделаннойРаботы"), Header = "Описание проделанной работы" });

            dgOutputStaff.ItemsSource = db.Ремонтыs.Where(r => r.КодСотрудникаNavigation.Имя == DataAutorization.Name && r.КодСотрудникаNavigation.Фамилия == DataAutorization.Surname && r.КодСотрудникаNavigation.Отчество == DataAutorization.Patronymic).ToList();
        }
        private void DevicesStaff_Click(object sender, RoutedEventArgs e)
        {
            SearchCheck.Parameter = "Devices";

            gbFilterStaff.Header = "Фильтр по типу устройства";

            dgOutputStaff.Visibility = Visibility.Collapsed;

            lvOutputDevicesStaff.Visibility = Visibility.Visible;

            db.Клиентыs.Load();
            lvOutputDevicesStaff.ItemsSource = db.Устройстваs.ToList();
        }
        private void AccessoriesStaff_Click(object sender, RoutedEventArgs e)
        {
            SearchCheck.Parameter = "Accessories";

            dgStaffVisibility();

            gbFilterStaff.Header = "Фильтр по названию";

            db.Поставщикиs.Load();

            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("Название"), Header = "Название" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("Описание"), Header = "Описание" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("Производитель"), Header = "Производитель" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("Стоимость"), Header = "Стоимость" });
            dgOutputStaff.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодПоставщикаNavigation.НазваниеКомпании"), Header = "Название компании поставщика" });

            dgOutputStaff.ItemsSource = db.КомплектующиеЗапчастиs.ToList();
        }

        private void bFilterStaff_Click(object sender, RoutedEventArgs e)
        {
            if (tbFilterStaff.Text.Length == 0)
            {
                MessageWindow message = new MessageWindow();
                message.View = "Information";
                message.Title = "Ошибка";
                message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                message.MessageText = "Введите параметр фильтрации!";
                message.ShowMessageWindow();

                return;
            }

            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    if (!DateTime.TryParse(tbFilterStaff.Text, out DateTime date))
                    {
                        MessageWindow message = new MessageWindow();
                        message.View = "Information";
                        message.Title = "Ошибка";
                        message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                        message.MessageText = "Введите дату!";
                        message.ShowMessageWindow();

                        return;
                    }

                    dgOutputStaff.ItemsSource = db.Ремонтыs.Where(d => d.ДатаПриёма == date && d.КодСотрудникаNavigation.Имя == DataAutorization.Name && d.КодСотрудникаNavigation.Фамилия == DataAutorization.Surname && d.КодСотрудникаNavigation.Отчество == DataAutorization.Patronymic).ToList();
                    break;

                case "Devices":
                    lvOutputDevicesStaff.ItemsSource = db.Устройстваs.Where(d => d.ТипУстройства.ToLower() == tbFilterStaff.Text.ToLower()).ToList();
                    break;

                case "Accessories":
                    dgOutputStaff.ItemsSource = db.КомплектующиеЗапчастиs.Where(d => d.Название.ToLower() == tbFilterStaff.Text.ToLower()).ToList();
                    break;
            }
        }

        private void OriginStaff_Click(object sender, RoutedEventArgs e)
        {
            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    dgOutputStaff.ItemsSource = db.Ремонтыs.Where(r => r.КодСотрудникаNavigation.Имя == DataAutorization.Name && r.КодСотрудникаNavigation.Фамилия == DataAutorization.Surname && r.КодСотрудникаNavigation.Отчество == DataAutorization.Patronymic).ToList();
                    break;

                case "Devices":
                    lvOutputDevicesStaff.ItemsSource = db.Устройстваs.ToList();
                    break;

                case "Accessories":
                    dgOutputStaff.ItemsSource = db.КомплектующиеЗапчастиs.ToList();
                    break;

            }
        }

        void dgClientsVisibility()
        {
            dgOutputClients.Columns.Clear();
            lvOutputDevicesClients.Visibility = Visibility.Hidden;
            dgOutputClients.Visibility = Visibility.Visible;

        }

        private void RepairsClients_Click(object sender, RoutedEventArgs e)
        {
            SearchCheck.Parameter = "Repairs";

            dgClientsVisibility();

            gbFilterClients.Header = "Фильтр по дате заказа";

            db.Клиентыs.Load();
            db.Устройстваs.Load();
            db.Сотрудникиs.Load();

            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодУстройстваNavigation.ТипУстройства"), Header = "Тип устройства" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("ДатаПриёма") { StringFormat = "dd/MM/yyyy" }, Header = "Дата приёма" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("ДатаОкончания") { StringFormat = "dd/MM/yyyy" }, Header = "Дата окончания" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("СтоимостьРемонта"), Header = "Стоимость ремонта" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("ОписаниеПроблемы"), Header = "Описание проблемы" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("ОписаниеПроделаннойРаботы"), Header = "Описание проделанной работы" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодСотрудникаNavigation.Фамилия"), Header = "Фамилия сотрудника" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодСотрудникаNavigation.Имя"), Header = "Имя сотрудника" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодСотрудникаNavigation.Отчество"), Header = "Отчество сотрудника" });

            dgOutputClients.ItemsSource = db.Ремонтыs.Where(r => r.КодУстройстваNavigation.КодКлиентаNavigation.Имя == DataAutorization.Name && r.КодУстройстваNavigation.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && r.КодУстройстваNavigation.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
        }

        private void DevicesClients_Click(object sender, RoutedEventArgs e)
        {
            SearchCheck.Parameter = "Devices";

            gbFilterClients.Header = "Фильтр по типу устройства";

            dgOutputClients.Visibility = Visibility.Collapsed;

            lvOutputDevicesClients.Visibility = Visibility.Visible;

            db.Клиентыs.Load();
            lvOutputDevicesClients.ItemsSource = db.Устройстваs.Where(d => d.КодКлиентаNavigation.Имя == DataAutorization.Name && d.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && d.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
        }

        private void AccessoriesClients_Click(object sender, RoutedEventArgs e)
        {
            SearchCheck.Parameter = "OrdersAccessories";

            dgClientsVisibility();

            gbFilterClients.Header = "Фильтр по дате заказа";

            db.КомплектующиеЗапчастиs.Load();
            db.Поставщикиs.Load();

            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.Название"), Header = "Название комплектующего/запчасти" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.Описание"), Header = "Описание" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.Производитель"), Header = "Производитель" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.Стоимость"), Header = "Стоимость" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("КодКомплектующегоNavigation.КодПоставщикаNavigation.НазваниеКомпании"), Header = "Название компании поставщика" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("Количество"), Header = "Количество" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("Стоимость"), Header = "Стоимость заказа" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("ДатаЗаказа") { StringFormat = "dd/MM/yyyy" }, Header = "Дата заказа" });
            dgOutputClients.Columns.Add(new DataGridTextColumn { Binding = new Binding("СтатусЗаказа"), Header = "Статус заказа" });

            dgOutputClients.ItemsSource = db.ЗаказыКомплектующихs.Where(o => o.КодКлиентаNavigation.Имя == DataAutorization.Name && o.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && o.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
        }

        private void bFilterClients_Click(object sender, RoutedEventArgs e)
        {
            if (tbFilterClients.Text.Length == 0)
            {
                MessageWindow message = new MessageWindow();
                message.View = "Information";
                message.Title = "Ошибка";
                message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                message.MessageText = "Введите параметр фильтрации!";
                message.ShowMessageWindow();

                return;
            }

            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    if (!DateTime.TryParse(tbFilterClients.Text, out DateTime date))
                    {
                        MessageWindow message = new MessageWindow();
                        message.View = "Information";
                        message.Title = "Ошибка";
                        message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                        message.MessageText = "Введите дату!";
                        message.ShowMessageWindow();

                        return;
                    }

                    dgOutputClients.ItemsSource = db.Ремонтыs.Where(d => d.ДатаПриёма == date && d.КодУстройстваNavigation.КодКлиентаNavigation.Имя == DataAutorization.Name && d.КодУстройстваNavigation.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && d.КодУстройстваNavigation.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
                    break;

                case "Devices":
                    lvOutputDevicesClients.ItemsSource = db.Устройстваs.Where(d => d.ТипУстройства.ToLower() == tbFilterClients.Text.ToLower() && d.КодКлиентаNavigation.Имя == DataAutorization.Name && d.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && d.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
                    break;

                case "OrdersAccessories":
                    if (!DateTime.TryParse(tbFilterClients.Text, out DateTime dateOrders))
                    {
                        MessageWindow message = new MessageWindow();
                        message.View = "Information";
                        message.Title = "Ошибка";
                        message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
                        message.MessageText = "Введите дату!";
                        message.ShowMessageWindow();

                        return;
                    }

                    dgOutputClients.ItemsSource = db.ЗаказыКомплектующихs.Where(d => d.ДатаЗаказа == dateOrders && d.КодКлиентаNavigation.Имя == DataAutorization.Name && d.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && d.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
                    break;
            }
        }

        private void OriginClients_Click(object sender, RoutedEventArgs e)
        {
            switch (SearchCheck.Parameter)
            {
                case "Repairs":
                    dgOutputClients.ItemsSource = db.Ремонтыs.Where(r => r.КодУстройстваNavigation.КодКлиентаNavigation.Имя == DataAutorization.Name && r.КодУстройстваNavigation.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && r.КодУстройстваNavigation.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
                    break;

                case "Devices":
                    lvOutputDevicesClients.ItemsSource = db.Устройстваs.Where(d => d.КодКлиентаNavigation.Имя == DataAutorization.Name && d.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && d.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
                    break;

                case "OrdersAccessories":
                    dgOutputClients.ItemsSource = db.ЗаказыКомплектующихs.Where(o => o.КодКлиентаNavigation.Имя == DataAutorization.Name && o.КодКлиентаNavigation.Фамилия == DataAutorization.Surname && o.КодКлиентаNavigation.Отчество == DataAutorization.Patronymic).ToList();
                    break;

            }
        }
    }
}
