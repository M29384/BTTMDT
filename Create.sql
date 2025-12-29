create database BookStoreDB
go
use BookStoreDB
go
create table category
(
	categoryID int primary key ,
	categoryName nvarchar(20),

)
create Table product
(
	productID int primary key,
	productName nvarchar(100),
	tenTG nvarchar(50),
	nhaXuatBan nvarchar(50),
	NXB date,
	Price decimal,	
	Soluong int,
	imageUrl varchar(max),
	Description nvarchar(max),
	categoryID int foreign key references category(categoryID)
)
create table carts
(
	cartID int primary key,
	productID int foreign key references product(productID),
	productName nvarchar(100),
	tenTG nvarchar(50),
	imageUrl varchar(max),
	Soluong int,
	Price decimal,
	Tong decimal,
) 
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FullName] nvarchar(max) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [orderDetails] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [OrderCode] nvarchar(max) NOT NULL,
    [ProductId] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Soluong] int NOT NULL,
    CONSTRAINT [PK_orderDetails] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [OrderCode] nvarchar(max) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
);
GO  

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251215091100_IdentityMigration', N'8.0.22');
GO

COMMIT;
GO



insert dbo.category (categoryID,categoryName)
values
(1,N'Tiểu thuyết'),
(2,N'Lịch sử'),
(3,N'Manga-comic');

