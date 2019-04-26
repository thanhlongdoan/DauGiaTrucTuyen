CREATE TABLE [dbo].[Message_User_Chat] (
    [MessageChat_Id]   NVARCHAR (128) NOT NULL,
    [FromConnectionId] NVARCHAR (250) NULL,
    [FromUser_Id]      NVARCHAR (128) NULL,
    [ToUser_Id]        NVARCHAR (128) NULL,
    [DateSend]         DATETIME       NULL,
    [IsRead]           BIT            NULL,
    [DateRead]         DATETIME       NULL,
    [Message]          NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_MessageChat] PRIMARY KEY CLUSTERED ([MessageChat_Id] ASC)
);

