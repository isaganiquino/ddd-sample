USE [master]
GO
/****** Object:  Database [TradingEngine]    Script Date: 22/04/2020 8:09:43 PM ******/
CREATE DATABASE [TradingEngine]

USE [TradingEngine]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 22/04/2020 8:09:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Ratio] [decimal](16, 2) NOT NULL,
 CONSTRAINT [PK_Currency_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 22/04/2020 8:09:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserBalance]    Script Date: 22/04/2020 8:09:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBalance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[Amount] [decimal](16, 2) NOT NULL,
 CONSTRAINT [PK_Balance_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Currency] ON 

INSERT [dbo].[Currency] ([Id], [Name], [Ratio]) VALUES (1, N'USD', CAST(1.00 AS Decimal(16, 2)))
INSERT [dbo].[Currency] ([Id], [Name], [Ratio]) VALUES (2, N'PHP', CAST(50.00 AS Decimal(16, 2)))
SET IDENTITY_INSERT [dbo].[Currency] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [username]) VALUES (1, N'isagani')
INSERT [dbo].[User] ([Id], [username]) VALUES (2, N'jemma')
INSERT [dbo].[User] ([Id], [username]) VALUES (3, N'jianne')
INSERT [dbo].[User] ([Id], [username]) VALUES (5, N'leonarda')
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserBalance] ON 

INSERT [dbo].[UserBalance] ([Id], [UserId], [CurrencyId], [Amount]) VALUES (1, 1, 1, CAST(306.00 AS Decimal(16, 2)))
INSERT [dbo].[UserBalance] ([Id], [UserId], [CurrencyId], [Amount]) VALUES (2, 1, 2, CAST(1270.00 AS Decimal(16, 2)))
INSERT [dbo].[UserBalance] ([Id], [UserId], [CurrencyId], [Amount]) VALUES (7, 2, 1, CAST(310.00 AS Decimal(16, 2)))
SET IDENTITY_INSERT [dbo].[UserBalance] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Username]    Script Date: 22/04/2020 8:09:44 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Username] ON [dbo].[User]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserBalance]  WITH CHECK ADD  CONSTRAINT [FK_Balance_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserBalance] CHECK CONSTRAINT [FK_Balance_User]
GO
USE [master]
GO
ALTER DATABASE [TradingEngine] SET  READ_WRITE 
GO
