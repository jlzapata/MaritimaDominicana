USE [master]
GO
/****** Object:  Database [SupportDB]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE DATABASE [SupportDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SupportDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.JULIAN\MSSQL\DATA\SupportDB.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SupportDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.JULIAN\MSSQL\DATA\SupportDB_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SupportDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SupportDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SupportDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SupportDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SupportDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SupportDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SupportDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SupportDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SupportDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SupportDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SupportDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SupportDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SupportDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SupportDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SupportDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SupportDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SupportDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SupportDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SupportDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SupportDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SupportDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SupportDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SupportDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SupportDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SupportDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SupportDB] SET  MULTI_USER 
GO
ALTER DATABASE [SupportDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SupportDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SupportDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SupportDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SupportDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SupportDB]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Telephone] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Departments]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
 CONSTRAINT [PK_dbo.Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Followers]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Followers](
	[UserId] [int] NOT NULL,
	[FollowerId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Followers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[FollowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Places]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Places](
	[PlaceId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Places] PRIMARY KEY CLUSTERED 
(
	[PlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProblemDetails]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProblemDetails](
	[ProblemDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ProblemId] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[Date] [datetime] NOT NULL,
	[PlaceId] [int] NOT NULL,
	[Update_at] [datetime] NULL,
	[Modified_by] [int] NULL,
	[AssignedTo] [int] NULL,
	[AssignedAt] [datetime] NULL,
	[state] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ProblemDetails] PRIMARY KEY CLUSTERED 
(
	[ProblemDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Problems]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Problems](
	[ProblemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Problems] PRIMARY KEY CLUSTERED 
(
	[ProblemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Solutions]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Solutions](
	[SolutionId] [int] NOT NULL,
	[SolutionDescription] [varchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Solutions] PRIMARY KEY CLUSTERED 
(
	[SolutionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Types]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Types](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Types] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/26/2017 1:25:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Pasword] [varchar](max) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[TypeId] [int] NOT NULL,
	[Active] [bit] NULL,
	[Connected] [bit] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Index [IX_FollowerId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_FollowerId] ON [dbo].[Followers]
(
	[FollowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Followers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AssignedTo]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_AssignedTo] ON [dbo].[ProblemDetails]
(
	[AssignedTo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ClientId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_ClientId] ON [dbo].[ProblemDetails]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatedBy]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_CreatedBy] ON [dbo].[ProblemDetails]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DepartmentId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_DepartmentId] ON [dbo].[ProblemDetails]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlaceId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_PlaceId] ON [dbo].[ProblemDetails]
(
	[PlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProblemId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProblemId] ON [dbo].[ProblemDetails]
(
	[ProblemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SolutionId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_SolutionId] ON [dbo].[Solutions]
(
	[SolutionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Solutions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Email]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TypeId]    Script Date: 5/26/2017 1:25:35 AM ******/
CREATE NONCLUSTERED INDEX [IX_TypeId] ON [dbo].[Users]
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Followers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Followers_dbo.Users_FollowerId] FOREIGN KEY([FollowerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Followers] CHECK CONSTRAINT [FK_dbo.Followers_dbo.Users_FollowerId]
GO
ALTER TABLE [dbo].[Followers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Followers_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Followers] CHECK CONSTRAINT [FK_dbo.Followers_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[ProblemDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProblemDetails_dbo.Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProblemDetails] CHECK CONSTRAINT [FK_dbo.ProblemDetails_dbo.Clients_ClientId]
GO
ALTER TABLE [dbo].[ProblemDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProblemDetails_dbo.Departments_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProblemDetails] CHECK CONSTRAINT [FK_dbo.ProblemDetails_dbo.Departments_DepartmentId]
GO
ALTER TABLE [dbo].[ProblemDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProblemDetails_dbo.Places_PlaceId] FOREIGN KEY([PlaceId])
REFERENCES [dbo].[Places] ([PlaceId])
GO
ALTER TABLE [dbo].[ProblemDetails] CHECK CONSTRAINT [FK_dbo.ProblemDetails_dbo.Places_PlaceId]
GO
ALTER TABLE [dbo].[ProblemDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProblemDetails_dbo.Problems_ProblemId] FOREIGN KEY([ProblemId])
REFERENCES [dbo].[Problems] ([ProblemId])
GO
ALTER TABLE [dbo].[ProblemDetails] CHECK CONSTRAINT [FK_dbo.ProblemDetails_dbo.Problems_ProblemId]
GO
ALTER TABLE [dbo].[ProblemDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProblemDetails_dbo.Users_AssignedTo] FOREIGN KEY([AssignedTo])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ProblemDetails] CHECK CONSTRAINT [FK_dbo.ProblemDetails_dbo.Users_AssignedTo]
GO
ALTER TABLE [dbo].[ProblemDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProblemDetails_dbo.Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ProblemDetails] CHECK CONSTRAINT [FK_dbo.ProblemDetails_dbo.Users_CreatedBy]
GO
ALTER TABLE [dbo].[Solutions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Solutions_dbo.ProblemDetails_SolutionId] FOREIGN KEY([SolutionId])
REFERENCES [dbo].[ProblemDetails] ([ProblemDetailId])
GO
ALTER TABLE [dbo].[Solutions] CHECK CONSTRAINT [FK_dbo.Solutions_dbo.ProblemDetails_SolutionId]
GO
ALTER TABLE [dbo].[Solutions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Solutions_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Solutions] CHECK CONSTRAINT [FK_dbo.Solutions_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Types_TypeId] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Types] ([TypeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Types_TypeId]
GO
USE [master]
GO
ALTER DATABASE [SupportDB] SET  READ_WRITE 
GO
