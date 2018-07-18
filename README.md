# AirportRESRfulApiEF TESTS
ASP.NET Core Web API for Airport

How to compile
==============

1. Install software

- Git
  https://git-for-windows.github.io/
  Select a file summarized as "Full installer for official Git for Windows"
   with the highest version
- Visual Studio
  http://www.visualstudio.com/downloads/download-visual-studio-vs
  Select "Visual Studio Community 2017 for Windows Desktop" version 15.7.5 or newer.
- Microsoft .NET Core 2.1

2. Check out

- Create an empty folder anywhere
- In explorer left click and select "Git Clone"
  set URL https://github.com/5avel/AirportRESRfulApi.git
  OK

3. Build

- Open ParkingWebAPI.sln with Visual Studio 2017 for windows desktop.
- Compile.

4. Use

-Open Postman (https://www.getpostman.com/) or Fiddler (https://www.telerik.com/fiddler)

***TESTS Postman*** --->> Download`.json` file [here](https://github.com/5avel/AirportRESRfulApiEFTests/blob/tests/AirportRESRfulApi.BLL.Tests/PSTests/AirPortTwst.postman_collection.json)

![REST](https://github.com/5avel/AirportRESRfulApiEFTests/blob/tests/AirportRESRfulApi.BLL.Tests/PSTests/pmTests.jpg)

## Endpoints API
Type |         Method        | Description                                                          | Sample
------|-----------------------|---------------------------------------------------------------------|--------------------------
GET   | /api/Tickets          | Список всіх бiлетiв                                                 |
GET   | /api/Tickets/{id}     | Один бiлет по id                                                    | /api/Tickets/2
GET   | /api/Tickets/{flightId}/{flightDate} | Вiльнi бiлети по рейсу на дату                       | /api/Tickets/QW11/2018-07-13T08:22:56.6404304+03:00
GET   | /api/Tickets/Bay/{id}  | Покупка бiлета                                                     | /api/Tickets/Bay/2
GET   | /api/Tickets/Return/{id}| Повернення бiлета                                                 | /api/Tickets/Return/2
GET   | /api/Stewardesses | Список всіх стюардес                                                    | /api/Stewardesses
GET   | /api/Stewardesses/{id}   | Одна по id                                                       | /api/Stewardesses/2
POST  | /api/Stewardesses   | Додати стюардессу                                                     | /api/Stewardesses
PUT   | /api/Stewardesses/{id}   | Оновити стюардессу                                               | /api/Stewardesses/2
DELETE| /api/Stewardesses/{id}    | Видалити стюардессу                                             | /api/Stewardesses/2
GET   | /api/Pilots           | Список всіх                                                         | /api/Pilots
GET   | /api/Pilots/{id}     | Однин по id                                                        | /api/Pilots/2
POST  | /api/Pilots          | Додати пiлота                                                      | /api/Pilots
PUT   | /api/Pilots/{id}     | Оновити пiлота                                                     | /api/Pilots/2
DELETE| /api/Pilots/{id}     | Видалити пiлота                                                    | /api/Pilots/2
GET   | /api/PlaneTypes       | Список всіх                                                         | /api/PlaneTypes
GET   | /api/PlaneTypes/{id}     | Однин по id                                                        | /api/PlaneTypes/2
POST  | /api/PlaneTypes          | Додати тип лiтака                                                      | /api/PlaneTypes
PUT   | /api/PlaneTypes/{id}     | Оновити тип лiтака                                                     | /api/PlaneTypes/2
DELETE| /api/PlaneTypes/{id}     | Видалити тип лiтака                                                    | /api/PlaneTypes/2
GET   | /api/Planes | Список всіх                                                         | /api/Planes
GET   | /api/Planes/{id}     | Однин по id                                                        | /api/Planes/2
POST  | /api/Planes          | Додати лiтак                                                      | /api/Planes
PUT   | /api/Planes/{id}     | Оновити лiтак                                                     | /api/Planes/2
DELETE| /api/Planes/{id}     | Видалити лiтак                                                    | /api/Planes/2
GET   | /api/Crews | Список всіх                                                         | /api/Crews
GET   | /api/Crews/{id}     | Однинa по id                                                        | /api/Crews/2
POST  | /api/Crews          | Додати команду                                                      | /api/Crews
PUT   | /api/Crews/{id}     | Оновити команду                                                     | /api/Crews/2
DELETE| /api/Crews/{id}     | Видалити команду                                                    | /api/Crews/2
GET   | /api/Departures         | Список всіх                                                         | /api/Departures
GET   | /api/Departures/{id}     | Однин по id                                                        | /api/Departures/2
POST  | /api/Departures          | Додати вилiт                                                      | /api/Departures
PUT   | /api/Departures/{id}     | Оновити вилiт                                                     | /api/Departures/2
DELETE| /api/Departures/{id}     | Видалити вилiт                                                    | /api/Departures/2  
GET   | /api/Flights          | Список всіх                                                         | /api/Flights
GET   | /api/Flights/{id}     | Однин по id                                                        | /api/Flights/2
POST  | /api/Flights          | Додати рейс                                                      | /api/Flights
PUT   | /api/Flights/{id}     | Оновити рейс                                                     | /api/Flights/2
DELETE| /api/Flights/{id}     | Видалити рейс                                                    | /api/Flights/2  

![REST](https://github.com/5avel/AirportRESRfulApi/blob/develop/123.jpg)
