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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE TABLE [Stores] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Address] nvarchar(250) NOT NULL,
        CONSTRAINT [PK_Stores] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Price] float NOT NULL,
        [StoreId] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE TABLE [Images] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [PhotoExtension] nvarchar(max) NULL,
        [Bytes] varbinary(max) NULL,
        [ProductId] int NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Images_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE TABLE [Offers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Description] nvarchar(500) NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [ImageId] int NOT NULL,
        CONSTRAINT [PK_Offers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Offers_Images_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Images] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE TABLE [ProductOffers] (
        [Id] int NOT NULL IDENTITY,
        [StartDate] datetime2 NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [StoreId] int NULL,
        [ProductId] int NULL,
        [OfferId] int NOT NULL,
        CONSTRAINT [PK_ProductOffers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductOffers_Offers_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductOffers_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ProductOffers_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name') AND [object_id] = OBJECT_ID(N'[Stores]'))
        SET IDENTITY_INSERT [Stores] ON;
    EXEC(N'INSERT INTO [Stores] ([Id], [Address], [Name])
    VALUES (1, N''Cairo'', N''Cairo store''),
    (2, N''Alex'', N''Alex store''),
    (3, N''Tanta'', N''Tanta store''),
    (4, N''Giza'', N''Giza store'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name') AND [object_id] = OBJECT_ID(N'[Stores]'))
        SET IDENTITY_INSERT [Stores] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Price', N'StoreId') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] ON;
    EXEC(N'INSERT INTO [Products] ([Id], [Name], [Price], [StoreId])
    VALUES (1, N'' Iphone 13 Mobile'', 10000.0E0, 1),
    (2, N'' Samsung s 10 Mobile'', 80000.0E0, 1),
    (3, N''mi 11 Mobile'', 4000.0E0, 2),
    (4, N'' LG Smart'', 20000.0E0, 2),
    (5, N'' Maths Books'', 1000.0E0, 3),
    (6, N'' Coffe Machine'', 9000.0E0, 3),
    (7, N'' Ipad'', 2000.0E0, 4),
    (8, N''Swimming tools'', 300.0E0, 4)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Price', N'StoreId') AND [object_id] = OBJECT_ID(N'[Products]'))
        SET IDENTITY_INSERT [Products] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE INDEX [IX_Images_ProductId] ON [Images] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE UNIQUE INDEX [IX_Offers_ImageId] ON [Offers] ([ImageId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE INDEX [IX_ProductOffers_OfferId] ON [ProductOffers] ([OfferId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE INDEX [IX_ProductOffers_ProductId] ON [ProductOffers] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE INDEX [IX_ProductOffers_StoreId] ON [ProductOffers] ([StoreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    CREATE INDEX [IX_Products_StoreId] ON [Products] ([StoreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220528181848_InitialDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220528181848_InitialDB', N'5.0.17');
END;
GO

COMMIT;
GO

