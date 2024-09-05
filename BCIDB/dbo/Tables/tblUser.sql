CREATE TABLE [dbo].[tblUser] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [FirstName]   VARCHAR (150)    NULL,
    [LastName]    VARCHAR (150)    NOT NULL,
    [Email]       VARCHAR (255)    NULL,
    [Password]    VARCHAR (MAX)    NOT NULL,
    [__CREATE_TS] DATETIME         DEFAULT (getutcdate()) NOT NULL,
    [__UPDATE_TS] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_tblUser_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);

