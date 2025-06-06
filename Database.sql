USE [master]
GO
/****** Object:  Database [CaseControl_Dev]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE DATABASE [CaseControl_Dev]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CaseControl_Dev', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.WRKSYS\MSSQL\DATA\CaseControl_Dev.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CaseControl_Dev_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.WRKSYS\MSSQL\DATA\CaseControl_Dev_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CaseControl_Dev] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CaseControl_Dev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CaseControl_Dev] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET ARITHABORT OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CaseControl_Dev] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CaseControl_Dev] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CaseControl_Dev] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CaseControl_Dev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CaseControl_Dev] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CaseControl_Dev] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CaseControl_Dev] SET  MULTI_USER 
GO
ALTER DATABASE [CaseControl_Dev] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CaseControl_Dev] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CaseControl_Dev] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CaseControl_Dev] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CaseControl_Dev] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CaseControl_Dev] SET QUERY_STORE = OFF
GO
USE [CaseControl_Dev]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2/6/2025 11:29:41 a. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Access_Roles]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Access_Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[AccessID] [int] NOT NULL,
 CONSTRAINT [PK_Access_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accesses]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accesses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Accesses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Binnacles]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Binnacles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
	[CaseID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Binnacles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaseAssignments]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaseAssignments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userNameRegistered] [nvarchar](max) NULL,
	[Observations] [nvarchar](max) NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
	[CaseID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_CaseAssignments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cases]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cases](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[Transmitter] [nvarchar](max) NULL,
	[Recipient] [nvarchar](max) NULL,
	[CommunicationNumber] [nvarchar](max) NULL,
	[DateOfCommunication] [datetime2](7) NOT NULL,
	[DateOfReceipt] [datetime2](7) NOT NULL,
	[CommunicationReference] [nvarchar](max) NULL,
	[Branch] [nvarchar](max) NULL,
	[AmountDetected] [decimal](18, 2) NOT NULL,
	[AmountInvestigated] [decimal](18, 2) NOT NULL,
	[AmountRecovered] [decimal](18, 2) NOT NULL,
	[AmountLost] [decimal](18, 2) NOT NULL,
	[AffectedAccount] [nvarchar](max) NULL,
	[UserNameRegistered] [nvarchar](max) NULL,
	[ReceptionMediumID] [int] NOT NULL,
	[CaseTypeID] [int] NOT NULL,
	[CaseStatusID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[GerenciaID] [int] NULL,
 CONSTRAINT [PK_Cases] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaseStatusChanges]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaseStatusChanges](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userNameRegistered] [nvarchar](max) NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
	[CaseID] [int] NOT NULL,
	[CaseStatusID] [int] NOT NULL,
 CONSTRAINT [PK_CaseStatusChanges] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaseStatuses]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaseStatuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Percent] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_CaseStatuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaseTypes]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaseTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_CaseTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EvidenceClassifications]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EvidenceClassifications](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_EvidenceClassifications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evidences]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evidences](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Hash] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[CaseID] [int] NOT NULL,
	[EvidenceClassificationID] [int] NOT NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
	[Extension] [nvarchar](max) NULL,
	[SizeKB] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Evidences] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FaultLinkeds]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FaultLinkeds](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CaseID] [int] NOT NULL,
	[LinkedID] [int] NOT NULL,
	[FaultID] [int] NOT NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FaultLinkeds] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faults]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faults](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[FaultTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Faults] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FaultTypes]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FaultTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_FaultTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gerencias]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gerencias](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Gerencias] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IntervieweeTypes]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IntervieweeTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_IntervieweeTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interviews]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interviews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IntervieweeTypeID] [int] NOT NULL,
	[CaseID] [int] NOT NULL,
	[LinkedID] [int] NOT NULL,
	[DateInterview] [datetime2](7) NOT NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Interviews] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Linkeds]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Linkeds](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LinkTypeID] [int] NOT NULL,
	[CaseID] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Identification] [nvarchar](max) NULL,
	[Birthdate] [datetime2](7) NULL,
	[Phone] [nvarchar](max) NULL,
	[CellPhone] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Linkeds] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LinkTypes]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_LinkTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceptionMedia]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceptionMedia](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ReceptionMedia] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recommendations]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recommendations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](450) NOT NULL,
	[UnitToWhichItIsAddressed] [nvarchar](max) NOT NULL,
	[Contact] [nvarchar](max) NOT NULL,
	[Response] [nvarchar](max) NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
	[RecommendationStatusID] [int] NOT NULL,
	[CaseID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[RecommendationTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Recommendations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecommendationStatuses]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecommendationStatuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_RecommendationStatuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecommendationTypes]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecommendationTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_RecommendationTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoveryHistories]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoveryHistories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AmountRecovery] [decimal](18, 2) NOT NULL,
	[Observations] [nvarchar](max) NULL,
	[DateRecovery] [datetime2](7) NOT NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
	[CaseID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_RecoveryHistories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RelLinkedFaults]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelLinkedFaults](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DateFault] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[UserNameRegistered] [nvarchar](max) NULL,
	[DateRegistered] [datetime2](7) NOT NULL,
	[FaultID] [int] NOT NULL,
	[LinkedID] [int] NOT NULL,
 CONSTRAINT [PK_RelLinkedFaults] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLevels]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLevels](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserLevels] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](450) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[WorkingGroupID] [int] NOT NULL,
	[UserLevelID] [int] NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[LastPasswordChangeDate] [datetime2](7) NULL,
	[IDGerencia] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VwCostCenters]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VwCostCenters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Center] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Code] [int] NOT NULL,
	[Full_Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_VwCostCenters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vwEmployees]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vwEmployees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [nvarchar](max) NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[Nombre_Completo] [nvarchar](max) NULL,
	[Usuario] [nvarchar](max) NULL,
	[Correo] [nvarchar](max) NULL,
	[Identificacion] [nvarchar](max) NULL,
	[ID_de_Puesto] [nvarchar](max) NULL,
	[Puesto] [nvarchar](max) NULL,
	[Tipo_de_Empleado] [nvarchar](max) NULL,
	[Gerencia] [nvarchar](max) NULL,
	[Ubicacion] [nvarchar](max) NULL,
	[Empresa] [nvarchar](max) NULL,
	[Fecha_de_Nacimiento] [datetime2](7) NULL,
	[Fecha_de_Ingreso] [datetime2](7) NULL,
 CONSTRAINT [PK_vwEmployees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VwOficinas]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VwOficinas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Oficina] [nvarchar](max) NULL,
	[Oficina_Parsed] [nvarchar](max) NULL,
	[Nombre_Oficina] [nvarchar](max) NULL,
	[Tipo_Oficina] [nvarchar](max) NULL,
	[Provincia] [nvarchar](max) NULL,
	[Localidad] [nvarchar](max) NULL,
	[Oficina_Completa] [nvarchar](max) NULL,
 CONSTRAINT [PK_VwOficinas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkingGroups]    Script Date: 2/6/2025 11:29:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_WorkingGroups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Access_Roles_AccessID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Access_Roles_AccessID] ON [dbo].[Access_Roles]
(
	[AccessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Access_Roles_RoleID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Access_Roles_RoleID] ON [dbo].[Access_Roles]
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Accesses_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Accesses_Name] ON [dbo].[Accesses]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Binnacles_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Binnacles_CaseID] ON [dbo].[Binnacles]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Binnacles_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Binnacles_Name] ON [dbo].[Binnacles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Binnacles_UserID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Binnacles_UserID] ON [dbo].[Binnacles]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CaseAssignments_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_CaseAssignments_CaseID] ON [dbo].[CaseAssignments]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CaseAssignments_UserID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_CaseAssignments_UserID] ON [dbo].[CaseAssignments]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cases_CaseStatusID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Cases_CaseStatusID] ON [dbo].[Cases]
(
	[CaseStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cases_CaseTypeID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Cases_CaseTypeID] ON [dbo].[Cases]
(
	[CaseTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cases_GerenciaID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Cases_GerenciaID] ON [dbo].[Cases]
(
	[GerenciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cases_ReceptionMediumID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Cases_ReceptionMediumID] ON [dbo].[Cases]
(
	[ReceptionMediumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cases_UserID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Cases_UserID] ON [dbo].[Cases]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CaseStatusChanges_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_CaseStatusChanges_CaseID] ON [dbo].[CaseStatusChanges]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CaseStatusChanges_CaseStatusID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_CaseStatusChanges_CaseStatusID] ON [dbo].[CaseStatusChanges]
(
	[CaseStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CaseStatuses_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CaseStatuses_Name] ON [dbo].[CaseStatuses]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CaseTypes_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CaseTypes_Name] ON [dbo].[CaseTypes]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EvidenceClassifications_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_EvidenceClassifications_Name] ON [dbo].[EvidenceClassifications]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Evidences_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Evidences_CaseID] ON [dbo].[Evidences]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Evidences_EvidenceClassificationID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Evidences_EvidenceClassificationID] ON [dbo].[Evidences]
(
	[EvidenceClassificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FaultLinkeds_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_FaultLinkeds_CaseID] ON [dbo].[FaultLinkeds]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FaultLinkeds_FaultID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_FaultLinkeds_FaultID] ON [dbo].[FaultLinkeds]
(
	[FaultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FaultLinkeds_LinkedID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_FaultLinkeds_LinkedID] ON [dbo].[FaultLinkeds]
(
	[LinkedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Faults_FaultTypeID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Faults_FaultTypeID] ON [dbo].[Faults]
(
	[FaultTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Gerencias_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Gerencias_Name] ON [dbo].[Gerencias]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Interviews_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Interviews_CaseID] ON [dbo].[Interviews]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Interviews_IntervieweeTypeID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Interviews_IntervieweeTypeID] ON [dbo].[Interviews]
(
	[IntervieweeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Interviews_LinkedID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Interviews_LinkedID] ON [dbo].[Interviews]
(
	[LinkedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Linkeds_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Linkeds_CaseID] ON [dbo].[Linkeds]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Linkeds_LinkTypeID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Linkeds_LinkTypeID] ON [dbo].[Linkeds]
(
	[LinkTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_LinkTypes_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_LinkTypes_Name] ON [dbo].[LinkTypes]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReceptionMedia_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ReceptionMedia_Name] ON [dbo].[ReceptionMedia]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Recommendations_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Recommendations_CaseID] ON [dbo].[Recommendations]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Recommendations_RecommendationStatusID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Recommendations_RecommendationStatusID] ON [dbo].[Recommendations]
(
	[RecommendationStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Recommendations_RecommendationTypeID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Recommendations_RecommendationTypeID] ON [dbo].[Recommendations]
(
	[RecommendationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Recommendations_Title]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Recommendations_Title] ON [dbo].[Recommendations]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Recommendations_UserID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Recommendations_UserID] ON [dbo].[Recommendations]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RecommendationStatuses_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_RecommendationStatuses_Name] ON [dbo].[RecommendationStatuses]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RecommendationTypes_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_RecommendationTypes_Name] ON [dbo].[RecommendationTypes]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RecoveryHistories_CaseID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_RecoveryHistories_CaseID] ON [dbo].[RecoveryHistories]
(
	[CaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RecoveryHistories_UserID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_RecoveryHistories_UserID] ON [dbo].[RecoveryHistories]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RelLinkedFaults_FaultID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_RelLinkedFaults_FaultID] ON [dbo].[RelLinkedFaults]
(
	[FaultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RelLinkedFaults_LinkedID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_RelLinkedFaults_LinkedID] ON [dbo].[RelLinkedFaults]
(
	[LinkedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Roles_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Roles_Name] ON [dbo].[Roles]
(
	[Name] ASC
)
WHERE ([Name] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserLevels_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserLevels_Name] ON [dbo].[UserLevels]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserLevels_RoleID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserLevels_RoleID] ON [dbo].[UserLevels]
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_UserLevelID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Users_UserLevelID] ON [dbo].[Users]
(
	[UserLevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_UserName]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_UserName] ON [dbo].[Users]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_WorkingGroupID]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE NONCLUSTERED INDEX [IX_Users_WorkingGroupID] ON [dbo].[Users]
(
	[WorkingGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_WorkingGroups_Name]    Script Date: 2/6/2025 11:29:41 a. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_WorkingGroups_Name] ON [dbo].[WorkingGroups]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Evidences] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DateRegistered]
GO
ALTER TABLE [dbo].[Evidences] ADD  DEFAULT ((0.0)) FOR [SizeKB]
GO
ALTER TABLE [dbo].[Interviews] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DateInterview]
GO
ALTER TABLE [dbo].[Interviews] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DateRegistered]
GO
ALTER TABLE [dbo].[Linkeds] ADD  DEFAULT (N'') FOR [Code]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__PasswordH__73852659]  DEFAULT (N'') FOR [PasswordHash]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IDGerencia]
GO
ALTER TABLE [dbo].[Access_Roles]  WITH CHECK ADD  CONSTRAINT [FK_Access_Roles_Accesses_AccessID] FOREIGN KEY([AccessID])
REFERENCES [dbo].[Accesses] ([ID])
GO
ALTER TABLE [dbo].[Access_Roles] CHECK CONSTRAINT [FK_Access_Roles_Accesses_AccessID]
GO
ALTER TABLE [dbo].[Access_Roles]  WITH CHECK ADD  CONSTRAINT [FK_Access_Roles_Roles_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Access_Roles] CHECK CONSTRAINT [FK_Access_Roles_Roles_RoleID]
GO
ALTER TABLE [dbo].[Binnacles]  WITH CHECK ADD  CONSTRAINT [FK_Binnacles_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Binnacles] CHECK CONSTRAINT [FK_Binnacles_Cases_CaseID]
GO
ALTER TABLE [dbo].[Binnacles]  WITH CHECK ADD  CONSTRAINT [FK_Binnacles_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Binnacles] CHECK CONSTRAINT [FK_Binnacles_Users_UserID]
GO
ALTER TABLE [dbo].[CaseAssignments]  WITH CHECK ADD  CONSTRAINT [FK_CaseAssignments_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CaseAssignments] CHECK CONSTRAINT [FK_CaseAssignments_Cases_CaseID]
GO
ALTER TABLE [dbo].[CaseAssignments]  WITH CHECK ADD  CONSTRAINT [FK_CaseAssignments_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[CaseAssignments] CHECK CONSTRAINT [FK_CaseAssignments_Users_UserID]
GO
ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_CaseStatuses_CaseStatusID] FOREIGN KEY([CaseStatusID])
REFERENCES [dbo].[CaseStatuses] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_CaseStatuses_CaseStatusID]
GO
ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_CaseTypes_CaseTypeID] FOREIGN KEY([CaseTypeID])
REFERENCES [dbo].[CaseTypes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_CaseTypes_CaseTypeID]
GO
ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_Gerencias_GerenciaID] FOREIGN KEY([GerenciaID])
REFERENCES [dbo].[Gerencias] ([ID])
GO
ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_Gerencias_GerenciaID]
GO
ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_ReceptionMedia_ReceptionMediumID] FOREIGN KEY([ReceptionMediumID])
REFERENCES [dbo].[ReceptionMedia] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_ReceptionMedia_ReceptionMediumID]
GO
ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_Users_UserID]
GO
ALTER TABLE [dbo].[CaseStatusChanges]  WITH CHECK ADD  CONSTRAINT [FK_CaseStatusChanges_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
GO
ALTER TABLE [dbo].[CaseStatusChanges] CHECK CONSTRAINT [FK_CaseStatusChanges_Cases_CaseID]
GO
ALTER TABLE [dbo].[CaseStatusChanges]  WITH CHECK ADD  CONSTRAINT [FK_CaseStatusChanges_CaseStatuses_CaseStatusID] FOREIGN KEY([CaseStatusID])
REFERENCES [dbo].[CaseStatuses] ([ID])
GO
ALTER TABLE [dbo].[CaseStatusChanges] CHECK CONSTRAINT [FK_CaseStatusChanges_CaseStatuses_CaseStatusID]
GO
ALTER TABLE [dbo].[Evidences]  WITH CHECK ADD  CONSTRAINT [FK_Evidences_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Evidences] CHECK CONSTRAINT [FK_Evidences_Cases_CaseID]
GO
ALTER TABLE [dbo].[Evidences]  WITH CHECK ADD  CONSTRAINT [FK_Evidences_EvidenceClassifications_EvidenceClassificationID] FOREIGN KEY([EvidenceClassificationID])
REFERENCES [dbo].[EvidenceClassifications] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Evidences] CHECK CONSTRAINT [FK_Evidences_EvidenceClassifications_EvidenceClassificationID]
GO
ALTER TABLE [dbo].[FaultLinkeds]  WITH CHECK ADD  CONSTRAINT [FK_FaultLinkeds_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FaultLinkeds] CHECK CONSTRAINT [FK_FaultLinkeds_Cases_CaseID]
GO
ALTER TABLE [dbo].[FaultLinkeds]  WITH CHECK ADD  CONSTRAINT [FK_FaultLinkeds_Faults_FaultID] FOREIGN KEY([FaultID])
REFERENCES [dbo].[Faults] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FaultLinkeds] CHECK CONSTRAINT [FK_FaultLinkeds_Faults_FaultID]
GO
ALTER TABLE [dbo].[FaultLinkeds]  WITH CHECK ADD  CONSTRAINT [FK_FaultLinkeds_Linkeds_LinkedID] FOREIGN KEY([LinkedID])
REFERENCES [dbo].[Linkeds] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FaultLinkeds] CHECK CONSTRAINT [FK_FaultLinkeds_Linkeds_LinkedID]
GO
ALTER TABLE [dbo].[Faults]  WITH CHECK ADD  CONSTRAINT [FK_Faults_FaultTypes_FaultTypeID] FOREIGN KEY([FaultTypeID])
REFERENCES [dbo].[FaultTypes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Faults] CHECK CONSTRAINT [FK_Faults_FaultTypes_FaultTypeID]
GO
ALTER TABLE [dbo].[Interviews]  WITH CHECK ADD  CONSTRAINT [FK_Interviews_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Interviews] CHECK CONSTRAINT [FK_Interviews_Cases_CaseID]
GO
ALTER TABLE [dbo].[Interviews]  WITH CHECK ADD  CONSTRAINT [FK_Interviews_IntervieweeTypes_IntervieweeTypeID] FOREIGN KEY([IntervieweeTypeID])
REFERENCES [dbo].[IntervieweeTypes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Interviews] CHECK CONSTRAINT [FK_Interviews_IntervieweeTypes_IntervieweeTypeID]
GO
ALTER TABLE [dbo].[Interviews]  WITH CHECK ADD  CONSTRAINT [FK_Interviews_Linkeds_LinkedID] FOREIGN KEY([LinkedID])
REFERENCES [dbo].[Linkeds] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Interviews] CHECK CONSTRAINT [FK_Interviews_Linkeds_LinkedID]
GO
ALTER TABLE [dbo].[Linkeds]  WITH CHECK ADD  CONSTRAINT [FK_Linkeds_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
GO
ALTER TABLE [dbo].[Linkeds] CHECK CONSTRAINT [FK_Linkeds_Cases_CaseID]
GO
ALTER TABLE [dbo].[Linkeds]  WITH CHECK ADD  CONSTRAINT [FK_Linkeds_LinkTypes_LinkTypeID] FOREIGN KEY([LinkTypeID])
REFERENCES [dbo].[LinkTypes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Linkeds] CHECK CONSTRAINT [FK_Linkeds_LinkTypes_LinkTypeID]
GO
ALTER TABLE [dbo].[Recommendations]  WITH CHECK ADD  CONSTRAINT [FK_Recommendations_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Recommendations] CHECK CONSTRAINT [FK_Recommendations_Cases_CaseID]
GO
ALTER TABLE [dbo].[Recommendations]  WITH CHECK ADD  CONSTRAINT [FK_Recommendations_RecommendationStatuses_RecommendationStatusID] FOREIGN KEY([RecommendationStatusID])
REFERENCES [dbo].[RecommendationStatuses] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Recommendations] CHECK CONSTRAINT [FK_Recommendations_RecommendationStatuses_RecommendationStatusID]
GO
ALTER TABLE [dbo].[Recommendations]  WITH CHECK ADD  CONSTRAINT [FK_Recommendations_RecommendationTypes_RecommendationTypeID] FOREIGN KEY([RecommendationTypeID])
REFERENCES [dbo].[RecommendationTypes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Recommendations] CHECK CONSTRAINT [FK_Recommendations_RecommendationTypes_RecommendationTypeID]
GO
ALTER TABLE [dbo].[Recommendations]  WITH CHECK ADD  CONSTRAINT [FK_Recommendations_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Recommendations] CHECK CONSTRAINT [FK_Recommendations_Users_UserID]
GO
ALTER TABLE [dbo].[RecoveryHistories]  WITH CHECK ADD  CONSTRAINT [FK_RecoveryHistories_Cases_CaseID] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Cases] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecoveryHistories] CHECK CONSTRAINT [FK_RecoveryHistories_Cases_CaseID]
GO
ALTER TABLE [dbo].[RecoveryHistories]  WITH CHECK ADD  CONSTRAINT [FK_RecoveryHistories_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[RecoveryHistories] CHECK CONSTRAINT [FK_RecoveryHistories_Users_UserID]
GO
ALTER TABLE [dbo].[RelLinkedFaults]  WITH CHECK ADD  CONSTRAINT [FK_RelLinkedFaults_Faults_FaultID] FOREIGN KEY([FaultID])
REFERENCES [dbo].[Faults] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RelLinkedFaults] CHECK CONSTRAINT [FK_RelLinkedFaults_Faults_FaultID]
GO
ALTER TABLE [dbo].[RelLinkedFaults]  WITH CHECK ADD  CONSTRAINT [FK_RelLinkedFaults_Linkeds_LinkedID] FOREIGN KEY([LinkedID])
REFERENCES [dbo].[Linkeds] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RelLinkedFaults] CHECK CONSTRAINT [FK_RelLinkedFaults_Linkeds_LinkedID]
GO
ALTER TABLE [dbo].[UserLevels]  WITH CHECK ADD  CONSTRAINT [FK_UserLevels_Roles_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[UserLevels] CHECK CONSTRAINT [FK_UserLevels_Roles_RoleID]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserLevels_UserLevelID] FOREIGN KEY([UserLevelID])
REFERENCES [dbo].[UserLevels] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserLevels_UserLevelID]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_WorkingGroups_WorkingGroupID] FOREIGN KEY([WorkingGroupID])
REFERENCES [dbo].[WorkingGroups] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_WorkingGroups_WorkingGroupID]
GO
USE [master]
GO
ALTER DATABASE [CaseControl_Dev] SET  READ_WRITE 
GO
