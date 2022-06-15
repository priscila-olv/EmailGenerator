using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailGenerator
{
    public class Program
    {
        private const string emailBase = "@contoso.com";

        static void Main(string[] args)
        {
            List<string> generatedEmails = new List<string>();
            List<string> fullNames = GenerateNames();

            Console.WriteLine("Lista de nomes:");
            Console.WriteLine();

            foreach (var fullName in fullNames)
            {
                var generatedEmail = GenerateEmail(generatedEmails, fullName);
                generatedEmails.Add(generatedEmail);
                Console.WriteLine(fullName);
            }

            Console.WriteLine();
            Console.WriteLine("Lista de e-mails gerados à partir dos nomes: ");
            Console.WriteLine();

            foreach (var emails in generatedEmails)
            {
                Console.WriteLine(emails);
            }

        }

        private static List<string> ValidateName(string fullName)
        {
            List<string> validNames = new List<string>();
            var splittedFullName = RemoveSpecialCharacters(fullName).ToLower().Split(' ').ToList();

            foreach (string name in splittedFullName)
            {
                if (!name.Equals("da") && !name.Equals("de") && !name.Equals("do") && !name.Equals("das") && !name.Equals("dos"))
                    validNames.Add(name);
            }

            return validNames;
        }

        private static string RemoveSpecialCharacters(string words)
        {
            return Encoding.ASCII.GetString(
                Encoding.GetEncoding("Cyrillic").GetBytes(words)
            );

        }

        private static string GenerateEmail(List<string> generatedEmails, string fullName)
        {
            var splittedCompleteName = GetSplittedCompleteName(fullName);
            var firstName = splittedCompleteName.First();
            var lastName = splittedCompleteName.Last();
            var generatedEmail = firstName + "." + lastName + emailBase;
            var emailExists = generatedEmails.FirstOrDefault(e => e.Equals(generatedEmail));

            if (!string.IsNullOrEmpty(emailExists))
            {
                var middleNames = splittedCompleteName.Where(e => !e.Equals(firstName) && !e.Equals(lastName)).ToList();

                foreach (var middleName in middleNames)
                {
                    generatedEmail = firstName + "." + middleName + emailBase;
                    emailExists = generatedEmails.FirstOrDefault(e => e.Equals(generatedEmail));
                    if (string.IsNullOrEmpty(emailExists))
                    {
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(emailExists))
                {
                    generatedEmail = lastName + "." + firstName + emailBase;
                    emailExists = generatedEmails.FirstOrDefault(e => e.Equals(generatedEmail));

                    if (!string.IsNullOrEmpty(emailExists))
                    {
                        var qtdEmailsExistentes = generatedEmails.Where(e => e.Contains(firstName + "." + lastName)).Count();
                        generatedEmail = firstName + "." + lastName + qtdEmailsExistentes + emailBase;
                    }

                }
            }

            return generatedEmail;
        }

        private static List<string> GetSplittedCompleteName(string fullName)
        {
            return ValidateName(fullName);
        }

        private static List<string> GenerateNames()
        {
            List<string> nomesCompletos = new List<string>();

            nomesCompletos.Add("José da Silva Soares");
            nomesCompletos.Add("José Pereira Gomes Soares");
            nomesCompletos.Add("José Pereira Silva Soares");
            nomesCompletos.Add("José Silva Soares");
            nomesCompletos.Add("José Silva");
            nomesCompletos.Add("José Silva");
            nomesCompletos.Add("José Silva");
            nomesCompletos.Add("José Silva");
            return nomesCompletos;
        }
    }
}
