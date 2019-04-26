CREATE TABLE [dbo].[Users_Chat] (
    [UserChat_Id]  NVARCHAR (128) NOT NULL,
    [ConnectionId] NVARCHAR (250) NULL,
    [User_Id]      NVARCHAR (128) NULL,
    [IsOnline]     BIT            NULL,
    [DateOnline]   DATETIME       NULL,
    CONSTRAINT [PK_UserChat] PRIMARY KEY CLUSTERED ([UserChat_Id] ASC)
);

