CREATE TABLE [dbo].[Reports] (
    [Reports_Id]     NVARCHAR (128) NOT NULL,
    [Title]          NVARCHAR (MAX) NULL,
    [Content]        NVARCHAR (MAX) NULL,
    [ReportUser]     NVARCHAR (50)  NULL,
    [Transaction_Id] NVARCHAR (128) NULL,
    [CreateDate]     DATETIME       NULL,
    [CreateBy]       NVARCHAR (50)  NULL,
    [Status]         NVARCHAR (30)  NULL,
    [User_Id]        NVARCHAR (128) NULL,
    CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED ([Reports_Id] ASC),
    CONSTRAINT [FK_Report_Users] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);

