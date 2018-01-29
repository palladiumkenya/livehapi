using System.Text;
using IQCare.Security;

namespace LiveHAPI.Shared.Custom
{
    public static class Utils
    {
        public static string HasToEndWith(this string value, string end)
        {
            return value.EndsWith(end) ? value : $"{value}{end}";
        }

        public static string Decrypt(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                return string.Empty;

            Decryptor decry = new Decryptor(EncryptionAlgorithm.TripleDes)
            {
                IV = Encoding.ASCII.GetBytes("t3ilc0m3")
            };
            return decry.Decrypt(parameter, "3wmotherwdrtybnio12ewq23");
        }
    }
}