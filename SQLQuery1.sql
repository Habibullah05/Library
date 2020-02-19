CREATE DATABASE Rasing;
--DROP DATABASE Rasing;
GO
USE Rasing;
GO
CREATE TABLE Gamers(
Id int PRIMARY KEY IDENTITY(1,1),
[Name] nvarchar(8) UNIQUE,
[Login] nvarchar(10) UNIQUE,
[Password] nvarchar(10) UNIQUE,
Coins int NOT NULL DEFAULT(0)
);
GO
ALTER TABLE Gamers
ADD CONSTRAINT CHECK_Coins CHECK(Coins>0)
GO
CREATE TABLE Cars(
Id int PRIMARY KEY IDENTITY(1,1),
Color nvarchar(10) NULL ,
Price int NOT NULL,
MotorPrice int NOT NULL,
TransmissionPrice int NOT NULL,
VolumePrice int NOT NULL,
ClutchPrice int NOT NULL

);
GO
ALTER TABLE Cars
ADD CONSTRAINT CHECK_Price CHECK(ClutchPrice>0AND MotorPrice>0 AND TransmissionPrice>0 AND VolumePrice>0 AND Price>0)

GO

CREATE TABLE [Routes](
Id int PRIMARY KEY IDENTITY(1,1),
Title nvarchar(15) NOT NULL UNIQUE,
[Length] int NOT NULL DEFAULT(0),
Price int NOT NULL 
);

GO
ALTER TABLE [Routes]
ADD CONSTRAINT CHECK_RoutesPrice CHECK(Price>0);
GO
ALTER TABLE [Routes]
ADD CONSTRAINT CHECK_RoutesLength CHECK([Length]>0);
GO
CREATE TABLE GamerCars(
IdCar int NOT NULL,
IdGamer int NOT NULL,
MotorLevel tinyint NOT NULL DEFAULT(1),
TransmissionLevel tinyint NOT NULL DEFAULT(1),
VolumeLevel tinyint NOT NULL DEFAULT(1),
ClutchLevel tinyint NOT NULL DEFAULT(1)
CONSTRAINT FK_Car FOREIGN KEY(IdCar) REFERENCES Cars(Id),
CONSTRAINT FK_Gamer FOREIGN KEY (IdGamer) REFERENCES Gamers(Id)

); 
GO
ALTER TABLE GamerCars
ADD CONSTRAINT CHECK_Level CHECK((ClutchLevel>1 AND ClutchLevel<21 )AND(MotorLevel>1 AND MotorLevel<21)
AND(TransmissionLevel>1 AND TransmissionLevel<21)AND(VolumeLevel>1 AND VolumeLevel<21));

GO
CREATE TABLE GamersRouts(
IdRoutes int NOT NULL,
IdGamer int NOT NULL,
MaxLenght int NOT NULL DEFAULT(0),
CONSTRAINT FK_GamerR FOREIGN KEY (IdGamer) REFERENCES Gamers(Id),
CONSTRAINT FK_Routes FOREIGN KEY (IdRoutes) REFERENCES [Routes](Id)
);
GO
ALTER TABLE GamersRouts
ADD CONSTRAINT CHECK_MaxLenght CHECK(MaxLenght>0);