
# Rentful
### Autor: Paweł Kamiński

### Testowanie i Jakość Oprogramowania


#### Temat projektu
Aplikacja do efektywnego wyszukiwania oraz zarządzania nieruchomościami

#### Opis projektu
Celem było stworzenie kompleksowej aplikacji umożliwiającej właścicielom nieruchomości skuteczne i efektywne wyszukiwanie oraz zarządzanie wynajmowanymi mieszkaniami. Aplikacja obejmuje opracowanie systemu od samego początku, który pozwala na dodawanie nowych mieszkań, w których właściciel ma możliwość wystawienia ogłoszenia z wolnym mieszkaniem wraz z wszystkimi najważniejszymi informacjami na temat tego mieszkania. Użytkownicy serwisu mają możliwość przeglądania najnowszych ofert najmu z podziałem na kategorie oraz filtrujące dane takie jak długość, oraz szerokość geograficzną co zapewnia szybkie i efektywne przeglądanie ofert na podstawie preferowanych topografii miast.  Następnie potencjalni lokatorzy mają możliwość zgłaszać się na termin oględzin mieszkania, co ułatwi proces wynajęcia mieszkania. System usprawni także zarządzanie mieszkaniami oraz prowadzenie komunikacji z lokatorami, którzy będą mieli możliwość szybkiego i sprawnego zgłaszania usterek w mieszkaniu. Rozwiązanie ma na celu usprawnić proces zarządzania nieruchomościami dla właścicieli mieszkań oraz dla lokatorów w sprawnej komunikacji.

### Uruchomienie projektu
#### Backend
    dotnet run
#### Frontend
    npm i
    npm run dev

### Testy
#### Testy jednostkowe


[![Testy](https://img.shields.io/badge/Przejd%C5%BA%20do%20testów%20jednostkowych-8A2BE2)](https://github.com/enqer/be-rentful/tree/main/src/Rentful.Tests/UseCases)
    
AddAnnouncementTest :
- Should_AddNewAnnouncement - test sprawdzający dodanie nowego ogłoszenia
- When_CityIsMissing_Should_AddNewAnnouncementWithClosestCity - test dodający nowe ogłoszenie z automatycznym przypisaniem wględnej lokalizacji
- When_CityCoordsAreMissing_Should_AddNewAnnouncementWithCoordsByCity - dodawanie ogłoszenia z automatycznym przypisaniem lokalizacji geograficznej
- When_ImagesAreEmpty_Should_Throws_HttpResponseException - test sprawdzający czy zdjęcia mieszkań są dodane do ogłoszenia
- Should_AddAnnouncementWithThumbnailAsFirstImage() - dodawanie nowego ogłoszenia z pierwszym zdjęciem jako miniaturka

GetAnnouncementByIdTest:
- Should_ReturnAnnouncementById - test zwrócenia konkretnego ogłoszenia
- Should_ReturnAnnouncementByIdWithReservationsDifferentThanApproved - zwracanie ogłoszenia tylko z zgodnymi rezerwacjami
- When_AnnouncementDoesntExist_Should_ThrowHttpResponseException - wyrzucenie wyjątku, jeśli ogłoszenie nie istnieje

GetAnnouncementsTest:
- Should_ReturnAnnouncements - test zwrócenia listy dostępnych mieszkań

GetProvinceCitiesTest:
- Should_ReturnListOfCitiesGroupedByProvince - test sprawdzający grupowanie miast na podstawie województwa

#### Testy integracyjne

[![Testy](https://img.shields.io/badge/Przejd%C5%BA%20do%20testów%20integracyjnych-8A2BE2)](https://github.com/enqer/be-rentful/tree/main/src/Rentful.Tests/Controllers)

Identities/AuthUserTest:
- When_UserWithIncorrectPassword_Should_ReturnBadRequest - sprawdzanie poprawności logowania
- When_UserExistWithCorrectPassword_Should_ReturnOKStatusCodeAndAccessToken - poprawne logowanie powinno zwrócić jwt token

Identities/RegisterUserTest:
- When_UserAlreadyExist_Should_ReturnBadRequest - test rejestracji sprawdzającej istniejącego użytkownika
- When_UserDoesntExist_Should_RegisterUserAndReturnOkStatusCode - test poprawnej rejestracji

Reservations/AssignReservationTest
- When_ReservationDoesntExist_Should_ReturnNotFoundStatusCode - test sprawdzający istnienie rezerwacji
- When_AssignedReservation_Should_ReturnOkStatusCode - przypisanie rezerwacji
- When_UserNotLogged_Should_ReturnUnauthorized - testowanie autoryzacji użytkownika przy przypisaniu rezerwacji

Reservations/CancelReservationTest
- Should_CancelReservation - anulowanie rezerwacji
- When_ReservationIsMissing_Should_ThrowExceptionAndReturnNotFound - testowanie wyrzucenia wyjątku w przypadku gdy rezerwacja nie istnieje

Reservations/GetUserReservationTest
- Should_ReturnUserReservations - zwrócenie rezerwacji autora ogłoszeń

### Dokumentacja API
Po uruchomieniu aplikacji Swagger powinnien być dostępny pod adresem

    https://localhost:7191/swagger/index.html 
    http://localhost:5110/swagger/index.html

### Przypadki testowe


[![Logging](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC001-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC001.JPG)

[![Register](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC002-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC002.JPG)

[![Register](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC003-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC003.JPG)

[![CancelRegister](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC004-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC004.JPG)

[![FilteringAnnouncements](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC005-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC005.JPG)

[![AssignReservation](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC006-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC006.JPG)

[![Location](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC007-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC007.JPG)

[![Images](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC008-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC008.JPG)

[![MapDisplay](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC009-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC009.JPG)

[![AddAnnouncement](https://img.shields.io/badge/Przejd%C5%BA%20do%20TC010-8A2BE2)](https://github.com/enqer/fe-rentful/tree/main/src/assets/test_images/TC010.JPG)





### Stos technologiczny:

- ASP.NET Core
- C# 
- Vue.js
- TypeScrpit
- JavaScript
- Quasar 
- TailWind CSS
- Postgresql
