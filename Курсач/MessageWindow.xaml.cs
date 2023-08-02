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

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow()
        {
            InitializeComponent();
        }

        public string View;

        public string MessageText { get; set; }

        public MessageBoxResult Result { get; private set; }

        public MessageBoxResult ShowMessageWindow()
        {
            Output.Text = MessageText;
            ShowDialog();
            return Result;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (View)
            {
                case "Information":
                    OK.Visibility = Visibility.Visible;
                    break;

                case "OK/CANSEL":
                    OK.Visibility = Visibility.Visible;
                    Cansel.Visibility = Visibility.Visible;
                    break;

                case "YES/NO":
                    Yes.Visibility = Visibility.Visible;
                    No.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
        }
    }
}
