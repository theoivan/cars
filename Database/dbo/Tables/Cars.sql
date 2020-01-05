CREATE TABLE [dbo].[Cars]
(
	[CarId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Brand] NVARCHAR(256) NOT NULL, 
    [Model] NVARCHAR(256) NOT NULL, 
    [RegistrationNumber] NVARCHAR(256) NOT NULL UNIQUE, 
    [UserId] INT FOREIGN KEY REFERENCES Users(UserId) NOT NULL, 
    [Combustible] NVARCHAR(10) NULL, 
    [FirstRegistrationDate] DATETIME NULL, 
    [EngineSize] INT NULL, 
    [Transmission] NVARCHAR(20) NULL, 
    [OriginCountry] NVARCHAR(50) NULL, 
    [NumberOfDoors] INT NULL, 
    [NumberOfSeats] INT NULL, 
    [EmissionStandard] NVARCHAR(15) NULL, 
    [Colour] NVARCHAR(50) NULL, 
    [BodyType] NVARCHAR(50) NULL, 
    [Power] INT NULL, 
    [ImagePath] NVARCHAR(MAX) NULL
)

