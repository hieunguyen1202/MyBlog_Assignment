
GO
/****** Object:  Database [MyBlogPRN211]    Script Date: 3/20/2024 9:44:46 AM ******/
CREATE DATABASE [MyBlogPRN211]

ALTER DATABASE [MyBlogPRN211] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyBlogPRN211].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyBlogPRN211] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyBlogPRN211] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyBlogPRN211] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyBlogPRN211] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyBlogPRN211] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET RECOVERY FULL 
GO
ALTER DATABASE [MyBlogPRN211] SET  MULTI_USER 
GO
ALTER DATABASE [MyBlogPRN211] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyBlogPRN211] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyBlogPRN211] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyBlogPRN211] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyBlogPRN211] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyBlogPRN211] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyBlogPRN211', N'ON'
GO
ALTER DATABASE [MyBlogPRN211] SET QUERY_STORE = OFF
GO
USE [MyBlogPRN211]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 3/20/2024 9:44:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Phone] [varchar](10) NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3/20/2024 9:44:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Thumb] [nvarchar](255) NULL,
	[Published] [bit] NOT NULL,
	[Ordering] [int] NULL,
	[Parents] [int] NULL,
	[Levels] [int] NULL,
	[Icon] [nvarchar](255) NULL,
	[Cover] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 3/20/2024 9:44:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](250) NULL,
	[Email] [nvarchar](100) NULL,
	[PostId] [int] NULL,
	[Published] [bit] NULL,
	[CreateAt] [datetime] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 3/20/2024 9:44:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [varchar](max) NULL,
	[Published] [bit] NOT NULL,
	[CreateTime] [datetime] NULL,
	[Author] [nvarchar](255) NULL,
	[SContent] [nvarchar](255) NULL,
	[CatID] [int] NULL,
	[isHot] [bit] NOT NULL,
	[isNewFeed] [bit] NOT NULL,
	[AccountID] [int] NULL,
	[Tags] [nvarchar](max) NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/20/2024 9:44:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountID], [FullName], [Email], [Password], [Phone], [RoleID]) VALUES (1, N'Admin', N'hieunguyenhd2003@gmail.com', N'123', N'0367752115', 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CatID], [CatName], [Title], [Thumb], [Published], [Ordering], [Parents], [Levels], [Icon], [Cover], [Description]) VALUES (2, N'Y Tế', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Categories] ([CatID], [CatName], [Title], [Thumb], [Published], [Ordering], [Parents], [Levels], [Icon], [Cover], [Description]) VALUES (3, N'Chính Trị', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Categories] ([CatID], [CatName], [Title], [Thumb], [Published], [Ordering], [Parents], [Levels], [Icon], [Cover], [Description]) VALUES (4, N'Giáo Dục', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Categories] ([CatID], [CatName], [Title], [Thumb], [Published], [Ordering], [Parents], [Levels], [Icon], [Cover], [Description]) VALUES (10, N'Công Nghệ', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Categories] ([CatID], [CatName], [Title], [Thumb], [Published], [Ordering], [Parents], [Levels], [Icon], [Cover], [Description]) VALUES (11, N'Xã Hội', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Categories] ([CatID], [CatName], [Title], [Thumb], [Published], [Ordering], [Parents], [Levels], [Icon], [Cover], [Description]) VALUES (12, N'Tài Chính', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (12, N'newwww', N'hieunguyenhd2003@gmail.com', 1, 1, CAST(N'2024-03-05T14:07:21.427' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (13, N'okkk day', N'hieunguyenhd2003@gmail.com', 1, 1, CAST(N'2024-03-05T14:10:39.473' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (48, N'tuyet viu', N'hieunguyenhd2003@gmail.com', 1, 1, CAST(N'2024-03-07T11:18:42.660' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (54, N'neww', N'sfdsfdsfs@gmail.com', 1, 1, CAST(N'2024-03-07T11:56:58.703' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (61, N'tuyet voi qua', N'sfdsfdsfs@gmail.com', 107, 1, CAST(N'2024-03-07T16:56:12.897' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (69, N'hihi', N'hieunguyenhd2003@gmail.comewrew', 97, 1, CAST(N'2024-03-19T14:17:23.047' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (73, N'bai viet hay', N'hieunguyenhd2003@gmail.com', 147, 1, CAST(N'2024-03-19T15:32:13.050' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (74, N'thong tin moi', N'sfdsfdsfs@gmail.com', 147, 1, CAST(N'2024-03-19T15:32:36.153' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (75, N'good', N'hieunguyenhd2003@gmail.com', 147, 1, CAST(N'2024-03-19T15:33:07.567' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (76, N'wow', N'hieunguyenhd2003@gmail.com', 151, 1, CAST(N'2024-03-19T15:37:59.223' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (77, N'good', N'hieunguyenhd2003@gmail.com', 151, 1, CAST(N'2024-03-19T15:38:07.117' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (78, N'nice (y)', N'rtyrt@gmail.com', 151, 1, CAST(N'2024-03-19T15:38:24.563' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (79, N'tuyet voi', N'hieunguyenhd2003@gmail.com', 151, 1, CAST(N'2024-03-19T15:38:46.037' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (80, N'Mọi người cẩn thận hơn', N'rtyrt@gmail.com', 97, 1, CAST(N'2024-03-19T15:39:24.863' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (81, N'gooddd', N'hieunguyenhd2003@gmail.com', 97, 1, CAST(N'2024-03-19T15:40:16.907' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (82, N'Tôi cũng thích xe nàyyyy', N'hieunguyenhd2003@gmail.com', 7, 1, CAST(N'2024-03-19T15:41:04.100' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (83, N'Sợ quáaaa', N'hieunguyenhd2003@gmail.com', 150, 1, CAST(N'2024-03-19T15:44:09.763' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (84, N'@@', N'sfdsfdsfs@gmail.com', 150, 1, CAST(N'2024-03-19T15:44:22.723' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (85, N'bai viet bo ich', N'hieunguyenhd2003@gmail.com', 144, 1, CAST(N'2024-03-20T00:04:25.840' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (86, N'good', N'rtyrt@gmail.com', 1, 1, CAST(N'2024-03-20T07:58:57.510' AS DateTime))
INSERT [dbo].[Comment] ([CommentId], [Content], [Email], [PostId], [Published], [CreateAt]) VALUES (87, N'good', N'hieunguyenhd2003@gmail.com', 7, 1, CAST(N'2024-03-20T08:40:46.383' AS DateTime))
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (1, N'Chung cư Smart City ', N'Nằm trên trên Đại lộ Thăng Long – huyết mạch Tây thủ đô, Vinhomes Smart City sở hữu vị trí đắc địa, cách Trung tâm Hội nghị Quốc gia chỉ 7 phút di chuyển.

Đây được coi là tọa độ vàng cho cuộc sống hiện đại, năng động. Không chỉ thuận lợi để cư dân di chuyển về mọi phía, Vinhomes Smart City còn là tiềm năng thúc đẩy sự gia tăng giá trị bất động sản. Nơi đây cũng tập trung cộng đồng người nước ngoài đang sống và làm việc tại Việt Nam.

Hệ thống đường Metro tương lai cùng tầm nhìn và quy hoạch ưu tiên khu phía Tây Hà Nội. Cùng với đó là sự phát triển của khu Công Nghệ Cao Láng Hòa Lạc thu hút rất nhiều chuyên gia, chuyên viên,…..', N'ocps206.jpg', 1, CAST(N'2023-02-20T00:00:00.000' AS DateTime), N'Phillip Nguyen', N'Nằm trên trên Đại lộ Thăng Long – huyết mạch Tây thủ đô', 11, 1, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (7, N'Porsche Panamera GTS với những công nghệ mới', N'Panamera GTS sẽ rất phù hợp với những ông chồng đam mê tốc độ và có đủ khả năng thuyết phục được "vợ cả" cùng trải nghiệm sự ồn ào của một mẫu xe thể thao, hoặc ngược lại. Tất nhiên nếu cả gia đình 3-4 người cùng thích một mẫu xe thể thao mạnh mẽ, ồn ào và đẹp, thì GTS có lẽ là phiên bản phù hợp nhất. Nếu muốn một mẫu xe hiền lành hơn, Panamera phiên bản khác với nhiều trang bị tiện nghi, bớt các phụ kiện thể thao sẽ là lựa chọn hợp lý.', N'panameragtszing2-1264.jpg', 1, CAST(N'2024-02-20T19:12:00.000' AS DateTime), N'Admin', N'Panamera GTS sẽ rất phù hợp với những ông chồng đam mê tốc độ ', 10, 1, 0, NULL, N'hot')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (97, N'Bùng phát hoạt động bói toán online', N'Trong bản tin tuần vừa qua, Cục An toàn thông tin (ATTT) - Bộ Thông tin & Truyền thông, tiếp tục cảnh báo một số hình thức lừa đảo trực tuyến đáng chú ý tại Việt Nam.
Lợi dụng lòng tin của nhiều người, kẻ lừa đảo tìm đến các hình thức như xem bói online, xâm nhập tài khoản Facebook hỏi mượn tiền, hoặc mạo danh quân nhân để chiếm đoạt tài sản.

', N'15821022024.jpg', 1, CAST(N'2024-02-22T13:25:33.197' AS DateTime), NULL, N'Trong và sau Tết, các hoạt động mê tín dị đoan trên mạng xã hội thu hút nhiều quan tâm', 11, 0, 1, NULL, N'trending')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (104, N'Ngày 15/08 ghi nhận 12.708 ca mắc Covid mới', N'Theo Time, công ty Moderna Therapeutics của Mỹ đã tạo ra được vắc xin ngừa Covid-19. Lô vắc-xin đầu tiên của Moderna Therapeutics đã được gửi đến Viện Dị ứng và bệnh truyền nhiễm quốc gia (NIAID) và quá trình thử nghiệm trên người có thể bắt đầu ngay từ tháng 4-2020.Báo South China Morning Post cho biết nhiều tín đồ của Tân Thiên Địa từng tổ chức các cuộc họp mặt ở Vũ Hán, Trung Quốc. Cho đến tháng 12-2019, họ mới dừng các hoạt động khi nhận thấy nhiều ca nhiễm bệnh Covid. Giáo phái Tân Thiên Địa này cho đến nay có liên quan tới hơn một nửa trong số hơn 1.000 ca nhiễm Covid-19 ở Hàn Quốc.
Tối 26-2, Sky News đưa tin một phụ nữ Hy Lạp, 38 tuổi, từng tới miền bắc Ý đã được xác định nhiễm Covid-19 và là trường hợp nhiễm đầu tiên ở Hy Lạp.

Hãng tin Reuters đưa tin ca dương tính với Covid-19 đầu tiên ở Brazil là một người đàn ông 61 tuổi, vừa ở Ý về. Đây cũng là ca nhiễm đầu tiên ở Nam Mỹ.', N'ca-moi-8-16494148644151778164481-1649417747916-16494177481861525689309.jpg', 1, CAST(N'2024-02-25T18:58:12.113' AS DateTime), N'Admin', N'Theo Time, công ty Moderna Therapeutics của Mỹ đã tạo ra được vắc xin', 2, 1, 0, NULL, N'xuhuong')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (106, N'Y tế hiện đại: Cứu bệnh nhân, giữ chân bác sĩ', N'Cùng với hệ thống BV của TP.HCM thì các BV trực thuộc bộ, ngành cũng góp phần rất lớn vào công tác chăm sóc sức khỏe người dân thành phố và cả khu vực phía nam. Trong đó, tên tuổi BV Chợ Rẫy đã quá quen thuộc, đặc biệt là với người mắc bệnh nặng, bệnh nhân nghèo. Đây là BV hạng đặc biệt, đa khoa chuyên sâu duy nhất ở phía nam trực thuộc Bộ Y tế.

BS Phạm Thanh Việt, Trưởng phòng Kế hoạch tổng hợp BV Chợ Rẫy, cho hay lĩnh vực mạnh nhất của Chợ Rẫy lâu nay là sọ não và ngoại thần kinh. Đây là chuyên khoa mũi nhọn của BV và luôn trong tình trạng quá tải. Dù hiện nay, nhiều BV đã làm được nhưng BV Chợ Rẫy vẫn đi đầu trong phẫu thuật sọ não, thần kinh.

Lĩnh vực phát triển mạnh tiếp theo của BV Chợ Rẫy là về tim mạch và đã phát triển thành trung tâm. Trung tâm tim mạch có các chuyên khoa: rối loạn nhịp, cắt đốt điện sinh lý, chuyên sâu về bệnh lý mạch máu, phẫu thuật tim trẻ em và người lớn... "Can thiệp mạch vành của BV Chợ Rẫy ngang tầm thế giới và là một trung tâm lớn của thế giới. Gắn liền với các chuyên khoa này là tên tuổi của TS-BS Nguyễn Thượng Nghĩa, BS Lý Ích Trung", BS Việt thông tin.

Trong lĩnh vực chấn thương chỉnh hình, Chợ Rẫy cũng là BV đi đầu về thay khớp gối, khớp háng, ghép xương 3D cá thể hóa, chỉnh bất thường cơ xương khớp; "chuyên trị" những ca khó. Những kỹ thuật này gắn liền tên tuổi của TS-BS Lê Văn Tuấn, Trưởng khoa Chấn thương chỉnh hình, và PGS-TS Đỗ Phước Hùng, Phó khoa Chấn thương chỉnh hình, Trưởng bộ môn Chấn thương chỉnh hình - Trường ĐH Y Dược TP.HCM.', N'bv1-1708859883087294806119.jpg', 1, CAST(N'2024-02-25T19:33:48.360' AS DateTime), N'Admin', N'Cùng với hệ thống BV của TP.HCM thì các BV trực thuộc bộ, ngành cũng góp phần', 2, 1, 0, NULL, N'software engineer')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (107, N'Pink Forever Bui mang chăn đi làm', N'Lương tháng 30 triệu vẫn bị bố mẹ thúc ép lấy chồng, trong bữa cơm tối, tôi đã có phản ứng khiến ông bà bất ngờ', N'hero-area.jpg', 0, CAST(N'2024-02-26T00:05:52.027' AS DateTime), NULL, N'Vòng 28 Premier League kết thúc với những kết quả khiến chặng đường còn lại của giải đấu mùa này trở nên kịch tính.', 2, 0, 0, NULL, NULL)
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (124, N'Giá USD tự do rớt mạnh', N'Giá bán USD trên thị trường tự do tiếp tục rơi thêm gần 300 đồng trong phiên giao dịch ngày 7/3, trong khi giá USD tại các ngân hàng thương mại chỉ giảm nhẹ.
Trong phiên giao dịch ngày 7/3, Ngân hàng Nhà nước giữ nguyên tỷ giá trung tâm của Đồng Việt Nam so với USD ở mức 24.017 đồng/USD. Xét trên biểu đồ hàng ngày, tỷ giá của cặp tiền tệ này đã giữ xu hướng tăng nhẹ liên tục trong nửa tháng qua. So với lần điều chỉnh vào giữa tháng 2, tỷ giá đã tăng 61 đồng.

Với biên độ giao dịch cho phép ở mức +/-5% theo tỷ giá trung tâm, tỷ giá sàn/trần đồng bạc xanh các ngân hàng thương mại được phép giao dịch hôm nay là 22.816 - 26.267 đồng/USD. Tại Sở giao dịch của NHNN, nhà điều hành vẫn niêm yết giá bán ngoại tệ này ở mức 25.167 đồng/USD, tăng 5 đồng so với phiên giao dịch trước đó.

Dù vậy, trong phiên giao dịch sáng nay, tỷ giá quy đổi ngoại tệ trên thị trường tự do vẫn tiếp tục rớt mạnh. Các đầu mối quy đổi ngoại tệ hiện chấp nhận mua vào đồng bạc xanh ở mức 25.280 đồng/USD, giảm 200 đồng so với phiên liền trước. Giá bán ra thậm chí rớt mạnh hơn tới 270 đồng, hiện giao dịch quanh mức 25.350 đồng/USD.

Đây là phiên thứ hai liên tiếp USD tự do rớt giá. Hôm qua (ngày 6/3), giá USD tự do giảm 60 đồng ở chiều mua và giảm 50 đồng ở chiều bán để giao dịch quanh mức 25.480 đồng/USD (mua) và 25.620 đồng/USD (bán). Như vậy chỉ sau hai ngày, giá USD trên thị trường tự do đã rớt hơn 300 đồng.

Còn trên kênh giao dịch của các ngân hàng thương mại, giá bán USD chỉ quay đầu giảm nhẹ, hiện về vùng 24.800-24.850 đồng/USD. Trong đó, Vietcombank niêm yết giá giao dịch đồng bạc xanh ở mức 24.480 - 24.850 đồng/USD (mua - bán), giảm 10 đồng so với phiên giao dịch liền trước.

Cùng giảm 25 đồng so với phiên giao dịch hôm qua là BIDV và Agribank. BIDV giao dịch đồng ngoại tệ này ở mức 24.535 - 24.845 đồng/USD, còn Agribank giao dịch ở mức 24.500 - 24.835 đồng/USD.

Tại VietinBank, tỷ giá USD tăng 38 đồng ở chiều mua vào, lên mức 24.448 đồng/USD, nhưng giảm 2 đồng ở chiều bán ra xuống còn 24.868 đồng/USD.

Ở nhóm ngân hàng thương mại tư nhân, đà giảm cũng chiếm ưu thế nhưng mức giảm tại mỗi đơn vị khác nhau. HDBank hiện niêm yết giá mua vào ở mức 24.430 đồng/USD và bán ra ở 24.970 đồng/USD, giảm 30 đồng so với phiên giao dịch liền trước.

Trong khi đó, Techcombank giữ nguyên giá mua - bán đồng bạc xanh hôm nay ở mức 24.528 - 24.860 đồng/USD; Eximbank giảm 10 đồng so với phiên liền trước, hiện giao dịch ở 24,460 - 24.850 đồng/USD; Sacombank giảm 2 đồng, hiện cố định ở 24.480 - 24.945 đồng/USD.

Với những diễn biến này, chênh lệch giá bán USD giữa kênh ngân hàng thương mại và thị trường tự do đã được rút ngắn còn 800 đồng ở chiều mua vào và 500 đồng ở chiều bán ra.

Trên thế giới, giá USD cũng quay đầu giảm. Chỉ số USD-Index - thước đo sức mạnh của đồng USD trong rổ tiền tệ - đã trượt 0,4 điểm xuống còn 103,3 điểm. Đồng bạc xanh giảm giá khi Chủ tịch Cục Dự trữ Liên bang Mỹ (Fed) Jerome Powell cho biết tiến trình đưa lạm phát về mục tiêu 2% vẫn còn xa, mặc dù Mỹ vẫn dự kiến giảm lãi suất cơ bản vào cuối năm nay.

Trong bài phát biểu trước Ủy ban Dịch vụ Tài chính Hạ viện, ông Jerome Powell cho rằng nếu kinh tế phát triển tốt, Fed sẽ nới lỏng chính sách tiền tệ trong năm 2024. Tuyên bố của Fed khiến nhà đầu tư giảm kỳ vọng cắt giảm lãi suất trong nửa đầu năm nay, dẫn đến đảo ngược đặt cược vào vàng thay vì đồng bạc xanh.', N'usd-giam-1681437687266635214453-32-0-672-1024-crop-1681437697298348550524.jpg', 1, CAST(N'2024-03-07T14:16:37.230' AS DateTime), N'Phillip Nguyen', N'Giá bán USD trên thị trường tự do tiếp tục rơi thêm gần 300 đồng', 12, 0, 1, NULL, N'it')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (142, N'Thương vụ bom tấn giữa Apple và Google sắp diễn ra', N'Nếu thỏa thuận giữa Apple và Google thành hiện thực, thành công phần lớn là nhờ vào quan hệ hợp tác phân phối công cụ tìm kiếm từ lâu nay của 2 công ty. Trong nhiều năm qua, Alphabet đã trả cho Táo khuyết hàng tỷ USD mỗi năm, để biến Google hãng trở thành công cụ tìm kiếm mặc định trong Safari.

Nguồn tin nội bộ cho hay hiện hai bên vẫn chưa chốt các điều khoản cụ thể và cách tích hợp Gemini vào thiết bị.

Theo Bloomberg, thương vụ sẽ đem lại cho Gemini một lợi thế khổng lồ với hàng tỷ người dùng tiềm năng từ iPhone. Nhưng đây cũng có thể là một dấu hiệu cho thấy Apple không còn đầu tư vào AI “cây nhà lá vườn” như nhiều người kỳ vọng.', N'1x_1_44_.jpg', 1, CAST(N'2024-03-19T09:09:03.887' AS DateTime), N'Phillip Nguyen', N'Bloomberg đánh giá động thái này sẽ tạo tiền đề cho một thương vụ bạc tỷ, làm rung chuyển ngành công nghiệp AI.', 10, 1, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (143, N'aaaa', N'aaaa', N'261899-ngon-ngu-trung-1-1-08175271.jpg', 0, CAST(N'2024-03-19T10:12:23.763' AS DateTime), NULL, N'aaa', 2, 0, 0, NULL, NULL)
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (144, N'Nhập viện ngay nếu có biểu hiện này của cúm mùa', N'Tôi và con gái 5 tuổi đang mắc cúm mùa. Xin hỏi tôi cần theo dõi dấu hiệu nào để biết bệnh chuyển nặng và nhập viện kịp thời?

Trung tâm Kiểm soát và Phòng ngừa Dịch bệnh Mỹ (CDC)
Bất cứ ai cũng có thể bị cúm mùa, ngay cả những người khỏe mạnh và các vấn đề nghiêm trọng liên quan đến cúm có thể xảy ra với bất kỳ ai ở mọi lứa tuổi. Các triệu chứng điển hình của bệnh cúm bao gồm sốt, ho, đau họng, đau nhức cơ thể và mệt mỏi.

Hầu hết người bị cúm mùa sẽ hồi phục sau vài ngày đến dưới 2 tuần, nhưng một số người sẽ bị các biến chứng (chẳng hạn viêm phổi) do cúm, một số biến chứng có thể đe dọa tính mạng và dẫn đến tử vong.', N'cum_mua.jpg', 1, CAST(N'2024-03-19T14:27:18.943' AS DateTime), N'Phillip Nguyen', N'Bất cứ ai cũng có thể bị cúm mùa, ngay cả những người khỏe mạnh ', 2, 0, 1, NULL, N'yte')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (145, N'Thực phẩm nên tránh xa khi bụng yếu', N'Hầu như ai cũng đều từng bị bụng yếu gây tiêu chảy. Trên thực tế, mỗi người gần như gặp phải tình trạng này vài lần trong một năm sau một bữa ăn không ngon, nhiễm trùng do vi khuẩn hoặc một số cơ chế kích thích dạ dày khác khiến phân lỏng và chảy nước.

Điều quan trọng là khi bị tiêu chảy, người bệnh vẫn phải duy trì việc ăn uống để đảm bảo sức khỏe. Việc lựa chọn thực phẩm và đồ uống đúng có thể giúp giảm bớt các triệu chứng tiêu chảy và hồi phục nhanh hơn.

Tuy nhiên, mọi người cũng cần cảnh giác và tránh một số loại thực phẩm khiến tình trạng trầm trọng hơn.', N'tieu_chay_1_1.jpg', 1, CAST(N'2024-03-19T14:29:16.080' AS DateTime), N'Admin', N'Hầu như ai cũng đều từng bị bụng yếu gây tiêu chảy. ', 2, 0, 1, NULL, N'yte')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (146, N'Xác định được nguyên nhân gây vụ ngộ độc cơm gà ở Nha Trang', N'Nguyên nhân khiến hàng loạt người nhập viện sau ăn cơm gà ở quán Trâm Anh là ngộ độc thực phẩm bởi vi khuẩn Salmonella spp, Bacillus cereus, Staphylococcus aureus gây nên.
Nhận định trên của Chi cục An toàn vệ sinh thực phẩm Khánh Hòa đưa ra dựa trên kết quả kiểm nghiệm các mẫu bệnh phẩm, mẫu thức ăn… của Viện Pasteur Nha Trang và các thông tin điều tra dịch tễ, triệu chứng lâm sàng của người bị ngộ độc.

Theo Chi cục An toàn thực phẩm tỉnh Khánh Hòa, các loại vi khuẩn gây nên ngộ độc chính là Salmonella spp, Bacillus cereus, Staphylococcus aureus.

Cụ thể, kết quả kiểm nghiệm của Viện Pasteur Nha Trang cho thấy: phát hiện vi khuẩn Bacillus cereus và vi khuẩn Escherichia coli trong mẫu rau dưa của quán Trâm Anh.', N'doc_hai_17107718965431744494656.jpg', 1, CAST(N'2024-03-19T14:31:00.853' AS DateTime), N'Admin', N'Nguyên nhân khiến hàng loạt người nhập viện sau ăn cơm gà ở quán Trâm Anh', 2, 1, 0, NULL, N'yte')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (147, N'Bị tố ''7h30 vẫn chưa làm việc'', bệnh viện ở Hà Nội nói gì?', N'Gần đây, mạng xã hội Tiktok xuất hiện video với nội dung "Bệnh nhân nhịn đói cả đêm để chờ lấy máu, 7h30 vẫn chưa có nhân viên đi làm". Nội dung video thể hiện quay ở Bệnh viện Thanh Nhàn (Hai Bà Trưng, Hà Nội).
Trong video, một người phụ nữ liên tục gọi tên Giám đốc Bệnh viện Thanh Nhàn và hỏi rằng "bao giờ bệnh viện mới tiếp đón bệnh nhân?", "bao giờ mới xong buổi khám ngày hôm nay?", "nhân viên của ông đang ở đâu?", "bệnh nhân nhịn đói cả đêm đến đây chưa được lấy mẫu xét nghiệm",… Video cho thấy khá đông bệnh nhân đang ngồi chờ nhưng không có nhân viên nào của bệnh viện.

Video nhận được rất nhiều lượt tương tác từ phía cộng đồng mạng. Một số ý kiến bình luận như: Tài khoản Võ Hòa: "Cứu người mà làm việc 8h là quan liêu quá"; Tài khoản Chu Việt: "Phải có người lên tiếng như vậy thì mới lấy được sự công bằng cho người dân, cảm ơn em…"; Tài khoản Thị Mía và Bí Đô: "Đi khám từ 6h đến 4h chiều mới xong. Sợ đến già"…', N'3_3eaba3da_e82d_4635_bc66_af5fa5f18251.jpg', 1, CAST(N'2024-03-19T14:32:53.577' AS DateTime), N'Admin', N'Bệnh viện Thanh Nhàn cho biết lịch làm việc từ 7h30 nhưng video được quay vào lúc 7h20 tại sảnh tiếp', 2, 0, 1, NULL, N'yte')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (148, N'Cảnh tượng khiến người Hàn hoang mang tột độ', N'Do các bệnh viện đại học thiếu bác sĩ, nhiều bệnh nhân tìm đến bệnh viện chuyên khoa với hy vọng sẽ được khám bệnh nhanh hơn. Tuy nhiên, tình hình của những bệnh viện nhỏ cũng không khả quan hơn là mấy vì lượng người đổ về quá đông, quy mô bệnh viện không đáp ứng kịp.

Sáng 15/3, theo ghi nhận của phóng viên Newis, sảnh của một bệnh viện chuyên khoa ở quận Yeongdeungpo (Seoul) chật kín bệnh nhân chờ khám.

Bà Jeong Mi-hyeon (57 tuổi) là một trong số rất nhiều người đang chờ khám ở bệnh viện này. Bà đến bệnh viện vì cơn đau đầu do chứng phình mạch máu não.

"Tôi bất ngờ phát hiện bệnh này khi đi khám sức khỏe định kỳ, nhưng giờ có đến bệnh viện thì tôi cũng không được điều trị ngay. Vì thế, tôi đành phải đến bệnh viện chuyên khoa để kiểm tra trước", bà Jeong thông tin.

Một người phụ nữ khác khoảng 50 tuổi cũng đưa mẹ đến bệnh viện cấp cứu vì bệnh viện lớn quá tải. Bà cho biết mẹ bà bị ngã gãy xương và cần cấp cứu ngay, biết tin bác sĩ bệnh viện chuyên khoa không đình công nên bà đưa mẹ đến.

Một lãnh đạo của bệnh viện chuyên khoa này cho biết kể từ khi bác sĩ thực tập ở bệnh viện lớn đình công, số lượng bệnh nhân tại đây tăng mạnh nên bệnh viện cũng phải tăng ca liên tục, các bác sĩ buộc phải đảm nhận nhiều nhiệm vụ hơn.', N'bac_si_dinh_cong.jpg', 1, CAST(N'2024-03-19T14:34:40.537' AS DateTime), NULL, N'Gần một tháng bác sĩ thực tập đình công, bệnh nhân Hàn Quốc nóng ruột vì không được điều trị và buộc', 2, 0, 0, NULL, N'yte')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (149, N'Đây là AI siêu mạnh của Apple', N'Một số chuyên gia cho biết Ajax thậm chí còn được đánh giá cao hơn cả ChatGPT 3.5. Với mục tiêu giúp các thiết bị trong hệ sinh thái giao tiếp với nhau, tài khoản chuyên rò rỉ tin công nghệ yeux1122 của Hàn Quốc tiết lộ Apple còn đang “huấn luyện” cách giao tiếp sao cho tự nhiên nhất có thể chứ không chỉ là những câu ra lệnh cứng nhắc.

Apple còn dự tính bổ sung các tính năng như tự động tóm tắt, tự động hoàn thành vào các ứng dụng cốt lõi và phần mềm năng suất như Pages và Keynote. Họ cũng nỗ lực tích hợp AI vào các dịch vụ như Apple Music để tối ưu hóa việc tạo danh sách phát.

Trong cuộc họp cổ đông thường niên của Apple vào đầu tháng 3, CEO Tim Cook hé lộ cuối năm nay, AI tạo sinh sẽ có mặt trên các sản phẩm của Táo khuyết.

"Apple nhận thấy tiềm năng đột phá đáng kinh ngạc của AI tạo sinh. Đó là lý do mà chúng tôi đang đầu tư đáng kể vào lĩnh vực này. Chúng tôi tin rằng điều đó sẽ mở ra những cơ hội mang tính đột phá cho người dùng khi nhắc đến năng suất, khả năng giải quyết vấn đề và hơn thế nữa", Tim Cook cho biết.

Để tăng thêm sức mạnh cho đội ngũ AI của mình, theo Bloomberg, Apple ngày 15/3 đã mua lại startup DarwinAI của Canada với số tiền chưa được tiết lộ.', N'girl_ai_gettyimages_1487960494.jpg', 1, CAST(N'2024-03-19T14:55:15.197' AS DateTime), N'Admin', N'Mô hình ngôn ngữ lớn (LLM) của Apple có tên MM1 ', 10, 0, 0, NULL, N'technology')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (150, N'Mạo danh ''Cục An ninh mạng'' để lừa đảo', N'Để hạn chế tình trạng bị lừa, Cục ATTT nhấn mạnh người dân không cung cấp thông tin cá nhân cho bất cứ ai dưới mọi hình thức.

Khi có cuộc gọi lạ hoặc tiếp xúc các hội nhóm cung cấp dịch vụ, tuyệt đối không giao dịch chuyển tiền khi chưa tìm hiểu, xác minh danh tính của đối tượng.

Nếu bị lừa đảo tài chính, người dân nên tuyệt đối cẩn trọng trước các website hoặc hội/nhóm liên tục chạy quảng cáo, mời chào lấy lại tiền đã mất.

Ngoài ra, có thể kiểm tra các trang tin chính thống của cơ quan công an tại phần “Liên kết website” trên Cổng Thông tin điện tử - Bộ Công an tại địa chỉ mps.gov.vn, hoặc bocongan.gov.vn.

Đối với các tài khoản trên mạng xã hội và ứng dụng OTT, Cục An ninh mạng và phòng chống tội phạm sử dụng công nghệ cao đang rà soát và sẽ công khai các đường dẫn để người dân nắm được.', N'10917032024.jpg', 1, CAST(N'2024-03-19T14:56:36.417' AS DateTime), N'Admin', N'Nhiều fanpage mạo danh "Cục An ninh mạng" để cung cấp dịch vụ lấy lại tiền bị lừa', 10, 1, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (151, N'Hàng triệu người châu Phi mất mạng Internet', N'“Việc sửa chữa có thể mất vài tuần đến vài tháng, tùy thuộc vào vị trí hư hỏng, những bộ phận cần sửa chữa và điều kiện thời tiết. Quy trình phân công các tàu sửa chữa cũng phụ thuộc vào nhiều yếu tố khác nhau, trong đó có cả quyền sở hữu các dây cáp Internet bị hỏng”, phát ngôn viên của hãng cung cấp máy chủ Internet Cloudflare cho biết.

Theo tổ chức giám sát an ninh mạng và quản trị Internet NetBlocks, kết nối mạng ở Bờ Biển Ngà đã giảm xuống chỉ còn khoảng 4% vào sáng ngày 14/3. Có thời điểm, dung lượng ở Liberia giảm xuống còn 17%, trong khi Benin ở mức 14% và Ghana là 25%.

Cụ thể, hệ thống cáp Tây Phi (WACS), MainOne, Nam Đại Tây Dương 3 và ACE là những tuyến cáp Internet huyết mạch ở khu vực này, nhưng đều đã bị hư hại từ ngày 14-15/3.', N'submarine_cables_cut_red_sea_hou.jpg', 1, CAST(N'2024-03-19T14:57:54.463' AS DateTime), N'Admin', N'Một sự cố diện rộng gây ra mất kết nối Internet', 10, 0, 1, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (152, N'Phản ứng ở quốc gia cấm cửa TikTok chỉ sau một đêm', N'Tháng 6/2020, chính quyền Ấn Độ bất ngờ cấm TikTok và một số ứng dụng nổi tiếng khác của Trung Quốc mà không báo trước.

Nikhil Pahwa, người sáng lập trang web công nghệ MediaNama có trụ sở tại Delhi, cho biết: "Điều quan trọng cần nhớ là khi Ấn Độ cấm TikTok và nhiều ứng dụng của Trung Quốc, Mỹ là nước đầu tiên ca ngợi quyết định này. Ngoại trưởng Mỹ Mike Pompeo đã hoan nghênh lệnh cấm, nói rằng nó ''sẽ thúc đẩy chủ quyền của Ấn Độ''".

Lệnh cấm đột ngột đã gây sốc cho 200 triệu người dùng TikTok ở Ấn Độ. Sau 4 năm kể từ đó, nhiều người đã tìm ra giải pháp thay thế.

"Lệnh cấm TikTok đã tạo ra cơ hội trị giá hàng tỷ USD, bởi 200 triệu người dùng cần một nơi khác để đi", Pahwa nói với CNN, cho biết thêm rằng cuối cùng các công ty công nghệ Mỹ đã nắm bắt được thời cơ và tạo ra các dịch vụ mới.', N'z5252174180240_6f499d4429d5480f0324f1165ed2de59.jpg', 1, CAST(N'2024-03-19T15:00:52.273' AS DateTime), N'Admin', N'Lệnh cấm đột ngột vào năm 2020 đã gây sốc cho 200 triệu người dùng TikTok ở Ấn Độ', 11, 1, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (153, N'Giá vàng bật tăng trở lại', N'Giá vàng thế giới giao dịch sáng 19/3 (giờ Việt Nam) đã bật tăng nhẹ 2 USD, hiện giao dịch tại 2.159 USD/ounce.

Giá vàng thế giới biến động trong bối cảnh nhà đầu tư đang đón chờ một loạt cuộc họp của các ngân hàng trung ương, bao gồm cả cuộc họp chính sách tiền tệ của Cục Dự trữ Liên bang Mỹ (Fed) vào ngày 20/3 để tìm ra manh mối về lạm phát và lãi suất.

Trong trường hợp Fed giữ lãi suất ở mức cao để chống lại lạm phát thì khả năng vàng sẽ mất đà hỗ trợ tăng giá.

Hưởng lợi từ đà tăng của giá vàng thế giới sáng nay, giá vàng trong nước cũng bật tăng. Trong đó, giá vàng miếng SJC đã hồi phục về sát mốc 82 triệu đồng/lượng; giá vàng nhẫn 9999 tăng cao nhất tới 250.000 đồng để neo tại vùng 69 triệu đồng/lượng.

Cụ thể, Công ty Vàng bạc Đá quý Sài Gòn (SJC) hiện niêm yết giá vàng miếng ở mức 79,9 - 81,9 triệu đồng/lượng (mua vào - bán ra), tăng 500.000 đồng cả hai chiều so với chốt phiên ngày hôm qua.', N'DSC_0555_vang_mieng_duc_anh.jpg', 1, CAST(N'2024-03-19T15:22:02.173' AS DateTime), N'Admin', N'Sau hai phiên giao dịch sụt giảm, giá vàng trong nước đồng loạt tăng mạnh tới nửa triệu đồng', 12, 1, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (154, N'Thị trường trái phiếu doanh nghiệp khởi đầu ảm đạm', N'Thông tin từ Bộ Tài chính cho thấy, tính đến ngày 23/2/2024 có 11 doanh nghiệp phát hành trái phiếu với khối lượng 7,25 nghìn tỷ đồng, gấp 8 lần cùng kỳ năm 2023; trong đó có 52,4% trái phiếu phát hành có tài sản đảm bảo. Tuy nhiên, khi so sánh với lượng trái phiếu phát hành sôi động nửa cuối năm vừa qua, có thể thấy rõ sự ảm đạm của thị trường trái phiếu doanh nghiệp hai tháng đầu năm 2024.Theo các chuyên gia, thị trường có xu hướng tăng trưởng chậm từ đầu năm trong bối cảnh một số quy định siết chặt kỷ luật, đòi hỏi yêu cầu cao hơn với các thành viên tham gia thị trường khi nhiều điều khoản tại Nghị định số 08/2023/NĐ-CP ngày 5/3/2023 sửa đổi, bổ sung và ngưng hiệu lực thi hành một số điều tại các nghị định quy định về chào bán, giao dịch trái phiếu doanh nghiệp riêng lẻ tại thị trường trong nước và chào bán trái phiếu doanh nghiệp ra thị trường quốc tế (Nghị định 08), hết hiệu lực và thực thi trở lại như Nghị định 65/2022/NĐ-CP ngày 16/9/2022, đó là tiêu chí xác định nhà đầu tư cá nhân chuyên nghiệp và yêu cầu xếp hạng tín nhiệm.', N'anh-t31-1.png', 1, CAST(N'2024-03-19T15:23:39.423' AS DateTime), N'Admin', N'Khởi đầu không mấy sôi động, thị trường trái phiếu doanh nghiệp hai tháng đầu năm', 12, 0, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (155, N'Đề xuất ngân hàng mua trái phiếu không được dùng tiền mặt và có thể mua lại ngay sau khi bán', N'Tại Công điện số 990/CĐ-TTg ngày 21/10/2023, theo chức năng, thẩm quyền khẩn trương rà soát, đánh giá tình hình thực hiện các Thông tư số 02/2023/TT-NHNN, Thông tư số 03/2023/TT-NHNN, Thông tư số 06/2023/TT-NHNN và các thông tư, văn bản quy định có liên quan để chủ động, kịp thời xem xét, sửa đổi, bổ sung, ban hành mới các văn bản quy định, nhất là các cơ chế, chính sách hết hiệu lực trong năm 2023.

Từ đó, tháo gỡ kịp thời các vướng mắc phát sinh trong thực tế về tiếp cận vốn của người dân, doanh nghiệp, mua, bán trái phiếu doanh nghiệp của các tổ chức tín dụng, bảo đảm đồng bộ, hiệu quả, phù hợp với tình hình thực tế, ổn định thị trường tiền tệ, an toàn hệ thống các tổ chức tín dụng theo đúng quyết nghị của Chính phủ, chỉ đạo của lãnh đạo Chính phủ và quy định của pháp luật.

"Do đó, việc sửa đổi, bổ sung Thông tư số 16/2021/TT-NHNN là cần thiết và cần được sớm ban hành nhằm góp phần tháo gỡ khó khăn trong bối cảnh tình hình thị trường trái phiếu doanh nghiệp hiện nay", Ngân hàng Nhà nước nhận định.', N'bank.png', 1, CAST(N'2024-03-19T15:24:56.917' AS DateTime), N'Admin', N'Ngân hàng Nhà nước đề xuất bỏ quy định cấm các ngân hàng mua lại trái phiếu chưa niêm yết sau ', 12, 0, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (156, N'Doanh nghiệp chật vật với bất ổn của lãi suất và tỷ giá', N'Trong đề án tái cơ cấu, Vietnam Airlines đề xuất giải pháp tăng vốn điều lệ nên lãnh đạo tổng công ty mong muốn Chính phủ, Ngân hàng Nhà nước chỉ đạo các định chế tài chính hỗ trợ hãng bay.

Ngoài ra, tỷ giá biến động cũng ảnh hưởng lớn đến hãng hàng không quốc gia. Lãnh đạo Vietnam Airlines cho biết khi tỷ giá tăng 1%, hãng sẽ mất tới 300 tỷ đồng, nếu biến động 5% thì chi phí phải gánh chịu 1.500 tỷ đồng/năm, do đó, ông Hòa mong muốn chính sách tỷ giá cần ổn định.

Chia sẻ tại hội nghị, ông Lê Mạnh Hùng, Chủ tịch Hội đồng thành viên Tập đoàn Dầu khí Việt Nam (PVN), cũng mong muốn sự hỗ trợ từ các tổ chức tín dụng, đặc biệt là 4 ngân hàng thương mại nhà nước lớn có thể xem xét áp dụng nâng trần hạn mức cho vay từng trường hợp với các tập đoàn lớn, các dự án lớn.', N'anh-t34-35.png', 1, CAST(N'2024-03-19T15:25:56.787' AS DateTime), N'Admin', N'Theo số liệu từ Ngân hàng Nhà nước, tín dụng toàn nền kinh tế tăng trưởng âm hai tháng liên tiếp.', 12, 1, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (157, N'Đại lộ Thăng Long tắc như không có lối thoát: Bố muộn làm, con nghỉ học', N'8h ngày 19/3, đứng giữa đường dưới trời mưa, chị Đào Thu Hà (37 tuổi, ở chung cư Gemek 1, An Khánh, Hoài Đức, Hà Nội) cố lấy chiếc điện thoại trong túi soạn tin nhắn xin phép trưởng phòng cho đến muộn.

Chị Hà cho biết, hai ngày nay, khi di chuyển trên Đại lộ Thăng Long đoạn giao cắt với đường Lê Quang Đạo (quận Nam Từ Liêm, Hà Nội), chị gặp phải cảnh tắc cứng gần như không lối thoát.

"Sáng thứ hai (18/3), tôi phải mất cả tiếng đồng hồ mới đi qua được nút giao này, sáng nay tình hình cũng không khá hơn, thậm chí còn chật vật do mưa lớn. Đoạn đường gần như tê liệt vì ô tô, xe máy chen chúc nhau di chuyển. Ngày thứ hai trong tuần tôi xin đến muộn vì không muốn bị phạt tiền", chị Hà kể.', N'tac3.jpg', 1, CAST(N'2024-03-19T15:28:04.193' AS DateTime), N'Admin', N'Nhiều người tham gia giao thông mệt mỏi trước tình trạng tắc cứng vào giờ cao điểm tại đoạn Đại Lộ T', 11, 0, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (158, N'Nợ thẻ tín dụng Eximbank 8,5 triệu, phải trả 8,8 tỷ: Hai bên bắt đầu làm việc', N'Theo thông tin từ Ngân hàng Eximbank, ông H.A. thực hiện mở thẻ Master Card tại Eximbank chi nhánh Quảng Ninh vào ngày 23/3/2013 với hạn mức 10 triệu đồng.

Khách hàng sau đó phát sinh 2 giao dịch thanh toán vào các ngày 23/4/2013 và 26/7/2013 tại một điểm chấp nhận giao dịch. Từ ngày 14/9/2013, khoản nợ thẻ nêu trên đã chuyển thành nợ xấu, thời gian quá hạn phát sinh đến thời điểm thông báo là gần 11 năm. Ngay sau đó, ngân hàng đã thực hiện nhiều bước để thu hồi khoản nợ của ông H.A.

“Đây là khoản nợ quá hạn đã kéo dài gần 11 năm. Ngân hàng nhiều lần thông báo và làm việc trực tiếp với khách hàng. Tuy nhiên, khách vẫn chưa có phương án xử lý nợ”, Eximbank thông tin.

Ngân hàng này khẳng định thêm việc phát thông báo nghĩa vụ nợ cho khách hàng là hoạt động nghiệp vụ thông thường trong quá trình xử lý, thu hồi nợ. Tính đến nay, ngân hàng chưa nhận được bất kỳ khoản thanh toán nào từ khách hàng.

Cũng theo Eximbank, phương thức tính lãi, phí trong khoản nợ nói trên "hoàn toàn phù hợp với thỏa thuận giữa Eximbank và khách hàng theo hồ sơ mở thẻ ngày 15/3/2013 có đầy đủ chữ ký khách hàng. Quy định về phí, lãi được quy định rõ trong biểu phí phát hành, sử dụng thẻ; cũng đã được đăng tải công khai trên website của Eximbank".', N'tin-dung1.jpg', 1, CAST(N'2024-03-19T15:29:21.980' AS DateTime), N'Admin', N'Dự kiến sáng nay 19/3, người đại diện của ông H.A. (Quảng Ninh) làm việc với Ngân hàng Eximbank về v', 11, 0, 0, NULL, N'news')
INSERT [dbo].[Posts] ([PostID], [Title], [Contents], [Thumb], [Published], [CreateTime], [Author], [SContent], [CatID], [isHot], [isNewFeed], [AccountID], [Tags]) VALUES (159, N'aaaa', N'aaaa', N'undefined', 0, CAST(N'2024-03-20T00:34:32.323' AS DateTime), NULL, N'aaa', 2, 0, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Posts] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([PostID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Posts]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Account] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Account]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Categories] FOREIGN KEY([CatID])
REFERENCES [dbo].[Categories] ([CatID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Categories]
GO
USE [master]
GO
ALTER DATABASE [MyBlogPRN211] SET  READ_WRITE 
GO
