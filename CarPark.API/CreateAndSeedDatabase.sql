-- Database and Table Creation
IF DB_ID('CarPark') IS NULL
BEGIN
    CREATE DATABASE CarPark;
END
GO

USE CarPark
GO

BEGIN TRANSACTION
GO

IF OBJECT_ID('ParkedCar') IS NULL
BEGIN
    CREATE TABLE [ParkedCar]
    (
        ParkingId INT NOT NULL IDENTITY(1,1),
        VehicleReg NVARCHAR(MAX) NOT NULL, 
        ParkingDate DATETIME NOT NULL,
        CONSTRAINT ParkingId PRIMARY KEY (ParkingId)
    )
END

IF OBJECT_ID('ParkingSpace') IS NULL
BEGIN
    CREATE TABLE [ParkingSpace]
    (
        ParkingSpaceId INT NOT NULL,
        ParkingId INT NULL
        CONSTRAINT ParkingSpaceId PRIMARY KEY (ParkingSpaceId),
        CONSTRAINT [FK_ParkingId] FOREIGN KEY(ParkingId)
                REFERENCES [ParkedCar](ParkingId)
                ON DELETE SET NULL
    )
END

COMMIT TRANSACTION
GO

-- Database Seeding
BEGIN TRANSACTION
GO

INSERT INTO [CarPark].[dbo].[ParkingSpace]
(ParkingSpaceId)
VALUES
(1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10)

COMMIT TRANSACTION
GO

