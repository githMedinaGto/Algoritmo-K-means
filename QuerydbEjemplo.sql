USE [master]
GO
/****** Object:  Database [dbEjemplo]    Script Date: 28/07/2023 04:47:10 p. m. ******/
CREATE DATABASE [dbEjemplo]
 
USE [dbEjemplo]
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 28/07/2023 04:47:10 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoria](
	[cat_id] [int] IDENTITY(1,1) NOT NULL,
	[cat_nom] [varchar](250) NULL,
	[cat_desp] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[cat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriasInteres]    Script Date: 28/07/2023 04:47:10 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriasInteres](
	[ID] [int] NOT NULL,
	[Categoria] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciudad]    Script Date: 28/07/2023 04:47:10 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciudad](
	[ID] [int] NOT NULL,
	[Ciudad] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PosibilidadesEconomicas]    Script Date: 28/07/2023 04:47:10 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PosibilidadesEconomicas](
	[ID] [int] NOT NULL,
	[Rango] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preferencias]    Script Date: 28/07/2023 04:47:10 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preferencias](
	[ID] [int] NOT NULL,
	[CategoriaID] [int] NULL,
	[Preferencia] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 28/07/2023 04:47:10 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[ID] [int] NOT NULL,
	[Region] [varchar](100) NULL,
	[CiudadID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 28/07/2023 04:47:10 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[ID] [int] NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Edad] [int] NULL,
	[Genero] [varchar](100) NULL,
	[UbicacionID] [int] NULL,
	[PosibilidadesEconomicasID] [int] NULL,
	[PreferenciasID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[categoria] ON 
GO
INSERT [dbo].[categoria] ([cat_id], [cat_nom], [cat_desp]) VALUES (1, N'ejemplo 1', N'ejemplo 1')
GO
INSERT [dbo].[categoria] ([cat_id], [cat_nom], [cat_desp]) VALUES (2, N'ejemplo 2', N'ejemplo 2')
GO
SET IDENTITY_INSERT [dbo].[categoria] OFF
GO
INSERT [dbo].[CategoriasInteres] ([ID], [Categoria]) VALUES (1, N'Ropa y moda')
GO
INSERT [dbo].[CategoriasInteres] ([ID], [Categoria]) VALUES (2, N'Electrónica')
GO
INSERT [dbo].[CategoriasInteres] ([ID], [Categoria]) VALUES (3, N'Alimentos y bebidas')
GO
INSERT [dbo].[Ciudad] ([ID], [Ciudad]) VALUES (1, N'Ciudad A')
GO
INSERT [dbo].[Ciudad] ([ID], [Ciudad]) VALUES (2, N'Ciudad B')
GO
INSERT [dbo].[Ciudad] ([ID], [Ciudad]) VALUES (3, N'Ciudad C')
GO
INSERT [dbo].[PosibilidadesEconomicas] ([ID], [Rango]) VALUES (1, N'Bajas')
GO
INSERT [dbo].[PosibilidadesEconomicas] ([ID], [Rango]) VALUES (2, N'Medias')
GO
INSERT [dbo].[PosibilidadesEconomicas] ([ID], [Rango]) VALUES (3, N'Altas')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (1, 1, N'Estilo casual')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (2, 1, N'Estilo formal')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (3, 1, N'Ropa deportiva')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (4, 2, N'Teléfonos móviles')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (5, 2, N'Computadoras')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (6, 2, N'Electrodomésticos')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (7, 3, N'Comida rápida')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (8, 3, N'Bebidas alcohólicas')
GO
INSERT [dbo].[Preferencias] ([ID], [CategoriaID], [Preferencia]) VALUES (9, 3, N'Productos orgánicos')
GO
INSERT [dbo].[Region] ([ID], [Region], [CiudadID]) VALUES (1, N'Region A', 1)
GO
INSERT [dbo].[Region] ([ID], [Region], [CiudadID]) VALUES (2, N'Region B', 2)
GO
INSERT [dbo].[Region] ([ID], [Region], [CiudadID]) VALUES (3, N'Region C', 3)
GO
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Edad], [Genero], [UbicacionID], [PosibilidadesEconomicasID], [PreferenciasID]) VALUES (1, N'Juan', 20, N'Masculino', 1, 1, 1)
GO
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Edad], [Genero], [UbicacionID], [PosibilidadesEconomicasID], [PreferenciasID]) VALUES (2, N'Maria', 30, N'Femenino', 2, 2, 2)
GO
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Edad], [Genero], [UbicacionID], [PosibilidadesEconomicasID], [PreferenciasID]) VALUES (3, N'Pedro', 25, N'Masculino', 3, 3, 3)
GO
ALTER TABLE [dbo].[Preferencias]  WITH CHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[CategoriasInteres] ([ID])
GO
ALTER TABLE [dbo].[Region]  WITH CHECK ADD FOREIGN KEY([CiudadID])
REFERENCES [dbo].[Ciudad] ([ID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([PosibilidadesEconomicasID])
REFERENCES [dbo].[PosibilidadesEconomicas] ([ID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([PreferenciasID])
REFERENCES [dbo].[Preferencias] ([ID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([UbicacionID])
REFERENCES [dbo].[Region] ([ID])
GO
USE [master]
GO
ALTER DATABASE [dbEjemplo] SET  READ_WRITE 
GO
