using System;
using System.ComponentModel;

string hashedAdminPassword = PasswordEncryption.HashPassword("Menojelszo21");
string bejelentkezesfej = "Username|Password|Privileges\n" + $"sysadmin|{hashedAdminPassword}|Admin\n";
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

    string[] reszek = sor.Split("|");
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
    }

    if (bejelentkezett)
    {
        Console.WriteLine("Bejelentkezés sikeres");
    }
}
// to be modified
while (true)
{
    if (privilege.ToLower() == "admin")
    { 
        Console.WriteLine("0. Kilépés || 1. Hozzáadás || 2. Lékérés || 3. Módosítás ");
        string input = Console.ReadLine();
        int choice;

        do
        {
            Console.Write("");
            input = Console.ReadLine();
        }
        while (!int.TryParse(input, out choice) || choice < 0 || choice > 3);

        if (choice == 1)
        {
            void Add()
            {
                Console.WriteLine("0. Visszalépés || 1. Osztály || 2. Diák");
                int input1 = int.Parse(Console.ReadLine());

                if (input1 == 1)
                {
                    Console.Write("Osztály neve: ");
                    string className = Console.ReadLine();

                    Console.Write("Osztály osztályfőnöke: ");
                    string formTeacher = Console.ReadLine();

                    Console.Write("Szeretne most diákot is hozzáadni?: ");
                    string addStudent = Console.ReadLine();

                    
                    if (addStudent.ToLower() == "igen")
                    {
                        Console.Write("Diákok száma: ");
                        int amountOfStudents = int.Parse(Console.ReadLine());

                        List<Student> students = new List<Student>();

                        for (int i = 0; i < amountOfStudents; i++)
                        {
                            Console.Write($"Diák {i + 1} neve: ");
                            string studentName = Console.ReadLine();
                            Console.Write($"Diák {i + 1} azonosítója: ");
                            string studentId = Console.ReadLine();
                    
                            Student student = new Student(studentId, studentName, "N/A", "N/A", "N/A", "N/A", "N/A");   
                            students.Add(student);
                        }

                        SchoolClass schoolClass = new SchoolClass(className, formTeacher, students);  

                        Directory.CreateDirectory($"Osztalyok/{className}");

                        for (int i = 0; i < students.Count; i++)
                        {
                            string studentFilePath = $"Osztalyok/{className}/{students[i].Name}.txt";
                            File.WriteAllText(studentFilePath, $"Név: {students[i].Name}\nAzonosító: {students[i].Id}\n" +
                                $"Jegyek:\n " +
                                $"Matek:{students[i].Math}\n " +
                                $"Irodalom: {students[i].Litrature}\n " +
                                $"Történelem: {students[i].History}\n " +
                                $"Angol: {students[i].English}\n " +
                                $"Nyelvtan: {students[i].Grammer}");
                        }

                    }
                    else
                    {
                        List<Student> students = new List<Student>();

                        SchoolClass schoolClass = new SchoolClass(className, formTeacher, students);

                    }



                    Directory.CreateDirectory($"Osztalyok/{className}");

                }
                Add();
            }
           
        }
    }
}
// ---------------