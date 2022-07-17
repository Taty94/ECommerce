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

CREATE TABLE [Brand] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Brand] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Catalogue] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Catalogue] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Client] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220627164237_Inicial', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Brand];
GO

DROP TABLE [Client];
GO

CREATE TABLE [Product] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220627190110_AddProductEntity', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ProductBrand] (
    [Id] nvarchar(4) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductBrand] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProductType] (
    [Id] nvarchar(4) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220627195709_AddProductBrandAndType', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductType]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProductType] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ProductType] DROP COLUMN [Name];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductBrand]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ProductBrand] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ProductBrand] DROP COLUMN [Name];
GO

ALTER TABLE [ProductType] ADD [Description] nvarchar(256) NOT NULL DEFAULT N'';
GO

ALTER TABLE [ProductBrand] ADD [Description] nvarchar(256) NOT NULL DEFAULT N'';
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'Name');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Product] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220628141801_AgregarConfiguracion', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Product] ADD [Description] nvarchar(256) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Product] ADD [Price] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Product] ADD [ProductBrandId] nvarchar(4) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Product] ADD [ProductTypeId] nvarchar(4) NOT NULL DEFAULT N'';
GO

CREATE INDEX [IX_Product_ProductBrandId] ON [Product] ([ProductBrandId]);
GO

CREATE INDEX [IX_Product_ProductTypeId] ON [Product] ([ProductTypeId]);
GO

ALTER TABLE [Product] ADD CONSTRAINT [FK_Product_ProductBrand_ProductBrandId] FOREIGN KEY ([ProductBrandId]) REFERENCES [ProductBrand] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [Product] ADD CONSTRAINT [FK_Product_ProductType_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220628154214_AddProductConfiguration', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Product] DROP CONSTRAINT [FK_Product_ProductBrand_ProductBrandId];
GO

ALTER TABLE [Product] DROP CONSTRAINT [FK_Product_ProductType_ProductTypeId];
GO

ALTER TABLE [ProductType] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ProductType] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ProductType] ADD [ModificationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ProductBrand] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ProductBrand] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ProductBrand] ADD [ModificationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Product] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Product] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Product] ADD [ModificationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Product] ADD CONSTRAINT [FK_Product_ProductBrand_ProductBrandId] FOREIGN KEY ([ProductBrandId]) REFERENCES [ProductBrand] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Product] ADD CONSTRAINT [FK_Product_ProductType_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220628171502_AddBaseEntities', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductType]') AND [c].[name] = N'ModificationDate');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ProductType] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [ProductType] DROP COLUMN [ModificationDate];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductBrand]') AND [c].[name] = N'ModificationDate');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ProductBrand] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [ProductBrand] DROP COLUMN [ModificationDate];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'ModificationDate');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Product] DROP COLUMN [ModificationDate];
GO

ALTER TABLE [ProductType] ADD [ModifiedDate] datetime2 NULL;
GO

ALTER TABLE [ProductBrand] ADD [ModifiedDate] datetime2 NULL;
GO

ALTER TABLE [Product] ADD [ModifiedDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220628172538_AdjustNullableModfiedDate', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220717023153_DuplicateBddToLocal', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220717220031_AddOrderEntities-AddTheirConfiguration', N'6.0.6');
GO

COMMIT;
GO

