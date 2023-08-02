using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Курсач.Database;

public partial class РемонтКомпьютернойТехникиContext : DbContext
{
    public РемонтКомпьютернойТехникиContext()
    {
    }

    public РемонтКомпьютернойТехникиContext(DbContextOptions<РемонтКомпьютернойТехникиContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ЗаказыКомплектующих> ЗаказыКомплектующихs { get; set; }

    public virtual DbSet<Клиенты> Клиентыs { get; set; }

    public virtual DbSet<КомплектующиеЗапчасти> КомплектующиеЗапчастиs { get; set; }

    public virtual DbSet<Поставщики> Поставщикиs { get; set; }

    public virtual DbSet<Ремонты> Ремонтыs { get; set; }

    public virtual DbSet<Роли> Ролиs { get; set; }

    public virtual DbSet<Сотрудники> Сотрудникиs { get; set; }

    public virtual DbSet<Устройства> Устройстваs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Trusted_Connection = true;TrustServerCertificate = true;Database=Ремонт компьютерной техники");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ЗаказыКомплектующих>(entity =>
        {
            entity.HasKey(e => e.Код);

            entity.ToTable("Заказы комплектующих");

            entity.Property(e => e.ДатаЗаказа)
                .HasColumnType("date")
                .HasColumnName("Дата заказа");
            entity.Property(e => e.КодКлиента).HasColumnName("Код клиента");
            entity.Property(e => e.КодКомплектующего).HasColumnName("Код комплектующего");
            entity.Property(e => e.Количество).HasMaxLength(50);
            entity.Property(e => e.СтатусЗаказа)
                .HasMaxLength(50)
                .HasColumnName("Статус заказа");
            entity.Property(e => e.Стоимость).HasColumnType("money");

            entity.HasOne(d => d.КодКлиентаNavigation).WithMany(p => p.ЗаказыКомплектующихs)
                .HasForeignKey(d => d.КодКлиента)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Заказы комплектующих_Клиенты");

            entity.HasOne(d => d.КодКомплектующегоNavigation).WithMany(p => p.ЗаказыКомплектующихs)
                .HasForeignKey(d => d.КодКомплектующего)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Заказы комплектующих_Комплектующие");
        });

        modelBuilder.Entity<Клиенты>(entity =>
        {
            entity.HasKey(e => e.Код);

            entity.ToTable("Клиенты", tb => tb.HasTrigger("InsertedClients"));

            entity.Property(e => e.Адрес).HasMaxLength(50);
            entity.Property(e => e.Имя).HasMaxLength(50);
            entity.Property(e => e.КодРоли).HasColumnName("Код роли");
            entity.Property(e => e.НомерТелефона)
                .HasMaxLength(50)
                .HasColumnName("Номер телефона");
            entity.Property(e => e.Отчество).HasMaxLength(50);
            entity.Property(e => e.Фамилия).HasMaxLength(50);
            entity.Property(e => e.ЭлектроннаяПочта)
                .HasMaxLength(50)
                .HasColumnName("Электронная почта");

            entity.HasOne(d => d.КодРолиNavigation).WithMany(p => p.Клиентыs)
                .HasForeignKey(d => d.КодРоли)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Клиенты_Роли");
        });

        modelBuilder.Entity<КомплектующиеЗапчасти>(entity =>
        {
            entity.HasKey(e => e.Код).HasName("PK_Комплектующие");

            entity.ToTable("Комплектующие\\Запчасти", tb => tb.HasTrigger("InsertedAccessories"));

            entity.Property(e => e.КодПоставщика).HasColumnName("Код поставщика");
            entity.Property(e => e.Название).HasMaxLength(50);
            entity.Property(e => e.Описание).HasMaxLength(250);
            entity.Property(e => e.Производитель).HasMaxLength(50);
            entity.Property(e => e.Стоимость).HasColumnType("money");

            entity.HasOne(d => d.КодПоставщикаNavigation).WithMany(p => p.КомплектующиеЗапчастиs)
                .HasForeignKey(d => d.КодПоставщика)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Комплектующие\\Запчасти_Поставщики");
        });

