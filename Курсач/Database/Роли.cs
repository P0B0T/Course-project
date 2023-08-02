using System;
using System.Collections.Generic;

namespace Курсач.Database;

public partial class Роли
{
    public int КодРоли { get; set; }

    public string Роль { get; set; } = null!;

    public virtual ICollection<Клиенты> Клиентыs { get; set; } = new List<Клиенты>();

    public virtual ICollection<Сотрудники> Сотрудникиs { get; set; } = new List<Сотрудники>();
}
