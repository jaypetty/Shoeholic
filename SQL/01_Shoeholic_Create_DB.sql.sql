USE [master]

IF db_id('Shoeholic') IS NULl
  CREATE DATABASE [Shoeholic]
GO

USE [Shoeholic]
GO

DROP TABLE IF EXISTS [ShoeTag];
DROP TABLE IF EXISTS [Tag];
DROP TABLE IF EXISTS [Shoe];
DROP TABLE IF EXISTS [Collection];
DROP TABLE IF EXISTS [Brand];
DROP TABLE IF EXISTS [UserProfile];
GO

CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [FirebaseUserProfileId] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Brand] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Shoe] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [BrandId] int NOT NULL,
  [ReleaseDate] datetime NOT NULL,
  [RetailPrice] int,
  [PurchaseDate] datetime,
  [Title] nvarchar(255) NOT NULL,
  [ColorWay] nvarchar(255) NOT NULL,
  [CollectionId] int,

  CONSTRAINT [FK_Shoe_Brand] FOREIGN KEY ([BrandId]) REFERENCES [Brand] ([Id])
)
GO

CREATE TABLE [Collection] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [UserProfileId] int NOT NULL,

 CONSTRAINT [FK_Collection_UserProfile]  FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
)
GO


CREATE TABLE [Tag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [ShoeTag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ShoeId] int NOT NULL,
  [TagId] int NOT NULL,

CONSTRAINT [Fk_ShoeTag_Shoe]  FOREIGN KEY ([ShoeId]) REFERENCES [Shoe] ([Id]) ON DELETE CASCADE,
CONSTRAINT [Fk_ShoeTag_Tag]  FOREIGN KEY ([TagId]) REFERENCES [Tag] ([Id])
)
GO

ALTER TABLE [Shoe] ADD FOREIGN KEY ([CollectionId]) REFERENCES [Collection] ([Id]) ON DELETE SET NULL
GO

