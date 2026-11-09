# Lanț Hotelier (HotelExplorer)

Autor: Ungureanu Lorena-Mihaela, CR S2B.

HotelExplorer prezintă mai multe lanțuri hoteliere: hoteluri, camere, servicii, oferte,
galerie foto, evenimente, recenzii și rezervări (confirmate sau respinse de un administrator).

## Starea curentă

Depozitul conține prototipul ASP.NET Core MVC (.NET 9, Entity Framework Core, Identity,
SQL Server LocalDB), în folderul `Lant_Hotelier/`. Urmează refacerea aplicației cu
Node.js + Express + Prisma + PostgreSQL pentru backend și React pentru frontend.

## Rularea prototipului

1. Deschide `Lant_Hotelier/Lant_Hotelier.slnx` în Visual Studio.
2. Dacă baza de date nu există, în Package Manager Console rulează `Update-Database`.
3. Pornește aplicația cu F5.

## Cum lucrăm

- `main` rămâne stabil; modificările se fac pe branch-uri (`docs/...`, `feature/...`, `fix/...`).
- Fiecare modificare are un issue și ajunge în `main` printr-un Pull Request.
- Mesajele de commit au formatul `tip: descriere (#issue)`.