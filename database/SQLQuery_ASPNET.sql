USE [BelanjaYukDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LtCategory]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LtCategory](
	[IdCategory] [nvarchar](36) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LtCategory] PRIMARY KEY CLUSTERED 
(
	[IdCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LtGender]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LtGender](
	[IdGender] [nvarchar](36) NOT NULL,
	[GenderName] [nvarchar](50) NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LtGender] PRIMARY KEY CLUSTERED 
(
	[IdGender] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LtPayment]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LtPayment](
	[IdPayment] [nvarchar](36) NOT NULL,
	[PaymentName] [nvarchar](100) NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LtPayment] PRIMARY KEY CLUSTERED 
(
	[IdPayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MsProduct]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsProduct](
	[IdProduct] [nvarchar](36) NOT NULL,
	[IdUserSeller] [nvarchar](36) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[ProductDesc] [nvarchar](2000) NULL,
	[IdCategory] [nvarchar](36) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DiscountPercent] [decimal](5, 2) NOT NULL,
	[Qty] [int] NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_MsProduct] PRIMARY KEY CLUSTERED 
(
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MsUser]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsUser](
	[IdUser] [nvarchar](36) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](200) NULL,
	[DOB] [datetime2](7) NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
	[IdGender] [nvarchar](36) NULL,
 CONSTRAINT [PK_MsUser] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MsUserPassword]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsUserPassword](
	[IdUserPassword] [nvarchar](36) NOT NULL,
	[IdUser] [nvarchar](36) NOT NULL,
	[PasswordHash] [nvarchar](200) NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](max) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_MsUserPassword] PRIMARY KEY CLUSTERED 
(
	[IdUserPassword] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MsUserSeller]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsUserSeller](
	[IdUserSeller] [nvarchar](36) NOT NULL,
	[IdUser] [nvarchar](36) NOT NULL,
	[SellerName] [nvarchar](100) NOT NULL,
	[SellerDesc] [nvarchar](1000) NULL,
	[Address] [nvarchar](500) NULL,
	[SellerCode] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_MsUserSeller] PRIMARY KEY CLUSTERED 
(
	[IdUserSeller] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrBuyerCart]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrBuyerCart](
	[IdBuyerCart] [nvarchar](36) NOT NULL,
	[IdUser] [nvarchar](36) NOT NULL,
	[IdProduct] [nvarchar](36) NOT NULL,
	[Qty] [int] NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TrBuyerCart] PRIMARY KEY CLUSTERED 
(
	[IdBuyerCart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrBuyerTransaction]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrBuyerTransaction](
	[IdBuyerTransaction] [nvarchar](36) NOT NULL,
	[IdUser] [nvarchar](36) NOT NULL,
	[IdPayment] [nvarchar](36) NOT NULL,
	[FinalPrice] [decimal](18, 2) NOT NULL,
	[Rating] [int] NULL,
	[RatingComment] [nvarchar](1000) NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TrBuyerTransaction] PRIMARY KEY CLUSTERED 
(
	[IdBuyerTransaction] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrBuyerTransactionDetail]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrBuyerTransactionDetail](
	[IdBuyerTransactionDetail] [nvarchar](36) NOT NULL,
	[IdBuyerTransaction] [nvarchar](36) NOT NULL,
	[IdProduct] [nvarchar](36) NOT NULL,
	[Qty] [int] NOT NULL,
	[PriceProduct] [decimal](18, 2) NOT NULL,
	[DiscountProduct] [decimal](18, 2) NULL,
	[Rating] [int] NULL,
	[RatingComment] [nvarchar](1000) NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TrBuyerTransactionDetail] PRIMARY KEY CLUSTERED 
