CREATE TABLE [dbo].[Products] (
    [Products_Id]   NVARCHAR (128) NOT NULL,
    [CreateDate]    DATETIME       NULL,
    [CreateBy]      NVARCHAR (50)  NULL,
    [UpdateDate]    DATETIME       NULL,
    [UpdateBy]      NVARCHAR (50)  NULL,
    [StatusProduct] NVARCHAR (30)  NULL,
    [Category_Id]   NVARCHAR (128) NULL,
    [User_Id]       NVARCHAR (128) NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Products_Id] ASC),
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([Category_Id]) REFERENCES [dbo].[Categorys] ([Categorys_Id]),
    CONSTRAINT [FK_Products_Users] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);

