# Eaton Test

## Intro

You monitor devices, which are sending data to you.
Each device has a unique name.
Each device produces measurements.


The challenge is:

Compute the number of messages you got or read from the devices.

The solution should be written in C#.
The scope is open, you must decide how the �devices� will work in your system.
The solution should be posted on GitHub or a similar page for review.

Please add documentation explaining to us how to run your code.
Try to show us as much as possible your seniority in programming.

## How to run?

To run API app, go to API folder (EatonTest)
> cd "EatonTest"

build the project:
> dotnet build

and run the API:
> dotnet run


To run the integrations tests, go to integration test folder:
> cd "EatonTest.IntegrationTests"

and run the tests:
> dotnet test
