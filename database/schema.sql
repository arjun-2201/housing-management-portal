-- Optional SQL Server schema setup. For EF Core, prefer dotnet ef migrations.
IF DB_ID('HousingPortfolio') IS NULL CREATE DATABASE HousingPortfolio;
GO
USE HousingPortfolio;
GO
IF OBJECT_ID('dbo.Applications','U') IS NULL
BEGIN
 CREATE TABLE dbo.Applications (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,Applicant NVARCHAR(100) NOT NULL,Property NVARCHAR(120) NOT NULL,Program NVARCHAR(60) NOT NULL,Status NVARCHAR(30) NOT NULL DEFAULT 'Pending',Units INT NOT NULL CHECK(Units BETWEEN 1 AND 1000),SubmittedAt DATE NOT NULL DEFAULT CONVERT(date,GETUTCDATE()));
 CREATE INDEX IX_Applications_Status ON dbo.Applications(Status);
 CREATE INDEX IX_Applications_SubmittedAt ON dbo.Applications(SubmittedAt);
END;
GO
