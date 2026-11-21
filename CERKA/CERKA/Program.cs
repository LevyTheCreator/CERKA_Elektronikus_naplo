using System;
using System.ComponentModel;

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

            Console.Write("Osztály neve: ");
            string className = Console.ReadLine();

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
                            $"Osztalyok/{className}/{s.Name}.txt",
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

                foreach (Student s in students)
                {
                    File.WriteAllText(
                        $"Osztalyok/{whichClass}/{s.Name}.txt",
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
void retrieve()
{

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

string[] sorok = File.ReadAllLines("bejelentkezes.csv");

string[] talalt = null;

foreach (string sor in sorok.Skip(1))
{
    if (string.IsNullOrWhiteSpace(sor))
    {
        continue;
    }

    string[] reszek = sor.Split(";");
    if (reszek.Length == 3 && reszek[0] == username)
    {
        talalt = reszek;
        break;
    }
}

string privilege = "user";

if (talalt == null)
{
    Console.WriteLine("Nincs ilyen felhasználó!");
}
else
{
    Console.Write("Jelszó: ");
    string jelszo = Console.ReadLine();
    Console.WriteLine("-----------------");
    bool bejelentkezett = false;
    byte muvelet = 1;
    for (int i = 1; i <= 3; i++)
    {
        if (PasswordEncryption.VerifyPassword(jelszo, talalt[1]))
        {
            bejelentkezett = true;
            privilege = talalt[2];
            break;
        }

        Console.WriteLine($"Helytelen jelszó, {3 - i} próbálkozás van hátra!");
        if (i < 3)
        {
            Console.Write("kérem a jelszót újra: ");
            jelszo = Console.ReadLine();
        }

        if (i == 3)
        {
            Console.WriteLine("A program le fog állni!");
            Thread.Sleep(2000);
            return;
        }
    }

    if (bejelentkezett)
    {
        Console.WriteLine("Bejelentkezés sikeres");
    }
}


//------------------------------------------------------------------------------


while (true)
{
    if (privilege.ToLower() == "admin")
    {
        int choice;
        do
        {
            Console.WriteLine("0. Kilépés || 1. Hozzáadás || 2. Lekérés || 3. Módosítás");

            string input;
            do
            {
                Console.Write("Válassz (0-3): ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out choice) || choice < 0 || choice > 3);

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
                    // Lékérés logic here
                    break;

                case 3:
                    // Módosítás logic here
                    break;
            }
        } while (choice != 0);
    }
}
