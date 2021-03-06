USE [NIKE]
GO
/****** Object:  Table [dbo].[Goods]    Script Date: 2018/9/16 14:26:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goods](
	[GoodsID] [int] IDENTITY(1,1) NOT NULL,
	[BarCode] [nvarchar](6) NOT NULL,
	[TypeID] [int] NOT NULL,
	[GoodsName] [nvarchar](50) NOT NULL,
	[StorePrice] [decimal](8, 2) NULL,
	[SalePrice] [decimal](8, 2) NULL,
	[Discount] [decimal](4, 2) NOT NULL,
	[StockNum] [int] NULL,
	[StockDate] [datetime] NULL,
 CONSTRAINT [PK_Goods] PRIMARY KEY CLUSTERED 
(
	[GoodsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sales]    Script Date: 2018/9/16 14:26:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[SalesID] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptsCode] [nvarchar](14) NULL,
	[SalesDate] [date] NOT NULL,
	[Amount] [decimal](8, 0) NULL,
	[SalesmanID] [int] NULL,
	[CashierID] [int] NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[SalesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesDetail]    Script Date: 2018/9/16 14:26:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesDetail](
	[SDID] [int] IDENTITY(1,1) NOT NULL,
	[SalesID] [int] NOT NULL,
	[GoodsID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Amountlone] [decimal](8, 2) NULL,
 CONSTRAINT [PK_SalesDetail] PRIMARY KEY CLUSTERED 
(
	[SDID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Salesman]    Script Date: 2018/9/16 14:26:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salesman](
	[SalessmanID] [int] IDENTITY(1,1) NOT NULL,
	[Salesman-Name] [nvarchar](10) NOT NULL,
	[Mobile] [nvarchar](20) NOT NULL,
	[Pwd] [nvarchar](20) NOT NULL,
	[Gender] [nvarchar](2) NOT NULL,
	[Wage] [decimal](8, 2) NULL,
	[CommissonRate] [decimal](8, 2) NULL,
	[Role] [nvarchar](20) NULL,
 CONSTRAINT [PK_Salesman] PRIMARY KEY CLUSTERED 
(
	[SalessmanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Type]    Script Date: 2018/9/16 14:26:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Goods] ON 

INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (1, N'213212', 4, N'ZOOM HYPERFRANCHISE XD 男子篮球鞋', CAST(550.00 AS Decimal(8, 2)), CAST(1099.00 AS Decimal(8, 2)), CAST(0.85 AS Decimal(4, 2)), 29, CAST(0x0000A1EA00DD4E4C AS DateTime))
INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (2, N'423423', 4, N'JORDAN COURT AC.1男子男球鞋', CAST(420.00 AS Decimal(8, 2)), CAST(649.00 AS Decimal(8, 2)), CAST(1.00 AS Decimal(4, 2)), 30, CAST(0x0000A1EA00DD4E4C AS DateTime))
INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (3, N'423465', 5, N'BOMBA II男子足球鞋', CAST(330.00 AS Decimal(8, 2)), CAST(529.00 AS Decimal(8, 2)), CAST(0.90 AS Decimal(4, 2)), 25, CAST(0x0000A1EA00DD4E4C AS DateTime))
INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (4, N'545334', 5, N'MERCURIAL VELOCE AG 男子跑步鞋', CAST(580.00 AS Decimal(8, 2)), CAST(999.00 AS Decimal(8, 2)), CAST(0.90 AS Decimal(4, 2)), 19, CAST(0x0000A1EA00DD4E4C AS DateTime))
INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (5, N'364643', 6, N'FREE 5.0+ 男子跑步鞋', CAST(518.00 AS Decimal(8, 2)), CAST(869.00 AS Decimal(8, 2)), CAST(0.80 AS Decimal(4, 2)), 45, CAST(0x0000A1EA00DD4E4C AS DateTime))
INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (6, N'553311', 6, N'LUNARGLIDE+4男子跑步鞋', CAST(480.00 AS Decimal(8, 2)), CAST(969.00 AS Decimal(8, 2)), CAST(0.88 AS Decimal(4, 2)), 29, CAST(0x0000A1EA00DD4E4C AS DateTime))
INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (7, N'888888', 6, N'酸酸乳跑步鞋', CAST(300.00 AS Decimal(8, 2)), CAST(800.00 AS Decimal(8, 2)), CAST(0.80 AS Decimal(4, 2)), 181, CAST(0x0000A9010029A126 AS DateTime))
INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (8, N'000000', 4, N'XUZONGLIN', CAST(5000.00 AS Decimal(8, 2)), CAST(8000.00 AS Decimal(8, 2)), CAST(0.80 AS Decimal(4, 2)), 85, CAST(0x0000A9150130C866 AS DateTime))
INSERT [dbo].[Goods] ([GoodsID], [BarCode], [TypeID], [GoodsName], [StorePrice], [SalePrice], [Discount], [StockNum], [StockDate]) VALUES (9, N'001001', 6, N'中百NIKE', CAST(500.00 AS Decimal(8, 2)), CAST(1000.00 AS Decimal(8, 2)), CAST(0.80 AS Decimal(4, 2)), 89, CAST(0x0000A95D00C0FBC6 AS DateTime))
SET IDENTITY_INSERT [dbo].[Goods] OFF
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([SalesID], [ReceiptsCode], [SalesDate], [Amount], [SalesmanID], [CashierID]) VALUES (49, N'20180916142317', CAST(0xB83E0B00 AS Date), CAST(7040 AS Decimal(8, 0)), 1, 2)
INSERT [dbo].[Sales] ([SalesID], [ReceiptsCode], [SalesDate], [Amount], [SalesmanID], [CashierID]) VALUES (50, N'20180916142333', CAST(0xB83E0B00 AS Date), CAST(1440 AS Decimal(8, 0)), 1, 2)
INSERT [dbo].[Sales] ([SalesID], [ReceiptsCode], [SalesDate], [Amount], [SalesmanID], [CashierID]) VALUES (51, N'20180916142358', CAST(0xB83E0B00 AS Date), CAST(7840 AS Decimal(8, 0)), 3, 2)
INSERT [dbo].[Sales] ([SalesID], [ReceiptsCode], [SalesDate], [Amount], [SalesmanID], [CashierID]) VALUES (52, N'20180916142415', CAST(0xB83E0B00 AS Date), CAST(1440 AS Decimal(8, 0)), 3, 2)
SET IDENTITY_INSERT [dbo].[Sales] OFF
SET IDENTITY_INSERT [dbo].[SalesDetail] ON 

INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (61, 49, 8, 1, CAST(6400.00 AS Decimal(8, 2)))
INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (62, 49, 7, 1, CAST(640.00 AS Decimal(8, 2)))
INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (63, 50, 9, 1, CAST(800.00 AS Decimal(8, 2)))
INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (64, 50, 7, 1, CAST(640.00 AS Decimal(8, 2)))
INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (65, 51, 8, 1, CAST(6400.00 AS Decimal(8, 2)))
INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (66, 51, 7, 1, CAST(640.00 AS Decimal(8, 2)))
INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (67, 51, 9, 1, CAST(800.00 AS Decimal(8, 2)))
INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (68, 52, 9, 1, CAST(800.00 AS Decimal(8, 2)))
INSERT [dbo].[SalesDetail] ([SDID], [SalesID], [GoodsID], [Quantity], [Amountlone]) VALUES (69, 52, 7, 1, CAST(640.00 AS Decimal(8, 2)))
SET IDENTITY_INSERT [dbo].[SalesDetail] OFF
SET IDENTITY_INSERT [dbo].[Salesman] ON 

INSERT [dbo].[Salesman] ([SalessmanID], [Salesman-Name], [Mobile], [Pwd], [Gender], [Wage], [CommissonRate], [Role]) VALUES (1, N'韩树清', N'13912345678', N'A123', N'男', CAST(10000.00 AS Decimal(8, 2)), CAST(0.02 AS Decimal(8, 2)), N'导购员')
INSERT [dbo].[Salesman] ([SalessmanID], [Salesman-Name], [Mobile], [Pwd], [Gender], [Wage], [CommissonRate], [Role]) VALUES (2, N'张晓静', N'13812345555', N'B456', N'女', CAST(30000.00 AS Decimal(8, 2)), CAST(0.02 AS Decimal(8, 2)), N'店长')
INSERT [dbo].[Salesman] ([SalessmanID], [Salesman-Name], [Mobile], [Pwd], [Gender], [Wage], [CommissonRate], [Role]) VALUES (3, N'刘晓慧', N'15912345678', N'C789', N'女', CAST(10000.00 AS Decimal(8, 2)), CAST(0.02 AS Decimal(8, 2)), N'导购员')
INSERT [dbo].[Salesman] ([SalessmanID], [Salesman-Name], [Mobile], [Pwd], [Gender], [Wage], [CommissonRate], [Role]) VALUES (4, N'李春波', N'13625718726', N'D789', N'女', CAST(8000.00 AS Decimal(8, 2)), CAST(0.01 AS Decimal(8, 2)), N'收银员')
INSERT [dbo].[Salesman] ([SalessmanID], [Salesman-Name], [Mobile], [Pwd], [Gender], [Wage], [CommissonRate], [Role]) VALUES (5, N'汪郑辉', N'10086', N'0000', N'男', CAST(10000.00 AS Decimal(8, 2)), CAST(0.30 AS Decimal(8, 2)), N'店长')
SET IDENTITY_INSERT [dbo].[Salesman] OFF
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (1, N'鞋类', 0)
INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (2, N'服装', 0)
INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (3, N'装备', 0)
INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (4, N'篮球鞋', 1)
INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (5, N'足球鞋', 1)
INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (6, N'跑步鞋', 1)
INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (7, N'外套', 2)
INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (8, N'T恤', 2)
INSERT [dbo].[Type] ([TypeID], [TypeName], [ParentID]) VALUES (9, N'运动短袖', 2)
SET IDENTITY_INSERT [dbo].[Type] OFF
ALTER TABLE [dbo].[Goods] ADD  CONSTRAINT [DF_Goods_Discount]  DEFAULT ((1)) FOR [Discount]
GO
ALTER TABLE [dbo].[Goods] ADD  CONSTRAINT [DF_Goods_StockDate]  DEFAULT (getdate()) FOR [StockDate]
GO
ALTER TABLE [dbo].[Sales] ADD  CONSTRAINT [DF_Sales_SalesDate]  DEFAULT (getdate()) FOR [SalesDate]
GO
ALTER TABLE [dbo].[Goods]  WITH CHECK ADD  CONSTRAINT [FK_Goods_Goods] FOREIGN KEY([TypeID])
REFERENCES [dbo].[Type] ([TypeID])
GO
ALTER TABLE [dbo].[Goods] CHECK CONSTRAINT [FK_Goods_Goods]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Sales] FOREIGN KEY([SalesmanID])
REFERENCES [dbo].[Salesman] ([SalessmanID])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Sales]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Salesman] FOREIGN KEY([SalesmanID])
REFERENCES [dbo].[Salesman] ([SalessmanID])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Salesman]
GO
ALTER TABLE [dbo].[SalesDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesDetail_Goods] FOREIGN KEY([GoodsID])
REFERENCES [dbo].[Goods] ([GoodsID])
GO
ALTER TABLE [dbo].[SalesDetail] CHECK CONSTRAINT [FK_SalesDetail_Goods]
GO
ALTER TABLE [dbo].[SalesDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesDetail_SalesDetail] FOREIGN KEY([SalesID])
REFERENCES [dbo].[Sales] ([SalesID])
GO
ALTER TABLE [dbo].[SalesDetail] CHECK CONSTRAINT [FK_SalesDetail_SalesDetail]
GO
