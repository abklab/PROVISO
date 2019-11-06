CREATE TABLE [dbo].CustomerAccount
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ReferenceNumber] VARCHAR(50) NOT NULL, 
    [AccountNumber] VARCHAR(50) NOT NULL, 
    [AccountName] VARCHAR(150) NOT NULL, 
    [Banker] VARCHAR(50) NOT NULL, 
    [MomoNumber] VARCHAR(50) NULL, 
    [MomoProvider] VARCHAR(50) NULL, 
    [TelephoneNumber] VARCHAR(50) NOT NULL, 
    [LastAccessed] DATETIME2 NULL, 
    [AccountBalance] DECIMAL NOT NULL
)
