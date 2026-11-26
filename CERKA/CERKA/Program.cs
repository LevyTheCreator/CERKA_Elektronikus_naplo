
using System.Xml;

void add(string[] found)
{
    if (found[2] == "Admin")
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
                DateTime now = DateTime.Now;
                File.AppendAllText("bejelentkezes.csv", formTeacher + ";" + PasswordEncryption.HashPassword(formTeacher.Replace(" ", "") + now.ToString("yyyy")) + ";Tanar;" + className.ToLower() + "\n");

                Console.Write("Szeretne most diákot is hozzáadni?(0 - nem | 1 - igen): ");
                int addStudent = int.Parse(Console.ReadLine());

                switch (addStudent)
                {
                    case 0:

                        List<Student> students = new List<Student>();

                        SchoolClass schoolClass = new SchoolClass(className, formTeacher, students);

                        Directory.CreateDirectory($"Osztalyok/{className.ToLower()}");

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
                            now = DateTime.Now;
                            File.AppendAllText("bejelentkezes.csv", studentId + ";" + PasswordEncryption.HashPassword(studentName.Replace(" ", "") + now.ToString("yyyy")) + ";Diak;" + className.ToLower() + "\n");

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

                        Directory.CreateDirectory($"Osztalyok/{className.ToLower()}");

                        foreach (Student s in students)
                        {
                            File.WriteAllText(
                                $"Osztalyok/{className.ToLower()}/{s.Id}.txt",
                                $"Név: {s.Name}\nAzonosító: {s.Id}\n"
                                    + $"Jegyek:\n"
                                    + $"Matek: {s.Math}\n"
                                    + $"Irodalom: {s.Litrature}\n"
                                    + $"Történelem: {s.History}\n"
                                    + $"Angol: {s.English}\n"
                                    + $"Nyelvtan: {s.Grammer}", System.Text.Encoding.UTF8
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
                    Directory.CreateDirectory($"Osztalyok/{whichClass.ToLower()}");
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
                        now = DateTime.Now;
                        File.AppendAllText("bejelentkezes.csv", studentId + ";" + PasswordEncryption.HashPassword(studentName.Replace(" ", "") + now.ToString("yyyy")) + ";Diak;" + whichClass.ToLower() + "\n");
                        Student student = new Student(
                            studentId,
                            studentName.Replace(" ", "_"),
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
                            $"Osztalyok/{whichClass}/{s.Id}.txt",
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
    
    if (found[2] == "Tanar")
    {
        Console.Write("Diák azonosítója: ");
        string studentId = Console.ReadLine() ;

        Console.WriteLine("Melyik tantárgy? 0. Visszalépés || 1. Matek || 2. Irodalom || 3. Történelem || 4. Angol || 5. Nyelvtan");

        string input;
        int choice;
        do
        {
            Console.Write("Válassz (0-5): ");
            input = Console.ReadLine();

        } while (!int.TryParse(input, out choice) || choice < 0 || choice > 5);


    }
}

void passwordchange(string[] found)
{
    Console.WriteLine("Jelszó megváltoztatása");
    Console.Write("Régi jelszó: ");
    string oldpassword = Console.ReadLine();
    Console.Write("Új jelszó: ");
    string newpassword = Console.ReadLine();
    Console.Write("Új jelszó még egyszer: ");
    string newpassword2 = Console.ReadLine();
    if (PasswordEncryption.VerifyPassword(oldpassword, found[1]))
    {
        if (newpassword != newpassword2)
        {
            Console.WriteLine("A jelszavak nem eggyeznek!!");
        }
        else
        {
            string newhash = PasswordEncryption.HashPassword(newpassword);
            found[1] = newhash;
            string[] userlines = File.ReadAllLines("bejelentkezes.csv");
            for (int i = 0; i < userlines.Length; i++)
            {
                string[] userparts = userlines[i].Split(";");
                if (userparts[0] == found[0])
                {
                    userparts[1] = newhash;
                    userlines[i] = string.Join(";", userparts);
                    break;
                }
            }
            File.SetAttributes("bejelentkezes.csv", ~FileAttributes.Hidden);
            File.WriteAllLines("bejelentkezes.csv", userlines);
            File.SetAttributes("bejelentkezes.csv", FileAttributes.Hidden);
            Console.WriteLine("A jelszó frissítve lett");
        }
    }
    else
    {
        Console.Write("Nem helyes a régi jelszó!!");
    }
    return;
}

void viewgpa(string[] found)
{
    Console.WriteLine("Mit szeretne megtekinteni? 0. Visszalépés || 1. Jegy átlagának megtekintése || 2. Össz átlag megtekintése");
    int choice2;
    string input2;
    do
    {
        Console.Write("Válassz (0-2): ");
        input2 = Console.ReadLine();

    } while (!int.TryParse(input2, out choice2) || choice2 < 0 || choice2 > 2);

    string[] grades = File.ReadAllLines($"Osztalyok/{found[^1]}/{found[0]}.csv").Skip(3).ToArray();
    switch (choice2)
    {
        case 0:
            return;

        case 1:
            Console.WriteLine("Melyik tantárgy? 0. Visszalépés || 1. Matek || 2. Irodalom || 3. Történelem || 4. Angol || 5. Nyelvtan");
            do
            {
                Console.Write("Válassz (0-5): ");
                input2 = Console.ReadLine();

            } while (!int.TryParse(input2, out choice2) || choice2 < 0 || choice2 > 5);

            string gpaCalculator(string subject)
            {
                foreach (string line in grades)
                {
                    string[] subjectAndGrades = line.Split(":");

                    if (subjectAndGrades[0] == subject)
                    {
                        if (subjectAndGrades[1] == "N/A")
                        {
                            return "Nincs még jegye ebből a tantárgyból, ezért nincs átlaga";
                        }
                        else
                        {
                            string[] gradesSplit = subjectAndGrades[1].Split(";");
                            List<int> ints = gradesSplit.Select(int.Parse).ToList();

                            double gpa = ints.Average();
                            return $"{subject} átlága: {gpa}";
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                return $"Nem található ilyen tantárgy: {subject}";
            }

            switch (choice2)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine(gpaCalculator("Matek"));
                    break;
                case 2:
                    Console.WriteLine(gpaCalculator("Irodalom"));
                    break;
                case 3:
                    Console.WriteLine(gpaCalculator("Történelem"));
                    break;
                case 4:
                    Console.WriteLine(gpaCalculator("Angol"));
                    break;
                case 5:
                    Console.WriteLine(gpaCalculator("Nyelvtan"));
                    break;
            }
            break;
        case 2:
            List<double> gpas = new List<double>();
            foreach (string line in grades)
            {
                string[] subjectAndGrades = line.Split(":");

                if (subjectAndGrades[1] == "N/A")
                {
                    continue;
                }
                else
                {
                    string[] gradesSplit = subjectAndGrades[1].Split(";");
                    List<int> ints = gradesSplit.Select(int.Parse).ToList();

                    double gpa = ints.Average();
                    gpas.Add(gpa);
                }
            }
            double gpaAll = gpas.Average();
            Console.WriteLine($"Az össz átlaga: {gpaAll}");
            return;
    }
    return;

}

void viewgrades(string[] found)
{
    string input2;
    int choice2;
    string[] grades = File.ReadAllLines($"Osztalyok/{found[^1]}/{found[0]}.txt").Skip(2).ToArray();
    Console.WriteLine("Melyik tantárgy? 0. Visszalépés || 1. Matek || 2. Irodalom || 3. Történelem || 4. Angol || 5. Nyelvtan || 6. Összes");
    List<string> lines = new List<string>();
    foreach (string line in grades)
    {
        lines.Add(line);
    }
    do
    {

        Console.Write("Válassz (0-6): ");
        input2 = Console.ReadLine();

    } while (!int.TryParse(input2, out choice2) || choice2 < 0 || choice2 > 6);
    switch (choice2)
    {

        case 0:
            break;
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
            Console.WriteLine(lines[choice2]);
            return;
        case 6:
            foreach (string line in grades)
            {
                Console.WriteLine(line);
            }
            return;
    }
}

void modify(string[] found)
{
    if (found[2] == "Admin")
    {
        Console.WriteLine("0. Visszalépés || 1. Azonosító változtatása || 2. Név változtatása");

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
                Console.Write("Diák osztálya (pl. 10.a): ");
                string whichClass = Console.ReadLine();

                Console.Write("Diák jelenlegi azonosítója: ");
                string studentId = Console.ReadLine();

                Console.Write("Diák új azonosítója: ");
                string newStudentId = Console.ReadLine();

                string[] studentFileLines = File.ReadAllLines($"Osztalyok/{whichClass}/{studentId}.csv");

                for (int i = 0; i < studentFileLines.Length; i++)
                {
                    if (studentFileLines[i].StartsWith("Azonosító:"))
                    {
                        studentFileLines[i] = "Azonosító: " + newStudentId;
                    }  
                }

                File.WriteAllLines($"Osztalyok/{whichClass}/{studentId}.csv", studentFileLines);
                File.Move($"Osztalyok/{whichClass}/{studentId}.csv", $"Osztalyok/{whichClass}/{newStudentId}.csv");

                string[] lines = File.ReadAllLines("bejelentkezes.csv");

                for (int j = 0; j < lines.Length; j++)
                {
                    string[] contents = lines[j].Split(";");

                    if (contents[0] == studentId)
                    {
                        lines[j] = $"{newStudentId};{contents[1]};{contents[2]};{contents[3]}";
                    }
                }

                File.WriteAllLines("bejelentkezes.csv", lines);
                break;

            case 2:
                Console.Write("Osztály (pl. 10.a): ");
                whichClass = Console.ReadLine();

                Console.Write("Diák jelenlegi azonosítója: ");
                studentId = Console.ReadLine();

                Console.Write("Diák új neve: ");
                string newStudentName = Console.ReadLine();

                studentFileLines = File.ReadAllLines($"Osztalyok/{whichClass}/{studentId}.csv");

                for (int i = 0; i < studentFileLines.Length; i++)
                {
                    if (studentFileLines[i].StartsWith("Név:"))
                    {
                        studentFileLines[i] = "Név: " + newStudentName.Replace(" ", "_");
                    }
                }

                File.WriteAllLines($"Osztalyok/{whichClass}/{studentId}.csv", studentFileLines);
                break;
        }
    }

    if (found[2] == "Tanár")
    {

    }
}

//------------------------------------------------------------------------------

if (!Directory.Exists("Osztalyok"))
{
    Directory.CreateDirectory("Osztalyok");
}

if (!File.Exists("bejelentkezes.csv"))
{
    string hashedAdminPassword = PasswordEncryption.HashPassword("Menojelszo21");
    string loginhead = "Username;Password;Privileges\n" + $"sysadmin;{hashedAdminPassword};Admin\n";
    File.WriteAllText("bejelentkezes.csv", loginhead);
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
Console.Clear();
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine(" ▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄   ▄▄▄   ▄▄▄   ▄▄▄▄\r\n███▀▀▀▀▀ ███▀▀▀▀▀ ███▀▀███▄ ███ ▄███▀ ▄██▀▀██▄\r\n███      ███▄▄    ███▄▄███▀ ███████   ███  ███\r\n███      ███      ███▀▀██▄  ███▀███▄  ███▀▀███\r\n▀███████ ▀███████ ███  ▀███ ███  ▀███ ███  ███");
Console.WriteLine("----------------------------------------------");
Console.ResetColor();
Console.WriteLine();
//------------------------------------------------------------------------------

Console.WriteLine($"A jogosultsági kör:{privilege}");// elég ha az elején kiírja
while (true)
{
    if (privilege == "Admin")// ez nem fog kelleni mert bekerjuk ugy is a prevet
    {
        int choice;
        do
        {
            Console.WriteLine("0. Kilépés || 1. Hozzáadás || 2. Módosítás || 3. Jelszó módosítása");

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
                    add();
                    break;

                case 2:
                    modify(found);
                    break;

                case 3:
                    passwordchange(found);
                    break;
            }
        } while (choice != 0);
    }
    if (privilege == "Diak")
    {
        Console.WriteLine("mit szeretne tenni? 0. Kilépés || 1. Átlag megtekintése || 2. Jegyek megtekintése || 3. Jelszó módosítása");
        int choice;
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
                Environment.Exit(0);
                break;

            case 1:
                viewgpa(found);
                return;
            case 2:
                viewgrades(found);
                return;
            case 3:
                passwordchange(found);
                return;
        }
    }
    if (privilege == "Tanár")
    {
        Console.WriteLine("mit szeretne tenni? 0. Kilépés || 1. Jegy hozzáadása || 2. Jegy módosítása || 3. Jegyek vagy átlagok megtekintése || 4. Jelszó módosítása");
        int choice;
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
                Environment.Exit(0);
                break;
            case 1:
                add(found);
                return;
            case 2:
                viewgpa(found);
                return;
            case 3:
                return;
            case 4:
                passwordchange(found);
                return;
        }
}
