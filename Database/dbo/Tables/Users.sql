CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Username] NVARCHAR(256) NOT NULL UNIQUE, 
    [NormalizedUsername] NVARCHAR(256) NOT NULL, 
    [Email] NVARCHAR(256) NULL UNIQUE, 
    [NormalizedEmail] NVARCHAR(256) NULL, 
    [PasswordHash] VARBINARY(MAX) NOT NULL,
    [Salt] VARBINARY(MAX) NOT NULL,
)

