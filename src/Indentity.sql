CREATE TABLE aspnetusers (
	Id bigint NOT NULL IDENTITY(1, 1),
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	UserName varchar(100),
	NormalizedUserName varchar(100),
	Email varchar(100) not null,
	NormalizedEmail varchar(100),
	EmailConfirmed bit not null,
	PasswordHash text not null,
	SecurityStamp text not null,
	ConcurrencyStamp text,
	PhoneNumber varchar(100),
	PhoneNumberConfirmed bit not null,
	TwoFactorEnabled bit not null,
	LockoutEnd datetime,
	LockoutEnabled bit not null,
	AdminFlag tinyint not null,
	AccessFailedCount bit not null,
	LastLoginDate datetime,
	LastLogoutDate datetime,
	PRIMARY KEY(Id)
)
Go
CREATE TABLE aspnetroles (
	Id bigint NOT NULL IDENTITY(1, 1),
	Name nvarchar(256) not null,
	NormalizedName nvarchar(MAX) not null,
	ConcurrencyStamp nvarchar(MAX),
)
go
CREATE TABLE aspnetroleclaims (
	Id bigint NOT NULL IDENTITY(1, 1),
	RoleId nvarchar(450) not null,
	ClaimType nvarchar(MAX),
	ClaimValue nvarchar(MAX),
)
go
CREATE TABLE aspnetuserclaims (
	Id bigint NOT NULL IDENTITY(1, 1),
	UserId bigint not null,
	ClaimType nvarchar(MAX),
	ClaimValue nvarchar(MAX),
)
go
CREATE TABLE aspnetuserlogins (
	LoginProvider nvarchar(450) not null,
	ProviderKey nvarchar(450) not null,
	ProviderDisplayName nvarchar(MAX),
	UserId bigint
)
go
CREATE TABLE aspnetuserroles (
	UserId bigint not null,
	RoleId int not null
)
go
CREATE TABLE aspnetusertokens (
	UserId bigint not null,
	LoginProvider nvarchar(450) not null,
	Name nvarchar(450) not null,
	Value nvarchar(MAX)
) 