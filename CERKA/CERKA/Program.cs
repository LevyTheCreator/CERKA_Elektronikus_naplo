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