CREATE TABLE [dbo].[BlogPost] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (1000) NULL,
    [Content]      NVARCHAR (MAX)  NULL,
    [PostDate]     DATETIME2 (7)   NOT NULL,
    [Status]       INT             NOT NULL,
    [IsDeleted]    BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [CreatedBy]    NVARCHAR (450)  NULL,
    [CreatedDate]  DATETIME2 (7)   NOT NULL,
    [ModifiedBy]   NVARCHAR (450)  NULL,
    [ModifiedDate] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_BlogPost] PRIMARY KEY CLUSTERED ([Id] ASC)
);