(
	[IdBuyerTransactionDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrHomeAddress]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrHomeAddress](
	[IdHomeAddress] [nvarchar](36) NOT NULL,
	[IdUser] [nvarchar](36) NOT NULL,
	[Provinsi] [nvarchar](200) NOT NULL,
	[KotaKabupaten] [nvarchar](200) NOT NULL,
	[Kecamatan] [nvarchar](200) NOT NULL,
	[KodePos] [nvarchar](10) NOT NULL,
	[HomeAddressDesc] [nvarchar](2000) NULL,
	[IsPrimaryAddress] [bit] NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](max) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TrHomeAddress] PRIMARY KEY CLUSTERED 
(
	[IdHomeAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrProductImages]    Script Date: 11/1/2025 11:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrProductImages](
	[IdProductImages] [nvarchar](36) NOT NULL,
	[IdProduct] [nvarchar](36) NOT NULL,
	[ProductImage] [nvarchar](max) NOT NULL,
	[DateIn] [datetime2](7) NOT NULL,
	[UserIn] [nvarchar](36) NULL,
	[DateUp] [datetime2](7) NULL,
	[UserUp] [nvarchar](36) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TrProductImages] PRIMARY KEY CLUSTERED 
(
	[IdProductImages] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20251030143957_InitClean', N'8.0.21')
GO
INSERT [dbo].[LtCategory] ([IdCategory], [CategoryName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'cat-elektronik', N'Elektronik', CAST(N'2025-10-30T21:41:03.5200000' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtCategory] ([IdCategory], [CategoryName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'cat-fashion', N'Fashion', CAST(N'2025-10-30T21:41:03.5200000' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtCategory] ([IdCategory], [CategoryName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'cat-lainnya', N'Lainnya', CAST(N'2025-11-01T02:10:48.5766667' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtCategory] ([IdCategory], [CategoryName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'cat-makanan', N'Makanan & Minuman', CAST(N'2025-10-30T21:41:03.5200000' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtCategory] ([IdCategory], [CategoryName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'cat-olahraga', N'Olahraga', CAST(N'2025-11-01T02:10:48.5766667' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtCategory] ([IdCategory], [CategoryName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'cat-rumahTangga', N'Rumah Tangga', CAST(N'2025-11-01T02:10:48.5766667' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtGender] ([IdGender], [GenderName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'id-gender-laki', N'Laki-laki', CAST(N'2025-10-30T21:40:46.8566667' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtGender] ([IdGender], [GenderName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'id-gender-perempuan', N'Perempuan', CAST(N'2025-10-30T21:40:46.8566667' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtPayment] ([IdPayment], [PaymentName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'pay-cod', N'COD (Bayar di Tempat)', CAST(N'2025-10-30T21:41:08.4900000' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[LtPayment] ([IdPayment], [PaymentName], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'pay-transfer', N'Transfer Bank', CAST(N'2025-10-30T21:41:08.4900000' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MsProduct] ([IdProduct], [IdUserSeller], [ProductName], [ProductDesc], [IdCategory], [Price], [DiscountPercent], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'0f7a9944-31fc-4346-ab97-c6520da73954', N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'Sepatu Nike', N'Sepatu lari terbaik', N'cat-fashion', CAST(1500000.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(5, 2)), 3, CAST(N'2025-11-01T22:19:18.1596887' AS DateTime2), NULL, CAST(N'2025-11-01T22:20:33.6171340' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[MsProduct] ([IdProduct], [IdUserSeller], [ProductName], [ProductDesc], [IdCategory], [Price], [DiscountPercent], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'232cdc95-aa6e-449e-a838-4ce8e667073a', N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'Kulkas', N'Kulkas 2 pintu', N'cat-rumahTangga', CAST(8000000.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(5, 2)), 4, CAST(N'2025-11-01T22:42:24.2512315' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MsProduct] ([IdProduct], [IdUserSeller], [ProductName], [ProductDesc], [IdCategory], [Price], [DiscountPercent], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'3261127a-73f1-4db9-983f-81a387029b4e', N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'Xiaomi 14', N'Smarthphone terbaik saat ini', N'cat-elektronik', CAST(8000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(5, 2)), 8, CAST(N'2025-11-01T10:01:18.0517817' AS DateTime2), NULL, CAST(N'2025-11-01T13:19:24.6615188' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[MsProduct] ([IdProduct], [IdUserSeller], [ProductName], [ProductDesc], [IdCategory], [Price], [DiscountPercent], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'3dd621df-7e39-495c-8aa0-4d44fa4fee8b', N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'Chitato ayam panggang', N'Snack rasa ayam panggang', N'cat-makanan', CAST(12000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(5, 2)), 5, CAST(N'2025-11-01T22:22:09.0825509' AS DateTime2), NULL, CAST(N'2025-11-01T22:31:39.8766382' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[MsProduct] ([IdProduct], [IdUserSeller], [ProductName], [ProductDesc], [IdCategory], [Price], [DiscountPercent], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'4cfaedbf-ecb5-491f-b0db-201b4afcb375', N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'Sepatu Nike', N'Sepatu lari dengan kualitas terbaik', N'cat-fashion', CAST(1500000.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(5, 2)), 3, CAST(N'2025-11-01T22:07:34.3960521' AS DateTime2), NULL, CAST(N'2025-11-01T22:09:02.1945540' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[MsProduct] ([IdProduct], [IdUserSeller], [ProductName], [ProductDesc], [IdCategory], [Price], [DiscountPercent], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'55192497-5d85-4a43-803d-1eef3b9a7539', N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'Laptop lenovo Legion', N'Laptop dengan processor core i9', N'cat-elektronik', CAST(20000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(5, 2)), 5, CAST(N'2025-11-01T08:31:43.8659260' AS DateTime2), NULL, CAST(N'2025-11-01T10:31:25.3639181' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[MsProduct] ([IdProduct], [IdUserSeller], [ProductName], [ProductDesc], [IdCategory], [Price], [DiscountPercent], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'a9bbb816-b876-4a3b-9ac8-74759d6a3e24', N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'Bola basket', N'Bola basket', N'cat-olahraga', CAST(100000.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(5, 2)), 7, CAST(N'2025-11-01T22:43:46.8381987' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MsProduct] ([IdProduct], [IdUserSeller], [ProductName], [ProductDesc], [IdCategory], [Price], [DiscountPercent], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'f975867a-2737-4cc9-9144-41fbdc09bbc6', N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'Sepatu Nike', N'Sepatu lari terbaik', N'cat-elektronik', CAST(1500000.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(5, 2)), 3, CAST(N'2025-11-01T22:11:02.7791853' AS DateTime2), NULL, CAST(N'2025-11-01T22:18:52.9601539' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[MsUser] ([IdUser], [UserName], [Email], [PhoneNumber], [FirstName], [LastName], [DOB], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive], [IdGender]) VALUES (N'3ab9269b-3123-4844-9cfc-c18c401350f8', N'user_2', N'user2@mail.com', N'081234582938', N'User Belanja', NULL, CAST(N'2000-10-10T00:00:00.0000000' AS DateTime2), CAST(N'2025-10-31T08:46:02.4738225' AS DateTime2), NULL, NULL, NULL, 1, N'id-gender-perempuan')
GO
INSERT [dbo].[MsUser] ([IdUser], [UserName], [Email], [PhoneNumber], [FirstName], [LastName], [DOB], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive], [IdGender]) VALUES (N'db48270e-fb2c-4761-9520-cc28784febb3', N'theo_belanja', N'theo@mail.com', N'081255982370', N'Theo', NULL, CAST(N'2000-10-10T00:00:00.0000000' AS DateTime2), CAST(N'2025-10-31T17:19:57.0588578' AS DateTime2), NULL, NULL, NULL, 1, N'id-gender-laki')
GO
INSERT [dbo].[MsUser] ([IdUser], [UserName], [Email], [PhoneNumber], [FirstName], [LastName], [DOB], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive], [IdGender]) VALUES (N'e95735b3-635b-4748-bf2c-1bdd20c8d107', N'theo_user', N'theo2@mail.com', N'081234582910', N'Theo Filus Iglesias Triadi', NULL, CAST(N'2005-09-10T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-01T21:56:29.7570305' AS DateTime2), NULL, NULL, NULL, 1, N'id-gender-laki')
GO
INSERT [dbo].[MsUser] ([IdUser], [UserName], [Email], [PhoneNumber], [FirstName], [LastName], [DOB], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive], [IdGender]) VALUES (N'f2caf510-c72d-41c6-8758-a7ccc9809376', N'andi_user', N'andi@2mail.com', N'081234829120', N'Andi Setiawan', NULL, CAST(N'2005-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-01T21:57:49.3434434' AS DateTime2), NULL, NULL, NULL, 1, N'id-gender-laki')
GO
INSERT [dbo].[MsUserPassword] ([IdUserPassword], [IdUser], [PasswordHash], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'0c13ef5c-f622-4720-a427-fdaefac2b69e', N'3ab9269b-3123-4844-9cfc-c18c401350f8', N'06d8b3c70c4ebd5bee566d662b8e1cf77e1d458299ddfd355d09ad79df11f525', CAST(N'2025-10-31T08:46:02.4753270' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MsUserPassword] ([IdUserPassword], [IdUser], [PasswordHash], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'19763422-c0f3-4811-a8c2-28889dd363ea', N'db48270e-fb2c-4761-9520-cc28784febb3', N'5da5ab9936606c7ae61356b305235fcc3d696be9490329457d2ca21d77792843', CAST(N'2025-10-31T17:19:57.0603762' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MsUserPassword] ([IdUserPassword], [IdUser], [PasswordHash], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'87dcbf07-7c65-43c0-94f9-474038041aba', N'f2caf510-c72d-41c6-8758-a7ccc9809376', N'0a9f173d20ecd8450fd138fdc56adf7cc3348292dd915ca92d262a3fdc347e97', CAST(N'2025-11-01T21:57:49.3435406' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MsUserPassword] ([IdUserPassword], [IdUser], [PasswordHash], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'b4d428d7-8132-45e2-bbf1-7a294d585f0e', N'e95735b3-635b-4748-bf2c-1bdd20c8d107', N'b8e8f20781764dedc7b5df86bdd02a185905795456b34ccba99e4b184438a4cc', CAST(N'2025-11-01T21:56:29.7577006' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MsUserSeller] ([IdUserSeller], [IdUser], [SellerName], [SellerDesc], [Address], [SellerCode], [PhoneNumber], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'14a902de-e643-4639-aa00-779608dd6719', N'e95735b3-635b-4748-bf2c-1bdd20c8d107', N'Toko Sukses', N'Toko jual elektronik, peralatan rumah tangga', N'Jalan Kebayoran lama no 25', N'toko-sukses', N'081234582910', CAST(N'2025-11-01T23:13:37.3419864' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MsUserSeller] ([IdUserSeller], [IdUser], [SellerName], [SellerDesc], [Address], [SellerCode], [PhoneNumber], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'3d72a52a-0ef5-4705-8234-90bae6a1a153', N'db48270e-fb2c-4761-9520-cc28784febb3', N'Toko Sinar', N'Toko buka 24 jam. Menjual berbagai produk elektronik', N'Jalan Kebon Jeruk No 22', N'toko-sinar', N'081234831928', CAST(N'2025-11-01T07:47:06.0806643' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerCart] ([IdBuyerCart], [IdUser], [IdProduct], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'89cae13d-2f23-48f3-9fed-8e5a14fae344', N'db48270e-fb2c-4761-9520-cc28784febb3', N'232cdc95-aa6e-449e-a838-4ce8e667073a', 1, CAST(N'2025-11-01T22:45:36.6724596' AS DateTime2), NULL, CAST(N'2025-11-01T22:45:51.7184673' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[TrBuyerCart] ([IdBuyerCart], [IdUser], [IdProduct], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'e0f04111-fae7-4a3a-a82d-7e9611887ce6', N'db48270e-fb2c-4761-9520-cc28784febb3', N'55192497-5d85-4a43-803d-1eef3b9a7539', 1, CAST(N'2025-11-01T16:59:49.7388128' AS DateTime2), NULL, CAST(N'2025-11-01T17:00:01.7760863' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[TrBuyerCart] ([IdBuyerCart], [IdUser], [IdProduct], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'f12db1dc-e77d-4d3d-b2b5-24222aaa05cf', N'db48270e-fb2c-4761-9520-cc28784febb3', N'0f7a9944-31fc-4346-ab97-c6520da73954', 1, CAST(N'2025-11-01T22:45:39.0958368' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerCart] ([IdBuyerCart], [IdUser], [IdProduct], [Qty], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'f193d1b2-3b0b-42ff-be04-6b6e9a506db3', N'db48270e-fb2c-4761-9520-cc28784febb3', N'3261127a-73f1-4db9-983f-81a387029b4e', 1, CAST(N'2025-11-01T15:25:07.3782912' AS DateTime2), NULL, CAST(N'2025-11-01T17:00:01.7760870' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[TrBuyerTransaction] ([IdBuyerTransaction], [IdUser], [IdPayment], [FinalPrice], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'2d89c4c6-c1b2-420d-bdd2-4494c39c3eb2', N'db48270e-fb2c-4761-9520-cc28784febb3', N'pay-transfer', CAST(16000000.00 AS Decimal(18, 2)), NULL, NULL, CAST(N'2025-11-01T16:42:02.3739362' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerTransaction] ([IdBuyerTransaction], [IdUser], [IdPayment], [FinalPrice], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'32b5fcc9-fb11-4f8e-8316-43c0cc51210d', N'db48270e-fb2c-4761-9520-cc28784febb3', N'pay-transfer', CAST(8000000.00 AS Decimal(18, 2)), NULL, NULL, CAST(N'2025-11-01T16:41:24.2046957' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerTransaction] ([IdBuyerTransaction], [IdUser], [IdPayment], [FinalPrice], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'38f025b7-45b5-4dc4-af68-e89cab11239d', N'db48270e-fb2c-4761-9520-cc28784febb3', N'pay-cod', CAST(28000000.00 AS Decimal(18, 2)), NULL, NULL, CAST(N'2025-11-01T17:00:01.6709319' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerTransaction] ([IdBuyerTransaction], [IdUser], [IdPayment], [FinalPrice], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'68694bac-c2ea-44a1-baa9-e51b1cd8f23f', N'db48270e-fb2c-4761-9520-cc28784febb3', N'pay-cod', CAST(8000000.00 AS Decimal(18, 2)), NULL, NULL, CAST(N'2025-11-01T15:43:20.0683272' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerTransaction] ([IdBuyerTransaction], [IdUser], [IdPayment], [FinalPrice], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'6c8fb14b-3a6a-4228-b407-28f935c6898e', N'3ab9269b-3123-4844-9cfc-c18c401350f8', N'pay-cod', CAST(225000.00 AS Decimal(18, 2)), NULL, NULL, CAST(N'2025-10-31T10:33:44.9633583' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerTransaction] ([IdBuyerTransaction], [IdUser], [IdPayment], [FinalPrice], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'87c1cab0-7047-4c69-91e4-985a375f5dfd', N'db48270e-fb2c-4761-9520-cc28784febb3', N'pay-cod', CAST(16000000.00 AS Decimal(18, 2)), NULL, NULL, CAST(N'2025-11-01T16:18:26.4332601' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerTransaction] ([IdBuyerTransaction], [IdUser], [IdPayment], [FinalPrice], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'c86c123c-fd70-4486-b4e8-6e24dd5b09d6', N'db48270e-fb2c-4761-9520-cc28784febb3', N'pay-transfer', CAST(19400000.00 AS Decimal(18, 2)), NULL, NULL, CAST(N'2025-11-01T16:55:39.6588877' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrBuyerTransactionDetail] ([IdBuyerTransactionDetail], [IdBuyerTransaction], [IdProduct], [Qty], [PriceProduct], [DiscountProduct], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'44d8cfda-27c3-4464-ac49-242242bac1ad', N'38f025b7-45b5-4dc4-af68-e89cab11239d', N'55192497-5d85-4a43-803d-1eef3b9a7539', 1, CAST(20000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 4, N'', CAST(N'2025-11-01T17:00:01.7201444' AS DateTime2), NULL, CAST(N'2025-11-01T17:44:02.1557330' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[TrBuyerTransactionDetail] ([IdBuyerTransactionDetail], [IdBuyerTransaction], [IdProduct], [Qty], [PriceProduct], [DiscountProduct], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'7125660c-367c-498e-9a17-215f696243ab', N'87c1cab0-7047-4c69-91e4-985a375f5dfd', N'3261127a-73f1-4db9-983f-81a387029b4e', 2, CAST(8000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 3, N'', CAST(N'2025-11-01T16:18:26.5127040' AS DateTime2), NULL, CAST(N'2025-11-01T22:44:33.1623371' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[TrBuyerTransactionDetail] ([IdBuyerTransactionDetail], [IdBuyerTransaction], [IdProduct], [Qty], [PriceProduct], [DiscountProduct], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'87492236-7648-4481-8d82-12b67d720117', N'68694bac-c2ea-44a1-baa9-e51b1cd8f23f', N'3261127a-73f1-4db9-983f-81a387029b4e', 1, CAST(8000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 4, N'', CAST(N'2025-11-01T15:43:20.2806631' AS DateTime2), NULL, CAST(N'2025-11-01T22:44:28.4738414' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[TrBuyerTransactionDetail] ([IdBuyerTransactionDetail], [IdBuyerTransaction], [IdProduct], [Qty], [PriceProduct], [DiscountProduct], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'9b7812fe-0ec8-4f8a-bc60-c05fab7b539c', N'38f025b7-45b5-4dc4-af68-e89cab11239d', N'3261127a-73f1-4db9-983f-81a387029b4e', 1, CAST(8000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 3, N'', CAST(N'2025-11-01T17:00:01.7491223' AS DateTime2), NULL, CAST(N'2025-11-01T22:45:24.2636648' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[TrBuyerTransactionDetail] ([IdBuyerTransactionDetail], [IdBuyerTransaction], [IdProduct], [Qty], [PriceProduct], [DiscountProduct], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'a752c121-3985-4fb6-89ef-cdfe4723ce12', N'c86c123c-fd70-4486-b4e8-6e24dd5b09d6', N'3261127a-73f1-4db9-983f-81a387029b4e', 1, CAST(8000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 5, N'', CAST(N'2025-11-01T16:55:39.7789648' AS DateTime2), NULL, CAST(N'2025-11-01T22:45:20.6877150' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[TrBuyerTransactionDetail] ([IdBuyerTransactionDetail], [IdBuyerTransaction], [IdProduct], [Qty], [PriceProduct], [DiscountProduct], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'b4e2dd7d-0429-4156-85ac-4fc1e55be54f', N'32b5fcc9-fb11-4f8e-8316-43c0cc51210d', N'3261127a-73f1-4db9-983f-81a387029b4e', 1, CAST(8000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 4, N'', CAST(N'2025-11-01T16:41:24.2741881' AS DateTime2), NULL, CAST(N'2025-11-01T22:45:10.1793052' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[TrBuyerTransactionDetail] ([IdBuyerTransactionDetail], [IdBuyerTransaction], [IdProduct], [Qty], [PriceProduct], [DiscountProduct], [Rating], [RatingComment], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'ddcae051-ed4a-4ef8-807b-5cf4af6bc9f3', N'2d89c4c6-c1b2-420d-bdd2-4494c39c3eb2', N'3261127a-73f1-4db9-983f-81a387029b4e', 2, CAST(8000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 3, N'', CAST(N'2025-11-01T16:42:02.3787923' AS DateTime2), NULL, CAST(N'2025-11-01T22:45:16.3104043' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[TrHomeAddress] ([IdHomeAddress], [IdUser], [Provinsi], [KotaKabupaten], [Kecamatan], [KodePos], [HomeAddressDesc], [IsPrimaryAddress], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'f4cab3cd-4843-4ab0-8821-baaadc8a8900', N'3ab9269b-3123-4844-9cfc-c18c401350f8', N'Jakarta', N'Jakarta Barat', N'Palmerah', N'13760', N'Jalan Syahdan No 39', 1, CAST(N'2025-10-31T08:46:02.4974289' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrProductImages] ([IdProductImages], [IdProduct], [ProductImage], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'08350f33-6338-4d5d-a672-23bc72f78a0c', N'0f7a9944-31fc-4346-ab97-c6520da73954', N'http://localhost:5283/img/no-image.jpg', CAST(N'2025-11-01T22:19:18.2053928' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrProductImages] ([IdProductImages], [IdProduct], [ProductImage], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'27b34a4d-3d79-4e25-9427-0c0ad48da63e', N'4cfaedbf-ecb5-491f-b0db-201b4afcb375', N'/img/no-image.png', CAST(N'2025-11-01T22:07:34.6874679' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrProductImages] ([IdProductImages], [IdProduct], [ProductImage], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'3bf0c90e-07ae-4549-93bb-a1cce1654e5e', N'a9bbb816-b876-4a3b-9ac8-74759d6a3e24', N'http://localhost:5283/img/no-image.jpg', CAST(N'2025-11-01T22:43:46.8433634' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrProductImages] ([IdProductImages], [IdProduct], [ProductImage], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'53e42a01-77be-4a46-b2dc-b390ebdc9e7f', N'f975867a-2737-4cc9-9144-41fbdc09bbc6', N'/img/no-image.png', CAST(N'2025-11-01T22:11:02.8940087' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrProductImages] ([IdProductImages], [IdProduct], [ProductImage], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'db51de53-a4f1-469c-912c-fa12fb63c957', N'3dd621df-7e39-495c-8aa0-4d44fa4fee8b', N'http://localhost:5283/img/no-image.jpg', CAST(N'2025-11-01T22:22:09.0870946' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[TrProductImages] ([IdProductImages], [IdProduct], [ProductImage], [DateIn], [UserIn], [DateUp], [UserUp], [IsActive]) VALUES (N'e5b34126-0da8-46a2-8872-84749eb072bb', N'232cdc95-aa6e-449e-a838-4ce8e667073a', N'http://localhost:5283/img/no-image.jpg', CAST(N'2025-11-01T22:42:24.5388274' AS DateTime2), NULL, NULL, NULL, 1)
GO
ALTER TABLE [dbo].[MsProduct]  WITH CHECK ADD  CONSTRAINT [FK_MsProduct_LtCategory_IdCategory] FOREIGN KEY([IdCategory])
REFERENCES [dbo].[LtCategory] ([IdCategory])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MsProduct] CHECK CONSTRAINT [FK_MsProduct_LtCategory_IdCategory]
GO
ALTER TABLE [dbo].[MsProduct]  WITH CHECK ADD  CONSTRAINT [FK_MsProduct_MsUserSeller_IdUserSeller] FOREIGN KEY([IdUserSeller])
REFERENCES [dbo].[MsUserSeller] ([IdUserSeller])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MsProduct] CHECK CONSTRAINT [FK_MsProduct_MsUserSeller_IdUserSeller]
GO
ALTER TABLE [dbo].[MsUser]  WITH CHECK ADD  CONSTRAINT [FK_MsUser_LtGender_IdGender] FOREIGN KEY([IdGender])
REFERENCES [dbo].[LtGender] ([IdGender])
GO
ALTER TABLE [dbo].[MsUser] CHECK CONSTRAINT [FK_MsUser_LtGender_IdGender]
GO
ALTER TABLE [dbo].[MsUserPassword]  WITH CHECK ADD  CONSTRAINT [FK_MsUserPassword_MsUser_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[MsUser] ([IdUser])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MsUserPassword] CHECK CONSTRAINT [FK_MsUserPassword_MsUser_IdUser]
GO
ALTER TABLE [dbo].[MsUserSeller]  WITH CHECK ADD  CONSTRAINT [FK_MsUserSeller_MsUser_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[MsUser] ([IdUser])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MsUserSeller] CHECK CONSTRAINT [FK_MsUserSeller_MsUser_IdUser]
GO
ALTER TABLE [dbo].[TrBuyerCart]  WITH CHECK ADD  CONSTRAINT [FK_TrBuyerCart_MsProduct_IdProduct] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[MsProduct] ([IdProduct])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrBuyerCart] CHECK CONSTRAINT [FK_TrBuyerCart_MsProduct_IdProduct]
GO
ALTER TABLE [dbo].[TrBuyerCart]  WITH CHECK ADD  CONSTRAINT [FK_TrBuyerCart_MsUser_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[MsUser] ([IdUser])
GO
ALTER TABLE [dbo].[TrBuyerCart] CHECK CONSTRAINT [FK_TrBuyerCart_MsUser_IdUser]
GO
ALTER TABLE [dbo].[TrBuyerTransaction]  WITH CHECK ADD  CONSTRAINT [FK_TrBuyerTransaction_LtPayment_IdPayment] FOREIGN KEY([IdPayment])
REFERENCES [dbo].[LtPayment] ([IdPayment])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrBuyerTransaction] CHECK CONSTRAINT [FK_TrBuyerTransaction_LtPayment_IdPayment]
GO
ALTER TABLE [dbo].[TrBuyerTransaction]  WITH CHECK ADD  CONSTRAINT [FK_TrBuyerTransaction_MsUser_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[MsUser] ([IdUser])
GO
ALTER TABLE [dbo].[TrBuyerTransaction] CHECK CONSTRAINT [FK_TrBuyerTransaction_MsUser_IdUser]
GO
ALTER TABLE [dbo].[TrBuyerTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_TrBuyerTransactionDetail_MsProduct_IdProduct] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[MsProduct] ([IdProduct])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrBuyerTransactionDetail] CHECK CONSTRAINT [FK_TrBuyerTransactionDetail_MsProduct_IdProduct]
GO
ALTER TABLE [dbo].[TrBuyerTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_TrBuyerTransactionDetail_TrBuyerTransaction_IdBuyerTransaction] FOREIGN KEY([IdBuyerTransaction])
REFERENCES [dbo].[TrBuyerTransaction] ([IdBuyerTransaction])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrBuyerTransactionDetail] CHECK CONSTRAINT [FK_TrBuyerTransactionDetail_TrBuyerTransaction_IdBuyerTransaction]
GO
ALTER TABLE [dbo].[TrHomeAddress]  WITH CHECK ADD  CONSTRAINT [FK_TrHomeAddress_MsUser_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[MsUser] ([IdUser])
GO
ALTER TABLE [dbo].[TrHomeAddress] CHECK CONSTRAINT [FK_TrHomeAddress_MsUser_IdUser]
GO
ALTER TABLE [dbo].[TrProductImages]  WITH CHECK ADD  CONSTRAINT [FK_TrProductImages_MsProduct_IdProduct] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[MsProduct] ([IdProduct])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrProductImages] CHECK CONSTRAINT [FK_TrProductImages_MsProduct_IdProduct]
GO
