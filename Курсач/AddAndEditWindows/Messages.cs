using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Курсач
{
    public static class Messages
    {
        public static void MessageAddSuccessfully()
        {
            MessageWindow message = new MessageWindow();
            message.View = "Information";
            message.Title = "Уведомление";
            message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Информация.png", UriKind.RelativeOrAbsolute));
            message.MessageText = "Запись добавлена!";
            message.ShowMessageWindow();
        }

        public static void MessageEditSuccessfully()
        {
            MessageWindow message = new MessageWindow();
            message.View = "Information";
            message.Title = "Уведомление";
            message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Информация.png", UriKind.RelativeOrAbsolute));
            message.MessageText = "Запись изменена!";
            message.ShowMessageWindow();
        }
    }
}
