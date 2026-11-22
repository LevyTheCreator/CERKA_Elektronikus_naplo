using System;
using System.ComponentModel;
using System.Net;

void Add()
{
    Console.WriteLine("0. Visszalépés || 1. Osztály || 2. Diák");

    int choice;
    string input;
    do
    {
        Console.Write("Válassz (0-2): ");
        input = Console.ReadLine();

    } while (!int.TryParse(input, out choice) || choice < 0 || choice > 2);

    switch (choice)
    {
        case 1:

            Console.Write("Osztály neve (pl.: 11.a vagy 11.A): ");
            string className = Console.ReadLine().ToLower();

            Console.Write("Osztály osztályfőnöke: ");
            string formTeacher = Console.ReadLine();

            Console.Write("Szeretne most diákot is hozzáadni?(0 - nem | 1 - igen): ");
            int addStudent = int.Parse(Console.ReadLine());

            switch(addStudent)
            {
                case 0:

                    List<Student> students = new List<Student>();

                    SchoolClass schoolClass = new SchoolClass(className, formTeacher, students);

                    Directory.CreateDirectory($"Osztalyok/{className}");

                    break;

                case 1:

                    Console.Write("Diákok száma: ");
                    int studentAmount = int.Parse(Console.ReadLine());

                    students = new List<Student>();

                    for (int i = 0; i < studentAmount; i++)
                    {
                        Console.Write($"Diák {i + 1} neve: ");
                        string studentName = Console.ReadLine();
                        Console.Write($"Diák {i + 1} azonosítója: ");
                        string studentId = Console.ReadLine();

                        Student student = new Student(
                            studentId,
                            studentName,
                            "N/A",
                            "N/A",
                            "N/A",
                            "N/A",
                            "N/A"
                        );
                        students.Add(student);
                    }

                    schoolClass = new SchoolClass(className, formTeacher, students);

                    Directory.CreateDirectory($"Osztalyok/{className}");

                    foreach (Student s in students)
                    {
                        File.WriteAllText(
                            $"Osztalyok/{className}/{s.Id}.csv",
                            $"Név: {s.Name}\nAzonosító: {s.Id}\n"
                                + $"Jegyek:\n"
                                + $"Matek: {s.Math}\n"
                                + $"Irodalom: {s.Litrature}\n"
                                + $"Történelem: {s.History}\n"
                                + $"Angol: {s.English}\n"
                                + $"Nyelvtan: {s.Grammer}"
                        );
                    }
                    break;
            }       
            break;

        case 2:

            Console.Write("Melyik osztály: ");
            string whichClass = Console.ReadLine();

            if (!Directory.Exists(whichClass.ToLower()))
            {
                Directory.CreateDirectory($"Osztalyok/{whichClass}");
            }

                Console.Write("Diákok száma: ");
            int amountOfStudents = int.Parse(Console.ReadLine());

            if (Directory.Exists($"Osztalyok/{whichClass}"))
            {
                List<Student> students = new List<Student>();

                for (int i = 0; i < amountOfStudents; i++)
                {
                    Console.Write($"Diák {i + 1} neve: ");
                    string studentName = Console.ReadLine();
                    Console.Write($"Diák {i + 1} azonosítója: ");
                    string studentId = Console.ReadLine();
                    DateTime now = DateTime.Now;
                    File.AppendAllText("bejelentkezes.csv", studentId + ";" + PasswordEncryption.HashPassword(studentName.Replace(" " , "") + now.ToString("yyyy"))+";Diak\n");
                    Student student = new Student(
                        studentId,
                        studentName.Replace(" ","_"),
                        "N/A",
                        "N/A",
                        "N/A",
                        "N/A",
                        "N/A"
                    );
                    students.Add(student);
                }

                foreach (Student s in students)
                {
                    File.WriteAllText(
                        $"Osztalyok/{whichClass}/{s.Id}.csv",
                        $"Név: {s.Name}\nAzonosító: {s.Id}\n"
                            + $"Jegyek:\n"
                            + $"Matek: {s.Math}\n"
                            + $"Irodalom: {s.Litrature}\n"
                            + $"Történelem: {s.History}\n"
                            + $"Angol: {s.English}\n"
                            + $"Nyelvtan: {s.Grammer}"
                    );
                }
            }
            break;
    }
}

