# Phone Book App

## Description

This solution is made up of three projects:

- Phonebook.AngularApp: Main website using Angular
- PhoneBook.Models: Database context/entities and other classes
- PhoneBook.WebApi: Web service that updates the database

## Installation

The WebApi appsettings.json contains a connection string called PhoneBookContext that needs to point at a MS SQL Server - the database does not need to be created.

## First Run

When the PhoneBook database is created at first run, an Application User row needs adding, just the created/updated dates.

The Application User Id used in several places (hardcoded for now) needs to be change from 1 if adding the row creates an Id that isn't 1
