using System;
using System.Security.Cryptography;
using System.Text;

class HashGenerator
{
    static void Main()
    {
        string password = "cmesa";
        string pswKey = "JC21kkl09gfu89grhdfjhfu77843yurhi7f4yuuh9fghuig2789egrhvuh89fvdhJC894KJ786FG_1HJHFI934NH5DKJI8893"; // Clave de appsettings.json
        
        string passwordHash = HashPassword(password, pswKey);
        Console.WriteLine($"Password: {password}");
        Console.WriteLine($"Hash: {passwordHash}");
    }
    
    static string HashPassword(string password, string pswKey)
    {
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(pswKey)))
        {
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(passwordHash);
        }
    }
}