        modelBuilder.Entity<Поставщики>(entity =>
        {
            entity.HasKey(e => e.Код).HasName("PK_Поставщики комплектующих");

            entity.ToTable("Поставщики", tb => tb.HasTrigger("InsertedSuppliers"));

            entity.Property(e => e.Адрес).HasMaxLength(50);
            entity.Property(e => e.КонтактноеЛицо)
                .HasMaxLength(50)
                .HasColumnName("Контактное лицо");
            entity.Property(e => e.НазваниеКомпании)
                .HasMaxLength(50)
                .HasColumnName("Название компании");
            entity.Property(e => e.НомерТелефона)
                .HasMaxLength(50)
                .HasColumnName("Номер телефона");
            entity.Property(e => e.ЭлектроннаяПочта)
                .HasMaxLength(50)
                .HasColumnName("Электронная почта");
        });

        modelBuilder.Entity<Ремонты>(entity =>
        {
            entity.HasKey(e => e.Код);

            entity.ToTable("Ремонты");

            entity.Property(e => e.ДатаОкончания)
                .HasColumnType("date")
                .HasColumnName("Дата окончания");
            entity.Property(e => e.ДатаПриёма)
                .HasColumnType("date")
                .HasColumnName("Дата приёма");
            entity.Property(e => e.КодСотрудника).HasColumnName("Код сотрудника");
            entity.Property(e => e.КодУстройства).HasColumnName("Код устройства");
            entity.Property(e => e.ОписаниеПроблемы)
                .HasMaxLength(500)
                .HasColumnName("Описание проблемы");
            entity.Property(e => e.ОписаниеПроделаннойРаботы)
                .HasMaxLength(500)
                .HasColumnName("Описание проделанной работы");
            entity.Property(e => e.СтоимостьРемонта)
                .HasColumnType("money")
                .HasColumnName("Стоимость ремонта");

            entity.HasOne(d => d.КодСотрудникаNavigation).WithMany(p => p.Ремонтыs)
                .HasForeignKey(d => d.КодСотрудника)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ремонты_Сотрудники");

            entity.HasOne(d => d.КодУстройстваNavigation).WithMany(p => p.Ремонтыs)
                .HasForeignKey(d => d.КодУстройства)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ремонты_Устройства");
        });

        modelBuilder.Entity<Роли>(entity =>
        {
            entity.HasKey(e => e.КодРоли);

            entity.ToTable("Роли");

            entity.Property(e => e.КодРоли).HasColumnName("Код роли");
            entity.Property(e => e.Роль).HasMaxLength(50);
        });

        modelBuilder.Entity<Сотрудники>(entity =>
        {
            entity.HasKey(e => e.Код);

            entity.ToTable("Сотрудники", tb => tb.HasTrigger("InsertedStaff"));

            entity.Property(e => e.ДатаПриёмаНаРаботу)
                .HasColumnType("date")
                .HasColumnName("Дата приёма на работу");
            entity.Property(e => e.Должность).HasMaxLength(50);
            entity.Property(e => e.Зарплата).HasColumnType("money");
            entity.Property(e => e.Имя).HasMaxLength(50);
            entity.Property(e => e.КодРоли).HasColumnName("Код роли");
            entity.Property(e => e.Отчество).HasMaxLength(50);
            entity.Property(e => e.Фамилия).HasMaxLength(50);
            entity.Property(e => e.Фотография).HasMaxLength(50);

            entity.HasOne(d => d.КодРолиNavigation).WithMany(p => p.Сотрудникиs)
                .HasForeignKey(d => d.КодРоли)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Сотрудники_Роли");
        });

        modelBuilder.Entity<Устройства>(entity =>
        {
            entity.HasKey(e => e.Код);

            entity.ToTable("Устройства", tb => tb.HasTrigger("InsertedDevices"));

            entity.Property(e => e.ГодВыпуска).HasColumnName("Год выпуска");
            entity.Property(e => e.КодКлиента).HasColumnName("Код клиента");
            entity.Property(e => e.Модель).HasMaxLength(50);
            entity.Property(e => e.Производитель).HasMaxLength(50);
            entity.Property(e => e.СерийныйНомер).HasColumnName("Серийный номер");
            entity.Property(e => e.ТипУстройства)
                .HasMaxLength(50)
                .HasColumnName("Тип устройства");
            entity.Property(e => e.ФотоУстройства)
                .HasMaxLength(50)
                .HasColumnName("Фото устройства");

            entity.HasOne(d => d.КодКлиентаNavigation).WithMany(p => p.Устройстваs)
                .HasForeignKey(d => d.КодКлиента)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Устройства_Клиенты");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
