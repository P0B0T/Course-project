using System;
using System.Collections.Generic;

namespace Курсач.Database;

public partial class Поставщики
{
    public int Код { get; set; }

    public string НазваниеКомпании { get; set; } = null!;

    public string? КонтактноеЛицо { get; set; }

    public string? НомерТелефона { get; set; }

    public string Адрес { get; set; } = null!;

    public string ЭлектроннаяПочта { get; set; } = null!;

    public virtual ICollection<КомплектующиеЗапчасти> КомплектующиеЗапчастиs { get; set; } = new List<КомплектующиеЗапчасти>();
}
