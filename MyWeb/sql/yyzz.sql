USE [master]
GO
/****** Object:  Database [YYZZ]    Script Date: 2016/1/28 20:36:08 ******/
CREATE DATABASE [YYZZ]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YYZZ', FILENAME = N'D:\MyWeb\Data\YYZZ.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'YYZZ_log', FILENAME = N'D:\MyWeb\Data\YYZZ_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [YYZZ] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YYZZ].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YYZZ] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YYZZ] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YYZZ] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YYZZ] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YYZZ] SET ARITHABORT OFF 
GO
ALTER DATABASE [YYZZ] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [YYZZ] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [YYZZ] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YYZZ] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YYZZ] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YYZZ] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YYZZ] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YYZZ] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YYZZ] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YYZZ] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YYZZ] SET  DISABLE_BROKER 
GO
ALTER DATABASE [YYZZ] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YYZZ] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YYZZ] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YYZZ] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YYZZ] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YYZZ] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YYZZ] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YYZZ] SET RECOVERY FULL 
GO
ALTER DATABASE [YYZZ] SET  MULTI_USER 
GO
ALTER DATABASE [YYZZ] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YYZZ] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YYZZ] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YYZZ] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [YYZZ]
GO
/****** Object:  User [yyzz]    Script Date: 2016/1/28 20:36:08 ******/
CREATE USER [yyzz] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [yyzz]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 2016/1/28 20:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[a_Title] [nvarchar](50) NOT NULL,
	[a_TypeId] [int] NOT NULL,
	[a_KeyWord] [nvarchar](100) NULL,
	[a_From] [nvarchar](50) NULL,
	[a_Content] [text] NOT NULL,
	[a_IsTop] [bit] NULL,
	[a_IsHot] [bit] NULL,
	[a_States] [tinyint] NULL,
	[a_CreateBy] [bigint] NOT NULL,
	[a_CreateDate] [datetime] NULL,
	[a_ModifiedBy] [bigint] NULL,
	[a_ModifiedDate] [datetime] NOT NULL,
	[a_Statistics] [int] NULL,
	[a_LikeCount] [int] NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArticleType]    Script Date: 2016/1/28 20:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[a_t_ArticleCategory] [nvarchar](50) NOT NULL,
	[a_t_Flag] [tinyint] NOT NULL,
	[a_t_CreateBy] [nvarchar](50) NULL,
	[a_t_CreateDate] [datetime] NULL,
 CONSTRAINT [PK_ArticleTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Collection]    Script Date: 2016/1/28 20:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collection](
	[C_id] [bigint] IDENTITY(1,1) NOT NULL,
	[U_id] [int] NOT NULL,
	[A_id] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Flg] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2016/1/28 20:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comment](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[titleID] [bigint] NOT NULL,
	[content] [nvarchar](500) NULL,
	[userId] [bigint] NULL,
	[username] [nvarchar](50) NULL,
	[address] [varchar](100) NULL,
	[createtime] [datetime] NULL,
	[state] [int] NULL,
	[hasson] [bit] NULL,
	[isson] [bit] NULL,
	[parentid] [bigint] NULL,
	[cool] [int] NULL,
	[browserInfo] [nvarchar](100) NULL,
	[ipaddress] [nvarchar](50) NULL,
	[isp] [nvarchar](50) NULL,
	[os] [nvarchar](50) NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LikeCount]    Script Date: 2016/1/28 20:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LikeCount](
	[l_id] [bigint] IDENTITY(1,1) NOT NULL,
	[a_id] [bigint] NOT NULL,
	[u_id] [bigint] NOT NULL,
	[liketime] [datetime] NULL,
	[stats] [tinyint] NULL,
 CONSTRAINT [PK_LikeCount] PRIMARY KEY CLUSTERED 
(
	[l_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserDetail]    Script Date: 2016/1/28 20:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetail](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[U_Id] [bigint] NOT NULL,
	[UD_Sex] [nvarchar](10) NULL,
	[UD_Age] [nvarchar](8) NULL,
	[UD_Birth] [date] NULL,
	[UD_Cale] [nvarchar](10) NULL,
	[UD_Const] [nvarchar](20) NULL,
	[UD_BooldType] [nvarchar](10) NULL,
	[UD_EmotState] [nvarchar](20) NULL,
	[UD_ContactWay] [nvarchar](50) NULL,
	[UD_Education] [nvarchar](50) NULL,
	[UD_School] [nvarchar](50) NULL,
	[UD_PaperType] [nvarchar](50) NULL,
	[UD_PaperNumber] [nvarchar](50) NULL,
	[UD_Company] [nvarchar](50) NULL,
	[UD_Worker] [nvarchar](50) NULL,
	[UD_HomeTown] [nvarchar](50) NULL,
	[UD_NowPlace] [nvarchar](150) NULL,
	[UD_Address] [nvarchar](150) NULL,
	[UD_Remark] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2016/1/28 20:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserInfo](
	[U_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[U_GUID] [uniqueidentifier] NOT NULL,
	[U_UserName] [nvarchar](50) NULL,
	[U_Password] [varchar](100) NOT NULL,
	[U_Salt] [varchar](50) NOT NULL,
	[U_RealName] [nvarchar](50) NULL,
	[U_NickName] [nvarchar](50) NULL,
	[U_Email] [varchar](50) NULL,
	[U_MobilePhone] [varchar](16) NULL,
	[U_State] [tinyint] NOT NULL,
	[U_Access] [tinyint] NOT NULL,
	[U_HeadPic] [varchar](100) NULL,
	[U_RegisterDate] [datetime] NULL,
	[U_LastLoginTime] [datetime] NULL,
	[U_Tag] [nvarchar](max) NULL,
	[U_Signature] [nvarchar](200) NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[U_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Articles_a_Statistics]  DEFAULT ((1)) FOR [a_Statistics]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Articles_a_LikeCount]  DEFAULT ((0)) FOR [a_LikeCount]
GO
ALTER TABLE [dbo].[Collection] ADD  CONSTRAINT [DF_Collections_Flg]  DEFAULT ((0)) FOR [Flg]
GO
ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_Comment_state]  DEFAULT ((1)) FOR [state]
GO
ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_Comment_cool]  DEFAULT ((0)) FOR [cool]
GO
ALTER TABLE [dbo].[LikeCount] ADD  CONSTRAINT [DF_LikeCount_stats]  DEFAULT ((0)) FOR [stats]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_U_State]  DEFAULT ((1)) FOR [U_State]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_U_Access]  DEFAULT ((9)) FOR [U_Access]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_U_HeadPic]  DEFAULT ('th.jpg') FOR [U_HeadPic]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Articles_ArticleTypes] FOREIGN KEY([a_TypeId])
REFERENCES [dbo].[ArticleType] ([id])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Articles_ArticleTypes]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Articles_UserInfo] FOREIGN KEY([a_CreateBy])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Articles_UserInfo]
GO
ALTER TABLE [dbo].[LikeCount]  WITH CHECK ADD  CONSTRAINT [FK_LikeCount_Articles] FOREIGN KEY([a_id])
REFERENCES [dbo].[Article] ([id])
GO
ALTER TABLE [dbo].[LikeCount] CHECK CONSTRAINT [FK_LikeCount_Articles]
GO
ALTER TABLE [dbo].[LikeCount]  WITH CHECK ADD  CONSTRAINT [FK_LikeCount_UserInfo] FOREIGN KEY([u_id])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[LikeCount] CHECK CONSTRAINT [FK_LikeCount_UserInfo]
GO
ALTER TABLE [dbo].[UserDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserDetail_UserDetail] FOREIGN KEY([U_Id])
REFERENCES [dbo].[UserInfo] ([U_Id])
GO
ALTER TABLE [dbo].[UserDetail] CHECK CONSTRAINT [FK_UserDetail_UserDetail]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_From'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'首页显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_IsHot'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_States'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_CreateBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_ModifiedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改日期,默认当前日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_ModifiedDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'统计点击次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_Statistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点赞数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'a_LikeCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ArticleType', @level2type=N'COLUMN',@level2name=N'a_t_ArticleCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1：启用；0：未启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ArticleType', @level2type=N'COLUMN',@level2name=N'a_t_Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户收藏表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collection', @level2type=N'COLUMN',@level2name=N'C_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collection', @level2type=N'COLUMN',@level2name=N'U_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collection', @level2type=N'COLUMN',@level2name=N'A_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collection', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:正常,1:删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collection', @level2type=N'COLUMN',@level2name=N'Flg'
GO
EXEC sys.sp_addextendedproperty @name=N'备注', @value=N'用户收藏表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'titleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ip所在地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:需审核；1:审核通过；2:审核失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'state'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有回复' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'hasson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为回复' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'isson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被回复评论的id号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'parentid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点赞' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'cool'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'浏览器信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'browserInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'ipaddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网络服务商' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'isp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作系统' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Comment', @level2type=N'COLUMN',@level2name=N'os'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点赞表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LikeCount', @level2type=N'COLUMN',@level2name=N'l_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被点赞的文章' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LikeCount', @level2type=N'COLUMN',@level2name=N'a_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点赞人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LikeCount', @level2type=N'COLUMN',@level2name=N'u_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点赞时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LikeCount', @level2type=N'COLUMN',@level2name=N'liketime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：正常 1：取消不计数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LikeCount', @level2type=N'COLUMN',@level2name=N'stats'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'U_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公历农历' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_Cale'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'星座' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_Const'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'血型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_BooldType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'感情状况' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_EmotState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_ContactWay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文化程度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_Education'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'毕业院校' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_School'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_PaperType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'证件号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_PaperNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_Company'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职业' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_Worker'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'籍贯' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_HomeTown'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现居地' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_NowPlace'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDetail', @level2type=N'COLUMN',@level2name=N'UD_Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号GUID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_GUID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密钥' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_Salt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_RealName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_NickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_MobilePhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_Access'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_HeadPic'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标签' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_Tag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'个性签名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'U_Signature'
GO
USE [master]
GO
ALTER DATABASE [YYZZ] SET  READ_WRITE 
GO
