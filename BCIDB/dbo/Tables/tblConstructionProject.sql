CREATE TABLE [dbo].[tblConstructionProject] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (200)    NOT NULL,
    [Location]    VARCHAR (500)    NOT NULL,
    [Stage]       SMALLINT         NOT NULL,
    [Category]    VARCHAR (100)    NOT NULL,
    [StartDate]   DATETIME         NOT NULL,
    [Description] VARCHAR (MAX)    NOT NULL,
    [CreatorId]   UNIQUEIDENTIFIER NULL,
    [__CREATE_TS] DATETIME         DEFAULT (getutcdate()) NOT NULL,
    [__UPDATE_TS] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblConstructionProject_tblUser] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[tblUser] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblConstructionProject_Stage]
    ON [dbo].[tblConstructionProject]([Stage] ASC);

