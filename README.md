# AGL Dev Test
This is coding test from AGL based on following specification - http://agl-developer-test.azurewebsites.net/

## Requirements

A json web service has been set up at the url: http://agl-developer-test.azurewebsites.net/people.json

AGL.Service consumes the json and output a list of all the cats in alphabetical order under a heading of the gender of their owner.
This application is written in C#.net using third party libraries like Newtonsoft.JSON, NUnit

Below is the example of how output should be displayed

```yml
Male

   - Angel
   - Molly
   - Tigger

Female

   - Gizmo
   - Jasper
```


## Prerequisites

Visual Studio 2017

## AGL.Client
This is the client application which consumes the service layer. For this test purpose I have used a simple console
application as client. To run the application, load the solution and press run (F5).

## AGL.Service
This is the service layer, I have moved all the business objects and business logic in this layer on purpose. If we want to add more client applications like Web app, mobile app etc to 
we can use this service layer to fulfil such requirement.

## AGL.Tests
This is the unit test project and it uses NUnit framework to test the service layer. 