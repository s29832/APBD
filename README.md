# APLIKACJA KONSOLOWA WYPOZYCZALNI SPRZETU 

```
Apikacja napisana w języku C# pozwalająca na zarządzanie wypożyczalnią sprzętu przez menu w terminalu
```
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

Aplikacja została podzielona na trzy warstwy i została zaprojketowana na podstawie analogii do świata reczywistego przez co każda klasa ma swoją odpowiedzialność, żadna z klas nie jest od siebie bezpośrednio zależne. Przekazywanie elementów przez konstruktor do klas serwisowych pozwala na operowanie na tych samych instancjach.

1. **Warstwa Modeli**
W folderze model klasy takie jak `Equipment`, `User` czy `Rental` są tylko kontenerami na dane. Mają swoje ID oraz znają swój własny stan i podstawią się przedstawić ale nie ma w nich żadnej logiki biznesowej. W projekcie wykorzystano dziedziczenie tam, gdzie naturalnie wynikało (np. bazowa klasa abstrakcyjna `Equipment` i dziedziczące po niej `Laptop`, `Camera`, `Projector`) - wszystkie mają cechy Equipment.
Klasa Rental to interpretacja fizycznego papieru wypożyczenia. Papier taki powinien zawierać informacje takie jak kto, co, kiedy. Na podstawie papieru powinno dać się wyczytać czy wypożyczenie przekroczyło termin.

2. **Warstwa Serwisów**
   System został w późniejszym etapie dla ułatwienia poruszania podzielony na oddzielne serwisy:
   * `EqServ` - odpowiedzialny wyłącznie za zarządzanie sprzętem, jako jeden wielki regał magazynowy z którego wydaje się lub wkłada sprzęt
   * `UserServ` - odpowiedzialny wyłącznie za listę klientów, jako szafa z kartotekami klientów, prowadzi ich rejestr.
   * `RentServ` - Klasa logiki biznesowej, wymuszająca zasady jako kierownik obsługujący logikę wypożyczalni, Podpisuje wypożyczenia, przyjmuje zwroty musi mieć       dostęp do Sprzętu. Prowadzi listę wypożyczeń ale i sprawdza użytkowników, czy ten sprzęt jest wypożyczony, przedstawia raporty.

3. **Warstwa Interfejsu**
   Klasa `Interface` zajmuje się wyłącznie prezentacją. Zbiera dane, przekazuje je do odpowiednich serwisów i formatuje wyniki - 8 pierwszych znaków ID

4. **Warstwa incjacyjna**
   Uruchomienie programu w pliku Program.cs

## PREZENTACJA (przykładowa)

Dodanie użytkownika
> 1 -> Wpisz s (Student) -> Podaj imię i nazwisko.

Dodanie sprzętu
> 2 -> Wpisz laptop -> Podaj nazwę (np. Dell), procesor i RAM.

Lista całego sprzętu
> 3

Lista dostępnego sprzętu
> 4

Wypożyczenie (i blokada limitu)
> 5 -> Wpisz ? (wybierz ID studenta) -> Wpisz ? (wybierz ID sprzętu) -> Wpisz 7 (dni).

Mozna sprobowac wypozyczyc temu samemu studentowi 4 przedmioty, by zobaczyć blokadę limitu.

Zwrot i naliczenie kary
> 6 -> Wpisz ? (wybierz ID wypożyczenia) -> Wpisz datę z przyszłości (np. 2026-12-01), aby system wyliczył karę w PLN.

Oznaczenie sprzętu jako uszkodzony/serwis
> 7 -> Wpisz ? (wybierz ID sprzętu). Sprzęt otrzyma status Unavailable.

Aktywne wypożyczenia użytkownika
> 8 -> Wpisz ? (wybierz ID studenta).

Przeterminowane wypożyczenia
> 9

Raport podsumowujący
> 10

## UWAGI
W pewnym momencie branche Equipment, Rental, RentalService i Users zostaly zmergowane w jeden wspólny branch dla ulatwienia podziału INTERFEJS | MAIN | BACKEND