// Itt csinald meg a lekérést. - Levi
void retrieve(string[] found)
{
    if (found[2] == "Diak")
    {
        Console.Write("mit szeretne kiiratni? 0. Kilépés || 1. Átlag megtekintése || 2. Jegyek megtekintése");
        int choice;
        string input;
        do
        {
            Console.Write("Válassz (0-2): ");
            input = Console.ReadLine();

        } while (!int.TryParse(input, out choice) || choice < 0 || choice > 2);
        switch (choice)
        {
            case 0:
                return;
            case 1:
                Console.WriteLine("mit szeretne megtekinteni? 0. Visszalépés || 1. Jegy átlagának megtekintése || 2. Össz átlag megtekintése");
                int choice2;
                string input2;
                do
                {
                    Console.Write("Válassz (0-2): ");
                    input2 = Console.ReadLine();

                } while (!int.TryParse(input2, out choice2) || choice2 < 0 || choice2 > 2);
                switch (choice2)
                {
                    case 0:
                        return;
                    case 1:
                        string[] jegyek = File.ReadAllLines($@"C:\Users\Csanad\Documents\Git directories\CERKA_Elektronikus_naplo\CERKA\CERKA\bin\Debug\net10.0\Osztalyok\11.a\{found[0]}.csv");
                        jegyek.Skip(3);
                        int choice3;
                        string input3;
                        Console.Write("Melyik tantárgy? 0. Visszalépés || 1. Matek || 2. Irodalom || 3. Történelem || 4. Angol || 5. Nyelvtan");
                        do
                        {
                            Console.Write("Válassz (0-5): ");
                            input3 = Console.ReadLine();

                        } while (!int.TryParse(input3, out choice3) || choice3 < 0 || choice3 > 5);
                        switch (choice3)
                        {
                            case 0:
                                break;
                            case 1:
                                Console.Write(jegyek);
                                return;
                        }
                        return;

                }
                return;
        }
    }
}



//------------------------------------------------------------------------------


string hashedAdminPassword = PasswordEncryption.HashPassword("Menojelszo21");
string bejelentkezesfej =
    "Username;Password;Privileges\n" + $"sysadmin;{hashedAdminPassword};Admin\n";
if (!Directory.Exists("Osztalyok"))
{
    Directory.CreateDirectory("Osztalyok");
}

if (!File.Exists("bejelentkezes.csv"))
{
    File.WriteAllText("bejelentkezes.csv", bejelentkezesfej);
    File.SetAttributes("bejelentkezes.csv", FileAttributes.Hidden);
}

Console.WriteLine("Kérem jelentkezzen be!");
Console.WriteLine("-----------------");
Console.Write("Felhasználónév: ");
string username = Console.ReadLine();

string[] rows = File.ReadAllLines("bejelentkezes.csv");

string[] found = null;

foreach (string row in rows.Skip(1))
{
    if (string.IsNullOrWhiteSpace(row))
    {
        continue;
    }

    string[] parts = row.Split(";");
    if (parts[0] == username)
    {
        found = parts;
        break;
    }
}

string privilege = "Diak";

if (found == null)
{
    Console.WriteLine("Nincs ilyen felhasználó!");
    Thread.Sleep(2000);
    return;
}
else
{
    Console.Write("Jelszó: ");
    string password = Console.ReadLine();
    Console.WriteLine("-----------------");
    bool logedin = false;
    for (int i = 1; i <= 3; i++)
    {
        if (PasswordEncryption.VerifyPassword(password, found[1]))
        {
            logedin = true;
            privilege = found[2];
            break;
        }

        Console.WriteLine($"Helytelen jelszó, {3 - i} próbálkozás van hátra!");
        if (i < 3)
        {
            Console.Write("kérem a jelszót újra: ");
            password = Console.ReadLine();
        }

        if (i == 3)
        {
            Console.WriteLine("A program le fog állni!");
            Thread.Sleep(2000);
            return;
        }
    }

    if (logedin)
    {
        Console.WriteLine("Bejelentkezés sikeres");
    }
}


//------------------------------------------------------------------------------


while (true)
{
    if (privilege == "Admin")// ez nem fog kelleni mert bekerjuk ugy is a prevet
    {
        int choice;
        do
        {
            Console.WriteLine($"A jogosultsági kör:{privilege}");
            Console.WriteLine("0. Kilépés || 1. Hozzáadás || 2. Módosítás");

            string input;
            do
            {
                Console.Write("Válassz (0-2): ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out choice) || choice < 0 || choice > 2);

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Viszontlátásra");
                    Thread.Sleep(1000);
                    return;

                case 1:
                    Add();
                    break;

                case 2:
                    // Módosítás logic here
                    break;
            }
        } while (choice != 0);
    }
    if (privilege == "Diak")// ez nem fog kelleni mert bekerjuk ugy is a prevet de igazabol igy is jo
    {
        Console.WriteLine($"A jogosultsági kör:{privilege}");
        retrieve(found);
    }
}
