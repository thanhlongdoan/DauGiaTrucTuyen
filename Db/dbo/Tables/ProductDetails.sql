CREATE TABLE [dbo].[ProductDetails] (
    [ProductDetails_Id] NVARCHAR (128) NOT NULL,
    [ProductName]       NVARCHAR (50)  NULL,
    [Image]             NVARCHAR (MAX) NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [Product_Id]        NVARCHAR (128) NULL,
    CONSTRAINT [PK_ProductDetails] PRIMARY KEY CLUSTERED ([ProductDetails_Id] ASC),
    CONSTRAINT [FK_ProductDetails_Products] FOREIGN KEY ([Product_Id]) REFERENCES [dbo].[Products] ([Products_Id])
);

