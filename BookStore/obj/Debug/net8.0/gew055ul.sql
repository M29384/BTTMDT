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

CREATE TABLE [category] (
    [categoryID] int NOT NULL,
    [categoryName] nvarchar(20) NULL,
    CONSTRAINT [PK__category__23CAF1F890EE987B] PRIMARY KEY ([categoryID])
);
GO

CREATE TABLE [product] (
    [productID] int NOT NULL,
    [productName] nvarchar(100) NULL,
    [tenTG] nvarchar(20) NULL,
    [nhaXuatBan] nvarchar(10) NULL,
    [Price] decimal(18,0) NULL,
    [Soluong] int NULL,
    [imageUrl] varchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [categoryID] int NULL,
    [NXB] nvarchar(max) NULL,
    CONSTRAINT [PK__product__2D10D14AE12D0CA9] PRIMARY KEY ([productID]),
    CONSTRAINT [FK__product__categor__398D8EEE] FOREIGN KEY ([categoryID]) REFERENCES [category] ([categoryID])
);
GO

CREATE TABLE [carts] (
    [cartID] int NOT NULL,
    [productID] int NULL,
    [ProductName] nvarchar(max) NULL,
    [tenTg] nvarchar(max) NULL,
    [Price] decimal(18,0) NULL,
    [imageUrl] nvarchar(max) NULL,
    [Soluong] int NULL,
    [usersID] int NULL,
    CONSTRAINT [PK__carts__415B03D8BC45F806] PRIMARY KEY ([cartID]),
    CONSTRAINT [FK__carts__productID__3C69FB99] FOREIGN KEY ([productID]) REFERENCES [product] ([productID])
);
GO

CREATE INDEX [IX_carts_productID] ON [carts] ([productID]);
GO

CREATE INDEX [IX_product_categoryID] ON [product] ([categoryID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251215082326_V0', N'8.0.22');
GO

COMMIT;
GO

