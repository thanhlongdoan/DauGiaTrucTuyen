CREATE TABLE [dbo].[Transactions] (
    [Transaction_Id]      NVARCHAR (128) NOT NULL,
    [TimeLine]            TIME (7)       NULL,
    [AuctionDateApproved] DATETIME       NULL,
    [AuctionDateStart]    DATETIME       NULL,
    [PriceStart]          DECIMAL (18)   NULL,
    [StepPrice]           INT            NULL,
    [Product_Id]          NVARCHAR (128) NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([Transaction_Id] ASC),
    CONSTRAINT [FK_Transaction_Products] FOREIGN KEY ([Product_Id]) REFERENCES [dbo].[Products] ([Products_Id])
);