insert dbo.product (productID,productName,Price,tenTG,nhaXuatBan,NXB,Soluong,imageUrl,categoryID,Description)
values
(1,N'Nhà giả kim',80000,'Paulo Coelho',N'Hội nhà văn','2020',50,'NGK.jpg',1,N'"Nhà Giả Kim" không đơn thuần là một cuốn tiểu thuyết, mà là bản đồ dẫn lối đến giấc mơ, khao khát và định mệnh của mỗi con người. Câu chuyện về chàng trai chăn cừu Santiago không chỉ mang đến những cuộc phiêu lưu hấp dẫn, mà còn mở ra nhiều tầng triết lý sâu sắc về cuộc sống.'),
(2,N'Hồ điệp và kình ngư',120000,N'Tuế Kiến',N'Văn học','2024',43,'HDKN.jpg',1,N'Một câu chuyện cuốn hút ngay từ những trang đầu tiên - Khi tình yêu trở thành sợi dây mong manh giữa sinh tử, phản bội và hy vọng. Khi một nàng hồ điệp nhỏ bé chạm trán với kình ngư mạnh mẽ, liệu đó là định mệnh hay chỉ là một giấc mộng chóng tàn?'),
(3,N'Người Bà Tài Giỏi Vùng Saga',100000,'Yoshichi Shimada',N'Thanh Niên','2021',45,'NBTGVSg.jpg',1,N'Hạnh phúc không phải là thứ được định đoạt bằng tiền. Hạnh phúc phải được định đoạt bằng tâm thế của mỗi chúng ta.'),
(4,N'Cây Cam Ngọt Của Tôi',90000,'Jose Mauro de Vasconcelos',N'Hội Nhà Văn','2020',34,'CCNCT.jpg',1,N'Với một đứa trẻ, thế giới không giới hạn trong một bữa ăn, mà thế giới cần có hào quang của tình thương. Bạn có bao giờ cảm thấy bị lạc lõng trong chính ngôi nhà của mình? Một câu chuyện chạm đến tận cùng cảm xúc'),
(5,N'Trường Ca Achilles',130000,'Madeline Miller',N'Kim Đồng','2020',36,'TCA.jpg',1,N'Lấy cảm hứng từ sử thi Iliad, Madeline Miller đã tái hiện một câu chuyện tình yêu đầy say đắm nhưng cũng nhuốm màu bi kịch giữa hai người anh hùng Hy Lạp trong tác phẩm đầu tay của mình – Trường Ca Achilles.'),
(6,N'Hồi ký Nguyễn Thị Bình',150000,N'Nguyễn Thị Bình',N'Chính Trị Quốc Gia Sự Thật','2025',45,'HKNTB.jpg',2,N'Đây là cuốn hồi ký của bà Nguyễn Thị Bình, nguyên Phó Chủ tịch nước, nữ Bộ trưởng Ngoại giao đầu tiên, Trưởng đoàn đàm phán của Chính phủ Cách mạng Lâm thời Cộng hòa miền Nam Việt Nam tại Hòa đàm Paris.'),
(7,N'Nhật ký Trong Tù',65000,N'Hồ Chí Minh',N'Văn Học','2022',56,'NKTT.jpg',2,N'Giáo sư Phương Lựu, nhà lý luận phê bình NXB Văn Học đã đưa ra bằng chứng, tuy được viết bằng chữ Hán nhưng "Nhật ký trong tù" của Bác rất khác với thơ Đường. Trước hết, nét riêng ấy có được là do từ ngữ được sử dụng theo chiều hướng phổ thông hóa, đại chúng hóa. Bên cạnh vốn từ vựng cổ được vận dụng, Bác còn đưa vào nhiều từ ngữ bạch thoại - khẩu ngữ.'),
(8,N'Hồi Ức Quảng Trị',95000,N'Nguyễn Thụy Kha',N'Chính Trị Quốc Gia Sự Thật','2025',24,'HUQT.jpg',2,N'“Hồi Ức Quảng Trị” là một ấn phẩm đặc biệt của nhà văn, nhà báo Nguyễn Thụy Kha - đưa bạn đọc trở về với mùa hè đỏ lửa năm 1972, khi Thành Cổ Quảng Trị trở thành biểu tượng của sự kiên cường và hy sinh.'),
(9,N'Những Ngày Sống Gần Bác',35000,N'Nông Thị Trưng, Phùng Lê, Hoàng Hải',N'Kim Đồng','2025',26,'NGSGB.jpg',2,N'Cuốn hồi kí mộc mạc kể về thời gian hoạt động bí mật tại núi rừng Pác Bó, cùng một số đồng chí, bà Nông Thị Trưng, một phụ nữ dân tộc Tày, đã có những tháng ngày được sống gần Bác. Được Bác giáo dục, đào tạo, bà dần trưởng thành, vượt qua những chông gai trong cuộc sống riêng tư và công việc, trở thành nữ cán bộ gương mẫu, nhiệt huyết.'),
(10,N'Đi Tìm Chính Mình',160000,N'Phạm Đức Nhuận',N'Thế Giới','2025',30,'DTCM.jpg',2,N'“Cuộc đời là một cuộc đi – Hễ mà dừng lại là đi cuộc đời” – câu mở đầu như một tuyên ngôn sống đã gợi mở tinh thần xuyên suốt cuốn hồi ký Đi tìm chính mình của PGS.TS. Phạm Đức Nhuận. Không đơn thuần là dòng chảy ký ức, đây là một hành trình chiêm nghiệm đầy chiều sâu về cuộc đời, thời thế, khoa học và nghệ thuật – những mảnh ghép làm nên một con người trọn vẹn.'),
(11,N'Made in abyss - tập 1',50000,'Akihito Tsukushi',N'Hồng đức','2021',30,'MIA-1.jpg',3,N'Abyss - nơi duy nhất trên thế gian chưa được khám phá, ẩn chứa vô vàn sinh vật quái dị và những món đồ quý hiếm mà con người không thể tạo ra. Điều này đã biến nó trở thành địa điểm hấp dẫn, lôi kéo vô vàn nhà thám hiểm vào các cuộc phiêu lưu.'),
(12,N'Made in abyss - tập 2',50000,'Akihito Tsukushi',N'Hồng đức','2021',25,'MIA-2.jpg',3,N'Riko và Reg bắt đầu chuyến phiêu lưu tìm kiếm người mẹ dưới đáy Abyss. Tuy nhiên, con đường phía trước sẽ rất gian nan khi dưới vực thẳm là vô vàn những sinh vật nguyên sinh kì quái và nguy hiểm. Hai đứa trẻ tràn ngập kì vọng mà không hề biết rằng hố sâu ma quái này đang dần nuốt lấy chúng…'),
(13,N'Made in abyss - tập 3',50000,'Akihito Tsukushi',N'Hồng đức','2021',20,'MIA-3.jpg',3,N'Sau khi được Ozen “Bất di bất dịch” chỉ dạy, Riko và Reg tiếp tục hành trình xuống dưới đáy vực thẳm. Những cuộc tấn công liên tiếp của các sinh vật hung dữ khiến chuyến phiêu lưu càng thêm gian nan. Giữa tình thế nguy cấp, một sinh vật bí ẩn xuất hiện…'),
(14,N'Made in abyss - tập 4',50000,'Akihito Tsukushi',N'Hồng đức','2021',23,'MIA-4.jpg',3,N'Sau khi chữa lành vết thương trong cuộc đụng độ với cầu gai, đội thám hiểm của Riko đã có thêm thành viên mới. Ba đứa trẻ tiếp tục chuyến đi đầy gian nan xuống tầng 5, nơi chỉ những Còi trắng mới được phép thám hiểm. Chưa hết, họ còn gặp gỡ Bondrewd, kẻ thù cũ của Nanachi, đồng thời là một kẻ đặc biệt quái gở. Liệu Riko và các bạn sẽ giải quyết chuyện này ra sao? Hãy cùng đón đọc tập 4 của bộ truyện phiêu lưu đầy kịch tính đan xen giữa điều tất yếu và kì tích!'),
(15,N'Made in abyss - tập 5',50000,'Akihito Tsukushi',N'Hồng đức','2021',14,'MIA-5.jpg',3,N'Nhóm Riko và “Chúa tể bình minh” Bondrewd đã có cuộc đụng độ nảy lửa. Tuy Reg đã cố hết sức nhưng vẫn không thể chống lại sức mạnh của một Còi trắng dày dặn kinh nghiệm. Không chỉ thế, cô bé Prushka đang rơi vào tình thế nguy hiểm khi sắp trở thành đối tượng cho thí nghiệm đáng sợ của ông ta…');
