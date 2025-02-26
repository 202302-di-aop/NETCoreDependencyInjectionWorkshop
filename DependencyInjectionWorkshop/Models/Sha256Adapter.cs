using System.Text;

namespace DependencyInjectionWorkshop.Models;

public interface IHash
{
    string GetHashResult(string password);
}

public class Sha256Adapter : IHash
{
    [Obsolete("Obsolete")]
    public string GetHashResult(string password)
    {
        var crypt = new System.Security.Cryptography.SHA256Managed();
        var hash = new StringBuilder();
        var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
        foreach (var theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }

        return hash.ToString();
    }
}