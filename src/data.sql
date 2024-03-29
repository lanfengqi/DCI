USE [book]
GO
/****** Object:  Table [dbo].[LibraryAccount]    Script Date: 09/24/2011 13:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LibraryAccount](
	[Id] [varchar](50) NOT NULL,
	[Number] [varchar](50) NULL,
	[OwnerName] [varchar](50) NULL,
	[IsLocked] [bit] NULL,
 CONSTRAINT [PK_LibraryAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BorrowInfo]    Script Date: 09/24/2011 13:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BorrowInfo](
	[Id] [varchar](50) NOT NULL,
	[BorrowTime] [datetime] NULL,
	[ReturnTime] [datetime] NULL,
	[Book] [varchar](50) NULL,
	[LibraryAccount] [varchar](50) NULL,
 CONSTRAINT [PK_BorrowInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BookStoreInfo]    Script Date: 09/24/2011 13:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BookStoreInfo](
	[Id] [varchar](50) NOT NULL,
	[Count] [int] NULL,
	[Location] [varchar](50) NULL,
	[Book] [varchar](50) NULL,
 CONSTRAINT [PK_BookStoreInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BookOutInfo]    Script Date: 09/24/2011 13:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BookOutInfo](
	[Id] [varchar](50) NOT NULL,
	[Count] [int] NULL,
	[OutTime] [datetime] NULL,
	[Book] [varchar](50) NULL,
 CONSTRAINT [PK_BookOutInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Book]    Script Date: 09/24/2011 13:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Book](
	[Id] [varchar](50) NOT NULL,
	[BookName] [varchar](50) NULL,
	[Author] [varchar](50) NULL,
	[Publisher] [varchar](50) NULL,
	[ISBN] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
