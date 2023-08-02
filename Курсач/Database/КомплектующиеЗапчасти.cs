using System;
using System.Collections.Generic;

namespace Курсач.Database;

public partial class КомплектующиеЗапчасти
{
    public int Код { get; set; }

    public string Название { get; set; } = null!;

    public string? Описание { get; set; }

    public string Производитель { get; set; } = null!;

    public decimal Стоимость { get; set; }

    public int КодПоставщика { get; set; }

    public virtual ICollection<ЗаказыКомплектующих> ЗаказыКомплектующихs { get; set; } = new List<ЗаказыКомплектующих>();

    public virtual Поставщики КодПоставщикаNavigation { get; set; } = null!;
}
