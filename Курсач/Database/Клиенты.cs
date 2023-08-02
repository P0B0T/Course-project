using System;
using System.Collections.Generic;

namespace Курсач.Database;

public partial class Клиенты
{
    public int Код { get; set; }

    public string Имя { get; set; } = null!;

    public string Фамилия { get; set; } = null!;

    public string? Отчество { get; set; }

    public string ФИО
    {
        get { return Фамилия + " " + Имя + " " + Отчество; }
    }

    public string Адрес { get; set; } = null!;

    public string НомерТелефона { get; set; } = null!;

    public string ЭлектроннаяПочта { get; set; } = null!;

    public int КодРоли { get; set; }

    public string Логин { get; set; } = null!;

    public string Пароль { get; set; } = null!;

    public virtual ICollection<ЗаказыКомплектующих> ЗаказыКомплектующихs { get; set; } = new List<ЗаказыКомплектующих>();

    public virtual Роли КодРолиNavigation { get; set; } = null!;

    public virtual ICollection<Устройства> Устройстваs { get; set; } = new List<Устройства>();
}
