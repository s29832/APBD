# APLIKACJA KONSOLOWA WYPOZYCZALNI SPRZETU 

## INSTRUKCJA URUCHOMIENIA

Aby uruchomić aplikację, potrzebujesz:
> Zainstalowanego pakietu **.NET SDL** (wersja 8.0)

> IDE (np. JetBrains Rider, VS Code) lub zwykłego terminala

### OPCJA TERMINAL
1. Sklonuj to repozytorium na swój dysk lokalny:
   `git clone [LINK DO REPO]'
2. Otwórz terminal i przejdź do folderu z projektem.
3. Wpisz komendę `dotnet run`
4. Aplikacja uruchomi interaktywne menu w konsoli

### OPCJA IDE
1. Otwórz plik `.sln` lub plik projektu `.csproj` w IDE.
2. Kliknij przycisk **Run** (zielony trójkąt) na górnym pasku narzędzi.

## DECYZJE PROJEKTOWE
# System Wypożyczalni Sprzętu (Rental Service System)

## Architektura i Decyzje Projektowe
 
Aplikacja została podzielona na trzy warstwy:

1. **Warstwa Modeli**
Klasy takie jak `Equipment`, `User` czy `Rental` są tylko kontenerami na dane. Mają swoje ID oraz znają swój własny stan. W projekcie wykorzystano dziedziczenie tam, gdzie naturalnie wynikało (np. bazowa klasa abstrakcyjna `Equipment` i dziedziczące po niej `Laptop`, `Camera`, `Projector`) - wszystkie mają cechy Equipment

2. **Warstwa Serwisów**
   System został w późniejszym etapie dla ułatwienia poruszania podzielony na oddzielne serwisy:
   * `EqServ` - odpowiedzialny wyłącznie za zarządzanie sprzętem.
   * `UserServ` - odpowiedzialny wyłącznie za listę klientów.
   * `RentServ` - Klasa logiki biznesowej, wymuszająca zasady (np. limity sprzętu na użytkownika, dostępność sprzętu, naliczanie kar za opóźnienia).

3. **Warstwa Interfejsu**
   Klasa `Interface` zajmuje się wyłącznie prezentacją. Zbiera dane, przekazuje je do odpowiednich serwisów i formatuje wyniki - 8 pierwszych znaków ID

## UWAGI
W pewnym momencie branche Equipment, Rental, RentalService i Users zostaly zmergowane w jeden wspólny branch dla ulatwienia podziału INTERFEJS | MAIN | BACKEND
