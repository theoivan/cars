﻿CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Username] NVARCHAR(256) NOT NULL UNIQUE, 
    [Email] NVARCHAR(256) NULL UNIQUE, 
    [PasswordHash] VARBINARY(MAX) NOT NULL,
    [Salt] VARBINARY(MAX) NOT NULL,
)

