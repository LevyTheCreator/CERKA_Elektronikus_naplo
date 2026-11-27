<div align="center" style="margin-top:0px">
  <img src="Pictur/cerka_icon.png" alt="CERKA" width="20%" align="middle">
  <img src="Pictur/cerka_wordmark.png" alt="CERKA" width="79%" style="position:relative;left:-20;bottom:10" align="middle">
</div>

# CERKA – Elektronikus Napló

Konzolos elektronikus iskolai napló- és nyilvántartó alkalmazás <b>C#</b> nyelven. A rendszer osztályok, diákok, tanárok és adminisztrátorok kezelésére, beléptetésére, jelszókezelésére és tanulmányi adatok tárolására szolgál.

---

## Főbb funkciók és működés

### 1. Beléptetés és jogosultságok

* Felhasználókeresés a `bejelentkezes.csv` fájlban (<i></i> elválasztással) a megadott felhasználónév alapján.
* Jelszóellenőrzés a `PasswordEncryption.VerifyPassword` metódussal, legfeljebb **3 próbálkozással** (3. sikertelen próbánál a program leáll).
* Jogosultsági szintek: <b>`Admin`</b> / <b>`Diák`</b> / <b>`Tanár`</b>.
* **Jelszóváltoztatás (`passwordchange`):** ellenőrzi a régi jelszót, bekéri és egyezteti az új jelszót kétszer, hash-eli (`PasswordEncryption.HashPassword`), frissíti a `bejelentkezes.csv` fájlt, közben ideiglenesen leveszi és visszateszi a `Hidden` fájlattribútumot.

### 2. Admin (`sysadmin`) funkciók

* **Hozzáadás (`add`):**
  * **Új osztály (`1. Osztály`):** bekéri az osztály nevét (kisbetűsítve) és az osztályfőnök nevét, létrehozza a `SchoolClass` példányt és az `Osztalyok/<osztálynév>` mappát, valamint felveszi a tanárt a `bejelentkezes.csv`-be (`Tanar` szerepkörrel, `<NévSzóközNélkül><Év>` alapjelszó-hashhel). Rögzíthetőek diákok is (`0 - nem | 1 - igen`), létrehozva a `Student` objektumokat és a tanulói adatfájlokat (`N/A` kezdeti jegyekkel: `Matek`, `Irodalom`, `Történelem`, `Angol`, `Nyelvtan`).
  * **Új diák (`2. Diák`):** megadott osztályhoz vesz fel új diákokat (`Id`, `Name`), regisztrálja őket a `bejelentkezes.csv`-ben (`Diak` szerepkörrel, automatikusan generált kezdőjelszó-hashhel), és létrehozza a diák adatfájlját.
* **Módosítás (`modify`):**
  * **Azonosító változtatása (`1`):** frissíti a diák azonosítóját a tanulói fájlban, átnevezi a fájlt az új azonosítóra (`File.Move`), és frissíti a felhasználói azonosítót a `bejelentkezes.csv`-ben is.
  * **Név változtatása (`2`):** frissíti a diák nevét a tanulói fájlban (`szóköz -> _` formázással).
* **Jelszó módosítása (`passwordchange`):** saját jelszó cseréje.

### 3. Diák (`Diak`) funkciók

* **Átlag megtekintése (`viewgpa`):**
  * **Tantárgyi átlag (`1`):** `Matek`, `Irodalom`, `Történelem`, `Angol` vagy `Nyelvtan` kiválasztása után `;`-vel elválasztott jegyekből számol átlagot (ha `N/A`, jelzi, hogy még nincs jegy).
  * **Összátlag (`2`):** a meglévő tantárgyi átlagok átlagát számolja ki.
* **Jegyek megtekintése (`viewgrades`):** kiválasztott tantárgy vagy az összes tantárgy sorainak kiírása a konzolra.
* **Jelszó módosítása (`passwordchange`):** saját jelszó cseréje.

### 4. Tanár (`Tanár` / `Tanar`) szerepkör

* **Jegyek kezelése (`add`)** – új jegy rögzítése vagy meglévő jegy módosítása.
* **Átlagok lekérdezése (`viewgpa`)** – tantárgyi vagy összesített átlag számítása.
* **Jegyek lekérdezése (`viewgrades`)** – egy tantárgy jegyeinek vagy az összes jegy megjelenítése.
* **Jelszó módosítása (`passwordchange`)** – a saját jelszó cseréje.

---

## Projekt felépítése és fájlok

* `Program.cs` – Beléptető logika, inicializálás, ASCII banner, főmenük és a `add`, `passwordchange`, `viewgpa`, `viewgrades`, `modify` metódusok
* `Student.cs` – Diák modell (`Id`, `Name`, tantárgyi jegymezők)
* `SchoolClass.cs` – Osztály modell (`className`, `formTeacher`, diákok listája)
* `PasswordEncryption.cs` – Jelszó-hashelő (`HashPassword`) és ellenőrző (`VerifyPassword`) metódusok
* `bejelentkezes.csv` – Rejtett (`Hidden`) CSV fájl a felhasználókkal, hash-elt jelszavakkal, szerepkörökkel és osztályokkal (`;` elválasztóval)
* `Osztalyok/` – Osztályonkénti mappák a diákok adataival

---

### Indítás

```bash
git clone https://github.com/LevyTheCreator/CERKA_Elektronikus_naplo.git
cd CERKA/CERKA
dotnet restore
dotnet run
``` 
**Vagy** 
```bash
git clone https://github.com/LevyTheCreator/CERKA_Elektronikus_naplo.git
```
a <b>`CERKA_Elektronikus_naplo\CERKA`</b> mappában
a <b>`CERKA.exe`</b> file elindításával.