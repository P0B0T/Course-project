using System;
using System.Collections.Generic;
using System.IO;

namespace Курсач.Database;

public partial class Сотрудники
{
    public int Код { get; set; }

    public string Имя { get; set; } = null!;

    public string Фамилия { get; set; } = null!;

    public string? Отчество { get; set; }

    public string ФИО
    {
        get { return Фамилия + " " + Имя + " " + Отчество; }
    }

    public int? Стаж { get; set; }

    public string Должность { get; set; } = null!;

    public decimal Зарплата { get; set; }

    public DateTime ДатаПриёмаНаРаботу { get; set; }

    public string? Фотография { get; set; }

    public string ФотографияGet
    {
        get
        {
            if (Фотография == null)
            {
                return null;
            }
            else
            {
                string namePhoto = Directory.GetCurrentDirectory() + "\\PhotoStaff\\" + Фотография;
                return namePhoto;
            }
        }
    }

    public int КодРоли { get; set; }

    public string Логин { get; set; } = null!;

    public string Пароль { get; set; } = null!;

    public virtual Роли КодРолиNavigation { get; set; } = null!;

    public virtual ICollection<Ремонты> Ремонтыs { get; set; } = new List<Ремонты>();
}
