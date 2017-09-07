CREATE TABLE [dbo].[blood] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [bloodgroup] VARCHAR (50) NULL,
    [date]       DATETIME     NOT NULL,
    [bid]        VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

