CREATE TABLE [dbo].[TransactionAuction] (
    [Transaction_Id] NVARCHAR (128) NOT NULL,
    [User_Id]        NVARCHAR (128) NOT NULL,
    [AuctionDate]    DATETIME       NOT NULL,
    [AuctionPrice]   DECIMAL (18)   NULL,
    [Status]         NVARCHAR (30)  NULL,
    CONSTRAINT [PK_TransactionAuction] PRIMARY KEY CLUSTERED ([Transaction_Id] ASC, [User_Id] ASC, [AuctionDate] ASC),
    CONSTRAINT [FK_TransactionAuction_Transactions] FOREIGN KEY ([Transaction_Id]) REFERENCES [dbo].[Transactions] ([Transaction_Id]),
    CONSTRAINT [FK_TransactionAuction_Users1] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);

