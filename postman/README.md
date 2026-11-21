# Teste API (Postman + Newman)

Colectia `HotelExplorer.postman_collection.json` verifica API-ul: health, autentificare, drepturile
USER/ADMIN si operatiile complete (creare, citire, modificare, stergere) pentru lanturi, hoteluri,
camere si servicii. Datele create de teste se sterg la final, deci colectia se poate rula de mai
multe ori.

## Rulare cu Newman (din terminal)

Backend-ul trebuie sa ruleze pe `http://localhost:3000` (`npm run dev` in `backend`), cu baza de
date pornita si seed-ul aplicat (contul `admin@hotelexplorer.ro` si contul demo).

```powershell
cd postman
npm install
npm test
```

Comanda iese cu cod diferit de 0 daca un test pica.

## Rulare in Postman

1. Import: `HotelExplorer.postman_collection.json` si `local.postman_environment.json`.
2. Alege mediul "HotelExplorer local".
3. Click dreapta pe colectie, Run collection, Run. Ordinea conteaza: fiecare folder depinde de cel
   dinaintea lui.

Conturile din `local.postman_environment.json` sunt cele din seed si sunt valabile doar pentru
dezvoltare locala.
