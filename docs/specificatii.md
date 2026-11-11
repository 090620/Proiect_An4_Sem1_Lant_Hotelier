# Specificații

Sursa: Raport 1 (`Raport1_HotelExplorer.pdf`), secțiunea 3.

## Cerințe funcționale

| ID | Cerință | Actor | Stare |
|---|---|---|---|
| RF1 | Înregistrarea unui cont nou | Vizitator | reimplementat |
| RF2 | Autentificare și deconectare | Vizitator, Utilizator | reimplementat |
| RF3 | Profil și actualizarea pozei de profil (JPG, PNG, GIF, WEBP) | Utilizator | reimplementat |
| RF4 | Consultarea lanțurilor hoteliere și a hotelurilor fiecărui lanț | Utilizator | reimplementat |
| RF5 | Consultarea camerelor (tip, preț, descriere) și a serviciilor | Utilizator | reimplementat (camera aparține unui hotel) |
| RF6 | Consultarea ofertelor (preț vechi, preț nou, reducere, cod promoțional) și a evenimentelor | Utilizator | reimplementat (prețuri în RON) |
| RF7 | Consultarea galeriei foto a hotelurilor | Utilizator | reimplementat |
| RF8 | Consultarea și adăugarea de recenzii (rating 1–5 și comentariu) | Utilizator | reimplementat (legate de contul autentificat) |
| RF9 | Crearea unei rezervări: lanț, hotel, cameră, perioadă, ofertă opțională, validarea perioadei și a suprapunerilor pe server, calcul automat al prețului, status inițial „în așteptare” | Utilizator | reimplementat și corectat |
| RF10 | Vizualizarea rezervărilor proprii și a confirmării | Utilizator | reimplementat |
| RF11 | Adăugarea, modificarea și ștergerea lanțurilor, hotelurilor, camerelor, serviciilor, ofertelor, galeriei și evenimentelor | Administrator | reimplementat în API |
| RF12 | Ștergerea recenziilor necorespunzătoare | Administrator | reimplementat în API |
| RF13 | Listarea tuturor rezervărilor, acceptarea, respingerea și ștergerea lor | Administrator | reimplementat în API |
| RF14 | Expunerea funcțiilor aplicației printr-un API REST, cu acces pe roluri | Vizitator, Utilizator, Administrator | nou |
| RF15 | Frontend separat, în React, pentru paginile principale (autentificare, lanțuri și hoteluri, camere, rezervare, rezervările mele, galerie); administrarea dacă timpul permite | Vizitator, Utilizator, Administrator | nou |
| RF16 | Colecție Postman pentru documentarea și testarea API-ului | Dezvoltator | nou |

## Cerințe nefuncționale

| ID | Categorie | Cerință |
|---|---|---|
| RNF1 | Securitate | Parole cu hash (bcrypt), acces pe roluri, date validate pe server, CORS doar pentru originea frontend-ului |
| RNF2 | Portabilitate | API-ul, frontend-ul și baza de date pornesc cu o singură comandă, prin Docker Compose |
| RNF3 | Mentenabilitate | Cod pe straturi (rute, servicii, acces la date prin Prisma); schimbările de schemă prin migrări Prisma versionate în Git |
| RNF4 | Testabilitate | Funcțiile principale ale API-ului (inclusiv calculul prețului și validarea rezervărilor) acoperite de teste Postman, rulate cu Newman la fiecare push |
| RNF5 | Utilizabilitate | Interfața se adaptează pe telefon și pe calculator |
| RNF6 | Trasabilitate | Fiecare modificare trece prin Git, cu issue asociat și mesaj de commit clar |
| RNF7 | Documentare | API documentat prin colecția Postman; README descrie instalarea și rularea |

## Modelul de date

