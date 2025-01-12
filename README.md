# RDSoft.OwnerCardActions

This is a .NET 8 WebAPI microservice<br />
It deliveres allowed actions for certain users card types<br />

## Contents
- [Description](#description)
- [Solution structure](#solution-structure)

## Description
To be done...

## Solution structure

1) RDSoftOwnerCardActions.Api:

Zawiera kontrolery API i konfigurację WebAPI.
Obsługuje żądania HTTP i mapuje dane z/do warstwy aplikacyjnej.
Zajmuje się obsługą formatów JSON oraz walidacją danych wejściowych.


2) RDSoftOwnerCardActions.Application:

Implementuje logikę aplikacyjną, czyli przetwarzanie danych wejściowych i generowanie dozwolonych akcji dla kart.
Zawiera interfejsy do komunikacji z innymi warstwami, np. domeną i infrastrukturą.
Odpowiada za obsługę przypadków brzegowych i reguł biznesowych.

3) RDSoftOwnerCardActions.Domain:


Zawiera logikę biznesową w najczystszej postaci, niezależną od infrastruktury.
Klasy domenowe, takie jak CardDetails, CardType, CardStatus, oraz logikę mapującą typ/status karty na akcje.
Implementuje wzorce projektowe, takie jak specyfikacja czy fabryka, jeśli będą potrzebne.


4) RDSoftOwnerCardActions.Infrastructure:


Odpowiada za komunikację z zewnętrznymi usługami lub bazami danych.
Może zawierać implementację repozytoriów, klienta HTTP dla pobierania szczegółów kart (na razie symulowaną), oraz logikę związana z konfiguracją dostępu do danych.