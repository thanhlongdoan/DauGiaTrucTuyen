CREATE TABLE [dbo].[Categorys] (
    [Categorys_Id]   NVARCHAR (128) NOT NULL,
    [CategoryName]   NVARCHAR (50)  NULL,
    [StatusCategory] NVARCHAR (10)  NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Categorys_Id] ASC)
);

