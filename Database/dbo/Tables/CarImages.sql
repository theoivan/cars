﻿CREATE TABLE [dbo].[CarImages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[CarId] INT NOT NULL,
	[ImagePath] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [FK_CarImages_Cars] FOREIGN KEY ([CarId]) REFERENCES [Cars]([CarId])	
)
