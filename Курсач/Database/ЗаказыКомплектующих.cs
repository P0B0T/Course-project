using System;
using System.Collections.Generic;

namespace Курсач.Database;

public partial class ЗаказыКомплектующих
{
    public int Код { get; set; }

    public int КодКлиента { get; set; }

    public int КодКомплектующего { get; set; }

    public string Количество { get; set; } = null!;

    public decimal Стоимость { get; set; }

    public DateTime ДатаЗаказа { get; set; }

    public string СтатусЗаказа { get; set; } = null!;

    public virtual Клиенты КодКлиентаNavigation { get; set; } = null!;

    public virtual КомплектующиеЗапчасти КодКомплектующегоNavigation { get; set; } = null!;
}
