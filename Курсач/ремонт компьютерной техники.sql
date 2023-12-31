USE [master]
GO
/****** Object:  Database [Ремонт компьютерной техники]    Script Date: 30.06.2023 13:22:53 ******/
CREATE DATABASE [Ремонт компьютерной техники]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ремонт компьютерной техники', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Ремонт компьютерной техники.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Ремонт компьютерной техники_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Ремонт компьютерной техники_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Ремонт компьютерной техники] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Ремонт компьютерной техники].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Ремонт компьютерной техники] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET ARITHABORT OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET  MULTI_USER 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Ремонт компьютерной техники] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Ремонт компьютерной техники] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Ремонт компьютерной техники] SET QUERY_STORE = OFF
GO
USE [Ремонт компьютерной техники]
GO
/****** Object:  UserDefinedFunction [dbo].[GetCountOrdersClient]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetCountOrdersClient] (@Code int)
RETURNS int
AS
BEGIN
	DECLARE @Count int
	SELECT @Count = COUNT(*) FROM Ремонты
	INNER JOIN Устройства ON [Код устройства] = Устройства.Код
	INNER JOIN Клиенты ON [Код клиента] = Клиенты.Код
	WHERE @Code = [Код устройства]
	RETURN @Count
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCountOrdersOfAccessoriesClients]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetCountOrdersOfAccessoriesClients] (@Code int)
RETURNS int
AS
BEGIN
	DECLARE @Count int
	SELECT @Count = COUNT(*) FROM [Заказы комплектующих]
	INNER JOIN Клиенты ON [Код клиента] = Клиенты.Код
	WHERE @Code = [Код клиента]
	RETURN @Count
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCountOrdersStaff]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetCountOrdersStaff] (@Code int)
RETURNS int
AS
BEGIN
	DECLARE @Count int
	SELECT @Count = COUNT(*) FROM Ремонты
	INNER JOIN Сотрудники ON [Код сотрудника] = Сотрудники.Код
	WHERE @Code = [Код сотрудника]
	RETURN @Count
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCountTypeDevices]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetCountTypeDevices] (@Type nvarchar(50))
RETURNS int
AS
BEGIN
	DECLARE @Count int
	SELECT @Count = COUNT(*) FROM Устройства
	WHERE @Type = [Тип устройства]
	RETURN @Count
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetSumOrdersClient]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetSumOrdersClient] (@Code int)
RETURNS money
AS
BEGIN
	DECLARE @Sum money
	SELECT @Sum = SUM(Ремонты.[Стоимость ремонта]) FROM Ремонты
	INNER JOIN Устройства ON [Код устройства] = Устройства.Код
	INNER JOIN Клиенты ON [Код клиента] = Клиенты.Код
	WHERE @Code = [Код клиента]
	RETURN @Sum
END
GO
/****** Object:  Table [dbo].[Ремонты]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ремонты](
	[Код] [int] IDENTITY(1,1) NOT NULL,
	[Код устройства] [int] NOT NULL,
	[Код сотрудника] [int] NOT NULL,
	[Дата приёма] [date] NOT NULL,
	[Дата окончания] [date] NOT NULL,
	[Стоимость ремонта] [money] NOT NULL,
	[Описание проблемы] [nvarchar](500) NOT NULL,
	[Описание проделанной работы] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Ремонты] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Клиенты]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Клиенты](
	[Код] [int] IDENTITY(1,1) NOT NULL,
	[Имя] [nvarchar](50) NOT NULL,
	[Фамилия] [nvarchar](50) NOT NULL,
	[Отчество] [nvarchar](50) NULL,
	[Адрес] [nvarchar](50) NOT NULL,
	[Номер телефона] [nvarchar](50) NOT NULL,
	[Электронная почта] [nvarchar](50) NOT NULL,
	[Код роли] [int] NOT NULL,
	[Логин] [nvarchar](50) NOT NULL,
	[Пароль] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Клиенты] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Сотрудники]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Сотрудники](
	[Код] [int] IDENTITY(1,1) NOT NULL,
	[Имя] [nvarchar](50) NOT NULL,
	[Фамилия] [nvarchar](50) NOT NULL,
	[Отчество] [nvarchar](50) NULL,
	[Стаж] [int] NULL,
	[Должность] [nvarchar](50) NOT NULL,
	[Зарплата] [money] NOT NULL,
	[Дата приёма на работу] [date] NOT NULL,
	[Фотография] [nvarchar](50) NULL,
	[Код роли] [int] NOT NULL,
	[Логин] [nvarchar](50) NOT NULL,
	[Пароль] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Сотрудники] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Устройства]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Устройства](
	[Код] [int] IDENTITY(1,1) NOT NULL,
	[Модель] [nvarchar](50) NOT NULL,
	[Производитель] [nvarchar](50) NOT NULL,
	[Тип устройства] [nvarchar](50) NOT NULL,
	[Год выпуска] [smallint] NOT NULL,
	[Серийный номер] [int] NULL,
	[Код клиента] [int] NOT NULL,
	[Фото устройства] [nvarchar](50) NULL,
 CONSTRAINT [PK_Устройства] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AllInfAboutRepair]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllInfAboutRepair]
AS
SELECT dbo.Клиенты.Имя, dbo.Клиенты.Фамилия, dbo.Клиенты.Отчество, dbo.Устройства.[Тип устройства], dbo.Ремонты.[Дата приёма], dbo.Ремонты.[Дата окончания], dbo.Ремонты.[Стоимость ремонта], 
                  dbo.Ремонты.[Описание проблемы], dbo.Ремонты.[Описание проделанной работы], dbo.Сотрудники.Имя AS [Имя сотрудника], dbo.Сотрудники.Фамилия AS [Фамилия сотрудника], 
                  dbo.Сотрудники.Отчество AS [Отчество сотрудника]
FROM     dbo.Устройства INNER JOIN
                  dbo.Клиенты ON dbo.Устройства.[Код клиента] = dbo.Клиенты.Код INNER JOIN
                  dbo.Сотрудники INNER JOIN
                  dbo.Ремонты ON dbo.Сотрудники.Код = dbo.Ремонты.[Код сотрудника] ON dbo.Устройства.Код = dbo.Ремонты.[Код устройства]
GO
/****** Object:  Table [dbo].[Комплектующие\Запчасти]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Комплектующие\Запчасти](
	[Код] [int] IDENTITY(1,1) NOT NULL,
	[Название] [nvarchar](50) NOT NULL,
	[Описание] [nvarchar](250) NULL,
	[Производитель] [nvarchar](50) NOT NULL,
	[Стоимость] [money] NOT NULL,
	[Код поставщика] [int] NOT NULL,
 CONSTRAINT [PK_Комплектующие] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Заказы комплектующих]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Заказы комплектующих](
	[Код] [int] IDENTITY(1,1) NOT NULL,
	[Код клиента] [int] NOT NULL,
	[Код комплектующего] [int] NOT NULL,
	[Количество] [nvarchar](50) NOT NULL,
	[Стоимость] [money] NOT NULL,
	[Дата заказа] [date] NOT NULL,
	[Статус заказа] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Заказы комплектующих] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Поставщики]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Поставщики](
	[Код] [int] IDENTITY(1,1) NOT NULL,
	[Название компании] [nvarchar](50) NOT NULL,
	[Контактное лицо] [nvarchar](50) NULL,
	[Номер телефона] [nvarchar](50) NULL,
	[Адрес] [nvarchar](50) NOT NULL,
	[Электронная почта] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Поставщики комплектующих] PRIMARY KEY CLUSTERED 
(
	[Код] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AllInfAdoutOrdersAccessories]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllInfAdoutOrdersAccessories]
AS
SELECT dbo.Клиенты.Имя, dbo.Клиенты.Фамилия, dbo.Клиенты.Отчество, dbo.[Комплектующие\Запчасти].Название, dbo.[Комплектующие\Запчасти].Описание, dbo.[Комплектующие\Запчасти].Производитель, 
                  dbo.[Комплектующие\Запчасти].Стоимость, dbo.Поставщики.[Название компании], dbo.[Заказы комплектующих].Количество, dbo.[Заказы комплектующих].Стоимость AS [Стоимость заказа], 
                  dbo.[Заказы комплектующих].[Дата заказа], dbo.[Заказы комплектующих].[Статус заказа]
FROM     dbo.[Заказы комплектующих] INNER JOIN
                  dbo.Клиенты ON dbo.[Заказы комплектующих].[Код клиента] = dbo.Клиенты.Код INNER JOIN
                  dbo.[Комплектующие\Запчасти] ON dbo.[Заказы комплектующих].[Код комплектующего] = dbo.[Комплектующие\Запчасти].Код INNER JOIN
                  dbo.Поставщики ON dbo.[Комплектующие\Запчасти].[Код поставщика] = dbo.Поставщики.Код
GO
/****** Object:  View [dbo].[AccessorsBySuppliers]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AccessorsBySuppliers]
AS
SELECT dbo.[Комплектующие\Запчасти].Название, dbo.[Комплектующие\Запчасти].Описание, dbo.[Комплектующие\Запчасти].Производитель, dbo.[Комплектующие\Запчасти].Стоимость, 
                  dbo.Поставщики.[Название компании]
FROM     dbo.[Комплектующие\Запчасти] INNER JOIN
                  dbo.Поставщики ON dbo.[Комплектующие\Запчасти].[Код поставщика] = dbo.Поставщики.Код
GO
/****** Object:  View [dbo].[DevicesByClients]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DevicesByClients]
AS
SELECT dbo.Клиенты.Имя, dbo.Клиенты.Фамилия, dbo.Клиенты.Отчество, dbo.Устройства.[Тип устройства], dbo.Устройства.Модель
FROM     dbo.Клиенты INNER JOIN
                  dbo.Устройства ON dbo.Клиенты.Код = dbo.Устройства.[Код клиента]
GO
/****** Object:  View [dbo].[DevicesLinkedToEmployees]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DevicesLinkedToEmployees]
AS
SELECT dbo.Сотрудники.Имя, dbo.Сотрудники.Фамилия, dbo.Сотрудники.Отчество, dbo.Устройства.[Тип устройства], dbo.Устройства.Модель
FROM     dbo.Сотрудники INNER JOIN
                  dbo.Ремонты ON dbo.Сотрудники.Код = dbo.Ремонты.[Код сотрудника] INNER JOIN
                  dbo.Устройства ON dbo.Ремонты.[Код устройства] = dbo.Устройства.Код
GO
/****** Object:  Table [dbo].[Роли]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Роли](
	[Код роли] [int] IDENTITY(1,1) NOT NULL,
	[Роль] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Роли] PRIMARY KEY CLUSTERED 
(
	[Код роли] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Заказы комплектующих] ON 

INSERT [dbo].[Заказы комплектующих] ([Код], [Код клиента], [Код комплектующего], [Количество], [Стоимость], [Дата заказа], [Статус заказа]) VALUES (1, 1, 1, N'1 штука', 1000.0000, CAST(N'2023-03-01' AS Date), N'Получен')
INSERT [dbo].[Заказы комплектующих] ([Код], [Код клиента], [Код комплектующего], [Количество], [Стоимость], [Дата заказа], [Статус заказа]) VALUES (2, 2, 3, N'1 штука', 2000.0000, CAST(N'2023-05-01' AS Date), N'Получен')
SET IDENTITY_INSERT [dbo].[Заказы комплектующих] OFF
GO
SET IDENTITY_INSERT [dbo].[Клиенты] ON 

INSERT [dbo].[Клиенты] ([Код], [Имя], [Фамилия], [Отчество], [Адрес], [Номер телефона], [Электронная почта], [Код роли], [Логин], [Пароль]) VALUES (1, N'Егор', N'Давыдов', N'Тимофеевич', N'г. Рязань ул. Крупской д. 11', N'8(927)973-61-56', N'Davidov@mail.ru', 3, N'egor', N'1234567890')
INSERT [dbo].[Клиенты] ([Код], [Имя], [Фамилия], [Отчество], [Адрес], [Номер телефона], [Электронная почта], [Код роли], [Логин], [Пароль]) VALUES (2, N'Варвара', N'Павловна', N'Степановна', N'г. Рязань ул. Новаторов д. 10', N'+7(345)654-23-78', N'PavlovnaV@gmail.com', 3, N'VPS', N'jksdhfjah')
INSERT [dbo].[Клиенты] ([Код], [Имя], [Фамилия], [Отчество], [Адрес], [Номер телефона], [Электронная почта], [Код роли], [Логин], [Пароль]) VALUES (3, N'Дмитрий', N'Попов', N'Даниилович', N'г. Рязань ул. Костычева д. 8', N'+7(965)543-78-65', N'DmitriyP@yandex.ru', 3, N'DPopov', N'mdsng')
INSERT [dbo].[Клиенты] ([Код], [Имя], [Фамилия], [Отчество], [Адрес], [Номер телефона], [Электронная почта], [Код роли], [Логин], [Пароль]) VALUES (4, N'Александр', N'Петров', N'Григорьевич', N'г. Рязань ул. Мервинская д. 119', N'8(976)874-32-12', N'AGPetrov@gmail.com', 3, N'AGPetrov', N'u43563nvhgwhqt4')
INSERT [dbo].[Клиенты] ([Код], [Имя], [Фамилия], [Отчество], [Адрес], [Номер телефона], [Электронная почта], [Код роли], [Логин], [Пароль]) VALUES (5, N'Лев', N'Смирнов', N'Львович', N'г. Рязань ул. Советская д. 142', N'+7(954)789-00-31', N'Lion@yandex.ru', 3, N'Lion123', N'efnjwyj')
SET IDENTITY_INSERT [dbo].[Клиенты] OFF
GO
SET IDENTITY_INSERT [dbo].[Комплектующие\Запчасти] ON 

INSERT [dbo].[Комплектующие\Запчасти] ([Код], [Название], [Описание], [Производитель], [Стоимость], [Код поставщика]) VALUES (1, N'Аккумулятор для ноутбуков HP', N'Литиевый аккумулятор, подходящий для большинства моделей ноутбуков HP', N'HP', 1000.0000, 1)
INSERT [dbo].[Комплектующие\Запчасти] ([Код], [Название], [Описание], [Производитель], [Стоимость], [Код поставщика]) VALUES (2, N'Аккумулятор для ноутбуков ASUS', N'Литиевый аккумулятор, подходящий для большинства моделей ноутбуков ASUS', N'ASUS', 1500.0000, 1)
INSERT [dbo].[Комплектующие\Запчасти] ([Код], [Название], [Описание], [Производитель], [Стоимость], [Код поставщика]) VALUES (3, N'Картиридж для лазерного принтера', NULL, N'Pantum', 2000.0000, 1)
INSERT [dbo].[Комплектующие\Запчасти] ([Код], [Название], [Описание], [Производитель], [Стоимость], [Код поставщика]) VALUES (4, N'Внутренние модули для ноутбука', N'Набор, состоящий из схем, плат, различной электронники для ноутбуков.', N'ASUS', 3000.0000, 2)
INSERT [dbo].[Комплектующие\Запчасти] ([Код], [Название], [Описание], [Производитель], [Стоимость], [Код поставщика]) VALUES (5, N'Плата для сматрфона HONOR', NULL, N'HONOR', 7000.0000, 2)
SET IDENTITY_INSERT [dbo].[Комплектующие\Запчасти] OFF
GO
SET IDENTITY_INSERT [dbo].[Поставщики] ON 

INSERT [dbo].[Поставщики] ([Код], [Название компании], [Контактное лицо], [Номер телефона], [Адрес], [Электронная почта]) VALUES (1, N'MARVEL Дистрибуция', N'Рузаев Андрей Викторович', N'8(978)876-23-41', N'г. Москва, ул. Краснобогатырская, д. 89 стр. 1', N'info@marvel.ru')
INSERT [dbo].[Поставщики] ([Код], [Название компании], [Контактное лицо], [Номер телефона], [Адрес], [Электронная почта]) VALUES (2, N'ARIAT TECH', N'Мамаев Вадим Никитич', N'8(954)632-71-23', N'г. Москва ул. Евремова д.12', N'Info@ariat-tech.com')
SET IDENTITY_INSERT [dbo].[Поставщики] OFF
GO
SET IDENTITY_INSERT [dbo].[Ремонты] ON 

INSERT [dbo].[Ремонты] ([Код], [Код устройства], [Код сотрудника], [Дата приёма], [Дата окончания], [Стоимость ремонта], [Описание проблемы], [Описание проделанной работы]) VALUES (1, 1, 2, CAST(N'2023-03-01' AS Date), CAST(N'2023-03-07' AS Date), 5000.0000, N'Ноутбук не может работать без подключения к сети электропитания.', N'Замена аккамулятора, чистка устройства.')
INSERT [dbo].[Ремонты] ([Код], [Код устройства], [Код сотрудника], [Дата приёма], [Дата окончания], [Стоимость ремонта], [Описание проблемы], [Описание проделанной работы]) VALUES (2, 2, 1, CAST(N'2023-05-01' AS Date), CAST(N'2023-05-04' AS Date), 2000.0000, N'Принтер не включается.', N'Замена и перепайка внутренних микросхем.')
INSERT [dbo].[Ремонты] ([Код], [Код устройства], [Код сотрудника], [Дата приёма], [Дата окончания], [Стоимость ремонта], [Описание проблемы], [Описание проделанной работы]) VALUES (3, 3, 4, CAST(N'2023-04-20' AS Date), CAST(N'2023-04-21' AS Date), 1200.0000, N'Ноутбук сильно греется при работе.', N'Чистка устройства.')
INSERT [dbo].[Ремонты] ([Код], [Код устройства], [Код сотрудника], [Дата приёма], [Дата окончания], [Стоимость ремонта], [Описание проблемы], [Описание проделанной работы]) VALUES (4, 4, 3, CAST(N'2023-03-13' AS Date), CAST(N'2023-03-15' AS Date), 3000.0000, N'Принтер плохо печатает.', N'Замена картриджа.')
INSERT [dbo].[Ремонты] ([Код], [Код устройства], [Код сотрудника], [Дата приёма], [Дата окончания], [Стоимость ремонта], [Описание проблемы], [Описание проделанной работы]) VALUES (5, 5, 5, CAST(N'2023-05-02' AS Date), CAST(N'2023-05-03' AS Date), 10000.0000, N'Сматртфон не включается.', N'Замена перегоревшей платы на новую.')
INSERT [dbo].[Ремонты] ([Код], [Код устройства], [Код сотрудника], [Дата приёма], [Дата окончания], [Стоимость ремонта], [Описание проблемы], [Описание проделанной работы]) VALUES (6, 1, 4, CAST(N'2023-04-20' AS Date), CAST(N'2023-04-21' AS Date), 3000.0000, N'Ноутбук сильно греется при работе.', N'Чистка устройства.')
SET IDENTITY_INSERT [dbo].[Ремонты] OFF
GO
SET IDENTITY_INSERT [dbo].[Роли] ON 

INSERT [dbo].[Роли] ([Код роли], [Роль]) VALUES (1, N'Администратор')
INSERT [dbo].[Роли] ([Код роли], [Роль]) VALUES (2, N'Сотрудник')
INSERT [dbo].[Роли] ([Код роли], [Роль]) VALUES (3, N'Клиент')
SET IDENTITY_INSERT [dbo].[Роли] OFF
GO
SET IDENTITY_INSERT [dbo].[Сотрудники] ON 

INSERT [dbo].[Сотрудники] ([Код], [Имя], [Фамилия], [Отчество], [Стаж], [Должность], [Зарплата], [Дата приёма на работу], [Фотография], [Код роли], [Логин], [Пароль]) VALUES (1, N'Иван', N'Калинин', N'Сергеевич', 8, N'Электрик', 34000.0000, CAST(N'2021-04-01' AS Date), N'Калинин.jpg', 2, N'electrik228', N'huewiygshf')
INSERT [dbo].[Сотрудники] ([Код], [Имя], [Фамилия], [Отчество], [Стаж], [Должность], [Зарплата], [Дата приёма на работу], [Фотография], [Код роли], [Логин], [Пароль]) VALUES (2, N'Валентин', N'Ермаков', N'Андреевич', 12, N'Техник', 50000.0000, CAST(N'2020-09-08' AS Date), N'Ермаков.jpg', 2, N'Ermak123', N'735673hft')
INSERT [dbo].[Сотрудники] ([Код], [Имя], [Фамилия], [Отчество], [Стаж], [Должность], [Зарплата], [Дата приёма на работу], [Фотография], [Код роли], [Логин], [Пароль]) VALUES (3, N'Антон', N'Хомяков', N'Олегович', 10, N'Техник', 50000.0000, CAST(N'2023-03-06' AS Date), N'Хомяков.jpg', 2, N'dgungarik', N'mfhgjkyr4y7326572')
INSERT [dbo].[Сотрудники] ([Код], [Имя], [Фамилия], [Отчество], [Стаж], [Должность], [Зарплата], [Дата приёма на работу], [Фотография], [Код роли], [Логин], [Пароль]) VALUES (4, N'Генадий', N'Авдеев', N'Дмитриевич', NULL, N'Стажёр', 15000.0000, CAST(N'2023-05-06' AS Date), N'Авдеев.jpg', 2, N'AGD', N'jhwruhur')
INSERT [dbo].[Сотрудники] ([Код], [Имя], [Фамилия], [Отчество], [Стаж], [Должность], [Зарплата], [Дата приёма на работу], [Фотография], [Код роли], [Логин], [Пароль]) VALUES (5, N'Глеб', N'Задирако', N'Михайлович', 12, N'Электрик', 35000.0000, CAST(N'2023-05-09' AS Date), N'Задирако.jpg', 2, N'GridiGleb', N'12456hjgdfhhhl')
INSERT [dbo].[Сотрудники] ([Код], [Имя], [Фамилия], [Отчество], [Стаж], [Должность], [Зарплата], [Дата приёма на работу], [Фотография], [Код роли], [Логин], [Пароль]) VALUES (6, N'Олег', N'Касаткин', N'Андреевич', 3, N'Программист', 99999.0000, CAST(N'2020-09-01' AS Date), NULL, 1, N'Admin', N'admin')
SET IDENTITY_INSERT [dbo].[Сотрудники] OFF
GO
SET IDENTITY_INSERT [dbo].[Устройства] ON 

INSERT [dbo].[Устройства] ([Код], [Модель], [Производитель], [Тип устройства], [Год выпуска], [Серийный номер], [Код клиента], [Фото устройства]) VALUES (1, N'Pavilion 15 Gaming', N'HP', N'Ноутбук', 2020, NULL, 1, N'Pavilion15Gaming.jpg')
INSERT [dbo].[Устройства] ([Код], [Модель], [Производитель], [Тип устройства], [Год выпуска], [Серийный номер], [Код клиента], [Фото устройства]) VALUES (2, N'P2516/P2518', N'Pantum', N'Принтер лазерный', 2017, 6574896, 2, N'P2516.jpg')
INSERT [dbo].[Устройства] ([Код], [Модель], [Производитель], [Тип устройства], [Год выпуска], [Серийный номер], [Код клиента], [Фото устройства]) VALUES (3, N'VivoBook R410MA-212.BK128', N'ASUS', N'Ноутбук', 2022, NULL, 3, N'VivoBook.jpg')
INSERT [dbo].[Устройства] ([Код], [Модель], [Производитель], [Тип устройства], [Год выпуска], [Серийный номер], [Код клиента], [Фото устройства]) VALUES (4, N'P2500', N'Pantum', N'Принтер лазерный', 2015, 3254295, 4, NULL)
INSERT [dbo].[Устройства] ([Код], [Модель], [Производитель], [Тип устройства], [Год выпуска], [Серийный номер], [Код клиента], [Фото устройства]) VALUES (5, N'Magic4 Pro', N'HONOR', N'Смартфон', 2023, 8647552, 5, N'Magic4Pro.jpg')
SET IDENTITY_INSERT [dbo].[Устройства] OFF
GO
ALTER TABLE [dbo].[Заказы комплектующих]  WITH CHECK ADD  CONSTRAINT [FK_Заказы комплектующих_Клиенты] FOREIGN KEY([Код клиента])
REFERENCES [dbo].[Клиенты] ([Код])
GO
ALTER TABLE [dbo].[Заказы комплектующих] CHECK CONSTRAINT [FK_Заказы комплектующих_Клиенты]
GO
ALTER TABLE [dbo].[Заказы комплектующих]  WITH CHECK ADD  CONSTRAINT [FK_Заказы комплектующих_Комплектующие] FOREIGN KEY([Код комплектующего])
REFERENCES [dbo].[Комплектующие\Запчасти] ([Код])
GO
ALTER TABLE [dbo].[Заказы комплектующих] CHECK CONSTRAINT [FK_Заказы комплектующих_Комплектующие]
GO
ALTER TABLE [dbo].[Клиенты]  WITH CHECK ADD  CONSTRAINT [FK_Клиенты_Роли] FOREIGN KEY([Код роли])
REFERENCES [dbo].[Роли] ([Код роли])
GO
ALTER TABLE [dbo].[Клиенты] CHECK CONSTRAINT [FK_Клиенты_Роли]
GO
ALTER TABLE [dbo].[Комплектующие\Запчасти]  WITH CHECK ADD  CONSTRAINT [FK_Комплектующие\Запчасти_Поставщики] FOREIGN KEY([Код поставщика])
REFERENCES [dbo].[Поставщики] ([Код])
GO
ALTER TABLE [dbo].[Комплектующие\Запчасти] CHECK CONSTRAINT [FK_Комплектующие\Запчасти_Поставщики]
GO
ALTER TABLE [dbo].[Ремонты]  WITH CHECK ADD  CONSTRAINT [FK_Ремонты_Сотрудники] FOREIGN KEY([Код сотрудника])
REFERENCES [dbo].[Сотрудники] ([Код])
GO
ALTER TABLE [dbo].[Ремонты] CHECK CONSTRAINT [FK_Ремонты_Сотрудники]
GO
ALTER TABLE [dbo].[Ремонты]  WITH CHECK ADD  CONSTRAINT [FK_Ремонты_Устройства] FOREIGN KEY([Код устройства])
REFERENCES [dbo].[Устройства] ([Код])
GO
ALTER TABLE [dbo].[Ремонты] CHECK CONSTRAINT [FK_Ремонты_Устройства]
GO
ALTER TABLE [dbo].[Сотрудники]  WITH CHECK ADD  CONSTRAINT [FK_Сотрудники_Роли] FOREIGN KEY([Код роли])
REFERENCES [dbo].[Роли] ([Код роли])
GO
ALTER TABLE [dbo].[Сотрудники] CHECK CONSTRAINT [FK_Сотрудники_Роли]
GO
ALTER TABLE [dbo].[Устройства]  WITH CHECK ADD  CONSTRAINT [FK_Устройства_Клиенты] FOREIGN KEY([Код клиента])
REFERENCES [dbo].[Клиенты] ([Код])
GO
ALTER TABLE [dbo].[Устройства] CHECK CONSTRAINT [FK_Устройства_Клиенты]
GO
/****** Object:  StoredProcedure [dbo].[AddAccessories]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAccessories] (
@Name nvarchar(50), @Description nvarchar(50), @Manufacturer nvarchar(50),
@Cost money, @SupplierId int)
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO [Комплектующие\Запчасти](Название, Описание, Производитель, Стоимость, [Код поставщика])
	VALUES (@Name, @Description, @Manufacturer, @Cost, @SupplierId)
END
GO
/****** Object:  StoredProcedure [dbo].[AddClient]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddClient] (
@Name nvarchar(50), @Surname nvarchar(50), @Patronymic nvarchar(50),
@Adress nvarchar(50), @Telephonenumber nvarchar(50), @Email nvarchar(50))
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Клиенты(Имя, Фамилия, Отчество, Адрес, [Номер телефона], [Электронная почта])
	VALUES (@Name, @Surname, @Patronymic, @Adress, @Telephonenumber, @Email)
END
GO
/****** Object:  StoredProcedure [dbo].[AddDevice]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddDevice] (
@Model nvarchar(50), @Manufacturer nvarchar(50), @Type nvarchar(50),
@YearOfRelease smallint, @SerialNumber int, @ClientId int, 
@Photo nvarchar(50))
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Устройства(Модель, Производитель, [Тип устройства], [Год выпуска], [Серийный номер], [Код клиента], [Фото устройства])
	VALUES (@Model, @Manufacturer, @Type, @YearOfRelease, @SerialNumber, @ClientId, @Photo)
END
GO
/****** Object:  StoredProcedure [dbo].[AddStaff]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStaff] (
@Name nvarchar(50), @Surname nvarchar(50), @Patronymic nvarchar(50),
@Experience int, @Post nvarchar(50), @Salary money, 
@DateOfEmployment date, @Photo nvarchar(50))
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Сотрудники(Имя, Фамилия, Отчество, Стаж, Должность, Зарплата, [Дата приёма на работу], Фотография)
	VALUES (@Name, @Surname, @Patronymic, @Experience, @Post, @Salary, @DateOfEmployment, @Photo)
END
GO
/****** Object:  StoredProcedure [dbo].[AddSupllier]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSupllier] (
@NameOfCompany nvarchar(50), @ContactPerson nvarchar(50), @Telephonenumber nvarchar(50),
@Adress nvarchar(50), @Email nvarchar(50))
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Поставщики([Название компании], [Контактное лицо], [Номер телефона], Адрес, [Электронная почта])
	VALUES (@NameOfCompany, @ContactPerson, @Telephonenumber, @Adress, @Email)
END
GO
/****** Object:  Trigger [dbo].[InsertedClients]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[InsertedClients]
	ON [dbo].[Клиенты]
	AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Клиенты 
	WHERE Клиенты.Код = (SELECT Код FROM inserted)
END
GO
ALTER TABLE [dbo].[Клиенты] ENABLE TRIGGER [InsertedClients]
GO
/****** Object:  Trigger [dbo].[InsertedAccessories]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[InsertedAccessories]
ON [dbo].[Комплектующие\Запчасти]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM [Комплектующие\Запчасти] 
	WHERE [Комплектующие\Запчасти].Код = (SELECT Код FROM inserted)
END
GO
ALTER TABLE [dbo].[Комплектующие\Запчасти] ENABLE TRIGGER [InsertedAccessories]
GO
/****** Object:  Trigger [dbo].[InsertedSuppliers]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[InsertedSuppliers]
ON [dbo].[Поставщики]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Поставщики 
	WHERE Поставщики.Код = (SELECT Код FROM inserted)
END
GO
ALTER TABLE [dbo].[Поставщики] ENABLE TRIGGER [InsertedSuppliers]
GO
/****** Object:  Trigger [dbo].[InsertedStaff]    Script Date: 30.06.2023 13:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[InsertedStaff]
ON [dbo].[Сотрудники]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Сотрудники 
	WHERE Сотрудники.Код = (SELECT Код FROM inserted)
END
GO
ALTER TABLE [dbo].[Сотрудники] ENABLE TRIGGER [InsertedStaff]
GO
/****** Object:  Trigger [dbo].[InsertedDevices]    Script Date: 30.06.2023 13:22:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[InsertedDevices]
ON [dbo].[Устройства]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Устройства 
	WHERE Устройства.Код = (SELECT Код FROM inserted)
END
GO
ALTER TABLE [dbo].[Устройства] ENABLE TRIGGER [InsertedDevices]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Комплектующие\Запчасти"
            Begin Extent = 
               Top = 58
               Left = 189
               Bottom = 221
               Right = 397
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Поставщики"
            Begin Extent = 
               Top = 54
               Left = 567
               Bottom = 217
               Right = 805
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccessorsBySuppliers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccessorsBySuppliers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Устройства"
            Begin Extent = 
               Top = 118
               Left = 360
               Bottom = 281
               Right = 578
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Клиенты"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Сотрудники"
            Begin Extent = 
               Top = 48
               Left = 1103
               Bottom = 211
               Right = 1361
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Ремонты"
            Begin Extent = 
               Top = 29
               Left = 681
               Bottom = 192
               Right = 1001
            End
            DisplayFlags = 280
            TopColumn = 4
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2088
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AllInfAboutRepair'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AllInfAboutRepair'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Заказы комплектующих"
            Begin Extent = 
               Top = 39
               Left = 312
               Bottom = 202
               Right = 556
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Клиенты"
            Begin Extent = 
               Top = 36
               Left = 36
               Bottom = 199
               Right = 265
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Комплектующие\Запчасти"
            Begin Extent = 
               Top = 38
               Left = 664
               Bottom = 201
               Right = 872
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Поставщики"
            Begin Extent = 
               Top = 10
               Left = 950
               Bottom = 173
               Right = 1188
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AllInfAdoutOrdersAccessories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AllInfAdoutOrdersAccessories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Клиенты"
            Begin Extent = 
               Top = 10
               Left = 259
               Bottom = 260
               Right = 488
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Устройства"
            Begin Extent = 
               Top = 13
               Left = 656
               Bottom = 269
               Right = 874
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DevicesByClients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DevicesByClients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Сотрудники"
            Begin Extent = 
               Top = 35
               Left = 38
               Bottom = 318
               Right = 296
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Ремонты"
            Begin Extent = 
               Top = 29
               Left = 371
               Bottom = 287
               Right = 691
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Устройства"
            Begin Extent = 
               Top = 31
               Left = 835
               Bottom = 291
               Right = 1053
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DevicesLinkedToEmployees'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DevicesLinkedToEmployees'
GO
USE [master]
GO
ALTER DATABASE [Ремонт компьютерной техники] SET  READ_WRITE 
GO
