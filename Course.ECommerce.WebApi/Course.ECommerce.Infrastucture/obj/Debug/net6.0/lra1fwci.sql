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

