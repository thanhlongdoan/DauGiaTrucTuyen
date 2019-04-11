CREATE TABLE [dbo].[StatusUsers] (
    [StatusUsers_Id]     NVARCHAR (128) NOT NULL,
    [BlockAuctionStatus] BIT            NULL,
    [BlockAuctionTimes]  DATETIME       NULL,
    [BlockAuctionDate]   DATETIME       NULL,
    [BlockUserStatus]    BIT            NULL,
    [BlockUserTime]      DATETIME       NULL,
    [User_Id]            NVARCHAR (128) NULL,
    CONSTRAINT [PK_StatusUser] PRIMARY KEY CLUSTERED ([StatusUsers_Id] ASC),
    CONSTRAINT [FK_StatusUser_Users] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);

