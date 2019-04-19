CREATE TABLE [dbo].[StatusUsers] (
    [StatusUsers_Id]     NVARCHAR (128) NOT NULL,
    [BlockAuctionStatus] NVARCHAR (10)  NULL,
    [BlockAuctionDate]   DATETIME       NULL,
    [BlockUserStatus]    NVARCHAR (10)  NULL,
    [BlockUserDate]      DATETIME       NULL,
    [User_Id]            NVARCHAR (128) NULL,
    CONSTRAINT [PK_StatusUser] PRIMARY KEY CLUSTERED ([StatusUsers_Id] ASC),
    CONSTRAINT [FK_StatusUser_Users] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);





