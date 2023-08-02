using System;
using System.Collections.Generic;
using System.IO;

namespace Курсач.Database;

public partial class Устройства
{
    public int Код { get; set; }

    public string Модель { get; set; } = null!;

    public string Производитель { get; set; } = null!;

    public string ТипУстройства { get; set; } = null!;

    public short ГодВыпуска { get; set; }

    public int? СерийныйНомер { get; set; }

    public int КодКлиента { get; set; }

    public string? ФотоУстройства { get; set; }

    public string ФотоУстройстваGet
    {
        get
        {
            if (ФотоУстройства == null) return null;
            else
            {
                string namePhoto = Directory.GetCurrentDirectory() + "\\PhotoDevices\\" + ФотоУстройства;
                return namePhoto;
            }
        }
    }

    public virtual Клиенты КодКлиентаNavigation { get; set; } = null!;

    public virtual ICollection<Ремонты> Ремонтыs { get; set; } = new List<Ремонты>();
}
