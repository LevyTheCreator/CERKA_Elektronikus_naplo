using System;

string hashedAdminPassword = PasswordEncryption.HashPassword("Menojelszo21");
string bejelentkezesfej = "Username|Password|Privileges\n" + $"sysadmin|{hashedAdminPassword}|Admin\n";
if (!File.Exists("bejelentkezes.txt"))
{
    File.WriteAllText("bejelentkezes.txt", bejelentkezesfej);
    File.SetAttributes("bejelentkezes.txt", FileAttributes.Hidden);
}


Console.WriteLine("Kérem jelentkezzen be!");
Console.WriteLine("-----------------");
Console.Write("Felhasználónév: ");
string username = Console.ReadLine();

string[] sorok = File.ReadAllLines("bejelentkezes.txt");

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

string privilege = null

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
    if (privilege.ToLower == "admin")
    { 
        Console.WriteLine("0. Kilépés || 1. Hozzáadás || 2. Lékérés || 3. Módosítás");
        int input = int.Parse(Console.ReadLine());

        if (input == 1)
        {
            Console.WriteLine("0. Visszalépés || 1. Osztály || 2. Diák");
            int input1 = int.Parse(Console.ReadLine());

            if ( input1 == 1)
            {
                Console.Write("Osztály neve: ")
                string className = Console.ReadLine();
                
                Console.Write("Osztály osztályfőnöke: ");
                string formTeacher = Console.ReadLine();

                Console.Write("Szeretne most diákot is hozzáadni?: ");
                string addStudent = Console.ReadLine();

                List<Student> students = null;
                if (addStudent.ToLower() == "nem")
                {
                    students = new List<Student>(); 
                } 
                // else not complete

                SchoolClass(className, formTeacher, students)
            }
           
        }
    }
}
// ---------------