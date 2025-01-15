# RDSoft.OwnerCardActions

This is a .NET 8 WebAPI microservice<br />
It deliveres allowed actions for certain users card types<br />

## Contents
- [Description](#description)
- [Solution structure](#solution-structure)

## Description
Grabs card details from external service and returns allowed actions for the card based on its type and status.

## Solution structure

1) RDSoftOwnerCardActions.Api:

Contains API controllers and WebAPI configuration. Handles HTTP requests and maps data to/from the application layer. Manages JSON formats and input data validation.


2) RDSoftOwnerCardActions.Application:

Implements application logic, including processing input data and generating allowed actions for cards. Contains interfaces for communication with other layers, such as the domain and infrastructure. Responsible for handling edge cases and business rules.

3) RDSoftOwnerCardActions.Domain:

Contains business logic in its purest form, independent of infrastructure. Includes domain classes like CardDetails, CardType, CardStatus, as well as logic for mapping card types/statuses to actions. Implements design patterns such as Specification or Factory if needed.

4) RDSoftOwnerCardActions.Infrastructure:

Handles communication with external services or databases. May include the implementation of repositories, an HTTP client for retrieving card details (currently simulated), and logic related to data access configuration.obierania szczegółów kart (na razie symulowaną), oraz logikę związana z konfiguracją dostępu do danych.