using System;
using System.Collections.Generic;

namespace Курсач.Database;

public partial class Ремонты
{
    public int Код { get; set; }

    public int КодУстройства { get; set; }

    public int КодСотрудника { get; set; }

    public DateTime ДатаПриёма { get; set; }

    public DateTime ДатаОкончания { get; set; }

    public decimal СтоимостьРемонта { get; set; }

    public string ОписаниеПроблемы { get; set; } = null!;

    public string ОписаниеПроделаннойРаботы { get; set; } = null!;

    public virtual Сотрудники КодСотрудникаNavigation { get; set; } = null!;

    public virtual Устройства КодУстройстваNavigation { get; set; } = null!;
}
