# About
This is a coding assignment to display my code habits and knowledge of .Net. 
It's a simple Car Park API that allows users to park cars, check parking availability, and calculate parking charges on 
vehicle exit.

# Setup
## Requirements
- .Net 9.0 SDK
- MS Sql Server

## Setting up the database
A single sql script has been added to the root of the solution called CreateAndSeedDatabase.sql, run this one script
in SSMS or in the console using:
```
sqlcmd -S server_name -i CreateAndSeedDatabase.sql
```
additionally the connection string to the associated database needs to be added to appsettings.json

## Building and running the program 
To build and run the program either open up the solution file in your preferred IDE and hit debug. Alternatively 
open up the solution folder and navigate to the sub folder CarPark.API and run the following command:
```
dotnet run
```
If you receive and error please make sure you have the 
[.Net 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) installed.

# Additional Notes
## Assumptions Made
I made the following assumptions when developing:
- UTC time was used to timestamp entry and exit datetime for consistency purposes.
- Parking costs would potentially change so a IOptions patters was used with the appsettings.json to allow
for configurable pricing costs. 
- The database would not change frequently, so a basic SQL script was used to create, scaffold, and seed the
database instead of using EF migration scripts.
- VehicleReg values were unique and if a duplicate value was used the parking would fail with an error message.

## Questions
There were some requirements that were missing form the initial instructions, bellow are the
questions I would have for the client about this project and the considerations associated with
them. 

- Is the cost supposed to be rounded down to the minute or are seconds counted?
My implementation does not round, and for example will calculate 5 cents for a 30 second parking stay. 
- Is the amount of base parking spaces likely to change?
If so additional endpoints could be added to configure the amount of parking spaces available.
- Is the data from parking needed in the future?
If so a different pattern could be used, and instead of removing parking entries after exit entried could
simply be marked as "deleted" or "removed" so a history of who was parked there could be queried from the 
database.
- Is any of the code likely to be used elsewhere?
If so it would be pertinent to extract any of the domain object and code, and put them in their own project to 
either be referenced by other projects or be compiled into a nuget package.