| Entitate | Atribute principale | Relații |
|---|---|---|
| Lanț hotelier | nume, descriere | are hoteluri, servicii, oferte și evenimente |
| Hotel | nume, oraș, țară, adresă, descriere, imagine | aparține unui lanț; are camere, galerie, recenzii și rezervări |
| Cameră | tip, preț (RON), descriere | aparține unui hotel |
| Serviciu | nume, descriere, preț, iconiță | aparține unui lanț; se leagă de hoteluri |
| Ofertă | titlu, descriere, preț vechi, preț nou (RON), procent de reducere, cod promoțional | aparține unui lanț |
| Eveniment | titlu, descriere, imagine, dată și oră, locație | aparține unui lanț |
| Galerie | nume imagine, cale imagine | aparține unui hotel |
| Recenzie | comentariu, rating | aparține unui hotel și unui utilizator |
| Rezervare | dată început, dată sfârșit, observații, status, preț total, data creării | leagă un utilizator, o cameră, un hotel și, opțional, o ofertă |
| Utilizator | email, parolă (hash), nume complet, poză de profil, rol | face rezervări și recenzii |

## Reguli de funcționare

- O rezervare nouă pornește cu statusul „în așteptare” (`Pending`); administratorul o confirmă sau o respinge.
- Data de sfârșit trebuie să fie după data de început (verificat pe server).
- Preț total = preț cameră × număr nopți × (1 − procent reducere ofertă / 100). Fără ofertă, nu se aplică reducere.
- Prețurile sunt în RON.
- O cameră nu poate fi rezervată în perioade care se suprapun (regulă nouă).
- Utilizatorul vede doar rezervările lui; administratorul le vede pe toate.
- Doar administratorul adaugă, modifică sau șterge conținutul și poate șterge recenzii.
- Rating-ul unei recenzii este între 1 și 5.
- Contul de administrator se creează la inițializarea bazei de date; conturile create prin înregistrare primesc rolul `User`.
- Serviciile, ofertele și evenimentele aparțin unui lanț; camerele, galeria și recenziile aparțin unui hotel.

## Probleme din prototip, corectate în noua versiune

Verificate direct în codul prototipului (`Lant_Hotelier/Lant_Hotelier`).

| Problemă în prototip | Unde se vede în cod | Soluție în noua versiune |
|---|---|---|
| Reducerea din oferte se calculează greșit: `PretTotal * (1 - camera.Pret / 100)` în loc de procentul ofertei; totalul poate deveni negativ. Oferta nici nu este citită (apelul `GetOferteByLantIdAsync(0)` nu e folosit) | `RezervariController.Create` | Calcul într-un serviciu, cu procentul ofertei, acoperit de teste |
| Perioada rezervării e validată doar în browser (atribute `min` și JavaScript); pe server nu se verifică perioada și nici suprapunerile | `Views/Rezervari/Create.cshtml`, `RezervariService` | Validări pe server |
| Recenziile se salvează cu `ID_Client = 1` fix, iar numele clientului se scrie manual; rezervările păstrează un `ID_Utilizator = 1` fix (câmp moștenit), deși au și `AspNetUserId` legat de contul autentificat | `RecenziiController.Create`, `RezervariController.Create`, modelele `Recenzie` și `Rezervare` | Rezervările și recenziile se leagă doar de contul autentificat; tabelul vechi `Utilizator` nu se mai folosește |
| Camera aparține unui lanț (`Camera.ID_Lant`), nu unui hotel | `Models/Camera.cs` | Camera aparține unui hotel |
| Prețuri camere în RON, prețuri oferte în EUR | `Camera` (`Preț (RON)`), `Oferte` (`Preț Vechi (€)`, `Preț Nou (€)`) | O singură monedă (RON) |
| `HotelOferta` și `Contact` sunt definite și apar în `DbContext`, dar nu sunt folosite nicăieri | `Models/HotelOferta.cs`, `Models/Contact.cs` | Nu se mai preiau |
| Formularul de creare a serviciilor (acțiunea GET `Create`) nu cere rolul Admin; salvarea (POST) îl cere | `HotelServiciiController` | Rolul se verifică în API pentru toate operațiile de administrare |
| Primul cont înregistrat primește automat rolul Admin | `IdentityService.RegisterAsync` | Contul de administrator se creează la inițializarea bazei de date; conturile înregistrate primesc `User` |
