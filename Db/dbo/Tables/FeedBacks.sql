CREATE TABLE [dbo].[FeedBacks] (
    [FeedBacks_Id] NVARCHAR (128) NOT NULL,
    [Point]        INT            NULL,
    [Comment]      NVARCHAR (MAX) NULL,
    [User_Id]      NVARCHAR (128) NULL,
    CONSTRAINT [PK_FeedBack] PRIMARY KEY CLUSTERED ([FeedBacks_Id] ASC),
    CONSTRAINT [FK_FeedBack_Users] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);

