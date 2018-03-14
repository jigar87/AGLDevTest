# AGL Dev Test

## AGL.Client
This is the client application which consumes the service layer. For this test purpose I have used a simple console
application as client. To run the application, load the solution and press run (F5).

## AGL.Service
This is the service layer, I have moved all the business objects and business logic in this layer on purpose. If we want to add more client applications like Web app, mobile app etc to 
we can use this service layer to fulfil such requirement.

## AGL.Tests
This is the unit test project and it uses NUnit framework to test the service layer. 